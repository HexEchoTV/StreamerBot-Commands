// Copyright (c) 2025 HexEchoTV (CUB)
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// https://github.com/HexEchoTV/Streamerbot-Commands
//
// DEPENDENCIES:
// - ConfigSetup.cs (must be run first to initialize all config variables)
// - youtube-player.html (must be added as a browser source in OBS)
//
// SETUP INSTRUCTIONS:
// 1. Create a browser source in OBS:
//    - Name it "YouTube Player" (or whatever you set in config)
//    - URL: file:///G:/GitHub Projects/StreamerBot-Commands/Utilities/YouTube-Player/youtube-player.html
//    - Width: 1920, Height: 1080
//    - Check "Shutdown source when not visible"
//    - IMPORTANT: Do NOT add any URL parameters - the HTML will read from current-video.json
// 2. Create a Channel Points reward in StreamerBot
// 3. Set this action to trigger on that redemption
// 4. User input should capture the YouTube URL

using System;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;
using System.Net;

public class CPHInline
{
    public bool Execute()
    {
        try
        {
            // Load configuration
            string obsScene = CPH.GetGlobalVar<string>("config_youtube_obs_scene", true);
            string obsSource = CPH.GetGlobalVar<string>("config_youtube_obs_source", true);
            int maxDuration = CPH.GetGlobalVar<int>("config_youtube_max_duration", true);
            if (maxDuration == 0) maxDuration = 600; // Default 10 minutes

            // Get user info
            if (!CPH.TryGetArg("user", out string user))
            {
                CPH.LogError("YouTube Redemption: Missing 'user' argument");
                return false;
            }

            if (!CPH.TryGetArg("userId", out string userId))
            {
                CPH.LogError("YouTube Redemption: Missing 'userId' argument");
                return false;
            }

            // Get YouTube URL from redemption input
            string youtubeUrl = "";
            if (CPH.TryGetArg("rawInput", out string rawInput) && !string.IsNullOrEmpty(rawInput))
            {
                youtubeUrl = rawInput.Trim();
            }
            else if (CPH.TryGetArg("input0", out string input0) && !string.IsNullOrEmpty(input0))
            {
                youtubeUrl = input0.Trim();
            }
            else
            {
                CPH.SendMessage($"{user}, please provide a YouTube URL when redeeming! Example: https://www.youtube.com/watch?v=dQw4w9WgXcQ");
                LogWarning("YouTube Redemption - No URL", $"**User:** {user}");
                return false;
            }

            // Check if this is a YouTube Music URL (for lyrics display)
            bool isMusicVideo = youtubeUrl.Contains("music.youtube.com");

            // Extract video ID from URL
            string videoId = ExtractVideoId(youtubeUrl);

            if (string.IsNullOrEmpty(videoId))
            {
                CPH.SendMessage($"{user}, invalid YouTube URL! Please provide a valid YouTube link.");
                LogWarning("YouTube Redemption - Invalid URL", $"**User:** {user}\n**URL:** {youtubeUrl}");
                return false;
            }

            // Check if a video is actually playing by checking if JSON file exists and is recent
            string jsonPath = System.IO.Path.GetFullPath("G:\\GitHub Projects\\StreamerBot-Commands\\Utilities\\YouTube-Player\\current-video.json");
            bool videoIsPlaying = false;

            if (System.IO.File.Exists(jsonPath))
            {
                try
                {
                    DateTime lastModified = System.IO.File.GetLastWriteTime(jsonPath);
                    TimeSpan timeSinceUpdate = DateTime.Now - lastModified;
                    // If JSON was updated within the last maxDuration seconds, a video is playing
                    videoIsPlaying = timeSinceUpdate.TotalSeconds < maxDuration;
                }
                catch
                {
                    videoIsPlaying = false;
                }
            }

            if (videoIsPlaying)
            {
                // Add to queue instead of rejecting
                AddToQueue(user, videoId, isMusicVideo);
                int queuePosition = GetQueueLength();
                CPH.SendMessage($"📺 {user}'s video added to queue! Position: #{queuePosition}");
                LogInfo("YouTube Redemption - Added to Queue",
                    $"**User:** {user}\n**Video ID:** {videoId}\n**Queue Position:** {queuePosition}");
                return true;
            }

            // Get exact video duration and title from YouTube API (if configured)
            CPH.LogInfo($"Fetching duration for video ID: {videoId}");
            string videoTitle = "";
            string channelName = "";
            int videoDuration = GetExactVideoDuration(videoId, out videoTitle, out channelName);
            CPH.LogInfo($"API returned duration: {videoDuration} seconds");

            // If API failed or not configured, use maxDuration as fallback
            if (videoDuration == 0)
            {
                videoDuration = maxDuration;
                CPH.LogInfo($"Using fallback duration: {maxDuration} seconds");
                CPH.SendMessage($"⚠️ Could not get exact video duration. Playing for up to {maxDuration}s");
            }
            else
            {
                // Cap video duration at maxDuration for safety
                if (videoDuration > maxDuration)
                {
                    CPH.SendMessage($"{user}, that video is {videoDuration}s long! Maximum allowed: {maxDuration}s");
                    LogWarning("YouTube Redemption - Video Too Long",
                        $"**User:** {user}\n**Video ID:** {videoId}\n**Duration:** {videoDuration}s\n**Max:** {maxDuration}s");
                    return false;
                }

                CPH.LogInfo($"Video duration: {videoDuration}s (within {maxDuration}s limit)");
            }

            // Fetch synced lyrics if this is a music video
            string syncedLyrics = "";
            if (isMusicVideo && !string.IsNullOrEmpty(videoTitle))
            {
                CPH.LogInfo($"[LYRICS DEBUG] Detected music video - fetching lyrics for: {videoTitle}");
                syncedLyrics = GetSyncedLyrics(videoTitle, channelName);
                CPH.LogInfo($"[LYRICS DEBUG] Lyrics result - Length: {syncedLyrics.Length} chars");
                if (syncedLyrics.Length > 0)
                {
                    CPH.SendMessage($"[DEBUG] Fetched {syncedLyrics.Split('\n').Length} lines of lyrics");
                }
                else
                {
                    CPH.SendMessage($"[DEBUG] No lyrics found for this song");
                }
            }
            else
            {
                CPH.LogInfo($"[LYRICS DEBUG] Not a music video or no title - isMusicVideo: {isMusicVideo}, hasTitle: {!string.IsNullOrEmpty(videoTitle)}");
            }

            // Play the video immediately
            CPH.LogInfo($"Calling PlayVideo with duration: {videoDuration}s");
            PlayVideo(user, videoId, videoTitle, obsScene, obsSource, videoDuration, syncedLyrics);

            return true;
        }
        catch (Exception ex)
        {
            LogError("YouTube Redemption Exception",
                $"**Error:** {ex.Message}\n**Stack Trace:** {ex.StackTrace}");

            CPH.SendMessage("⚠️ An error occurred while playing the video");
            CPH.LogError($"YouTube Redemption error: {ex.Message}");
            return false;
        }
    }

    // ═══════════════════════════════════════════════════════════
    // QUEUE MANAGEMENT METHODS
    // ═══════════════════════════════════════════════════════════

    private void AddToQueue(string user, string videoId, bool isMusicVideo)
    {
        // Get current queue (format: "user1:videoId1:isMusicFlag|user2:videoId2:isMusicFlag|...")
        // isMusicFlag: 1 = music video, 0 = regular video
        string musicFlag = isMusicVideo ? "1" : "0";
        string queue = CPH.GetGlobalVar<string>("youtube_video_queue", false);
        if (string.IsNullOrEmpty(queue))
        {
            queue = $"{user}:{videoId}:{musicFlag}";
        }
        else
        {
            queue += $"|{user}:{videoId}:{musicFlag}";
        }
        CPH.SetGlobalVar("youtube_video_queue", queue, false);
    }

    private int GetQueueLength()
    {
        string queue = CPH.GetGlobalVar<string>("youtube_video_queue", false);
        if (string.IsNullOrEmpty(queue))
            return 0;

        return queue.Split('|').Length;
    }

    private void ProcessQueue(string obsScene, string obsSource, int maxDuration)
    {
        string queue = CPH.GetGlobalVar<string>("youtube_video_queue", false);

        if (string.IsNullOrEmpty(queue))
        {
            // Queue is empty, hide the source and delete JSON file
            CPH.ObsSetSourceVisibility(obsScene, obsSource, false, 0);

            // Delete JSON file so next redemption plays immediately
            string jsonPath = System.IO.Path.GetFullPath("G:\\GitHub Projects\\StreamerBot-Commands\\Utilities\\YouTube-Player\\current-video.json");
            try
            {
                if (System.IO.File.Exists(jsonPath))
                {
                    System.IO.File.Delete(jsonPath);
                }
            }
            catch { }

            LogInfo("YouTube Queue", "Queue empty - hiding source");
            return;
        }

        // Get the next video from queue
        string[] queueItems = queue.Split('|');
        string nextItem = queueItems[0];
        string[] parts = nextItem.Split(':');
        string user = parts[0];
        string videoId = parts[1];
        // Parse music video flag (3rd part) - 1 = music video, 0 = regular video
        bool isMusicVideo = parts.Length > 2 && parts[2] == "1";

        // Remove this item from queue
        if (queueItems.Length == 1)
        {
            CPH.SetGlobalVar("youtube_video_queue", "", false);
        }
        else
        {
            string newQueue = string.Join("|", queueItems, 1, queueItems.Length - 1);
            CPH.SetGlobalVar("youtube_video_queue", newQueue, false);
        }

        // Get exact video duration and title from YouTube API (if configured)
        string videoTitle = "";
        string channelName = "";
        int videoDuration = GetExactVideoDuration(videoId, out videoTitle, out channelName);

        // If API failed or not configured, use maxDuration as fallback
        if (videoDuration == 0)
        {
            videoDuration = maxDuration;
        }

        CPH.LogInfo($"[QUEUE] Music video flag from queue: {isMusicVideo}, Channel: '{channelName}'");
        CPH.SendMessage($"[DEBUG QUEUE] Music Flag: {isMusicVideo} | Channel: '{channelName}'");

        // Fetch synced lyrics if this is a music video (based on queue flag)
        string syncedLyrics = "";
        if (isMusicVideo && !string.IsNullOrEmpty(videoTitle))
        {
            CPH.LogInfo($"[QUEUE] Fetching lyrics for queued music video: {videoTitle}");
            CPH.SendMessage($"[DEBUG QUEUE] Fetching lyrics for: {videoTitle}");
            syncedLyrics = GetSyncedLyrics(videoTitle, channelName);
            if (syncedLyrics.Length > 0)
            {
                CPH.SendMessage($"[DEBUG QUEUE] ✅ Found {syncedLyrics.Split('\n').Length} lines of lyrics!");
            }
            else
            {
                CPH.SendMessage($"[DEBUG QUEUE] ❌ No lyrics found for this song");
            }
        }
        else
        {
            if (!isMusicVideo)
            {
                CPH.SendMessage($"[DEBUG QUEUE] Not detected as music video - no lyrics fetched");
            }
        }

        // Play the next video
        int remaining = queueItems.Length - 1;
        CPH.SendMessage($"🎬 Next video: {user}'s video is now playing! ({remaining} videos left in queue)");
        PlayVideo(user, videoId, videoTitle, obsScene, obsSource, videoDuration, syncedLyrics);
    }

    private void PlayVideo(string user, string videoId, string videoTitle, string obsScene, string obsSource, int maxDuration, string syncedLyrics = "")
    {
        CPH.LogInfo($"=== PlayVideo START === User: {user}, VideoID: {videoId}, Title: {videoTitle}, Duration: {maxDuration}s, HasLyrics: {!string.IsNullOrEmpty(syncedLyrics)}");

        LogSuccess("YouTube Redemption Started",
            $"**User:** {user}\n**Video ID:** {videoId}\n**Title:** {videoTitle}\n**URL:** https://www.youtube.com/watch?v={videoId}");

        CPH.SendMessage($"🎬 {user}'s video is now playing! Video ID: {videoId}");

        // Get progress bar color from config
        string progressColor = CPH.GetGlobalVar<string>("config_youtube_progress_color", true);
        if (string.IsNullOrEmpty(progressColor))
        {
            progressColor = "#FF0000"; // Default to red if not set
        }

        // Get lyrics timing offset from config
        double lyricsOffset = CPH.GetGlobalVar<double>("config_youtube_lyrics_offset", true);

        // Escape video title and username for JSON (handle quotes and backslashes)
        string escapedTitle = EscapeJsonString(videoTitle);
        string escapedUser = EscapeJsonString(user);
        string escapedLyrics = EscapeJsonString(syncedLyrics);

        // Write video ID to a JSON file that the HTML will poll
        string jsonPath = System.IO.Path.GetFullPath("G:\\GitHub Projects\\StreamerBot-Commands\\Utilities\\YouTube-Player\\current-video.json");
        string jsonContent = $"{{\"videoId\":\"{videoId}\",\"timestamp\":{DateTime.UtcNow.Ticks},\"progressColor\":\"{progressColor}\",\"title\":\"{escapedTitle}\",\"requestedBy\":\"{escapedUser}\",\"lyrics\":\"{escapedLyrics}\",\"lyricsOffset\":{lyricsOffset}}}";

        CPH.LogInfo($"Writing JSON to: {jsonPath}");
        CPH.LogInfo($"[LYRICS DEBUG] Lyrics in JSON - Length: {escapedLyrics.Length} chars");
        CPH.LogInfo($"JSON content: {jsonContent.Substring(0, Math.Min(200, jsonContent.Length))}...");

        try
        {
            System.IO.File.WriteAllText(jsonPath, jsonContent);
            CPH.LogInfo($"✅ Successfully wrote video ID to JSON");
        }
        catch (Exception ex)
        {
            CPH.LogError($"❌ Failed to write video JSON: {ex.Message}");
            CPH.LogError($"Stack trace: {ex.StackTrace}");
        }

        // Small delay to ensure file is written
        System.Threading.Thread.Sleep(200);

        // Show the source
        CPH.LogInfo($"Showing OBS source: Scene='{obsScene}', Source='{obsSource}'");
        CPH.ObsSetSourceVisibility(obsScene, obsSource, true, 0);
        CPH.LogInfo($"OBS source visibility set to: true");

        // Monitor for video end using exact duration
        Task.Run(async () =>
        {
            string monitorId = Guid.NewGuid().ToString().Substring(0, 8);
            CPH.LogInfo($"[MONITOR-{monitorId}] Started monitoring video {videoId} for {maxDuration}s");

            DateTime startTime = DateTime.Now;
            TimeSpan elapsed = TimeSpan.Zero;
            const int checkInterval = 2000; // Check every 2 seconds

            while (true)
            {
                await Task.Delay(checkInterval);
                elapsed = DateTime.Now - startTime;

                // Check if JSON was replaced (new video started) - FIRST priority
                if (System.IO.File.Exists(jsonPath))
                {
                    try
                    {
                        string currentJson = System.IO.File.ReadAllText(jsonPath);
                        // More robust check: look for exact "videoId":"XXX" pattern
                        string searchPattern = $"\"videoId\":\"{videoId}\"";
                        if (!currentJson.Contains(searchPattern))
                        {
                            CPH.LogInfo($"[MONITOR-{monitorId}] New video detected in JSON, stopping monitoring for {videoId}");
                            return; // Don't process queue, new video is already playing
                        }
                    }
                    catch (Exception ex)
                    {
                        CPH.LogError($"[MONITOR-{monitorId}] Error reading JSON: {ex.Message}");
                    }
                }

                // Check if source was manually hidden (stop command)
                bool isVisible = CPH.ObsIsSourceVisible(obsScene, obsSource, 0);
                if (!isVisible && elapsed.TotalSeconds > 3)
                {
                    CPH.LogInfo($"[MONITOR-{monitorId}] Source hidden externally after {elapsed.TotalSeconds:F1}s, stopping monitoring");
                    return; // Don't process queue if manually stopped
                }

                // Check if duration reached (add 2 second buffer for safety)
                if (elapsed.TotalSeconds >= maxDuration + 2)
                {
                    CPH.LogInfo($"[MONITOR-{monitorId}] Duration reached: {elapsed.TotalSeconds:F1}s >= {maxDuration}s (+2s buffer)");
                    break;
                }
            }

            CPH.LogInfo($"[MONITOR-{monitorId}] Video {videoId} ended naturally after {elapsed.TotalSeconds:F1}s");
            LogInfo("YouTube Video Ended",
                $"**User:** {user}\n**Video ID:** {videoId}\n**Duration:** {elapsed.TotalSeconds:F1}s / {maxDuration}s");

            // Check queue for next video
            ProcessQueue(obsScene, obsSource, maxDuration);
        });
    }

    // ═══════════════════════════════════════════════════════════
    // YOUTUBE API - VIDEO DURATION FETCHING
    // ═══════════════════════════════════════════════════════════

    private int GetExactVideoDuration(string videoId, out string videoTitle, out string channelName)
    {
        videoTitle = ""; // Initialize out parameter
        channelName = ""; // Initialize out parameter

        try
        {
            string apiKey = CPH.GetGlobalVar<string>("config_youtube_api_key", true);
            CPH.LogInfo($"YouTube API key retrieved: {(string.IsNullOrEmpty(apiKey) ? "EMPTY" : "EXISTS")}");

            // DEBUG: Send to chat
            CPH.SendMessage($"[DEBUG] API Key: {(string.IsNullOrEmpty(apiKey) ? "EMPTY" : "EXISTS (last 4: ..." + apiKey.Substring(Math.Max(0, apiKey.Length - 4)) + ")")}");

            // If no API key configured, return 0 (will use fallback monitoring)
            if (string.IsNullOrEmpty(apiKey) || apiKey == "YOUR_YOUTUBE_API_KEY_HERE")
            {
                CPH.LogInfo("YouTube API key not configured - using fallback duration monitoring");
                CPH.SendMessage("[DEBUG] API key not configured - using fallback");
                return 0;
            }

            // Query YouTube Data API v3 - request both contentDetails (duration) and snippet (title)
            string url = $"https://www.googleapis.com/youtube/v3/videos?id={videoId}&key={apiKey}&part=contentDetails,snippet";
            CPH.LogInfo($"Calling YouTube API: {url.Replace(apiKey, "***KEY***")}");

            using (WebClient client = new WebClient())
            {
                client.Headers.Add("User-Agent", "StreamerBot-YouTube/1.0");
                CPH.LogInfo("Downloading API response...");
                string response = client.DownloadString(url);
                CPH.LogInfo($"API Response length: {response.Length} chars");

                // DEBUG: Send response snippet to chat
                string responseSnippet = response.Length > 150 ? response.Substring(0, 150) + "..." : response;
                CPH.SendMessage($"[DEBUG] API Response: {responseSnippet}");

                // Parse JSON response manually (no JSON library in StreamerBot)
                // Look for "duration": "PT..." (note: space after colon)
                int durationStart = response.IndexOf("\"duration\": \"");
                if (durationStart == -1)
                {
                    // Try without space for backwards compatibility
                    durationStart = response.IndexOf("\"duration\":\"");
                    CPH.SendMessage("[DEBUG] Trying duration parse WITHOUT space");
                }
                else
                {
                    CPH.SendMessage("[DEBUG] Found duration WITH space");
                }

                if (durationStart > -1)
                {
                    durationStart = response.IndexOf("\"", durationStart + 11) + 1; // Find opening quote of value
                }

                int durationEnd = response.IndexOf("\"", durationStart);

                CPH.LogInfo($"Duration parse: start={durationStart}, end={durationEnd}");
                CPH.SendMessage($"[DEBUG] Parse positions - start: {durationStart}, end: {durationEnd}");

                if (durationStart > 0 && durationEnd > durationStart)
                {
                    string duration = response.Substring(durationStart, durationEnd - durationStart);
                    CPH.LogInfo($"Extracted duration string: {duration}");
                    int seconds = ParseISO8601Duration(duration);

                    CPH.LogInfo($"YouTube API: Video duration = {seconds} seconds ({duration})");
                    CPH.SendMessage($"[DEBUG] Extracted: '{duration}' = {seconds} seconds");

                    // Parse video title from snippet
                    int titleStart = response.IndexOf("\"title\": \"");
                    if (titleStart == -1)
                    {
                        // Try without space
                        titleStart = response.IndexOf("\"title\":\"");
                    }

                    if (titleStart > -1)
                    {
                        titleStart = response.IndexOf("\"", titleStart + 9) + 1; // Find opening quote of value
                        int titleEnd = response.IndexOf("\"", titleStart);

                        if (titleEnd > titleStart)
                        {
                            videoTitle = response.Substring(titleStart, titleEnd - titleStart);
                            // Unescape JSON characters
                            videoTitle = videoTitle.Replace("\\\"", "\"").Replace("\\\\", "\\");
                            CPH.LogInfo($"Extracted video title: {videoTitle}");
                        }
                    }

                    // Parse channel name from snippet (this is usually the artist for music videos)
                    int channelStart = response.IndexOf("\"channelTitle\": \"");
                    if (channelStart == -1)
                    {
                        // Try without space
                        channelStart = response.IndexOf("\"channelTitle\":\"");
                    }

                    if (channelStart > -1)
                    {
                        channelStart = response.IndexOf("\"", channelStart + 16) + 1; // Find opening quote of value
                        int channelEnd = response.IndexOf("\"", channelStart);

                        if (channelEnd > channelStart)
                        {
                            channelName = response.Substring(channelStart, channelEnd - channelStart);
                            // Unescape JSON characters
                            channelName = channelName.Replace("\\\"", "\"").Replace("\\\\", "\\");
                            // Clean up common suffixes from channel names
                            channelName = channelName.Replace(" - Topic", "").Replace("VEVO", "").Replace("Official", "").Trim();
                            CPH.LogInfo($"Extracted channel name: {channelName}");
                            CPH.SendMessage($"[DEBUG] Channel: {channelName}");
                        }
                    }

                    return seconds;
                }
                else
                {
                    CPH.LogError("Could not find duration in API response");
                    CPH.LogInfo($"Response snippet: {response.Substring(0, Math.Min(200, response.Length))}");
                    CPH.SendMessage("[DEBUG] ERROR: Could not find duration in response!");
                }
            }
        }
        catch (Exception ex)
        {
            CPH.LogError($"Failed to fetch video duration from YouTube API: {ex.Message}");
            CPH.LogError($"Stack trace: {ex.StackTrace}");
            CPH.SendMessage($"[DEBUG] API EXCEPTION: {ex.Message}");
        }

        return 0; // Return 0 on failure (will use fallback)
    }

    private int ParseISO8601Duration(string duration)
    {
        // Parse ISO 8601 duration format: PT4M33S, PT1H2M3S, PT45S, etc.
        // PT = Period of Time
        // H = Hours, M = Minutes, S = Seconds

        try
        {
            int totalSeconds = 0;

            // Remove "PT" prefix
            if (duration.StartsWith("PT"))
                duration = duration.Substring(2);

            // Extract hours
            if (duration.Contains("H"))
            {
                int hIndex = duration.IndexOf("H");
                string hours = duration.Substring(0, hIndex);
                totalSeconds += int.Parse(hours) * 3600;
                duration = duration.Substring(hIndex + 1);
            }

            // Extract minutes
            if (duration.Contains("M"))
            {
                int mIndex = duration.IndexOf("M");
                string minutes = duration.Substring(0, mIndex);
                totalSeconds += int.Parse(minutes) * 60;
                duration = duration.Substring(mIndex + 1);
            }

            // Extract seconds
            if (duration.Contains("S"))
            {
                int sIndex = duration.IndexOf("S");
                string seconds = duration.Substring(0, sIndex);
                totalSeconds += int.Parse(seconds);
            }

            return totalSeconds;
        }
        catch
        {
            return 0;
        }
    }

    // ═══════════════════════════════════════════════════════════
    // MUSIXMATCH API - SYNCED LYRICS FETCHING
    // ═══════════════════════════════════════════════════════════

    private string GetSyncedLyrics(string songTitle, string channelName)
    {
        try
        {
            // Check if lyrics are enabled in config
            bool lyricsEnabled = CPH.GetGlobalVar<bool>("config_youtube_lyrics_enabled", true);

            CPH.LogInfo($"[LYRICS DEBUG] GetSyncedLyrics called - Enabled: {lyricsEnabled}, Title: {songTitle}, Channel: {channelName}");

            // If lyrics disabled, skip (video will still play normally)
            if (!lyricsEnabled)
            {
                CPH.LogInfo("Lyrics disabled in config - skipping lyrics");
                CPH.SendMessage("[DEBUG] Lyrics are disabled in config");
                return "";
            }

            CPH.LogInfo($"Fetching lyrics for: {songTitle}");
            CPH.SendMessage($"[DEBUG] Full video title: {songTitle}");

            // Try to parse artist and song from title
            // Common formats: "Artist - Song", "Song by Artist", "Artist: Song"
            string artist = "";
            string song = songTitle;

            if (songTitle.Contains(" - "))
            {
                string[] parts = songTitle.Split(new[] { " - " }, 2, StringSplitOptions.None);
                artist = parts[0].Trim();
                song = parts[1].Trim();
            }
            else if (songTitle.Contains(" by "))
            {
                string[] parts = songTitle.Split(new[] { " by " }, 2, StringSplitOptions.None);
                song = parts[0].Trim();
                artist = parts[1].Trim();
            }

            // Clean up artist and song names (remove common suffixes)
            if (song.Contains("(Official"))
            {
                song = song.Substring(0, song.IndexOf("(Official")).Trim();
            }
            if (song.Contains("[Official"))
            {
                song = song.Substring(0, song.IndexOf("[Official")).Trim();
            }

            // If we couldn't parse an artist from title, use the channel name
            if (string.IsNullOrEmpty(artist) && !string.IsNullOrEmpty(channelName))
            {
                artist = channelName;
                CPH.LogInfo($"[LYRICS DEBUG] Using channel name as artist: {artist}");
                CPH.SendMessage($"[DEBUG] Using channel '{channelName}' as artist");
            }

            // If we still don't have an artist, we can't fetch lyrics
            if (string.IsNullOrEmpty(artist))
            {
                CPH.LogInfo($"[LYRICS DEBUG] Could not determine artist from title or channel");
                CPH.SendMessage($"[DEBUG] Could not determine artist - no channel name available");
                return "";
            }

            // Use LRCLIB API (completely free, provides timestamped lyrics)
            string lyricsUrl = $"https://lrclib.net/api/get?artist_name={System.Uri.EscapeDataString(artist)}&track_name={System.Uri.EscapeDataString(song)}";

            CPH.LogInfo($"Fetching from LRCLIB: {artist} - {song}");
            CPH.LogInfo($"[LYRICS DEBUG] API URL: {lyricsUrl}");
            CPH.SendMessage($"[DEBUG] Searching lyrics: {artist} - {song}");

            using (WebClient client = new WebClient())
            {
                client.Headers.Add("User-Agent", "StreamerBot-YouTube/1.0");
                string response = client.DownloadString(lyricsUrl);

                CPH.LogInfo($"[LYRICS DEBUG] LRCLIB Response length: {response.Length} chars");

                // Parse synced lyrics from response (LRC format with timestamps)
                int syncedStart = response.IndexOf("\"syncedLyrics\":\"");
                if (syncedStart == -1)
                {
                    // Try with space
                    syncedStart = response.IndexOf("\"syncedLyrics\": \"");
                }

                if (syncedStart == -1)
                {
                    // No synced lyrics, try plain lyrics as fallback
                    CPH.LogInfo("No synced lyrics found, trying plain lyrics");
                    int plainStart = response.IndexOf("\"plainLyrics\":\"");
                    if (plainStart == -1)
                    {
                        plainStart = response.IndexOf("\"plainLyrics\": \"");
                    }

                    if (plainStart > -1)
                    {
                        plainStart = response.IndexOf("\"", plainStart + 14) + 1;
                        int plainEnd = response.IndexOf("\"", plainStart);

                        if (plainEnd > plainStart)
                        {
                            string lyrics = response.Substring(plainStart, plainEnd - plainStart);
                            lyrics = lyrics.Replace("\\n", "\n").Replace("\\r", "").Replace("\\\"", "\"").Replace("\\\\", "\\");

                            int lineCount = lyrics.Split('\n').Length;
                            CPH.LogInfo($"Successfully fetched {lineCount} lines of plain lyrics (no timestamps)");
                            CPH.SendMessage($"[DEBUG] Found plain lyrics ({lineCount} lines, no timestamps)");
                            return lyrics;
                        }
                    }

                    CPH.LogInfo("No lyrics found in API response");
                    CPH.SendMessage("[DEBUG] No lyrics found");
                    return "";
                }

                syncedStart = response.IndexOf("\"", syncedStart + 15) + 1;
                int syncedEnd = response.IndexOf("\"", syncedStart);

                if (syncedEnd > syncedStart)
                {
                    string lyrics = response.Substring(syncedStart, syncedEnd - syncedStart);
                    // Unescape JSON characters
                    lyrics = lyrics.Replace("\\n", "\n").Replace("\\r", "").Replace("\\\"", "\"").Replace("\\\\", "\\");

                    int lineCount = lyrics.Split('\n').Length;
                    CPH.LogInfo($"Successfully fetched {lineCount} lines of SYNCED lyrics with timestamps");
                    CPH.SendMessage($"[DEBUG] Found synced lyrics ({lineCount} lines with timestamps)");
                    return lyrics;
                }
            }
        }
        catch (Exception ex)
        {
            CPH.LogError($"Failed to fetch lyrics: {ex.Message}");
        }

        return "";
    }

    // ═══════════════════════════════════════════════════════════
    // VIDEO ID EXTRACTION
    // ═══════════════════════════════════════════════════════════

    private string ExtractVideoId(string url)
    {
        try
        {
            // Handle different YouTube URL formats
            // https://www.youtube.com/watch?v=VIDEO_ID
            // https://www.youtube.com/watch?v=VIDEO_ID&list=... (playlist/radio links)
            // https://music.youtube.com/watch?v=VIDEO_ID
            // https://youtu.be/VIDEO_ID
            // https://www.youtube.com/embed/VIDEO_ID
            // https://m.youtube.com/watch?v=VIDEO_ID

            // Pattern 1: youtube.com/watch?v=VIDEO_ID (with optional extra params like &list=, &start_radio=, etc)
            var match = Regex.Match(url, @"(?:music\.)?youtube\.com\/watch\?v=([a-zA-Z0-9_-]{11})(?:&|$)");

            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            // Pattern 2: youtu.be/VIDEO_ID or youtube.com/embed/VIDEO_ID
            match = Regex.Match(url, @"(?:youtu\.be\/|youtube\.com\/embed\/)([a-zA-Z0-9_-]{11})(?:\?|&|$)");

            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            // Pattern 3: Just the video ID was provided
            if (Regex.IsMatch(url, @"^[a-zA-Z0-9_-]{11}$"))
            {
                return url;
            }

            // Pattern 4: youtube.com/v/VIDEO_ID
            match = Regex.Match(url, @"(?:music\.)?youtube\.com\/v\/([a-zA-Z0-9_-]{11})(?:\?|&|$)");
            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            return null;
        }
        catch
        {
            return null;
        }
    }

    // ═══════════════════════════════════════════════════════════
    // DISCORD LOGGING METHODS
    // ═══════════════════════════════════════════════════════════

    private const int COLOR_INFO = 3447003;      // Blue
    private const int COLOR_SUCCESS = 5763719;   // Green
    private const int COLOR_WARNING = 16705372;  // Orange
    private const int COLOR_ERROR = 15548997;    // Red
    private const int COLOR_COMMAND = 10181046;  // Purple

    private void LogInfo(string title, string message)
    {
        SendToDiscord(title, message, COLOR_INFO, "INFO");
    }

    private void LogSuccess(string title, string message)
    {
        SendToDiscord(title, message, COLOR_SUCCESS, "SUCCESS");
    }

    private void LogWarning(string title, string message)
    {
        SendToDiscord(title, message, COLOR_WARNING, "WARNING");
    }

    private void LogError(string title, string message)
    {
        SendToDiscord(title, message, COLOR_ERROR, "ERROR");
    }

    private void LogCommand(string commandName, string user, string details = "")
    {
        string message = $"**User:** {user}";
        if (!string.IsNullOrEmpty(details))
        {
            message += $"\n**Details:** {details}";
        }
        SendToDiscord($"Command: {commandName}", message, COLOR_COMMAND, "COMMAND");
    }

    private void SendToDiscord(string title, string description, int color, string footer)
    {
        try
        {
            // Check if logging is enabled (global toggle)
            bool loggingEnabled = CPH.GetGlobalVar<bool>("discordLoggingEnabled", true);
            if (!loggingEnabled)
            {
                return; // Logging is disabled
            }

            // Get webhook URL from global variable (set in ConfigSetup.cs)
            string webhookUrl = CPH.GetGlobalVar<string>("discordLogWebhook", true);

            if (string.IsNullOrEmpty(webhookUrl))
            {
                CPH.LogWarn("Discord webhook not configured. Run ConfigSetup.cs first.");
                return;
            }

            // Escape special characters for JSON
            title = EscapeJson(title);
            description = EscapeJson(description);
            footer = EscapeJson(footer);

            // Get current timestamp in ISO format
            string timestamp = DateTime.UtcNow.ToString("o");

            // Build Discord embed JSON manually (no JSON library in StreamerBot)
            StringBuilder json = new StringBuilder();
            json.Append("{");
            json.Append("\"embeds\":[{");
            json.Append($"\"title\":\"{title}\",");
            json.Append($"\"description\":\"{description}\",");
            json.Append($"\"color\":{color},");
            json.Append($"\"timestamp\":\"{timestamp}\",");
            json.Append("\"footer\":{");
            json.Append($"\"text\":\"{footer} | HexEchoTV Logging System\"");
            json.Append("}");
            json.Append("}]");
            json.Append("}");

            // Send to Discord webhook
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                client.Headers.Add("Content-Type", "application/json");
                client.UploadString(webhookUrl, "POST", json.ToString());
            }
        }
        catch (Exception ex)
        {
            CPH.LogError($"DiscordLogger error: {ex.Message}");
        }
    }

    private string EscapeJsonString(string str)
    {
        if (string.IsNullOrEmpty(str))
            return "";

        return str
            .Replace("\\", "\\\\")   // Backslash MUST be first
            .Replace("\"", "\\\"")   // Escape quotes
            .Replace("\n", "\\n")    // Newlines
            .Replace("\r", "\\r")    // Carriage returns
            .Replace("\t", "\\t")    // Tabs
            .Replace("\b", "\\b")    // Backspace
            .Replace("\f", "\\f");   // Form feed
    }

    private string EscapeJson(string str)
    {
        if (string.IsNullOrEmpty(str))
            return "";

        return str
            .Replace("\\", "\\\\")
            .Replace("\"", "\\\"")
            .Replace("\n", "\\n")
            .Replace("\r", "\\r")
            .Replace("\t", "\\t");
    }
}
