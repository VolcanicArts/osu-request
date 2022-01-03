using System;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.Color4Extensions;
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
                new BackgroundSprite(_backgroundTexture)
                {
                    Colour = Color4.White,
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
                        new BackgroundContainer(Color4.Gray.Opacity(0.5f)),
                        new Container()
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            Size = new Vector2(1.0f, 0.2f),
                            RelativeAnchorPosition = new Vector2(0.5f, 0.3f),
                            Child = new AutoSizingSpriteText
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                Text = { Value = _beatmapset.Value.Title },
                                Font = new FontUsage("Roboto", weight: "Regular"),
                                Shadow = true
                            }
                        },
                        new Container()
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            Size = new Vector2(1.0f, 0.2f),
                            RelativeAnchorPosition = new Vector2(0.5f, 0.7f),
                            Child = new AutoSizingSpriteText
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                Text = { Value = _beatmapset.Value.Creator },
                                Font = new FontUsage("Roboto", weight: "Regular"),
                                Shadow = true
                            }
                        },
                    }
                }
            };
        }
    }
}