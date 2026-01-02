# Crime Command

## Description
Commit various crimes for rewards... but watch out! Get caught and pay a fine. Features multiple crime types and risk/reward gameplay.

## Command
`!crime`

## Features
- 8 different crime types with thematic messages
- 50% success rate (default)
- Success: Earn 100-400 coins
- Failure: Pay 50-200 coin fine
- Minutes-based cooldown
- Randomized crime selection
- Discord logging for all crime attempts

## Configuration
Set in `ConfigSetup.cs`:
- `config_crime_min_reward` - Minimum success reward (default: 100)
- `config_crime_max_reward` - Maximum success reward (default: 400)
- `config_crime_success_rate` - Success chance percentage (default: 50)
- `config_crime_fail_penalty` - Fine amount on failure (default: 100)
- `config_crime_cooldown_minutes` - Cooldown in minutes (default: 15)
- `config_currency_name` - Currency name for messages
- `config_currency_key` - User variable key for balance

## Dependencies
- `ConfigSetup.cs` (must be run first)

## How It Works
1. User runs `!crime` command
2. Command checks cooldown (minutes since last crime)
3. If on cooldown, displays time remaining
4. Random crime type selected from list
5. 50% chance to succeed or get caught
6. Success: Award 100-400 coins
7. Failure: Deduct 50-200 coin fine (balance can't go below 0)
8. Cooldown timer set
9. Result announced with crime type
10. All attempts logged to Discord (if enabled)

## Crime Types
- Robbed a convenience store
- Hacked an ATM
- Pickpocketed a tourist
- Scammed a viewer
- Sold fake merch
- Stole a car
- Counterfeited channel points
- Ran a Ponzi scheme

## Example Output
**Success:**
```
😈 [Username] robbed a convenience store and earned $283 Cub Coins! Balance: $883
```

**Failure (Caught):**
```
🚔 [Username] tried to rob a convenience store but got caught! Paid $127 Cub Coins fine. Balance: $273
```

**Cooldown Active:**
```
[Username], lay low! Try again in 9 minutes.
```

## Installation
1. Create a new C# action in StreamerBot
2. Copy the contents of `CrimeCommand.cs`
3. Set the trigger to `!crime` command
4. Ensure `ConfigSetup.cs` has been run first
5. Test with `!crime` in chat

## Tips
- Risk/reward balance: can earn big or lose coins
- 50% success rate keeps it fair
- Fines prevent balance from going negative
- Add your own custom crimes by editing the array in code

---
Created by HexEchoTV (CUB) | [GitHub](https://github.com/HexEchoTV/Streamerbot-Commands)
