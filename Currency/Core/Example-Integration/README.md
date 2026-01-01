# Example Integration

A comprehensive guide showing how to integrate the currency system into your own custom commands.

## Overview

This example file demonstrates all common patterns used throughout the currency system. Use these code snippets as templates when creating your own custom commands that interact with the currency system.

## What You'll Learn

### Core Operations
- **Check Balance** - Get a user's current currency balance
- **Give Coins** - Award currency to users (earning commands)
- **Take Coins** - Deduct currency from users (shops, gambling)
- **Validate Funds** - Check if users have enough currency

### Advanced Features
- **Cooldown System** - Prevent command spam with time restrictions
- **Counter Tracking** - Track usage counts, streaks, and statistics
- **Discord Logging** - Integrated logging for all operations
- **Error Handling** - Proper exception handling and user feedback

## Code Examples

### Example 1: Check User Balance
```csharp
string currencyName = CPH.GetGlobalVar<string>("config_currency_name", true);
string currencyKey = CPH.GetGlobalVar<string>("config_currency_key", true);
string userId = args["userId"].ToString();

int balance = CPH.GetTwitchUserVarById<int>(userId, currencyKey, true);
CPH.SendMessage($"{userName} has ${balance} {currencyName}");
```

### Example 2: Give Coins (Earning Commands)
```csharp
int coinsToGive = 50;
int currentBalance = CPH.GetTwitchUserVarById<int>(userId, currencyKey, true);
int newBalance = currentBalance + coinsToGive;
CPH.SetTwitchUserVarById(userId, currencyKey, newBalance, true);

LogSuccess("Coins Earned", $"{userName} earned ${coinsToGive}");
```

### Example 3: Take Coins (Shops/Gambling)
```csharp
int cost = 100;
int currentBalance = CPH.GetTwitchUserVarById<int>(userId, currencyKey, true);

if (currentBalance < cost)
{
    CPH.SendMessage($"{userName}, you need ${cost} but only have ${currentBalance}!");
    return false;
}

int newBalance = currentBalance - cost;
CPH.SetTwitchUserVarById(userId, currencyKey, newBalance, true);
```

### Example 4: Cooldown System
```csharp
string cooldownKey = "command_lastuse";
string lastUseStr = CPH.GetTwitchUserVarById<string>(userId, cooldownKey, true);
DateTime now = DateTime.UtcNow;
int cooldownMinutes = 30;

if (!string.IsNullOrEmpty(lastUseStr))
{
    DateTime lastUse = DateTime.Parse(lastUseStr).ToUniversalTime();
    if ((now - lastUse).TotalMinutes < cooldownMinutes)
    {
        // Still on cooldown
        return false;
    }
}

// Update last use
CPH.SetTwitchUserVarById(userId, cooldownKey, now.ToString("o"), true);
```

### Example 5: Usage Counter/Streaks
```csharp
string counterKey = "command_usecount";
int useCount = CPH.GetTwitchUserVarById<int>(userId, counterKey, true);
useCount++;
CPH.SetTwitchUserVarById(userId, counterKey, useCount, true);
```

## Key Patterns

### Always Load Config First
```csharp
string currencyName = CPH.GetGlobalVar<string>("config_currency_name", true);
string currencyKey = CPH.GetGlobalVar<string>("config_currency_key", true);
```
This ensures your command works even if the currency name changes.

### Use User Variables (Not Global Variables)
```csharp
// ✅ CORRECT - Uses user variables
CPH.GetTwitchUserVarById<int>(userId, currencyKey, true);

// ❌ WRONG - Old system with global variables
CPH.GetGlobalVar<int>($"cubcoins_{userId}", false);
```

### Include Discord Logging
```csharp
LogCommand("!mycommand", userName, "Additional details");
LogSuccess("Action Completed", $"{userName} did something");
LogError("Error Occurred", $"Error: {ex.Message}");
```

### Wrap in Try-Catch
```csharp
public bool Execute()
{
    try
    {
        // Your command logic here
        return true;
    }
    catch (Exception ex)
    {
        LogError("Command Error", $"Error: {ex.Message}");
        return false;
    }
}
```

## Common Use Cases

### Creating an Earning Command
1. Load config (currency name, key)
2. Get user info
3. Check cooldown (if applicable)
4. Generate random reward amount
5. Add coins to user balance
6. Update cooldown timestamp
7. Send success message
8. Log to Discord

### Creating a Gambling Command
1. Load config
2. Get user info and balance
3. Validate bet amount (min/max from config)
4. Check if user has enough coins
5. Deduct bet amount
6. Run game logic
7. Award winnings (if won)
8. Send result message
9. Log to Discord

### Creating a Shop Command
1. Load config
2. Get user info and balance
3. Define item cost
4. Check if user can afford item
5. Deduct cost from balance
6. Grant item/effect
7. Send purchase confirmation
8. Log to Discord

## Discord Logging Methods

All methods are included in the example file:

- `LogInfo()` - Blue - General information
- `LogSuccess()` - Green - Successful operations
- `LogWarning()` - Orange - Warnings (insufficient funds, etc.)
- `LogError()` - Red - Errors and exceptions
- `LogCommand()` - Purple - Command executions

## Variable Naming Conventions

- **Config vars**: `config_<setting_name>` (e.g., `config_currency_name`)
- **Currency balance**: Use the value from `config_currency_key`
- **Cooldowns**: `<command>_lastuse` (e.g., `work_lastuse`)
- **Counters**: `<command>_<counter>` (e.g., `daily_count`)

## Files

| File | Description |
|------|-------------|
| `ExampleIntegration.cs` | Full example with all patterns |

## Dependencies

- **ConfigSetup.cs** - Must be run first to initialize global variables

## Related Documentation

- `/Currency/Core/Config-Setup/` - Configuration system
- `/Currency/Core/Balance-Check/` - Balance checking
- `/Currency/Core/Daily-Claim/` - Cooldown system example
- `/Currency/Work/` - Earning command examples
- `/Currency/Games/` - Gambling command examples

## Author

Created by **HexEchoTV (CUB)**
https://github.com/HexEchoTV/Streamerbot-Commands

## License

Licensed under the MIT License. See LICENSE file in the project root for full license information.
