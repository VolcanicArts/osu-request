using Newtonsoft.Json;

namespace osu_request.Websocket.Structures;

public class BanUserMessage
{
    [JsonProperty("d")]
    public BanUserData Data;

    [JsonProperty("op")]
    public OutgoingOpCode Op { get; } = OutgoingOpCode.BANUSER;
}

public class BanUserData
{
    [JsonProperty("username")]
    public string Username;
}