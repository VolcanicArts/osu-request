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
            StartWebsocket();
        }

        private void StartWebsocket()
        {
            WebSocketClient.OnFailedConnection += () => NotificationContainer.Notify("Failed Connection", "Could not connect to the server. Please try again later");
            WebSocketClient.OnLoggedIn += () => NotificationContainer.Notify("Logged In!", "Connection was successful. Requests incoming!");
            WebSocketClient.OnInvalidUsername += () =>
            {
                NotificationContainer.Notify("Invalid username", "Please login with Twitch or enter a correct username");
                TabsContainer.Select(Tabs.Settings);
            };
            WebSocketClient.OnInvalidCode += () =>
            {
                NotificationContainer.Notify("Invalid code", "Please login with Twitch or enter a correct code");
                TabsContainer.Select(Tabs.Settings);
            };
            WebSocketClient.ConnectAsync();
            WebSocketClient.SendAuth(OsuRequestConfig);
        }

        private void CreateConfigsAndManagers(Storage storage)
        {
            OsuRequestConfig = new OsuRequestConfig(storage);
            WebSocketClient = new WebSocketClient();
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