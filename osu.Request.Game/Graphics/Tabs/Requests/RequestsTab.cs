// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Request.Game.Beatmaps;
using osuTK;

namespace osu.Request.Game.Graphics.Tabs.Requests;

public class RequestsTab : BaseTab
{
    private FillFlowContainer<RequestEntry> entryFlow;

    [BackgroundDependencyLoader]
    private void load()
    {
        Add(new Container
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            RelativeSizeAxes = Axes.Both,
            Padding = new MarginPadding(10),
            Child = new BasicScrollContainer
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                ClampExtension = 20,
                ScrollbarVisible = false,
                Child = entryFlow = new FillFlowContainer<RequestEntry>
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Direction = FillDirection.Vertical,
                    Spacing = new Vector2(10),
                    Padding = new MarginPadding(10)
                }
            }
        });
    }

    public void AddRequest(RequestedBeatmapset beatmapset)
    {
        entryFlow.Add(new RequestEntry
        {
            Anchor = Anchor.TopCentre,
            Origin = Anchor.TopCentre,
            SourceBeatmapset = beatmapset
        });
    }
}
