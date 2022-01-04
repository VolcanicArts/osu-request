using System.Drawing;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;
using osuTK.Graphics;

namespace osu_request.Drawables
{
    public class RequestsListingTab : Container
    {
        private Container _borderContainer;

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
            Size = new Vector2(0.95f, 1.0f);
            Padding = new MarginPadding(20.0f);
        }

        protected override void UpdateAfterAutoSize()
        {
            base.UpdateAfterAutoSize();
            _borderContainer.CornerRadius = DrawWidth / 20.0f;
        }

        private void InitChildren()
        {
            Child = _borderContainer = new Container
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                BorderThickness = 6,
                BorderColour = Color4.Black,
                Masking = true,
                CornerRadius = 20,
                Children = new Drawable[]
                {
                    new BackgroundColour
                    {
                        Colour = OsuRequestColour.Gray6
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
            };
        }
    }
}