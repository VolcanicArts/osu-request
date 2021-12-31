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
        private TextFlowContainer _playCount;

        private TextFlowContainer _ranked;

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
            InitContent();
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
                new Box
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.Gray.Lighten(0.25f)
                },
                new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.5f, 1.0f),
                    RelativeAnchorPosition = new Vector2(0.75f, 0.5f),
                    Padding = new MarginPadding(10.0f),
                    Children = new Drawable[]
                    {
                        _ranked = new TextFlowContainer
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            TextAnchor = Anchor.TopLeft
                        },
                        _playCount = new TextFlowContainer
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            TextAnchor = Anchor.CentreLeft
                        }
                    }
                }
            };
        }

        private void InitContent()
        {
            _ranked.AddText($"Rank Status: {_beatmapset.Value.Ranked}",
                t =>
                {
                    t.Font = new FontUsage("Roboto", weight: "Regular", size: 20);
                    t.Shadow = true;
                    t.ShadowColour = new Color4(0, 0, 0, 0.75f);
                    t.ShadowOffset = new Vector2(0.05f);
                });
            _playCount.AddText($"Play Count: {_beatmapset.Value.PlayCount.ToString("N0", new CultureInfo("en-US"))}",
                t =>
                {
                    t.Font = new FontUsage("Roboto", weight: "Regular", size: 20);
                    t.Shadow = true;
                    t.ShadowColour = new Color4(0, 0, 0, 0.75f);
                    t.ShadowOffset = new Vector2(0.05f);
                });
        }
    }
}