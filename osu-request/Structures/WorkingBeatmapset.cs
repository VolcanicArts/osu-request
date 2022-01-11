using osu.Framework.Audio.Track;
using osu.Framework.Graphics.Textures;
using volcanicarts.osu.NET.Structures;

namespace osu_request.Structures
{
    public class WorkingBeatmapset
    {
        protected internal Beatmapset Beatmapset { get; }
        protected internal Texture Cover { get; }
        protected internal Track Preview { get; }

        public WorkingBeatmapset(Beatmapset beatmapset, Texture cover, Track preview)
        {
            Beatmapset = beatmapset;
            Cover = cover;
            Preview = preview;

            Preview.Volume.Value = 0.25f;
            Preview.Completed += Preview.Restart;
        }
    }
}