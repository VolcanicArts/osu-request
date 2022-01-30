using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu_request.Structures;

namespace osu_request.Drawables;

public class BeatmapsetRequestCard : BeatmapsetCard
{
    private readonly RequestedBeatmapset RequestedBeatmapset;

    public BeatmapsetRequestCard(WorkingBeatmapset workingBeatmapset, RequestedBeatmapset requestedBeatmapset) : base(workingBeatmapset)
    {
        RequestedBeatmapset = requestedBeatmapset;
    }

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
            Text = $"Requested by {RequestedBeatmapset.Requester.DisplayName}"
        });
    }
}