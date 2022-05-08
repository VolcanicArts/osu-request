// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Input.Events;
using osu.Request.Game.Graphics.Triangles;
using osuTK.Graphics;

namespace osu.Request.Game.Graphics.UI.Button;

public class OsuRequestButton : Framework.Graphics.UserInterface.Button
{
    public new Color4 Colour { get; init; }

    [BackgroundDependencyLoader]
    private void load()
    {
        Masking = true;
        EdgeEffect = OsuRequestEdgeEffects.NO_SHADOW;

        InternalChild = new TrianglesBackground
        {
            ColourLight = Colour.Lighten(0.25f),
            ColourDark = Colour
        };
    }

    protected override bool OnClick(ClickEvent e)
    {
        base.OnClick(e);
        if (!IsHovered) return true;

        this.MoveToY(-1.5f, 100, Easing.InCubic);
        TweenEdgeEffectTo(OsuRequestEdgeEffects.BASIC_SHADOW, 100, Easing.InCubic);
        return true;
    }

    protected override bool OnMouseDown(MouseDownEvent e)
    {
        this.MoveToY(0, 100, Easing.OutCubic);
        TweenEdgeEffectTo(OsuRequestEdgeEffects.NO_SHADOW, 100, Easing.OutCubic);
        return true;
    }

    protected override bool OnHover(HoverEvent e)
    {
        this.MoveToY(-1.5f, 100, Easing.InCubic);
        TweenEdgeEffectTo(OsuRequestEdgeEffects.BASIC_SHADOW, 100, Easing.InCubic);
        return true;
    }

    protected override void OnHoverLost(HoverLostEvent e)
    {
        this.MoveToY(0, 100, Easing.OutCubic);
        TweenEdgeEffectTo(OsuRequestEdgeEffects.NO_SHADOW, 100, Easing.OutCubic);
        base.OnHoverLost(e);
    }
}
