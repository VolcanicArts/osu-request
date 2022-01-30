using Newtonsoft.Json;

namespace osu_request.Websocket.Structures;

public class BanUserMessage
{
    [JsonProperty("d")]
    public BanUserData Data;

    [JsonProperty("op")]
    public OutgoingOpCode Op { get; } = OutgoingOpCode.REQUEST_USER_BAN;
}

public class BanUserData
{
    [JsonProperty("username")]
    public string Username;
}