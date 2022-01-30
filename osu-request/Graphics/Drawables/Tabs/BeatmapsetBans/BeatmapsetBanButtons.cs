using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Textures;
using osu_request.Structures;
using osu_request.Websocket;

namespace osu_request.Drawables;

public class BeatmapsetBanButtons : Container
{
    [Resolved]
    private Bindable<WorkingBeatmapset> WorkingBeatmapset { get; set; }

    [BackgroundDependencyLoader]
    private void Load(TextureStore textureStore, WebSocketClient webSocketClient)
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
                Padding = new MarginPadding(5),
                Child = new SpriteButton
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    CornerRadius = 5,
                    BackgroundColour = OsuRequestColour.BlueDark,
                    Texture = textureStore.Get("undo"),
                    OnButtonClicked = () => webSocketClient.UnBanBeatmapset(WorkingBeatmapset.Value.Beatmapset.Id.ToString())
                }
            }
        };
    }
}