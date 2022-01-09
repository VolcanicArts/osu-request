using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK.Graphics;

namespace osu_request.Drawables
{
    public class TrianglesBackground : Container
    {
        protected internal Color4 ColourLight { get; init; } = OsuRequestColour.Blue;
        protected internal Color4 ColourDark { get; init; } = OsuRequestColour.BlueDark;
        protected internal float Velocity { get; init; } = 1.0f;
        protected internal float TriangleScale { get; init; } = 1.0f;

        [BackgroundDependencyLoader]
        private void Load()
        {
            var backgroundColour = new BackgroundColour
            {
                Colour = ColourDark
            };
            var triangles = new Triangles
            {
                ColourLight = ColourLight,
                ColourDark = ColourDark,
                RelativeSizeAxes = Axes.Both,
                Velocity = Velocity,
                TriangleScale = TriangleScale
            };
            AddInternal(backgroundColour);
            AddInternal(triangles);
        }
    }
}