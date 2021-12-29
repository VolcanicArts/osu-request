using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Logging;
using osu_request.Twitch;
using osuTK;
using osuTK.Graphics;
using TwitchLib.Client.Models;
using volcanicarts.osu.NET.Client;
using volcanicarts.osu.NET.Structures;

namespace osu_request.Drawables
{
    public class BeatmapsetListContainer : Container
    {
        private readonly List<BeatmapsetContainer> _containers = new();
        private readonly List<Beatmapset> BeatmapsToAdd = new();
        private FillFlowContainer _fillFlowContainer;
        private TwitchClientLocal _localTwitchClient;

        private OsuClient localOsuClient;

        private void AddAllBeatmapsets()
        {
            foreach (var beatmapset in BeatmapsToAdd) AddBeatmapset(beatmapset);
            BeatmapsToAdd.Clear();
        }

        private void AddBeatmapset(Beatmapset beatmapset)
        {
            var beatmapsetContainer = new BeatmapsetContainer(beatmapset);
            _containers.Add(beatmapsetContainer);
            _fillFlowContainer.Add(beatmapsetContainer);
        }

        private void HandleTwitchMessage(ChatMessage message)
        {
            if (message.Message.StartsWith("!rq"))
            {
                var beatmapId = message.Message.Split(" ")[1];
                LoadBeatmap(beatmapId).ConfigureAwait(false);
            }
        }

        private async Task LoadBeatmap(string beatmapId)
        {
            try
            {
                Logger.Log($"Requesting beatmap using Id {beatmapId}");
                var beatmap = await localOsuClient.GetBeatmapAsync(beatmapId);
                var beatmapset = await beatmap.GetBeatmapsetAsync();
                Logger.Log($"Successfully loaded beatmapset from beatmap Id {beatmapId}");
                BeatmapsToAdd.Add(beatmapset);
                Scheduler.AddOnce(AddAllBeatmapsets);
            }
            catch (HttpRequestException e)
            {
                Logger.Log($"Unavailable beatmap using Id {beatmapId}", LoggingTarget.Runtime, LogLevel.Error);
            }
        }

        [BackgroundDependencyLoader]
        private void Load(TwitchClientLocal twitchClient, OsuClient osuClient)
        {
            _localTwitchClient = twitchClient;
            _localTwitchClient.OnChatMessage += HandleTwitchMessage;
            localOsuClient = osuClient;
            Children = new Drawable[]
            {
                new Box
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(1.0f),
                    Colour = Color4.Aqua
                },
                new BasicScrollContainer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(1.0f),
                    ClampExtension = 20.0f,
                    Padding = new MarginPadding(20.0f),
                    ScrollbarVisible = false,
                    Child = _fillFlowContainer = new FillFlowContainer
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                        Direction = FillDirection.Vertical
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