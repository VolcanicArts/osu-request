using osu.Framework.Graphics;
using osuTK;

namespace osu_request.Drawables
{
    public class SecretSettingContainer : SettingContainer
    {
        protected override OsuRequestTextBox CreateTextBox(string text)
        {
            return new OsuRequestPasswordTextBox
            {
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                RelativeSizeAxes = Axes.Both,
                Size = new Vector2(1.0f),
                Text = text
            };
        }
    }
}