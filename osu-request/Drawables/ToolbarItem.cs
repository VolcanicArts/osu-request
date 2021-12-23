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
        private Box _background;
        private Container _scaleContainer;
        public Action<ToolbarItem> OnSelected;
        private bool Selected;

        [BackgroundDependencyLoader]
        private void Load()
        {
            Child = _scaleContainer = new Container
            {
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                RelativeSizeAxes = Axes.Both,
                Size = new Vector2(1.0f),
                Masking = true,
                CornerRadius = 10,
                BorderColour = Color4.Black,
                BorderThickness = 3,
                Children = new Drawable[]
                {
                    _background = new Box
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Size = new Vector2(1.0f),
                        Colour = Color4.Gray
                    },
                    new TextFlowContainer
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Text = "Test",
                        TextAnchor = Anchor.Centre
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
            if (!Selected) _scaleContainer.MoveToY(5f, 200, Easing.OutCubic);

            return true;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            if (!Selected) _scaleContainer.MoveToY(0f, 200, Easing.OutBounce);

            base.OnHoverLost(e);
        }

        protected override bool OnClick(ClickEvent e)
        {
            Selected = true;
            OnSelected.Invoke(this);
            _scaleContainer.MoveToY(10f, 200, Easing.OutCubic);
            _background.FadeColour(Color4.DarkGray, 250, Easing.OutCubic);
            _scaleContainer.BorderThickness = 0;
            return true;
        }

        public void Deselect()
        {
            _scaleContainer.MoveToY(0f, 500, Easing.OutBounce);
            _background.FadeColour(Color4.Gray, 250, Easing.OutCubic);
            _scaleContainer.BorderThickness = 3;
            Selected = false;
        }
    }
}