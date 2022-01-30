using osu.Framework.Audio.Track;
using osu.Framework.Graphics.Textures;
using TwitchLib.Api.Helix.Models.Users.GetUsers;

namespace osu_request.Structures;

public class WorkingRequestedBeatmapset : WorkingBeatmapset
{
    public WorkingRequestedBeatmapset(RequestedBeatmapset requestedBeatmapset, Texture cover, Track preview) : base(requestedBeatmapset.Beatmapset,
        cover, preview)
    {
        Requester = requestedBeatmapset.Requester;
    }

    protected internal User Requester { get; }
}