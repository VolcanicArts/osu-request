using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;

namespace osu_request.Drawables
{
    public class BackgroundColour : Box
    {
        [BackgroundDependencyLoader]
        private void Load()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;
        }
    }
}