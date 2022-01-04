﻿using osu.Framework.Allocation;
using osu.Framework.Audio.Track;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Graphics;

namespace osu_request.Drawables
{
    public class BeatmapsetCoverContainer : Container
    {
        private readonly Texture _backgroundTexture;
        private readonly Track _previewMp3;

        public BeatmapsetCoverContainer(Texture backgroundTexture, Track previewMp3)
        {
            _backgroundTexture = backgroundTexture;
            _previewMp3 = previewMp3;
        }

        [BackgroundDependencyLoader]
        private void Load()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;
            Masking = true;
            CornerRadius = 10;
            Colour = Color4.White.Opacity(0.75f);
            EdgeEffect = new EdgeEffectParameters
            {
                Colour = Color4.Black.Opacity(0f),
                Radius = 2.5f,
                Type = EdgeEffectType.Shadow,
                Offset = new Vector2(1.5f)
            };
            Child = new BackgroundSprite(_backgroundTexture)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                Size = new Vector2(2.0f, 1.0f)
            };
        }

        protected override bool OnHover(HoverEvent e)
        {
            _previewMp3.Restart();
            this.ScaleTo(1.01f, 100, Easing.OutCubic);
            FadeEdgeEffectTo(Color4.Black.Opacity(0.6f), 100, Easing.OutCubic);
            return true;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            _previewMp3.Stop();
            this.ScaleTo(1.0f, 100, Easing.InCubic);
            FadeEdgeEffectTo(Color4.Black.Opacity(0f), 100, Easing.InCubic);
        }
    }
}