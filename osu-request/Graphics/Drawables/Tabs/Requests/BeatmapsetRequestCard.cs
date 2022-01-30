using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu_request.Structures;

namespace osu_request.Drawables;

public class BeatmapsetRequestCard : BeatmapsetCard<WorkingRequestedBeatmapset>
{
    [Resolved]
    private Bindable<WorkingRequestedBeatmapset> WorkingBeatmapset { get; set; }

    [BackgroundDependencyLoader]
    private void Load()
    {
        AddInternal(new TextFlowContainer(t => t.Font = OsuRequestFonts.Regular.With(size: 25))
        {
            Anchor = Anchor.BottomLeft,
            Origin = Anchor.BottomLeft,
            TextAnchor = Anchor.BottomLeft,
            RelativeSizeAxes = Axes.Both,
            Padding = new MarginPadding(5),
            Text = $"Requested by {WorkingBeatmapset.Value.Requester.DisplayName}"
        });
    }
}