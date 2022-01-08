using System.Drawing;
using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Framework.IO.Stores;
using osu.Framework.Platform;
using osu_request.Resources;
using osuTK;

namespace osu_request
{
    public class OsuRequestBase : Game, IKeyBindingHandler<FrameworkAction>
    {
        private const string WindowTitle = @"osu!request";
        private static readonly Size InitialWindowSize = new(960, 700);

        protected new DependencyContainer Dependencies;

        public OsuRequestBase()
        {
            base.Content.Add(new DrawSizePreservingFillContainer
            {
                TargetDrawSize = new Vector2(InitialWindowSize.Width, InitialWindowSize.Height)
            });
        }

        // Override framework bindings
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
            return Dependencies = new DependencyContainer(base.CreateChildDependencies(parent));
        }

        [BackgroundDependencyLoader]
        private void Load(GameHost host, FrameworkConfigManager frameworkConfig)
        {
            SetWindowAttributes(host);
            AddResources();
            SetupDefaultFrameworkConfig(frameworkConfig);
        }

        private void SetWindowAttributes(GameHost host)
        {
            host.Window.Title = WindowTitle;
        }

        private void AddResources()
        {
            Resources.AddStore(new DllResourceStore(OsuRequestResources.ResourceAssembly));
        }

        private void SetupDefaultFrameworkConfig(FrameworkConfigManager frameworkConfig)
        {
            frameworkConfig.GetBindable<ExecutionMode>(FrameworkSetting.ExecutionMode).Value = ExecutionMode.MultiThreaded;
            frameworkConfig.GetBindable<Size>(FrameworkSetting.WindowedSize).Value = InitialWindowSize;
        }
    }
}