using System.Linq;
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
using volcanicarts.osu.NET.Structures;

namespace osu_request.Drawables.Bans
{
    public class BeatmapsetBansList : Container
    {
        private AudioManager _audioManager;
        private FillFlowContainer<BeatmapsetBanEntry> _fillFlowContainer;
        private GameHost _host;
        private OsuClientLocal _localOsuClient;
        private TextureStore _textureStore;

        private void BeatmapsetLoaded(Beatmapset beatmapset)
        {
            var previewMp3 = _audioManager.GetTrackStore().Get(beatmapset.PreviewUrl);
            var backgroundTexture = _textureStore.Get(beatmapset.Covers.CardAt2X);
            if (previewMp3 == null || backgroundTexture == null) return;

            var beatmapsetBan = new BeatmapsetBanEntry(beatmapset, backgroundTexture, previewMp3)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.X,
                Size = new Vector2(1.0f, 120.0f),
                Scale = new Vector2(0.49f)
            };

            Scheduler.AddOnce(() => _fillFlowContainer.Add(beatmapsetBan));
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
            InitChildren();

            banManager.OnBeatmapsetBan += beatmapsetId =>
            {
                _localOsuClient.RequestBeatmapsetFromBeatmapsetId(beatmapsetId,
                    beatmapset => Scheduler.Add(() => BeatmapsetLoaded(beatmapset)));
            };
            banManager.OnBeatmapsetUnBan += beatmapsetId =>
            {
                foreach (var beatmapsetBan in _fillFlowContainer.Where(child => child.BeatmapsetId == beatmapsetId))
                    beatmapsetBan.DisposeGracefully();
            };
        }

        private void UpdateSizing()
        {
            var width = _host.Window.ClientSize.Width;
            if (width < 500)
                _fillFlowContainer.Children?.ForEach(child => child.Scale = Vector2.One);
            else
                _fillFlowContainer.Children?.ForEach(child => child.Scale = new Vector2(0.49f));
        }

        private void InitChildren()
        {
            Child = new Container
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                Masking = true,
                CornerRadius = 10,
                EdgeEffect = OsuRequestEdgeEffects.BasicShadow,
                Children = new Drawable[]
                {
                    new TrianglesBackground
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        ColourLight = OsuRequestColour.Gray3,
                        ColourDark = OsuRequestColour.Gray2,
                        Velocity = 0.5f,
                        TriangleScale = 5
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
                            ClampExtension = 20,
                            ScrollbarVisible = false,
                            Child = _fillFlowContainer = new FillFlowContainer<BeatmapsetBanEntry>
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                RelativeSizeAxes = Axes.X,
                                AutoSizeAxes = Axes.Y,
                                Direction = FillDirection.Full,
                                Spacing = new Vector2(10),
                                Padding = new MarginPadding(10)
                            }
                        }
                    }
                }
            };
        }
    }
}