using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Textures;
using osu.Framework.Platform;
using osu_request.Clients;
using osu_request.Osu;
using osuTK;
using osuTK.Graphics;
using volcanicarts.osu.NET.Structures;

namespace osu_request.Drawables.Bans
{
    public class BeatmapsetBanContainer : Container
    {
        private AudioManager _audioManager;
        private OsuRequestButton _banButton;
        private FillFlowContainer<BeatmapsetCard> _fillFlowContainer;
        private OsuClientLocal _localOsuClient;
        private OsuRequestTextBox _textBox;
        private TextureStore _textureStore;
        private GameHost _host;

        private void BeatmapsetLoaded(Beatmapset beatmapset)
        {
            var previewMp3 = _audioManager.GetTrackStore().Get(beatmapset.PreviewUrl);
            var backgroundTexture = _textureStore.Get(beatmapset.Covers.CardAt2X);

            if (previewMp3 == null || backgroundTexture == null) return;
            _textBox.Text = string.Empty;

            var beatmapsetContainer = new BeatmapsetCard(beatmapset, backgroundTexture, previewMp3)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.X,
                Size = new Vector2(1.0f, 120.0f),
                Scale = new Vector2(0.49f)
            };

            Scheduler.AddOnce(() => _fillFlowContainer.Add(beatmapsetContainer));
        }

        [BackgroundDependencyLoader]
        private void Load(OsuClientLocal localOsuClient, AudioManager audioManager, TextureStore textureStore, GameHost host,
            BeatmapsetBanManager banManager)
        {
            _host = host;
            _host.Window.Resized += UpdateSizing;
            _localOsuClient = localOsuClient;
            _audioManager = audioManager;
            _textureStore = textureStore;
            InitSelf();
            InitChildren();

            banManager.OnBeatmapsetBan += (beatmapsetId) => _localOsuClient.RequestBeatmapsetFromBeatmapsetId(beatmapsetId, BeatmapsetLoaded);
            banManager.OnBeatmapsetUnBan += (beatmapsetId) => _fillFlowContainer.RemoveAll(child => child.BeatmapsetId == beatmapsetId);

            _banButton.OnButtonClicked += () =>
            {
                var success = banManager.Ban(_textBox.Text);
                if (!success) _textBox.Text = string.Empty;
            };
        }

        private void InitSelf()
        {
            Anchor = Anchor.CentreLeft;
            Origin = Anchor.CentreLeft;
            RelativeSizeAxes = Axes.Both;
            Size = new Vector2(1.0f);
            Masking = true;
            CornerRadius = 10;
            BorderThickness = 2;
            BorderColour = Color4.Black;
            EdgeEffect = OsuRequestEdgeEffects.BasicShadow;
        }

        private void UpdateSizing()
        {
            var width = _host.Window.ClientSize.Width;
            if (width < 500)
            {
                _fillFlowContainer.Children?.ForEach(child => child.Scale = Vector2.One);
            }
            else
            {
                _fillFlowContainer.Children?.ForEach(child => child.Scale = new Vector2(0.49f));
            }
        }

        private void InitChildren()
        {
            Children = new Drawable[]
            {
                new BackgroundColour
                {
                    Colour = OsuRequestColour.GreyLimeDarker
                },
                new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(1.0f),
                    Padding = new MarginPadding(10),
                    Children = new Drawable[]
                    {
                        new Container
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            RelativeSizeAxes = Axes.X,
                            Size = new Vector2(1.0f, 80.0f),
                            Masking = true,
                            CornerRadius = 10,
                            BorderColour = Color4.Black,
                            BorderThickness = 2,
                            EdgeEffect = OsuRequestEdgeEffects.BasicShadow,
                            Children = new Drawable[]
                            {
                                new BackgroundColour
                                {
                                    Colour = OsuRequestColour.GreyLimeDarker
                                },
                                new Container
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    RelativeSizeAxes = Axes.Both,
                                    Size = new Vector2(1.0f),
                                    Padding = new MarginPadding(10),
                                    Child = new Container
                                    {
                                        Anchor = Anchor.TopCentre,
                                        Origin = Anchor.TopCentre,
                                        RelativeSizeAxes = Axes.Both,
                                        Size = new Vector2(1.0f),
                                        Children = new Drawable[]
                                        {
                                            new Container
                                            {
                                                Anchor = Anchor.CentreLeft,
                                                Origin = Anchor.CentreLeft,
                                                RelativeSizeAxes = Axes.Both,
                                                Size = new Vector2(0.795f, 1.0f),
                                                Child = _textBox = new OsuRequestTextBox
                                                {
                                                    Anchor = Anchor.Centre,
                                                    Origin = Anchor.Centre,
                                                    PlaceholderText = "Beatmapset ID",
                                                    CornerRadius = 10,
                                                    BorderThickness = 2
                                                }
                                            },
                                            new Container
                                            {
                                                Anchor = Anchor.CentreRight,
                                                Origin = Anchor.CentreRight,
                                                RelativeSizeAxes = Axes.Both,
                                                Size = new Vector2(0.195f, 1.0f),
                                                Child = _banButton = new OsuRequestButton
                                                {
                                                    Anchor = Anchor.BottomRight,
                                                    Origin = Anchor.BottomRight,
                                                    RelativeSizeAxes = Axes.Both,
                                                    Size = new Vector2(1.0f),
                                                    Text = "Ban",
                                                    FontSize = 40,
                                                    CornerRadius = 10,
                                                    BorderThickness = 2
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        new Container
                        {
                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.BottomCentre,
                            RelativeSizeAxes = Axes.Both,
                            Padding = new MarginPadding
                            {
                                Top = 90.0f
                            },
                            Child = new Container
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                RelativeSizeAxes = Axes.Both,
                                Masking = true,
                                CornerRadius = 10,
                                BorderColour = Color4.Black,
                                BorderThickness = 2,
                                EdgeEffect = OsuRequestEdgeEffects.BasicShadow,
                                Children = new Drawable[]
                                {
                                    new BackgroundColour
                                    {
                                        Colour = OsuRequestColour.GreyLimeDarker
                                    },
                                    new Container
                                    {
                                        Anchor = Anchor.Centre,
                                        Origin = Anchor.Centre,
                                        RelativeSizeAxes = Axes.Both,
                                        Padding = new MarginPadding(5),
                                        Child = new BasicScrollContainer
                                        {
                                            Anchor = Anchor.Centre,
                                            Origin = Anchor.Centre,
                                            RelativeSizeAxes = Axes.Both,
                                            ClampExtension = 20.0f,
                                            ScrollbarVisible = false,
                                            Child = _fillFlowContainer = new FillFlowContainer<BeatmapsetCard>
                                            {
                                                Anchor = Anchor.Centre,
                                                Origin = Anchor.Centre,
                                                RelativeSizeAxes = Axes.X,
                                                AutoSizeAxes = Axes.Y,
                                                Direction = FillDirection.Full,
                                                Spacing = new Vector2(10),
                                                Padding = new MarginPadding(10.0f)
                                            }
                                        }
                                    }
                                }
                            }
                        },
                    }
                }
            };
        }
    }
}