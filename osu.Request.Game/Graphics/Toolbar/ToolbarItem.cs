// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osu.Request.Game.Graphics.Tabs;
using osuTK;
using osuTK.Graphics;

namespace osu.Request.Game.Graphics.Toolbar;

public class ToolbarItem : Button
{
    private const int toolbar_item_width = 200;
    private static readonly ColourInfo hover_colour = ColourInfo.GradientVertical(OsuRequestColour.Gray7.Opacity(0.5f), OsuRequestColour.Invisible);
    private static readonly ColourInfo hover_lost_colour = OsuRequestColour.Invisible;
    private static readonly ColourInfo selected_colour = ColourInfo.GradientVertical(OsuRequestColour.Gray7, OsuRequestColour.Invisible);
    private static readonly ColourInfo deselected_colour = OsuRequestColour.Invisible;

    [Resolved]
    private TabsManager TabsManager { get; set; }

    public int ID { get; init; }

    private bool selected;
    private Box background;

    [BackgroundDependencyLoader]
    private void load()
    {
        Anchor = Anchor.TopLeft;
        Origin = Anchor.TopLeft;
        RelativeSizeAxes = Axes.Y;
        Width = toolbar_item_width;

        Children = new Drawable[]
        {
            background = new Box
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

    protected override bool OnHover(HoverEvent e)
    {
        if (!selected) background.FadeColour(hover_colour, 300, Easing.OutCubic);
        return true;
    }

    protected override void OnHoverLost(HoverLostEvent e)
    {
        base.OnHoverLost(e);
        if (!selected) background.FadeColour(hover_lost_colour, 300, Easing.InCubic);
    }

    protected override bool OnDoubleClick(DoubleClickEvent e)
    {
        return true;
    }

    public void Select()
    {
        FinishTransforms();
        selected = true;
        background.FadeColour(selected_colour, 200, Easing.OutCubic);
    }

    public void DeSelect()
    {
        FinishTransforms();
        selected = false;
        background.FadeColour(deselected_colour, 200, Easing.InCubic);
    }
}
