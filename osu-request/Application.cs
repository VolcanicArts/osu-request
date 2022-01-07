using System.Drawing;
using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Framework.Platform;
using osu_request.Config;
using osu_request.Drawables;
using osuTK;

namespace osu_request
{
    public class Application : Game, IKeyBindingHandler<FrameworkAction>
    {
        private static readonly Size InitialWindowSize = new(960, 700);
        
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
        private void Load(FrameworkConfigManager frameworkConfig, Storage storage, GameHost host)
        {
            host.Window.Title = @"osu!request";
            _osuRequestConfig = new OsuRequestConfig(storage);
            SetupDefaults(frameworkConfig);
            _dependencies.CacheAs(_osuRequestConfig);
            _dependencies.CacheAs(_clientManager);
            _dependencies.CacheAs(_clientManager.OsuClient);
            _dependencies.CacheAs(_clientManager.TwitchClient);

            Children = new Drawable[]
            {
                new DrawSizePreservingFillContainer
                {
                    Strategy = DrawSizePreservationStrategy.Minimum,
                    TargetDrawSize = new Vector2(InitialWindowSize.Width, InitialWindowSize.Height),
                    Children = new Drawable[]
                    {
                        new BackgroundColour
                        {
                            Colour = OsuRequestColour.Gray4
                        },
                        TabsContainer = new TabsContainer()
                    }
                }
            };
        }

        protected override void LoadComplete()
        {
            TabsContainer.Select(Tabs.Requests);
            _clientManager.OnFailed += () => Scheduler.AddOnce(() => TabsContainer.Override(Tabs.Settings));
            _clientManager.OnSuccess += () => Scheduler.AddOnce(() => TabsContainer.ReleaseAndSelect(Tabs.Requests));
            _clientManager.TryConnectClients(_osuRequestConfig);
            base.LoadComplete();
        }

        private void SetupDefaults(FrameworkConfigManager frameworkConfig)
        {
            frameworkConfig.GetBindable<ExecutionMode>(FrameworkSetting.ExecutionMode).Value = ExecutionMode.SingleThread;
            frameworkConfig.GetBindable<Size>(FrameworkSetting.WindowedSize).Value = InitialWindowSize;
        }
    }
}