// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Textures;
using osu.Request.Game.Beatmaps;
using osu.Request.Game.Graphics.Tabs.Requests;

namespace osu.Request.Game.Tests.Visual.Requests;

public class TestRequestEntry : OsuRequestTestScene
{
    [Resolved]
    private TextureStore textureStore { get; set; }

    [Resolved]
    private AudioManager audioManager { get; set; }

    [SetUp]
    public void SetUp()
    {
        var texture = textureStore.Get("https://assets.ppy.sh/beatmaps/236292/covers/cover.jpg?1631509201");
        var preview = audioManager.GetTrackStore().Get("https://b.ppy.sh/preview/236292.mp3");

        Child = new RequestEntry
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            SourceBeatmapset = new WorkingBeatmapset
            {
                Title = "Tornado (Original Mix)",
                Creator = "VolcanicArts",
                PreviewAudio = preview,
                CoverTexture = texture
            }
        };
    }
}
