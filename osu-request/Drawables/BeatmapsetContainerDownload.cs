using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Graphics;

namespace osu_request.Drawables
{
    public class BeatmapsetContainerDownload : Container
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
            Masking = true;
            Size = new Vector2(1f);
            Position = new Vector2(-20, 0);
        }

        private void InitChildren()
        {
            Children = new Drawable[]
            {
                new Box
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(1.0f),
                    RelativeSizeAxes = Axes.Both,
                    Colour = new Color4(0.0f, 0.75f, 0.0f, 1.0f)
                }
            };
        }

        protected override bool OnHover(HoverEvent e)
        {
            this.MoveTo(new Vector2(-50, 0), 200, Easing.OutQuad);
            return false;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            this.MoveTo(new Vector2(-20, 0), 500, Easing.OutBounce);
        }
    }
}