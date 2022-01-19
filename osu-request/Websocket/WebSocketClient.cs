using System;
using Newtonsoft.Json;
using osu_request.Config;
using osu_request.Websocket.Structures;
using volcanicarts.osu.NET.Structures;
using User = TwitchLib.Api.Helix.Models.Users.GetUsers.User;

namespace osu_request.Websocket;

public class WebSocketClient : WebSocketClientBase
{
    public Action<RequestArgs> OnNewRequest;
    public Action<Beatmapset> OnBeatmapsetBan;
    public Action<User> OnUserBan;
    public Action<string> OnBeatmapsetUnBan;
    public Action<UserUnBanArgs> OnUserUnBan;

    protected override void OnMessage(WebSocketMessage message)
    {
        base.OnMessage(message);
        switch (message.Op)
        {
            case IncomingOpCode.REQUEST:
                HandleNewRequest(message);
                break;
            case IncomingOpCode.BEATMAPSETBAN:
                HandleBeatmapsetBan(message);
                break;
            case IncomingOpCode.USERBAN:
                HandleUserBan(message);
                break;
            case IncomingOpCode.BEATMAPSETUNBAN:
                HandleBeatmapsetUnBan(message);
                break;
            case IncomingOpCode.USERUNBAN:
                HandleUserUnBan(message);
                break;
        }
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
        OnUserUnBan?.Invoke(userUnBanMessage.Data);
    }

    public void BanBeatmapset(string beatmapsetId)
    {
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