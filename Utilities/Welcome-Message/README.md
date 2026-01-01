# Welcome First-Time Chatter

## Description
Automatically welcomes first-time chatters to your stream! Greets each user **ONCE PER DAY** when they chat. Resets at midnight UTC, so viewers get a fresh welcome each calendar day. Perfect for building a welcoming community atmosphere without overwhelming chat with repeated welcomes.

## Trigger
**Event:** Twitch → Chat → Message

This trigger fires on every chat message, and the code handles checking if the user was already welcomed today.

## Features
- **Automatic welcome messages** - No commands needed
- **Once per calendar day** - Users welcomed once daily
- **Date-based tracking** - Resets at midnight UTC
- **Daily statistics** - Tracks unique chatters per day
- **Discord logging** - Logs all welcomes to Discord
- **Easy to test** - Just type in chat to verify
- **Lightweight** - Minimal resource usage
- **Non-intrusive** - Doesn't spam repeat messages

## How It Works

```
User's first message today (any time):
├─ Message trigger fires → Code checks date
└─ lastWelcomeDate ≠ today → Welcome ✅ + Save date

User's subsequent messages today:
├─ Message trigger fires → Code checks date
└─ lastWelcomeDate = today → Silent (already welcomed)

Next day (after midnight UTC):
├─ User types message → Code checks date
└─ lastWelcomeDate = yesterday → Welcome ✅ + Update date
```

The code checks the date on every message and only sends a welcome once per calendar day.

## Example Output

**User's First Message Today:**
```
👋 Welcome to the stream, UserName! 💜
```

**Subsequent Messages Today:**
```
(No message - user already welcomed today)
```

**Next Day:**
```
👋 Welcome to the stream, UserName! 💜
```

## Data Storage

### User Variables (per user)
- `last_welcome_date` - Tracks the date user was last welcomed (for statistics)

### Global Variables
- `first_timers_YYYY-MM-DD` - Count of unique chatters per day

## Configuration

### Welcome Message
Customize the welcome message (line 29):
```csharp
CPH.SendMessage($"👋 Welcome to the stream, {user}! 💜");
```

### Examples:
```csharp
// Simple
CPH.SendMessage($"Welcome, {user}!");

// Friendly
CPH.SendMessage($"Hey {user}! Great to see you! 🎉");

// Themed
CPH.SendMessage($"{user} has entered the arena! ⚔️");
```

## Dependencies
- None (standalone script)
- Discord webhook (optional, for logging)

## Installation

1. **Create a new C# action** in StreamerBot
2. **Copy the contents** of `WelcomeFirstTimer.cs`
3. **Set trigger:**
   - Event → Twitch → Chat → **Message**
4. **Enable the action**
5. **Test** by typing in chat - you should get welcomed!
6. **Test again** - no welcome (already welcomed today)

**IMPORTANT:** Use the "Message" trigger (fires on every message). The code handles the date checking.

## Current Setup: Once Per Calendar Day

- ✅ Trigger: Message (fires on every chat message)
- ✅ Behavior: Welcome once per calendar day (resets at midnight UTC)
- ✅ Best for: Multi-stream days, consistent daily welcomes
- ✅ Easy to test: Just type in chat!

### How Date Checking Works:
```csharp
// Get today's date
string today = DateTime.UtcNow.ToString("yyyy-MM-dd");

// Check if already welcomed today
string lastWelcomeDate = CPH.GetTwitchUserVarById<string>(userId, "last_welcome_date", true);

if (!string.IsNullOrEmpty(lastWelcomeDate) && lastWelcomeDate == today)
{
    return false;  // Already welcomed today, do nothing
}

// Welcome user and save today's date
CPH.SetTwitchUserVarById(userId, "last_welcome_date", today, true);
```

## Statistics & Tracking

The system tracks unique chatters per day:

**View in StreamerBot:**
- Go to Variables → Global Variables
- Look for `first_timers_2025-01-01` (current date)
- Value = number of unique chatters that day

**Example:**
```
first_timers_2025-01-15 → 47 unique chatters
first_timers_2025-01-16 → 52 unique chatters
```

## Customization Ideas

### Random Welcome Messages
```csharp
string[] welcomes = {
    $"👋 Welcome to the stream, {user}! 💜",
    $"Hey {user}! Great to see you! 🎉",
    $"Welcome aboard, {user}! 🚀",
    $"{user} has joined the adventure! 🗺️"
};

Random random = new Random();
CPH.SendMessage(welcomes[random.Next(welcomes.Length)]);
```

### Role-Based Welcomes
```csharp
bool isSubscriber = args.ContainsKey("isSubscriber") && (bool)args["isSubscriber"];
bool isModerator = args.ContainsKey("isModerator") && (bool)args["isModerator"];
bool isVip = args.ContainsKey("isVip") && (bool)args["isVip"];

if (isModerator)
{
    CPH.SendMessage($"👋 Welcome, Mod {user}! 🛡️");
}
else if (isSubscriber)
{
    CPH.SendMessage($"👋 Welcome, subscriber {user}! Thanks for the support! 💜");
}
else if (isVip)
{
    CPH.SendMessage($"👋 Welcome, VIP {user}! ⭐");
}
else
{
    CPH.SendMessage($"👋 Welcome to the stream, {user}! 💜");
}
```

### Emote-Only Welcomes
```csharp
// Simple emote-based welcome
CPH.SendMessage($"hexecho1Wave {user} hexecho1Love");
```

## Discord Logging

Logs are sent to Discord if webhook is configured in ConfigSetup.cs:

**Log Format:**
```
Command: First Timer Welcomed
User: UserName
Date: 2025-01-15
Session Count: 23
Status: SUCCESS
```

**Configure Discord:**
1. Run ConfigSetup.cs first
2. Set your Discord webhook URL
3. Enable logging with `discordLoggingEnabled = true`

## Use Cases

- **Welcome new viewers** - Make first-time chatters feel noticed
- **Build community** - Create friendly, welcoming atmosphere
- **Track participation** - See how many unique chatters per stream
- **Greet regulars** - Welcome returning viewers each session
- **Engagement metric** - Monitor daily chat activity

## Performance Notes

- **Lightweight** - Only runs on first message per user
- **Efficient** - Minimal variable reads/writes
- **Scalable** - Works with any community size
- **Non-blocking** - Doesn't interfere with other actions

## Troubleshooting

### Users Getting Multiple Welcomes
- **Check:** Only ONE "Message" trigger should be set
- **Check:** No duplicate actions running
- **Check:** Date checking logic is correct (see code)
- **Check:** User variables are being saved properly

### No Welcome Messages
- **Verify:** Trigger is set to "Message" (fires on every chat)
- **Verify:** Action is enabled in StreamerBot
- **Verify:** No syntax errors (check logs)
- **Test:** Type in chat yourself - should get welcomed immediately

### Welcome Messages on Every Message (Spam)
- **Problem:** Date checking code is broken or bypassed
- **Fix:** Check that `lastWelcomeDate` variable is being set correctly
- **Debug:** Add `CPH.LogInfo($"Last welcome: {lastWelcomeDate}, Today: {today}");` to see values

### Statistics Not Tracking
- **Check:** Global variables in StreamerBot
- **Look for:** `first_timers_YYYY-MM-DD` variables
- **Verify:** Counter increments when users chat

## Privacy Considerations

- Data stored locally in StreamerBot only
- No external databases or APIs
- Only tracks welcome date (no personal info)
- Discord logs are optional (can be disabled)

## Advanced Features

### Award Points for First Chat
Integrate with a points system:
```csharp
// After sending welcome message
CPH.SetTwitchUserVarById(userId, "points", currentPoints + 10, true);
CPH.SendMessage($"{user} earned 10 bonus points for chatting! 🎁");
```

### Track Consecutive Sessions
```csharp
// Increment session counter
int sessionCount = CPH.GetTwitchUserVarById<int>(userId, "session_count", true);
sessionCount++;
CPH.SetTwitchUserVarById(userId, "session_count", sessionCount, true);

if (sessionCount >= 10)
{
    CPH.SendMessage($"👋 Welcome back, {user}! You've been here {sessionCount} times! 🎉");
}
```

### Special Milestone Welcomes
```csharp
// Check if this is a milestone chatter for the day
if (todayCount == 50)
{
    CPH.SendMessage($"🎉 {user} is our 50th unique chatter today! Special welcome! 🎊");
}
```

## Testing Made Easy

### Test Right Now:
1. Type a message in your Twitch chat
2. You should see: `👋 Welcome to the stream, YourName! 💜`
3. Type another message
4. No welcome (already welcomed today)

### Verify It's Working:
1. Check **StreamerBot logs** - Look for "New chatter welcomed"
2. Check **Variables tab** - Find your user's `last_welcome_date`
3. Check **Discord** - If logging enabled, see welcome notification

### Test Tomorrow:
- Type in chat again
- Should get welcomed (new day!)
- Date variable updates to new date

---

**Author:** HexEchoTV (CUB)
**GitHub:** https://github.com/ThortonEllmers/Streamerbot-Commands
**License:** MIT License
