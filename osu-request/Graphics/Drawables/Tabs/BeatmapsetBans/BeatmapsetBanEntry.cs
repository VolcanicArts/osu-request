using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu_request.Structures;
using osuTK;

namespace osu_request.Drawables.Bans;

public class BeatmapsetBanEntry : Container
{
    public readonly string BeatmapsetId;

    public BeatmapsetBanEntry(WorkingBeatmapset workingBeatmapset)
    {
        WorkingBeatmapset.Value = workingBeatmapset;
        BeatmapsetId = workingBeatmapset.Beatmapset.Id.ToString();
    }

    [Cached]
    private Bindable<WorkingBeatmapset> WorkingBeatmapset { get; set; } = new();

    protected override void LoadComplete()
    {
        base.LoadComplete();
        this.FadeInFromZero(1000, Easing.InQuad);
    }

    [BackgroundDependencyLoader]
    private void Load()
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
                Size = new Vector2(0.9f, 1.0f),
                Padding = new MarginPadding
                {
                    Left = 5,
                    Right = 2.5f,
                    Top = 5,
                    Bottom = 5
                },
                Child = new BeatmapsetCard<WorkingBeatmapset>
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
                Size = new Vector2(0.1f, 1.0f),
                Padding = new MarginPadding
                {
                    Left = 2.5f,
                    Right = 5,
                    Top = 5,
                    Bottom = 5
                },
                Child = new BeatmapsetBanButtons
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both
                }
            }
        };
    }

    protected internal void DisposeGracefully()
    {
        this.FadeOutFromOne(500, Easing.OutQuad).Finally(t => t.RemoveAndDisposeImmediately());
    }
}