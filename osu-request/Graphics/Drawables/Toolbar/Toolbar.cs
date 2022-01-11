using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;

namespace osu_request.Drawables
{
    public class Toolbar : Container
    {
        private readonly List<ToolbarItem> _items = new();
        private FillFlowContainer _fillFlowContainer;
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
                _fillFlowContainer = new FillFlowContainer
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
                    ID = i,
                    Name = ItemNames[i].Replace("_", " ")
                };
                toolbarItem.OnSelected += e => NewSelectionEvent?.Invoke(e);
                _fillFlowContainer.Add(toolbarItem);
                _items.Add(toolbarItem);
            }
        }

        public void Select(int id)
        {
            foreach (var item in _items) item.Deselect();
            _items[id].Select(false);
        }
    }
}