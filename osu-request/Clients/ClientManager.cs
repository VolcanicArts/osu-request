using System;
using System.Threading.Tasks;
using osu.Framework.Logging;
using osu.Framework.Platform;
using osu_request.Config;
using osu_request.Osu;
using osu_request.Twitch;
using TwitchLib.Client.Models;
using volcanicarts.osu.NET.Client;

namespace osu_request
{
    public class ClientManager
    {
        protected internal readonly OsuClientLocal OsuClient;
        protected internal readonly TwitchClientLocal TwitchClient = new();

        public Action OnFailed;
        public Action OnSuccess;

        public ClientManager(Storage storage)
        {
            OsuClient = new OsuClientLocal(storage);
        }

        public void Update()
        {
            TwitchClient.Update();
        }

        public void TryConnectClients(OsuRequestConfig osuRequestConfig)
        {
            TryConnectOsuClient(osuRequestConfig).ConfigureAwait(false);
        }

        private async Task TryConnectOsuClient(OsuRequestConfig osuRequestConfig)
        {
            var osuClientId = osuRequestConfig.Get<string>(OsuRequestSetting.OsuClientId);
            var osuClientSecret = osuRequestConfig.Get<string>(OsuRequestSetting.OsuClientSecret);
            OsuClientCredentials osuClientCredentials = new(osuClientId, osuClientSecret);
            OsuClient.SetClientCredentials(osuClientCredentials);
            var osuLoggedIn = await OsuClient.LoginAsync();
            if (osuLoggedIn)
            {
                Logger.Log("OsuClient login successful");
                TryConnectTwitchClient(osuRequestConfig);
            }
            else
            {
                Logger.Log("OsuClient login failed");
                OnFailed?.Invoke();
            }
        }

        private void TryConnectTwitchClient(OsuRequestConfig osuRequestConfig)
        {
            var twitchChannelName = osuRequestConfig.Get<string>(OsuRequestSetting.TwitchChannelName);
            var twitchOAuthToken = osuRequestConfig.Get<string>(OsuRequestSetting.TwitchOAuthToken);
            ConnectionCredentials twitchCredentials = new(twitchChannelName, twitchOAuthToken);
            TwitchClient.OnSuccess += OnTwitchClientOnSuccess;
            TwitchClient.OnFailed += OnTwitchClientOnFailed;
            TwitchClient.Init(twitchCredentials);
        }

        private void OnTwitchClientOnFailed()
        {
            Logger.Log("TwitchClient login failed");
            TwitchClient.OnSuccess -= OnTwitchClientOnSuccess;
            TwitchClient.OnFailed -= OnTwitchClientOnFailed;
            OnFailed?.Invoke();
        }

        private void OnTwitchClientOnSuccess()
        {
            Logger.Log("TwitchClient login successful");
            TwitchClient.OnSuccess -= OnTwitchClientOnSuccess;
            TwitchClient.OnFailed -= OnTwitchClientOnFailed;
            OnSuccess?.Invoke();
        }
    }
}