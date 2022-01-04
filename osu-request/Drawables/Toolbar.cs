using System;
using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK.Graphics;

namespace osu_request.Drawables
{
    public class Toolbar : Container
    {
        private readonly List<ToolbarItem> _items = new();
        private FillFlowContainer _fillFlowContainer;
        private BindableBool Locked;
        public Action<int> NewSelectionEvent;
        protected internal string[] ItemNames { get; init; }

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