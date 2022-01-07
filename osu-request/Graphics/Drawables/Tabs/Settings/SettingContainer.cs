﻿using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu_request.Config;
using osuTK;
using osuTK.Graphics;

namespace osu_request.Drawables
{
    public class SettingContainer : Container
    {
        protected internal OsuRequestTextBox TextBox { get; private set; }
        protected internal string Prompt { get; init; }
        protected internal OsuRequestSetting Setting { get; init; }

        [BackgroundDependencyLoader]
        private void Load(OsuRequestConfig osuRequestConfig)
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;
            Size = new Vector2(1.0f, 0.2f);
            Masking = true;
            CornerRadius = 10;
            BorderColour = Color4.Black;
            BorderThickness = 2;
            EdgeEffect = OsuRequestEdgeEffects.BasicShadow;

            TextFlowContainer _text;

            Children = new Drawable[]
            {
                new BackgroundColour
                {
                    Colour = OsuRequestColour.GreyLimeDarker
                },
                new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding(10),
                    Children = new Drawable[]
                    {
                        _text = new TextFlowContainer
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            TextAnchor = Anchor.TopLeft,
                            RelativeSizeAxes = Axes.Both,
                            Size = new Vector2(1.0f, 0.5f)
                        },
                        new Container
                        {
                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.BottomCentre,
                            RelativeSizeAxes = Axes.Both,
                            Size = new Vector2(1.0f, 0.5f),
                            Child = TextBox = CreateTextBox(osuRequestConfig.Get<string>(Setting))
                        }
                    }
                }
            };

            _text.AddText(Prompt, t => t.Font = OsuRequestFonts.Regular.With(size: 30));
        }

        protected virtual OsuRequestTextBox CreateTextBox(string text)
        {
            return new OsuRequestTextBox
            {
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                Text = text,
                CornerRadius = 5,
                BorderThickness = 2
            };
        }
    }
}