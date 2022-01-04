using System;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK.Graphics;

namespace osu_request.Drawables
{
    public class ToolbarItem : Container
    {
        private BackgroundColour _background;
        private Container _content;
        private bool _selected;
        private BindableBool Locked;
        public Action<int> OnSelected;
        protected internal int ID { get; init; }

        [BackgroundDependencyLoader]
        private void Load(BindableBool locked)
        {
            Locked = locked;
            InitSelf();
            InitChildren();
        }

        private void InitSelf()
        {
            Anchor = Anchor.TopLeft;
            Origin = Anchor.TopLeft;
            RelativeSizeAxes = Axes.Y;
            AutoSizeAxes = Axes.X;
            Height = 1.0f;
        }

        private void InitChildren()
        {
            Child = _content = new Container
            {
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                RelativeSizeAxes = Axes.Y,
                AutoSizeAxes = Axes.X,
                Height = 1.0f,
                Masking = true,
                BorderThickness = 3,
                BorderColour = Color4.Black,
                CornerRadius = 10,
                Children = new Drawable[]
                {
                    _background = new BackgroundColour
                    {
                        Colour = OsuRequestColour.GrayA
                    },
                    new AutoSizingSpriteText
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Y,
                        AutoSizeAxes = Axes.X,
                        Height = 1.0f,
                        Text = { Value = Name },
                        Padding = new MarginPadding(10.0f),
                        Font = new FontUsage("Roboto", weight: "Regular")
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
            if (Locked.Value) return true;
            Select(true);
            return true;
        }

        public void Select(bool trigger)
        {
            _selected = true;
            if (trigger) OnSelected?.Invoke(ID);
            _content.MoveToY(10f, 200, Easing.OutCubic);
            _background.FadeColour(OsuRequestColour.Gray4, 250, Easing.OutCubic);
            _content.BorderThickness = 0;
        }

        public void Deselect()
        {
            _content.MoveToY(0f, 500, Easing.OutBounce);
            _background.FadeColour(OsuRequestColour.GrayA, 250, Easing.OutCubic);
            _content.BorderThickness = 3;
            _selected = false;
        }
    }
}