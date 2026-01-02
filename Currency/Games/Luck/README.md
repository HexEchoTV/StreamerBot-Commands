# Luck Command

## Description
Test your luck with this simple gambling game! A chance-based earning command with configurable odds.

## Command
`!luck [bet amount]`

## Features
- Simple luck-based gambling
- Configurable success rate and multipliers
- Win or lose based on random chance
- Configurable min/max bet limits
- Discord logging for all luck attempts
- No cooldown (play as often as you have currency)

## Configuration
Set in `ConfigSetup.cs`:
- `config_luck_min_bet` - Minimum bet amount
- `config_luck_max_bet` - Maximum bet amount
- `config_luck_success_rate` - Success chance percentage
- `config_luck_win_mult` - Win multiplier
- `config_currency_name` - Currency name for messages
- `config_currency_key` - User variable key for balance

## Dependencies
- `ConfigSetup.cs` (must be run first)

## How It Works
1. User runs `!luck [bet]` with amount between min and max
2. Command validates bet amount and checks user balance
3. Bet is deducted from user balance
4. Roll random chance against success rate
5. If successful, win at configured multiplier
6. If failed, lose bet
7. Result announced with winnings or loss
8. All attempts logged to Discord (if enabled)

## Example Output
**Win:**
```
🍀 [Username] got lucky and won $[amount] coins! Balance: $[balance]
```

**Loss:**
```
❌ [Username]'s luck ran out and lost $[amount] coins! Balance: $[balance]
```

**Invalid Usage:**
```
[Username], usage: !luck [min]-[max]
```

## Installation
1. Create a new C# action in StreamerBot
2. Copy the contents of `LuckCommand.cs`
3. Set the trigger to `!luck` command
4. Ensure `ConfigSetup.cs` has been run first
5. Test with `!luck [amount]` in chat

## Tips
- Adjust success rate to balance risk vs reward
- Higher multipliers should have lower success rates
- No cooldown allows rapid-fire gambling

---
Created by HexEchoTV (CUB) | [GitHub](https://github.com/HexEchoTV/Streamerbot-Commands)
