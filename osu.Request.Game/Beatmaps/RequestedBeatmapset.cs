// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using TwitchLib.Api.Helix.Models.Users.GetUsers;

namespace osu.Request.Game.Beatmaps;

public class RequestedBeatmapset
{
    public WorkingBeatmapset WorkingBeatmapset { get; init; }
    public User Requester { get; init; }
}
