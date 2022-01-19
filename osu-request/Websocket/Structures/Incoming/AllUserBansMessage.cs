using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Users.GetUsers;

namespace osu_request.Websocket.Structures;

public class AllUserBansMessage
{
    [JsonProperty("d")]
    public AllUserBansArgs Data;
}

public class AllUserBansArgs
{
    [JsonProperty("users")]
    public User[] Users;
}