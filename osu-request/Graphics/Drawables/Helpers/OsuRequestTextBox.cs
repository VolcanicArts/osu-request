using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osuTK;
using osuTK.Graphics;

namespace osu_request.Drawables
{
    public class OsuRequestTextBox : BasicTextBox
    {
        public OsuRequestTextBox()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;
            Size = new Vector2(1.0f);
            BorderColour = Color4.Black;
            BackgroundFocused = OsuRequestColour.Gray6;
            BackgroundUnfocused = OsuRequestColour.GreyLime;
        }

        protected override SpriteText CreatePlaceholder()
        {
            var fadingPlaceholderText = base.CreatePlaceholder();
            fadingPlaceholderText.Colour = Color4.White.Opacity(0.5f);
            return fadingPlaceholderText;
        }
    }
}