using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Logging;
using osu.Framework.Platform;
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
        private readonly Storage Storage;
        private const string FileName = "BeatmapsetCache.json";
        private Dictionary<string, Beatmapset> BeatmapsetCache = new();

        public OsuClientLocal(Storage storage)
        {
            Storage = storage;
        }

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

                if (BeatmapsetCache.ContainsKey(beatmapsetId))
                {
                    callback?.Invoke(BeatmapsetCache[beatmapsetId]);
                    return;
                }

                var beatmapset = await OsuClient.GetBeatmapsetAsync(beatmapsetId);
                BeatmapsetCache.Add(beatmapsetId, beatmapset);

                Logger.Log($"Successfully loaded beatmapset from beatmap Id {beatmapsetId}");

                callback?.Invoke(beatmapset);
            }
            catch (HttpRequestException)
            {
                Logger.Log($"Unavailable beatmapset using Id {beatmapsetId}", LoggingTarget.Runtime, LogLevel.Error);
            }
        }

        public void LoadCache()
        {
            if (!Storage.Exists(FileName)) return;
            var file = LoadFileToString();
            BeatmapsetCache = JsonConvert.DeserializeObject<Dictionary<string, Beatmapset>>(file);
        }

        private string LoadFileToString()
        {
            var stream = Storage.GetStream(FileName);
            MemoryStream ms = new();
            stream.CopyTo(ms);
            stream.Close();
            return Encoding.Unicode.GetString(ms.ToArray());
        }

        public void SaveCache()
        {
            var serialisedData = JsonConvert.SerializeObject(BeatmapsetCache);
            if (Storage.Exists(FileName)) Storage.Delete(FileName);
            var stream = Storage.GetStream(FileName, FileAccess.Write);
            stream.Write(Encoding.Unicode.GetBytes(serialisedData));
            stream.Close();
        }
    }
}