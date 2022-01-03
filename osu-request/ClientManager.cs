using System;
using System.Threading.Tasks;
using osu_request.Config;
using osu_request.Osu;
using osu_request.Twitch;
using TwitchLib.Client.Models;
using volcanicarts.osu.NET.Client;

namespace osu_request
{
    public class ClientManager
    {
        protected internal readonly OsuClientLocal OsuClient = new();
        protected internal readonly TwitchClientLocal TwitchClient = new();

        public Action OnFailedLogin;

        public void Update()
        {
            TwitchClient.Update();
        }

        public void TryConnectClients(OsuRequestConfig osuRequestConfig)
        {
            _tryConnectClients(osuRequestConfig).ConfigureAwait(false);
        }

        private async Task _tryConnectClients(OsuRequestConfig osuRequestConfig)
        {
            var osuClientId = osuRequestConfig.Get<string>(OsuRequestSetting.OsuClientId);
            var osuClientSecret = osuRequestConfig.Get<string>(OsuRequestSetting.OsuClientSecret);
            OsuClientCredentials osuClientCredentials = new(osuClientId, osuClientSecret);
            OsuClient.SetClientCredentials(osuClientCredentials);
            var osuLoggedIn = await OsuClient.LoginAsync();

            var twitchChannelName = osuRequestConfig.Get<string>(OsuRequestSetting.TwitchChannelName);
            var twitchOAuthToken = osuRequestConfig.Get<string>(OsuRequestSetting.TwitchOAuthToken);
            ConnectionCredentials twitchCredentials = new(twitchChannelName, twitchOAuthToken);
            TwitchClient.Init(twitchCredentials);

            if (!osuLoggedIn) OnFailedLogin?.Invoke();
        }
    }
}