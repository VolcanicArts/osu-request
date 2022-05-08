// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Request.Game.Graphics.Tabs.BeatmapsetBans;
using osu.Request.Game.Graphics.Tabs.Requests;
using osu.Request.Game.Graphics.Tabs.Settings;
using osu.Request.Game.Graphics.Tabs.UserBans;
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
                Position = new Vector2(Convert.ToInt32(OsuRequestTab.Requests), 0.0f)
            },
            new BeatmapsetBansTab
            {
                Position = new Vector2(Convert.ToInt32(OsuRequestTab.BeatmapsetBans), 0.0f)
            },
            new UserBansTab
            {
                Position = new Vector2(Convert.ToInt32(OsuRequestTab.UserBans), 0.0f)
            },
            new SettingsTab
            {
                Position = new Vector2(Convert.ToInt32(OsuRequestTab.Settings), 0.0f)
            }
        };
    }

    public void AnimateTo(int oldId, int newId)
    {
        if (newId == oldId) return;

        var newPosition = newId > oldId ? 1 : -1;
        Children[newId].MoveToX(newPosition).Then().MoveToX(0.0f, 200, Easing.InOutQuart);
        Children[oldId].MoveToX(-newPosition, 200, Easing.InOutQuart);
    }
}
