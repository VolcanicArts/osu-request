#nullable enable
// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using osu.Framework.Audio.Track;
using osu.Framework.Graphics.Textures;
using volcanicarts.osu.NET.Structures;

namespace osu.Request.Game.Beatmaps;

public class WorkingBeatmapset
{
    public Beatmapset Beatmapset { get; init; }

    public Texture? CoverTexture { get; set; }
    public Track? PreviewAudio { get; set; }

    public string ExternalUrl => $"https://osu.ppy.sh/beatmapsets/{Beatmapset.Id}";
    public string DirectUrl => $"osu://b/{Beatmapset.Id}";
    public bool IsLoaded => CoverTexture != null && PreviewAudio != null;

    public void Dispose()
    {
        CoverTexture?.Dispose();
        PreviewAudio?.Dispose();
    }
}
