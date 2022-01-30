using Newtonsoft.Json;

namespace osu_request_server;

public class UserUnBanMessage : IncomingMessageBase
{
    [JsonProperty("userId")]
    public string UserId = null!;
}