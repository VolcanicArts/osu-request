using Newtonsoft.Json;

namespace osu_request.Websocket.Structures;

public class UnBanUserMessage
{
    [JsonProperty("op")]
    public OutgoingOpCode Op { get; } = OutgoingOpCode.UNBANUSER;

    [JsonProperty("d")]
    public UnBanUserData Data;
}

public class UnBanUserData
{
    [JsonProperty("userId")]
    public string UserId;
}
