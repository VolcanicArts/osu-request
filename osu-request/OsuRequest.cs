using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Platform;
using osu_request.Clients;
using osu_request.Config;
using osu_request.Drawables;

namespace osu_request
{
    public class OsuRequest : OsuRequestBase
    {
        private BeatmapsetBanManager BeatmapsetBanManager;
        private ClientManager ClientManager;
        private OsuRequestConfig OsuRequestConfig;

        private TabsContainer TabsContainer;

        protected override void UpdateAfterChildren()
        {
            base.UpdateAfterChildren();
            ClientManager.Update();
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            TabsContainer.Select(Tabs.Requests);
            ClientManager.OnFailed += () => Scheduler.AddOnce(() => TabsContainer.Override(Tabs.Settings));
            ClientManager.OnSuccess += () => Scheduler.AddOnce(() => TabsContainer.ReleaseAndSelect(Tabs.Requests));
            ClientManager.TryConnectClients(OsuRequestConfig);
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
            BeatmapsetBanManager = new BeatmapsetBanManager();
            OsuRequestConfig = new OsuRequestConfig(storage);
            ClientManager = new ClientManager();
        }

        private void CacheDependencies()
        {
            Dependencies.CacheAs(OsuRequestConfig);
            Dependencies.CacheAs(ClientManager);
            Dependencies.CacheAs(ClientManager.OsuClient);
            Dependencies.CacheAs(ClientManager.TwitchClient);
            Dependencies.CacheAs(BeatmapsetBanManager);
        }

        private void InitialiseChildren()
        {
            Children = new Drawable[]
            {
                new BackgroundColour
                {
                    Colour = OsuRequestColour.Gray4
                },
                TabsContainer = new TabsContainer()
            };
        }
    }
}