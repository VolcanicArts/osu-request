using Newtonsoft.Json;

namespace osu_request.Websocket.Structures;

public class UserUnBanMessage
{
    [JsonProperty("d")]
    public UserUnBanArgs Data;
}

public class UserUnBanArgs
{
    [JsonProperty("userId")]
    public string UserId;
}