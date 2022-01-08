using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using osu.Framework.Logging;
using osu.Framework.Threading;
using osu_request.Drawables;
using volcanicarts.osu.NET.Client;
using volcanicarts.osu.NET.Structures;

namespace osu_request.Osu
{
    public class OsuClientLocal
    {
        private bool IsReady;
        private OsuClient OsuClient;

        public void SetClientCredentials(OsuClientCredentials clientCredentials)
        {
            OsuClient = new OsuClient(clientCredentials);
        }

        public async Task<bool> LoginAsync()
        {
            try
            {
                await OsuClient.LoginAsync();
                IsReady = true;
                return true;
            }
            catch (Exception)
            {
                IsReady = false;
                return false;
            }
        }

        /// <summary>
        /// Middleman method to make requesting beatmapsets easier
        /// </summary>
        /// <param name="beatmapsetId">The beatmapset Id that will be used to get the beatmapset</param>
        /// <param name="callback">What to do with the resulting beatmapset</param>
        public void RequestBeatmapsetFromBeatmapsetId(string beatmapsetId, Action<Beatmapset> callback = null)
        {
            requestBeatmapsetFromBeatmapsetId(beatmapsetId, callback).ConfigureAwait(false);
        }

        private async Task requestBeatmapsetFromBeatmapsetId(string beatmapsetId, Action<Beatmapset> callback)
        {
            try
            {
                Logger.Log($"Requesting beatmapset using Id {beatmapsetId}");

                if (!IsReady)
                {
                    Logger.Log("Client not ready. Cannot request beatmapset");
                    return;
                }

                var beatmapset = await OsuClient.GetBeatmapsetAsync(beatmapsetId);

                Logger.Log($"Successfully loaded beatmapset from beatmap Id {beatmapsetId}");

                callback?.Invoke(beatmapset);
            }
            catch (HttpRequestException)
            {
                Logger.Log($"Unavailable beatmapset using Id {beatmapsetId}", LoggingTarget.Runtime, LogLevel.Error);
            }
        }
    }
}