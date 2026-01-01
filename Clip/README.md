# Clip Command (with Stream Title Modification)

[![Discord](https://img.shields.io/badge/Discord-Join%20for%20Help-7289da)](https://discord.gg/ngQXHUbnKg)

## Overview

The **Clip Command** creates a new Twitch clip and temporarily modifies your stream title to credit the clip creator. This advanced command integrates directly with the Twitch Helix API to provide a seamless clipping experience with automatic title management.

## What This Command Does

1. **Gets current stream title** from Twitch API
2. **Temporarily changes title** to "Clipped by {username} - {original title}"
3. **Creates a new clip** using StreamerBot's clip creation
4. **Restores original title** automatically
5. **Posts clip to Discord webhook** (hardcoded)
6. **Logs all actions** to Discord (if configured)

## Key Features

- ✅ **Automatic title modification** - Credits clip creator in stream title
- ✅ **Title restoration** - Original title restored after clip creation
- ✅ **Discord integration** - Posts clips to Discord webhook automatically
- ✅ **Live stream detection** - Only works when stream is live
- ✅ **Error handling** - Graceful fallbacks if title changes fail
- ✅ **Comprehensive logging** - All actions logged to Discord
- ✅ **Twitch API integration** - Direct Helix API calls with OAuth

## Dependencies

### Required Dependencies

1. **ConfigSetup.cs** (REQUIRED)
   - Location: `Currency/Core/Config-Setup/ConfigSetup.cs`
   - Purpose: Stores Twitch API credentials
   - **Must be configured before using this command**

2. **Twitch OAuth Token** (REQUIRED)
   - Required scope: `channel:manage:broadcast`
   - Needed for: Reading and modifying stream title
   - Configure in ConfigSetup.cs (lines 344-346)

3. **Stream Must Be Live** (REQUIRED)
   - Clips can only be created during live streams
   - Command will fail if stream is offline

### Optional Dependencies

- **Discord Logging** - Built-in, configure webhook in ConfigSetup.cs

## Installation

### Step 1: Get Twitch API Credentials

1. Go to [Twitch Token Generator](https://twitchtokengenerator.com)
2. Select the required scopes:
   - **`channel:manage:broadcast`** (required for creating clips)
   - **`moderator:read:followers`** (optional, for other commands)
3. Click **Generate Token**
4. Copy your **Access Token**, **Refresh Token**, and **Client ID**

### Step 2: Configure ConfigSetup.cs

1. Open `Currency/Core/Config-Setup/ConfigSetup.cs`
2. Find lines 347-349:
   ```csharp
   string twitchAccessToken = "YOUR_ACCESS_TOKEN_HERE";
   string twitchRefreshToken = "YOUR_REFRESH_TOKEN_HERE";
   string twitchClientId = "YOUR_CLIENT_ID_HERE";
   ```
3. Replace with your actual credentials from twitchtokengenerator.com
4. **Run ConfigSetup.cs** to save the credentials

### Step 3: Configure Discord Webhook (Optional)

In ClipCommand.cs line 151, update the webhook URL:
```csharp
string webhookUrl = "YOUR_DISCORD_WEBHOOK_URL_HERE";
```

Or leave the default if you want clips posted to the existing webhook.

### Step 4: Import Clip Command

1. Open StreamerBot
2. Go to **Actions** tab
3. Click **Import**
4. Select `Clip/Clip-Fetch/ClipCommand.cs`
5. Click **Import**

### Step 6: Create Chat Trigger

1. In the imported action, go to **Triggers** tab
2. Click **Add** → **Chat Message** → **Command**
3. Configure:
   - **Command**: `!clip`
   - **Permission**: Everyone (or Moderators only)
   - **Enabled**: ✅ Check
4. Click **OK**

### Step 7: Test the Command

1. **Make sure your stream is LIVE**
2. Type in chat: `!clip`
3. Watch the title change and clip creation happen!

## Usage

### Basic Usage

```
!clip
```

### Expected Behavior

**Step 1: Title Change**
```
Original title: "Playing Valorant - Ranked Grind"
↓
Modified title: "Clipped by Username - Playing Valorant - Ranked Grind"
```

**Step 2: Clip Creation**
```
@Username Generating your clip, please wait...
```

**Step 3: Title Restored**
```
Restored title: "Playing Valorant - Ranked Grind"
```

**Step 4: Clip Delivered**
```
@Username Your clip is ready! https://clips.twitch.tv/...
```

### Example Output

**Successful Clip:**
```
@Username Generating your clip, please wait...
@Username Your clip is ready! https://clips.twitch.tv/ExampleClipURL
```

**Stream Not Live:**
```
@Username Cannot create clip - stream is not live!
```

**API Not Configured:**
```
⚠️ Twitch API not configured! Please configure in ConfigSetup.cs and run it.
```

## Configuration

### Twitch API Credentials

Located in ConfigSetup.cs (lines 344-346):
```csharp
CPH.SetGlobalVar("twitchApiClientId", "YOUR_CLIENT_ID", true);
CPH.SetGlobalVar("twitchApiAccessToken", "YOUR_ACCESS_TOKEN", true);
```

**Security Note:** Keep these credentials private! Don't commit them to public repositories.

### Discord Webhook URL

Located in ClipCommand.cs (line 151):
```csharp
string webhookUrl = "https://discord.com/api/webhooks/YOUR_WEBHOOK_ID/YOUR_WEBHOOK_TOKEN";
```

To get a Discord webhook:
1. Go to Discord server settings → Integrations
2. Create webhook
3. Copy webhook URL
4. Paste into ClipCommand.cs line 151

### Custom Title Format

To change the temporary title format (line 96):
```csharp
// Default format
string tempTitle = $"Clipped by {clipRequester} - {originalTitle}";

// Custom examples:
string tempTitle = $"🎬 {clipRequester} made a clip! - {originalTitle}";
string tempTitle = $"[CLIP] {originalTitle} - by {clipRequester}";
string tempTitle = $"{clipRequester}'s Highlight - {originalTitle}";
```

### Custom Chat Messages

**Clip request message (line 77):**
```csharp
CPH.SendMessage($"@{clipRequester} Generating your clip, please wait...");
```

**Clip ready message (line 144):**
```csharp
CPH.SendMessage($"@{clipRequester} Your clip is ready! {clipUrl}");
```

**Custom examples:**
```csharp
// More excited
CPH.SendMessage($"🎉 @{clipRequester} Your epic clip is ready! {clipUrl}");

// With emoji
CPH.SendMessage($"✂️ @{clipRequester} Clip created! {clipUrl}");

// Simple
CPH.SendMessage($"Clip: {clipUrl}");
```

### Wait Times

**Title propagation wait (line 106):**
```csharp
CPH.Wait(3000);  // 3 seconds - increase if title doesn't appear in clip
```

**Clip processing wait (line 118):**
```csharp
CPH.Wait(2000);  // 2 seconds - increase if clip URL is null
```

## How It Works

### Detailed Workflow

```
User types !clip
    ↓
[1] Check if API credentials configured
    ↓
[2] Check if stream is live (OBS streaming)
    ↓
[3] Get broadcaster ID from StreamerBot
    ↓
[4] Call Twitch API to get current stream title
    ↓
[5] Create temporary title: "Clipped by {user} - {original}"
    ↓
[6] Call Twitch API to set new title (PATCH request)
    ↓
[7] Wait 3 seconds for title to propagate
    ↓
[8] Create clip using CPH.CreateClip()
    ↓
[9] Wait 2 seconds for clip to process
    ↓
[10] Call Twitch API to restore original title
    ↓
[11] Send clip URL to chat
    ↓
[12] Post clip to Discord webhook
    ↓
[13] Log everything to Discord (if enabled)
```

### API Methods Used

**GetChannelTitle() - Lines 178-232**
- Calls: `GET https://api.twitch.tv/helix/channels?broadcaster_id={id}`
- Purpose: Retrieves current stream title
- Returns: Stream title as string

**SetChannelTitle() - Lines 235-282**
- Calls: `PATCH https://api.twitch.tv/helix/channels?broadcaster_id={id}`
- Purpose: Updates stream title
- Requires: OAuth token with `channel:manage:broadcast` scope

**CPH.CreateClip() - Line 116**
- StreamerBot built-in method
- Creates clip from current broadcast
- Returns: ClipData object with URL

## Discord Logging

If Discord logging is enabled (configured in ConfigSetup.cs):

### Command Execution (Purple)
```
🎮 COMMAND
Command: !clip (with title)
User: Username
```

### Title Retrieved (Blue)
```
ℹ️ INFO
Getting Stream Title
User: Username
Broadcaster ID: 123456789
```

### Title Changed (Blue)
```
ℹ️ INFO
Title Changed
User: Username
New Title: Clipped by Username - Original Title
```

### Title Restored (Blue)
```
ℹ️ INFO
Title Restored
Original Title: Original Title
```

### Clip Created (Green)
```
✅ SUCCESS
Clip Created (with Title Change)
User: Username
URL: https://clips.twitch.tv/...
Temp Title: Clipped by Username - Original Title
Restored: True
```

### Warnings (Orange)
```
⚠️ WARNING
Clip - Stream Not Live
User: Username
```

```
⚠️ WARNING
Title Change Failed
User: Username
Reason: API call failed
```

### Errors (Red)
```
❌ ERROR
Twitch API Not Configured
Reason: Credentials not set in ConfigSetup.cs
```

```
❌ ERROR
Clip Command Exception
User: Username
Error: [Error message]
Stack Trace: [Stack trace]
```

## Troubleshooting

### Issue: "Twitch API not configured" error

**Solution:**
1. Open ConfigSetup.cs
2. Add your Twitch Client ID and Access Token (lines 344-346)
3. Run ConfigSetup.cs to save credentials
4. Try !clip command again

### Issue: "Cannot create clip - stream is not live"

**Solution:**
- This command only works during live streams
- Start streaming in OBS
- Verify StreamerBot shows you as live
- Try command again

### Issue: Title changes but doesn't appear in clip

**Solution:**
- Increase wait time after title change (line 106)
- Change from 3000ms to 5000ms or higher
- Twitch API propagation can be slow

### Issue: Clip URL is null/empty

**Solution:**
- Increase wait time after clip creation (line 118)
- Change from 2000ms to 4000ms or higher
- Clips need time to process on Twitch

### Issue: Title doesn't restore after clip

**Cause:**
- API call failed or timed out
- Check StreamerBot logs for errors

**Solution:**
- Manually change title back in Twitch dashboard
- Check OAuth token has correct permissions
- Verify token hasn't expired

### Issue: "Broadcaster ID not found" error

**Solution:**
- This is a StreamerBot configuration issue
- Verify you're logged into correct Twitch account
- Reconnect Twitch account in StreamerBot settings
- Check StreamerBot logs for available args keys

### Issue: OAuth token expired

**Symptoms:**
- API calls fail with 401 errors
- "Unauthorized" in logs

**Solution:**
1. Generate new OAuth token (see Installation Step 2)
2. Update ConfigSetup.cs with new token
3. Run ConfigSetup.cs again

## Permissions & Security

### Required Twitch Permissions

The OAuth token MUST have:
- ✅ `channel:manage:broadcast` - To read and modify stream title

### Security Best Practices

1. **Never share your OAuth token publicly**
2. **Don't commit ConfigSetup.cs with real credentials to Git**
3. **Regenerate tokens if exposed**
4. **Use a dedicated bot account if possible**

### Recommended User Permissions

**Who can use !clip:**
- Everyone - Let all viewers create clips
- Moderators Only - More controlled clip creation
- VIPs Only - Reward trusted members

Configure in StreamerBot trigger settings.

## Advanced Features

### Add Clip Cooldown

Prevent spam by adding a user cooldown:

1. In StreamerBot, select the Clip action
2. Go to **Settings** or **Queue** tab
3. Add user cooldown: 60 seconds (1 clip per minute per user)

### Multiple Discord Webhooks

Post clips to multiple Discord servers:

```csharp
// After line 157, add:
string webhook2 = "https://discord.com/api/webhooks/SECOND_WEBHOOK";
try {
    CPH.DiscordPostTextToWebhook(webhook2, discordMessage, "Clip Bot", avatarUrl, false);
} catch { }
```

### Custom Avatar for Discord Posts

Change the bot avatar (line 152):
```csharp
string avatarUrl = "https://your-image-url-here.png";
```

### Add Clip Duration to Message

```csharp
// After clip creation (line 116):
int duration = clipData?.Duration ?? 30;
CPH.SendMessage($"@{clipRequester} Your {duration}s clip is ready! {clipUrl}");
```

### Skip Title Change for Specific Users

```csharp
// After line 44, add:
string[] skipUsers = { "mod1", "mod2", "streamer" };
bool skipTitle = Array.Exists(skipUsers, u => u == clipRequester.ToLower());

if (!skipTitle) {
    // Do title change
} else {
    // Skip straight to clip creation
}
```

## API Rate Limits

### Twitch API Limits

- **GET /channels**: 800 requests per minute
- **PATCH /channels**: 800 requests per minute
- StreamerBot handles rate limiting automatically

### Recommended Cooldowns

- **User cooldown**: 60 seconds (1 clip per user per minute)
- **Global cooldown**: 10 seconds (prevent spam)

## Use Cases

### Credit Clip Creators

Show appreciation by crediting users in the stream title when they create clips.

### Promote Clip Creation

Encourage viewers to clip moments by making it easy and rewarding with title recognition.

### Track Clip Activity

Monitor who creates clips through Discord logs.

### Highlight Moments

Allow viewers to mark important moments that appear in title temporarily.

## File Structure

```
Clip/
├── Clip-Fetch/
│   ├── ClipCommand.cs          # Main clip creation command
│   └── TwitchAPIDiagnosticCommand.cs  # API debugging tool
└── README.md                    # This file
```

## Related Files

- **ConfigSetup.cs** - Required for API credentials
- **TwitchAPIDiagnosticCommand.cs** - Tool for debugging API issues

## Support

Need help with the Clip Command?

- **Discord**: [https://discord.gg/ngQXHUbnKg](https://discord.gg/ngQXHUbnKg)
- **Twitch API Docs**: [https://dev.twitch.tv/docs/api/reference](https://dev.twitch.tv/docs/api/reference)
- **StreamerBot Support**: [StreamerBot Discord](https://discord.gg/streamerbot)

## Technical Details

### Twitch API Endpoints

**Get Channel Information:**
```
GET https://api.twitch.tv/helix/channels?broadcaster_id={id}
Headers:
  - Authorization: Bearer {access_token}
  - Client-Id: {client_id}
```

**Update Channel Information:**
```
PATCH https://api.twitch.tv/helix/channels?broadcaster_id={id}
Headers:
  - Authorization: Bearer {access_token}
  - Client-Id: {client_id}
  - Content-Type: application/json
Body:
  {"title": "New Title Here"}
```

### Error Handling

The command includes comprehensive error handling:
- API credential validation
- Live stream detection
- Broadcaster ID verification
- API call failures (graceful degradation)
- Clip creation failures
- Discord logging (silent failures)

## File Information

- **Folder**: `Clip/Clip-Fetch/`
- **File**: `ClipCommand.cs`
- **Created by**: HexEchoTV (CUB) | [GitHub](https://github.com/HexEchoTV/Streamerbot-Commands)
- **License**: MIT License
- **Dependencies**: ConfigSetup.cs, Twitch OAuth Token
