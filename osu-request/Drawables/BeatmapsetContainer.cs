using System;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Audio.Track;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Graphics;
using volcanicarts.osu.NET.Structures;

namespace osu_request.Drawables
{
    public class BeatmapsetContainer : Container
    {
        private const int CORNER_RADIUS = 50;
        private readonly Beatmapset _beatmapset;
        private Track _currentTrack;

        private Sprite background;
        private TextFlowContainer beatmapCreator;
        private TextFlowContainer beatmapTitle;

        public Action<BeatmapsetContainer> ContainerClicked;
        private Container contentContainer;

        public BeatmapsetContainer(Beatmapset beatmapset)
        {
            _beatmapset = beatmapset;
        }

        protected override bool OnHover(HoverEvent e)
        {
            this.ScaleTo(1.1f, 300, Easing.OutBounce);
            _currentTrack?.Restart();
            return true;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            this.ScaleTo(1.0f, 300, Easing.OutBounce);
            _currentTrack?.Stop();
            base.OnHoverLost(e);
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            this.ScaleTo(0.9f, 200, Easing.InOutQuart);
            return true;
        }

        protected override void OnMouseUp(MouseUpEvent e)
        {
            this.ScaleTo(1.1f, 200, Easing.InOutQuart);
            base.OnMouseUp(e);
        }

        protected override bool OnClick(ClickEvent e)
        {
            ContainerClicked.Invoke(this);
            return true;
        }

        protected override void UpdateAfterAutoSize()
        {
            Size = new Vector2(Size.X, DrawWidth * 0.35f);
            base.UpdateAfterAutoSize();
        }

        [BackgroundDependencyLoader]
        private void Load(TextureStore textureStore, AudioManager audioManager)
        {
            if (_beatmapset == null) return;

            var backgroundTexture = textureStore.Get(_beatmapset.Covers.CardAt2X);

            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Size = new Vector2(500);
            BorderColour = Color4.Black;
            BorderThickness = 5;
            CornerRadius = CORNER_RADIUS;
            Masking = true;
            Margin = new MarginPadding(10.0f);

            Children = new Drawable[]
            {
                background = new Sprite
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Texture = backgroundTexture,
                    Size = new Vector2(1.0f),
                    RelativeSizeAxes = Axes.Both,
                    Colour = new Color4(0.75f, 0.75f, 0.75f, 1.0f)
                },
                contentContainer = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.9f),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    CornerRadius = CORNER_RADIUS * 0.9f,
                    BorderColour = Color4.Black,
                    BorderThickness = 3,
                    Masking = true,

                    Children = new Drawable[]
                    {
                        new Box
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            Colour = new Color4(0.5f, 0.5f, 0.5f, 0.5f)
                        },
                        beatmapTitle = new TextFlowContainer
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            TextAnchor = Anchor.BottomCentre,
                            RelativeSizeAxes = Axes.Both,
                            Size = new Vector2(1.0f, 0.5f),
                            Padding = new MarginPadding(10)
                        },
                        beatmapCreator = new TextFlowContainer
                        {
                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.BottomCentre,
                            TextAnchor = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            Size = new Vector2(1.0f, 0.5f),
                            Padding = new MarginPadding(10)
                        }
                    }
                }
            };
        
            beatmapTitle.AddText(_beatmapset.Title,
                t =>
                {
                    t.Font = new FontUsage("Roboto", weight: "Regular", size: 30);
                    t.Shadow = true;
                    t.ShadowColour = new Color4(0, 0, 0, 0.75f);
                    t.ShadowOffset = new Vector2(0.05f);
                });

            beatmapCreator.AddText($"Mapped by {_beatmapset.Creator}",
                t =>
                {
                    t.Font = new FontUsage("Roboto", weight: "Regular", size: 25);
                    t.Shadow = true;
                    t.ShadowColour = new Color4(0, 0, 0, 0.75f);
                    t.ShadowOffset = new Vector2(0.05f);
                });

            _currentTrack = audioManager.GetTrackStore().Get(_beatmapset.PreviewUrl);
            _currentTrack.Volume.Value = .5;
            _currentTrack.Completed += _currentTrack.Restart;
        }

        protected override void Dispose(bool isDisposing)
        {
            _currentTrack?.Dispose();
            base.Dispose(isDisposing);
        }
    }
}