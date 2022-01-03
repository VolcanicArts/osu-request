using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Localisation;
using osuTK;
using osuTK.Graphics;

namespace osu_request.Drawables
{
    public class SettingsTextBox : BasicTextBox
    {
        private readonly LocalisableString _placeholderText;

        public SettingsTextBox(LocalisableString placeholderText)
        {
            _placeholderText = placeholderText;
        }

        [BackgroundDependencyLoader]
        private void Load()
        {
            InitSelf();
        }

        private void InitSelf()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.X;
            Size = new Vector2(0.75f, 50);
            PlaceholderText = _placeholderText;
            Masking = true;
            CornerRadius = 10;
            BorderColour = Color4.Black;
            BorderThickness = 5;
            Margin = new MarginPadding
            {
                Bottom = 20.0f
            };
        }
    }
}