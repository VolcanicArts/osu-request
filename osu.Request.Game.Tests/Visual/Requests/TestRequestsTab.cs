// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using NUnit.Framework;
using osu.Request.Game.Beatmaps;
using osu.Request.Game.Graphics.Tabs.Requests;
using volcanicarts.osu.NET.Structures;

namespace osu.Request.Game.Tests.Visual.Requests;

public class TestRequestsTab : OsuRequestTestScene
{
    private RequestsTab requestsTab;

    [SetUp]
    public void SetUp()
    {
        Child = requestsTab = new RequestsTab();
    }

    [Test]
    public void TestAddRequest()
    {
        AddStep("add request", () =>
        {
            requestsTab.AddRequest(new WorkingBeatmapset
            {
                Beatmapset = new Beatmapset
                {
                    Title = "Tornado (Original Mix)",
                    Creator = "VolcanicArts",
                    PreviewUrl = "//b.ppy.sh/preview/236292.mp3"
                }
            });
        });
    }
}
