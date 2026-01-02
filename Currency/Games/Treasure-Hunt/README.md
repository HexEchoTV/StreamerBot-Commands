# Treasure Hunt System

## Description
A two-part treasure spawning system! Treasures randomly appear in chat, and viewers race to claim them.

## Commands
- **No command needed** - TreasureHuntSpawner.cs runs automatically on a timer
- `!loot` - Viewers use this to claim spawned treasure (TreasureHuntClaim.cs)

## Features
- Automated treasure spawning via timed action
- 50% chance to spawn each timer trigger
- 4 rarity tiers with different rewards
- First viewer to type !loot claims the treasure
- One treasure active at a time
- Discord logging for spawns and claims

## Files
1. **TreasureHuntSpawner.cs** - Spawns treasures automatically
2. **TreasureHuntClaim.cs** - Handles !loot command to claim treasure

## Treasure Rarities
| Rarity | Emoji | Chance | Reward |
|--------|-------|--------|--------|
| Legendary | 💎 | 3% | 500-1000 coins |
| Epic | 🔮 | 12% | 150-300 coins |
| Rare | ✨ | 25% | 50-150 coins |
| Common | 📦 | 60% | 10-50 coins |

## Configuration
Set in `ConfigSetup.cs`:
- `config_currency_name` - Currency name for messages
- `config_currency_key` - User variable key for balance

**Note**: Treasure rewards are hardcoded in TreasureHuntSpawner.cs

## Dependencies
- `ConfigSetup.cs` (must be run first)

## How It Works

### TreasureHuntSpawner.cs (Timed Action)
1. Timer triggers (recommended: every 5-15 minutes)
2. Check if treasure is already active (skip if yes)
3. 50% chance to spawn treasure
4. Determine rarity and reward amount
5. Store treasure data in global variables
6. Announce treasure in chat
7. Wait for viewer to claim with !loot

### TreasureHuntClaim.cs (!loot Command)
1. Viewer types `!loot` in chat
2. Check if treasure is active
3. First viewer to claim gets the reward
4. Add coins to winner's balance
5. Clear treasure data (mark as claimed)
6. Announce winner
7. Log claim to Discord

## Example Output

**Treasure Spawn:**
```
💎 LEGENDARY TREASURE appeared! Type !loot to claim $847 Cub Coins! 💎
```

**Successful Claim:**
```
🎉 [Username] claimed the LEGENDARY treasure and earned $847 Cub Coins! Balance: $1547
```

**Already Claimed:**
```
[Username], the treasure has already been claimed!
```

**No Active Treasure:**
```
[Username], there's no treasure to claim right now!
```

## Installation

### Part 1: Treasure Spawner (Timed Action)
1. Create a new C# action in StreamerBot
2. Copy the contents of `TreasureHuntSpawner.cs`
3. Create a **Timed Action** that runs this every 5-15 minutes
4. Enable the timer

### Part 2: Loot Claim (Chat Command)
1. Create another C# action in StreamerBot
2. Copy the contents of `TreasureHuntClaim.cs`
3. Set the trigger to `!loot` command
4. Ensure `ConfigSetup.cs` has been run first

### Testing
1. Manually run TreasureHuntSpawner.cs to spawn a treasure
2. Type `!loot` in chat to claim it
3. Verify reward is awarded and treasure is marked claimed

## Tips
- Adjust timer interval based on desired spawn frequency
- 50% spawn chance on timer means average 10-30 min between treasures
- First viewer to type !loot wins (creates chat interaction)
- Only one treasure can be active at a time
- Consider using a shorter timer (5 min) for more frequent spawns

---
Created by HexEchoTV (CUB) | [GitHub](https://github.com/HexEchoTV/Streamerbot-Commands)
