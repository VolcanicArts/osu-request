using System.Net.Http.Json;
using Newtonsoft.Json;
using osu_request.Config;
using osu_request.Websocket.Structures;

namespace osu_request.Websocket;

public class WebSocketClient : WebSocketClientBase
{
    public WebSocketClient(string address, int port) : base(address, port) { }

    protected override void OnMessage(string message)
    {
        base.OnMessage(message);
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
}