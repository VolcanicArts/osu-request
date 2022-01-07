using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Sprites;
using osuTK;
using osuTK.Graphics;
using volcanicarts.osu.NET.Structures;

namespace osu_request.Drawables.Bans
{
    public class BeatmapsetBanEntry : Container
    {
        private readonly Beatmapset _beatmapset;

        public BeatmapsetBanEntry(Beatmapset beatmapset)
        {
            _beatmapset = beatmapset;
        }

        protected override void UpdateAfterAutoSize()
        {
            Size = new Vector2(Size.X, DrawWidth * 0.2f);
            base.UpdateAfterAutoSize();
        }

        protected override void LoadComplete()
        {
            this.FadeInFromZero(1000, Easing.InQuad);
            base.LoadComplete();
        }

        [BackgroundDependencyLoader]
        private void Load()
        {
            InitSelf();
            InitChildren();
        }

        private void InitSelf()
        {
            Alpha = 0;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.X;
            Size = new Vector2(1.0f);
            Masking = true;
            CornerRadius = 10;
            BorderThickness = 2;
            BorderColour = Color4.Black;
            EdgeEffect = OsuRequestEdgeEffects.BasicShadow;
        }

        private void InitChildren()
        {
            Children = new Drawable[]
            {
                new BackgroundColour
                {
                    Colour = OsuRequestColour.GreyLime
                },
                new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(1.0f),
                    Padding = new MarginPadding(5),
                    Children = new Drawable[]
                    {
                        new Container
                        {
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                            RelativeSizeAxes = Axes.Both,
                            Size = new Vector2(1.0f),
                            Masking = true,
                            CornerRadius = 5,
                            Children = new Drawable[]
                            {
                                new BackgroundColour
                                {
                                    Colour = OsuRequestColour.GreyLimeDarker
                                },
                                new Container
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    RelativeSizeAxes = Axes.Both,
                                    Size = new Vector2(1.0f),
                                    Padding = new MarginPadding(2),
                                    Child = new FillFlowContainer
                                    {
                                        Anchor = Anchor.TopLeft,
                                        Origin = Anchor.TopLeft,
                                        RelativeSizeAxes = Axes.Both,
                                        Size = new Vector2(1.0f),
                                        Spacing = new Vector2(0, 2),
                                        Direction = FillDirection.Vertical,
                                        Children = new Drawable[]
                                        {
                                            new AutoSizingSpriteText
                                            {
                                                Anchor = Anchor.TopLeft,
                                                Origin = Anchor.TopLeft,
                                                SpriteAnchor = Anchor.TopLeft,
                                                SpriteOrigin = Anchor.TopLeft,
                                                Size = new Vector2(0.8f, 0.3f),
                                                RelativeSizeAxes = Axes.Both,
                                                Text = { Value = _beatmapset.Title },
                                                Font = new FontUsage("Roboto", weight: "Regular"),
                                            },
                                            new AutoSizingSpriteText
                                            {
                                                Anchor = Anchor.TopLeft,
                                                Origin = Anchor.TopLeft,
                                                SpriteAnchor = Anchor.TopLeft,
                                                SpriteOrigin = Anchor.TopLeft,
                                                RelativeSizeAxes = Axes.Both,
                                                Size = new Vector2(1.0f, 0.25f),
                                                Text = { Value = $"Mapped by {_beatmapset.Creator}" },
                                                Font = new FontUsage("Roboto", weight: "Regular"),
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}