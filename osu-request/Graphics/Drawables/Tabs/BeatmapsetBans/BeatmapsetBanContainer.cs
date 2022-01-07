using System.Net.Http;
using System.Threading.Tasks;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Logging;
using osu_request.Osu;
using osuTK;
using osuTK.Graphics;
using volcanicarts.osu.NET.Structures;

namespace osu_request.Drawables.Bans
{
    public class BeatmapsetBanContainer : Container
    {
        private FillFlowContainer _fillFlowContainer;
        private OsuClientLocal _localOsuClient;
        private OsuRequestButton _banButton;
        private OsuRequestTextBox _textBox;
        private AudioManager _audioManager;
        private TextureStore _textureStore;

        private async void BeatmapsetLoaded(Beatmapset beatmapset)
        {
            var previewMp3 = await _audioManager.GetTrackStore().GetAsync(beatmapset.PreviewUrl);
            var backgroundTexture = _textureStore.Get(beatmapset.Covers.CardAt2X);
            var beatmapsetContainer = new BeatmapsetContainer(beatmapset, previewMp3, backgroundTexture)
            {
                Size = new Vector2(0.49f, 1.0f)
            };

            Scheduler.AddOnce(() => _fillFlowContainer.Add(beatmapsetContainer));
        }

        [BackgroundDependencyLoader]
        private void Load(OsuClientLocal localOsuClient, AudioManager audioManager, TextureStore textureStore)
        {
            _localOsuClient = localOsuClient;
            _audioManager = audioManager;
            _textureStore = textureStore;
            InitSelf();
            InitChildren();

            _banButton.OnButtonClicked += () => _localOsuClient.RequestBeatmapsetFromBeatmapId(_textBox.Text, BeatmapsetLoaded);
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
                            RelativeSizeAxes = Axes.Both,
                            Size = new Vector2(1.0f, 0.14f),
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
                                                    PlaceholderText = "Beatmap ID",
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
                                                    TextSize = new Vector2(0.5f, 0.75f),
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
                            Size = new Vector2(1.0f, 0.84f),
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
                                        Child = _fillFlowContainer = new FillFlowContainer
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
                    }
                }
            };
        }
    }
}