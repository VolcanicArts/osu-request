using Newtonsoft.Json;

namespace osu_request.Websocket.Structures;

public class AuthMessage
{
    [JsonProperty("d")]
    public AuthData Data;

    [JsonProperty("op")]
    public OutgoingOpCode Op { get; } = OutgoingOpCode.REQUEST_AUTH;
}

public class AuthData
{
    [JsonProperty("code")]
    public string Passcode;

    [JsonProperty("username")]
    public string Username;
}