using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu_request.Drawables;
using osuTK;

namespace osu_request.Graphics.Drawables.Tabs.BeatmapsetBans
{
    public class UserBansTextBox : Container
    {
        private OsuRequestButton BanButton;
        private OsuRequestTextBox TextBox;

        [BackgroundDependencyLoader]
        private void Load()
        {
            Masking = true;
            CornerRadius = 10;
            EdgeEffect = OsuRequestEdgeEffects.BasicShadow;
            Children = new Drawable[]
            {
                new Box
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Colour = OsuRequestColour.Gray2
                },
                new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding(10),
                    Child = new Container
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        RelativeSizeAxes = Axes.Both,
                        Children = new Drawable[]
                        {
                            new Container
                            {
                                Anchor = Anchor.CentreLeft,
                                Origin = Anchor.CentreLeft,
                                RelativeSizeAxes = Axes.Both,
                                Size = new Vector2(0.795f, 1.0f),
                                Child = TextBox = new OsuRequestTextBox
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    RelativeSizeAxes = Axes.Both,
                                    PlaceholderText = "Username",
                                    CornerRadius = 10
                                }
                            },
                            new Container
                            {
                                Anchor = Anchor.CentreRight,
                                Origin = Anchor.CentreRight,
                                RelativeSizeAxes = Axes.Both,
                                Size = new Vector2(0.195f, 1.0f),
                                Child = BanButton = new OsuRequestButton
                                {
                                    Anchor = Anchor.BottomRight,
                                    Origin = Anchor.BottomRight,
                                    RelativeSizeAxes = Axes.Both,
                                    Text = "Ban",
                                    FontSize = 40,
                                    CornerRadius = 10
                                }
                            }
                        }
                    }
                }
            };

            BanButton.OnButtonClicked += () =>
            {
                Console.WriteLine("Ban button clicked");
            };
        }
    }
}