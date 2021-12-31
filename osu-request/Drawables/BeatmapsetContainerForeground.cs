using System;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Graphics;
using volcanicarts.osu.NET.Structures;

namespace osu_request.Drawables
{
    public class BeatmapsetContainerForeground : Container
    {
        private readonly Texture _backgroundTexture;
        private readonly Bindable<Beatmapset> _beatmapset;
        private SpriteText _mapper;
        private SpriteText _title;
        private Container _detailsContainer;

        public Action<HoverEvent> OnHoverAction;
        public Action<HoverLostEvent> OnHoverLostAction;

        public BeatmapsetContainerForeground(Bindable<Beatmapset> beatmapset, Texture backgroundTexture, Bindable<float> GlobalCornerRadius)
        {
            GlobalCornerRadius.ValueChanged += UpdateSizing;
            _beatmapset = beatmapset;
            _backgroundTexture = backgroundTexture;
        }

        [BackgroundDependencyLoader]
        private void Load()
        {
            InitSelf();
            InitChildren();
        }

        private void UpdateSizing(ValueChangedEvent<float> e)
        {
            CornerRadius = e.NewValue;
            _detailsContainer.CornerRadius = e.NewValue * 0.9f;
        }

        protected override void UpdateAfterAutoSize()
        {
            base.UpdateAfterAutoSize();
            _title.Font = new FontUsage("Roboto", weight: "Regular", size: DrawWidth / 15.0f);
            _mapper.Font = new FontUsage("Roboto", weight: "Regular", size: DrawWidth / 20.0f);
        }

        protected override bool OnHover(HoverEvent e)
        {
            this.ScaleTo(1.1f, 1000, Easing.OutElastic);
            OnHoverAction?.Invoke(e);
            return true;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            this.ScaleTo(1.0f, 1000, Easing.OutElastic);
            OnHoverLostAction.Invoke(e);
            base.OnHoverLost(e);
        }

        private void InitSelf()
        {
            Anchor = Anchor.CentreLeft;
            Origin = Anchor.CentreLeft;
            RelativeSizeAxes = Axes.Both;
            Size = new Vector2(0.5f, 1.0f);
            BorderColour = Color4.Black;
            BorderThickness = 5;
            Masking = true;
        }

        private void InitChildren()
        {
            Children = new Drawable[]
            {
                new Sprite
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(1.0f),
                    RelativeSizeAxes = Axes.Both,
                    Colour = new Color4(0.75f, 0.75f, 0.75f, 1.0f),
                    Texture = _backgroundTexture
                },
                _detailsContainer = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.9f),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    BorderColour = Color4.Black,
                    BorderThickness = 3,
                    Masking = true,

                    Children = new Drawable[]
                    {
                        new BackgroundContainer(new Color4(0.5f, 0.5f, 0.5f, 0.5f)),
                        _title = new SpriteText
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            RelativeAnchorPosition = new Vector2(0.5f, 0.2f),
                            Text = _beatmapset.Value.Title,
                            Shadow = true,
                            ShadowColour = new Color4(0, 0, 0, 0.75f),
                            ShadowOffset = new Vector2(0.05f),
                        },
                        _mapper = new SpriteText
                        {
                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.BottomCentre,
                            RelativeAnchorPosition = new Vector2(0.5f, 0.7f),
                            Text = _beatmapset.Value.Creator,
                            Shadow = true,
                            ShadowColour = new Color4(0, 0, 0, 0.75f),
                            ShadowOffset = new Vector2(0.05f),
                        },
                    }
                }
            };
        }
    }
}