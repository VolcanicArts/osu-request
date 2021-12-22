using System.Collections.Generic;
using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Audio.Track;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
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

        private Track currentTrack;

        private readonly List<Beatmapset> BeatmapsetsToAdd = new();

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
            Child = new SpinBox
            {
                Size = new Vector2(150, 150),
                Colour = Color4.Red,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre
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
            foreach (var beatmapset in BeatmapsetsToAdd)
            {
                var backgroundTexture = Textures.Get(beatmapset.Covers.CardAt2X);
                var background = new Sprite
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = backgroundTexture.Size,
                    Texture = backgroundTexture,
                    Colour = new Colour4(1.0f, 1.0f, 1.0f, 0.5f),
                };
                Add(background);
                
                var textFlowContainer = new TextFlowContainer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    TextAnchor = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both
                };
                textFlowContainer.AddText(beatmapset.Title,
                    t => { t.Font = new FontUsage("Roboto", weight: "Regular", size: 50); });
                Add(textFlowContainer);
                
                currentTrack = Audio.GetTrackStore().Get(beatmapset.PreviewUrl);
                currentTrack.Volume.Value = .5;
                currentTrack.Start();
                currentTrack.Completed += currentTrack.Restart;
            }
        }
    }
}