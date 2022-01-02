using System;
using System.Drawing;
using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Framework.Platform;
using osu_request.Drawables;
using osu_request.Osu;
using osu_request.Twitch;
using osuTK;
using osuTK.Graphics;
using TwitchLib.Client.Models;
using volcanicarts.osu.NET.Client;

namespace osu_request
{
    public class Application : Game, IKeyBindingHandler<FrameworkAction>
    {
        private readonly OsuClient _osuClient;
        private readonly TwitchClientLocal _twitchClient;

        private Container _contentContainer;

        private DependencyContainer _dependencies;
        private RequestsListingTab requestListingTab;

        public Application()
        {
            var osuClientId = Environment.GetEnvironmentVariable("osuClientId");
            var osuClientSecret = Environment.GetEnvironmentVariable("osuClientSecret");
            OsuClientCredentials osuClientCredentials = new(osuClientId, osuClientSecret);
            _osuClient = new OsuClientLocal(osuClientCredentials);
            
            var twitchChannelName = Environment.GetEnvironmentVariable("twitchChannelName");
            var twitchOAuthToken = Environment.GetEnvironmentVariable("twitchOAuthToken");
            ConnectionCredentials twitchCredentials = new(twitchChannelName, twitchOAuthToken);
            _twitchClient = new TwitchClientLocal(twitchCredentials);
            Login();
        }

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

        private async void Login()
        {
            _twitchClient.Connect();
            await _osuClient.LoginAsync();
        }

        private void InitTabs()
        {
            requestListingTab = new RequestsListingTab();
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            InitTabs();
            _contentContainer.Child = requestListingTab;
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
        private void Load(FrameworkConfigManager frameworkConfig)
        {
            SetupDefaults(frameworkConfig);
            _dependencies.CacheAs(_twitchClient);
            _dependencies.CacheAs(_osuClient);

            Children = new Drawable[]
            {
                new BackgroundContainer(Color4.DarkGray),
                new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        new Toolbar
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            RelativeSizeAxes = Axes.X,
                            Size = new Vector2(1.0f, 60.0f)
                        },
                        _contentContainer = new Container
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            Padding = new MarginPadding
                            {
                                Top = 60.0f
                            }
                        }
                    }
                }
            };
        }

        private void SetupDefaults(FrameworkConfigManager frameworkConfig)
        {
            frameworkConfig.GetBindable<ExecutionMode>(FrameworkSetting.ExecutionMode).Value = ExecutionMode.SingleThread;
            frameworkConfig.GetBindable<Size>(FrameworkSetting.WindowedSize).Value = new Size(960, 700);
        }
    }
}