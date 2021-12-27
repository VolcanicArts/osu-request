using System.Collections.Generic;
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
using volcanicarts.osu.NET.Structures;

namespace osu_request
{
    public class Application : Game, IKeyBindingHandler<FrameworkAction>
    {
        private const string twitchChannelName = "";
        private const string twitchOAuthToken = "";
        private const string osuClientId = "";
        private const string osuClientSecret = "";
        
        private readonly OsuClient osuClient;
        private readonly TwitchClientLocal twitchClient;

        private DependencyContainer _dependencies;

        private readonly List<Beatmapset> BeatmapsetsToAdd = new();

        private Container _contentContainer;

        private BeatmapsetListContainer beatmapsetListContainer;

        public Application()
        {
            ConnectionCredentials twitchCredentials = new(twitchChannelName, twitchOAuthToken);
            twitchClient = new TwitchClientLocal(twitchCredentials, false);
            OsuClientCredentials osuClientCredentials = new(osuClientId, osuClientSecret);
            osuClient = new OsuClientLocal(osuClientCredentials);
            Login();
        }

        private async void Login()
        {
            twitchClient.Connect();
            await osuClient.LoginAsync();
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

        [BackgroundDependencyLoader]
        private void Load(FrameworkConfigManager frameworkConfig)
        {
            SetupDefaults(frameworkConfig);
            _dependencies.CacheAs(twitchClient);
            _dependencies.CacheAs(osuClient);

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
                    Child = _contentContainer = new Container
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Child = beatmapsetListContainer = new BeatmapsetListContainer
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

        protected override void LoadAsyncComplete()
        {
            twitchClient.ScheduleBeatmapAddition += beatmapset => BeatmapsetsToAdd.Add(beatmapset);
            base.LoadAsyncComplete();
        }

        protected override void Update()
        {
            AddBeatmapsets();
            BeatmapsetsToAdd.Clear();
            base.Update();
        }

        protected override bool OnExiting()
        {
            twitchClient.Disconnect();
            return base.OnExiting();
        }

        private void AddBeatmapsets()
        {
            if (BeatmapsetsToAdd.Count == 0) return;
            beatmapsetListContainer.AddBeatmapset(BeatmapsetsToAdd[0]);
        }
    }
}