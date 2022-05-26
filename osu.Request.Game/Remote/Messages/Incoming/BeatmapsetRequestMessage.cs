// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using Newtonsoft.Json;
using volcanicarts.osu.NET.Structures;
using User = TwitchLib.Api.Helix.Models.Users.GetUsers.User;

namespace osu.Request.Game.Remote.Messages.Incoming;

public class BeatmapsetRequestMessage : IncomingMessageBase
{
    [JsonProperty("beatmapset")]
    public Beatmapset Beatmapset = null!;

    [JsonProperty("requester")]
    public User Requester = null!;
}
