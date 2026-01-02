# Wordle Commands

Two versions of Wordle for your StreamerBot currency system!

## Overview

**WordleCommand.cs** - Simple chat-only Wordle game
**WordleHtmlCommand.cs** - Wordle with browser source overlay (shows current game state in OBS)

Both commands use the same gameplay mechanics but differ in output:
- Simple version: Chat messages only
- HTML version: Chat messages + live browser source updates

## Features

- 🎮 Classic Wordle gameplay with 5-letter words
- 💰 Costs currency to play (configurable)
- 🏆 Win reward for successful guesses (configurable)
- ⏱️ Configurable cooldown system
- 🎯 6 guesses by default (configurable)
- 🟩🟨⬛ Color-coded feedback (green=correct, yellow=wrong position, black=not in word)
- 📊 Discord logging integration
- 🖥️ Browser source support (HTML version only)

## Installation

### 1. Set Up Google Sheet with Word List

**Create Your Google Sheet:**
1. Go to [Google Sheets](https://sheets.google.com) and create a new spreadsheet
2. Name it something like "Wordle Words"
3. In Column A, paste the words from `wordle-words.txt` (one word per row)
   - You can find 70 words in the `wordle-words.txt` file included with this command
   - Or add your own 5-letter words!
4. Optional: Add a header in A1 called "WORD" (the script will skip it)

**Publish Your Sheet as CSV:**
1. In your Google Sheet, go to **File → Share → Publish to web**
2. Select the sheet tab you're using (usually "Sheet1")
3. Change "Web page" dropdown to **"Comma-separated values (.csv)"**
4. Click **"Publish"**
5. Copy the URL that appears (it should look like: `https://docs.google.com/spreadsheets/d/XXXX/export?format=csv&gid=0`)

**Add the URL to ConfigSetup.cs:**
1. Open `ConfigSetup.cs`
2. Find line 367 (the Wordle config section)
3. Replace `YOUR_SHEET_ID` with your full published CSV URL:
   ```csharp
   CPH.SetGlobalVar("config_wordle_sheet_url", "https://docs.google.com/spreadsheets/d/YOUR_ACTUAL_SHEET_ID/export?format=csv&gid=0", true);
   ```
4. Save and run ConfigSetup.cs to update the configuration

### 2. Run ConfigSetup.cs
Make sure you've run `ConfigSetup.cs` to initialize the Wordle configuration variables.

### 3. Import Commands into StreamerBot

**For Simple Wordle (Chat Only):**
1. Create a new C# action in StreamerBot
2. Copy the code from `WordleCommand.cs`
3. Create a command `!wordle` that triggers this action
4. Set the command to pass arguments: `user`, `userId`, and `input0` (for the guess)

**For Wordle HTML (With Browser Source):**
1. Create a new C# action in StreamerBot
2. Copy the code from `WordleHtmlCommand.cs`
3. Create a command `!wordlehtml` that triggers this action
4. Set the command to pass arguments: `user`, `userId`, and `input0` (for the guess)

### 4. Browser Source Setup (HTML Version Only)

1. Set the HTML file path in ConfigSetup.cs (default: `wordle.html` in StreamerBot directory)
2. In OBS, add a new Browser Source:
   - URL: `file:///PATH/TO/YOUR/wordle.html` (replace with your actual path)
   - Width: 800
   - Height: 600
   - Check "Refresh browser when scene becomes active"
3. Position and resize in your OBS scene as desired

## Configuration

All settings are in `ConfigSetup.cs`:

```csharp
// Wordle Command
CPH.SetGlobalVar("config_wordle_cost", 50, true);              // Cost to start a game
CPH.SetGlobalVar("config_wordle_win_reward", 150, true);       // Reward for winning
CPH.SetGlobalVar("config_wordle_max_guesses", 6, true);        // Maximum guesses allowed
CPH.SetGlobalVar("config_wordle_cooldown_seconds", 60, true);  // Cooldown between games
CPH.SetGlobalVar("config_wordle_html_path", "wordle.html", true); // HTML file path
CPH.SetGlobalVar("config_wordle_sheet_url", "https://docs.google.com/spreadsheets/d/YOUR_SHEET_ID/export?format=csv&gid=0", true); // Google Sheet URL
```

You can change these values in StreamerBot's Global Variables UI or edit ConfigSetup.cs and run it again.

## Usage

### Starting a New Game

**Simple version:**
```
!wordle
```

**HTML version:**
```
!wordlehtml
```

This will:
- Deduct the game cost from your balance
- Start a new Wordle game
- Tell you how many guesses you have

### Making a Guess

```
!wordle APPLE
!wordlehtml CRANE
```

The command will respond with color-coded feedback:
- 🟩 = Letter is correct and in the right position
- 🟨 = Letter is in the word but wrong position
- ⬛ = Letter is not in the word

### Winning

Guess the correct word within the maximum guesses to win the reward!

### Losing

If you run out of guesses, the game ends and reveals the correct word.

## Examples

```
User: !wordle
Bot: 🎮 User started Wordle! Cost: $50 Cub Coins. Guess a 5-letter word! Guesses left: 6

User: !wordle CRANE
Bot: 🎯 🟩⬛🟨⬛⬛ | User has 5 guesses left!

User: !wordle CREAM
Bot: 🎉 🟩🟩🟩🟩🟩 | User WON Wordle in 2/6 guesses! +$150 Cub Coins! Balance: $200
```

## Browser Source Display (HTML Version)

The HTML version creates a beautiful overlay showing:
- **Title:** "HexEchoTV Wordle Challenge"
- **Word Display:** Shows "? ? ? ? ?" during play, reveals word when game ends
- **Guess Grid:** Visual grid showing all guesses with color-coded letters
- **Game Status:** Remaining guesses, win/loss message
- **Styling:** Dark theme with purple accents, glowing effects

## Word Pool

The commands fetch words from a Google Sheet that you configure. This allows you to:
- Easily add/remove words without editing code
- Share word lists between multiple commands
- Update words in real-time
- Use different word lists for different streams

The included `wordle-words.txt` file contains 70 carefully selected 5-letter words to get you started!

**Word Requirements:**
- Must be exactly 5 letters
- Only alphabetic characters (A-Z)
- One word per row in your Google Sheet

**Adding More Words:**
Simply add new 5-letter words to your Google Sheet! The commands will automatically pick up the changes the next time someone starts a game.

## Technical Details

### Game State Storage

Each user's game state is stored in user variables:
- **Simple version:** Uses `wordle_current_word`, `wordle_guess_count`, `wordle_guesses`
- **HTML version:** Uses `wordle_html_current_word`, `wordle_html_guess_count`, `wordle_html_guesses`

This means users can play both versions simultaneously without conflict!

### Cooldown System

Cooldowns are tracked per user and only start after a game completes (win or lose).

### Discord Logging

Both commands log to Discord when enabled:
- Game started (with the target word - spoilers!)
- Game won (with guess count and reward)
- Game lost (with the target word)
- Cooldown triggers
- Insufficient funds

## Customization Tips

### Change the Cost/Reward
Edit the values in ConfigSetup.cs or StreamerBot's Global Variables UI.

### Adjust Max Guesses
Want to make it easier/harder? Change `config_wordle_max_guesses` (default is 6).

### Change Cooldown
Adjust `config_wordle_cooldown_seconds` to control how often users can play.

### Customize HTML Appearance
Edit the `<style>` section in the `UpdateHtmlFile()` method in `WordleHtmlCommand.cs` to change:
- Colors
- Fonts
- Sizes
- Animations
- Layout

### Add More Words
Expand the `WORD_POOL` array in either command file with more 5-letter words.

## Troubleshooting

**"Wordle error: No words available"**
- Google Sheet URL is not configured or is invalid
- Check that you've published your sheet as CSV
- Verify the URL in ConfigSetup.cs is correct
- Make sure your sheet contains valid 5-letter words
- Check StreamerBot logs for specific error messages

**"Missing user/userId argument"**
- Make sure your command is configured to pass `user` and `userId` arguments from Twitch

**"Wordle cooldown!"**
- User must wait for the cooldown period to expire before starting a new game

**"You only have $X!"**
- User doesn't have enough currency to play

**Google Sheet not loading**
- Ensure the sheet is published to web as CSV (not as web page)
- Check that the sheet is publicly accessible
- Verify there are no typos in the URL
- Test the URL in a browser - it should download a CSV file

**Browser source not updating (HTML version)**
- Check that the file path in `config_wordle_html_path` is correct
- Verify the file is being created (check StreamerBot logs)
- Try refreshing the browser source in OBS
- Make sure OBS has read permissions for the HTML file location

**HTML file path issues**
- Use absolute paths if having trouble: `C:/StreamerBot/wordle.html`
- Avoid spaces in file paths or use quotes
- Check Windows file permissions

## Credits

Created by **HexEchoTV (CUB)**
- GitHub: https://github.com/HexEchoTV/Streamerbot-Commands
- License: MIT

## Dependencies

- ConfigSetup.cs (must be run first to initialize config variables)
- StreamerBot currency system
- Write access to file system (HTML version only)
