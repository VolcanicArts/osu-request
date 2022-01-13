using System;
using System.Net.Http.Json;
using Newtonsoft.Json;
using osu_request.Config;
using osu_request.Websocket.Structures;

namespace osu_request.Websocket;

public class WebSocketClient : WebSocketClientBase
{
    public Action<RequestArgs> OnNewRequest;
    public Action<BeatmapsetBanArgs> OnBeatmapsetBan;

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
        }
    }

    public void SendAuth(OsuRequestConfig osuRequestConfig)
    {
        var authMessage = new AuthMessage
        {
            Op = OutgoingOpCode.AUTH,
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
        var requestArgsMessage = JsonConvert.DeserializeObject<BeatmapsetBanMessage>(message.RawMessage);
        OnBeatmapsetBan?.Invoke(requestArgsMessage.Data);
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
}