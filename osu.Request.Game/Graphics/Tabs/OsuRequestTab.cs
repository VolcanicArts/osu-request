// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using System.Collections.Generic;
using osu.Framework.Graphics.Sprites;

namespace osu.Request.Game.Graphics.Tabs;

public struct OsuRequestTab
{
    public static IReadOnlyList<OsuRequestTab> TabList = new List<OsuRequestTab>
    {
        new()
        {
            Id = 0,
            Title = "Requests",
            Icon = FontAwesome.Solid.Download
        },
        new()
        {
            Id = 1,
            Title = "Beatmapset Bans",
            Icon = FontAwesome.Solid.Ban
        },
        new()
        {
            Id = 2,
            Title = "User Bans",
            Icon = FontAwesome.Solid.UserSlash
        },
        new()
        {
            Id = 3,
            Title = "Settings",
            Icon = FontAwesome.Solid.Cog
        }
    };

    public int Id { get; init; }
    public string Title { get; init; }
    public IconUsage Icon { get; init; }
}
