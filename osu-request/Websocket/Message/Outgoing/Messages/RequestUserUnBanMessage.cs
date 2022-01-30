using Newtonsoft.Json;
using osu_request.Websocket;

namespace osu_request_server;

public class RequestUserUnBanMessage : OutgoingMessageBase
{
    [JsonProperty("userId")]
    public string UserId = null!;

    public RequestUserUnBanMessage()
    {
        Op = OutgoingOpCode.REQUEST_USER_UNBAN;
    }
}