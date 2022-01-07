using System;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;

namespace osu_request.Drawables
{
    public class ToolbarItem : Container
    {
        private Container _innerContent;
        private BackgroundColour _outerBackground;
        private Container _outerContent;
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
            TextFlowContainer _text;

            Child = _outerContent = new Container
            {
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                RelativeSizeAxes = Axes.Y,
                Size = new Vector2(200, 1.0f),
                Masking = true,
                CornerRadius = 5,
                Children = new Drawable[]
                {
                    _outerBackground = new BackgroundColour
                    {
                        Colour = OsuRequestColour.Gray7
                    },
                    _innerContent = new Container
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Size = new Vector2(0.9f, 0.75f),
                        Masking = true,
                        CornerRadius = 5,
                        EdgeEffect = OsuRequestEdgeEffects.BasicShadowBlack,
                        Children = new Drawable[]
                        {
                            new BackgroundColour
                            {
                                Colour = OsuRequestColour.GreyLimeDarker
                            },
                            _text = new TextFlowContainer
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                TextAnchor = Anchor.Centre,
                                RelativeSizeAxes = Axes.Both,
                                Size = new Vector2(1.0f)
                            }
                        }
                    }
                }
            };

            _text.AddText(Name, t => t.Font = new FontUsage("Roboto", weight: "Regular", size: 30));
        }

        protected override bool OnHover(HoverEvent e)
        {
            if (!_selected) _outerContent.MoveToY(5f, 200, Easing.OutCubic);

            return true;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            if (!_selected) _outerContent.MoveToY(0f, 200, Easing.OutCubic);

            base.OnHoverLost(e);
        }

        protected override bool OnClick(ClickEvent e)
        {
            if (Locked.Value) return true;
            Select(true);
            return true;
        }

        protected override bool OnDoubleClick(DoubleClickEvent e) => true;

        public void Select(bool trigger)
        {
            _selected = true;
            if (trigger) OnSelected?.Invoke(ID);
            _outerContent.MoveToY(7.5f, 100, Easing.InCubic);
            _outerBackground.FadeColour(OsuRequestColour.Gray4, 250, Easing.OutCubic);
            _innerContent.TweenEdgeEffectTo(OsuRequestEdgeEffects.NoShadow, 250, Easing.OutCubic);
        }

        public void Deselect()
        {
            _outerContent.MoveToY(0f, 100, Easing.OutCubic);
            _outerBackground.FadeColour(OsuRequestColour.Gray7, 250, Easing.InCubic);
            _innerContent.TweenEdgeEffectTo(OsuRequestEdgeEffects.BasicShadowBlack, 250, Easing.InCubic);
            _selected = false;
        }
    }
}