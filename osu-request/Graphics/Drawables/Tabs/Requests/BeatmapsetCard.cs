using osu.Framework.Allocation;
using osu.Framework.Audio.Track;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Events;
using osuTK;
using volcanicarts.osu.NET.Structures;

namespace osu_request.Drawables
{
    public class BeatmapsetCard : Container
    {
        public readonly string BeatmapsetId;
        private readonly Beatmapset Beatmapset;
        private readonly Texture CoverTexture;
        private readonly Track PreviewMp3;

        public BeatmapsetCard(Beatmapset beatmapset, Texture coverTexture, Track previewMp3)
        {
            BeatmapsetId = beatmapset.Id.ToString();
            Beatmapset = beatmapset;
            CoverTexture = coverTexture;
            PreviewMp3 = previewMp3;

            PreviewMp3.Volume.Value = 0.25f;
            PreviewMp3.Completed += PreviewMp3.Restart;
        }

        [BackgroundDependencyLoader]
        private void Load()
        {
            TextFlowContainer TopText;

            Masking = true;
            CornerRadius = 10;
            EdgeEffect = OsuRequestEdgeEffects.NoShadow;

            Children = new Drawable[]
            {
                new BeatmapsetCoverContainer(CoverTexture),
                TopText = new TextFlowContainer
                {
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(1.0f),
                    Padding = new MarginPadding(5)
                }
            };

            TopText.AddText($"{Beatmapset.Title}\n", t => t.Font = OsuRequestFonts.Regular.With(size: 30));
            TopText.AddText($"Mapped by {Beatmapset.Creator}", t => t.Font = OsuRequestFonts.Regular.With(size: 25));
        }

        protected override bool OnHover(HoverEvent e)
        {
            PreviewMp3.Restart();
            this.MoveToY(-1.5f, 100, Easing.OutCubic);
            TweenEdgeEffectTo(OsuRequestEdgeEffects.BasicShadow, 100, Easing.OutCubic);
            return true;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            PreviewMp3.Stop();
            this.MoveToY(0.0f, 100, Easing.InCubic);
            TweenEdgeEffectTo(OsuRequestEdgeEffects.NoShadow, 100, Easing.InCubic);
        }

        protected override void Dispose(bool isDisposing)
        {
            PreviewMp3.Dispose();
            base.Dispose(isDisposing);
        }
    }
}