using osu.Framework.Allocation;
using osu.Framework.Audio.Track;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Textures;
using osuTK;
using volcanicarts.osu.NET.Structures;

namespace osu_request.Drawables.Bans
{
    public class BeatmapsetBanEntry : Container
    {
        public readonly string BeatmapsetId;
        private readonly Beatmapset Beatmapset;
        private readonly Texture CoverTexture;
        private readonly Track PreviewMp3;

        public BeatmapsetBanEntry(Beatmapset beatmapset, Texture coverTexture, Track previewMp3)
        {
            BeatmapsetId = beatmapset.Id.ToString();
            Beatmapset = beatmapset;
            CoverTexture = coverTexture;
            PreviewMp3 = previewMp3;

            PreviewMp3.Volume.Value = 0.25f;
            PreviewMp3.Completed += PreviewMp3.Restart;
        }

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
                    Child = new BeatmapsetCard(Beatmapset, CoverTexture, PreviewMp3)
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
                    Child = new BeatmapsetBanButtons(Beatmapset)
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
}