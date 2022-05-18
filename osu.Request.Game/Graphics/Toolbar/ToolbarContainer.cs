// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Request.Game.Graphics.Tabs;

namespace osu.Request.Game.Graphics.Toolbar;

public class ToolbarContainer : Container
{
    private FillFlowContainer<ToolbarItem> Items;

    [BackgroundDependencyLoader]
    private void load()
    {
        Anchor = Anchor.Centre;
        Origin = Anchor.Centre;
        RelativeSizeAxes = Axes.Both;

        Children = new Drawable[]
        {
            new Box
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                Colour = OsuRequestColour.Gray3
            },
            Items = new FillFlowContainer<ToolbarItem>
            {
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                RelativeSizeAxes = Axes.Both,
                Direction = FillDirection.Horizontal
            }
        };

        populateItems();
    }

    private void populateItems()
    {
        foreach (var tab in OsuRequestTab.TabList)
        {
            Items.Add(new ToolbarItem
            {
                Tab = tab
            });
        }
    }

    public void SelectItem(int id)
    {
        foreach (var item in Items)
        {
            item.DeSelect();
            if (item.Tab.Id == id) item.Select();
        }
    }
}
