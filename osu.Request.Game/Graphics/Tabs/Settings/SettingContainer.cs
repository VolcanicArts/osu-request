// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Request.Game.Configuration;
using osu.Request.Game.Graphics.UI.TextBox;
using osuTK;

namespace osu.Request.Game.Graphics.Tabs.Settings;

public class SettingContainer : Container
{
    private OsuRequestTextBox SettingText;

    public string SettingValue => SettingText.Text;

    public string Title { get; init; }
    public OsuRequestSetting Setting { get; init; }

    [BackgroundDependencyLoader]
    private void load(OsuRequestConfig osuRequestConfig)
    {
        Masking = true;
        CornerRadius = 10;

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
                    new TextFlowContainer(t => t.Font = OsuRequestFonts.REGULAR.With(size: 30))
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        TextAnchor = Anchor.TopLeft,
                        RelativeSizeAxes = Axes.Both,
                        Size = new Vector2(1.0f, 0.5f),
                        Text = Title
                    },
                    new Container
                    {
                        Anchor = Anchor.BottomCentre,
                        Origin = Anchor.BottomCentre,
                        RelativeSizeAxes = Axes.Both,
                        Size = new Vector2(1.0f, 0.5f),
                        Child = SettingText = CreateTextBox(osuRequestConfig.Get<string>(Setting))
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
