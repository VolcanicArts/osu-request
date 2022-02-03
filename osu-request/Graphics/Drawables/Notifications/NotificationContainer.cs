using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using osuTK.Graphics;

namespace osu_request.Drawables.Notifications;

public class NotificationContainer : Container
{
    private FillFlowContainer<Notification> Notifications;

    [BackgroundDependencyLoader]
    private void Load()
    {
        InternalChild = Notifications = new FillFlowContainer<Notification>
        {
            Anchor = Anchor.BottomRight,
            Origin = Anchor.BottomRight,
            AutoSizeAxes = Axes.Both,
            Direction = FillDirection.Vertical,
            Spacing = new Vector2(0, 10),
            Padding = new MarginPadding(10)
        };
    }

    public void Notify(string title, string message, Color4 highlight)
    {
        Scheduler.Add(() =>
        {
            Notifications.Add(new Notification
            {
                Anchor = Anchor.TopRight,
                Origin = Anchor.TopRight,
                Size = new Vector2(200, 60),
                Title = title,
                Message = message,
                Highlight = highlight
            });
        });
    }
}