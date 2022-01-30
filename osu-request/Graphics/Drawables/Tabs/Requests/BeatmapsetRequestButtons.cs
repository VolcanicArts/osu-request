using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Textures;
using osu.Framework.Platform;
using osu_request.Structures;
using osu_request.Websocket;
using osuTK;

namespace osu_request.Drawables;

public class BeatmapsetRequestButtons : Container
{
    private readonly RequestedBeatmapset RequestedBeatmapset;

    public BeatmapsetRequestButtons(RequestedBeatmapset requestedBeatmapset)
    {
        RequestedBeatmapset = requestedBeatmapset;
    }

    [Resolved]
    private BindableBool ShouldDispose { get; set; }

    [BackgroundDependencyLoader]
    private void Load(TextureStore textureStore, GameHost host, WebSocketClient webSocketClient)
    {
        Masking = true;
        CornerRadius = 10;

        Children = new Drawable[]
        {
            new Box
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                Colour = OsuRequestColour.Gray2
            },
            new Container
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                Padding = new MarginPadding(3),
                Children = new Drawable[]
                {
                    new Container
                    {
                        Anchor = Anchor.TopRight,
                        Origin = Anchor.TopRight,
                        RelativeSizeAxes = Axes.Both,
                        Size = new Vector2(0.5f),
                        Padding = new MarginPadding(2),
                        Child = new SpriteButton
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            CornerRadius = 5,
                            BackgroundColour = OsuRequestColour.BlueDark,
                            Texture = textureStore.Get("open-externally"),
                            OnButtonClicked = () => host.OpenUrlExternally($"https://osu.ppy.sh/beatmapsets/{RequestedBeatmapset.Beatmapset.Id}")
                        }
                    },
                    new Container
                    {
                        Anchor = Anchor.TopLeft,
                        Origin = Anchor.TopLeft,
                        RelativeSizeAxes = Axes.Both,
                        Size = new Vector2(0.5f),
                        Padding = new MarginPadding(2),
                        Child = new SpriteButton
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            CornerRadius = 5,
                            BackgroundColour = OsuRequestColour.BlueDark,
                            Texture = textureStore.Get("download"),
                            OnButtonClicked = () => host.OpenUrlExternally($"osu://b/{RequestedBeatmapset.Beatmapset.Id}")
                        }
                    },
                    new Container
                    {
                        Anchor = Anchor.BottomLeft,
                        Origin = Anchor.BottomLeft,
                        RelativeSizeAxes = Axes.Both,
                        Size = new Vector2(0.5f),
                        Padding = new MarginPadding(2),
                        Child = new SpriteButton
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            CornerRadius = 5,
                            BackgroundColour = OsuRequestColour.RedDark,
                            Texture = textureStore.Get("ban"),
                            OnButtonClicked = () => webSocketClient.BanBeatmapset(RequestedBeatmapset.Beatmapset.Id.ToString())
                        }
                    },
                    new Container
                    {
                        Anchor = Anchor.BottomRight,
                        Origin = Anchor.BottomRight,
                        RelativeSizeAxes = Axes.Both,
                        Size = new Vector2(0.5f),
                        Padding = new MarginPadding(2),
                        Child = new SpriteButton
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            CornerRadius = 5,
                            BackgroundColour = OsuRequestColour.RedDark,
                            Texture = textureStore.Get("ban-user"),
                            OnButtonClicked = () => webSocketClient.BanUser(RequestedBeatmapset.Requester.Login)
                        }
                    }
                }
            }
        };
    }
}