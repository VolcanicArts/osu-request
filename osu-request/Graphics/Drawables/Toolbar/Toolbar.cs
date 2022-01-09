using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace osu_request.Drawables
{
    public class Toolbar : Container
    {
        private readonly List<ToolbarItem> _items = new();
        private FillFlowContainer _fillFlowContainer;
        public Action<int> NewSelectionEvent;

        [Resolved]
        private BindableBool Locked { get; init; }

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
                    Size = new Vector2(1.0f, 0.9f),
                    Colour = OsuRequestColour.GreyLimeDark
                },
                new Box
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(1.0f, 0.4f),
                    RelativePositionAxes = Axes.Both,
                    Position = new Vector2(0.0f, 0.9f),
                    Colour = ColourInfo.GradientVertical(OsuRequestColour.GreyLimeDark, OsuRequestColour.GreyLimeDark.Opacity(0.0f))
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