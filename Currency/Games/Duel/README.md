# Duel Command

## Description
Challenge another viewer to a duel! Both players bet the same amount, winner takes all based on random dice rolls.

## Command
`!duel @username [bet amount]`

## Features
- PvP betting system
- Both players must have sufficient funds
- Random roll determines winner (1-100 each)
- Winner takes entire pot (2× bet amount)
- Tie returns both bets
- Configurable min/max bet limits
- Discord logging for all duels

## Configuration
Set in `ConfigSetup.cs`:
- `config_duel_min_bet` - Minimum bet amount (default: 10)
- `config_duel_max_bet` - Maximum bet amount (default: 500)
- `config_currency_name` - Currency name for messages
- `config_currency_key` - User variable key for balance

## Dependencies
- `ConfigSetup.cs` (must be run first)

## How It Works
1. User runs `!duel @opponent [bet]`
2. Command validates both usernames and bet amount
3. Checks that challenger can't duel themselves
4. Validates challenger has enough currency
5. Validates opponent has enough currency
6. Deducts bet from both players
7. Both players roll 1-100
8. Higher roll wins entire pot (2× bet)
9. If tied, both players get their bet back
10. Winner announced with roll results
11. All duels logged to Discord (if enabled)

## Example Output
**Victory:**
```
⚔️ [Challenger] (73) vs [Opponent] (45) - [Challenger] WINS $200 Cub Coins!
```

**Defeat:**
```
⚔️ [Challenger] (34) vs [Opponent] (89) - [Opponent] WINS $200 Cub Coins!
```

**Tie:**
```
⚔️ [Challenger] (67) vs [Opponent] (67) - IT'S A TIE! Bets returned.
```

**Insufficient Funds (Opponent):**
```
[Challenger], [Opponent] only has $50 Cub Coins! They can't match your $100 bet.
```

**Self-Duel Attempt:**
```
[Username], you can't duel yourself!
```

**User Not Found:**
```
[Challenger], could not find user: [username]
```

**Invalid Usage:**
```
[Username], who do you want to duel? Usage: !duel @username 10-500
```

## Installation
1. Create a new C# action in StreamerBot
2. Copy the contents of `DuelCommand.cs`
3. Set the trigger to `!duel` command
4. Ensure `ConfigSetup.cs` has been run first
5. Test with `!duel @username [amount]` in chat

## Tips
- Truly fair 50/50 chance for both players
- Creates fun rivalry and interaction between viewers
- Both players must have the funds before duel starts
- Consider lower bet limits to prevent massive losses

---
Created by HexEchoTV (CUB) | [GitHub](https://github.com/HexEchoTV/Streamerbot-Commands)
