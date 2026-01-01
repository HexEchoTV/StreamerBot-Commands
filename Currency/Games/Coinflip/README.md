# Coinflip Command

[![Discord](https://img.shields.io/badge/Discord-Join%20for%20Help-7289da)](https://discord.gg/ngQXHUbnKg)

## Overview

The **Coinflip Command** is a simple gambling game where users bet on the outcome of a coin flip (heads or tails). If they guess correctly, they win double their bet!

## What This Command Does

- Flips a virtual coin (heads or tails)
- Users bet currency and choose their prediction
- 50/50 chance to win
- Winners receive 2x their bet amount
- Losers lose their bet
- Logs all flips, wins, and losses to Discord (if logging is enabled)
- Validates bet amounts and user balances

## Dependencies

This command requires the following files to be set up first:

1. **ConfigSetup.cs** (Required)
   - Location: `Currency/Core/Config-Setup/ConfigSetup.cs`
   - Purpose: Initializes coinflip settings (min/max bet, win multiplier)
   - **Must be run before using this command**

2. **Discord Logging** (Optional)
   - Built-in to this command file
   - Purpose: Logs all coinflip attempts and results to Discord
   - Only works if Discord webhook is configured in ConfigSetup.cs

## Installation

### Step 1: Run ConfigSetup.cs First

1. Open StreamerBot
2. Go to **Actions** tab
3. Import `Currency/Core/Config-Setup/ConfigSetup.cs`
4. Edit the file to configure coinflip settings (lines 102-105):
   ```csharp
   CPH.SetGlobalVar("config_coinflip_min_bet", 10, true);
   CPH.SetGlobalVar("config_coinflip_max_bet", 500, true);
   CPH.SetGlobalVar("config_coinflip_win_mult", 2, true);
   ```
5. **Run the action** to initialize all global variables

### Step 2: Import Coinflip Command

1. In StreamerBot, go to **Actions** tab
2. Click **Import** (bottom left)
3. Select `Currency/Games/Coinflip/CoinflipCommand.cs`
4. Click **Import**

### Step 3: Create Chat Command Trigger

1. In the action you just imported, go to **Triggers** tab
2. Click **Add** → **Chat Message** → **Command**
3. Configure the trigger:
   - **Command**: `!coinflip`
   - **Permission**: Everyone
   - **Enabled**: Check this box
4. Click **OK**

### Step 4: Test the Command

1. Make sure you have some currency (use `!daily` or `!balance`)
2. Type in chat: `!coinflip heads 50`
3. Watch the result!

## Usage

### Basic Command

```
!coinflip <heads/tails> <amount>
```

### Examples

**Bet on heads:**
```
!coinflip heads 50
```

**Bet on tails:**
```
!coinflip tails 100
```

**Short form (h/t):**
```
!coinflip h 25
!coinflip t 75
```

### Responses

**Win:**
```
🪙 HEADS! [Username] WON $100 Cub Coins! Balance: $350
```

**Loss:**
```
🪙 TAILS! [Username] LOST $50 Cub Coins. Balance: $200
```

**Insufficient funds:**
```
[Username], you only have $25 Cub Coins! You need $50.
```

**Invalid bet:**
```
[Username], bet must be between 10 and 500 Cub Coins!
```

## Configuration

### Changing Minimum Bet

To change the minimum bet amount:

1. Open `ConfigSetup.cs`
2. Find line 103:
   ```csharp
   CPH.SetGlobalVar("config_coinflip_min_bet", 10, true);
   ```
3. Change `10` to your preferred minimum
4. **Run ConfigSetup.cs again** to save changes

### Changing Maximum Bet

To change the maximum bet amount:

1. Open `ConfigSetup.cs`
2. Find line 104:
   ```csharp
   CPH.SetGlobalVar("config_coinflip_max_bet", 500, true);
   ```
3. Change `500` to your preferred maximum
4. **Run ConfigSetup.cs again** to save changes

### Changing Win Multiplier

To change how much users win:

1. Open `ConfigSetup.cs`
2. Find line 105:
   ```csharp
   CPH.SetGlobalVar("config_coinflip_win_mult", 2, true);
   ```
3. Change `2` to your preferred multiplier
   - `2` = double your bet (default)
   - `3` = triple your bet
   - `1.5` = 1.5x your bet
4. **Run ConfigSetup.cs again** to save changes

### Customizing Messages

To change the response messages:

1. Open `CoinflipCommand.cs`

**Win message (line 105):**
```csharp
CPH.SendMessage($"{coinEmoji} {result.ToUpper()}! {user} WON ${winnings} {currencyName}! Balance: ${balance}");
```

**Loss message (line 113):**
```csharp
CPH.SendMessage($"{coinEmoji} {result.ToUpper()}! {user} LOST ${betAmount} {currencyName}. Balance: ${balance}");
```

**Custom examples:**
```csharp
// More dramatic
CPH.SendMessage($"🎰 WINNER! {user} guessed {choice} and won ${winnings}!");

// Simple
CPH.SendMessage($"{user} won ${winnings}");

// Celebratory
CPH.SendMessage($"🎉 {result}! {user} predicted correctly and won ${winnings} {currencyName}!");
```

## What Gets Logged?

If Discord logging is enabled:

### Command Execution Log (Purple)
```
🎮 COMMAND
Command: !coinflip
User: [Username]
Details: Choice: heads | Bet: $50
```

### Success Log (Green) - Win
```
✅ SUCCESS
Coinflip Win
User: [Username]
Choice: heads
Result: heads
Bet: $50
Winnings: $100
New Balance: $350
```

### Warning Log (Orange) - Loss
```
⚠️ WARNING
Coinflip Loss
User: [Username]
Choice: heads
Result: tails
Bet: $50
Loss: $50
New Balance: $200
```

### Warning Log (Orange) - Insufficient Funds
```
⚠️ WARNING
Coinflip Insufficient Funds
User: [Username]
Bet: $100
Balance: $50
```

### Error Log (Red)
```
❌ ERROR
Coinflip Command Exception
Error: [Error message]
Stack Trace: [Stack trace]
```

## Troubleshooting

### Issue: Command doesn't respond

**Solutions:**
1. Make sure ConfigSetup.cs has been run
2. Check that the Chat Command trigger is **enabled**
3. Verify trigger is set to `!coinflip` (case-insensitive)
4. Check StreamerBot logs for errors

### Issue: "Invalid bet amount" error

**Solutions:**
1. Make sure you're entering a valid number
2. Check that your bet is between min and max
3. Don't use decimals (only whole numbers)
4. Format: `!coinflip heads 50` (not `!coinflip heads $50`)

### Issue: Balance doesn't update after win

**Solutions:**
1. Check StreamerBot logs for errors
2. Verify currency key is consistent
3. Use `!balance` to check current balance
4. Make sure ConfigSetup.cs was run

### Issue: Always shows the same result

**Solution:**
- This shouldn't happen - the Random class generates new results each time
- If this occurs, restart StreamerBot
- Check for any errors in the logs

## Game Mechanics

### Probability

- **Heads**: 50% chance
- **Tails**: 50% chance
- Truly random using C# Random class

### Expected Value

With default settings (2x multiplier):
- Expected value = 0 (fair game)
- Win: +100% of bet
- Loss: -100% of bet
- Average outcome over time: Break even

### Strategy

Since this is a 50/50 game:
- No strategy can improve your odds
- Best approach: Set a budget and stick to it
- Don't chase losses
- Consider it entertainment, not a way to earn currency

## Advanced Features

### Changing Coin Emoji

To use different emojis:

1. Open `CoinflipCommand.cs`
2. Find line 91:
   ```csharp
   string coinEmoji = result == "heads" ? "🪙" : "🪙";
   ```
3. Change to different emojis:
   ```csharp
   // Different emojis for heads vs tails
   string coinEmoji = result == "heads" ? "👑" : "💀";

   // Single emoji
   string coinEmoji = "🎲";
   ```

### Adding Betting Streaks

To track consecutive wins/losses:

1. Add after line 99 in win section:
   ```csharp
   // Track win streak
   int winStreak = CPH.GetTwitchUserVarById<int>(userId, "coinflip_streak", true);
   winStreak++;
   CPH.SetTwitchUserVarById(userId, "coinflip_streak", winStreak, true);
   CPH.SendMessage($"{user} is on a {winStreak}-win streak!");
   ```

2. Reset streak on loss (after line 112):
   ```csharp
   // Reset streak on loss
   CPH.SetTwitchUserVarById(userId, "coinflip_streak", 0, true);
   ```

### Adding Cooldown

To prevent spam:

1. In StreamerBot, go to the Coinflip action
2. Click **Settings** or **Queues**
3. Add a cooldown:
   - User Cooldown: 10 seconds (per user)
   - This prevents one user from spamming the command

## Related Commands

- **!balance** - Check your current balance
- **!daily** - Claim daily currency
- **!gamble** - Another gambling command with different mechanics
- **!slots** - Slot machine gambling game

## Economy Impact

### Balancing Considerations

- Coinflip is a **zero-sum game** (fair odds)
- Over time, the economy stays balanced
- Players can't get rich from coinflip alone
- Good for entertainment without breaking economy

### Recommended Settings

**Conservative (slow economy):**
- Min bet: 25, Max bet: 250
- Win mult: 2

**Balanced (moderate economy):**
- Min bet: 10, Max bet: 500 (default)
- Win mult: 2

**Generous (fast economy):**
- Min bet: 50, Max bet: 1000
- Win mult: 2.5

## Support

Need help? Join our Discord for support:
- Discord: [https://discord.gg/ngQXHUbnKg](https://discord.gg/ngQXHUbnKg)
- Report issues on the GitHub repository

## File Information

- **File**: `CoinflipCommand.cs`
- **Location**: `Currency/Games/Coinflip/`
- **Created by**: HexEchoTV (CUB) | [GitHub](https://github.com/HexEchoTV/Streamerbot-Commands)
- **License**: MIT License
- **Dependencies**: ConfigSetup.cs




