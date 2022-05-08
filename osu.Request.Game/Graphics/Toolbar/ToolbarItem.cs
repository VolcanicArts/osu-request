// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Request.Game.Graphics.Tabs;
using osuTK;
using osuTK.Graphics;

namespace osu.Request.Game.Graphics.Toolbar;

public class ToolbarItem : Button
{
    [Resolved]
    private TabsManager TabsManager { get; set; }

    public int ID { get; init; }

    private const int toolbar_item_width = 200;

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

        Action = () => TabsManager.Select(ID);
    }
}
