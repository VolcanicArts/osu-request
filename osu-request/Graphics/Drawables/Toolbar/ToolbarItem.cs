using System;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
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
        private Container _content;
        private bool _selected;

        public Action<int> OnSelected;

        [Resolved]
        private BindableBool Locked { get; init; }

        protected internal int ID { get; init; }

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
            RelativeSizeAxes = Axes.Y;
            AutoSizeAxes = Axes.X;
            Height = 1.0f;
        }

        private void InitChildren()
        {
            TextFlowContainer _text;

            Child = _content = new Container
            {
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                RelativeSizeAxes = Axes.Y,
                Size = new Vector2(200, 1.0f),
                Children = new Drawable[]
                {
                    _background = new Box
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        RelativeSizeAxes = Axes.Both,
                        Colour = OsuRequestColour.Invisible,
                        Size = new Vector2(1.0f, 0.9f)
                    },
                    _text = new TextFlowContainer
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        TextAnchor = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both
                    }
                }
            };

            _text.AddText(Name, t =>
            {
                t.Font = OsuRequestFonts.Regular.With(size: 30);
                t.Shadow = true;
                t.ShadowColour = Color4.Black.Opacity(0.5f);
                t.ShadowOffset = new Vector2(0.0f, 0.025f);
            });
        }

        protected override bool OnHover(HoverEvent e)
        {
            if (!_selected)
                _background.FadeColour(ColourInfo.GradientVertical(OsuRequestColour.Gray7.Opacity(0.5f), OsuRequestColour.Invisible), 300,
                    Easing.OutCubic);
            return true;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            if (!_selected) _background.FadeColour(OsuRequestColour.Invisible, 300, Easing.InCubic);
            base.OnHoverLost(e);
        }

        protected override bool OnClick(ClickEvent e)
        {
            if (Locked.Value) return true;
            Select(true);
            return true;
        }

        protected override bool OnDoubleClick(DoubleClickEvent e)
        {
            return true;
        }

        public void Select(bool trigger)
        {
            _selected = true;
            if (trigger) OnSelected?.Invoke(ID);
            _background.FadeColour(ColourInfo.GradientVertical(OsuRequestColour.Gray7, OsuRequestColour.Invisible), 200, Easing.OutCubic);
        }

        public void Deselect()
        {
            _selected = false;
            _background.FadeColour(OsuRequestColour.Invisible, 200, Easing.InCubic);
        }
    }
}