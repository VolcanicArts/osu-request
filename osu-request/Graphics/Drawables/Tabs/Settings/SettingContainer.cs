using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Platform;
using osu_request.Config;
using osuTK;
using osuTK.Graphics;

namespace osu_request.Drawables
{
    public class SettingContainer : Container
    {
        protected internal OsuRequestTextBox TextBox { get; private set; }
        protected internal string Prompt { get; init; }
        protected internal OsuRequestSetting Setting { get; init; }

        [BackgroundDependencyLoader]
        private void Load(OsuRequestConfig osuRequestConfig, GameHost host)
        {
            host.Window.Resized += () => Scheduler.AddOnce(() => TextBox.RecalculateSize());

            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;
            Size = new Vector2(1.0f, 0.45f);
            Masking = true;
            CornerRadius = 10;
            BorderColour = Color4.Black;
            BorderThickness = 2;
            EdgeEffect = OsuRequestEdgeEffects.BasicShadow;
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
                    Padding = new MarginPadding(10),
                    Children = new Drawable[]
                    {
                        new Container
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            RelativeSizeAxes = Axes.Both,
                            Size = new Vector2(1.0f, 0.5f),
                            Child = new AutoSizingSpriteText
                            {
                                Anchor = Anchor.CentreLeft,
                                Origin = Anchor.CentreLeft,
                                SpriteAnchor = Anchor.CentreLeft,
                                SpriteOrigin = Anchor.CentreLeft,
                                RelativeSizeAxes = Axes.Both,
                                Size = new Vector2(0.075f, 1.0f),
                                AutoSizeSpriteTextAxes = Axes.Both,
                                Text = { Value = Prompt }
                            }
                        },
                        new Container
                        {
                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.BottomCentre,
                            RelativeSizeAxes = Axes.Both,
                            Size = new Vector2(1.0f, 0.5f),
                            Child = TextBox = CreateTextBox(osuRequestConfig.Get<string>(Setting))
                        }
                    }
                }
            };
        }

        protected virtual OsuRequestTextBox CreateTextBox(string text)
        {
            return new OsuRequestTextBox
            {
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                RelativeSizeAxes = Axes.Both,
                Size = new Vector2(1.0f),
                Text = text
            };
        }
    }
}