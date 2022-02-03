using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;
using osuTK.Graphics;

namespace osu_request.Drawables.Notifications;

public class Notification : Container
{
    private new Container Content;
    public string Title { get; init; }
    public string Message { get; init; }
    public Color4 Highlight { get; init; }

    [BackgroundDependencyLoader]
    private void Load()
    {
        InternalChild = Content = new Container
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            RelativeSizeAxes = Axes.Both,
            RelativePositionAxes = Axes.Both,
            Position = new Vector2(2.0f, 0.0f),
            Masking = true,
            CornerRadius = 10,
            BorderThickness = 2,
            BorderColour = Highlight,
            EdgeEffect = OsuRequestEdgeEffects.DispersedShadow,
            Children = new Drawable[]
            {
                new Box
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Colour = OsuRequestColour.Gray2.Opacity(0.9f)
                },
                new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding(5),
                    Children = new Drawable[]
                    {
                        new TextFlowContainer(t => t.Font = OsuRequestFonts.Regular.With(size: 20))
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            TextAnchor = Anchor.TopCentre,
                            RelativeSizeAxes = Axes.Both,
                            Size = new Vector2(1.0f, 0.5f),
                            Text = Title
                        },
                        new TextFlowContainer(t => t.Font = OsuRequestFonts.Regular.With(size: 15))
                        {
                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.BottomCentre,
                            TextAnchor = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            Size = new Vector2(1.0f, 0.5f),
                            Text = Message
                        }
                    }
                }
            }
        };
    }

    protected override void LoadComplete()
    {
        base.LoadComplete();
        Content.Delay(500)
            .MoveToX(0.0f, 400, Easing.OutCirc)
            .Delay(3000)
            .MoveToX(2.0f, 400, Easing.InCirc)
            .Finally(_ => this.RemoveAndDisposeImmediately());
    }
}