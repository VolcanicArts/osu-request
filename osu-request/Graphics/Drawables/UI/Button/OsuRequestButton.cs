using System;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;
using osuTK.Graphics;

namespace osu_request.Drawables
{
    public class OsuRequestButton : Container
    {
        public Action OnButtonClicked;
        
        protected internal Color4 BackgroundColour { get; init; } = OsuRequestColour.BlueDark;

        [BackgroundDependencyLoader]
        private void Load()
        {
            Masking = true;
            EdgeEffect = OsuRequestEdgeEffects.NoShadow;

            InternalChild = new TrianglesBackground
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                ColourDark = BackgroundColour,
                ColourLight = BackgroundColour.Lighten(0.2f)
            };
        }

        protected override bool OnClick(ClickEvent e)
        {
            OnButtonClicked?.Invoke();
            if (!IsHovered) return true;
            this.MoveToY(-1.5f, 100, Easing.InCubic);
            TweenEdgeEffectTo(OsuRequestEdgeEffects.BasicShadow, 100, Easing.InCubic);
            return true;
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            this.MoveToY(0, 100, Easing.OutCubic);
            TweenEdgeEffectTo(OsuRequestEdgeEffects.NoShadow, 100, Easing.OutCubic);
            return true;
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