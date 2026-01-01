# Clip Commands

[![Discord](https://img.shields.io/badge/Discord-Join%20for%20Help-7289da)](https://discord.gg/ngQXHUbnKg)

## Overview

The **Clip Commands** folder contains utilities for fetching and displaying Twitch clips in your chat.

## Available Commands

| Command | File | Purpose | Usage |
|---------|------|---------|-------|
| **Clip Fetch** | `ClipCommand.cs` | Get latest clip from your channel | `!clip` |

## What This Does

The Clip command allows viewers to:
- Fetch the most recent clip from your channel
- Display clip title and URL in chat
- Share highlights easily
- Promote clip creation

## Installation

### Prerequisites

**Optional:**
- ConfigSetup.cs (for Discord logging only)
- No other dependencies required

### Install Clip Command

1. Open StreamerBot
2. Go to **Actions** tab
3. Click **Import**
4. Select `Clip/Clip-Fetch/ClipCommand.cs`
5. Click **Import**

### Create Chat Trigger

1. In the imported action, go to **Triggers** tab
2. Click **Add** → **Chat Message** → **Command**
3. Configure:
   - **Command**: `!clip`
   - **Permission**: Everyone
   - **Enabled**: ✅ Check
4. Click **OK**

### Test the Command

1. Go to your Twitch chat
2. Type: `!clip`
3. Bot should respond with your latest clip

## Usage

### Basic Usage

```
!clip
→ Latest clip: "Amazing Play!" https://clips.twitch.tv/...
```

### When No Clips Exist

```
!clip
→ No clips found for this channel.
```

## Configuration

### Customizing the Response

Edit `ClipCommand.cs` to customize the message format:

**Default format (line ~35):**
```csharp
CPH.SendMessage($"Latest clip: \"{clipTitle}\" {clipUrl}");
```

**Custom examples:**
```csharp
// With emotes
CPH.SendMessage($"🎬 Latest clip: {clipTitle} → {clipUrl}");

// More detailed
CPH.SendMessage($"Check out this clip: \"{clipTitle}\" by {creatorName} - {clipUrl}");

// Simple
CPH.SendMessage($"Latest clip: {clipUrl}");
```

### Changing Clip Count

To fetch multiple clips:

1. Edit `ClipCommand.cs`
2. Find the API call (line ~25):
   ```csharp
   // Change from 1 to desired number
   var clips = CPH.TwitchGetClips(1);  // Get 1 clip
   var clips = CPH.TwitchGetClips(5);  // Get 5 clips
   ```
3. Update message formatting to handle multiple clips

### Adding Cooldown

To prevent spam:

1. In StreamerBot, select the Clip action
2. Go to **Settings** tab
3. Add **Queue** delay:
   - Delay: 10000ms (10 seconds)
   - Concurrent: 1
4. Click **OK**

## Dependencies

### No Dependencies Required

The Clip command works standalone using:
- Built-in StreamerBot Twitch API
- No external files needed

### Optional Dependencies

**ConfigSetup.cs** (Optional)
- Location: `Currency/Core/Config-Setup/ConfigSetup.cs`
- Only needed for Discord logging
- Not required for clip functionality

**Discord Logging** (Optional)
- Built into ClipCommand.cs
- Logs clip fetches to Discord
- Configure in ConfigSetup.cs

## Permissions

### Who Can Use

By default: **Everyone**

To restrict to moderators:
1. Edit chat trigger
2. Change **Permission** to "Moderators"
3. Save

### Recommended Permissions

- **Everyone** - Let all viewers share clips
- **Moderators** - If you want controlled clip sharing

## Discord Logging

If Discord logging is configured, the command logs:

### Successful Clip Fetch
```
🎮 COMMAND
Command: !clip
User: Username
```

```
✅ SUCCESS
Clip Fetched
User: Username
Clip: "Amazing Play!"
URL: https://clips.twitch.tv/...
```

### No Clips Found
```
⚠️ WARNING
No Clips Available
User: Username
Channel: YourChannel
```

### Errors
```
❌ ERROR
Clip Command Error
Error: [Error message]
```

## Troubleshooting

### Issue: No clips found

**Possible causes:**
- No clips exist for your channel
- Clips may be deleted
- Twitch API delay (clips need time to process)

**Solutions:**
- Create a clip manually
- Wait 1-2 minutes after clip creation
- Check your Twitch dashboard for clips

### Issue: Command not responding

**Solutions:**
- Verify chat trigger is enabled
- Check StreamerBot is connected to Twitch
- Look for errors in StreamerBot logs
- Ensure Twitch API permissions are granted

### Issue: Shows wrong channel's clips

**Solution:**
- ClipCommand automatically uses your channel
- Verify StreamerBot broadcaster account is correct

### Issue: Old clip showing

**Explanation:**
- Command fetches most recent clip
- Clip order based on creation time
- Create new clip to update

## Advanced Features

### Fetch Random Clip

Modify the command to get random clips:

```csharp
// Fetch 10 clips
var clips = CPH.TwitchGetClips(10);

// Pick random one
Random rnd = new Random();
int randomIndex = rnd.Next(clips.Count);
var clip = clips[randomIndex];
```

### Fetch Clip by Creator

```csharp
// Add parameter for username
string targetUser = args.ContainsKey("rawInput")
    ? args["rawInput"].ToString()
    : userName;

// Fetch clips by that user
var clips = CPH.TwitchGetClipsForUser(targetUser, 1);
```

### Display Clip Stats

```csharp
// Include view count and creation date
CPH.SendMessage($"Latest clip: \"{clipTitle}\" ({viewCount} views, created {createdAt}) - {clipUrl}");
```

## Use Cases

### Promote Clip Creation

Encourage viewers to create clips:
```
Moderator: Great play! Create a clip!
[Viewers create clip]
Viewer: !clip
→ Latest clip: "Insane Headshot!" https://...
```

### Recap Stream Highlights

```
You: That was amazing! Check the clip!
Bot: !clip
→ Latest clip: "Epic Moment!" https://...
```

### Share Best Moments

```
Viewer: !clip
→ Show them your best recent moment
```

## Related Commands

- **!highlight** - Create a highlight (if you have that command)
- **!discord** - Share Discord where clips are posted
- **!social** - Share other social media

## API Information

### Twitch API Endpoints Used

The command uses StreamerBot's built-in:
```csharp
CPH.TwitchGetClips(count)
```

This calls:
- Twitch Helix API `/clips`
- Filtered to your channel
- Sorted by creation date (newest first)
- Returns clip metadata (title, URL, creator, views)

### Rate Limits

- Twitch API rate limits apply
- StreamerBot handles rate limiting automatically
- Recommended cooldown: 10+ seconds

## Support

Need help with clip commands?

- **Discord**: [https://discord.gg/ngQXHUbnKg](https://discord.gg/ngQXHUbnKg)
- **Twitch Clips API**: [Twitch Developer Docs](https://dev.twitch.tv/docs/api/reference#get-clips)
- **StreamerBot Support**: [StreamerBot Discord](https://discord.gg/streamerbot)

## File Information

- **Folder**: `Clip/Clip-Fetch/`
- **File**: `ClipCommand.cs`
- **Created by**: HexEchoTV (CUB) | [GitHub](https://github.com/ThortonEllmers/Streamerbot-Commands)
- **License**: Free for personal use only




