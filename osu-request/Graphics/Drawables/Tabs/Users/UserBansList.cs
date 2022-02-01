using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu_request.Websocket;
using osuTK;
using TwitchLib.Api.Helix.Models.Users.GetUsers;

namespace osu_request.Drawables.Users;

public class UserBansList : Container
{
    private FillFlowContainer<UserBanEntry> _fillFlowContainer;

    [BackgroundDependencyLoader]
    private void Load(WebSocketClient webSocketClient)
    {
        webSocketClient.OnConnect += () => Scheduler.Add(() => _fillFlowContainer.Clear());
        webSocketClient.OnUserBan += user => Scheduler.Add(() => OnUserBan(user));
        webSocketClient.OnUserUnBan += userId => Scheduler.Add(() => OnUserUnBan(userId));
        InitChildren();
    }

    private void OnUserUnBan(string userId)
    {
        _fillFlowContainer.Where(entry => entry.User.Id == userId).ForEach(entry => entry.DisposeGracefully());
    }

    private void OnUserBan(User user)
    {
        _fillFlowContainer.Add(new UserBanEntry(user)
        {
            Anchor = Anchor.TopLeft,
            Origin = Anchor.TopLeft,
            RelativeSizeAxes = Axes.X,
            Size = new Vector2(1.0f, 30.0f)
        });
    }

    private void InitChildren()
    {
        Child = new Container
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            RelativeSizeAxes = Axes.Both,
            Masking = true,
            CornerRadius = 10,
            EdgeEffect = OsuRequestEdgeEffects.BasicShadow,
            Children = new Drawable[]
            {
                new TrianglesBackground
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    ColourLight = OsuRequestColour.Gray3,
                    ColourDark = OsuRequestColour.Gray2,
                    Velocity = 0.5f,
                    TriangleScale = 5
                },
                new BasicScrollContainer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    ClampExtension = 20,
                    ScrollbarVisible = false,
                    Child = _fillFlowContainer = new FillFlowContainer<UserBanEntry>
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                        Direction = FillDirection.Vertical,
                        Spacing = new Vector2(2),
                        Padding = new MarginPadding(10)
                    }
                }
            }
        };
    }
}