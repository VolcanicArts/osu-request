using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Graphics;

namespace osu_request.Drawables
{
    public class ToolbarItem : Container
    {
        private Container _scaleContainer;

        [BackgroundDependencyLoader]
        private void Load()
        {
            Anchor = Anchor.TopLeft;
            Origin = Anchor.TopLeft;
            RelativeSizeAxes = Axes.Y;
            Size = new Vector2(1.0f);
            Child = _scaleContainer = new Container()
            {
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                RelativeSizeAxes = Axes.Both,
                Size = new Vector2(1.0f),
                Masking = true,
                BorderColour = Color4.Black,
                BorderThickness = 5,
                Children = new Drawable[]
                {
                    new Box()
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Size = new Vector2(1.0f),
                        Colour = Color4.Green
                    }
                }
            };
        }

        protected override void UpdateAfterAutoSize()
        {
            Size = new Vector2(DrawHeight, Size.Y);
            base.UpdateAfterAutoSize();
        }

        protected override bool OnHover(HoverEvent e)
        {
            _scaleContainer.MoveToY(10f, 200, Easing.OutCubic);
            return true;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            _scaleContainer.MoveToY(0f, 500, Easing.OutBounce);
            base.OnHoverLost(e);
        }
    }
}