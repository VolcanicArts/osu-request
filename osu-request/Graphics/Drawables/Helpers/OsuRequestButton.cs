using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Graphics;

namespace osu_request.Drawables
{
    public class OsuRequestButton : Container
    {
        public Action OnButtonClicked;
        protected internal string Text { get; init; }
        protected internal Vector2 TextSize { get; init; }

        [BackgroundDependencyLoader]
        private void Load()
        {
            Masking = true;
            BorderColour = Color4.Black;
            EdgeEffect = OsuRequestEdgeEffects.NoShadow;

            Children = new Drawable[]
            {
                new BackgroundColour
                {
                    Colour = OsuRequestColour.GreyLime
                },
                new AutoSizingSpriteText
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Size = TextSize,
                    AutoSizeSpriteTextAxes = Axes.Both,
                    Text = { Value = Text }
                }
            };
        }

        protected override bool OnClick(ClickEvent e)
        {
            OnButtonClicked?.Invoke();
            return true;
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            this.MoveToY(0, 100, Easing.OutCubic);
            TweenEdgeEffectTo(OsuRequestEdgeEffects.NoShadow, 100, Easing.OutCubic);
            return true;
        }

        protected override void OnMouseUp(MouseUpEvent e)
        {
            this.MoveToY(-1.5f, 100, Easing.InCubic);
            TweenEdgeEffectTo(OsuRequestEdgeEffects.BasicShadow, 100, Easing.InCubic);
            base.OnMouseUp(e);
        }

        protected override bool OnHover(HoverEvent e)
        {
            this.MoveToY(-1.5f, 100, Easing.InCubic);
            TweenEdgeEffectTo(OsuRequestEdgeEffects.BasicShadow, 100, Easing.InCubic);
            return true;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            this.MoveToY(0, 100, Easing.OutCubic);
            TweenEdgeEffectTo(OsuRequestEdgeEffects.NoShadow, 100, Easing.OutCubic);
            base.OnHoverLost(e);
        }
    }
}