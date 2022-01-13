using System;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Platform;
using osu_request.Config;
using osu_request.Drawables;
using osu_request.Drawables.Notifications;
using osu_request.Websocket;
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
        private WebSocketClient WebSocketClient;

        [BackgroundDependencyLoader]
        private void Load(Storage storage)
        {
            CreateConfigsAndManagers(storage);
            CacheDependencies();
            InitialiseChildren();
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            TabsContainer.Select(Tabs.Requests);
            WebSocketClient.ConnectAsync();
            WebSocketClient.SendAuth(OsuRequestConfig);
        }

        private void CreateConfigsAndManagers(Storage storage)
        {
            OsuRequestConfig = new OsuRequestConfig(storage);
            WebSocketClient = new WebSocketClient("127.0.0.1", 8080);
        }

        private void CacheDependencies()
        {
            Dependencies.CacheAs(OsuRequestConfig);
            Dependencies.CacheAs(WebSocketClient);
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