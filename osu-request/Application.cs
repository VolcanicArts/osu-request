using System.Drawing;
using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Input;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Framework.Logging;
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
        private readonly ClientManager _clientManager = new();
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
            _clientManager.Update();
        }

        [BackgroundDependencyLoader]
        private void Load(FrameworkConfigManager frameworkConfig, Storage storage)
        {
            _osuRequestConfig = new OsuRequestConfig(storage);
            SetupDefaults(frameworkConfig);
            _dependencies.CacheAs(_osuRequestConfig);
            _dependencies.CacheAs(_clientManager);
            _dependencies.CacheAs(_clientManager.OsuClient);
            _dependencies.CacheAs(_clientManager.TwitchClient);

            Children = new Drawable[]
            {
                new BackgroundContainer(Color4.DarkGray),
                TabsContainer = new TabsContainer()
            };
        }

        protected override void LoadComplete()
        {
            _clientManager.OnFailedLogin += RedirectToSettings;
            _clientManager.TwitchClient.SuccessfulLogin += RedirectToRequests;
            _clientManager.TwitchClient.FailedLogin += RedirectToSettings;
            _clientManager.TryConnectClients(_osuRequestConfig);
            base.LoadComplete();
        }

        private void RedirectToSettings()
        {
            TabsContainer.Select(1);
            TabsContainer.Locked.Value = true;
        }

        private void RedirectToRequests()
        {
            TabsContainer.Locked.Value = false;
            TabsContainer.Select(0);
        }

        private void SetupDefaults(FrameworkConfigManager frameworkConfig)
        {
            frameworkConfig.GetBindable<ExecutionMode>(FrameworkSetting.ExecutionMode).Value = ExecutionMode.SingleThread;
            frameworkConfig.GetBindable<Size>(FrameworkSetting.WindowedSize).Value = new Size(960, 700);
        }
    }
}