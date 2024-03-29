﻿using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu_request.Config;
using osuTK;

namespace osu_request.Drawables;

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
        RelativeSizeAxes = Axes.X;
        Size = new Vector2(1.0f, 100.0f);
        Masking = true;
        CornerRadius = 10;
        EdgeEffect = OsuRequestEdgeEffects.BasicShadow;

        Children = new Drawable[]
        {
            new Box
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                Colour = OsuRequestColour.Gray2
            },
            new Container
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                Padding = new MarginPadding(10),
                Children = new Drawable[]
                {
                    new TextFlowContainer(t => t.Font = OsuRequestFonts.Regular.With(size: 30))
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        TextAnchor = Anchor.TopLeft,
                        RelativeSizeAxes = Axes.Both,
                        Size = new Vector2(1.0f, 0.5f),
                        Text = Prompt
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
    }

    protected virtual OsuRequestTextBox CreateTextBox(string text)
    {
        return new OsuRequestTextBox
        {
            Anchor = Anchor.TopCentre,
            Origin = Anchor.TopCentre,
            RelativeSizeAxes = Axes.Both,
            Text = text,
            CornerRadius = 5
        };
    }
}