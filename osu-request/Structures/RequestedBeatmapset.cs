using volcanicarts.osu.NET.Structures;
using User = TwitchLib.Api.Helix.Models.Users.GetUsers.User;

namespace osu_request.Structures;

public class RequestedBeatmapset
{
    public readonly Beatmapset Beatmapset;
    public readonly User Requester;

    public RequestedBeatmapset(Beatmapset beatmapset, User requester)
    {
        Beatmapset = beatmapset;
        Requester = requester;
    }
}