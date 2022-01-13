using Newtonsoft.Json;

namespace osu_request.Websocket.Structures;

public class BanUserMessage
{
    [JsonProperty("op")]
    public OutgoingOpCode Op { get; } = OutgoingOpCode.BANUSER;

    [JsonProperty("d")]
    public BanUserData Data;
}

public class BanUserData
{
    [JsonProperty("username")]
    public string Username;
}