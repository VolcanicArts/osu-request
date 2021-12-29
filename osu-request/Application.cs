using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
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
        private const string twitchChannelName = "";
        private const string twitchOAuthToken = "";
        private const string osuClientId = "";
        private const string osuClientSecret = "";
        
        private readonly OsuClient _osuClient;
        private readonly TwitchClientLocal _twitchClient;

        private DependencyContainer _dependencies;

        private BeatmapsetListContainer _beatmapsetListContainer;

        public Application()
        {
            OsuClientCredentials osuClientCredentials = new(osuClientId, osuClientSecret);
            _osuClient = new OsuClientLocal(osuClientCredentials);
            ConnectionCredentials twitchCredentials = new(twitchChannelName, twitchOAuthToken);
            _twitchClient = new TwitchClientLocal(twitchCredentials);
            Login();
        }

        private async void Login()
        {
            _twitchClient.Connect();
            await _osuClient.LoginAsync();
        }

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) =>
            _dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        // Override framework bindings to stop the user being able to cycle the frame sync
        bool IKeyBindingHandler<FrameworkAction>.OnPressed(KeyBindingPressEvent<FrameworkAction> e)
        {
            switch (e.Action)
            {
                case FrameworkAction.CycleFrameSync:
                    return true;
                default:
                    return base.OnPressed(e);
            }
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
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Colour = Color4.DarkGray
                },
                new Toolbar
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    RelativeSizeAxes = Axes.X,
                    Height = 60.0f
                },
                new FillFlowContainer
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.5f, 1.0f),
                    Direction = FillDirection.Vertical,
                    Margin = new MarginPadding
                    {
                        Top = 60.0f
                    },
                    Child = new Container
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Child = _beatmapsetListContainer = new BeatmapsetListContainer
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both
                        }
                    }
                }
            };
        }

        private void SetupDefaults(FrameworkConfigManager frameworkConfig)
        {
            frameworkConfig.GetBindable<FrameSync>(FrameworkSetting.FrameSync).Value = FrameSync.VSync;
        }
    }
}