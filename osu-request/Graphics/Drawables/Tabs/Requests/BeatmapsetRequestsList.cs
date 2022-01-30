using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Textures;
using osu_request.Structures;
using osu_request.Websocket;
using osuTK;
using volcanicarts.osu.NET.Structures;
using User = TwitchLib.Api.Helix.Models.Users.GetUsers.User;

namespace osu_request.Drawables;

public class BeatmapsetRequestsList : Container
{
    private FillFlowContainer<BeatmapsetRequestEntry> _fillFlowContainer;

    [Resolved]
    private AudioManager AudioManager { get; set; }

    [Resolved]
    private TextureStore TextureStore { get; set; }

    private void NewRequest(RequestedBeatmapset requestedBeatmapset)
    {
        var previewMp3 = AudioManager.GetTrackStore().Get(requestedBeatmapset.Beatmapset.PreviewUrl);
        var backgroundTexture = TextureStore.Get(requestedBeatmapset.Beatmapset.Covers.CardAt2X);
        var workingBeatmapset = new WorkingBeatmapset(requestedBeatmapset.Beatmapset, backgroundTexture, previewMp3);
        var beatmapsetContainer = new BeatmapsetRequestEntry(workingBeatmapset, requestedBeatmapset)
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

    private void OnUserBan(User user)
    {
        _fillFlowContainer.Where(entry => entry.Username == user.Login).ForEach(entry => entry.DisposeGracefully());
    }

    [BackgroundDependencyLoader]
    private void Load(WebSocketClient webSocketClient)
    {
        webSocketClient.OnNewRequest += requestedBeatmapset => Scheduler.Add(() => NewRequest(requestedBeatmapset));
        webSocketClient.OnBeatmapsetBan += beatmapset => Scheduler.Add(() => OnBeatmapsetBan(beatmapset));
        webSocketClient.OnUserBan += user => Scheduler.Add(() => OnUserBan(user));

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