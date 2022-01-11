using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace osu_request.Drawables.Notifications
{
    public class NotificationContainer : Container
    {
        private new Container Content;
        private TextFlowContainer Message;
        private TextFlowContainer Title;

        [BackgroundDependencyLoader]
        private void Load()
        {
            InternalChild = Content = new Container
            {
                Anchor = Anchor.BottomCentre,
                Origin = Anchor.BottomCentre,
                Size = new Vector2(300.0f, 100.0f),
                RelativeAnchorPosition = new Vector2(0.5f, 0.0f),
                Masking = true,
                CornerRadius = 10,
                EdgeEffect = OsuRequestEdgeEffects.DispersedShadow,
                Children = new Drawable[]
                {
                    new Box
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Colour = OsuRequestColour.Gray2.Opacity(0.75f)
                    },
                    new Container
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Padding = new MarginPadding(10),
                        Children = new Drawable[]
                        {
                            Title = new TextFlowContainer
                            {
                                Anchor = Anchor.TopCentre,
                                Origin = Anchor.TopCentre,
                                TextAnchor = Anchor.TopCentre,
                                RelativeSizeAxes = Axes.Both,
                                Size = new Vector2(1.0f, 0.5f)
                            },
                            Message = new TextFlowContainer
                            {
                                Anchor = Anchor.BottomCentre,
                                Origin = Anchor.BottomCentre,
                                TextAnchor = Anchor.Centre,
                                RelativeSizeAxes = Axes.Both,
                                Size = new Vector2(1.0f, 0.5f)
                            }
                        }
                    }
                }
            };
        }

        public void Notify(string title, string message)
        {
            Scheduler.Add(() => notify(title, message));
        }

        private void notify(string title, string message)
        {
            Content.FinishTransforms();

            Title.Text = string.Empty;
            Message.Text = string.Empty;

            Title.AddText(title, t => t.Font = OsuRequestFonts.Regular.With(size: 30));
            Message.AddText(message, t => t.Font = OsuRequestFonts.Regular.With(size: 20));

            Content.MoveToY(150.0f, 300, Easing.OutCubic)
                .Delay(3000)
                .MoveToY(0.0f, 300, Easing.InCubic);
        }
    }
}