using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using osu.Framework.Logging;
using osu.Framework.Platform;

namespace osu_request.Clients
{
    public class BeatmapsetBanManager
    {
        private readonly Storage Storage;
        private List<string> _bannedBeatmapsets = new();
        private const string FileName = "BannedBeatmapsets.json";

        public Action<string> OnBeatmapsetBan;
        public Action<string> OnBeatmapsetUnBan;

        public BeatmapsetBanManager(Storage storage)
        {
            Storage = storage;
        }

        public bool Ban(string beatmapsetId)
        {
            if (!int.TryParse(beatmapsetId, out _)) return false;
            if (IsBanned(beatmapsetId)) return true;
            OnBeatmapsetBan?.Invoke(beatmapsetId);
            _bannedBeatmapsets.Add(beatmapsetId);
            return true;
        }

        public bool UnBan(string beatmapsetId)
        {
            OnBeatmapsetUnBan?.Invoke(beatmapsetId);
            return _bannedBeatmapsets.Remove(beatmapsetId);
        }
        
        public bool IsBanned(string beatmapsetId)
        {
            return _bannedBeatmapsets.Contains(beatmapsetId);
        }

        public void Load()
        {
            if (!Storage.Exists(FileName)) return;
            var file = LoadFileToString();
            _bannedBeatmapsets = JsonConvert.DeserializeObject<List<string>>(file);
            _bannedBeatmapsets!.ForEach(beatmapsetId => OnBeatmapsetBan?.Invoke(beatmapsetId));
        }

        private string LoadFileToString()
        {
            var stream = Storage.GetStream(FileName);
            MemoryStream ms = new();
            stream.CopyTo(ms);
            stream.Close();
            return Encoding.Unicode.GetString(ms.ToArray());
        }

        public void Save()
        {
            var serialisedData = JsonConvert.SerializeObject(_bannedBeatmapsets);
            if (Storage.Exists(FileName)) Storage.Delete(FileName);
            var stream = Storage.GetStream(FileName, FileAccess.Write);
            stream.Write(Encoding.Unicode.GetBytes(serialisedData));
            stream.Close();
        }
    }
}