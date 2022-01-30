using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Users.GetUsers;

namespace osu_request_server;

public class AuthAllUserBansMessage : IncomingMessageBase
{
    [JsonProperty("users")]
    public User[] Users = null!;
}