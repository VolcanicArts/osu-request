using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Graphics;

namespace osu_request.Drawables
{
    public class ToolbarItem : Container
    {
        protected internal int ID { get; set; }
        private BackgroundContainer _background;
        private bool _selected;
        public Action<ToolbarItem> OnSelected;
        private Container _content;

        [BackgroundDependencyLoader]
        private void Load()
        {
            InitSelf();
            InitChildren();
        }

        private void InitSelf()
        {
            Anchor = Anchor.TopLeft;
            Origin = Anchor.TopLeft;
            RelativeSizeAxes = Axes.Both;
            Size = new Vector2(0.1f, 1.0f);
        }

        private void InitChildren()
        {
            Child = _content = new Container
            {
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                RelativeSizeAxes = Axes.Both,
                Masking = true,
                BorderThickness = 3,
                BorderColour = Color4.Black,
                CornerRadius = 10,
                Children = new Drawable[]
                {
                    _background = new BackgroundContainer(Color4.Gray),
                    new Container()
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Size = new Vector2(0.5f),
                        Child = new AutoSizingSpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Text = { Value = "Test" },
                            Font = new FontUsage("Roboto", weight: "Regular")
                        }
                    }
                }
            };
        }

        protected override bool OnHover(HoverEvent e)
        {
            if (!_selected) _content.MoveToY(5f, 200, Easing.OutCubic);

            return true;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            if (!_selected) _content.MoveToY(0f, 200, Easing.OutBounce);

            base.OnHoverLost(e);
        }

        protected override bool OnClick(ClickEvent e)
        {
            Select();
            return true;
        }

        public void Select()
        {
            _selected = true;
            OnSelected.Invoke(this);
            _content.MoveToY(10f, 200, Easing.OutCubic);
            _background.FadeColour(Color4.DarkGray, 250, Easing.OutCubic);
            _content.BorderThickness = 0;
        }

        public void Deselect()
        {
            _content.MoveToY(0f, 500, Easing.OutBounce);
            _background.FadeColour(Color4.Gray, 250, Easing.OutCubic);
            _content.BorderThickness = 3;
            _selected = false;
        }
    }
}