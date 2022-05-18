// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osu.Request.Game.Graphics.Tabs;

namespace osu.Request.Game.Graphics.Toolbar;

public class ToolbarItem : Button
{
    private static readonly ColourInfo selected_colour = OsuRequestColour.Gray7;
    private static readonly ColourInfo deselected_colour = OsuRequestColour.Invisible;

    [Resolved]
    private TabsManager TabsManager { get; set; }

    public OsuRequestTab Tab { get; init; }

    private Box background;

    [BackgroundDependencyLoader]
    private void load()
    {
        Anchor = Anchor.TopLeft;
        Origin = Anchor.TopLeft;
        RelativeSizeAxes = Axes.Both;
        FillMode = FillMode.Fit;
        Padding = new MarginPadding(5);

        Children = new Drawable[]
        {
            new Container
            {
                RelativeSizeAxes = Axes.Both,
                Masking = true,
                CornerRadius = 5,
                Children = new Drawable[]
                {
                    background = new Box
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        RelativeSizeAxes = Axes.Both,
                        Colour = OsuRequestColour.Invisible,
                    },
                    new Container
                    {
                        RelativeSizeAxes = Axes.Both,
                        Padding = new MarginPadding(5),
                        Children = new Drawable[]
                        {
                            new SpriteIcon
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                RelativeSizeAxes = Axes.Both,
                                Icon = Tab.Icon,
                                Shadow = true
                            }
                        }
                    }
                }
            },
        };

        Action = () => TabsManager.Select(Tab.Id);
    }

    protected override bool OnDoubleClick(DoubleClickEvent e)
    {
        return true;
    }

    public void Select()
    {
        FinishTransforms();
        background.FadeColour(selected_colour, 200, Easing.OutCubic);
    }

    public void DeSelect()
    {
        FinishTransforms();
        background.FadeColour(deselected_colour, 200, Easing.InCubic);
    }
}
