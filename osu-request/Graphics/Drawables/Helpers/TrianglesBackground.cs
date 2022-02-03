using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK.Graphics;

namespace osu_request.Drawables;

public class TrianglesBackground : Container
{
    protected internal Color4 ColourLight { get; init; } = OsuRequestColour.Blue;
    protected internal Color4 ColourDark { get; init; } = OsuRequestColour.BlueDark;
    protected internal float Velocity { get; init; } = 1;
    protected internal float TriangleScale { get; init; } = 1;

    [BackgroundDependencyLoader]
    private void Load()
    {
        InternalChildren = new Drawable[]
        {
            new Box
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                Colour = ColourDark
            },
            new Triangles
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                ColourLight = ColourLight,
                ColourDark = ColourDark,
                Velocity = Velocity,
                TriangleScale = TriangleScale
            }
        };
    }
}