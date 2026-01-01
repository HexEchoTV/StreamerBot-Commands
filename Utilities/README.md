# Utility Commands

[![Discord](https://img.shields.io/badge/Discord-Join%20for%20Help-7289da)](https://discord.gg/ngQXHUbnKg)

## Overview

The **Utility Commands** folder contains helpful tools and features for your Twitch stream that aren't part of the currency system. These commands provide stream information, viewer stats, shoutouts, and more.

## Available Utilities

### 📊 Stream Information

| Command | Purpose | Usage |
|---------|---------|-------|
| **Uptime** | Show stream uptime | `!uptime` |
| **Title** | Show/set stream title | `!title [new title]` |
| **Game** | Show/set stream category | `!game [new game]` |

### 👥 Viewer Information

| Command | Purpose | Usage |
|---------|---------|-------|
| **Followage** | Show how long someone has followed | `!followage [@user]` |
| **Watchtime** | Show total watchtime | `!watchtime [@user]` |

### 🎯 Channel Tools

| Command | Purpose | Usage |
|---------|---------|-------|
| **Shoutout** | Shoutout another streamer | `!so @streamer` |
| **Discord** | Share Discord invite link | `!discord` |
| **Multi-Twitch** | Generate multi-stream link | `!multi @streamer1 @streamer2` |
| **Commands List** | List all available commands | `!commands` |
| **Quotes** | Random quote system | `!quote` / `!addquote` |
| **Clip** | Fetch latest clip | `!clip` |

### 🎨 Stream Events

| Command | Purpose | Usage |
|---------|---------|-------|
| **Welcome First-Timer** | Auto-welcome new viewers | Automatic |
| **Watchtime Tracker** | Track viewer watchtime | Automatic |
| **Watchtime Awarder** | Give rewards for watching | Automatic |

### 🔧 System Tools

| Command | Purpose | Usage |
|---------|---------|-------|
| **Discord Logging** | Log commands to Discord | `!logging on/off` |

## Installation

### Prerequisites

**Some utilities require:**
1. **ConfigSetup.cs** (for Discord logging integration)
   - Location: `Currency/Core/Config-Setup/ConfigSetup.cs`
   - Only needed if using Discord logging

### Install Individual Utilities

Each utility folder contains:
- **CommandName.cs** - The command file
- **README.md** - Specific installation instructions

**Steps to install:**
1. Go to the utility's folder (e.g., `Utilities/Uptime/`)
2. Read the README.md file
3. Import the .cs file into StreamerBot
4. Add appropriate triggers (chat command or automatic)
5. Test functionality

### Recommended Utilities to Start With

**Essential:**
1. **Uptime** - Viewers always ask this
2. **Shoutout** - Support other streamers
3. **Discord** - Grow your community

**Highly Recommended:**
4. **Followage** - Viewer engagement
5. **Welcome First-Timer** - Greet new viewers
6. **Commands List** - Help viewers discover features

**Optional:**
7. **Quotes** - Fun community feature
8. **Multi-Twitch** - Multi-stream viewing
9. **Watchtime** - Track engagement
10. **Clip** - Share highlights

## Usage Examples

### Stream Information
```
!uptime
→ Stream has been live for 2 hours, 34 minutes

!title
→ Current title: Playing games and chatting!

!game
→ Current game: Just Chatting
```

### Viewer Information
```
!followage
→ Username has been following for 6 months, 15 days

!followage @someviewer
→ someviewer has been following for 2 years, 3 months

!watchtime
→ Username has watched for 127 hours total
```

### Channel Tools
```
!so @somestre amer
→ 📢 Check out @somestreamer! They were last playing Just Chatting!
→ https://twitch.tv/somestreamer

!discord
→ 💬 Join our Discord community! → https://discord.gg/YOUR_SERVER

!multi @streamer1 @streamer2
→ Watch multiple streams: https://multistre.am/yourname/streamer1/streamer2

!commands
→ Available commands: !uptime, !followage, !discord, !so, !balance, !daily, !work...

!quote
→ [Quote #42] "This is an example quote" - Username, 2024

!addquote This is a new quote
→ Quote #43 added!

!clip
→ Latest clip: "Amazing Play!" https://clips.twitch.tv/...
```

### Automatic Events
```
[New viewer joins chat]
→ 🎉 Welcome @newviewer! Thanks for stopping by for the first time!

[Viewer watches for 1 hour]
→ [Automatically tracked in background]

[Viewer reaches 10 hours watched]
→ [Optional reward system triggers]
```

## Configuration

### Uptime Command
- No configuration needed
- Uses Twitch API to get stream start time

### Shoutout Command
- Optional: Customize shoutout message
- Optional: Restrict to moderators only

### Discord Command
1. Open `ConfigSetup.cs`
2. Set Discord invite link (line 365):
   ```csharp
   string discordServerLink = "https://discord.gg/YOUR_INVITE";
   ```
3. Run ConfigSetup.cs

### Watchtime System
1. Configure rewards in WatchtimeAwarder.cs
2. Set tracking interval (default: every 5 minutes)
3. Optional: Link to currency system

### Welcome Message
1. Edit WelcomeFirstTimer.cs
2. Customize welcome message
3. Optional: Add emotes or channel-specific text

## Dependencies

### No Dependencies
These utilities work standalone:
- Uptime
- Followage
- Title
- Game
- Multi-Twitch
- Commands List
- Quotes (standalone system)
- Clip

### Requires ConfigSetup.cs
These need ConfigSetup.cs for full functionality:
- Discord (for Discord link variable)
- Discord Logging (for logging toggle)
- Watchtime Awarder (if linking to currency system)

### Requires Twitch API
These use Twitch API (built into StreamerBot):
- Uptime
- Followage
- Title (for setting)
- Game (for setting)
- Clip

## Permissions

### Everyone Can Use
- Uptime
- Followage
- Discord
- Multi-Twitch
- Commands
- Quote
- Clip
- Watchtime (own stats)

### Moderator/Broadcaster Only
- Shoutout (recommended)
- Title (set)
- Game (set)
- Add Quote
- Discord Logging Toggle
- Watchtime (other users)

## Discord Logging

Most utilities support Discord logging:

### What Gets Logged

**Command Usage:**
```
🎮 COMMAND
Command: !uptime
User: Username
```

**Shoutouts:**
```
✅ SUCCESS
Shoutout Given
From: Moderator
To: @somestreamer
```

**First-Time Visitors:**
```
ℹ️ INFO
First Time Viewer
User: newviewer
Message: Welcome message sent
```

**Errors:**
```
❌ ERROR
Command Error
Command: !clip
Error: No clips found
```

## Best Practices

### Command Organization

1. **Start minimal** - Don't add everything at once
2. **Group by category** - Stream info, viewer info, tools
3. **Test each command** - Before going live
4. **Document for mods** - Let mods know what exists

### Viewer Engagement

1. **!commands** - Essential for discoverability
2. **!discord** - Grow your community
3. **!so** - Support other streamers
4. **Welcome messages** - Make new viewers feel included

### Moderation

1. **Shoutout** - Moderators only (prevents spam)
2. **Title/Game** - Broadcaster only (prevent trolling)
3. **Add Quote** - Moderators only (quality control)

### Performance

1. **Watchtime tracking** - Uses minimal resources
2. **API calls** - StreamerBot handles rate limiting
3. **Logging** - Toggle off if too verbose

## Troubleshooting

### Common Issues

**Commands not responding:**
- ✅ Check chat trigger is enabled
- ✅ Verify StreamerBot is connected to Twitch
- ✅ Check for syntax errors in code

**Followage not working:**
- Twitch API may be slow
- User might not be following
- Check StreamerBot logs

**Shoutout not working:**
- Verify target user exists
- Check Twitch API status
- Ensure proper permissions

**Discord link not showing:**
- Run ConfigSetup.cs first
- Set Discord link in ConfigSetup.cs line 365
- Verify global variable is set

**Welcome messages not firing:**
- Check event trigger is enabled
- Verify message format isn't causing errors
- Test with alt account

## Support

Need help with utility commands?

- **Discord**: [https://discord.gg/ngQXHUbnKg](https://discord.gg/ngQXHUbnKg)
- **Individual Command READMEs**: Check each utility's folder
- **Main README**: See root StreamerBot-Commands folder

## Utility Folders

```
Utilities/
├── Uptime/               # Stream uptime display
├── Followage/            # Follower duration
├── Watchtime/            # Track viewing time
├── Stream-Info/          # Title & game commands
├── Shoutouts/            # Shoutout commands
│   ├── Shoutout-Simple/ # Basic shoutout
│   └── Shoutout-Full/   # Advanced shoutout
├── Discord/              # Discord invite link
├── Discord-Logging/      # Logging system
├── Multi-Twitch/         # Multi-stream links
├── Commands-List/        # List all commands
├── Quotes/               # Quote system
└── Welcome-Message/      # Welcome first-timers
```

## Credits

**Created by**: HexEchoTV (CUB) | [GitHub](https://github.com/ThortonEllmers/Streamerbot-Commands)
**License**: Free for personal use only




