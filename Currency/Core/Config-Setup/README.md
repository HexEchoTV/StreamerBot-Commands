# Config Setup

The central configuration system for the entire Currency command suite.

## Overview

`ConfigSetup.cs` is the **foundation file** that initializes all global variables used across all currency commands. This single file controls all settings for earning commands, gambling games, adventure games, PVP mechanics, and system integrations.

## Quick Start

1. **Create a C# action** in StreamerBot with the code from `ConfigSetup.cs`
2. **Run it once** to initialize all configuration values
3. **Edit values** in StreamerBot's Global Variables UI to customize settings
4. **Or** edit this file and run it again to update all values at once

## What This Configures

### Core Currency Settings
- Currency name and internal key
- Daily rewards and cooldowns
- Transfer/give settings
- Leaderboard display count

### Work/Earning Commands (10 commands)
- Work, Fish, Hunt, Mine, Forage
- Scavenge, Dig, Search, Collect, Beg
- Each with customizable min/max rewards and cooldowns

### Betting/Gambling Games (16 commands)
- Wheel, Coinflip, Slots, Blackjack, Roulette
- Dice, Flip, Gamble, Bingo, Keno
- Lottery, Highlow, Plinko, Scratch, Spin
- Crash, Limbo, Match
- Each with bet limits and multipliers

### Adventure/Challenge Games (10 commands)
- Tower, Heist, Boss, Vault, Dungeon
- Explore, Quest, Ladder, Battle, Mines, Race
- Each with rewards, success rates, and cooldowns

### PVP Games (2 commands)
- Duel - User vs User betting
- Rob - Steal from other users

### Special Games (10 commands)
- Trivia (single-player and live Google Sheets)
- Treasure Hunt (timed spawns)
- Bounty, Crime, Invest, Luck
- Magic, Pet, Pickpocket, Streak

### System Integrations
- **Twitch API** - For clip creation, title/game changes
- **Discord Logging** - Centralized webhook logging for all commands
- **Discord Server Link** - For the !discord command

## Configuration Files

### Required API Credentials

Edit lines 344-346 for Twitch API integration:
```csharp
string twitchAccessToken = "YOUR_ACCESS_TOKEN_HERE";
string twitchRefreshToken = "YOUR_REFRESH_TOKEN_HERE";
string twitchClientId = "YOUR_CLIENT_ID_HERE";
```

Get credentials from: https://twitchtokengenerator.com/

**Required scopes:**
- `channel:manage:broadcast` (for clips, stream title, game commands)
- `moderator:read:followers` (for followage command)

### Discord Webhook Setup

Edit line 357 for Discord logging:
```csharp
string discordWebhookUrl = "YOUR_WEBHOOK_URL_HERE";
```

Get webhook from: Discord Server Settings → Integrations → Webhooks

Toggle logging on/off at line 361:
```csharp
bool discordLoggingEnabled = true;  // Set to false to disable
```

## How It Works

1. All commands read their settings from **global variables**
2. This file creates/updates those global variables
3. Changes take effect immediately for all commands
4. No need to edit individual command files

## Customization

### Option 1: StreamerBot UI
1. Go to **Settings → Global Variables**
2. Find variables starting with `config_`
3. Edit values directly
4. Changes apply immediately

### Option 2: Edit & Rerun
1. Edit values in `ConfigSetup.cs`
2. Run the action again in StreamerBot
3. All global variables update at once

## Dependencies

- **None** - This is the foundation file
- All other commands depend on this file being run first

## Files

| File | Description |
|------|-------------|
| `ConfigSetup.cs` | Main configuration script |

## Related Commands

All commands in the Currency system use these configuration values:
- `/Currency/Core/` - Balance, Daily, Give, Leaderboard
- `/Currency/Work/` - All earning commands
- `/Currency/Games/` - All gambling and adventure games

## Author

Created by **HexEchoTV (CUB)**
https://github.com/HexEchoTV/Streamerbot-Commands

## License

Licensed under the MIT License. See LICENSE file in the project root for full license information.
