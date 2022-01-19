using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Platform;
using osuTK.Graphics;

namespace osu_request.Drawables
{
    public class OsuRequestTextBox : BasicTextBox
    {
        public OsuRequestTextBox()
        {
            BackgroundFocused = OsuRequestColour.Gray6;
            BackgroundUnfocused = OsuRequestColour.Gray4;
        }

        [BackgroundDependencyLoader]
        private void Load(GameHost host)
        {
            host.Window.Resized += () => Scheduler.AddOnce(KillFocus);
        }

        protected override SpriteText CreatePlaceholder()
        {
            var fadingPlaceholderText = base.CreatePlaceholder();
            fadingPlaceholderText.Colour = Color4.White.Opacity(0.5f);
            return fadingPlaceholderText;
        }
    }
}