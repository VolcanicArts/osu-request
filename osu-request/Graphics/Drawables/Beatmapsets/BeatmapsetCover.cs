using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK.Graphics;

namespace osu_request.Drawables;

public class BeatmapsetCover : Container
{
    private readonly Texture Texture;

    public BeatmapsetCover(Texture texture)
    {
        Texture = texture;
    }

    [BackgroundDependencyLoader]
    private void Load()
    {
        Masking = true;
        CornerRadius = 10;

        Children = new Drawable[]
        {
            new Sprite
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                FillMode = FillMode.Fill,
                FillAspectRatio = Texture.Width / (float)Texture.Height,
                Texture = Texture
            },
            new Box
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                Colour = ColourInfo.GradientHorizontal(Color4.Black.Opacity(0.9f), Color4.Black.Opacity(0.5f))
            }
        };
    }
}