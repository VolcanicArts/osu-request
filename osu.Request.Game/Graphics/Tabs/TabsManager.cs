// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using System;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Request.Game.Graphics.Toolbar;
using osu.Request.Game.Graphics.Triangles;

namespace osu.Request.Game.Graphics.Tabs;

public sealed class TabsManager : Container
{
    private const int toolbar_height = 60;
    private const OsuRequestTab default_tab = OsuRequestTab.Requests;

    public BindableInt CurrentTabId { get; } = new();

    public TabsManager()
    {
        Anchor = Anchor.Centre;
        Origin = Anchor.Centre;
        RelativeSizeAxes = Axes.Both;
    }

    [BackgroundDependencyLoader]
    private void load()
    {
        ToolbarContainer toolbarContainer;
        TabsContainer tabsContainer;

        Children = new Drawable[]
        {
            new TrianglesBackground
            {
                ColourLight = OsuRequestColour.Gray7,
                ColourDark = OsuRequestColour.Gray4,
                TriangleScale = 4
            },
            new GridContainer
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                RowDimensions = new[]
                {
                    new Dimension(GridSizeMode.Absolute, toolbar_height),
                    new Dimension(GridSizeMode.Distributed)
                },
                Content = new[]
                {
                    new Drawable[]
                    {
                        toolbarContainer = new ToolbarContainer()
                    },
                    new Drawable[]
                    {
                        tabsContainer = new TabsContainer()
                    }
                }
            }
        };

        Select(default_tab);
        CurrentTabId.BindValueChanged(e => toolbarContainer.SelectItem(e.NewValue), true);
        CurrentTabId.BindValueChanged(e => tabsContainer.AnimateTo(e.OldValue, e.NewValue), true);
    }

    public void Select(OsuRequestTab tab)
    {
        var id = Convert.ToInt32(tab);
        Select(id);
    }

    public void Select(int id)
    {
        Scheduler.Add(() => select(id));
    }

    private void select(int id)
    {
        CurrentTabId.Value = id;
    }
}
