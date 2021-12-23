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
        private FillFlowContainer _items;

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
                    Colour = Color4.Firebrick
                },
                _items = new FillFlowContainer
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(1.0f)
                }
            };

            for (var i = 0; i < 5; i++)
            {
                var toolbarItem = new ToolbarItem();
                _items.Add(toolbarItem);
            }
        }
    }
}