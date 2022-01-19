using System;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;

namespace osu_request.Drawables
{
    public class Toolbar : Container
    {
        private FillFlowContainer<ToolbarItem> Items;
        public Action<int> NewSelectionEvent;

        [BackgroundDependencyLoader]
        private void Load()
        {
            Children = new Drawable[]
            {
                new Box
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
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

            var ItemNames = Enum.GetValues<Tabs>().Select(v => v.ToString()).ToList();

            for (var i = 0; i < ItemNames.Count; i++)
            {
                var toolbarItem = new ToolbarItem
                {
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                    RelativeSizeAxes = Axes.Y,
                    AutoSizeAxes = Axes.X,
                    Height = 1.0f,
                    ID = i,
                    Name = ItemNames[i].Replace("_", " ")
                };
                toolbarItem.OnSelected += e => NewSelectionEvent?.Invoke(e);
                Items.Add(toolbarItem);
            }
        }

        public void Select(int id)
        {
            foreach (var item in Items) item.Deselect();
            Items[id].Select(false);
        }
    }
}