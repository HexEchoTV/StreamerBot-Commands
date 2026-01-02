# Scratch Command

## Description
Scratch lottery cards for instant prizes! Buy a scratcher and reveal your winnings.

## Command
`!scratch [bet amount]`

## Features
- Instant lottery mechanics
- Multiple prize tiers
- Scratch card simulation
- Configurable min/max bet limits
- Various win conditions
- Discord logging for all scratchers
- Quick, addictive gameplay

## Configuration
Set in `ConfigSetup.cs`:
- `config_scratch_min_bet` - Minimum bet amount
- `config_scratch_max_bet` - Maximum bet amount
- `config_scratch_success_rate` - Win chance percentage
- `config_scratch_win_mult` - Win multiplier
- `config_currency_name` - Currency name for messages
- `config_currency_key` - User variable key for balance

## Dependencies
- `ConfigSetup.cs` (must be run first)

## How It Works
1. User runs `!scratch [bet]` with amount between min and max
2. Command validates bet amount and checks user balance
3. Bet is deducted (cost of scratch card)
4. Reveal scratch card results
5. Check for winning combinations
6. Award prize if won
7. Announce result with symbols/numbers
8. All scratchers logged to Discord (if enabled)

## Example Output
**Win:**
```
🎫 [Symbol][Symbol][Symbol] - [Username] won $[amount] coins! Balance: $[balance]
```

**Loss:**
```
🎫 [Symbol][Symbol][Symbol] - [Username] didn't win. Balance: $[balance]
```

**Jackpot:**
```
🎫 💰💰💰 JACKPOT! [Username] won $[amount] coins! Balance: $[balance]
```

**Invalid Usage:**
```
[Username], usage: !scratch [min]-[max]
```

## Installation
1. Create a new C# action in StreamerBot
2. Copy the contents of `ScratchCommand.cs`
3. Set the trigger to `!scratch` command
4. Ensure `ConfigSetup.cs` has been run first
5. Test with `!scratch [amount]` in chat

## Tips
- Quick gameplay with instant results
- Multiple prize tiers add excitement
- Adjust win rates for balance
- No cooldown allows rapid play

---
Created by HexEchoTV (CUB) | [GitHub](https://github.com/HexEchoTV/Streamerbot-Commands)
