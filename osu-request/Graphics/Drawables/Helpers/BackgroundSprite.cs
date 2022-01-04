using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;

namespace osu_request.Drawables
{
    public class BackgroundSprite : Sprite
    {
        private readonly Texture _texture;

        public BackgroundSprite(Texture texture)
        {
            _texture = texture;
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
            RelativeSizeAxes = Axes.Both;
            Texture = _texture;
        }
    }
}