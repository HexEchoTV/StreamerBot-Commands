# Scavenge Command

## Description
Scavenge for useful items and resources! A cooldown-based earning command with item discovery mechanics.

## Command
`!scavenge`

## Features
- Search for various items
- Multiple item types with different values
- Guaranteed rewards (no risk)
- Minutes-based cooldown
- Rarity-based reward system
- Discord logging for all scavenging
- Survival/exploration theme

## Configuration
Set in `ConfigSetup.cs`:
- `config_scavenge_cooldown_minutes` - Cooldown in minutes
- `config_currency_name` - Currency name for messages
- `config_currency_key` - User variable key for balance

**Note**: Scavenge rewards are typically hardcoded with different item rarities.

## Dependencies
- `ConfigSetup.cs` (must be run first)

## How It Works
1. User runs `!scavenge` command
2. Command checks cooldown (minutes since last scavenge)
3. If on cooldown, displays time remaining
4. Roll random chance for item rarity
5. Award corresponding reward based on item found
6. Add coins to user balance
7. Set cooldown timer
8. Announce discovery with item and reward
9. All scavenging logged to Discord (if enabled)

## Example Items
- Medical kit (rare) - High value
- Canned food (uncommon) - Medium value
- Water bottle (common) - Low value
- Scrap metal (common) - Low value

## Example Output
**Rare Find:**
```
🔍 [Username] scavenged MEDICAL KIT and earned $[amount] Cub Coins! Balance: $[balance]
```

**Common Find:**
```
🔍 [Username] scavenged SCRAP METAL and earned $[amount] Cub Coins! Balance: $[balance]
```

**Cooldown Active:**
```
[Username], scavenge area is picked clean! Wait [time].
```

## Installation
1. Create a new C# action in StreamerBot
2. Copy the contents of `ScavengeCommand.cs`
3. Set the trigger to `!scavenge` command
4. Ensure `ConfigSetup.cs` has been run first
5. Test with `!scavenge` in chat

## Tips
- No risk, always earns coins
- Survival theme great for apocalypse/survival streams
- Rarity system creates excitement
- Short cooldown allows frequent play

---
Created by HexEchoTV (CUB) | [GitHub](https://github.com/HexEchoTV/Streamerbot-Commands)
