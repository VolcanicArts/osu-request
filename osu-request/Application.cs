using System.Collections.Generic;
using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
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
    public class Application : Game
    {
        private const string twitchChannelName = "";
        private const string twitchOAuthToken = "";
        private const string osuClientId = "";
        private const string osuClientSecret = "";
        public static OsuClient osuClient;
        public static TwitchClientLocal twitchClient;

        private readonly List<Beatmapset> BeatmapsetsToAdd = new();

        private BeatmapsetContainer currentBeatmapset;

        public Application()
        {
            ConnectionCredentials twitchCredentials = new(twitchChannelName, twitchOAuthToken);
            twitchClient = new TwitchClientLocal(twitchCredentials, false);
            OsuClientCredentials osuClientCredentials = new(osuClientId, osuClientSecret);
            osuClient = new OsuClientLocal(osuClientCredentials);
        }

        [BackgroundDependencyLoader]
        private void Load()
        {
            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Colour = Color4.Gray
                },
                new SpinBox
                {
                    Size = new Vector2(150, 150),
                    Colour = Color4.Red,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre
                },
                currentBeatmapset = new BeatmapsetContainer(null)
            };
        }

        protected override async void LoadAsyncComplete()
        {
            twitchClient.Connect();
            await osuClient.LoginAsync();
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
            Remove(currentBeatmapset);
            currentBeatmapset.Dispose();
            currentBeatmapset = new BeatmapsetContainer(BeatmapsetsToAdd[0]);
            Add(currentBeatmapset);
        }
    }
}