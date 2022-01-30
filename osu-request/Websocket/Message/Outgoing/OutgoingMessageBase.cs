using Newtonsoft.Json;
using osu_request.Websocket;

namespace osu_request_server;

public class OutgoingMessageBase
{
    [JsonProperty("op")]
    public OutgoingOpCode Op;
}