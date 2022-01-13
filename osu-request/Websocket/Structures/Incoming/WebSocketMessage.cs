using Newtonsoft.Json;

namespace osu_request.Websocket.Structures;

public class WebSocketMessage
{
    [JsonProperty("op")]
    public OpCode Op;

    [JsonIgnore]
    public string RawMessage;

}