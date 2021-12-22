using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osuTK.Graphics;

namespace osu_request.Drawables
{
    public sealed class SpinBox : Box
    {
        protected override void LoadComplete()
        {
            BeginRotation();
            base.LoadComplete();
        }

        private void BeginRotation()
        {
            this.RotateTo(360, 3000, Easing.InOutQuint)
                .Then().FlashColour(Color4.White, 500, Easing.InOutSine)
                .Then().RotateTo(0, 3000, Easing.InOutQuint)
                .Then().FlashColour(Color4.White, 500, Easing.InOutSine)
                .Loop();
        }
    }
}