using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu_request.Structures;
using osu_request.Websocket.Structures;
using TwitchLib.Client.Models;

namespace osu_request.Drawables
{
    public class BeatmapsetRequestCard : BeatmapsetCard
    {
        private readonly RequestArgs RequestArgs;

        public BeatmapsetRequestCard(WorkingBeatmapset workingBeatmapset, RequestArgs requestArgs) : base(workingBeatmapset)
        {
            RequestArgs = requestArgs;
        }

        [BackgroundDependencyLoader]
        private void Load()
        {
            var RequestText = new TextFlowContainer
            {
                Anchor = Anchor.BottomLeft,
                Origin = Anchor.BottomLeft,
                TextAnchor = Anchor.BottomLeft,
                RelativeSizeAxes = Axes.Both,
                Padding = new MarginPadding(5)
            };
            RequestText.AddText($"Requested by {RequestArgs.Requester.DisplayName}", t => t.Font = OsuRequestFonts.Regular.With(size: 25));
            AddInternal(RequestText);
        }
    }
}