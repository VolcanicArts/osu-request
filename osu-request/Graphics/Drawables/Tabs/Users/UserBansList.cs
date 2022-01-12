using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu_request.Clients;
using osuTK;

namespace osu_request.Drawables.Users
{
    public class UserBansList : Container
    {
        private FillFlowContainer<UserBanEntry> _fillFlowContainer;

        [BackgroundDependencyLoader]
        private void Load(UserBanManager banManager)
        {
            banManager.OnUserBan += OnUserBan;
            banManager.OnUserUnBan += OnUserUnBan;
            InitChildren();
        }

        private void OnUserUnBan(string username)
        {
            _fillFlowContainer.Where(entry => entry.Username == username).ForEach(entry => entry.DisposeGracefully());
        }

        private void OnUserBan(string username)
        {
            _fillFlowContainer.Add(new UserBanEntry(username)
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
}