// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Request.Game.Beatmaps;
using osu.Request.Game.Graphics.Tabs.Requests;

namespace osu.Request.Game.Tests.Visual.Requests;

public class TestRequestEntry : OsuRequestTestScene
{
    [SetUp]
    public void SetUp()
    {
        Child = new RequestEntry
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            SourceBeatmapset = new RequestedBeatmapset
            {
                Title = "Test"
            }
        };
    }
}
