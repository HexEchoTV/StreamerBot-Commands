# StreamerBot Commands Collection

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
[![Discord](https://img.shields.io/badge/Discord-Join%20for%20Support-7289da)](https://discord.gg/ngQXHUbnKg)
[![GitHub](https://img.shields.io/badge/GitHub-HexEchoTV-181717?logo=github)](https://github.com/HexEchoTV/Streamerbot-Commands)
[![StreamerBot](https://img.shields.io/badge/StreamerBot-Compatible-9146FF)](https://streamer.bot/)

> A comprehensive collection of advanced C# commands and utilities for StreamerBot, featuring a complete currency system, interactive games, utility commands, and powerful integrations.

---

## 📋 Table of Contents

- [🎯 Overview](#-overview)
- [✨ Features](#-features)
- [📁 Project Structure](#-project-structure)
- [🚀 Quick Start](#-quick-start)
- [💰 Currency System](#-currency-system)
- [🎮 Available Commands](#-available-commands)
- [⚙️ Configuration](#️-configuration)
- [🎨 Discord Integration](#-discord-integration)
- [📚 Documentation](#-documentation)
- [🤝 Contributing](#-contributing)
- [💬 Support](#-support)
- [📄 License](#-license)
- [👏 Credits](#-credits)

---

## 🎯 Overview

**StreamerBot Commands** is a professionally-designed collection of C# actions for [StreamerBot](https://streamer.bot/), providing streamers with a complete economy system, 40+ interactive games, essential utilities, and powerful integrations. Built with modularity, extensibility, and ease of use in mind.

### What Makes This Collection Unique?

- ✅ **Complete Currency Economy** - Fully-featured currency system with daily claims, leaderboards, and transactions
- ✅ **40+ Interactive Games** - Gambling, PvP, quests, treasure hunts, and more
- ✅ **Advanced Discord Logging** - Comprehensive event tracking with color-coded embeds
- ✅ **Modular Architecture** - Use what you need, customize what you want
- ✅ **Professional Code Quality** - Well-documented, error-handled, and maintainable
- ✅ **Active Development** - Regular updates and community support
- ✅ **MIT Licensed** - Free to use, modify, and distribute

---

## ✨ Features

### 💰 Currency System
- **Daily Claims** - Users claim currency daily with streak bonuses
- **Balance Management** - Check balances, give/receive currency
- **Leaderboards** - Top earners tracking and display
- **Transaction Logging** - Complete audit trail via Discord
- **Configurable Economy** - Adjust rates, limits, and rewards

### 🎮 Games & Gambling (40+ Commands)
- **Classic Games** - Coinflip, Dice, Slots, Roulette, Blackjack
- **Advanced Games** - Crash, Plinko, Mines, Tower, Keno
- **PvP Games** - Duel, Battle, Rob, Heist
- **RPG Elements** - Quest, Dungeon, Boss, Explore, Hunt
- **Casual Games** - Bingo, Trivia, Magic 8-Ball, Scratch Cards
- **Risk Management** - Work, Scavenge, Forage, Fish, Mine, Dig

### 🛠️ Utility Commands
- **Stream Info** - Title, game, uptime tracking
- **Social Integration** - Discord links, multi-streaming
- **Clip Management** - Advanced clip creation with title modification
- **Quotes System** - Add, retrieve, and manage stream quotes
- **Watchtime Tracking** - Automated viewer time tracking
- **Followage Checker** - See how long users have been following
- **Welcome Messages** - Greet first-time daily chatters
- **Shoutouts** - OBS-integrated and chat-only variants

### 🎨 Advanced Features
- **Discord Webhook Logging** - Color-coded event tracking
- **Twitch API Integration** - Direct Helix API support
- **OBS Integration** - Source control and animations
- **Customizable Messages** - Easy-to-modify response templates
- **Error Handling** - Graceful failures with detailed logging
- **Cooldown Systems** - Built-in spam prevention

---

## 📁 Project Structure

```
StreamerBot-Commands/
│
├── Currency/                    # Complete currency system
│   ├── Core/                    # Essential currency commands
│   │   ├── Config-Setup/        # Configuration initialization
│   │   ├── Balance-Check/       # Check user balances
│   │   ├── Daily-Claim/         # Daily currency claims
│   │   ├── Give-Coins/          # Transfer currency
│   │   ├── Leaderboard/         # Top earners display
│   │   └── Example-Integration/ # Integration examples
│   │
│   └── Games/                   # 40+ interactive games
│       ├── Battle/               # PvP battle system
│       ├── Beg/                  # Beg for currency
│       ├── Bingo/                # Bingo game
│       ├── Blackjack/            # Casino blackjack
│       ├── Boss/                 # Boss battle minigame
│       ├── Bounty/               # Bounty hunting
│       ├── Coinflip/             # Heads or tails gambling
│       ├── Collect/              # Collection rewards
│       ├── Crime/                # Crime activities
│       ├── Crash/                # Crash gambling game
│       ├── Dice/                 # Dice rolling
│       ├── Dig/                  # Dig for treasures
│       ├── Duel/                 # PvP dueling
│       ├── Dungeon/              # Dungeon exploration
│       ├── Explore/              # Exploration rewards
│       ├── Fish/                 # Fishing minigame
│       ├── Flip/                 # Item flipping
│       ├── Forage/               # Foraging for items
│       ├── Gamble/               # All-in gambling
│       ├── Heist/                # Group heist system
│       ├── Highlow/              # High-low card game
│       ├── Hunt/                 # Hunting minigame
│       ├── Invest/               # Investment system
│       ├── Keno/                 # Keno lottery
│       ├── Ladder/               # Ladder climbing game
│       ├── Limbo/                # Limbo gambling
│       ├── Lottery/              # Lottery system
│       ├── Luck/                 # Luck-based rewards
│       ├── Magic/                # Magic 8-ball
│       ├── Match/                # Matching game
│       ├── Mine/                 # Mining minigame
│       ├── Mines/                # Minesweeper gambling
│       ├── Pet/                  # Virtual pet system
│       ├── Pickpocket/           # Pickpocketing
│       ├── Plinko/               # Plinko gambling
│       ├── Quest/                # Quest system
│       ├── Race/                 # Racing minigame
│       ├── Rob/                  # Rob other users
│       ├── Roulette/             # Casino roulette
│       ├── Scavenge/             # Scavenging
│       ├── Scratch/              # Scratch cards
│       ├── Search/               # Search for items
│       ├── Slots/                # Slot machine
│       ├── Spin/                 # Wheel spinning
│       ├── Streak/               # Streak bonuses
│       ├── Tower/                # Tower climbing game
│       ├── Treasure-Hunt/        # Treasure hunt events
│       ├── Trivia/               # Trivia questions
│       ├── Vault/                # Vault storage
│       ├── Wheel/                # Wheel of fortune
│       └── Work/                 # Work for currency
│
├── Utilities/                   # Essential utility commands
│   ├── Commands-List/           # List available commands
│   ├── Discord/                 # Discord link command
│   ├── Discord-Logging/         # Discord webhook logging system
│   ├── Followage/               # Check follow duration
│   ├── Multi-Twitch/            # Multi-stream links
│   ├── Quotes/                  # Quote management
│   ├── Shoutouts/               # Shoutout commands
│   │   ├── Shoutout-OBS Animations/    # Full OBS integration
│   │   └── Shoutout-Twitch Chat/       # Simple chat shoutouts
│   ├── Stream-Info/             # Title and game commands
│   ├── Uptime/                  # Stream uptime tracker
│   ├── Watchtime/               # Viewer watchtime system
│   └── Welcome-Message/         # First-time chatter greetings
│
├── Clip/                        # Clip management
│   └── Clip-Fetch/              # Advanced clip creation
│       ├── ClipCommand.cs       # Title-modifying clip creator
│       └── TwitchAPIDiagnosticCommand.cs  # API debugging
│
├── LICENSE                      # MIT License
├── COPYRIGHT.md                 # Copyright information
└── README.md                    # This file
```

---

## 🚀 Quick Start

### 1. Prerequisites

Before you begin, ensure you have:
- ✅ [StreamerBot](https://streamer.bot/) installed and running
- ✅ StreamerBot connected to your Twitch account
- ✅ Basic understanding of StreamerBot actions and triggers
- ✅ (Optional) Discord webhook for logging

### 2. Download

Clone or download this repository:
```bash
git clone https://github.com/HexEchoTV/Streamerbot-Commands.git
```

Or download as ZIP from GitHub.

### 3. Configure the Currency System

**IMPORTANT:** Start here before using any commands!

1. Navigate to `Currency/Core/Config-Setup/`
2. Open `ConfigSetup.cs` in a text editor
3. Customize your settings (lines 29-342):
   ```csharp
   // Currency Settings
   CPH.SetGlobalVar("config_currency_name", "Cub Coins", true);
   CPH.SetGlobalVar("config_currency_key", "cubcoins", true);

   // Daily Claim Settings
   CPH.SetGlobalVar("config_daily_reward", 100, true);
   CPH.SetGlobalVar("config_streak_bonus", 10, true);

   // Game Settings (customize min/max bets, win rates, etc.)
   // ...
   ```
4. Import `ConfigSetup.cs` into StreamerBot
5. **Run the action** to initialize all global variables

### 4. Import Commands

Import individual commands as needed:
1. Open StreamerBot
2. Go to **Actions** tab
3. Click **Import**
4. Select the `.cs` file you want to use
5. Create a chat trigger (e.g., `!balance`, `!daily`, `!coinflip`)
6. Enable the action

### 5. Test!

Type your command in Twitch chat to test:
```
!daily      → Claim daily currency
!balance    → Check your balance
!coinflip heads 50  → Gamble with coinflip
```

---

## 💰 Currency System

### Core Commands

| Command | File | Purpose |
|---------|------|---------|
| **ConfigSetup** | `Config-Setup/ConfigSetup.cs` | Initialize all settings (run this first!) |
| **!balance** | `Balance-Check/BalanceCommand.cs` | Check your or another user's balance |
| **!daily** | `Daily-Claim/DailyRedemption.cs` | Claim daily currency with streak bonuses |
| **!give** | `Give-Coins/GiveCommand.cs` | Give currency to another user |
| **!leaderboard** | `Leaderboard/LeaderboardCommand.cs` | View top currency holders |

### Economy Flow

```
User joins stream
    ↓
!daily (claim daily reward)
    ↓
Participate in games (!coinflip, !slots, !rob, etc.)
    ↓
Earn or lose currency
    ↓
!leaderboard (see rankings)
    ↓
!give (share with other viewers)
```

### Configuration Variables

All settings are in `ConfigSetup.cs`. Key variables include:

**Currency:**
- `config_currency_name` - Display name (e.g., "Cub Coins")
- `config_currency_key` - Database key (e.g., "cubcoins")

**Daily Claims:**
- `config_daily_reward` - Base daily amount
- `config_streak_bonus` - Bonus per consecutive day
- `config_max_streak` - Maximum streak multiplier

**Games:**
- Each game has configurable min/max bets, win rates, and rewards
- See `ConfigSetup.cs` lines 102-342 for all game settings

---

## 🎮 Available Commands

### Currency Core (5 commands)
- Balance Check, Daily Claim, Give Coins, Leaderboard, Example Integration

### Games & Gambling (40+ commands)
- Battle, Beg, Bingo, Blackjack, Boss, Bounty, Coinflip, Collect, Crime, Crash, Dice, Dig, Duel, Dungeon, Explore, Fish, Flip, Forage, Gamble, Heist, Highlow, Hunt, Invest, Keno, Ladder, Limbo, Lottery, Luck, Magic, Match, Mine, Mines, Pet, Pickpocket, Plinko, Quest, Race, Rob, Roulette, Scavenge, Scratch, Search, Slots, Spin, Streak, Tower, Treasure Hunt, Trivia, Vault, Wheel, Work

### Utilities (10+ commands)
- Commands List, Discord Link, Followage, Multi-Twitch, Quotes (Add/Get), Shoutout (2 variants), Stream Info (Title/Game), Uptime, Watchtime, Welcome Message

### Clip Management (1 command)
- Advanced clip creation with automatic stream title modification

**Total: 55+ Commands**

---

## ⚙️ Configuration

### Global Configuration (ConfigSetup.cs)

The `ConfigSetup.cs` file is the heart of the system. It initializes:
- Currency settings (name, key, starting balance)
- Daily claim rewards and streaks
- All game settings (bets, odds, rewards)
- Twitch API credentials (optional)
- Discord webhook URL (optional)

**To reconfigure:**
1. Edit `ConfigSetup.cs` with your desired values
2. Run the action in StreamerBot
3. All global variables update immediately
4. No need to edit individual command files

### Discord Logging Setup

1. Create a Discord webhook:
   - Go to Discord Server Settings → Integrations
   - Click "Create Webhook"
   - Copy the webhook URL

2. Add to ConfigSetup.cs (line 340):
   ```csharp
   CPH.SetGlobalVar("discordLogWebhook", "YOUR_WEBHOOK_URL", true);
   CPH.SetGlobalVar("discordLoggingEnabled", true, true);
   ```

3. Run ConfigSetup.cs

All commands will now log to Discord with color-coded embeds!

### Twitch API Integration (Advanced)

For commands that need Twitch API access (like ClipCommand):

1. Get credentials from [Twitch Token Generator](https://twitchtokengenerator.com)
   - Select required scopes (e.g., `channel:manage:broadcast` for clips)
   - Copy the **Access Token** and **Client ID**
2. Add to ConfigSetup.cs (lines 344-346):
   ```csharp
   CPH.SetGlobalVar("twitchApiClientId", "YOUR_CLIENT_ID", true);
   CPH.SetGlobalVar("twitchApiAccessToken", "YOUR_ACCESS_TOKEN", true);
   ```

3. Run ConfigSetup.cs

---

## 🎨 Discord Integration

### Logging System

Every command includes Discord webhook logging with:
- **Color-coded embeds** - Blue (info), Green (success), Orange (warning), Red (error), Purple (command)
- **Detailed context** - User, amounts, results, errors
- **Timestamps** - All logs include UTC timestamps
- **Searchable** - Easy to find specific events

### Example Log Format

```
🎮 COMMAND
Command: !coinflip
User: ViewerName
Details: Choice: heads | Bet: $50

✅ SUCCESS
Coinflip Win
User: ViewerName
Choice: heads
Result: heads
Bet: $50
Winnings: $100
New Balance: $350
```

### Toggle Logging

Enable/disable globally in ConfigSetup.cs:
```csharp
CPH.SetGlobalVar("discordLoggingEnabled", true, true);  // Enable
CPH.SetGlobalVar("discordLoggingEnabled", false, true); // Disable
```

---

## 📚 Documentation

Each command includes a comprehensive README.md:
- **Purpose** - What the command does
- **Installation** - Step-by-step setup
- **Usage** - Command syntax and examples
- **Configuration** - Customization options
- **Troubleshooting** - Common issues and solutions

### Documentation Locations

- **Currency System:** `Currency/README.md`
- **Each Game:** `Currency/Games/{GameName}/README.md`
- **Each Utility:** `Utilities/{UtilityName}/README.md`
- **Clip System:** `Clip/README.md`

---

## 🤝 Contributing

Contributions are welcome! Here's how you can help:

### Reporting Issues
- Use GitHub Issues to report bugs
- Include error messages and StreamerBot logs
- Describe steps to reproduce

### Suggesting Features
- Open a GitHub Issue with the "enhancement" label
- Describe the feature and use case
- Explain expected behavior

### Pull Requests
1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### Code Style
- Follow existing C# conventions
- Include comments for complex logic
- Add README.md for new commands
- Test thoroughly before submitting

---

## 💬 Support

### Get Help

- **Discord Community:** [Join our Discord](https://discord.gg/ngQXHUbnKg)
- **GitHub Issues:** [Report bugs or request features](https://github.com/HexEchoTV/Streamerbot-Commands/issues)
- **StreamerBot Support:** [Official StreamerBot Discord](https://discord.gg/streamerbot)

### FAQ

**Q: Do I need to use all commands?**
A: No! Use only what you need. The system is modular.

**Q: Can I modify the commands?**
A: Yes! All code is MIT licensed - modify freely.

**Q: Do commands work without ConfigSetup.cs?**
A: No, you must run ConfigSetup.cs first to initialize settings.

**Q: Can I use this commercially?**
A: Yes! MIT license allows commercial use.

**Q: How do I update to the latest version?**
A: Pull the latest changes from GitHub and re-import changed files.

---

## 📄 License

This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for full details.

### What This Means

✅ **You CAN:**
- Use commercially
- Modify the code
- Distribute and sublicense
- Use privately

❗ **You MUST:**
- Include the original license
- Include copyright notice

⚠️ **Limitations:**
- No warranty provided
- Author not liable for damages

See [COPYRIGHT.md](COPYRIGHT.md) for detailed copyright information.

---

## 👏 Credits

### Author
**HexEchoTV (CUB)**
- GitHub: [@HexEchoTV](https://github.com/HexEchoTV)
- Twitch: [HexEchoTV](https://twitch.tv/hexechotv)
- Discord: [Join Server](https://discord.gg/ngQXHUbnKg)

### Built With
- [StreamerBot](https://streamer.bot/) - The best Twitch bot platform
- [Twitch API](https://dev.twitch.tv/docs/api/) - Twitch Helix API
- C# (.NET Framework) - Programming language
- Discord Webhooks - Logging integration

### Acknowledgments
- StreamerBot community for support and inspiration
- Contributors who have helped improve this project
- Streamers using these commands in their channels

---

## 🌟 Star This Repository

If you find this project useful, please consider giving it a ⭐ on GitHub!

It helps others discover the project and motivates continued development.

---

## 📊 Statistics

- **55+ Commands** - Comprehensive command library
- **40+ Games** - Interactive gambling and minigames
- **100% Open Source** - Fully transparent codebase
- **MIT Licensed** - Free to use and modify
- **Active Development** - Regular updates and improvements
- **Community Supported** - Discord community for help

---

## 🔄 Recent Updates

See the commit history for detailed changes:
[View Commits](https://github.com/HexEchoTV/Streamerbot-Commands/commits/)

---

## 📬 Stay Connected

- 🌐 **GitHub:** [HexEchoTV/Streamerbot-Commands](https://github.com/HexEchoTV/Streamerbot-Commands)
- 💬 **Discord:** [Join Community](https://discord.gg/ngQXHUbnKg)
- 📺 **Twitch:** [Watch Live](https://twitch.tv/hexechotv)

---

<div align="center">

**Made with ❤️ by HexEchoTV (CUB)**

*Empowering streamers with professional-grade tools*

[⬆ Back to Top](#streamerbot-commands-collection)

</div>
