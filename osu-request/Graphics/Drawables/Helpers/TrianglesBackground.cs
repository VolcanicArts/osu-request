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
                Velocity = 0.5f
            };
            AddInternal(backgroundColour);
            AddInternal(triangles);
        }
    }
}