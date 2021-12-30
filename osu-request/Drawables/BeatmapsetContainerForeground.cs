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
        private TextFlowContainer _beatmapsetCreator;
        private TextFlowContainer _beatmapsetTitle;
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
            AddDetails();
        }

        private void UpdateSizing(ValueChangedEvent<float> e)
        {
            CornerRadius = e.NewValue;
            _detailsContainer.CornerRadius = e.NewValue * 0.9f;
            _beatmapsetTitle.ScaleTo(e.NewValue * 0.05f);
            _beatmapsetCreator.ScaleTo(e.NewValue * 0.04f);
        }

        protected override bool OnHover(HoverEvent e)
        {
            OnHoverAction?.Invoke(e);
            return true;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            OnHoverLostAction.Invoke(e);
            base.OnHoverLost(e);
        }

        private void InitSelf()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;
            Size = new Vector2(1f);
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
                        new Box
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            Colour = new Color4(0.5f, 0.5f, 0.5f, 0.5f)
                        },
                        _beatmapsetTitle = new TextFlowContainer
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            TextAnchor = Anchor.Centre,
                            RelativeAnchorPosition = new Vector2(0.5f, 0.3f),
                            RelativeSizeAxes = Axes.Both,
                            Size = new Vector2(1.0f, 0.5f),
                        },
                        _beatmapsetCreator = new TextFlowContainer
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            TextAnchor = Anchor.Centre,
                            RelativeAnchorPosition = new Vector2(0.5f, 0.7f),
                            RelativeSizeAxes = Axes.Both,
                            Size = new Vector2(1.0f, 0.5f),
                        }
                    }
                }
            };
        }

        private void AddDetails()
        {
            _beatmapsetTitle.AddText(_beatmapset.Value.Title,
                t =>
                {
                    t.Font = new FontUsage("Roboto", weight: "Regular", size: 10);
                    t.Shadow = true;
                    t.ShadowColour = new Color4(0, 0, 0, 0.75f);
                    t.ShadowOffset = new Vector2(0.05f);
                });

            _beatmapsetCreator.AddText($"Mapped by {_beatmapset.Value.Creator}",
                t =>
                {
                    t.Font = new FontUsage("Roboto", weight: "Regular", size: 10);
                    t.Shadow = true;
                    t.ShadowColour = new Color4(0, 0, 0, 0.75f);
                    t.ShadowOffset = new Vector2(0.05f);
                });
        }
    }
}