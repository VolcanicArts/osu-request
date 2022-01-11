using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu_request.Twitch;
using osuTK;
using TwitchLib.Client.Models;

namespace osu_request.Drawables.Users
{
    public class UsersList : Container
    {
        private FillFlowContainer<UserCard> _fillFlowContainer;

        [BackgroundDependencyLoader]
        private void Load(TwitchClientLocal localTwitchClient)
        {
            localTwitchClient.OnChatMessage += HandleTwitchMessage;

            Masking = true;
            CornerRadius = 10;
            EdgeEffect = OsuRequestEdgeEffects.BasicShadow;
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
                    Child = _fillFlowContainer = new FillFlowContainer<UserCard>
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
            };
        }

        private void HandleTwitchMessage(ChatMessage message)
        {
            if (!message.Message.StartsWith("!rq")) return;

            Scheduler.Add(() =>
            {
                if (_fillFlowContainer.Any(card => card.Username == message.Username)) return;
                _fillFlowContainer.Add(new UserCard(message)
                    {
                        Anchor = Anchor.TopLeft,
                        Origin = Anchor.TopLeft,
                        RelativeSizeAxes = Axes.X,
                        Size = new Vector2(1.0f, 30.0f)
                    }
                );
            });
        }
    }
}