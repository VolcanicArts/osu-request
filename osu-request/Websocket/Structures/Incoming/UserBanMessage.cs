using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Users.GetUsers;

namespace osu_request.Websocket.Structures;

public class UserBanMessage
{
    [JsonProperty("d")]
    public UserBanArgs Data;
}

public class UserBanArgs
{
    [JsonProperty("user")]
    public User User;
}