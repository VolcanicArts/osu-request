// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Request.Game.Beatmaps;
using osuTK.Graphics;

namespace osu.Request.Game.Graphics.Beatmaps;

public class BeatmapsetCover : Container
{
    [Resolved]
    private WorkingBeatmapset WorkingBeatmapset { get; set; }

    [BackgroundDependencyLoader]
    private void load()
    {
        Children = new Drawable[]
        {
            new Sprite
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                FillMode = FillMode.Fill,
                FillAspectRatio = WorkingBeatmapset.CoverTexture.Width / (float)WorkingBeatmapset.CoverTexture.Height,
                Texture = WorkingBeatmapset.CoverTexture
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
