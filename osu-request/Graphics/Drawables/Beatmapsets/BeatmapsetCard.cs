using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;
using osu_request.Structures;

namespace osu_request.Drawables;

public class BeatmapsetCard<T> : Container where T : WorkingBeatmapset
{
    private double hoverTime = double.MaxValue;

    [Resolved]
    private Bindable<T> WorkingBeatmapset { get; set; }

    [BackgroundDependencyLoader]
    private void Load()
    {
        TextFlowContainer TopText;

        Masking = true;
        CornerRadius = 10;
        EdgeEffect = OsuRequestEdgeEffects.NoShadow;

        Children = new Drawable[]
        {
            new BeatmapsetCover(WorkingBeatmapset.Value.Cover)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both
            },
            TopText = new TextFlowContainer
            {
                Anchor = Anchor.TopLeft,
                Origin = Anchor.TopLeft,
                RelativeSizeAxes = Axes.Both,
                Padding = new MarginPadding(5)
            }
        };

        TopText.AddText($"{WorkingBeatmapset.Value.Beatmapset.Title}\n", t => t.Font = OsuRequestFonts.Regular.With(size: 30));
        TopText.AddText($"Mapped by {WorkingBeatmapset.Value.Beatmapset.Creator}", t => t.Font = OsuRequestFonts.Regular.With(size: 25));
    }

    protected override void Update()
    {
        base.Update();
        if (!IsHovered || !(Time.Current - hoverTime > 250.0d)) return;
        hoverTime = double.MaxValue;
        WorkingBeatmapset.Value.Preview.Restart();
    }

    protected override bool OnHover(HoverEvent e)
    {
        hoverTime = Time.Current;
        this.MoveToY(-1.5f, 250, Easing.OutCubic);
        TweenEdgeEffectTo(OsuRequestEdgeEffects.BasicShadow, 250, Easing.OutCubic);
        return true;
    }

    protected override void OnHoverLost(HoverLostEvent e)
    {
        WorkingBeatmapset.Value.Preview.Stop();
        this.MoveToY(0.0f, 100, Easing.InCubic);
        TweenEdgeEffectTo(OsuRequestEdgeEffects.NoShadow, 100, Easing.InCubic);
    }

    protected override void Dispose(bool isDisposing)
    {
        WorkingBeatmapset.Value.Preview.Dispose();
        base.Dispose(isDisposing);
    }
}