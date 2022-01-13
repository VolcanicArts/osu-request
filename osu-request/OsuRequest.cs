using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Platform;
using osu_request.Config;
using osu_request.Drawables;
using osu_request.Drawables.Notifications;
using osuTK;

namespace osu_request
{
    public class OsuRequest : OsuRequestBase
    {
        [Cached]
        private readonly NotificationContainer NotificationContainer = new()
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            RelativeSizeAxes = Axes.Both
        };
        
        private OsuRequestConfig OsuRequestConfig;

        private TabsContainer TabsContainer;

        private void OnClientManagerFail()
        {
            Scheduler.AddOnce(() =>
            {
                TabsContainer.Select(Tabs.Settings);
                NotificationContainer.Notify("Invalid Settings", "Please enter valid settings to allow this app to work");
            });
        }

        [BackgroundDependencyLoader]
        private void Load(Storage storage)
        {
            CreateConfigsAndManagers(storage);
            CacheDependencies();
            InitialiseChildren();
        }

        private void CreateConfigsAndManagers(Storage storage)
        {
            OsuRequestConfig = new OsuRequestConfig(storage);
        }

        private void CacheDependencies()
        {
            Dependencies.CacheAs(OsuRequestConfig);
        }

        private void InitialiseChildren()
        {
            Child = new DrawSizePreservingFillContainer
            {
                TargetDrawSize = new Vector2(InitialWindowSize.Width, InitialWindowSize.Height),
                Children = new Drawable[]
                {
                    TabsContainer = new TabsContainer
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both
                    },
                    NotificationContainer
                }
            };
        }
    }
}