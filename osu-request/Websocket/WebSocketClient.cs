using System;
using Newtonsoft.Json;
using osu.Framework.Logging;
using osu_request.Config;
using osu_request.Structures;
using osu_request_server;
using volcanicarts.osu.NET.Structures;
using User = TwitchLib.Api.Helix.Models.Users.GetUsers.User;

namespace osu_request.Websocket;

public class WebSocketClient : WebSocketClientBase
{
    public Action<Beatmapset> OnBeatmapsetBan;
    public Action<string> OnBeatmapsetUnBan;
    public Action OnConnect;
    public Action OnDisconnect;
    public Action OnInvalidCode;
    public Action OnInvalidUsername;
    public Action OnLoggedIn;
    public Action<RequestedBeatmapset> OnNewRequest;
    public Action<User> OnUserBan;
    public Action<string> OnUserUnBan;
    public Action OnSocketUnauthenticated;
    public Action OnServerError;
    public Action OnBeatmapsetNonExistent;
    public Action OnUserNonexistent;

    protected override void OnMessage(IncomingMessageBase message, string rawMessage)
    {
        base.OnMessage(message, rawMessage);
        switch (message.Op)
        {
            case IncomingOpCode.SOCKET_UNAUTHENTICATED:
                OnSocketUnauthenticated?.Invoke();
                break;
            case IncomingOpCode.SERVER_ERROR:
                OnServerError?.Invoke();
                break;
            case IncomingOpCode.SERVER_BEATMAPSET_NONEXISTENT:
                OnBeatmapsetNonExistent?.Invoke();
                break;
            case IncomingOpCode.SERVER_USER_NONEXISTENT:
                OnUserNonexistent?.Invoke();
                break;
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
                HandleAllBeatmapsetBans(JsonConvert.DeserializeObject<AuthAllBeatmapsetBansMessage>(rawMessage));
                break;
            case IncomingOpCode.AUTH_ALL_USER_BANS:
                HandleAllUserBans(JsonConvert.DeserializeObject<AuthAllUserBansMessage>(rawMessage));
                break;
            case IncomingOpCode.BEATMAPSET_REQUEST:
                HandleNewRequest(JsonConvert.DeserializeObject<BeatmapsetRequestMessage>(rawMessage));
                break;
            case IncomingOpCode.BEATMAPSET_BAN:
                HandleBeatmapsetBan(JsonConvert.DeserializeObject<BeatmapsetBanMessage>(rawMessage));
                break;
            case IncomingOpCode.BEATMAPSET_UNBAN:
                HandleBeatmapsetUnBan(JsonConvert.DeserializeObject<BeatmapsetUnBanMessage>(rawMessage));
                break;
            case IncomingOpCode.USER_BAN:
                HandleUserBan(JsonConvert.DeserializeObject<UserBanMessage>(rawMessage));
                break;
            case IncomingOpCode.USER_UNBAN:
                HandleUserUnBan(JsonConvert.DeserializeObject<UserUnBanMessage>(rawMessage));
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
        var authMessage = new RequestAuthMessage
        {
            Username = osuRequestConfig.Get<string>(OsuRequestSetting.Username),
            Code = osuRequestConfig.Get<string>(OsuRequestSetting.Passcode)
        };
        SendText(JsonConvert.SerializeObject(authMessage));
    }

    private void HandleNewRequest(BeatmapsetRequestMessage message)
    {
        Logger.Log(message.Beatmapset.Title);
        OnNewRequest?.Invoke(new RequestedBeatmapset(message.Beatmapset, message.Requester));
    }

    private void HandleBeatmapsetBan(BeatmapsetBanMessage message)
    {
        OnBeatmapsetBan?.Invoke(message.Beatmapset);
    }

    private void HandleUserBan(UserBanMessage message)
    {
        OnUserBan?.Invoke(message.User);
    }

    private void HandleBeatmapsetUnBan(BeatmapsetUnBanMessage message)
    {
        OnBeatmapsetUnBan?.Invoke(message.BeatmapsetId);
    }

    private void HandleUserUnBan(UserUnBanMessage message)
    {
        OnUserUnBan?.Invoke(message.UserId);
    }

    private void HandleAllBeatmapsetBans(AuthAllBeatmapsetBansMessage message)
    {
        foreach (var beatmapset in message.Beatmapsets) OnBeatmapsetBan?.Invoke(beatmapset);
    }

    private void HandleAllUserBans(AuthAllUserBansMessage message)
    {
        foreach (var user in message.Users) OnUserBan?.Invoke(user);
    }

    public void BanBeatmapset(string beatmapsetId)
    {
        if (string.IsNullOrEmpty(beatmapsetId)) return;
        var banBeatmapsetMessage = new RequestBeatmapsetBanMessage
        {
            BeatmapsetId = beatmapsetId
        };
        SendText(JsonConvert.SerializeObject(banBeatmapsetMessage));
    }

    public void BanUser(string username)
    {
        if (string.IsNullOrEmpty(username)) return;
        var banUserMessage = new RequestUserBanMessage
        {
            Username = username
        };
        SendText(JsonConvert.SerializeObject(banUserMessage));
    }

    public void UnBanBeatmapset(string beatmapsetId)
    {
        if (string.IsNullOrEmpty(beatmapsetId)) return;
        var unBanBeatmapsetMessage = new RequestBeatmapsetUnBanMessage
        {
            BeatmapsetId = beatmapsetId
        };
        SendText(JsonConvert.SerializeObject(unBanBeatmapsetMessage));
    }

    public void UnBanUser(string userId)
    {
        if (string.IsNullOrEmpty(userId)) return;
        var unBanUserMessage = new RequestUserUnBanMessage
        {
            UserId = userId
        };
        SendText(JsonConvert.SerializeObject(unBanUserMessage));
    }
}