using System.Net.Http;
using System.Threading.Tasks;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Logging;
using osu_request.Osu;
using osuTK;
using osuTK.Graphics;

namespace osu_request.Drawables.Bans
{
    public class BeatmapsetBanContainer : Container
    {
        private FillFlowContainer _fillFlowContainer;
        private OsuClientLocal _localOsuClient;
        private OsuRequestButton _banButton;
        private OsuRequestTextBox _textBox;

        private async Task LoadBeatmap(string beatmapId)
        {
            try
            {
                Logger.Log($"Requesting beatmap using Id {beatmapId}");

                if (!_localOsuClient.IsReady)
                {
                    Logger.Log("Client not ready. Cannot request beatmap");
                    return;
                }

                var beatmap = await _localOsuClient.OsuClient.GetBeatmapAsync(beatmapId);
                var beatmapset = await beatmap.GetBeatmapsetAsync();

                Logger.Log($"Successfully loaded beatmapset from beatmap Id {beatmapId}");

                var beatmapsetBanEntry = new BeatmapsetBanEntry(beatmapset);
                Scheduler.AddOnce(() => _fillFlowContainer.Add(beatmapsetBanEntry));
            }
            catch (HttpRequestException)
            {
                Logger.Log($"Unavailable beatmap using Id {beatmapId}", LoggingTarget.Runtime, LogLevel.Error);
            }
        }

        [BackgroundDependencyLoader]
        private void Load(OsuClientLocal localOsuClient)
        {
            _localOsuClient = localOsuClient;
            InitSelf();
            InitChildren();

            _banButton.OnButtonClicked += () => LoadBeatmap(_textBox.Text).ConfigureAwait(false);
        }

        private void InitSelf()
        {
            Anchor = Anchor.CentreLeft;
            Origin = Anchor.CentreLeft;
            RelativeSizeAxes = Axes.Both;
            Size = new Vector2(0.49f, 1.0f);
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
                            Size = new Vector2(1.0f, 0.05f),
                            Child = new AutoSizingSpriteText
                            {
                                Anchor = Anchor.TopCentre,
                                Origin = Anchor.TopCentre,
                                SpriteAnchor = Anchor.TopCentre,
                                SpriteOrigin = Anchor.TopCentre,
                                RelativeSizeAxes = Axes.Both,
                                AutoSizeSpriteTextAxes = Axes.Both,
                                Size = new Vector2(0.2f, 0.75f),
                                Text = { Value = "Beatmap Bans" },
                                Font = new FontUsage("Roboto", weight: "Regular")
                            }
                        },
                        new Container
                        {
                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.BottomCentre,
                            RelativeSizeAxes = Axes.Both,
                            Size = new Vector2(1.0f, 0.95f),
                            Children = new Drawable[]
                            {
                                new Container
                                {
                                    Anchor = Anchor.TopCentre,
                                    Origin = Anchor.TopCentre,
                                    RelativeSizeAxes = Axes.Both,
                                    Size = new Vector2(1.0f, 0.09f),
                                    Masking = true,
                                    CornerRadius = 5,
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
                                                        Size = new Vector2(0.79f, 1.0f),
                                                        Child = _textBox = new OsuRequestTextBox
                                                        {
                                                            Anchor = Anchor.Centre,
                                                            Origin = Anchor.Centre,
                                                            PlaceholderText = "Beatmap ID"
                                                        }
                                                    },
                                                    new Container
                                                    {
                                                        Anchor = Anchor.CentreRight,
                                                        Origin = Anchor.CentreRight,
                                                        RelativeSizeAxes = Axes.Both,
                                                        Size = new Vector2(0.19f, 1.0f),
                                                        Child = _banButton = new OsuRequestButton
                                                        {
                                                            Anchor = Anchor.BottomRight,
                                                            Origin = Anchor.BottomRight,
                                                            RelativeSizeAxes = Axes.Both,
                                                            Size = new Vector2(1.0f),
                                                            Text = "Ban",
                                                            TextSize = new Vector2(0.5f, 0.75f)
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
                                    Size = new Vector2(1.0f, 0.89f),
                                    Masking = true,
                                    CornerRadius = 5,
                                    BorderColour = Color4.Black,
                                    BorderThickness = 2,
                                    EdgeEffect = OsuRequestEdgeEffects.BasicShadow,
                                    Children = new Drawable[]
                                    {
                                        new BackgroundColour
                                        {
                                            Colour = OsuRequestColour.GreyLimeDarker
                                        },
                                        new BasicScrollContainer
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
                                                Direction = FillDirection.Vertical,
                                                Spacing = new Vector2(10),
                                                Padding = new MarginPadding(10.0f)
                                            }
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