using osu.Framework;
using osu_request.Osu;
using osu_request.Twitch;
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