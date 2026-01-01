# Daily Claim Command

[![Discord](https://img.shields.io/badge/Discord-Join%20for%20Help-7289da)](https://discord.gg/ngQXHUbnKg)

## Overview

The **Daily Claim Command** allows viewers to claim a daily currency reward. This is the primary way for viewers to earn currency without gambling or playing games. Users can claim once per day (or per configured cooldown period).

## What This Command Does

- Allows users to claim a daily currency reward
- Enforces a cooldown period between claims (default: 24 hours)
- Tracks consecutive claim count for each user
- Shows remaining time if trying to claim too early
- Updates user balance automatically
- Logs all claims, cooldowns, and errors to Discord (if logging is enabled)
- Prevents double-claiming with timestamp tracking

## Dependencies

This command requires the following files to be set up first:

1. **ConfigSetup.cs** (Required)
   - Location: `Currency/Core/Config-Setup/ConfigSetup.cs`
   - Purpose: Initializes daily reward amount and cooldown settings
   - **Must be run before using this command**

2. **Discord Logging** (Optional)
   - Built-in to this command file
   - Purpose: Logs all daily claims and attempts to Discord
   - Only works if Discord webhook is configured in ConfigSetup.cs

## Installation

### Step 1: Run ConfigSetup.cs First

1. Open StreamerBot
2. Go to **Actions** tab
3. Import `Currency/Core/Config-Setup/ConfigSetup.cs`
4. Edit the file to configure daily settings (lines 28-29):
   ```csharp
   CPH.SetGlobalVar("config_daily_reward", 100, true);
   CPH.SetGlobalVar("config_daily_cooldown_hours", 24, true);
   ```
5. **Run the action** to initialize all global variables

### Step 2: Import Daily Command

1. In StreamerBot, go to **Actions** tab
2. Click **Import** (bottom left)
3. Select `Currency/Core/Daily-Claim/DailyRedemption.cs`
4. Click **Import**

### Step 3: Create Chat Command Trigger

1. In the action you just imported, go to **Triggers** tab
2. Click **Add** → **Chat Message** → **Command**
3. Configure the trigger:
   - **Command**: `!daily`
   - **Permission**: Everyone
   - **Enabled**: Check this box
4. Click **OK**

### Step 4: Test the Command

1. Type in chat: `!daily`
2. You should receive your first daily reward
3. Try again immediately - you should get a cooldown message

## Usage

### First Claim

```
!daily
```

**Response:**
```
[Username] claimed their daily $100 Cub Coins! (Day 1) Balance: $100 Cub Coins
```

### Second Claim (Next Day)

```
!daily
```

**Response:**
```
[Username] claimed their daily $100 Cub Coins! (Day 2) Balance: $200 Cub Coins
```

### Attempting Before Cooldown

```
!daily
```

**Response:**
```
[Username], you already claimed your daily Cub Coins! Come back in 23h 45m.
```

## Configuration

### Changing Daily Reward Amount

To change how much currency users receive:

1. Open `ConfigSetup.cs`
2. Find line 28:
   ```csharp
   CPH.SetGlobalVar("config_daily_reward", 100, true);
   ```
3. Change `100` to your preferred amount
4. **Run ConfigSetup.cs again** to save changes

**Examples:**
- `50` - Small daily reward
- `100` - Default balanced amount
- `250` - Generous daily reward
- `1000` - Very generous (may impact economy)

### Changing Cooldown Period

To change how often users can claim:

1. Open `ConfigSetup.cs`
2. Find line 29:
   ```csharp
   CPH.SetGlobalVar("config_daily_cooldown_hours", 24, true);
   ```
3. Change `24` to your preferred hours
4. **Run ConfigSetup.cs again** to save changes

**Examples:**
- `12` - Twice per day
- `24` - Once per day (default)
- `48` - Every 2 days
- `168` - Once per week

### Customizing Success Message

To change the claim confirmation message:

1. Open `DailyRedemption.cs`
2. Find line 27:
   ```csharp
   string successMessage = "{user} claimed their daily ${coins} {currency}! (Day {count}) Balance: ${total} {currency}";
   ```
3. Customize using these placeholders:
   - `{user}` - Username
   - `{coins}` - Daily reward amount
   - `{currency}` - Currency name
   - `{count}` - Consecutive claim count
   - `{total}` - New total balance

**Custom examples:**
```csharp
// Simple version
string successMessage = "{user} received ${coins} {currency}! New balance: ${total}";

// With emoji
string successMessage = "💰 {user} claimed ${coins} {currency}! (Streak: {count} days) Total: ${total}";

// Motivational
string successMessage = "✅ Daily claimed! {user} now has ${total} {currency}. Keep the {count}-day streak going!";
```

### Customizing Cooldown Message

To change the "already claimed" message:

1. Open `DailyRedemption.cs`
2. Find line 28:
   ```csharp
   string alreadyClaimedMessage = "{user}, you already claimed your daily {currency}! Come back in {hours}h {minutes}m.";
   ```
3. Customize using these placeholders:
   - `{user}` - Username
   - `{currency}` - Currency name
   - `{hours}` - Hours remaining
   - `{minutes}` - Minutes remaining

**Custom examples:**
```csharp
// Friendly
string alreadyClaimedMessage = "Hey {user}, you already got your daily! Try again in {hours}h {minutes}m 😊";

// Timer-focused
string alreadyClaimedMessage = "⏰ {user}, daily cooldown: {hours}h {minutes}m remaining";

// Encouraging
string alreadyClaimedMessage = "{user}, you're on a roll! Come back in {hours}h {minutes}m for Day {count}!";
```

## What Gets Logged?

If Discord logging is enabled:

### Command Execution Log (Purple)
```
🎮 COMMAND
Command: !daily
User: [Username]
```

### Success Log (Green)
```
✅ SUCCESS
Daily Claimed Successfully
User: [Username]
Reward: 100 Cub Coins
New Balance: 250 Cub Coins
Claim Count: 3
```

### Warning Log (Orange)
When user tries to claim before cooldown expires:
```
⚠️ WARNING
Daily Cooldown Active
User: [Username]
Time Remaining: 23h 45m
```

### Error Log (Red)
If something goes wrong:
```
❌ ERROR
Daily Command Error
User: [Username]
Error: [Error message]
Stack Trace: [Stack trace]
```

## Troubleshooting

### Issue: Command doesn't respond

**Solutions:**
1. Make sure ConfigSetup.cs has been run first
2. Check that the Chat Command trigger is **enabled**
3. Verify trigger is set to `!daily` (case-insensitive)
4. Check StreamerBot logs for errors
5. Ensure StreamerBot is connected to Twitch

### Issue: "Already claimed" immediately after claiming

**Solutions:**
1. This is normal - the cooldown is working
2. Wait for the full cooldown period before claiming again
3. Check remaining time in the bot's response

### Issue: Cooldown time seems wrong

**Solutions:**
1. Verify `config_daily_cooldown_hours` in Global Variables
2. Make sure you ran ConfigSetup.cs after changing the value
3. The timer is based on UTC time, not local time
4. If recently changed, old claims still use the old cooldown

### Issue: Claim count resets unexpectedly

**Solution:**
- Claim count tracks total claims, not consecutive daily claims
- Currently, there's no streak-breaking logic
- Count increases every time you claim, regardless of gaps

### Issue: Balance doesn't update

**Solutions:**
1. Check that `config_currency_key` is consistent
2. Use `!balance` to verify your current balance
3. Check StreamerBot logs for errors
4. Verify ConfigSetup.cs was run properly

## Advanced Features

### Adding Streak Bonuses

To reward consecutive daily claims with bonus currency:

1. Open `DailyRedemption.cs`
2. After line 84, add:
   ```csharp
   // Streak bonus: +10 coins per consecutive day
   int streakBonus = 0;
   if (timeSinceLastClaim.TotalHours < cooldownHours * 1.5)
   {
       // User claimed on time - add streak bonus
       streakBonus = claimCount * 10;
       dailyReward += streakBonus;
   }
   ```

### Resetting Streaks After Missed Days

To reset claim count if user misses a day:

1. Open `DailyRedemption.cs`
2. After line 59, add:
   ```csharp
   // Reset streak if more than 1.5x cooldown has passed
   if (timeSinceLastClaim.TotalHours > cooldownHours * 1.5)
   {
       claimCount = 0; // Reset streak
   }
   ```

### Showing Next Claim Time

To show exact time when daily will be available:

1. After line 78, add:
   ```csharp
   DateTime nextClaimTime = lastClaim.AddHours(cooldownHours);
   CPH.SendMessage($"{userName}, you can claim again at {nextClaimTime.ToLocalTime():HH:mm} (local time).");
   ```

## Related Commands

- **!balance** - Check your current balance
- **!leaderboard** - See if your daily claims put you on top
- **!streak** - If using streak command, check your consecutive days
- **Game commands** - Use your daily currency on games

## Technical Details

### How Daily Claims Are Tracked

1. **Last Claim Time**: Stored as `daily_lastclaim` (Twitch User Variable)
   - Format: ISO 8601 timestamp (UTC)
   - Example: `2025-12-31T14:30:00.0000000Z`

2. **Claim Count**: Stored as `daily_claimcount` (Twitch User Variable)
   - Type: Integer
   - Increments by 1 each claim
   - Never decreases (unless manually reset)

3. **Balance Update**: Adds to existing `{currencyKey}` value
   - Example: `cubcoins` variable

### Cooldown Calculation

```
Current Time (UTC) - Last Claim Time = Time Since Last Claim
If Time Since Last Claim < Cooldown Hours → ON COOLDOWN
If Time Since Last Claim >= Cooldown Hours → CAN CLAIM
```

### Time Zone Handling

- All times stored in **UTC** (Universal Time)
- Cooldown is calculated in UTC
- Display messages show hours/minutes remaining
- For local time display, use `ToLocalTime()` method

## Economy Balancing

### Calculating Daily Impact

With default settings (100 coins, 24hr cooldown):
- **Daily per user**: 100 coins
- **Weekly per user**: 700 coins
- **Monthly per user**: ~3,000 coins

Adjust `config_daily_reward` based on:
- Your game betting limits
- How generous you want the economy
- How active your community is

**Recommended ranges:**
- **Conservative**: 50-75 coins
- **Balanced**: 100-150 coins
- **Generous**: 200-300 coins
- **Very generous**: 500+ coins

## Support

Need help? Join our Discord for support:
- Discord: [https://discord.gg/ngQXHUbnKg](https://discord.gg/ngQXHUbnKg)
- Report issues on the GitHub repository

## File Information

- **File**: `DailyRedemption.cs`
- **Location**: `Currency/Core/Daily-Claim/`
- **Created by**: HexEchoTV (CUB) | [GitHub](https://github.com/HexEchoTV/Streamerbot-Commands)
- **License**: Free for personal use only
- **Dependencies**: ConfigSetup.cs




