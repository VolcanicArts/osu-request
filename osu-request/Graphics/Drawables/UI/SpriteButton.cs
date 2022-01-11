using System;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Events;
using osu.Framework.Logging;
using osuTK;
using osuTK.Graphics;

namespace osu_request.Drawables
{
    public class SpriteButton : Container
    {
        public Action OnButtonClicked;
        protected internal Texture Texture { get; init; }
        protected internal Color4 BackgroundColour { get; init; }

        [BackgroundDependencyLoader]
        private void Load()
        {
            Masking = true;
            CornerRadius = 5;
            EdgeEffect = OsuRequestEdgeEffects.NoShadow;

            Children = new Drawable[]
            {
                new TrianglesBackground
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    ColourDark = BackgroundColour,
                    ColourLight = BackgroundColour.Lighten(0.2f),
                    TriangleScale = 0.5f,
                    Velocity = 0.2f
                },
                new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding(5),
                    Child = new Sprite
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        FillMode = FillMode.Fit,
                        FillAspectRatio = Texture.Width / (float)Texture.Height,
                        Texture = Texture
                    }
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