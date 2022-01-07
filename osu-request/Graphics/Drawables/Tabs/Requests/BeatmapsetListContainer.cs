using System.Net.Http;
using System.Threading.Tasks;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Textures;
using osu.Framework.Logging;
using osu_request.Osu;
using osu_request.Twitch;
using osuTK;
using TwitchLib.Client.Models;
using volcanicarts.osu.NET.Structures;

namespace osu_request.Drawables
{
    public class BeatmapsetListContainer : Container
    {
        private AudioManager _audioManager;
        private FillFlowContainer _fillFlowContainer;

        private OsuClientLocal _localOsuClient;
        private TwitchClientLocal _localTwitchClient;
        private TextureStore _textureStore;

        private void HandleTwitchMessage(ChatMessage message)
        {
            if (message.Message.StartsWith("!rq"))
            {
                var beatmapId = message.Message.Split(" ")[1];
                _localOsuClient.RequestBeatmapsetFromBeatmapId(beatmapId, BeatmapsetLoaded);
            }
        }

        private async void BeatmapsetLoaded(Beatmapset beatmapset)
        {
            var previewMp3 = await _audioManager.GetTrackStore().GetAsync(beatmapset.PreviewUrl);
            var backgroundTexture = _textureStore.Get(beatmapset.Covers.CardAt2X);
            var beatmapsetContainer = new BeatmapsetContainer(beatmapset, previewMp3, backgroundTexture);

            Scheduler.AddOnce(() => _fillFlowContainer.Add(beatmapsetContainer));
        }

        [BackgroundDependencyLoader]
        private void Load(TextureStore textureStore, AudioManager audioManager, TwitchClientLocal twitchClient,
            OsuClientLocal osuClient)
        {
            _textureStore = textureStore;
            _audioManager = audioManager;
            _localTwitchClient = twitchClient;
            _localTwitchClient.OnChatMessage += HandleTwitchMessage;
            _localOsuClient = osuClient;
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