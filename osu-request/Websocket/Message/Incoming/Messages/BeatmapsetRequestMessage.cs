using Newtonsoft.Json;
using volcanicarts.osu.NET.Structures;
using User = TwitchLib.Api.Helix.Models.Users.GetUsers.User;

namespace osu_request_server;

public class BeatmapsetRequestMessage : IncomingMessageBase
{
    [JsonProperty("beatmapset")]
    public Beatmapset Beatmapset = null!;

    [JsonProperty("requester")]
    public User Requester = null!;
}