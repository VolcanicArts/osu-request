// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;
using osuTK.Graphics;

namespace osu.Request.Game.Graphics.Toolbar;

public class ToolbarItem : Container
{
    private const int toolbar_item_width = 200;

    public int ID { get; init; }

    [BackgroundDependencyLoader]
    private void load()
    {
        Anchor = Anchor.TopLeft;
        Origin = Anchor.TopLeft;
        RelativeSizeAxes = Axes.Y;
        Width = toolbar_item_width;

        Children = new Drawable[]
        {
            new Box
            {
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                RelativeSizeAxes = Axes.Both,
                Colour = OsuRequestColour.Invisible,
            },
            new TextFlowContainer(t =>
            {
                t.Font = OsuRequestFonts.REGULAR.With(size: 30);
                t.Shadow = true;
                t.ShadowColour = Color4.Black.Opacity(0.5f);
                t.ShadowOffset = new Vector2(0.0f, 0.025f);
            })
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                TextAnchor = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                Text = Name
            }
        };
    }
}
