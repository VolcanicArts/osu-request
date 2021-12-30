using osu.Framework.Allocation;
using osu.Framework.Audio.Track;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Events;
using osuTK;
using volcanicarts.osu.NET.Structures;

namespace osu_request.Drawables
{
    public class BeatmapsetContainer : Container
    {
        private readonly Texture _backgroundTexture;
        private readonly Bindable<Beatmapset> _beatmapset = new();
        private readonly Track _previewMp3;
        private BeatmapsetContainerForeground _foregroundContainer;
        private BeatmapsetContainerDownload _downloadContainer;
        private BeatmapsetContainerDelete _deleteContainer;
        private readonly Bindable<float> GlobalCornerRadius = new();

        public BeatmapsetContainer(Beatmapset beatmapset, Track previewMp3, Texture backgroundTexture)
        {
            _previewMp3 = previewMp3;
            _backgroundTexture = backgroundTexture;
            _beatmapset.Value = beatmapset;
        }

        private new void OnHover(HoverEvent e)
        {
            this.ScaleTo(0.95f, 300, Easing.OutBounce);
            _previewMp3?.Restart();
        }

        private new void OnHoverLost(HoverLostEvent e)
        {
            this.ScaleTo(0.9f, 300, Easing.OutBounce);
            _previewMp3?.Stop();
            base.OnHoverLost(e);
        }

        protected override void UpdateAfterAutoSize()
        {
            Size = new Vector2(Size.X, DrawWidth * 0.35f);
            UpdateAllCorners();
            base.UpdateAfterAutoSize();
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            this.FadeInFromZero(1000, Easing.InQuad);
        }

        private void UpdateAllCorners()
        {
            var newCornerRadius = DrawWidth / 10.0f;
            GlobalCornerRadius.Value = newCornerRadius;
            _deleteContainer.CornerRadius = newCornerRadius;
            _downloadContainer.CornerRadius = newCornerRadius;
        }

        [BackgroundDependencyLoader]
        private void Load()
        {
            InitSelf();
            InitChildren();
            InitContent();
        }

        private void InitSelf()
        {
            Scale = new Vector2(0.9f);
            Alpha = 0;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.X;
            Margin = new MarginPadding(10.0f);
        }

        private void InitChildren()
        {
            Children = new Drawable[]
            {
                _downloadContainer = new BeatmapsetContainerDownload(),
                _deleteContainer = new BeatmapsetContainerDelete(),
                _foregroundContainer = new BeatmapsetContainerForeground(_beatmapset, _backgroundTexture, GlobalCornerRadius)
            };
        }

        private void InitContent()
        {
            _previewMp3.Volume.Value = .5;
            _previewMp3.Completed += _previewMp3.Restart;
            _foregroundContainer.OnHoverAction += OnHover;
            _foregroundContainer.OnHoverLostAction += OnHoverLost;
        }

        protected override void Dispose(bool isDisposing)
        {
            _previewMp3?.Dispose();
            base.Dispose(isDisposing);
        }
    }
}