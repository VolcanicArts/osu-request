using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Users.GetUsers;

namespace osu_request_server;

public class UserBanMessage : IncomingMessageBase
{
    [JsonProperty("user")]
    public User User = null!;
}