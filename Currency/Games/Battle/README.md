# Battle Command

## Description
Fight various monsters for coin rewards! Each monster has different reward amounts. 50% win chance with cooldown system.

## Command
`!battle`

## Features
- 6 different monster types with varying rewards
- 50% win chance
- Cooldown system (default: 40 minutes)
- No coins lost on failure
- Discord logging integration
- Monster variety: Dragon, Goblin, Ghost, Zombie, Bat, Spider

## Monster Rewards
- 🐉 Dragon: 180 coins
- 👹 Goblin: 100 coins
- 👻 Ghost: 90 coins
- 🧟 Zombie: 70 coins
- 🦇 Bat: 50 coins
- 🕷️ Spider: 35 coins

## Configuration
Set in `ConfigSetup.cs`:
- `config_battle_min_bet`: Not currently used
- `config_battle_max_bet`: Not currently used
- `config_battle_win_mult`: Not currently used
- `config_battle_cooldown_minutes`: Cooldown duration (default: 40 minutes)

## Dependencies
- `ConfigSetup.cs` (must be run first)

## How It Works
1. User types `!battle`
2. System checks cooldown
3. 50% chance to win or lose
4. If win: Random monster selected, corresponding reward given
5. If lose: No coins lost, but cooldown still applies
6. Result logged to Discord

## Example Output
**Victory:**
```
⚔️ UserName defeated a 🐉 Dragon and earned $180 Cub Coins! Balance: $480
```

**Defeat:**
```
💀 UserName was defeated in battle! Try again later!
```

**Cooldown:**
```
UserName, rest before next battle! Wait 25m 15s.
```

## Installation
1. Create a new C# action in StreamerBot
2. Copy the contents of `BattleCommand.cs`
3. Set the trigger to `!battle` command
4. Ensure `ConfigSetup.cs` has been run first

---
Created by HexEchoTV (CUB) | [GitHub](https://github.com/HexEchoTV/Streamerbot-Commands)
