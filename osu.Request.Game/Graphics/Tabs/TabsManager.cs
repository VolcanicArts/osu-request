// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Request.Game.Graphics.Triangles;

namespace osu.Request.Game.Graphics.Tabs;

public sealed class TabsManager : Container
{
    public TabsManager()
    {
        Anchor = Anchor.Centre;
        Origin = Anchor.Centre;
        RelativeSizeAxes = Axes.Both;
    }

    [BackgroundDependencyLoader]
    private void load()
    {
        Children = new Drawable[]
        {
            new TrianglesBackground
            {
                ColourLight = OsuRequestColour.Gray7,
                ColourDark = OsuRequestColour.Gray4,
                TriangleScale = 4
            },
        };
    }
}
