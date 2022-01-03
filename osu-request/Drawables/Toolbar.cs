using System;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK.Graphics;

namespace osu_request.Drawables
{
    public class Toolbar : Container
    {
        private FillFlowContainer _items;
        private BindableBool Locked;
        public Action<int> NewSelectionEvent;
        protected internal string[] ItemNames { get; init; }

        [BackgroundDependencyLoader]
        private void Load(BindableBool locked)
        {
            Locked = locked;
            Children = new Drawable[]
            {
                new BackgroundContainer(Color4.DarkSlateGray),
                _items = new FillFlowContainer
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    RelativeSizeAxes = Axes.Both
                }
            };

            for (var i = 0; i < ItemNames.Length; i++)
            {
                var toolbarItem = new ToolbarItem
                {
                    ID = i,
                    Name = ItemNames[i]
                };
                toolbarItem.OnSelected += ConvertSelection;
                _items.Add(toolbarItem);
            }
        }

        public void Select(int id)
        {
            if (Locked.Value) return;
            foreach (var item in _items.Cast<ToolbarItem>()) item.Deselect();
            ((ToolbarItem)_items[id]).Select();
        }

        private void ConvertSelection(ToolbarItem selectedItem)
        {
            if (Locked.Value) return;
            foreach (var item in _items.Cast<ToolbarItem>().Where(item => !item.Equals(selectedItem))) item.Deselect();
            NewSelectionEvent?.Invoke(selectedItem.ID);
        }
    }
}