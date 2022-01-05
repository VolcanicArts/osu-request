using System;
using System.Linq;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osuTK.Graphics;

namespace osu_request.Drawables
{
    public class OsuRequestTextBox : BasicTextBox
    {
        public OsuRequestTextBox()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.X;
            Height = 40.0f;
            BorderColour = Color4.Black;
            BorderThickness = 2;
            CornerRadius = 5;
            BackgroundFocused = OsuRequestColour.Gray6;
            BackgroundUnfocused = OsuRequestColour.GreyLime;
        }

        protected override SpriteText CreatePlaceholder()
        {
            var fadingPlaceholderText = base.CreatePlaceholder();
            fadingPlaceholderText.Colour = Color4.White.Opacity(0.5f);
            return fadingPlaceholderText;
        }

        public void RecalculateSize()
        {
            TextFlow?.Children.Cast<Container>().ForEach(container =>
            {
                var character = (SpriteText)container.Child;
                var localSize = MathF.Max(CalculatedTextSize, 0);
                character.Font = character.Font.With(size: localSize);
            });
            KillFocus();
        }
    }
}