using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Textures;
using osu_request.Structures;
using osu_request.Websocket;
using osu_request.Websocket.Structures;
using osuTK;
using TwitchLib.Client.Models;
using volcanicarts.osu.NET.Structures;

namespace osu_request.Drawables
{
    public class BeatmapsetRequestsList : Container
    {
        private FillFlowContainer<BeatmapsetRequestEntry> _fillFlowContainer;

        [Resolved]
        private AudioManager AudioManager { get; set; }

        [Resolved]
        private TextureStore TextureStore { get; set; }

        private void NewRequest(RequestArgs requestArgs)
        {
            var previewMp3 = AudioManager.GetTrackStore().Get(requestArgs.Beatmapset.PreviewUrl);
            var backgroundTexture = TextureStore.Get(requestArgs.Beatmapset.Covers.CardAt2X);
            var workingBeatmapset = new WorkingBeatmapset(requestArgs.Beatmapset, backgroundTexture, previewMp3);
            var beatmapsetContainer = new BeatmapsetRequestEntry(workingBeatmapset, requestArgs)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.X,
                Size = new Vector2(1.0f, 120.0f)
            };

            Scheduler.AddOnce(() => _fillFlowContainer.Add(beatmapsetContainer));
        }

        private void OnBeatmapsetBan(Beatmapset beatmapset)
        {
            _fillFlowContainer.Where(entry => entry.BeatmapsetId == beatmapset.Id.ToString()).ForEach(entry => entry.DisposeGracefully());
        }

        private void OnUserBan(UserBanArgs userBanArgs)
        {
            _fillFlowContainer.Where(entry => entry.Username == userBanArgs.User.Login).ForEach(entry => entry.DisposeGracefully());
        }

        [BackgroundDependencyLoader]
        private void Load(WebSocketClient webSocketClient)
        {
            webSocketClient.OnNewRequest += (requestArgs) => Scheduler.Add(() => NewRequest(requestArgs));
            webSocketClient.OnBeatmapsetBan += (beatmapset) => Scheduler.Add(() => OnBeatmapsetBan(beatmapset));
            webSocketClient.OnUserBan += (userBanArgs) => Scheduler.Add(() => OnUserBan(userBanArgs));
            
            Children = new Drawable[]
            {
                new BasicScrollContainer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    ClampExtension = 20,
                    ScrollbarVisible = false,
                    Child = _fillFlowContainer = new FillFlowContainer<BeatmapsetRequestEntry>
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                        Direction = FillDirection.Vertical,
                        Spacing = new Vector2(10),
                        Padding = new MarginPadding(10)
                    }
                }
            };
        }
    }
}