using System.Globalization;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;
using osuTK.Graphics;
using volcanicarts.osu.NET.Structures;

namespace osu_request.Drawables
{
    public class BeatmapsetBackgroundContainer : Container
    {
        private readonly Bindable<Beatmapset> _beatmapset;
        private readonly Bindable<float> _globalCornerRadius;

        public BeatmapsetBackgroundContainer(Bindable<Beatmapset> beatmapset, Bindable<float> GlobalCornerRadius)
        {
            GlobalCornerRadius.ValueChanged += UpdateSizing;
            _beatmapset = beatmapset;
            _globalCornerRadius = GlobalCornerRadius;
        }

        private void UpdateSizing(ValueChangedEvent<float> e)
        {
            CornerRadius = e.NewValue;
        }

        [BackgroundDependencyLoader]
        private void Load()
        {
            InitSelf();
            InitChildren();
        }

        private void InitSelf()
        {
            Anchor = Anchor.CentreLeft;
            Origin = Anchor.CentreLeft;
            RelativeSizeAxes = Axes.Both;
            CornerRadius = _globalCornerRadius.Value;
            Masking = true;
            BorderThickness = 5;
            BorderColour = Color4.Black;
        }

        private void InitChildren()
        {
            Children = new Drawable[]
            {
                new BackgroundContainer(Color4.Gray.Lighten(0.25f)),
                new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.5f, 1.0f),
                    RelativeAnchorPosition = new Vector2(0.75f, 0.5f),
                    Children = new Drawable[]
                    {
                        new Container
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            RelativeAnchorPosition = new Vector2(0.5f, 0.25f),
                            Size = new Vector2(1.0f, 0.25f),
                            Children = new Drawable[]
                            {
                                new AutoSizingSpriteText
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.CentreRight,
                                    SpriteAnchor = Anchor.CentreRight,
                                    SpriteOrigin = Anchor.CentreRight,
                                    RelativeSizeAxes = Axes.Both,
                                    Text = { Value = "Rank Status:" },
                                    Font = new FontUsage("Roboto", weight: "Regular"),
                                    Shadow = true,
                                    Margin = new MarginPadding
                                    {
                                        Right = 2.5f
                                    },
                                },
                                new AutoSizingSpriteText
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.CentreLeft,
                                    SpriteAnchor = Anchor.CentreLeft,
                                    SpriteOrigin = Anchor.CentreLeft,
                                    RelativeSizeAxes = Axes.Both,
                                    Text = { Value = _beatmapset.Value.Ranked.ToString() },
                                    Font = new FontUsage("Roboto", weight: "Regular"),
                                    Shadow = true,
                                    Margin = new MarginPadding
                                    {
                                        Left = 2.5f
                                    },
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}