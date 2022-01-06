using System;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Platform;
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
            BorderThickness = 2;
            CornerRadius = 5;
            BackgroundFocused = OsuRequestColour.Gray6;
            BackgroundUnfocused = OsuRequestColour.GreyLime;
        }
        
        [BackgroundDependencyLoader]
        private void Load(GameHost host)
        {
            host.Window.Resized += () => Scheduler.AddOnce(RecalculateSize);
        }

        protected override SpriteText CreatePlaceholder()
        {
            var fadingPlaceholderText = base.CreatePlaceholder();
            fadingPlaceholderText.Colour = Color4.White.Opacity(0.5f);
            return fadingPlaceholderText;
        }

        private void RecalculateSize()
        {
            var localSize = MathF.Max(CalculatedTextSize, 0);
            TextFlow?.Children.Cast<Container>().ForEach(container =>
            {
                var character = (SpriteText)container.Child;
                character.Font = character.Font.With(size: localSize);
            });
            Placeholder.Font = Placeholder.Font.With(size: localSize);
            KillFocus();
        }
    }
}