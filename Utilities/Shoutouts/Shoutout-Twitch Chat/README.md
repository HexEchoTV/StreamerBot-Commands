# Shoutout Command (Chat Only)

## Description
A simple, lightweight shoutout command that works entirely in Twitch chat. No OBS setup required! Perfect for quick shoutouts during raids, to highlight other streamers, or for community showcases without the complexity of visual overlays.

## Command
`!shoutout @username` or `!so @username`

## Features
- **Chat-only** - No OBS setup required
- **Instant execution** - No delays or animations
- **User validation** - Verifies the user exists via Twitch API
- **Proper capitalization** - Preserves username display name formatting
- **Discord logging** - Tracks all shoutout commands
- **Error handling** - Graceful failure messages
- **Lightweight** - Minimal resource usage

## How It Works
1. User types `!so @username`
2. System verifies the user exists via Twitch API
3. Sends a shoutout message in chat with their channel link
4. Logs the shoutout to Discord (if configured)

**That's it!** No OBS sources, no animations, no delays.

## Example Output

**Successful Shoutout:**
```
Check out TargetUser at https://twitch.tv/TargetUser - They're awesome! Give them a follow!
```

**User Not Found:**
```
Could not find user: unknownuser
```

**Missing Username:**
```
Usage: !so @username or !shoutout @username
```

## Installation

1. Create a new C# action in StreamerBot
2. Copy the contents of `ShoutOutCommand.cs`
3. Set triggers for `!shoutout` and `!so` commands
4. (Optional) Configure Discord webhook in ConfigSetup for logging
5. Test by typing `!so @username` in chat

**No OBS setup required!**

## Dependencies

- StreamerBot with Twitch connection
- Twitch API access (built into StreamerBot)
- (Optional) Discord webhook for logging

## Customization

### Change Chat Message
Edit line 58 in the code:
```csharp
CPH.SendMessage($"Check out {displayName} at https://twitch.tv/{displayName} - They're awesome! Give them a follow!");
```

**Examples:**
```csharp
// Simple version
CPH.SendMessage($"Go check out {displayName} at https://twitch.tv/{displayName}!");

// Friendly version
CPH.SendMessage($"Show some love to {displayName}! https://twitch.tv/{displayName}");

// Raid-focused version
CPH.SendMessage($"Raiding {displayName}! Go follow at https://twitch.tv/{displayName} - They're amazing!");
```

## Use Cases

- **Quick raid shoutouts** - Instant message without waiting for animations
- **Regular streamer highlights** - Promote other creators in your community
- **Community showcases** - Easy way to highlight viewers who stream
- **Friend callouts** - Give quick shouts to friends without interrupting stream
- **Minimal setup streams** - Perfect for streams without OBS overlays

## Differences from OBS Animations Version

### Chat Only Version (This One):
- ✅ No OBS setup required
- ✅ Instant execution (< 1 second)
- ✅ Zero resource usage
- ✅ Works without visual overlays
- ✅ Perfect for simple setups

### OBS Animations Version:
- Requires OBS group and sources setup
- ~19 second animation sequence
- Shows profile picture and clips
- Professional visual presentation
- More complex configuration

## Discord Logging

This command integrates with the Discord logging system. If you've set up a Discord webhook in `ConfigSetup.cs`, it will log:

- **Command executions** - Who ran the shoutout and for whom
- **Successful shoutouts** - Confirmed shoutout completions
- **Warnings** - User not found errors
- **Errors** - Any exceptions or failures

### Logged Information:
```
Command: !shoutout
User: CommandRunner
Target: TargetUser
Status: SUCCESS
```

## Troubleshooting

**"Could not find user" message:**
- Check spelling of the username
- Verify the user exists on Twitch
- Try without the @ symbol: `!so username`

**Command not responding:**
- Verify the command trigger is set correctly in StreamerBot
- Check that the C# action is enabled
- Review StreamerBot logs for errors

**Discord logs not appearing:**
- Verify Discord webhook is configured in ConfigSetup.cs
- Check that `discordLoggingEnabled` is set to `true`
- Test the webhook URL separately

## Performance Notes

- **Execution time:** < 1 second
- **Resource usage:** Minimal (single API call)
- **No delays:** Instant chat message
- **No OBS impact:** Completely independent of OBS

## Author

Created by **HexEchoTV (CUB)**
https://github.com/ThortonEllmers/Streamerbot-Commands

## License

Licensed under the MIT License. See LICENSE file in the project root for full license information.
