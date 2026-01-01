# Rob Command

## Description
Rob other viewers in PvP (player vs player) action! High-risk criminal activity with big rewards or serious consequences. Attempt to steal a percentage of another user's currency, but risk getting caught and paying a fine.

## Command
`!rob @username`

## Features
- **PvP robbery** - Rob other viewers for their currency
- **Random steal amount** - Steals random percentage of victim's balance (configurable range)
- **Success chance** - Configurable probability of successful robbery
- **Failure penalty** - Fixed $50 fine when caught
- **Cooldown system** - Minutes-based cooldown prevents spam
- **Minimum requirements** - Robber needs 50+ coins, victim needs 10+ coins
- **Self-protection** - Can't rob yourself
- **Discord logging** - Tracks all robbery attempts

## Configuration
Set in `ConfigSetup.cs`:
- `config_rob_success_rate` - Success chance percentage (e.g., 40 = 40% chance)
- `config_rob_min_percent` - Minimum percentage stolen on success (e.g., 5 = 5%)
- `config_rob_max_percent` - Maximum percentage stolen on success (e.g., 15 = 15%)
- `config_rob_cooldown_minutes` - Cooldown in minutes between robbery attempts
- `config_currency_name` - Currency name for messages
- `config_currency_key` - User variable key for balance storage

## Dependencies
- `ConfigSetup.cs` (must be run first to initialize all config variables)

## How It Works
1. User runs `!rob @username` in chat
2. System validates target user exists via Twitch API
3. Checks that robber and victim aren't the same person
4. Verifies cooldown has expired (minutes since last robbery)
5. Confirms robber has at least $50 (for potential fine)
6. Confirms victim has at least $10 (minimum to rob)
7. Updates robber's cooldown timer
8. Rolls random number to determine success
9. **On Success:** Steals random percentage (min-max range) of victim's balance
10. **On Failure:** Robber pays $50 fine
11. Updates both users' balances
12. Sends result message to chat
13. Logs the robbery attempt to Discord (if enabled)

## Example Output

**Successful Robbery:**
```
RobberName successfully robbed $150 Cub Coins from VictimName! Balance: $850
```

**Caught - Failed Robbery:**
```
RobberName got caught trying to rob VictimName and paid a $50 Cub Coins fine! Balance: $200
```

**On Cooldown:**
```
RobberName, you need to lay low! Try again in 15 minutes.
```

**Target User Not Found:**
```
RobberName, could not find user: unknownuser
```

**Target Too Poor:**
```
RobberName, VictimName is too poor to rob!
```

**Robber Insufficient Funds:**
```
RobberName, you need at least 50 Cub Coins to attempt a robbery!
```

**Missing Target:**
```
RobberName, who are you trying to rob? Usage: !rob @username
```

**Self-Rob Attempt:**
```
RobberName, you can't rob yourself!
```

## Installation
1. Create a new C# action in StreamerBot
2. Copy the contents of `RobCommand.cs`
3. Set the trigger to `!rob` command
4. Ensure `ConfigSetup.cs` has been run first to initialize all configuration variables
5. Test with `!rob @username` in chat (username required)

## User Variables (Per User)
- `rob_cooldown` - Timestamp of last robbery attempt (ISO 8601 format)

## Mechanics & Balance

### Risk vs Reward
- **Success:** Steal 5-15% of victim's balance (random, configurable)
- **Failure:** Pay fixed $50 fine
- **Cooldown:** Minutes-based (configurable, prevents spam)

### Requirements
- **Robber minimum:** $50 (must have enough for potential fine)
- **Victim minimum:** $10 (must have something worth stealing)

### Success Rate Calculation
Example with default 40% success rate:
- Roll random number 1-100
- If roll ≤ 40: Success (steal coins)
- If roll > 40: Failure (pay $50 fine)

### Steal Amount Calculation
If successful with victim having $1000 and 5-15% range:
- Roll random percentage between 5-15% (e.g., 12%)
- Steal amount = $1000 × 12% = $120
- Minimum steal: $1 (if percentage calculation results in 0)

## Configuration Tips
- **Success Rate:** 40% creates balanced risk/reward
- **Min Percent:** 5% prevents huge losses from single robbery
- **Max Percent:** 15% makes robberies worthwhile
- **Cooldown:** 15-30 minutes prevents spam while allowing gameplay
- **Balance:** Lower success rate = higher risk, adjust penalty accordingly

## Discord Logging

All robbery attempts are logged to Discord (if webhook configured in ConfigSetup.cs):

**Logged Events:**
- Command executions (who robbed whom)
- Successful robberies (amount stolen, percentages, balances)
- Failed robberies (fines paid, balances)
- Warnings (invalid targets, insufficient balances, cooldowns)
- Errors (exceptions, parsing errors)

**Example Log - Successful Robbery:**
```
Command: !rob
User: RobberName
Target: VictimName

Rob Success
User: RobberName | Target: VictimName | Stolen: $120 | Percent: 12% | Robber balance: $370 | Target balance: $880
Status: SUCCESS
```

**Example Log - Failed Robbery:**
```
Rob Failed
User: RobberName | Target: VictimName | Fine: $50 | Balance: $200
Status: INFO
```

## Troubleshooting

**"You need to lay low!" message:**
- Cooldown is still active
- Check `rob_cooldown` user variable for last robbery time
- Wait for configured cooldown minutes to elapse

**"Could not find user" message:**
- Target username doesn't exist on Twitch
- Check spelling (@ symbol is optional)
- Verify user exists via Twitch search

**"You need at least 50 [currency]" message:**
- Robber doesn't have enough for potential fine
- Earn more currency before attempting robbery
- Minimum balance requirement protects against going negative

**"[Target] is too poor to rob!" message:**
- Target has less than $10
- This prevents robbing users with no meaningful balance
- Find a different target with more currency

**Cooldown not resetting:**
- Check `rob_cooldown` user variable format (should be ISO 8601)
- Verify DateTime parsing is working correctly
- Check StreamerBot logs for parsing errors

**Balance not updating:**
- Verify `config_currency_key` matches your currency system
- Check that user variables are being saved (persistent = true)
- Review Discord logs for balance update confirmations

## Use Cases

- **PvP Economy Interaction** - Viewers can compete for currency
- **Risk/Reward Gameplay** - Creates exciting chat moments
- **Community Engagement** - Encourages participation and strategy
- **Balance Redistribution** - Helps redistribute wealth from rich to poor (sometimes!)
- **Stream Entertainment** - Provides content and viewer interaction

---
Created by HexEchoTV (CUB) | [GitHub](https://github.com/HexEchoTV/Streamerbot-Commands)
