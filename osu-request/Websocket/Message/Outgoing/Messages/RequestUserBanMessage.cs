using Newtonsoft.Json;
using osu_request.Websocket;

namespace osu_request_server;

public class RequestUserBanMessage : OutgoingMessageBase
{
    [JsonProperty("username")]
    public string Username = null!;

    public RequestUserBanMessage()
    {
        Op = OutgoingOpCode.REQUEST_USER_BAN;
    }
}