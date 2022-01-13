using System;
using System.Net.Http.Json;
using Newtonsoft.Json;
using osu_request.Config;
using osu_request.Websocket.Structures;

namespace osu_request.Websocket;

public class WebSocketClient : WebSocketClientBase
{
    public Action<RequestArgs> OnNewRequest;

    protected override void OnMessage(WebSocketMessage message)
    {
        base.OnMessage(message);
        switch (message.Op)
        {
            case OpCode.REQUEST:
                HandleNewRequest(message);
                break;
        }
    }

    public void SendAuth(OsuRequestConfig osuRequestConfig)
    {
        var authMessage = new AuthMessage
        {
            Op = OpCode.AUTH,
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
}