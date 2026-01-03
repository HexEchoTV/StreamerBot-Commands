# YouTube Video Redemption Setup Guide

This feature allows viewers to redeem channel points to play YouTube videos on your stream!

## 🎬 Features

### Core Playback
- Full-screen YouTube video playback (1920x1080)
- Auto-show when video starts, auto-hide when finished
- **Smart duration detection** - Videos play their exact length using YouTube Data API
- Configurable scene and source names
- Maximum duration limit to prevent abuse
- **YouTube Music support** - Full support for music.youtube.com URLs

### Visual Display
- **Progress bar with time display** - Shows video progress and current/total time (e.g., "1:23 / 4:56")
- **Video title display** - Shows the video title at the top center
- **"Requested by" display** - Shows which user requested the video
- **Customizable progress bar color** - Change via global variables (hex color codes)
- **Captions disabled by default** - All videos play without subtitles for cleaner presentation

### Advanced Lyrics System 🎵
- **Ultra-precise synchronized lyrics** - Updates 1000 times per second (1ms precision) for perfect sync
- **Timestamped karaoke-style display** - Current line highlighted, past lines fade out
- **Automatic text wrapping** - Long lyrics wrap to multiple lines, up to 95% screen width
- **Smart artist detection** - Parses artist from video title or channel name
- **LRCLIB API integration** - Free timestamped lyrics (no API key needed!)
- **Fallback to plain text** - If timestamped lyrics not available
- **Queue-aware** - Music video flag preserved through queue system
- **Automatic initialization** - Lyrics start at first line, no jumping

### Queue System 📺
- **Automatic video queuing** - Multiple redemptions queue up automatically
- **FIFO processing** - First in, first out order
- **Queue position display** - Users see their position: "📺 Position: #3"
- **Smart queue processing** - Videos auto-advance when current video ends
- **Music video flag preservation** - Queue remembers if video is from music.youtube.com
- **Chat notifications** - "🎬 Next video: Username's video is now playing! (2 videos left)"
- **Moderator skip command** - `!stopvideo` to skip to next in queue

### Technical Features
- **YouTube Data API integration** - Exact video duration and metadata (requires free API key)
- **Robust monitoring system** - Unique task IDs, 2-second check intervals, +2 second safety buffer
- **Advanced JSON escaping** - Handles special characters, backslashes, quotes, control characters
- **Discord logging integration** - Track all redemptions and errors
- **Multiple URL format support** - youtube.com, youtu.be, music.youtube.com, embed, mobile

---

## 📋 Setup Instructions

### Step 1: Configure Settings

1. Run `ConfigSetup.cs` in StreamerBot
2. Edit these settings in the file:
   ```csharp
   // Lines 402-403: OBS Integration
   string youtubeObsScene = "Main Scene";     // Change to YOUR scene name
   string youtubeObsSource = "YouTube Player"; // Change to YOUR source name

   // Lines 408-409: Video Settings
   int youtubeRedemptionCost = 500;           // Cost in currency (if using chat command)
   int youtubeMaxDuration = 600;              // Max video length in seconds (10 min default)

   // Line 415: Progress Bar
   string youtubeProgressColor = "#FF0000";   // Progress bar color (hex code, default red)

   // Line 421: YouTube Data API
   string youtubeApiKey = "YOUR_YOUTUBE_API_KEY_HERE"; // See Step 1B below

   // Line 427: Lyrics System
   bool youtubeLyricsEnabled = true;          // Enable/disable lyrics globally

   // Line 433: Lyrics Timing
   double youtubeLyricsOffset = 0.0;          // Timing offset in seconds (+ = later, - = earlier)
   ```
3. Re-run `ConfigSetup.cs` after making changes

### Step 1B: Get YouTube API Key (Required for Best Experience)

The YouTube Data API provides exact video durations, titles, and artist detection:

1. Go to [Google Cloud Console](https://console.cloud.google.com/)
2. Create a new project or select an existing one
3. Enable **YouTube Data API v3**:
   - Go to **APIs & Services** → **Library**
   - Search for "YouTube Data API v3"
   - Click **Enable**
4. Create API credentials:
   - Go to **APIs & Services** → **Credentials**
   - Click **Create Credentials** → **API Key**
   - Copy the API key
5. Paste the API key into `ConfigSetup.cs` (line 421):
   ```csharp
   string youtubeApiKey = "AIzaSyC1234567890abcdefghijklmnopqrstu"; // Your key here
   ```
6. Re-run `ConfigSetup.cs` to save

**Note:** The YouTube Data API is free with 10,000 requests/day quota (more than enough for streaming)

### Step 2: Add Browser Source to OBS

1. Open OBS
2. Go to the scene specified in config (e.g., "Main Scene")
3. Add a **Browser** source
4. Configure the browser source:
   - **Name:** `YouTube Player` (must match your config)
   - **URL:** `file:///G:/GitHub Projects/StreamerBot-Commands/Utilities/YouTube-Player/youtube-player.html`
     - ⚠️ **Change the path** to match your actual file location!
   - **Width:** `1920`
   - **Height:** `1080`
   - **FPS:** `30`
   - ✅ Check **"Shutdown source when not visible"**
   - ✅ Check **"Refresh browser when scene becomes active"** (optional)
5. **CRITICAL:** Add Custom CSS or Command-Line Arguments:
   - Look for **"Custom CSS"** or **"Chromium Command-Line Arguments"** field
   - Add this argument: `--allow-file-access-from-files`
   - This allows the browser to read the JSON file with video data
   - Without this, the system **will not work**!
6. Position and size the source to fill your entire screen (1920x1080)
7. **Make sure the source is HIDDEN by default** (click the eye icon to hide it)

**Troubleshooting OBS Setup:**
- If you don't see a command-line arguments field:
  - Some OBS versions have it under "Advanced" or at the bottom of properties
  - In OBS Studio, it's typically in the browser source properties
- The flag `--allow-file-access-from-files` is **essential** - don't skip this!

### Step 3A: Create Channel Points Reward (Recommended)

1. In StreamerBot, go to **Platforms → Twitch → Channel Point Rewards**
2. Click **"Create Reward"** or use an existing one
3. Configure the reward:
   - **Title:** "Play YouTube Video" (or whatever you like)
   - **Cost:** Set your desired channel point cost
   - **User Input Required:** ✅ **ENABLED** (users must provide YouTube URL)
   - **Prompt:** "Enter a YouTube URL to play on stream!"
4. Save the reward

### Step 3B: Create Chat Command (Alternative - Using Custom Currency)

Alternatively, make it a chat command that costs your custom currency:

1. The cost is configured in `ConfigSetup.cs`:
   ```csharp
   int youtubeRedemptionCost = 500; // Cost in your currency
   ```
2. See "Setting Up as Currency Command" section below for full implementation

### Step 4: Create StreamerBot Actions

#### Main Redemption Action

1. In StreamerBot, go to **Actions**
2. Create a new action called "YouTube Redemption"
3. Add a **Sub-Action → Core → C# → Execute C# Code**
4. Copy the entire contents of `YouTubePlayCommand.cs`
5. Paste into the code editor
6. Click **Compile** - ensure no errors
7. Save the action

#### Moderator Stop Command (Highly Recommended)

1. Create a new action called "YouTube Stop"
2. Add **Sub-Action → Core → C# → Execute C# Code**
3. Copy the entire contents of `YouTubeStopCommand.cs`
4. Paste into the code editor
5. Click **Compile** - ensure no errors
6. Save the action

### Step 5: Link Triggers to Actions

#### For Channel Points Reward:
1. Go to **Settings → Events/Triggers**
2. Add trigger: **Twitch → Rewards → Reward Redemption**
3. Select your "Play YouTube Video" reward
4. Link it to the "YouTube Redemption" action
5. Save

#### For Moderator Stop Command:
1. Go to **Settings → Commands**
2. Create command: `!stopvideo` (or `!stopyt`, `!skipvideo`)
3. Set **Permissions:** Moderators only
4. Link to "YouTube Stop" action
5. Save

Now mods can type `!stopvideo` to immediately skip to the next video in queue!

---

## ✅ Testing

1. **Test basic playback:**
   - Redeem with a short video: `https://www.youtube.com/watch?v=dQw4w9WgXcQ`
   - Browser source should appear automatically
   - Video should play full-screen
   - After video ends, source should disappear

2. **Test music video with lyrics:**
   - Redeem with a music.youtube.com URL
   - Verify lyrics appear and sync correctly
   - Check that lyrics start at the first line (top)

3. **Test queue system:**
   - Redeem multiple videos while one is playing
   - Verify chat messages show queue position
   - Confirm videos play in order

4. **Test moderator command:**
   - While a video is playing, use `!stopvideo`
   - Next queued video should start immediately
   - If queue is empty, video should stop completely

---

## 🎛️ Accepted URL Formats

The system accepts various YouTube URL formats:
- `https://www.youtube.com/watch?v=VIDEO_ID`
- `https://music.youtube.com/watch?v=VIDEO_ID` (YouTube Music - enables lyrics)
- `https://youtu.be/VIDEO_ID`
- `https://www.youtube.com/embed/VIDEO_ID`
- `https://m.youtube.com/watch?v=VIDEO_ID`
- `https://www.youtube.com/watch?v=VIDEO_ID&list=...` (playlist/radio links)
- Just the video ID: `dQw4w9WgXcQ`

**Note:** For music videos, use `music.youtube.com` URLs to automatically enable lyrics!

---

## 🎵 Lyrics System Deep Dive

### How It Works

1. **Detection:** System detects `music.youtube.com` URLs and sets music video flag
2. **API Query:** Fetches video metadata from YouTube Data API (title, channel name)
3. **Artist Parsing:** Extracts artist from title ("Artist - Song") or channel name ("Artist - Topic")
4. **Lyrics Fetch:** Queries free LRCLIB API for timestamped lyrics
5. **Display:** Shows lyrics with 1ms precision sync (1000 updates/second)
6. **Queue Preservation:** Music video flag stored in queue format (`user:videoId:musicFlag`)

### Features

- **Ultra-precise sync:** 1000 updates per second for perfect timing
- **Smart initialization:** Lyrics start at first line, no jumping
- **Text wrapping:** Long lyrics wrap to multiple lines (95% screen width, max 1700px)
- **Multiple line support:** Displays all lyrics, scrolls smoothly through them
- **Karaoke-style highlighting:** Current line bright white, past lines fade out
- **Fallback support:** Uses plain text lyrics if timestamped version unavailable
- **Special character handling:** Robust JSON escaping for quotes, backslashes, unicode

### Artist Detection Logic

1. **Title parsing:** Looks for "Artist - Song" or "Song by Artist" format
2. **Channel name fallback:** Uses channel name if title doesn't contain artist
3. **Automatic cleanup:** Removes " - Topic", "VEVO", "Official" suffixes
4. **Works without artist:** If only song name available, uses channel as artist

### Configuration

```csharp
// In ConfigSetup.cs
bool youtubeLyricsEnabled = true;      // Enable/disable lyrics globally
double youtubeLyricsOffset = 0;         // Timing offset in seconds (usually 0)
```

### Troubleshooting Lyrics

**Lyrics not appearing:**
- Ensure URL is from `music.youtube.com` (not regular youtube.com)
- Check that `config_youtube_lyrics_enabled` is `true`
- Verify YouTube API key is configured (needed for title/artist detection)
- Check browser console (F12) for errors

**Lyrics out of sync:**
- Default offset is 0 seconds (no adjustment needed for most videos)
- If persistent drift, adjust `config_youtube_lyrics_offset`
- Positive values = lyrics appear later
- Negative values = lyrics appear earlier

**Lyrics start at wrong position:**
- This should be fixed in current version
- Browser console will show `[LYRICS] Initialized at line 0`
- If still occurring, refresh browser source in OBS

**Lyrics cut off on screen:**
- Long lyrics now automatically wrap to multiple lines
- Container width: 95% of screen (max 1700px)
- Text wraps at word boundaries

**Special characters showing incorrectly:**
- System now handles all special characters (quotes, backslashes, unicode)
- If you see literal backslashes (`\n`, `\t`), report as bug

---

## 📺 Queue System Details

### How Queuing Works

1. **First video plays immediately**
2. **Subsequent redemptions add to queue**
   - Format: `user:videoId:musicFlag` (1=music, 0=regular)
   - Music flag preserved through entire queue lifecycle
3. **Users notified of queue position**
4. **Videos play in FIFO order** (first in, first out)
5. **Chat notifications** when each video starts

### Queue Messages

**Adding to queue:**
```
📺 Username's video added to queue! Position: #3
```

**Next video starts:**
```
🎬 Next video: Username's video is now playing! (2 videos left in queue)
```

**Moderator skip:**
```
⏹️ Video stopped by moderator ModName. Playing next video in queue...
🎬 Next video: Username's video is now playing! (1 video left in queue)
```

**Queue empty after skip:**
```
⏹️ Video stopped by moderator ModName.
```

### Music Video Flag in Queue

The queue preserves whether each video is from music.youtube.com:
- **Regular video:** `"CUB:dQw4w9WgXcQ:0"`
- **Music video:** `"CUB:vWgnw-f3cHk:1"`

This ensures:
- Lyrics appear for queued music videos
- No lyrics appear for queued regular videos
- Detection works even if channel name doesn't contain "Topic/VEVO/Official"

---

## ⚙️ Configuration Reference

Edit these in `ConfigSetup.cs`:

| Setting | Default | Description |
|---------|---------|-------------|
| `config_youtube_obs_scene` | "Main Scene" | OBS scene containing the browser source |
| `config_youtube_obs_source` | "YouTube Player" | Name of the browser source (must match OBS!) |
| `config_youtube_max_duration` | 600 (10 min) | Maximum allowed video duration in seconds |
| `config_youtube_progress_color` | "#FF0000" (red) | Progress bar color (hex: #RRGGBB) |
| `config_youtube_api_key` | "" | YouTube Data API v3 key (get from Google Cloud Console) |
| `config_youtube_lyrics_enabled` | true | Enable/disable lyrics system globally |
| `config_youtube_lyrics_offset` | 0 | Timing offset in seconds (+ = later, - = earlier) |
| `config_youtube_redemption_cost` | 500 | Cost in custom currency (if using chat command) |

**Progress Bar Color Examples:**
- Red: `#FF0000`
- Green: `#00FF00`
- Blue: `#0080FF`
- Purple: `#8000FF`
- Orange: `#FF8000`
- Pink: `#FF00FF`

---

## 🛠️ Troubleshooting

### Video doesn't appear
- ✅ Browser source name matches `config_youtube_obs_source`
- ✅ Scene name matches `config_youtube_obs_scene`
- ✅ Source is hidden by default (eye icon off)
- ✅ Command-line argument `--allow-file-access-from-files` is added
- ✅ File path in URL is correct (check drive letter!)
- ✅ Check OBS logs for errors
- ✅ Refresh browser source (right-click → Refresh)

### Video doesn't disappear after playing
- System monitors every 2 seconds
- Videos get +2 second buffer to finish naturally
- Check StreamerBot logs for `[MONITOR-XXXXXXXX]` messages
- Verify no errors in monitoring task

### "Invalid YouTube URL" error
- Ensure URL contains valid video ID
- Test with known good URL: `https://www.youtube.com/watch?v=dQw4w9WgXcQ`
- Check that user provided URL, not just text

### Lyrics not appearing
- Must use `music.youtube.com` URL (not regular youtube.com)
- Check `config_youtube_lyrics_enabled = true`
- Verify YouTube API key is configured
- Check browser console (F12 in OBS browser source) for errors
- Verify artist name could be detected from title/channel

### Lyrics start at wrong position then jump
- Fixed in current version
- Force lyrics to line 0 for first 2 seconds of video
- Check browser console for `[LYRICS] Initialized at line 0`

### Lyrics have backslashes or weird characters
- System now handles all special characters correctly
- If you see literal `\n` or `\t`, this is a bug - report it
- JSON escaping improved to handle quotes, backslashes, unicode

### Videos randomly stopping mid-playback
- Check StreamerBot logs for `[MONITOR-XXXXXXXX]` messages
- Look for reason: "Duration reached", "New video detected", "Source hidden"
- Ensure videos aren't exceeding `max_duration`
- Verify API is returning correct duration (check logs)

### Queue messages out of order
- Fixed in current version
- Stop command now checks queue first before sending messages
- Messages appear in correct logical order

### !stopvideo not working
- Verify command is set to Moderators only permission
- Check that action is linked correctly
- Test as broadcaster (always has permission)
- Check StreamerBot logs for errors

---

## 📝 Technical Notes

### Smart Duration Monitoring
- **Unique monitor IDs:** Each video gets unique 8-character ID for tracking
- **2-second check interval:** Balances responsiveness with performance
- **+2 second safety buffer:** Prevents cutting videos off early
- **Priority checking:**
  1. JSON file changed (new video started)
  2. OBS source manually hidden (stop command)
  3. Duration reached (video finished)

### Lyrics Update Frequency
- **1ms interval** = 1000 updates per second
- Provides near-perfect synchronization with video playback
- Browser handles this frequency without performance issues
- Current line detected based on exact timestamp matching

### JSON Data Format
```json
{
  "videoId": "dQw4w9WgXcQ",
  "timestamp": 638400000000000000,
  "progressColor": "#FF0000",
  "title": "Never Gonna Give You Up",
  "requestedBy": "Username",
  "lyrics": "[00:15.00]First line\n[00:20.00]Second line",
  "lyricsOffset": 0
}
```

### Queue Format
```
user1:videoId1:0|user2:videoId2:1|user3:videoId3:0
```
- Pipe-separated entries
- Each entry: `username:videoId:musicFlag`
- Music flag: `1` = music video, `0` = regular video

### URL Path Requirements
- Must use `file:///` protocol (three slashes!)
- Use forward slashes even on Windows
- Example: `file:///G:/GitHub Projects/path/to/file.html`
- If path has spaces, they must be URL-encoded as `%20` or use quotes in OBS

---

## 🎨 Customization

Want to change the player appearance? Edit `youtube-player.html`:

### Colors
- **Background:** Line 15 - `background-color: transparent;`
- **Progress bar:** CSS variable in JSON - `progressColor`
- **Lyrics active line:** Line 170 - `color: white;`
- **Lyrics past lines:** Line 177 - `opacity: 0.3;`

### Sizes
- **Player dimensions:** Lines 33-34 - `width: 1920px; height: 1080px;`
- **Title font size:** Line 100 - `font-size: 48px;`
- **Lyrics font size:** Line 148 - `font-size: 36px;` (inactive), Line 171 - `font-size: 48px;` (active)
- **Lyrics container width:** Line 131 - `width: 95%; max-width: 1800px;`

### Timing
- **Progress bar update:** Line 382 - `setInterval(updateProgressBar, 1000)` (1 second)
- **Lyrics update:** Line 548 - `setInterval(updateLyrics, 1)` (1ms)
- **Video ID check:** Line 696 - `setInterval(..., 1000)` (1 second)
- **Monitor check:** C# code - `const int checkInterval = 2000` (2 seconds)

### Debug Mode
To enable debug messages:
1. Open browser source in OBS
2. Press F12 to open developer console
3. Watch for `[LYRICS]` and `[MONITOR]` messages
4. Check for errors or warnings

---

## 💰 Setting Up as Currency Command

If you want to use your custom currency instead of channel points:

### 1. Add Currency Check Code

Add this code **after line 48** (after getting user/userId) in `YouTubePlayCommand.cs`:

```csharp
// Load currency configuration
string currencyName = CPH.GetGlobalVar<string>("config_currency_name", true);
string currencyKey = CPH.GetGlobalVar<string>("config_currency_key", true);
int redemptionCost = CPH.GetGlobalVar<int>("config_youtube_redemption_cost", true);
if (redemptionCost == 0) redemptionCost = 500; // Default cost

// Check balance
int balance = CPH.GetTwitchUserVarById<int>(userId, currencyKey, true);

if (balance < redemptionCost)
{
    CPH.SendMessage($"{user}, you need ${redemptionCost} {currencyName} to play a video! You have ${balance}");
    LogWarning("YouTube Redemption - Insufficient Funds",
        $"**User:** {user}\n**Cost:** ${redemptionCost}\n**Balance:** ${balance}");
    return false;
}

// Deduct cost
balance -= redemptionCost;
CPH.SetTwitchUserVarById(userId, currencyKey, balance, true);

LogInfo("YouTube Redemption - Currency Deducted",
    $"**User:** {user}\n**Cost:** ${redemptionCost}\n**New Balance:** ${balance}");
```

### 2. Update Success Message

Find the line that sends `"🎬 {user}'s video is now playing!"` and replace with:

```csharp
CPH.SendMessage($"🎬 {user} spent ${redemptionCost} {currencyName} to play a video! Balance: ${balance}");
```

### 3. Create Chat Command Trigger

1. In StreamerBot, go to **Settings → Commands**
2. Create a new command: `!youtube` or `!video`
3. Set **Permissions** as desired (Everyone, Subscribers, etc.)
4. Link to your "YouTube Redemption" action
5. Test with: `!youtube https://www.youtube.com/watch?v=dQw4w9WgXcQ`

### 4. Adjust Cost

Edit the cost in `ConfigSetup.cs` (line 408):
```csharp
int youtubeRedemptionCost = 500; // Change to your desired cost
```

Then re-run `ConfigSetup.cs` to update the global variable.

---

## 📌 File Locations

- **HTML Player:** `Utilities/YouTube-Player/youtube-player.html`
- **Main Command:** `Utilities/YouTube-Player/YouTubePlayCommand.cs`
- **Stop Command:** `Utilities/YouTube-Player/YouTubeStopCommand.cs`
- **Configuration:** `Currency/Core/Config-Setup/ConfigSetup.cs` (lines 402-434)
- **This Guide:** `Utilities/YouTube-Player/README.md`

### ConfigSetup.cs Line Reference:
- **Lines 402-403:** OBS scene and source names
- **Line 408:** Redemption cost (for currency command)
- **Line 409:** Maximum video duration
- **Line 415:** Progress bar color
- **Line 421:** YouTube Data API key
- **Line 427:** Enable/disable lyrics
- **Line 433:** Lyrics timing offset

---

## 🔧 Recent Improvements (January 2025)

### Lyrics System
- ✅ Ultra-precise sync (1000 updates/second)
- ✅ Smart initialization (prevents jumping to wrong line)
- ✅ Text wrapping for long lyrics
- ✅ Music video flag preserved in queue
- ✅ Robust JSON escaping for special characters

### Monitoring System
- ✅ Unique monitor IDs for debugging
- ✅ +2 second safety buffer (prevents early cutoff)
- ✅ Priority-based checking (JSON → visibility → duration)
- ✅ Better error logging with context

### Queue System
- ✅ Music video flag storage (`user:videoId:musicFlag`)
- ✅ Corrected message order for !stopvideo
- ✅ Smart empty queue detection
- ✅ Preserved lyrics through queue lifecycle

### Bug Fixes
- ✅ Fixed special characters in lyrics (backslashes, quotes, unicode)
- ✅ Fixed lyrics starting at bottom then scrolling to top
- ✅ Fixed videos stopping randomly mid-playback
- ✅ Fixed duplicate/out-of-order chat messages
- ✅ Fixed queue not preserving music.youtube.com context

---

## 🆘 Support

For issues or questions:
1. **Check StreamerBot logs** - Look for error messages and `[MONITOR]` entries
2. **Check browser console** - Press F12 in OBS browser source, look for `[LYRICS]` messages
3. **Verify configuration** - Ensure all paths and names match exactly
4. **Test incrementally:**
   - First: Test basic video playback
   - Second: Test queue system
   - Third: Test lyrics with music.youtube.com
   - Fourth: Test moderator commands
5. **Check file paths** - Ensure drive letters and paths are correct for your system
6. **Refresh browser source** - Right-click source in OBS → Refresh

### Common Issues Quick Reference

| Symptom | Likely Cause | Fix |
|---------|-------------|-----|
| No video appears | Browser source misconfigured | Check name, path, command-line args |
| No lyrics appear | Not music.youtube.com URL | Use music.youtube.com URLs |
| Lyrics out of sync | Offset configured wrong | Set `lyricsOffset` to 0 |
| Video stops early | Old monitoring code | Update to latest version with +2s buffer |
| Special chars broken | Old JSON escaping | Update to latest escaping code |
| Queue loses lyrics | Old queue format | Update to 3-part format with music flag |

---

## 🏆 Credits

**System Design & Development:** HexEchoTV (CUB)
**APIs Used:**
- YouTube IFrame API (video playback)
- YouTube Data API v3 (metadata, duration)
- LRCLIB API (free timestamped lyrics)

**Special Thanks:**
- StreamerBot by Nate1280
- OBS Studio team
- LRCLIB.net for free lyrics API

---

Made with ❤️ by HexEchoTV (CUB)
Last Updated: January 2025

**Version:** 2.0 (Lyrics Update)

---

## 📄 License

MIT License - See LICENSE file in project root

Copyright (c) 2025 HexEchoTV (CUB)
