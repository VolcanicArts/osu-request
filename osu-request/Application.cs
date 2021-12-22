using osu.Framework;
using osu_request.Twitch;

namespace osu_request
{
    public class Application : Game
    {
        private readonly TwitchClientLocal _twitchClient;

        public Application()
        {
            _twitchClient = new TwitchClientLocal(true);
            _twitchClient.Connect();
        }

        protected override bool OnExiting()
        {
            _twitchClient.Disconnect();
            return base.OnExiting();
        }
    }
}