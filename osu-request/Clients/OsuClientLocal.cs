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
        private readonly Dictionary<string, Beatmapset> _beatmapsetCache = new();

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
        /// <param name="beatmapId">The beatmap Id that will be used to get the beatmapset</param>
        /// <param name="callback">What to do with the resulting beatmapset</param>
        public void RequestBeatmapsetFromBeatmapId(string beatmapId, Action<Beatmapset> callback = null)
        {
            requestBeatmapsetFromBeatmapId(beatmapId, callback).ConfigureAwait(false);
        }

        private async Task requestBeatmapsetFromBeatmapId(string beatmapId, Action<Beatmapset> callback)
        {
            if (_beatmapsetCache.ContainsKey(beatmapId))
            {
                callback?.Invoke(_beatmapsetCache[beatmapId]);
                return;
            }

            try
            {
                Logger.Log($"Requesting beatmap using Id {beatmapId}");

                if (!IsReady)
                {
                    Logger.Log("Client not ready. Cannot request beatmap");
                    return;
                }

                var beatmap = await OsuClient.GetBeatmapAsync(beatmapId);
                var beatmapset = await beatmap.GetBeatmapsetAsync();

                _beatmapsetCache.Add(beatmapId, beatmapset);

                Logger.Log($"Successfully loaded beatmapset from beatmap Id {beatmapId}");

                callback?.Invoke(beatmapset);
            }
            catch (HttpRequestException)
            {
                Logger.Log($"Unavailable beatmap using Id {beatmapId}", LoggingTarget.Runtime, LogLevel.Error);
            }
        }
    }
}