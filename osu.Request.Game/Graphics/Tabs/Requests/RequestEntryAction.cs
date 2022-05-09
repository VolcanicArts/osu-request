// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Request.Game.Graphics.UI.Button;
using osuTK.Graphics;

namespace osu.Request.Game.Graphics.Tabs.Requests;

public class RequestEntryAction : Container
{
    public new Color4 Colour { get; init; }
    public new float CornerRadius { get; init; }
    public IconUsage Icon { get; init; }
    public Action Action;

    [BackgroundDependencyLoader]
    private void load()
    {
        Anchor = Anchor.Centre;
        Origin = Anchor.Centre;
        RelativeSizeAxes = Axes.Both;

        Child = new IconButton
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            RelativeSizeAxes = Axes.Both,
            CornerRadius = CornerRadius,
            Colour = Colour,
            Icon = Icon,
            Action = () => Action.Invoke()
        };
    }
}
