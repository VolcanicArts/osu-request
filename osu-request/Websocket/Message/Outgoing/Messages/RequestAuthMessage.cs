using Newtonsoft.Json;
using osu_request.Websocket;

namespace osu_request_server;

public class RequestAuthMessage : OutgoingMessageBase
{
    [JsonProperty("code")]
    public string Code = null!;

    [JsonProperty("username")]
    public string Username = null!;

    public RequestAuthMessage()
    {
        Op = OutgoingOpCode.REQUEST_AUTH;
    }
}