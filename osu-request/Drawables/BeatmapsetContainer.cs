using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Audio.Track;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using volcanicarts.osu.NET.Structures;

namespace osu_request.Drawables
{
    public class BeatmapsetContainer : Container
    {
        private Beatmapset _beatmapset;
        private Track _currentTrack;

        public BeatmapsetContainer(Beatmapset beatmapset)
        {
            _beatmapset = beatmapset;
        }

        [BackgroundDependencyLoader]
        private void Load(TextureStore texture, AudioManager audio)
        {
            if (_beatmapset == null) return;
            var backgroundTexture = texture.Get(_beatmapset.Covers.CardAt2X);
            var background = new Sprite
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Size = backgroundTexture.Size,
                Texture = backgroundTexture,
                Colour = new Colour4(1.0f, 1.0f, 1.0f, 0.5f),
            };
            Add(background);
                
            var textFlowContainer = new TextFlowContainer
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                TextAnchor = Anchor.Centre,
                RelativeSizeAxes = Axes.Both
            };
            textFlowContainer.AddText(_beatmapset.Title,
                t => { t.Font = new FontUsage("Roboto", weight: "Regular", size: 50); });
            Add(textFlowContainer);
                
            _currentTrack = audio.GetTrackStore().Get(_beatmapset.PreviewUrl);
            _currentTrack.Volume.Value = .5;
            _currentTrack.Start();
            _currentTrack.Completed += _currentTrack.Restart;
        }

        protected override void Dispose(bool isDisposing)
        {
            _currentTrack?.Stop();
            _currentTrack?.Dispose();
            base.Dispose(isDisposing);
        }
    }
}