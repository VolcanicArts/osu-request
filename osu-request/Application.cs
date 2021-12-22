using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu_request.Drawables;
using osu_request.Osu;
using osu_request.Twitch;
using osuTK;
using osuTK.Graphics;
using volcanicarts.osu.NET.Client;

namespace osu_request
{
    public class Application : Game
    {
        private readonly OsuClient _osuClient;
        private readonly TwitchClientLocal _twitchClient;

        public Application()
        {
            _twitchClient = new TwitchClientLocal(true);
            _osuClient = new OsuClientLocal();
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
            _twitchClient.Connect();
            await _osuClient.LoginAsync();
            base.LoadAsyncComplete();
        }

        protected override bool OnExiting()
        {
            _twitchClient.Disconnect();
            return base.OnExiting();
        }
    }
}