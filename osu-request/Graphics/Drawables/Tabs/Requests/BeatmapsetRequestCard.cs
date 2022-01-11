using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu_request.Structures;
using TwitchLib.Client.Models;

namespace osu_request.Drawables
{
    public class BeatmapsetRequestCard : BeatmapsetCard
    {
        private readonly ChatMessage Message;

        public BeatmapsetRequestCard(WorkingBeatmapset workingBeatmapset, ChatMessage message) : base(workingBeatmapset)
        {
            Message = message;
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
            RequestText.AddText($"Requested by {Message.DisplayName}", t => t.Font = OsuRequestFonts.Regular.With(size: 25));
            AddInternal(RequestText);
        }
    }
}