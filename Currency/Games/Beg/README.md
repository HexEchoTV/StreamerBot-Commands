# Beg Command

[![Discord](https://img.shields.io/badge/Discord-Join%20for%20Help-7289da)](https://discord.gg/ngQXHUbnKg)

## Overview

The **Beg Command** allows viewers to beg for small amounts of currency. This is a low-reward, low-cooldown way to earn currency, with a chance of failure where no currency is earned.

## What This Command Does

- Request small amounts of currency from generous souls
- 80% success rate (20% chance of failure)
- Random fun response messages (e.g., "The streamer felt generous", "A mod took pity on you")
- Earns random currency between configured min/max on success
- Enforces a cooldown period (default: 10 minutes)
- No risk of losing currency (worst case: nothing earned)
- Logs all beg attempts, successes, and failures to Discord (if logging is enabled)

## Dependencies

This command requires the following files to be set up first:

1. **ConfigSetup.cs** (Required)
   - Location: `Currency/Core/Config-Setup/ConfigSetup.cs`
   - Purpose: Initializes beg earnings and cooldown settings
   - **Must be run before using this command**

## Installation

### Quick Setup

1. Run `ConfigSetup.cs` first (sets: min=5, max=30, cooldown=10 minutes)
2. Import `Currency/Games/Beg/BegCommand.cs` into StreamerBot
3. Create chat command trigger for `!beg`
4. Test with `!beg` in chat

### Detailed Steps

See the [Work Command README](../Work/README.md) for detailed installation instructions. The process is identical, just use `!beg` instead of `!work`.

## Usage

### Basic Command

```
!beg
```

**Success Response:**
```
🤲 The streamer felt generous! [Username] received $15 Cub Coins. Balance: $215
```

**Failure Response:**
```
[Username] begged but everyone ignored you. Try again later!
```

**Cooldown Response:**
```
[Username], stop begging! Wait 8m 45s.
```

## Configuration

All beg settings are configured in `ConfigSetup.cs` (lines 85-87):

```csharp
CPH.SetGlobalVar("config_beg_min", 5, true);        // Min earnings
CPH.SetGlobalVar("config_beg_max", 30, true);       // Max earnings
CPH.SetGlobalVar("config_beg_cooldown_minutes", 10, true); // Cooldown
```

### Recommended Settings

**Very Generous (Easy Economy):**
- Min: 10, Max: 50, Cooldown: 5 minutes

**Default (Balanced):**
- Min: 5, Max: 30, Cooldown: 10 minutes

**Stingy (Hard Economy):**
- Min: 1, Max: 10, Cooldown: 20 minutes

## Custom Beg Responses

Edit lines 59-67 in `BegCommand.cs` to add your own responses:

```csharp
string[] responses = {
    "The streamer felt generous",
    "A mod took pity on you",
    "A viewer donated to you",
    "You found coins on the ground",
    "Someone accidentally tipped you",
    "A kind soul helped you out",
    "The chat felt bad for you",
    // Add your own:
    "A mysterious benefactor appeared",
    "Your sad story worked"
};
```

## Success Rate

The command has an 80% success rate:
- Line 72: `if (roll <= 20)` → 20% chance of failure
- To change success rate, modify the number:
  - `10` = 90% success rate
  - `20` = 80% success rate (default)
  - `50` = 50% success rate

## Economy Impact

### Earnings Comparison

With default settings:
- **!beg**: 5-30 coins, 10min cooldown → ~180 coins/hour
- **!work**: 25-100 coins, 30min cooldown → ~124 coins/hour
- **!daily**: 100 coins, 24hr cooldown → ~4 coins/hour

Despite faster cooldown, beg earns less per hour than work due to lower amounts!

## Related Commands

- **!work** - Higher earnings, longer cooldown
- **!daily** - Largest single reward, longest cooldown
- **!fish**, **!hunt**, **!mine** - Other earning commands

## Support

Need help? Join our Discord:
- Discord: [https://discord.gg/ngQXHUbnKg](https://discord.gg/ngQXHUbnKg)

## File Information

- **File**: `BegCommand.cs`
- **Location**: `Currency/Games/Beg/`
- **Created by**: HexEchoTV (CUB) | [GitHub](https://github.com/ThortonEllmers/Streamerbot-Commands)
- **Dependencies**: ConfigSetup.cs




