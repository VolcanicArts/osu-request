using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Textures;
using osu_request.Clients;
using osu_request.Osu;
using osu_request.Twitch;
using osuTK;
using TwitchLib.Client.Models;
using volcanicarts.osu.NET.Structures;

namespace osu_request.Drawables
{
    public class BeatmapsetListContainer : Container
    {
        private FillFlowContainer _fillFlowContainer;

        private OsuClientLocal _localOsuClient;
        private TwitchClientLocal _localTwitchClient;
        private TextureStore _textureStore;
        private AudioManager _audioManager;
        private BeatmapsetBanManager _beatmapsetBanManager;

        private void HandleTwitchMessage(ChatMessage message)
        {
            if (message.Message.StartsWith("!rq"))
            {
                var beatmapsetId = message.Message.Split(" ")[1];
                if (_beatmapsetBanManager.IsBanned(beatmapsetId)) return;
                _localOsuClient.RequestBeatmapsetFromBeatmapsetId(beatmapsetId, (beatmapset) => Scheduler.Add(() => BeatmapsetLoaded(beatmapset)));
            }
        }

        private void BeatmapsetLoaded(Beatmapset beatmapset)
        {
            var previewMp3 = _audioManager.GetTrackStore().Get(beatmapset.PreviewUrl);
            var backgroundTexture = _textureStore.Get(beatmapset.Covers.CardAt2X);
            var beatmapsetContainer = new BeatmapsetRequestContainer(beatmapset, previewMp3, backgroundTexture)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.X,
                Size = new Vector2(1.0f, 120.0f)
            };

            Scheduler.AddOnce(() => _fillFlowContainer.Add(beatmapsetContainer));
        }

        [BackgroundDependencyLoader]
        private void Load(TextureStore textureStore, AudioManager audioManager, TwitchClientLocal twitchClient,
            OsuClientLocal osuClient, BeatmapsetBanManager beatmapsetBanManager)
        {
            _textureStore = textureStore;
            _audioManager = audioManager;
            _localTwitchClient = twitchClient;
            _localTwitchClient.OnChatMessage += HandleTwitchMessage;
            _localOsuClient = osuClient;
            _beatmapsetBanManager = beatmapsetBanManager;
            Children = new Drawable[]
            {
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
            };
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            _localTwitchClient.OnChatMessage -= HandleTwitchMessage;
        }
    }
}