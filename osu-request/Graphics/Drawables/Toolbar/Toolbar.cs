using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace osu_request.Drawables
{
    public class Toolbar : Container
    {
        private readonly List<ToolbarItem> _items = new();
        private FillFlowContainer _fillFlowContainer;
        private BindableBool Locked;
        public Action<int> NewSelectionEvent;

        [BackgroundDependencyLoader]
        private void Load(BindableBool locked)
        {
            Locked = locked;
            Children = new Drawable[]
            {
                new BackgroundColour
                {
                    Colour = OsuRequestColour.GreyCyanDark
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
            if (Locked.Value) return;
            foreach (var item in _items) item.Deselect();
            _items[id].Select(false);
        }
    }
}