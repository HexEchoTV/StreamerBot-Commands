# Currency Games

[![Discord](https://img.shields.io/badge/Discord-Join%20for%20Help-7289da)](https://discord.gg/ngQXHUbnKg)

## Overview

The **Currency Games** folder contains 60+ mini-games and activities that allow viewers to earn, spend, and gamble currency in your Twitch chat.

## Game Categories

### 🎰 Earning Games (Risk-Free)
Earn currency without losing anything:

| Game | Command | Cooldown | Reward Range |
|------|---------|----------|--------------|
| **Work** | `!work` | 30 min | $50-150 |
| **Battle** | `!battle` | 60 min | $35-180 |
| **Magic** | `!magic` | 45 min | $40-120 |
| **Fish** | `!fish` | 20 min | $30-100 |
| **Hunt** | `!hunt` | 25 min | $40-130 |
| **Mine** | `!mine` | 30 min | $45-140 |
| **Dig** | `!dig` | 25 min | $35-110 |
| **Search** | `!search` | 20 min | $30-95 |
| **Scavenge** | `!scavenge` | 30 min | $15-150 |
| **Collect** | `!collect` | 60 min | $50-200 |
| **Forage** | `!forage` | 25 min | $30-100 |
| **Beg** | `!beg` | 15 min | $10-50 |
| **Luck** | `!luck` | 45 min | $0-500 |

### 🎲 Gambling Games (Risk/Reward)
Bet currency to win (or lose):

| Game | Command | Bet Type | Win Rate |
|------|---------|----------|----------|
| **Slots** | `!slots <bet>` | Fixed bet | Variable |
| **Coinflip** | `!coinflip <bet>` | Fixed bet | 50% |
| **Dice** | `!dice <bet>` | Fixed bet | Variable |
| **Roulette** | `!roulette <bet> <color>` | Fixed bet | Variable |
| **Blackjack** | `!blackjack <bet>` | Fixed bet | Skill-based |
| **Wheel** | `!wheel <bet>` | Fixed bet | Variable |
| **Gamble** | `!gamble <bet>` | Fixed bet | 50% |
| **Flip** | `!flip <bet>` | Fixed bet | 50% |
| **Plinko** | `!plinko <bet>` | Fixed bet | Variable |
| **Crash** | `!crash <bet>` | Fixed bet | Timing-based |
| **Limbo** | `!limbo <bet>` | Fixed bet | Variable |
| **Highlow** | `!highlow <bet>` | Fixed bet | 50% |

### 🎮 Special Games
Unique mechanics:

| Game | Command | Type | Description |
|------|---------|------|-------------|
| **Lottery** | `!lottery` | Jackpot | Buy tickets, win big |
| **Trivia** | `!trivia` | Knowledge | Answer questions |
| **Treasure Hunt** | `!treasure` | Timed | Find hidden treasure |
| **Heist** | `!heist` | Team | Group robbery |
| **Duel** | `!duel @user <bet>` | PvP | 1v1 combat |
| **Boss** | `!boss` | Raid | Fight epic bosses |
| **Dungeon** | `!dungeon` | Adventure | Explore dungeons |
| **Quest** | `!quest` | Tasks | Complete quests |
| **Tower** | `!tower` | Climbing | Climb the tower |
| **Race** | `!race <bet>` | PvP | Race other players |

### ⚔️ PvP Games
Player vs Player:

| Game | Command | Type | Risk |
|------|---------|------|------|
| **Duel** | `!duel @user <bet>` | Combat | Bet currency |
| **Rob** | `!rob @user` | Theft | Steal from others |
| **Race** | `!race <bet>` | Competition | Race to win |
| **Bounty** | `!bounty @user` | Hunt | Hunt other players |

### 🃏 Card/Board Games

| Game | Command | Style | Skill Level |
|------|---------|-------|-------------|
| **Blackjack** | `!blackjack <bet>` | Cards | High |
| **Keno** | `!keno <bet>` | Numbers | Low |
| **Bingo** | `!bingo` | Numbers | Low |
| **Scratch** | `!scratch` | Scratch cards | Low |
| **Match** | `!match <bet>` | Matching | Medium |

## Installation

### Prerequisites

**⚠️ MUST INSTALL FIRST:**
1. **ConfigSetup.cs** from `Currency/Core/Config-Setup/`
   - Run this before ANY game commands
   - Initializes all global variables
   - Sets currency name and rewards

### Install Individual Games

Each game folder contains:
- **CommandName.cs** - The command file
- **README.md** - Installation and usage instructions

**Steps to install a game:**
1. Go to the game's folder (e.g., `Currency/Games/Work/`)
2. Read the README.md file
3. Import the .cs file into StreamerBot
4. Add chat command trigger
5. Test in your chat

### Recommended Games to Start With

**For new channels:**
1. **Work** - Reliable earning
2. **Daily** - Keep viewers coming back
3. **Battle** - Fun combat mechanics
4. **Slots** - Classic gambling
5. **Lottery** - Big jackpot excitement

**For established channels:**
- Add all earning games
- Add 5-10 gambling games
- Add special games (Heist, Trivia, Boss)
- Add PvP games for competition

## Usage Examples

### Earning Games
```
!work
→ 💼 Username worked as a Streamer and earned $125 Cub Coins! Balance: $625

!battle
→ ⚔️ Username defeated a 👹 Goblin and earned $100 Cub Coins! Balance: $725

!fish
→ 🎣 Username caught a 🐟 Bass worth $85 Cub Coins! Balance: $810
```

### Gambling Games
```
!slots 100
→ 🎰 [🍒][🍒][🍒] JACKPOT! Username won $500! Balance: $1,310

!coinflip 50
→ 🪙 Heads! Username won $50! Balance: $1,360

!roulette 100 red
→ 🔴 Red! Username won $200! Balance: $1,560
```

### Special Games
```
!lottery
→ 🎟️ Username bought a lottery ticket for $10! Jackpot: $5,000

!trivia
→ ❓ What is the capital of France? (30 seconds to answer)
→ Paris
→ ✅ Correct! Username won $100!

!heist
→ 🏦 HEIST STARTING! Type !join to participate! (30 seconds)
```

### PvP Games
```
!duel @viewer 100
→ ⚔️ Username challenged @viewer to a duel for $100!
→ ⚔️ Username won the duel! (+$100)

!rob @viewer
→ 💰 Username attempted to rob @viewer...
→ ✅ Success! Stole $75!
```

## Configuration

### Global Settings (ConfigSetup.cs)

Each game has configurable settings:

```csharp
// Work Command Settings (lines 200-203)
CPH.SetGlobalVar("config_work_min_reward", 50, true);
CPH.SetGlobalVar("config_work_max_reward", 150, true);
CPH.SetGlobalVar("config_work_cooldown_minutes", 30, true);

// Battle Command Settings (lines 230-234)
CPH.SetGlobalVar("config_battle_min_reward", 35, true);
CPH.SetGlobalVar("config_battle_max_reward", 180, true);
CPH.SetGlobalVar("config_battle_cooldown_minutes", 60, true);
CPH.SetGlobalVar("config_battle_win_chance", 50, true);

// Slots Command Settings (lines 280-284)
CPH.SetGlobalVar("config_slots_min_bet", 10, true);
CPH.SetGlobalVar("config_slots_max_bet", 500, true);
CPH.SetGlobalVar("config_slots_cooldown_seconds", 30, true);
```

### Customizing Games

1. Open `ConfigSetup.cs`
2. Find the game's settings section
3. Modify values:
   - **Cooldowns** - Balance frequency vs spam
   - **Rewards** - Adjust economy balance
   - **Win rates** - Make games easier/harder
   - **Bet limits** - Control gambling extremes
4. **Run ConfigSetup.cs again** to save changes

## Dependencies

### Required for ALL Games

1. **ConfigSetup.cs** (MUST RUN FIRST)
   - Location: `Currency/Core/Config-Setup/ConfigSetup.cs`
   - Sets up all global variables
   - Configures currency system

### Optional Dependencies

2. **Discord Logging** (Built-in)
   - Logs all game activity to Discord
   - Configure webhook in ConfigSetup.cs
   - Toggle with `!logging on/off`

3. **Balance Command** (Recommended)
   - Location: `Currency/Core/Balance-Check/BalanceCommand.cs`
   - Let users check balance before gambling

## Economy Balance Tips

### Earning vs Gambling

**Good balance:**
- Earning games: 50-150 per use
- Gambling minimum bets: 10-50
- Gambling maximum bets: 500-1000
- Daily rewards: 100-500

**Adjust based on:**
- Viewer activity level
- How many games you have
- Stream frequency
- Channel size

### Cooldown Recommendations

| Game Type | Cooldown | Reason |
|-----------|----------|--------|
| **High reward earning** | 60 min | Prevent farming |
| **Medium earning** | 30 min | Balanced |
| **Low earning** | 15 min | Frequent use OK |
| **Gambling** | 10-30 sec | Per-command limit |
| **Special events** | 24 hr | Daily activities |

### Reward Scaling

**Early channel (0-100 viewers):**
- Work: 50-100
- Battles: 30-120
- Gambling: 10-500 bets

**Growing channel (100-500 viewers):**
- Work: 75-150
- Battles: 50-180
- Gambling: 25-1000 bets

**Large channel (500+ viewers):**
- Work: 100-200
- Battles: 75-250
- Gambling: 50-2000 bets

## Troubleshooting

### Common Issues

**Game commands not working:**
- ✅ Have you run ConfigSetup.cs?
- ✅ Is the chat trigger enabled?
- ✅ Check StreamerBot logs for errors

**Rewards too low/high:**
- Edit ConfigSetup.cs
- Adjust min/max reward values
- Run ConfigSetup.cs again

**Users spamming commands:**
- Increase cooldowns in ConfigSetup.cs
- Use StreamerBot queue delays
- Add moderator restrictions

**Gambling exploits:**
- Set reasonable min/max bets
- Monitor Discord logs
- Adjust win rates if needed

### Game-Specific Issues

Check individual game README files for:
- Game-specific troubleshooting
- Configuration examples
- Known issues and fixes

## Discord Logging

All games log to Discord:

### What Gets Logged

**Command Usage:**
```
🎮 COMMAND
Command: !work
User: Username
```

**Wins/Rewards:**
```
✅ SUCCESS
Work Reward
User: Username
Job: Streamer
Earned: $125
Balance: $625
```

**Losses:**
```
⚠️ WARNING
Gambling Loss
User: Username
Game: Slots
Lost: $100
Balance: $425
```

**Errors:**
```
❌ ERROR
Command Error
Game: Blackjack
Error: [Error message]
```

## Best Practices

### Game Selection

1. **Start with 5-10 games** - Don't overwhelm viewers
2. **Mix earning + gambling** - Balance the economy
3. **Add gradually** - One game per stream
4. **Watch for favorites** - Keep what viewers use

### Viewer Engagement

1. **Promote new games** - Announce when adding
2. **Host tournaments** - Use PvP games for events
3. **Adjust rewards** - Based on usage data
4. **Create goals** - Leaderboard competition

### Security

1. **Monitor logs** - Watch for exploits
2. **Test thoroughly** - Before going live
3. **Backup variables** - Regular exports
4. **Update cooldowns** - If spam occurs

## Support

Need help with currency games?

- **Discord**: [https://discord.gg/ngQXHUbnKg](https://discord.gg/ngQXHUbnKg)
- **Individual Game READMEs**: Check each game's folder
- **Main Currency README**: See parent Currency/ folder

## Credits

**Created by**: HexEchoTV (CUB) | [GitHub](https://github.com/HexEchoTV/Streamerbot-Commands)
**License**: Free for personal use only




