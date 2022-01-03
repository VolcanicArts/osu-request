using System.Drawing;
using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Input;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Framework.Platform;
using osu_request.Config;
using osu_request.Drawables;
using osu_request.Osu;
using osu_request.Twitch;
using osuTK.Graphics;
using TwitchLib.Client.Models;
using volcanicarts.osu.NET.Client;

namespace osu_request
{
    public class Application : Game, IKeyBindingHandler<FrameworkAction>
    {
        private readonly OsuClientLocal _osuClient = new();
        private readonly TwitchClientLocal _twitchClient = new();
        private DependencyContainer _dependencies;
        private OsuRequestConfig _osuRequestConfig;

        private TabsContainer TabsContainer;

        // Override framework bindings to stop the user being able to cycle the frame sync
        bool IKeyBindingHandler<FrameworkAction>.OnPressed(KeyBindingPressEvent<FrameworkAction> e)
        {
            return e.Action switch
            {
                FrameworkAction.CycleFrameSync => true,
                FrameworkAction.ToggleFullscreen => true,
                FrameworkAction.CycleExecutionMode => true,
                _ => base.OnPressed(e)
            };
        }

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent)
        {
            return _dependencies = new DependencyContainer(base.CreateChildDependencies(parent));
        }

        protected override void UpdateAfterChildren()
        {
            _twitchClient.Update();
        }

        [BackgroundDependencyLoader]
        private void Load(FrameworkConfigManager frameworkConfig, Storage storage)
        {
            _osuRequestConfig = new OsuRequestConfig(storage);
            SetupDefaults(frameworkConfig);
            _dependencies.CacheAs(_osuRequestConfig);
            _dependencies.CacheAs(_twitchClient);
            _dependencies.CacheAs(_osuClient);

            Children = new Drawable[]
            {
                new BackgroundContainer(Color4.DarkGray),
                TabsContainer = new TabsContainer()
            };
        }

        protected override async void LoadComplete()
        {
            var osuClientId = _osuRequestConfig.Get<string>(OsuRequestSetting.OsuClientId);
            var osuClientSecret = _osuRequestConfig.Get<string>(OsuRequestSetting.OsuClientSecret);
            OsuClientCredentials osuClientCredentials = new(osuClientId, osuClientSecret);
            _osuClient.SetClientCredentials(osuClientCredentials);
            var osuLoggedIn = await _osuClient.LoginAsync();

            var twitchChannelName = _osuRequestConfig.Get<string>(OsuRequestSetting.TwitchChannelName);
            var twitchOAuthToken = _osuRequestConfig.Get<string>(OsuRequestSetting.TwitchOAuthToken);
            ConnectionCredentials twitchCredentials = new(twitchChannelName, twitchOAuthToken);
            var twitchLoggedIn = _twitchClient.Init(twitchCredentials);

            if (!osuLoggedIn || !twitchLoggedIn) RedirectToSettings();
            base.LoadComplete();
        }

        private void RedirectToSettings()
        {
            TabsContainer.Select(1);
            TabsContainer.Locked.Value = true;
        }

        private void SetupDefaults(FrameworkConfigManager frameworkConfig)
        {
            frameworkConfig.GetBindable<ExecutionMode>(FrameworkSetting.ExecutionMode).Value = ExecutionMode.SingleThread;
            frameworkConfig.GetBindable<Size>(FrameworkSetting.WindowedSize).Value = new Size(960, 700);
        }
    }
}