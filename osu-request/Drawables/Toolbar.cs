using System;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;
using osuTK.Graphics;

namespace osu_request.Drawables
{
    public class Toolbar : Container
    {
        protected internal int NumberOfItems { get; set; }
        private FillFlowContainer _items;
        public Action<int> NewSelectionEvent;

        [BackgroundDependencyLoader]
        private void Load()
        {
            Children = new Drawable[]
            {
                new Box
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(1.0f),
                    Colour = Color4.DarkSlateGray
                },
                _items = new FillFlowContainer
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(1.0f)
                }
            };

            for (var i = 0; i < NumberOfItems; i++)
            {
                var toolbarItem = new ToolbarItem
                {
                    ID = i,
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                    RelativeSizeAxes = Axes.Y,
                    Size = new Vector2(1.0f)
                };
                toolbarItem.OnSelected += ConvertSelection;
                _items.Add(toolbarItem);
            }
            
            Select(0);
        }

        public void Select(int id)
        {
            foreach (var item in _items.Cast<ToolbarItem>()) item.Deselect();
            ((ToolbarItem)_items[id]).Select();
        }

        private void ConvertSelection(ToolbarItem selectedItem)
        {
            foreach (var item in _items.Cast<ToolbarItem>().Where(item => !item.Equals(selectedItem))) item.Deselect();
            NewSelectionEvent?.Invoke(selectedItem.ID);
        }
    }
}