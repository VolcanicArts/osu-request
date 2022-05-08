// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using osu.Framework.Graphics;
using osu.Request.Game.Graphics.UI.TextBox;

namespace osu.Request.Game.Graphics.Tabs.Settings;

public class MaskedSettingContainer : SettingContainer
{
    protected override OsuRequestTextBox CreateTextBox(string text)
    {
        return new OsuRequestMaskedTextBox
        {
            Anchor = Anchor.TopCentre,
            Origin = Anchor.TopCentre,
            RelativeSizeAxes = Axes.Both,
            Text = text,
            CornerRadius = 5
        };
    }
}
