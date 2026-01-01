# Leaderboard Command

[![Discord](https://img.shields.io/badge/Discord-Join%20for%20Help-7289da)](https://discord.gg/ngQXHUbnKg)

## Overview

The **Leaderboard Command** displays the top currency holders in your Twitch chat. This creates friendly competition and shows who has the most currency in your community.

## What This Command Does

- Displays top currency holders ranked by balance
- Shows configurable number of top users (default: top 5)
- Filters out users with zero balance
- Sorts users by balance in descending order
- Shows username and balance for each ranked user
- Logs leaderboard requests to Discord (if logging is enabled)

## Dependencies

This command requires the following files to be set up first:

1. **ConfigSetup.cs** (Required)
   - Location: `Currency/Core/Config-Setup/ConfigSetup.cs`
   - Purpose: Initializes currency name, key, and leaderboard count
   - **Must be run before using this command**

2. **Discord Logging** (Optional)
   - Built-in to this command file
   - Purpose: Logs leaderboard requests to Discord webhook
   - Only works if Discord webhook is configured in ConfigSetup.cs

## Installation

### Step 1: Run ConfigSetup.cs First

1. Open StreamerBot
2. Go to **Actions** tab
3. Import `Currency/Core/Config-Setup/ConfigSetup.cs`
4. Edit the file to configure leaderboard settings (line 35):
   ```csharp
   CPH.SetGlobalVar("config_leaderboard_top_count", 5, true);
   ```
5. **Run the action** to initialize all global variables

### Step 2: Import Leaderboard Command

1. In StreamerBot, go to **Actions** tab
2. Click **Import** (bottom left)
3. Select `Currency/Core/Leaderboard/LeaderboardCommand.cs`
4. Click **Import**

### Step 3: Create Chat Command Trigger

1. In the action you just imported, go to **Triggers** tab
2. Click **Add** → **Chat Message** → **Command**
3. Configure the trigger:
   - **Command**: `!leaderboard`
   - **Permission**: Everyone
   - **Enabled**: Check this box
4. Click **OK**

### Step 4: (Optional) Add Alias Commands

You may want to add alternative commands:

1. Add another trigger with command: `!lb`
2. Add another trigger with command: `!top`
3. All will trigger the same leaderboard display

### Step 5: Test the Command

1. Make sure some users have earned currency (use `!daily` or games)
2. Type in chat: `!leaderboard`
3. The bot should display the top currency holders

## Usage

### Basic Command

```
!leaderboard
```

**Response (with users):**
```
Top 5 Cub Coins holders: 1. CubSoftware ($1250) 2. Viewer123 ($890) 3. GamerDude ($500) 4. StreamFan ($350) 5. CoolViewer ($200)
```

**Response (no currency yet):**
```
No one has any Cub Coins yet!
```

### Alternative Commands

If you set up aliases:
```
!lb
!top
```

Both display the same leaderboard.

## Configuration

### Changing Number of Top Users

To change how many users are shown on the leaderboard:

1. Open `ConfigSetup.cs`
2. Find line 35:
   ```csharp
   CPH.SetGlobalVar("config_leaderboard_top_count", 5, true);
   ```
3. Change `5` to your preferred number (e.g., 3, 10, 20)
4. **Run ConfigSetup.cs again** to save changes

**Examples:**
- `3` - Shows top 3 users
- `10` - Shows top 10 users
- `20` - Shows top 20 users (may be too long for chat)

### Customizing the Response Message

To change how the leaderboard is displayed:

1. Open `LeaderboardCommand.cs`
2. Find lines 66-72 to customize the message format:

**Current format (line 66):**
```csharp
string message = $"Top {leaderboard.Count} {currencyName} holders: ";
```

**Custom examples:**
```csharp
// With emoji
string message = $"🏆 Top {leaderboard.Count} {currencyName} holders: ";

// More descriptive
string message = $"Leaderboard - Top {leaderboard.Count} richest viewers: ";

// Competitive
string message = $"👑 {currencyName} Kings & Queens: ";
```

**Customizing individual rank display (lines 68-71):**

Current format:
```csharp
message += $"{i + 1}. {entry.UserLogin} (${entry.Value}) ";
```

Custom examples:
```csharp
// With medals for top 3
string rank = (i == 0) ? "🥇" : (i == 1) ? "🥈" : (i == 2) ? "🥉" : $"{i + 1}.";
message += $"{rank} {entry.UserLogin} (${entry.Value}) ";

// With separators
message += $"{i + 1}. {entry.UserLogin}: ${entry.Value} | ";

// More compact
message += $"#{i + 1} {entry.UserLogin} ${entry.Value} ";
```

### Customizing Empty Leaderboard Message

When no one has currency yet (lines 61, 34):

```csharp
CPH.SendMessage($"No one has any {currencyName} yet!");
```

Custom examples:
```csharp
CPH.SendMessage($"The {currencyName} leaderboard is empty! Be the first to earn some!");
CPH.SendMessage($"Nobody has earned {currencyName} yet. Type !daily to get started!");
```

## What Gets Logged?

If Discord logging is enabled:

### Command Execution Log (Purple)
```
🎮 COMMAND
Command: !leaderboard
User: [Username]
```

### Error Log (Red)
Only appears if something goes wrong:
```
❌ ERROR
Leaderboard Command Error
User: [Username] | Error: [Error message]
```

## Troubleshooting

### Issue: "No one has any currency yet" even though people do

**Solutions:**
1. Make sure ConfigSetup.cs has been run
2. Verify `config_currency_key` is consistent across all commands
3. Check that the currency key matches what's used in earning commands
4. Use `!balance` to confirm users actually have currency
5. Check StreamerBot's Global Variables to verify the currency key

### Issue: Leaderboard shows wrong usernames

**Solutions:**
1. This shouldn't happen - the command uses Twitch's UserLogin field
2. If usernames appear incorrect, try restarting StreamerBot
3. Check StreamerBot logs for any API errors

### Issue: Leaderboard shows fewer users than configured

**Solutions:**
- This is normal - the command only shows users with **positive balances**
- If you set top count to 10 but only 5 users have currency, only 5 will show
- Users with 0 balance are automatically filtered out

### Issue: Leaderboard is too long for Twitch chat

**Solutions:**
1. Reduce the number of users shown in ConfigSetup.cs
2. Twitch chat has a 500-character limit
3. Recommended maximum: 5-7 users to avoid truncation
4. Consider shortening currency name if using a long one

### Issue: Command doesn't respond

**Solutions:**
1. Make sure ConfigSetup.cs has been run
2. Check that the Chat Command trigger is **enabled**
3. Verify trigger command is `!leaderboard` (case-insensitive)
4. Check StreamerBot logs for errors
5. Ensure StreamerBot is connected to Twitch

## Advanced Features

### Adding Rank Indicators

To add visual flair for top positions:

1. Open `LeaderboardCommand.cs`
2. Modify the loop section (around line 68):

```csharp
for (int i = 0; i < leaderboard.Count; i++)
{
    var entry = leaderboard[i];

    // Add medals for top 3
    string rankDisplay;
    if (i == 0) rankDisplay = "🥇";
    else if (i == 1) rankDisplay = "🥈";
    else if (i == 2) rankDisplay = "🥉";
    else rankDisplay = $"{i + 1}.";

    message += $"{rankDisplay} {entry.UserLogin} (${entry.Value}) ";
}
```

### Showing User's Own Rank

To also show the command user their position:

Add after line 74:
```csharp
// Show user's own rank if not in top
string userId = args["userId"].ToString();
int userBalance = CPH.GetTwitchUserVarById<int>(userId, currencyKey, true);

// Find user's rank in full list
int userRank = -1;
var allBalancesList = allBalances.OrderByDescending(x => x.Value).ToList();
for (int i = 0; i < allBalancesList.Count; i++)
{
    if (allBalancesList[i].UserId == userId)
    {
        userRank = i + 1;
        break;
    }
}

if (userRank > topCount)
{
    CPH.SendMessage($"{userName}, you are rank #{userRank} with ${userBalance} {currencyName}.");
}
```

### Creating Multiple Leaderboards

You can create separate leaderboards for different currencies by:

1. Duplicating the LeaderboardCommand.cs file
2. Modifying the currency key variable to use different currency
3. Creating separate chat commands (e.g., `!leaderboard-gold`, `!leaderboard-gems`)

## Performance Notes

- The command fetches ALL user currency balances
- For channels with thousands of users, this may take a moment
- Leaderboard is computed fresh each time (no caching)
- If performance is an issue, consider adding a cooldown

### Adding a Cooldown

To prevent spam:

1. In StreamerBot, go to the Leaderboard action
2. Click **Settings** or **Queues**
3. Add a cooldown:
   - User Cooldown: 30 seconds (per user)
   - Global Cooldown: 10 seconds (affects everyone)

## Related Commands

- **!balance** - Check your own currency balance
- **!daily** - Claim daily currency to climb the leaderboard
- **!give** - Transfer currency to others
- **All game commands** - Earn currency to rank higher

## Technical Details

### How Leaderboard is Calculated

1. Fetches all Twitch user variables for the currency key
2. Filters out users with 0 or negative balance
3. Sorts remaining users by balance (highest to lowest)
4. Takes top N users (configured in ConfigSetup.cs)
5. Formats and displays as a single message

### Data Source

- Uses `CPH.GetTwitchUsersVar<int>()` to fetch all user balances
- Returns a list of `UserVariableValue<int>` objects
- Each object contains: UserId, UserLogin, and Value (balance)

## Support

Need help? Join our Discord for support:
- Discord: [https://discord.gg/ngQXHUbnKg](https://discord.gg/ngQXHUbnKg)
- Report issues on the GitHub repository

## File Information

- **File**: `LeaderboardCommand.cs`
- **Location**: `Currency/Core/Leaderboard/`
- **Created by**: HexEchoTV (CUB) | [GitHub](https://github.com/ThortonEllmers/Streamerbot-Commands)
- **License**: Free for personal use only
- **Dependencies**: ConfigSetup.cs




