using osu.Framework.Graphics;

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
                Text = text,
                CornerRadius = 5
            };
        }
    }
}