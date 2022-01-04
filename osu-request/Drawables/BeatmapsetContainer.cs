using osu.Framework.Allocation;
using osu.Framework.Audio.Track;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK;
using osuTK.Graphics;
using volcanicarts.osu.NET.Structures;

namespace osu_request.Drawables
{
    public class BeatmapsetContainer : Container
    {
        private readonly Texture _backgroundTexture;
        private readonly Beatmapset _beatmapset;
        private readonly Track _previewMp3;

        public BeatmapsetContainer(Beatmapset beatmapset, Track previewMp3, Texture backgroundTexture)
        {
            _beatmapset = beatmapset;
            _previewMp3 = previewMp3;
            _backgroundTexture = backgroundTexture;

            _previewMp3.Volume.Value = 0.5f;
            _previewMp3.Completed += _previewMp3.Restart;
        }

        protected override void UpdateAfterAutoSize()
        {
            Size = new Vector2(Size.X, DrawWidth * 0.35f * 0.5f);
            base.UpdateAfterAutoSize();
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            this.FadeInFromZero(1000, Easing.InQuad);
        }

        [BackgroundDependencyLoader]
        private void Load()
        {
            InitSelf();
            InitChildren();
        }

        private void InitSelf()
        {
            Alpha = 0;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.X;
            Margin = new MarginPadding(10.0f);
            Masking = true;
            CornerRadius = 10;
            EdgeEffect = new EdgeEffectParameters
            {
                Colour = Color4.Black.Opacity(0.6f),
                Radius = 2.5f,
                Type = EdgeEffectType.Shadow,
                Offset = new Vector2(1.5f, 1.5f)
            };
        }

        private void InitChildren()
        {
            Children = new Drawable[]
            {
                new BackgroundColour
                {
                    Colour = OsuRequestColour.GreyLime
                },
                new Container
                {
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.25f, 1.0f),
                    Padding = new MarginPadding
                    {
                        Left = 5,
                        Right = 2.5f,
                        Top = 5,
                        Bottom = 5
                    },
                    Child = new BeatmapsetCoverContainer(_backgroundTexture, _previewMp3)
                },
                new Container
                {
                    Anchor = Anchor.CentreRight,
                    Origin = Anchor.CentreRight,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.75f, 1.0f),
                    Padding = new MarginPadding
                    {
                        Left = 2.5f,
                        Right = 5f,
                        Top = 5,
                        Bottom = 5
                    },
                    Child = new Container
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Masking = true,
                        CornerRadius = 10,
                        Children = new Drawable[]
                        {
                            new BackgroundColour
                            {
                                Colour = OsuRequestColour.GreyLimeDarker
                            },
                            new AutoSizingSpriteText
                            {
                                Anchor = Anchor.TopLeft,
                                Origin = Anchor.TopLeft,
                                SpriteAnchor = Anchor.TopLeft,
                                SpriteOrigin = Anchor.TopLeft,
                                RelativeSizeAxes = Axes.Both,
                                Size = new Vector2(1.0f, 0.2f),
                                RelativeAnchorPosition = new Vector2(0f),
                                Text = { Value = _beatmapset.Title },
                                Font = new FontUsage("Roboto", weight: "Regular"),
                                Margin = new MarginPadding(10)
                            },
                            new AutoSizingSpriteText
                            {
                                Anchor = Anchor.TopLeft,
                                Origin = Anchor.TopLeft,
                                SpriteAnchor = Anchor.TopLeft,
                                SpriteOrigin = Anchor.TopLeft,
                                RelativeSizeAxes = Axes.Both,
                                Size = new Vector2(1.0f, 0.15f),
                                RelativeAnchorPosition = new Vector2(0.0f, 0.2f),
                                Text = { Value = $"Mapped by {_beatmapset.Creator}" },
                                Font = new FontUsage("Roboto", weight: "Regular"),
                                Margin = new MarginPadding(10)
                            }
                        }
                    }
                }
            };
        }

        protected override void Dispose(bool isDisposing)
        {
            _previewMp3?.Dispose();
            base.Dispose(isDisposing);
        }
    }
}