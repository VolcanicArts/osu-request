using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Request.Game.Graphics.Tabs;

namespace osu.Request.Game
{
    public class OsuRequestGame : OsuRequestGameBase
    {
        [Cached]
        private readonly TabsManager TabsManager = new();

        [BackgroundDependencyLoader]
        private void load()
        {
            Children = new Drawable[]
            {
                TabsManager
            };
        }
    }
}
