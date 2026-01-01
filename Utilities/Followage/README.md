# Followage Command

## Description
Shows how long a user has been following your channel. Users can check their own followage or check another user's followage. Displays the follow date and duration in a human-readable format.

## Command
`!followage` or `!followage @username`

## Features
- Check your own followage
- Check another user's followage
- Human-readable duration (years, months, days, hours)
- Handles non-followers gracefully
- Prevents broadcaster from checking their own followage
- Discord logging integration
- Twitch API integration

## Configuration
This command requires Twitch API credentials to be configured. The credentials are stored in global variables:

- `twitchApiAccessToken` - Your Twitch OAuth token
- `twitchApiClientId` - Your Twitch Client ID

**Required Scopes:**
- `moderator:read:followers` - Required to read follower information

## Dependencies
- Twitch API access token with `moderator:read:followers` scope
- Twitch Client ID
- ConfigSetup.cs or TwitchAPIConfigSetup.cs (for API credentials)

## How It Works
1. User types `!followage` or `!followage @username`
2. System determines target user (self or specified user)
3. Queries Twitch API for follow date
4. Calculates duration since follow date
5. Formats duration into readable text
6. Sends message to chat
7. Logs command usage to Discord

## Example Output

**Own Followage:**
```
UserName, you have been following for 2 years 3 months 15 days! 💜
```

**Another User's Followage:**
```
OtherUser has been following for 5 months 12 days!
```

**Not Following:**
```
UserName, you are not following the channel yet! Hit that follow button! 💜
```

**Broadcaster Check:**
```
StreamerName, you're the broadcaster! You can't follow yourself. 😄
```

**New Follower:**
```
UserName, you have been following for less than an hour! 💜
```

## API Error Handling
The command provides helpful error messages for common API issues:

- **401 Error:** Token invalid/expired - Get new token with correct scope
- **403 Error:** Missing scope - Regenerate token with `moderator:read:followers`
- User not found - Displays friendly error message

## Installation
1. Create a new C# action in StreamerBot
2. Copy the contents of `FollowageCommand.cs`
3. Set the trigger to `!followage` command
4. Configure Twitch API credentials in ConfigSetup.cs or TwitchAPIConfigSetup.cs
5. Test by typing `!followage` in chat

## Setup Twitch API
To get your Twitch API credentials:

1. Go to [https://twitchtokengenerator.com/](https://twitchtokengenerator.com/)
2. Select **Bot Chat Token** or **Custom Scope Token**
3. Add the scope: `moderator:read:followers`
4. Generate token and copy both the **Access Token** and **Client ID**
5. Run ConfigSetup.cs or TwitchAPIConfigSetup.cs to store credentials

## Troubleshooting
- **"Twitch API not configured"** - Run ConfigSetup.cs first
- **401/403 errors** - Regenerate token with correct scopes
- **User not found** - Check spelling of username
- **No followage data** - User may not be following

---
Created by HexEchoTV (CUB) | [GitHub](https://github.com/HexEchoTV/Streamerbot-Commands)
