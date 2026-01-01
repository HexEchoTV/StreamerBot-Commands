# Shoutout Command (OBS Animations)

## Description
A comprehensive shoutout command with full OBS integration! Features profile picture display, automatic clip playback, smooth fade transitions, and chat messages. Perfect for giving professional-looking shoutouts during raids or to highlight other streamers with visual flair.

## Command
`!shoutout @username` or `!so @username`

## Features
- **Profile picture display** with fade transitions
- **Automatic clip retrieval** and playback from Twitch
- **Smooth fade animations** between elements
- **Featured clips only** (avoids age-gated content)
- **Random clip selection** from last 90 days
- **Fallback behavior** when no clips available
- **OBS source control** integration
- **Customizable display times**
- **Discord logging** integration
- **Professional presentation**

## OBS Setup Requirements

### 1. Create OBS Group
Create a group named **"SO"** in your OBS scene (or customize `groupName` in code)

### 2. Add Sources to Group
Inside the "SO" group, create these sources:
- **GDI+ Text:** `ShoutoutUsername` - Displays the username
- **GDI+ Text:** `ShoutoutMessage` - Displays "Take a look at!"
- **Browser Source:** `ShoutoutProfilePic` - Shows profile picture (300x300px recommended)
- **Browser Source:** `ShoutoutClip` - Shows video clip (resize as desired)

### 3. Enable Transitions (IMPORTANT)
For smooth animations, enable transitions on each source:
1. Right-click each source → **Properties**
2. **Show Transition:** Fade (300ms)
3. **Hide Transition:** Fade (300ms)

### 4. Scene Configuration
Update the scene name in the code (line 19):
```csharp
string sceneName = "Bots";  // Change to your actual scene name
```

## Configuration

### Display Timing (Lines 20-23)
```csharp
string sceneName = "Bots";             // Your OBS scene name
string groupName = "SO";               // Your group source name
int profileDisplayTime = 2000;         // Profile picture: 2 seconds
int videoPlayTime = 15000;             // Video playback: 15 seconds
```

### Clip Settings
- Fetches clips from **last 90 days** for freshness
- **Featured clips only** (prevents age-gated content)
- **Random selection** for variety
- Auto-embeds with autoplay and sound

## Dependencies

- **OBS Studio** with Browser Sources enabled
- **StreamerBot** with OBS connection configured
- **Twitch API** access (built into StreamerBot)
- (Optional) **Discord webhook** for logging

## How It Works

### With Clips Available:
1. User types `!so @username` in chat
2. System fetches user's profile picture and clips from Twitch
3. Shows OBS group with username and message
4. Displays profile picture for 2 seconds
5. Fades to video clip playback for 15 seconds
6. Fades back to profile picture for 2 seconds
7. Sends chat message mentioning the clip
8. Hides all OBS elements with fade transitions
9. Logs the shoutout to Discord

**Total duration:** ~19 seconds

### Without Clips:
1. Same steps 1-3
2. Shows profile picture for the full animation duration
3. Sends chat message without clip mention
4. Hides all elements
5. Logs to Discord

**Total duration:** ~17 seconds

## Example Output

### Chat Messages

**With Clip:**
```
Take a looksee at FriendName at https://twitch.tv/friendname - Here's one of their clips!
```

**Without Clip:**
```
Take a looksee at FriendName at https://twitch.tv/friendname - They're a good creature!
```

**User Not Found:**
```
Missing user, we could not find: unknownuser
```

## Animation Sequence

```
┌─────────────────────────────────────┐
│ 1. [2s]  Profile Picture (fade in)  │
│ 2. [15s] Video Clip (fade in/out)   │
│ 3. [2s]  Profile Picture (fade in)  │
│ 4. Chat Message Sent                │
│ 5. Hide All (fade out)              │
└─────────────────────────────────────┘

Total: ~19 seconds with clip
Total: ~17 seconds without clip
```

## Installation

1. **Create OBS sources** as described above
2. **Enable fade transitions** on all sources (300ms)
3. **Create a new C# action** in StreamerBot
4. **Copy the contents** of `ShoutOutCommand.cs`
5. **Set command triggers** for `!shoutout` and `!so`
6. **Update scene/group names** in code (lines 19-20)
7. **Adjust display times** if desired (lines 22-23)
8. **Test** by typing `!so @username` in chat

## Customization

### Change Display Times
```csharp
int profileDisplayTime = 3000;     // 3 seconds instead of 2
int videoPlayTime = 20000;         // 20 seconds instead of 15
```

### Change Chat Messages
```csharp
// With clip (line 137)
CPH.SendMessage($"Take a looksee at {targetUser} at https://twitch.tv/{targetUser} - Here's one of their clips!");

// Without clip (line 144)
CPH.SendMessage($"Take a looksee at {targetUser} at https://twitch.tv/{targetUser} - They're a good creature!");
```

### Change OBS Text
```csharp
string message = $"Take a look at!";  // Line 74 - Customize the display message
```

### Change Scene/Group Names
```csharp
string sceneName = "YourSceneName";   // Line 19
string groupName = "YourGroupName";    // Line 20
```

## Use Cases

- **Professional raid shoutouts** with visual flair
- **Featured streamer highlights** during streams
- **Community showcases** with clips and profiles
- **Collaborative promotion** with other creators
- **Special events** requiring polished presentation

## Differences from Chat Only Version

### OBS Animations Version (This One):
- Requires full OBS setup
- ~19 second animation sequence
- Shows profile pictures and clips
- Professional visual presentation
- More complex configuration
- Higher resource usage

### Chat Only Version:
- ✅ No OBS setup required
- ✅ Instant execution (< 1 second)
- ✅ Chat message only
- ✅ Simple setup
- ✅ Minimal resources

## Discord Logging

Integrates with the Discord logging system to track:

- **Command executions** - Who ran the shoutout and target
- **Successful shoutouts** - Confirmation with clip status
- **Warnings** - User not found errors
- **Errors** - Exceptions or clip fetch failures

### Example Log:
```
Command: !shoutout
User: StreamerName
Target: FriendName
Details: Clip found and played
Status: SUCCESS
```

## Troubleshooting

**Black screen during clip:**
- Ensure browser source has "Shutdown source when not visible" disabled
- Check that autoplay is enabled in browser source settings
- Verify the clip URL is valid and accessible

**No transitions between elements:**
- Enable show/hide transitions on all sources (300ms fade)
- Right-click source → Properties → Show/Hide Transition

**Sources not updating:**
- Verify scene name matches code exactly (case-sensitive)
- Check source names match exactly: `ShoutoutUsername`, `ShoutoutMessage`, etc.
- Ensure sources are inside the group named in code

**Clip not playing:**
- Browser source may need refresh (right-click → Refresh)
- Check that clips exist for the target user (last 90 days)
- Verify featured clips are available (unfeatured clips are filtered out)

**Profile picture not loading:**
- Check browser source size (300x300px recommended)
- Disable caching in browser source properties
- Verify internet connection for image loading

## Performance Notes

- Clips fetched from **last 90 days** for freshness and relevance
- **Featured clips only** to avoid age restrictions and content issues
- Browser sources need ~500ms to load (timing accounts for this)
- Smooth fade transitions prevent jarring visual changes
- Profile picture caching may improve load times

## Advanced Customization

### Add Custom Styling to Text Sources
- Custom fonts and sizes
- Colors, gradients, and effects
- Drop shadows and outlines
- Background boxes and borders

### Modify Clip Selection
```csharp
// Change clip date range (line 83)
List<ClipData> clips = CPH.GetClipsForUser(targetUser, new TimeSpan(30, 0, 0, 0), true);  // Last 30 days instead of 90
```

### Add Sound Effects
Use StreamerBot's sound playback features to add audio cues during transitions.

## Author

Created by **HexEchoTV (CUB)**
https://github.com/HexEchoTV/Streamerbot-Commands

## License

Licensed under the MIT License. See LICENSE file in the project root for full license information.
