using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace osu_request.Drawables
{
    public class RequestsTab : Container
    {
        [BackgroundDependencyLoader]
        private void Load()
        {
            InitSelf();
            InitChildren();
        }

        private void InitSelf()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;
            Padding = new MarginPadding(20.0f);
        }

        private void InitChildren()
        {
            Children = new Drawable[]
            {
                new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Masking = true,
                    CornerRadius = 10,
                    EdgeEffect = OsuRequestEdgeEffects.BasicShadow,
                    Children = new Drawable[]
                    {
                        new TrianglesBackground
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            ColourLight = OsuRequestColour.Gray3,
                            ColourDark = OsuRequestColour.Gray2,
                            Velocity = 0.5f,
                            TriangleScale = 5.0f
                        },
                        new BeatmapsetListContainer
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            Padding = new MarginPadding
                            {
                                Vertical = 6.0f,
                                Horizontal = 20.0f
                            }
                        }
                    }
                }
            };
        }
    }
}