using osu.Framework.Allocation;
using osu.Framework.Audio.Track;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Textures;
using TwitchLib.Client.Models;
using volcanicarts.osu.NET.Structures;

namespace osu_request.Drawables
{
    public class BeatmapsetRequestCard : BeatmapsetCard
    {
        private readonly ChatMessage Message;

        public BeatmapsetRequestCard(Beatmapset beatmapset, Texture coverTexture, Track previewMp3, ChatMessage message) : base(beatmapset,
            coverTexture, previewMp3)
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