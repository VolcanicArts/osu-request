// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using osu.Framework.Audio.Track;
using osu.Framework.Graphics.Textures;
using volcanicarts.osu.NET.Structures;

namespace osu.Request.Game.Beatmaps;

public class RequestedBeatmapset : Beatmapset
{
    public Texture CoverTexture { get; init; }
    public Track PreviewAudio { get; init; }

    public void Dispose()
    {
        CoverTexture.Dispose();
        PreviewAudio.Dispose();
    }
}
