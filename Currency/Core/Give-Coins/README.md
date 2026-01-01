# Give Coins Command

[![Discord](https://img.shields.io/badge/Discord-Join%20for%20Help-7289da)](https://discord.gg/ngQXHUbnKg)

## Overview

The **Give Coins Command** allows viewers to transfer currency to other viewers in your Twitch chat. This enables player-to-player currency trading and gifting.

## What This Command Does

- Allows users to transfer currency to other users
- Validates sender has enough currency
- Enforces minimum transfer amount
- Prevents self-transfers
- Updates both sender and receiver balances
- Logs all transfers to Discord (if logging is enabled)
- Provides detailed transfer confirmation messages

## Dependencies

This command requires the following files to be set up first:

1. **ConfigSetup.cs** (Required)
   - Location: `Currency/Core/Config-Setup/ConfigSetup.cs`
   - Purpose: Initializes currency settings and minimum transfer amount
   - **Must be run before using this command**

2. **Discord Logging** (Optional)
   - Built-in to this command file
   - Purpose: Logs all transfers, failures, and errors to Discord
   - Only works if Discord webhook is configured in ConfigSetup.cs

## Installation

### Step 1: Run ConfigSetup.cs First

1. Open StreamerBot
2. Go to **Actions** tab
3. Import `Currency/Core/Config-Setup/ConfigSetup.cs`
4. Edit the file to configure transfer settings (line 32):
   ```csharp
   CPH.SetGlobalVar("config_give_min_amount", 1, true);
   ```
5. **Run the action** to initialize all global variables

### Step 2: Import Give Command

1. In StreamerBot, go to **Actions** tab
2. Click **Import** (bottom left)
3. Select `Currency/Core/Give-Coins/GiveCommand.cs`
4. Click **Import**

### Step 3: Create Chat Command Trigger

1. In the action you just imported, go to **Triggers** tab
2. Click **Add** → **Chat Message** → **Command**
3. Configure the trigger:
   - **Command**: `!give`
   - **Permission**: Everyone
   - **From**: Chat Message
   - **Enabled**: Check this box
4. Click **OK**

### Step 4: Test the Command

1. Make sure you have some currency (use `!daily` or give yourself some)
2. Type in chat: `!give @username 10`
3. The bot should confirm the transfer

## Usage

### Basic Command

```
!give @username amount
```

**Example:**
```
!give @cubsoftware 50
```

**Response:**
```
[YourName] gave $50 Cub Coins to cubsoftware!
```

### Usage Without @ Symbol

The @ symbol is optional:

```
!give cubsoftware 50
```

**Response:**
```
[YourName] gave $50 Cub Coins to cubsoftware!
```

### Minimum Transfer Amount

By default, the minimum transfer is 1 coin:

```
!give @username 0
```

**Response:**
```
[YourName], you must give at least 1 Cub Coins.
```

## Configuration

### Changing Minimum Transfer Amount

To change the minimum amount that can be transferred:

1. Open `ConfigSetup.cs`
2. Find line 32:
   ```csharp
   CPH.SetGlobalVar("config_give_min_amount", 1, true);
   ```
3. Change `1` to your preferred minimum (e.g., 10, 25, 100)
4. **Run ConfigSetup.cs again** to save changes

### Preventing Currency Trading (Disable Command)

If you don't want users to trade currency:

1. In StreamerBot, find the Give action
2. Go to **Triggers** tab
3. Uncheck **Enabled** on the `!give` trigger
4. Or delete the trigger entirely

### Customizing Success Message

To change the transfer confirmation message:

1. Open `GiveCommand.cs`
2. Find line 105:
   ```csharp
   CPH.SendMessage($"{userName} gave ${amount} {currencyName} to {targetUser}!");
   ```
3. Customize the format:
   ```csharp
   // Examples:
   CPH.SendMessage($"💰 {userName} gifted ${amount} {currencyName} to {targetUser}!");
   CPH.SendMessage($"Transfer complete! {userName} → {targetUser}: ${amount} {currencyName}");
   CPH.SendMessage($"{targetUser} received ${amount} {currencyName} from {userName}!");
   ```

### Customizing Error Messages

Edit these lines in `GiveCommand.cs`:

**Usage message (lines 42, 57):**
```csharp
CPH.SendMessage($"Usage: !give @username {minTransfer}+");
```

**Invalid amount (line 51):**
```csharp
CPH.SendMessage($"{userName}, please enter a valid number.");
```

**Minimum amount error (line 64):**
```csharp
CPH.SendMessage($"{userName}, you must give at least {minTransfer} {currencyName}.");
```

**User not found (line 75):**
```csharp
CPH.SendMessage($"{userName}, could not find user: {targetUser}");
```

**Insufficient funds (line 87):**
```csharp
CPH.SendMessage($"{userName}, you only have ${senderBalance} {currencyName}. You need ${amount}.");
```

## What Gets Logged?

If Discord logging is enabled, comprehensive logs are sent for all transfer events:

### Command Execution Log (Purple)
```
🎮 COMMAND
Command: !give
User: [Username]
Details: Sent $50 to cubsoftware
```

### Success Log (Green)
```
✅ SUCCESS
Transfer Complete
From: [Username] ($200 remaining)
To: cubsoftware ($150 total)
Amount: $50 Cub Coins
```

### Warning Log (Orange)
When user doesn't have enough currency:
```
⚠️ WARNING
!give - Insufficient Funds
User: [Username]
Attempted: $100
Balance: $50
Target: cubsoftware
```

### Error Log (Red)
If something goes wrong:
```
❌ ERROR
!give Command Error
Error: [Error message]
Stack Trace: [Stack trace]
```

## Troubleshooting

### Issue: Command doesn't work at all

**Solutions:**
1. Make sure you've run **ConfigSetup.cs** first
2. Check that the Chat Command trigger is **enabled**
3. Verify the trigger is set to `!give` (case-insensitive)
4. Check StreamerBot logs for errors
5. Ensure StreamerBot is connected to Twitch

### Issue: "Could not find user" error

**Solutions:**
1. Make sure the username is spelled correctly
2. The target user must have chatted in your channel at least once
3. The target user must exist on Twitch
4. Try without the @ symbol if you used it, or vice versa
5. Make sure there's no extra spaces in the username

### Issue: "You only have $X" error

**Solution:**
- You don't have enough currency to complete the transfer
- Check your balance with `!balance`
- Earn more currency through `!daily` or game commands

### Issue: "You must give at least X" error

**Solution:**
- You're trying to transfer less than the minimum amount
- Check `config_give_min_amount` in Global Variables
- Transfer a larger amount

### Issue: Transfer completes but balances don't update

**Solutions:**
1. Check that both users are using the same currency system
2. Verify `config_currency_key` is consistent in ConfigSetup.cs
3. Use `!balance` to refresh and check current balance
4. Check StreamerBot logs for errors during the transfer

### Issue: Can transfer to myself

**Note:** Currently, the command does NOT prevent self-transfers. If you want to add this check:

1. Open `GiveCommand.cs`
2. After line 79, add:
   ```csharp
   // Prevent self-transfer
   if (targetUserId == userId)
   {
       CPH.SendMessage($"{userName}, you cannot give currency to yourself!");
       return false;
   }
   ```

## Advanced Features

### Transfer Limits

To add maximum transfer limits, edit ConfigSetup.cs:

```csharp
CPH.SetGlobalVar("config_give_max_amount", 1000, true);
```

Then modify GiveCommand.cs to check this limit:
```csharp
int maxTransfer = CPH.GetGlobalVar<int>("config_give_max_amount", true);
if (amount > maxTransfer)
{
    CPH.SendMessage($"{userName}, you can only transfer up to ${maxTransfer} at a time.");
    return false;
}
```

### Transfer Tax/Fee

To implement a transfer fee (e.g., 10% tax):

1. Open `GiveCommand.cs`
2. After line 98, add:
   ```csharp
   // Apply 10% transfer tax
   int taxRate = 10; // 10%
   int taxAmount = (int)(amount * (taxRate / 100.0));
   int actualAmount = amount - taxAmount;

   // Update receiver with taxed amount instead
   CPH.SetTwitchUserVarById(targetUserId, currencyKey, receiverBalance + actualAmount, true);
   ```

## Related Commands

- **!balance** - Check your current currency balance
- **!leaderboard** - View top currency holders
- **!daily** - Claim daily currency reward
- **All game commands** - Earn currency to transfer

## Command Arguments

The command parses arguments as follows:

| Argument | Type | Description | Required |
|----------|------|-------------|----------|
| target | string | Username to transfer to (with or without @) | Yes |
| amount | integer | Amount to transfer | Yes |

**Valid formats:**
- `!give @username 100`
- `!give username 100`
- Both work identically

## Technical Details

### How Transfers Work

1. Command validates input (target user, amount)
2. Fetches target user info from Twitch API
3. Checks sender's current balance
4. Validates sender has enough currency
5. Deducts amount from sender's balance
6. Adds amount to receiver's balance
7. Logs transaction to Discord
8. Sends confirmation message

### Transaction Safety

- All balance operations are atomic
- If any step fails, the entire transaction fails
- No partial transfers occur
- Full error logging to Discord and StreamerBot logs

## Support

Need help? Join our Discord for support:
- Discord: [https://discord.gg/ngQXHUbnKg](https://discord.gg/ngQXHUbnKg)
- Report issues on the GitHub repository

## File Information

- **File**: `GiveCommand.cs`
- **Location**: `Currency/Core/Give-Coins/`
- **Created by**: HexEchoTV (CUB) | [GitHub](https://github.com/HexEchoTV/Streamerbot-Commands)
- **License**: Free for personal use only
- **Dependencies**: ConfigSetup.cs




