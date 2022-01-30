using Newtonsoft.Json;
using osu_request.Websocket;

namespace osu_request_server;

public class IncomingMessageBase
{
    [JsonProperty("op")]
    public IncomingOpCode Op;
}