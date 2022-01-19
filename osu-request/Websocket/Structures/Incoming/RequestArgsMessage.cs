using Newtonsoft.Json;
using volcanicarts.osu.NET.Structures;
using User = TwitchLib.Api.Helix.Models.Users.GetUsers.User;

namespace osu_request.Websocket.Structures;

public class RequestArgsMessage
{
    [JsonProperty("d")]
    public RequestArgs Data;
}

public class RequestArgs
{
    [JsonProperty("beatmapset")]
    public Beatmapset Beatmapset;

    [JsonProperty("requester")]
    public User Requester;
}