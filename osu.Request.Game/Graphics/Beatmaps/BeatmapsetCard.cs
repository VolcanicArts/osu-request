// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;
using osu.Request.Game.Beatmaps;

namespace osu.Request.Game.Graphics.Beatmaps;

public class BeatmapsetCard : Container
{
    private double hoverTime = double.MaxValue;
    private const double hover_duration = 250.0d;

    [Resolved]
    private WorkingBeatmapset WorkingBeatmapset { get; set; }

    [BackgroundDependencyLoader]
    private void load()
    {
        TextFlowContainer topText;

        Masking = true;
        EdgeEffect = OsuRequestEdgeEffects.NO_SHADOW;

        Children = new Drawable[]
        {
            new BeatmapsetCover()
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both
            },
            topText = new TextFlowContainer
            {
                Anchor = Anchor.TopLeft,
                Origin = Anchor.TopLeft,
                RelativeSizeAxes = Axes.Both,
                Padding = new MarginPadding(5)
            }
        };

        topText.AddText($"{WorkingBeatmapset.Title}\n", t => t.Font = OsuRequestFonts.REGULAR.With(size: 30));
        topText.AddText($"Mapped by {WorkingBeatmapset.Creator}", t => t.Font = OsuRequestFonts.REGULAR.With(size: 25));
    }

    protected override void Update()
    {
        base.Update();
        if (!IsHovered || !(Time.Current - hoverTime > hover_duration)) return;

        hoverTime = double.MaxValue;
        WorkingBeatmapset.PreviewAudio.Restart();
    }

    protected override bool OnHover(HoverEvent e)
    {
        hoverTime = Time.Current;
        this.MoveToY(-1.5f, 250, Easing.OutCubic);
        TweenEdgeEffectTo(OsuRequestEdgeEffects.BASIC_SHADOW, 250, Easing.OutCubic);
        return true;
    }

    protected override void OnHoverLost(HoverLostEvent e)
    {
        WorkingBeatmapset.PreviewAudio.Stop();
        this.MoveToY(0.0f, 100, Easing.InCubic);
        TweenEdgeEffectTo(OsuRequestEdgeEffects.NO_SHADOW, 100, Easing.InCubic);
    }
}
