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
        private SpriteText _playCount;
        private SpriteText _ranked;

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

        protected override void UpdateAfterAutoSize()
        {
            base.UpdateAfterAutoSize();
            _ranked.Font = new FontUsage("Roboto", weight: "Regular", size: DrawWidth / 30.0f);
            _playCount.Font = new FontUsage("Roboto", weight: "Regular", size: DrawWidth / 30.0f);
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
                    Padding = new MarginPadding(10.0f),
                    Children = new Drawable[]
                    {
                        _ranked = new SpriteText
                        {
                            Anchor = Anchor.TopLeft,
                            Origin = Anchor.TopLeft,
                            Text = $"Rank Status: {_beatmapset.Value.Ranked}",
                            Shadow = true,
                            ShadowColour = new Color4(0, 0, 0, 0.75f),
                            ShadowOffset = new Vector2(0.05f),
                        },
                        _playCount = new SpriteText
                        {
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                            Text = $"Play Count: {_beatmapset.Value.PlayCount.ToString("N0", new CultureInfo("en-US"))}",
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