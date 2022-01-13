using Newtonsoft.Json;

namespace osu_request.Websocket.Structures;

public class AuthMessage
{
    [JsonProperty("op")]
    public OutgoingOpCode Op;
    
    [JsonProperty("d")]
    public AuthData Data;
}

public class AuthData
{
    [JsonProperty("username")]
    public string Username;

    [JsonProperty("code")]
    public string Passcode;
}