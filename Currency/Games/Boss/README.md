# Boss Command

## Description
An epic cooldown-based challenge where users attempt to defeat a boss for high rewards with a 30% success rate and hours-long cooldown.

## Command
`!boss`

## Features
- High-risk, high-reward boss battle
- 30% success rate (default)
- Massive reward range (300-600 coins default)
- Long cooldown period (prevents spam)
- No bet required (free attempt)
- Hours-based cooldown tracking
- Discord logging for all attempts and victories

## Configuration
Set in `ConfigSetup.cs`:
- `config_boss_min_bet` - Not used (legacy parameter)
- `config_boss_max_bet` - Not used (legacy parameter)
- `config_boss_success_rate` - Success chance percentage (default: 30)
- `config_boss_win_mult` - Not used (legacy parameter)
- `config_boss_cooldown_hours` - Cooldown in hours (default: 24)
- `config_currency_name` - Currency name for messages
- `config_currency_key` - User variable key for balance

**Note**: Boss uses hardcoded reward range of 300-600 coins instead of config multipliers.

## Dependencies
- `ConfigSetup.cs` (must be run first)

## How It Works
1. User runs `!boss` command
2. Command checks cooldown (hours since last attempt)
3. If on cooldown, displays time remaining
4. If available, rolls random chance (30% default success)
5. On success: Awards 300-600 coins randomly
6. On failure: No coins earned but cooldown still applies
7. Cooldown timer set for configured hours
8. Result announced in chat
9. All attempts logged to Discord (if enabled)

## Example Output
**Victory:**
```
👑 [Username] defeated the BOSS and earned $487 Cub Coins! Balance: $1287
```

**Defeat:**
```
💀 [Username] was defeated by the boss! Come back stronger!
```

**Cooldown Active:**
```
[Username], boss respawns in 18h 45m!
```

## Installation
1. Create a new C# action in StreamerBot
2. Copy the contents of `BossCommand.cs`
3. Set the trigger to `!boss` command
4. Ensure `ConfigSetup.cs` has been run first
5. Test with `!boss` in chat

## Tips
- Boss is meant to be a rare, high-value event
- Default 24-hour cooldown prevents spam
- Consider setting cooldown to 12-48 hours depending on your economy
- Great for building anticipation in stream chat

---
Created by HexEchoTV (CUB) | [GitHub](https://github.com/HexEchoTV/Streamerbot-Commands)
