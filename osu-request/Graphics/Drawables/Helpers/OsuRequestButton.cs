﻿using System;
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

        [BackgroundDependencyLoader]
        private void Load()
        {
            Masking = true;
            CornerRadius = 5;
            BorderColour = Color4.Black;
            BorderThickness = 2;
            EdgeEffect = OsuRequestEdgeEffects.BasicShadow;

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
                    Size = new Vector2(0.15f),
                    AutoSizeSpriteTextAxes = Axes.X,
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
            TweenEdgeEffectTo(OsuRequestEdgeEffects.None, 100, Easing.OutCubic);
            return true;
        }

        protected override void OnMouseUp(MouseUpEvent e)
        {
            TweenEdgeEffectTo(OsuRequestEdgeEffects.BasicShadow, 100, Easing.InCubic);
            base.OnMouseUp(e);
        }
    }
}