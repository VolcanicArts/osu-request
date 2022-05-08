﻿// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Request.Game.Graphics.Tabs.BeatmapsetBans;
using osu.Request.Game.Graphics.Tabs.Requests;
using osuTK;

namespace osu.Request.Game.Graphics.Tabs;

public class TabsContainer : Container
{
    [BackgroundDependencyLoader]
    private void load()
    {
        Anchor = Anchor.Centre;
        Origin = Anchor.Centre;
        RelativeSizeAxes = Axes.Both;

        Children = new BaseTab[]
        {
            new RequestsTab
            {
                Position = new Vector2(0.0f, 0.0f)
            },
            new BeatmapsetBansTab
            {
                Position = new Vector2(1.0f, 0.0f)
            }
        };
    }
}
