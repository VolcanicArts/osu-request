using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;
using osuTK.Graphics;

namespace osu_request.Drawables.Bans
{
    public class BansTab : Container
    {
        [BackgroundDependencyLoader]
        private void Load()
        {
            InitSelf();
            InitChildren();
        }

        private void InitSelf()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;
            Size = new Vector2(1.0f);
            Padding = new MarginPadding(20.0f);
        }

        private void InitChildren()
        {
            Children = new Drawable[]
            {
                new Container
                {
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.49f, 1.0f),
                    Masking = true,
                    CornerRadius = 10,
                    BorderThickness = 2,
                    BorderColour = Color4.Black,
                    EdgeEffect = OsuRequestEdgeEffects.BasicShadow,
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
                            Padding = new MarginPadding(10),
                            Children = new Drawable[]
                            {
                                new Container
                                {
                                    Anchor = Anchor.TopCentre,
                                    Origin = Anchor.TopCentre,
                                    RelativeSizeAxes = Axes.Both,
                                    Size = new Vector2(1.0f, 0.05f),
                                    Child = new AutoSizingSpriteText
                                    {
                                        Anchor = Anchor.TopCentre,
                                        Origin = Anchor.TopCentre,
                                        SpriteAnchor = Anchor.TopCentre,
                                        SpriteOrigin = Anchor.TopCentre,
                                        RelativeSizeAxes = Axes.Both,
                                        AutoSizeSpriteTextAxes = Axes.Both,
                                        Size = new Vector2(0.2f, 0.75f),
                                        Text = { Value = "Beatmap Bans" },
                                        Font = new FontUsage("Roboto", weight: "Regular")
                                    }
                                },
                                new Container
                                {
                                    Anchor = Anchor.BottomCentre,
                                    Origin = Anchor.BottomCentre,
                                    RelativeSizeAxes = Axes.Both,
                                    Size = new Vector2(1.0f, 0.95f),
                                    Children = new Drawable[]
                                    {
                                        new Container
                                        {
                                            Anchor = Anchor.TopCentre,
                                            Origin = Anchor.TopCentre,
                                            RelativeSizeAxes = Axes.Both,
                                            Size = new Vector2(1.0f, 0.09f),
                                            Masking = true,
                                            CornerRadius = 5,
                                            BorderColour = Color4.Black,
                                            BorderThickness = 2,
                                            EdgeEffect = OsuRequestEdgeEffects.BasicShadow,
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
                                                    Padding = new MarginPadding(10),
                                                    Child = new Container
                                                    {
                                                        Anchor = Anchor.TopCentre,
                                                        Origin = Anchor.TopCentre,
                                                        RelativeSizeAxes = Axes.Both,
                                                        Size = new Vector2(1.0f),
                                                        Children = new Drawable[]
                                                        {
                                                            new Container
                                                            {
                                                                Anchor = Anchor.CentreLeft,
                                                                Origin = Anchor.CentreLeft,
                                                                RelativeSizeAxes = Axes.Both,
                                                                Size = new Vector2(0.79f, 1.0f),
                                                                Child = new OsuRequestTextBox
                                                                {
                                                                    Anchor = Anchor.Centre,
                                                                    Origin = Anchor.Centre,
                                                                    PlaceholderText = "Beatmap ID"
                                                                }
                                                            },
                                                            new Container
                                                            {
                                                                Anchor = Anchor.CentreRight,
                                                                Origin = Anchor.CentreRight,
                                                                RelativeSizeAxes = Axes.Both,
                                                                Size = new Vector2(0.19f, 1.0f),
                                                                Child = new OsuRequestButton
                                                                {
                                                                    Anchor = Anchor.BottomRight,
                                                                    Origin = Anchor.BottomRight,
                                                                    RelativeSizeAxes = Axes.Both,
                                                                    Size = new Vector2(1.0f),
                                                                    Text = "Ban",
                                                                    TextSize = new Vector2(0.5f, 0.75f)
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        },
                                        new Container
                                        {
                                            Anchor = Anchor.BottomCentre,
                                            Origin = Anchor.BottomCentre,
                                            RelativeSizeAxes = Axes.Both,
                                            Size = new Vector2(1.0f, 0.89f),
                                            Masking = true,
                                            CornerRadius = 5,
                                            BorderColour = Color4.Black,
                                            BorderThickness = 2,
                                            EdgeEffect = OsuRequestEdgeEffects.BasicShadow,
                                            Children = new Drawable[]
                                            {
                                                new BackgroundColour
                                                {
                                                    Colour = OsuRequestColour.GreyLimeDarker
                                                },
                                                new BasicScrollContainer
                                                {
                                                    Anchor = Anchor.Centre,
                                                    Origin = Anchor.Centre,
                                                    RelativeSizeAxes = Axes.Both,
                                                    ClampExtension = 20.0f,
                                                    ScrollbarVisible = false,
                                                    Child = new FillFlowContainer
                                                    {
                                                        Anchor = Anchor.Centre,
                                                        Origin = Anchor.Centre,
                                                        RelativeSizeAxes = Axes.X,
                                                        AutoSizeAxes = Axes.Y,
                                                        Direction = FillDirection.Vertical,
                                                        Spacing = new Vector2(10),
                                                        Padding = new MarginPadding(10.0f)
                                                    }
                                                }
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