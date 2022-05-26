// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using System;
using System.Net;
using NetCoreServer;
using Newtonsoft.Json;
using osu.Framework.Logging;
using osu.Request.Game.Beatmaps;
using osu.Request.Game.Configuration;
using osu.Request.Game.Remote.Messages;
using osu.Request.Game.Remote.Messages.Incoming;
using volcanicarts.osu.NET.Structures;

namespace osu.Request.Game.Remote;

public class WebSocketClient : WebSocketClientBase
{
    public Action OnAuthenticationFail;
    public Action<Beatmapset> OnBeatmapsetBan;
    public Action OnBeatmapsetNonExistent;
    public Action<string> OnBeatmapsetUnBan;
    public Action OnConnect;
    public Action OnDisconnect;
    public Action<RequestedBeatmapset> OnNewRequest;
    public Action OnServerError;
    public Action<User> OnUserBan;
    public Action OnUserNonexistent;
    public Action<string> OnUserUnBan;

    public WebSocketClient(OsuRequestConfig osuRequestConfig)
        : base(osuRequestConfig)
    {
    }

    protected override void OnMessage(IncomingMessageBase message, string rawMessage)
    {
        base.OnMessage(message, rawMessage);

        switch (message.Op)
        {
            case IncomingOpCode.SERVER_ERROR:
                OnServerError?.Invoke();
                break;

            case IncomingOpCode.SERVER_BEATMAPSET_NONEXISTENT:
                OnBeatmapsetNonExistent?.Invoke();
                break;

            case IncomingOpCode.SERVER_USER_NONEXISTENT:
                OnUserNonexistent?.Invoke();
                break;

            case IncomingOpCode.BEATMAPSET_REQUEST:
                handleNewRequest(JsonConvert.DeserializeObject<BeatmapsetRequestMessage>(rawMessage));
                break;

            // case IncomingOpCode.AUTH_ALL_BEATMAPSET_BANS:
            //     handleAllBeatmapsetBans(JsonConvert.DeserializeObject<AuthAllBeatmapsetBansMessage>(rawMessage));
            //     break;
            //
            // case IncomingOpCode.AUTH_ALL_USER_BANS:
            //     handleAllUserBans(JsonConvert.DeserializeObject<AuthAllUserBansMessage>(rawMessage));
            //     break;

            // case IncomingOpCode.BEATMAPSET_BAN:
            //     handleBeatmapsetBan(JsonConvert.DeserializeObject<BeatmapsetBanMessage>(rawMessage));
            //     break;
            //
            // case IncomingOpCode.BEATMAPSET_UNBAN:
            //     handleBeatmapsetUnBan(JsonConvert.DeserializeObject<BeatmapsetUnBanMessage>(rawMessage));
            //     break;
            //
            // case IncomingOpCode.USER_BAN:
            //     handleUserBan(JsonConvert.DeserializeObject<UserBanMessage>(rawMessage));
            //     break;
            //
            // case IncomingOpCode.USER_UNBAN:
            //     handleUserUnBan(JsonConvert.DeserializeObject<UserUnBanMessage>(rawMessage));
            //     break;

            default:
                throw new ArgumentOutOfRangeException($"Unexpected OpCode: {message.Op}");
        }
    }

    protected override void OnReceivedResponse(HttpResponse response)
    {
        base.OnReceivedResponse(response);
        if (response.Status == (int)HttpStatusCode.Unauthorized) OnAuthenticationFail?.Invoke();
    }

    public override void OnWsDisconnected()
    {
        base.OnWsDisconnected();
        OnDisconnect?.Invoke();
    }

    public override void OnWsConnected(HttpResponse response)
    {
        base.OnWsConnected(response);
        OnConnect?.Invoke();
    }

    private void handleNewRequest(BeatmapsetRequestMessage message)
    {
        Logger.Log(message.Beatmapset.Title);
        OnNewRequest?.Invoke(new RequestedBeatmapset
        {
            WorkingBeatmapset = new WorkingBeatmapset
            {
                Beatmapset = message.Beatmapset
            },
            Requester = message.Requester
        });
    }

    // private void handleBeatmapsetBan(BeatmapsetBanMessage message)
    // {
    //     OnBeatmapsetBan?.Invoke(message.Beatmapset);
    // }
    //
    // private void handleUserBan(UserBanMessage message)
    // {
    //     OnUserBan?.Invoke(message.User);
    // }
    //
    // private void handleBeatmapsetUnBan(BeatmapsetUnBanMessage message)
    // {
    //     OnBeatmapsetUnBan?.Invoke(message.BeatmapsetId);
    // }
    //
    // private void handleUserUnBan(UserUnBanMessage message)
    // {
    //     OnUserUnBan?.Invoke(message.UserId);
    // }
    //
    // private void handleAllBeatmapsetBans(AuthAllBeatmapsetBansMessage message)
    // {
    //     foreach (var beatmapset in message.Beatmapsets) OnBeatmapsetBan?.Invoke(beatmapset);
    // }
    //
    // private void handleAllUserBans(AuthAllUserBansMessage message)
    // {
    //     foreach (var user in message.Users) OnUserBan?.Invoke(user);
    // }

    // public void BanBeatmapset(string beatmapsetId)
    // {
    //     if (string.IsNullOrEmpty(beatmapsetId)) return;
    //     var banBeatmapsetMessage = new RequestBeatmapsetBanMessage
    //     {
    //         BeatmapsetId = beatmapsetId
    //     };
    //     SendText(JsonConvert.SerializeObject(banBeatmapsetMessage));
    // }
    //
    // public void BanUser(string username)
    // {
    //     if (string.IsNullOrEmpty(username)) return;
    //     var banUserMessage = new RequestUserBanMessage
    //     {
    //         Username = username
    //     };
    //     SendText(JsonConvert.SerializeObject(banUserMessage));
    // }
    //
    // public void UnBanBeatmapset(string beatmapsetId)
    // {
    //     if (string.IsNullOrEmpty(beatmapsetId)) return;
    //     var unBanBeatmapsetMessage = new RequestBeatmapsetUnBanMessage
    //     {
    //         BeatmapsetId = beatmapsetId
    //     };
    //     SendText(JsonConvert.SerializeObject(unBanBeatmapsetMessage));
    // }
    //
    // public void UnBanUser(string userId)
    // {
    //     if (string.IsNullOrEmpty(userId)) return;
    //     var unBanUserMessage = new RequestUserUnBanMessage
    //     {
    //         UserId = userId
    //     };
    //     SendText(JsonConvert.SerializeObject(unBanUserMessage));
    // }

    public void ConnectOrReconnect()
    {
        if (IsConnected)
        {
            ReconnectAsync();
        }
        else
        {
            ConnectAsync();
        }
    }
}
