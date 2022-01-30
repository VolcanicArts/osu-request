using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Textures;
using osu_request.Structures;
using osuTK;

namespace osu_request.Drawables;

public class BeatmapsetRequestEntry : Container
{
    public readonly string BeatmapsetId;
    private readonly RequestedBeatmapset RequestedBeatmapset;
    public readonly string Username;
    private readonly WorkingBeatmapset WorkingBeatmapset;

    [Cached]
    private BindableBool ShouldDispose = new();

    public BeatmapsetRequestEntry(WorkingBeatmapset workingBeatmapset, RequestedBeatmapset requestedBeatmapset)
    {
        RequestedBeatmapset = requestedBeatmapset;
        WorkingBeatmapset = workingBeatmapset;
        BeatmapsetId = RequestedBeatmapset.Beatmapset.Id.ToString();
        Username = RequestedBeatmapset.Requester.Login;

        ShouldDispose.BindValueChanged(_ => DisposeGracefully());
    }

    protected override void LoadComplete()
    {
        base.LoadComplete();
        this.FadeInFromZero(1000, Easing.InQuad);
    }

    [BackgroundDependencyLoader]
    private void Load(TextureStore textureStore)
    {
        Alpha = 0;
        Masking = true;
        CornerRadius = 10;

        Children = new Drawable[]
        {
            new Box
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                Colour = OsuRequestColour.Gray4
            },
            new Container
            {
                Anchor = Anchor.CentreLeft,
                Origin = Anchor.CentreLeft,
                RelativeSizeAxes = Axes.Both,
                Size = new Vector2(0.05f, 1.0f),
                Padding = new MarginPadding
                {
                    Left = 5,
                    Right = 2.5f,
                    Top = 5,
                    Bottom = 5
                },
                Child = new SpriteButton
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    CornerRadius = 10,
                    BackgroundColour = OsuRequestColour.GreenDark,
                    Texture = textureStore.Get("check"),
                    OnButtonClicked = DisposeGracefully
                }
            },
            new Container
            {
                Anchor = Anchor.CentreRight,
                Origin = Anchor.CentreRight,
                RelativeSizeAxes = Axes.Both,
                Size = new Vector2(0.95f, 1.0f),
                Children = new Drawable[]
                {
                    new Container
                    {
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        RelativeSizeAxes = Axes.Both,
                        Size = new Vector2(0.85f, 1.0f),
                        Padding = new MarginPadding
                        {
                            Left = 2.5f,
                            Right = 2.5f,
                            Top = 5,
                            Bottom = 5
                        },
                        Child = new BeatmapsetRequestCard(WorkingBeatmapset, RequestedBeatmapset)
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both
                        }
                    },
                    new Container
                    {
                        Anchor = Anchor.CentreRight,
                        Origin = Anchor.CentreRight,
                        RelativeSizeAxes = Axes.Both,
                        Size = new Vector2(0.15f, 1.0f),
                        Padding = new MarginPadding
                        {
                            Left = 2.5f,
                            Right = 5,
                            Top = 5,
                            Bottom = 5
                        },
                        Child = new BeatmapsetRequestButtons(RequestedBeatmapset)
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both
                        }
                    }
                }
            }
        };
    }

    public void DisposeGracefully()
    {
        this.FadeOutFromOne(500, Easing.OutQuad).Finally(t => t.RemoveAndDisposeImmediately());
    }
}