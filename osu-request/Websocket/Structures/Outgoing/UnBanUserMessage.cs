using Newtonsoft.Json;

namespace osu_request.Websocket.Structures;

public class UnBanUserMessage
{
    [JsonProperty("d")]
    public UnBanUserData Data;

    [JsonProperty("op")]
    public OutgoingOpCode Op { get; } = OutgoingOpCode.REQUEST_USER_UNBAN;
}

public class UnBanUserData
{
    [JsonProperty("userId")]
    public string UserId;
}