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
        private Sprite _background;
        private readonly Bindable<Beatmapset> _beatmapset;
        private TextFlowContainer _beatmapsetCreator;
        private TextFlowContainer _beatmapsetTitle;
        private TextureStore _textureStore;

        public Action<HoverEvent> OnHoverAction;
        public Action<HoverLostEvent> OnHoverLostAction;

        public BeatmapsetContainerForeground(Bindable<Beatmapset> beatmapset)
        {
            _beatmapset = beatmapset;
        }

        [BackgroundDependencyLoader]
        private void Load(TextureStore textureStore)
        {
            _textureStore = textureStore;

            InitSelf();
            InitChildren();
            AddDetails();
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
            CornerRadius = 50;
            Masking = true;
        }

        private void InitChildren()
        {
            Children = new Drawable[]
            {
                _background = new Sprite
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(1.0f),
                    RelativeSizeAxes = Axes.Both,
                    Colour = new Color4(0.75f, 0.75f, 0.75f, 1.0f)
                },
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.9f),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    CornerRadius = 50.0f * 0.9f,
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
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            TextAnchor = Anchor.BottomCentre,
                            RelativeSizeAxes = Axes.Both,
                            Size = new Vector2(1.0f, 0.5f),
                            Padding = new MarginPadding(10)
                        },
                        _beatmapsetCreator = new TextFlowContainer
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
        }

        private void AddDetails()
        {
            var backgroundTexture = _textureStore.Get(_beatmapset.Value.Covers.CardAt2X);
            _background.Texture = backgroundTexture;

            _beatmapsetTitle.AddText(_beatmapset.Value.Title,
                t =>
                {
                    t.Font = new FontUsage("Roboto", weight: "Regular", size: 30);
                    t.Shadow = true;
                    t.ShadowColour = new Color4(0, 0, 0, 0.75f);
                    t.ShadowOffset = new Vector2(0.05f);
                });

            _beatmapsetCreator.AddText($"Mapped by {_beatmapset.Value.Creator}",
                t =>
                {
                    t.Font = new FontUsage("Roboto", weight: "Regular", size: 25);
                    t.Shadow = true;
                    t.ShadowColour = new Color4(0, 0, 0, 0.75f);
                    t.ShadowOffset = new Vector2(0.05f);
                });
        }
    }
}