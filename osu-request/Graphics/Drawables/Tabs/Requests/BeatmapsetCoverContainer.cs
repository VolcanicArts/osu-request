﻿using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK.Graphics;

namespace osu_request.Drawables
{
    public class BeatmapsetCoverContainer : Container
    {
        private readonly Texture Texture;

        public BeatmapsetCoverContainer(Texture texture)
        {
            Texture = texture;
        }

        [BackgroundDependencyLoader]
        private void Load()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;
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
                new BackgroundColour
                {
                    Colour = ColourInfo.GradientHorizontal(Color4.Black.Opacity(0.9f), Color4.Black.Opacity(0.5f))
                }
            };
        }
    }
}