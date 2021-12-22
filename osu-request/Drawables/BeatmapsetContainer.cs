using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Audio.Track;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK;
using osuTK.Graphics;
using volcanicarts.osu.NET.Structures;

namespace osu_request.Drawables
{
    public class BeatmapsetContainer : Container
    {
        private const int CORNER_RADIUS = 100;
        private readonly Beatmapset _beatmapset;
        private Track _currentTrack;

        private Sprite background;
        private TextFlowContainer beatmapCreator;
        private TextFlowContainer beatmapTitle;
        private Container contentContainer;

        public BeatmapsetContainer(Beatmapset beatmapset)
        {
            _beatmapset = beatmapset;
        }

        [BackgroundDependencyLoader]
        private void Load(TextureStore textureStore, AudioManager audioManager)
        {
            if (_beatmapset == null) return;

            var backgroundTexture = textureStore.Get(_beatmapset.Covers.CardAt2X);

            var size = 750;
            var sizeRatio = backgroundTexture.Size.Y / backgroundTexture.Size.X;
            var vec2Size = new Vector2(size, size * sizeRatio);

            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Size = vec2Size;
            BorderColour = Color4.Black;
            BorderThickness = 5;
            CornerRadius = CORNER_RADIUS;
            Masking = true;

            Children = new Drawable[]
            {
                background = new Sprite
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Texture = backgroundTexture,
                    Size = vec2Size,
                    Colour = new Color4(0.75f, 0.75f, 0.75f, 1.0f)
                },
                contentContainer = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.9f),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    CornerRadius = CORNER_RADIUS,
                    BorderColour = Color4.Black,
                    BorderThickness = 2,
                    Masking = true,

                    Children = new Drawable[]
                    {
                        new Box
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            Colour = new Color4(0.5f, 0.5f, 0.5f, 0.5f)
                        },
                        beatmapTitle = new TextFlowContainer
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            TextAnchor = Anchor.BottomCentre,
                            RelativeSizeAxes = Axes.Both,
                            Size = new Vector2(1.0f, 0.5f),
                            Padding = new MarginPadding(10)
                        },
                        beatmapCreator = new TextFlowContainer
                        {
                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.BottomCentre,
                            TextAnchor = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            Size = new Vector2(1.0f, 0.5f),
                            Padding = new MarginPadding(10)
                        }
                    }
                }
            };

            beatmapTitle.AddText(_beatmapset.Title,
                t =>
                {
                    t.Font = new FontUsage("Roboto", weight: "Regular", size: 50);
                    t.Shadow = true;
                    t.ShadowColour = new Color4(0, 0, 0, 0.75f);
                    t.ShadowOffset = new Vector2(0.05f);
                });

            beatmapCreator.AddText($"Mapped by {_beatmapset.Creator}",
                t =>
                {
                    t.Font = new FontUsage("Roboto", weight: "Regular", size: 40);
                    t.Shadow = true;
                    t.ShadowColour = new Color4(0, 0, 0, 0.75f);
                    t.ShadowOffset = new Vector2(0.05f);
                });

            _currentTrack = audioManager.GetTrackStore().Get(_beatmapset.PreviewUrl);
            _currentTrack.Volume.Value = .5;
            _currentTrack.Start();
            _currentTrack.Completed += _currentTrack.Restart;
        }

        protected override void Dispose(bool isDisposing)
        {
            _currentTrack?.Dispose();
            base.Dispose(isDisposing);
        }
    }
}