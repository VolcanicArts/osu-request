using System;
using System.Collections.Generic;

namespace osu_request.Clients
{
    public class BeatmapsetBanManager
    {
        private readonly List<string> _bannedBeatmapsets = new();

        public Action<string> OnBeatmapsetBan;
        public Action<string> OnBeatmapsetUnBan;

        public bool Ban(string beatmapsetId)
        {
            if (!int.TryParse(beatmapsetId, out _)) return false;
            OnBeatmapsetBan?.Invoke(beatmapsetId);
            _bannedBeatmapsets.Add(beatmapsetId);
            return true;
        }

        public bool UnBan(string beatmapsetId)
        {
            OnBeatmapsetUnBan?.Invoke(beatmapsetId);
            return _bannedBeatmapsets.Remove(beatmapsetId);
        }
    }
}