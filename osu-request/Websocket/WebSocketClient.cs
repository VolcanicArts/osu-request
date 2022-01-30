using System;
using Newtonsoft.Json;
using osu_request.Config;
using osu_request.Websocket.Structures;
using volcanicarts.osu.NET.Structures;
using User = TwitchLib.Api.Helix.Models.Users.GetUsers.User;

namespace osu_request.Websocket;

public class WebSocketClient : WebSocketClientBase
{
    public Action<Beatmapset> OnBeatmapsetBan;
    public Action<string> OnBeatmapsetUnBan;
    public Action OnConnect;
    public Action OnDisconnect;
    public Action OnFailedConnection;
    public Action OnInvalidCode;
    public Action OnInvalidUsername;
    public Action OnLoggedIn;
    public Action<RequestArgs> OnNewRequest;
    public Action<User> OnUserBan;
    public Action<string> OnUserUnBan;

    protected override void OnMessage(WebSocketMessage message)
    {
        base.OnMessage(message);
        switch (message.Op)
        {
            case IncomingOpCode.AUTH_INVALID_USERNAME:
                OnInvalidUsername?.Invoke();
                break;
            case IncomingOpCode.AUTH_INVALID_CODE:
                OnInvalidCode?.Invoke();
                break;
            case IncomingOpCode.AUTH_LOGGED_IN:
                OnLoggedIn?.Invoke();
                break;
            case IncomingOpCode.AUTH_ALL_BEATMAPSET_BANS:
                HandleAllBeatmapsetBans(message);
                break;
            case IncomingOpCode.AUTH_ALL_USER_BANS:
                HandleAllUserBans(message);
                break;
            case IncomingOpCode.BEATMAPSET_REQUEST:
                HandleNewRequest(message);
                break;
            case IncomingOpCode.BEATMAPSET_BAN:
                HandleBeatmapsetBan(message);
                break;
            case IncomingOpCode.BEATMAPSET_UNBAN:
                HandleBeatmapsetUnBan(message);
                break;
            case IncomingOpCode.USER_BAN:
                HandleUserBan(message);
                break;
            case IncomingOpCode.USER_UNBAN:
                HandleUserUnBan(message);
                break;
            case IncomingOpCode.SOCKET_CONNECTED:
                // ignore
                break;
            default:
                throw new ArgumentOutOfRangeException($"Unexpected OpCode: {message.Op}");
        }
    }

    protected override void OnDisconnected()
    {
        base.OnDisconnected();
        OnDisconnect?.Invoke();
    }

    protected override void OnConnected()
    {
        base.OnConnected();
        OnConnect?.Invoke();
    }

    public void SendAuth(OsuRequestConfig osuRequestConfig)
    {
        var authMessage = new AuthMessage
        {
            Data = new AuthData
            {
                Username = osuRequestConfig.Get<string>(OsuRequestSetting.Username),
                Passcode = osuRequestConfig.Get<string>(OsuRequestSetting.Passcode)
            }
        };
        SendText(JsonConvert.SerializeObject(authMessage));
    }

    private void HandleNewRequest(WebSocketMessage message)
    {
        var requestArgsMessage = JsonConvert.DeserializeObject<RequestArgsMessage>(message.RawMessage);
        OnNewRequest?.Invoke(requestArgsMessage.Data);
    }

    private void HandleBeatmapsetBan(WebSocketMessage message)
    {
        var beatmapsetBanMessage = JsonConvert.DeserializeObject<BeatmapsetBanMessage>(message.RawMessage);
        OnBeatmapsetBan?.Invoke(beatmapsetBanMessage.Data.Beatmapset);
    }

    private void HandleUserBan(WebSocketMessage message)
    {
        var userBanMessage = JsonConvert.DeserializeObject<UserBanMessage>(message.RawMessage);
        OnUserBan?.Invoke(userBanMessage.Data.User);
    }

    private void HandleBeatmapsetUnBan(WebSocketMessage message)
    {
        var beatmapsetUnBanMessage = JsonConvert.DeserializeObject<BeatmapsetUnBanMessage>(message.RawMessage);
        OnBeatmapsetUnBan?.Invoke(beatmapsetUnBanMessage.Data.BeatmapsetId);
    }

    private void HandleUserUnBan(WebSocketMessage message)
    {
        var userUnBanMessage = JsonConvert.DeserializeObject<UserUnBanMessage>(message.RawMessage);
        OnUserUnBan?.Invoke(userUnBanMessage.Data.UserId);
    }

    private void HandleAllBeatmapsetBans(WebSocketMessage message)
    {
        var allBeatmapsetBansMessage = JsonConvert.DeserializeObject<AllBeatmapsetBansMessage>(message.RawMessage);
        foreach (var beatmapset in allBeatmapsetBansMessage.Data.Beatmapsets) OnBeatmapsetBan?.Invoke(beatmapset);
    }

    private void HandleAllUserBans(WebSocketMessage message)
    {
        var allUserBansMessage = JsonConvert.DeserializeObject<AllUserBansMessage>(message.RawMessage);
        foreach (var user in allUserBansMessage.Data.Users) OnUserBan?.Invoke(user);
    }

    public void BanBeatmapset(string beatmapsetId)
    {
        if (string.IsNullOrEmpty(beatmapsetId)) return;
        var banBeatmapsetMessage = new BanBeatmapsetMessage
        {
            Data = new BanBeatmapsetData
            {
                BeatmapsetId = beatmapsetId
            }
        };
        SendText(JsonConvert.SerializeObject(banBeatmapsetMessage));
    }

    public void BanUser(string username)
    {
        if (string.IsNullOrEmpty(username)) return;
        var banUserMessage = new BanUserMessage
        {
            Data = new BanUserData
            {
                Username = username
            }
        };
        SendText(JsonConvert.SerializeObject(banUserMessage));
    }

    public void UnBanBeatmapset(string beatmapsetId)
    {
        if (string.IsNullOrEmpty(beatmapsetId)) return;
        var unBanBeatmapsetMessage = new UnBanBeatmapsetMessage
        {
            Data = new UnBanBeatmapsetData
            {
                BeatmapsetId = beatmapsetId
            }
        };
        SendText(JsonConvert.SerializeObject(unBanBeatmapsetMessage));
    }

    public void UnBanUser(string userId)
    {
        if (string.IsNullOrEmpty(userId)) return;
        var unBanUserMessage = new UnBanUserMessage
        {
            Data = new UnBanUserData
            {
                UserId = userId
            }
        };
        SendText(JsonConvert.SerializeObject(unBanUserMessage));
    }
}