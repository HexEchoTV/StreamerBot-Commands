# Discord Invite Command

[![Discord](https://img.shields.io/badge/Discord-Join%20for%20Help-7289da)](https://discord.gg/ngQXHUbnKg)

## Overview

The **Discord Invite Command** allows your Twitch chat viewers to request your Discord server invite link. When a viewer types `!discord`, the bot will respond with your Discord server invite URL.

## What This Command Does

- Responds to `!discord` command in Twitch chat
- Shares your Discord server invite link with viewers
- Logs all command executions to Discord (if logging is enabled)
- Uses a configurable Discord invite link stored globally

## Dependencies

This command requires the following files to be set up first:

1. **ConfigSetup.cs** (Required)
   - Location: `Currency/Core/Config-Setup/ConfigSetup.cs`
   - Purpose: Initializes the `discordServerLink` global variable
   - **Must be run before using this command**

2. **Discord Logging** (Optional)
   - Location: `Utilities/Discord-Logging/DiscordLogger.cs`
   - Purpose: Logs command usage to Discord webhook
   - Only works if Discord webhook is configured in ConfigSetup.cs

## Installation

### Step 1: Run ConfigSetup.cs First

Before installing this command, you **must** run `ConfigSetup.cs`:

1. Open StreamerBot
2. Go to **Actions** tab
3. Import `Currency/Core/Config-Setup/ConfigSetup.cs`
4. Edit the file and set your Discord invite link (around line 365):
   ```csharp
   string discordServerLink = "https://discord.gg/YOUR_INVITE_LINK";
   ```
5. **Run the action** to initialize all global variables

### Step 2: Import Discord Command

1. In StreamerBot, go to **Actions** tab
2. Click **Import** (bottom left)
3. Select `Utilities/Discord/DiscordCommand.cs`
4. Click **Import**

### Step 3: Create Chat Command Trigger

1. In the action you just imported, go to **Triggers** tab
2. Click **Add** → **Chat Message** → **Command**
3. Configure the trigger:
   - **Command**: `!discord`
   - **Permission**: Everyone (or customize as needed)
   - **Enabled**: ✅ Check this box
4. Click **OK**

### Step 4: Test the Command

1. Go to your Twitch chat
2. Type: `!discord`
3. The bot should respond with your Discord invite link

## Usage

### Basic Command

```
!discord
```

**Response:**
```
💬 Join our Discord community, [Username]! → https://discord.gg/YOUR_INVITE_LINK
```

### Who Can Use This Command?

By default, **anyone** in your chat can use `!discord`. You can restrict it by:

1. Going to the action's **Triggers** tab
2. Editing the Chat Command trigger
3. Changing **Permission** to:
   - Moderators only
   - Subscribers only
   - VIPs only
   - Custom permission

## Configuration

### Changing Your Discord Invite Link

To change the Discord invite link:

1. Open `ConfigSetup.cs`
2. Find the Discord server link section (around line 365)
3. Update the URL:
   ```csharp
   string discordServerLink = "https://discord.gg/NEW_INVITE_LINK";
   ```
4. **Run ConfigSetup.cs again** to save the new link

### Disabling Discord Logging

If you don't want this command to log to Discord:

1. Type `!logging off` in your Twitch chat
2. This disables logging for ALL commands
3. To re-enable: `!logging on`

## What Gets Logged?

If Discord logging is enabled, the following is logged:

### Command Execution Log (Purple)
```
🎮 COMMAND
Command: !discord
User: [Username]
```

### Success Log (Green)
```
✅ SUCCESS
Discord Link Shared
User: [Username]
Link: https://discord.gg/YOUR_INVITE_LINK
```

### Warning Log (Orange)
Only appears if ConfigSetup.cs hasn't been run:
```
⚠️ WARNING
Discord Link Not Configured
Using fallback link. Please run ConfigSetup.cs
```

## Troubleshooting

### Issue: Bot doesn't respond to !discord

**Solutions:**
1. Make sure you've run **ConfigSetup.cs** first
2. Check that the Chat Command trigger is **enabled**
3. Verify the command is set to `!discord` (case-insensitive)
4. Check StreamerBot logs for errors

### Issue: Shows fallback link instead of my Discord link

**Solution:**
- You haven't run ConfigSetup.cs yet, or the `discordServerLink` variable isn't set
- Open ConfigSetup.cs, set your Discord link, and run it

### Issue: Command works but nothing logs to Discord

**Solutions:**
1. Discord logging might be disabled - Type `!logging status` to check
2. Discord webhook might not be configured in ConfigSetup.cs
3. Check that `discordLogWebhook` is set in ConfigSetup.cs

## Customization

### Changing the Response Message

Edit line 39 in `DiscordCommand.cs`:

```csharp
// Original:
CPH.SendMessage($"💬 Join our Discord community, {user}! → {discordLink}");

// Custom examples:
CPH.SendMessage($"🎮 Hey {user}! Join our Discord: {discordLink}");
CPH.SendMessage($"Come hang out with us on Discord: {discordLink}");
```

### Adding a Cooldown

To prevent spam:

1. In StreamerBot, go to the action
2. Click **Settings** tab
3. Add a **Queue** setting:
   - Delay: 5000ms (5 seconds)
   - Concurrent: 1

## Related Commands

- **!logging on/off** - Toggle Discord logging for all commands
- **!commands** - List all available commands (if you have CommandsList command)

## Support

Need help? Join our Discord for support:
- Discord: [https://discord.gg/ngQXHUbnKg](https://discord.gg/ngQXHUbnKg)
- Report issues on the GitHub repository

## File Information

- **File**: `DiscordCommand.cs`
- **Location**: `Utilities/Discord/`
- **Created by**: HexEchoTV (CUB) | [GitHub](https://github.com/HexEchoTV/Streamerbot-Commands)
- **License**: MIT License




