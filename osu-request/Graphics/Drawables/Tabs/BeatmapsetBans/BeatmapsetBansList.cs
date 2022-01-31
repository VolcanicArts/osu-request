using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Textures;
using osu.Framework.Platform;
using osu_request.Structures;
using osu_request.Websocket;
using osuTK;
using volcanicarts.osu.NET.Structures;

namespace osu_request.Drawables.Bans;

public class BeatmapsetBansList : Container
{
    private FillFlowContainer<BeatmapsetBanEntry> _fillFlowContainer;

    [Resolved]
    private AudioManager AudioManager { get; set; }

    [Resolved]
    private TextureStore TextureStore { get; set; }

    private void OnBeatmapsetBan(Beatmapset beatmapset)
    {
        var previewMp3 = AudioManager.GetTrackStore().Get(beatmapset.PreviewUrl);
        var backgroundTexture = TextureStore.Get(beatmapset.Covers.CardAt2X);
        if (previewMp3 == null || backgroundTexture == null) return;

        var beatmapsetBan = new BeatmapsetBanEntry(new WorkingBeatmapset(beatmapset, backgroundTexture, previewMp3))
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            RelativeSizeAxes = Axes.X,
            Size = new Vector2(1.0f, 120.0f),
            Scale = new Vector2(0.49f)
        };

        _fillFlowContainer.Add(beatmapsetBan);
    }

    [BackgroundDependencyLoader]
    private void Load(GameHost host, WebSocketClient webSocketClient)
    {
        host.Window.Resized += () => UpdateSizing(host.Window);
        webSocketClient.OnBeatmapsetBan += beatmapset => Scheduler.Add(() => OnBeatmapsetBan(beatmapset));
        webSocketClient.OnBeatmapsetUnBan += beatmapsetId => Scheduler.Add(() => OnBeatmapsetUnBan(beatmapsetId));
        InitChildren();
    }

    private void OnBeatmapsetUnBan(string beatmapsetId)
    {
        _fillFlowContainer.Where(entry => entry.BeatmapsetId == beatmapsetId).ForEach(entry => entry.DisposeGracefully());
    }

    private void UpdateSizing(IWindow window)
    {
        var width = window.ClientSize.Width;
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