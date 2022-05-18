// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Request.Game.Configuration;
using osu.Request.Game.Graphics.UI.TextBox;

namespace osu.Request.Game.Graphics.Tabs.Settings;

public class SettingContainer : Container
{
    private OsuRequestTextBox SettingText;

    public string SettingValue => SettingText.Text;

    public string Placeholder { get; init; }
    public OsuRequestSetting Setting { get; init; }

    [BackgroundDependencyLoader]
    private void load(OsuRequestConfig osuRequestConfig)
    {
        Masking = true;
        CornerRadius = 10;
        Padding = new MarginPadding(10);
        Child = SettingText = CreateTextBox(osuRequestConfig.Get<string>(Setting));
    }

    protected virtual OsuRequestTextBox CreateTextBox(string text)
    {
        return new OsuRequestTextBox
        {
            Anchor = Anchor.TopCentre,
            Origin = Anchor.TopCentre,
            RelativeSizeAxes = Axes.Both,
            Text = text,
            CornerRadius = 5,
            PlaceholderText = Placeholder
        };
    }
}
