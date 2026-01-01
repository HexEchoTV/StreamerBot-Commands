# Currency System

[![Discord](https://img.shields.io/badge/Discord-Join%20for%20Help-7289da)](https://discord.gg/ngQXHUbnKg)

## Overview

The **Currency System** is a complete economy system for your Twitch channel. Viewers can earn, spend, and gamble virtual currency through various commands and games.

## Folder Structure

```
Currency/
├── Core/              # Essential currency commands
│   ├── Config-Setup/  # Configuration (MUST RUN FIRST)
│   ├── Balance-Check/ # Check currency balance
│   ├── Daily-Claim/   # Daily currency reward
│   ├── Give-Coins/    # Transfer currency to others
│   └── Leaderboard/   # Top currency holders
│
└── Games/             # Currency earning/gambling games
    ├── Work/          # Earn currency by working
    ├── Daily/         # Daily rewards
    ├── Luck/          # Test your luck
    ├── Battle/        # Fight monsters for rewards
    ├── Magic/         # Cast spells for currency
    ├── Scavenge/      # Scavenge for items
    ├── Fish/          # Go fishing
    ├── Hunt/          # Hunt for game
    ├── Mine/          # Mine for resources
    ├── Dig/           # Dig for treasure
    ├── Search/        # Search for currency
    ├── Collect/       # Collect items
    ├── Beg/           # Beg for currency
    ├── Lottery/       # Buy lottery tickets
    ├── Trivia/        # Answer trivia questions
    ├── Treasure-Hunt/ # Find hidden treasure
    ├── Slots/         # Slot machine gambling
    ├── Coinflip/      # Flip a coin
    ├── Dice/          # Roll dice
    ├── Roulette/      # Roulette wheel
    ├── Blackjack/     # Blackjack card game
    ├── Wheel/         # Spin the wheel
    ├── Gamble/        # General gambling
    ├── Flip/          # Coin flip betting
    ├── Plinko/        # Plinko board game
    ├── Crash/         # Crash gambling game
    ├── Limbo/         # Limbo gambling
    ├── Highlow/       # High/low card game
    ├── Keno/          # Keno number game
    ├── Bingo/         # Bingo game
    ├── Scratch/       # Scratch cards
    ├── Spin/          # Spin to win
    ├── Match/         # Matching game
    ├── Tower/         # Tower climbing game
    ├── Mines/         # Minesweeper-style game
    ├── Ladder/        # Ladder climbing
    ├── Race/          # Racing game
    ├── Duel/          # Duel other players
    ├── Rob/           # Rob other players
    ├── Heist/         # Team heist game
    ├── Crime/         # Commit crimes
    ├── Bounty/        # Bounty hunting
    ├── Pickpocket/    # Pickpocket for currency
    ├── Pet/           # Virtual pet system
    ├── Streak/        # Streak rewards
    ├── Invest/        # Investment system
    ├── Boss/          # Boss battles
    ├── Vault/         # Vault storage
    ├── Dungeon/       # Dungeon exploration
    ├── Quest/         # Quest system
    └── Explore/       # Exploration game
```

## Quick Start

### Step 1: Run ConfigSetup.cs (REQUIRED)

**This is the most important step!** All currency commands depend on ConfigSetup.cs.

1. Open StreamerBot
2. Go to **Actions** tab
3. Import `Currency/Core/Config-Setup/ConfigSetup.cs`
4. **Edit the file** to customize your settings:
   - Currency name (line 24)
   - Currency key (line 25)
   - Discord webhook (line 356)
   - Game settings (cooldowns, rewards, etc.)
5. **Run the action** - You should see a success message in chat
6. **Verify** - Check Global Variables to ensure all config values are set

### Step 2: Install Core Commands

Install these essential commands in this order:

1. **BalanceCommand** - Let viewers check their balance
2. **DailyRedemption** - Daily currency rewards
3. **GiveCommand** - Transfer currency between users
4. **LeaderboardCommand** - Display top earners

### Step 3: Install Game Commands (Optional)

Install any games you want:
- **Earning Games**: Work, Battle, Magic, Fish, Hunt, Mine, Dig, Search
- **Gambling Games**: Slots, Coinflip, Dice, Roulette, Blackjack, Wheel
- **Special Games**: Lottery, Trivia, Treasure Hunt, Heist, Duel

## Core Commands

### Required Commands

| Command | Purpose | Priority |
|---------|---------|----------|
| **ConfigSetup** | Initialize all settings | ⚠️ MUST RUN FIRST |
| **BalanceCommand** | Check currency balance | Essential |
| **DailyRedemption** | Daily rewards | Highly Recommended |

### Optional Core Commands

| Command | Purpose | Usage |
|---------|---------|-------|
| **GiveCommand** | Transfer currency to others | `!give @user 100` |
| **LeaderboardCommand** | View top currency holders | `!leaderboard` |

## Game Categories

### 🎰 Earning Games
Games that give currency without risk:
- **Work** - Work for guaranteed currency
- **Battle** - Fight monsters for rewards
- **Magic** - Cast spells for currency
- **Fish** - Go fishing for rewards
- **Hunt** - Hunt animals for currency
- **Mine** - Mine resources
- **Dig** - Dig for treasure
- **Search** - Search for currency
- **Scavenge** - Scavenge for items
- **Collect** - Collect daily items

### 🎲 Gambling Games
Games where you can win or lose currency:
- **Slots** - Classic slot machine
- **Coinflip** - 50/50 coin flip
- **Dice** - Roll dice and bet
- **Roulette** - Roulette wheel
- **Blackjack** - Card game
- **Wheel** - Spin the wheel
- **Plinko** - Plinko board
- **Crash** - Crash betting
- **Limbo** - Limbo game
- **Highlow** - High/low cards

### 🎮 Special Games
Unique game mechanics:
- **Lottery** - Buy tickets, win jackpot
- **Trivia** - Answer questions for rewards
- **Treasure Hunt** - Find hidden treasure
- **Heist** - Team-based robbery
- **Duel** - Challenge other viewers
- **Boss** - Epic boss battles
- **Dungeon** - Explore dungeons
- **Quest** - Complete quests

### ⚔️ PvP Games
Player vs Player:
- **Duel** - 1v1 combat
- **Rob** - Steal from others
- **Race** - Race other players
- **Bounty** - Hunt other players

## Dependencies

### Global Dependency

**ALL currency commands require ConfigSetup.cs to be run first.**

### Discord Logging (Optional)

All commands include built-in Discord logging that works if you've configured:
- `discordLogWebhook` in ConfigSetup.cs (line 356)
- `discordLoggingEnabled` set to `true` (line 360)

### Command-Specific Dependencies

Some commands depend on others:
- **Leaderboard** requires users to have earned currency first
- **Give** requires users to have currency to transfer
- **Gambling games** require users to have currency to bet

## Configuration

### Currency Settings

Edit ConfigSetup.cs to customize:

```csharp
// Currency Name (displayed to users)
CPH.SetGlobalVar("config_currency_name", "Cub Coins", true);

// Currency Key (internal storage name)
CPH.SetGlobalVar("config_currency_key", "cubcoins", true);
```

### Game Settings

Each game has configurable settings in ConfigSetup.cs:
- **Cooldowns** - How long between uses
- **Rewards** - How much currency to give
- **Min/Max bets** - Gambling limits
- **Win rates** - Probability of winning
- **Multipliers** - Reward multipliers

Example for Work command (lines 200-203):
```csharp
CPH.SetGlobalVar("config_work_min_reward", 50, true);
CPH.SetGlobalVar("config_work_max_reward", 150, true);
CPH.SetGlobalVar("config_work_cooldown_minutes", 30, true);
```

## Discord Logging

All currency commands log the following events:

### Event Types
- **COMMAND** 🎮 - Every command execution
- **SUCCESS** ✅ - Successful transactions
- **WARNING** ⚠️ - Cooldowns, insufficient funds
- **ERROR** ❌ - Errors and exceptions

### Toggle Logging

```
!logging on    # Enable logging
!logging off   # Disable logging
!logging status # Check status
```

## Troubleshooting

### Common Issues

**Issue: Commands not working**
- ✅ Have you run ConfigSetup.cs?
- ✅ Are Global Variables set in StreamerBot?
- ✅ Is the chat command trigger enabled?

**Issue: Wrong currency name showing**
- Edit ConfigSetup.cs line 24
- Run ConfigSetup.cs again

**Issue: Users not earning currency**
- Check that commands are using the same currency key
- Verify Global Variables are correct
- Check StreamerBot logs for errors

**Issue: Gambling games not working**
- Ensure users have enough currency to bet
- Check min/max bet settings in ConfigSetup.cs
- Verify cooldowns aren't too restrictive

## Best Practices

### Recommended Setup

1. **Start Small** - Install core commands + 3-5 games
2. **Test Everything** - Test each command before going live
3. **Balance Economy** - Adjust rewards/cooldowns based on viewer activity
4. **Monitor Logs** - Use Discord logging to track usage
5. **Gradual Expansion** - Add more games over time

### Economy Balance Tips

- **Earning vs Gambling** - Make earning slightly easier than gambling
- **Cooldowns** - Prevent spam with reasonable cooldowns (10-60 minutes)
- **Rewards** - Start with moderate rewards, adjust based on activity
- **Leaderboard** - Creates competition and engagement

### Security

- **Moderator Commands** - Give and Leaderboard should be restricted
- **Rate Limiting** - Use cooldowns to prevent abuse
- **Logging** - Enable Discord logging to catch issues
- **Backups** - Export Global Variables regularly

## Support

Need help setting up the currency system?

- **Discord**: [https://discord.gg/ngQXHUbnKg](https://discord.gg/ngQXHUbnKg)
- **Documentation**: Check individual command README files
- **Issues**: Report bugs on GitHub

## Credits

**Created by**: HexEchoTV (CUB) | [GitHub](https://github.com/HexEchoTV/Streamerbot-Commands)
**License**: Free for personal use only




