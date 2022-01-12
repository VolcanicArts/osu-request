using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using osu.Framework.Platform;

namespace osu_request.Clients
{
    public class UserBanManager
    {
        private const string FileName = "BannedUsers.json";
        private readonly Storage Storage;
        private List<string> _bannedUsers = new();

        public Action<string> OnUserBan;
        public Action<string> OnUserUnBan;

        public UserBanManager(Storage storage)
        {
            Storage = storage;
        }

        public bool Ban(string beatmapsetId)
        {
            if (!int.TryParse(beatmapsetId, out _)) return false;
            if (IsBanned(beatmapsetId)) return true;
            OnUserBan?.Invoke(beatmapsetId);
            _bannedUsers.Add(beatmapsetId);
            return true;
        }

        public bool UnBan(string beatmapsetId)
        {
            OnUserUnBan?.Invoke(beatmapsetId);
            return _bannedUsers.Remove(beatmapsetId);
        }

        public bool IsBanned(string beatmapsetId)
        {
            return _bannedUsers.Contains(beatmapsetId);
        }

        public void Load()
        {
            if (!Storage.Exists(FileName)) return;
            var file = LoadFileToString();
            _bannedUsers = JsonConvert.DeserializeObject<List<string>>(file);
            _bannedUsers!.ForEach(beatmapsetId => OnUserBan?.Invoke(beatmapsetId));
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
            var serialisedData = JsonConvert.SerializeObject(_bannedUsers);
            if (Storage.Exists(FileName)) Storage.Delete(FileName);
            var stream = Storage.GetStream(FileName, FileAccess.Write);
            stream.Write(Encoding.Unicode.GetBytes(serialisedData));
            stream.Close();
        }
    }
}