using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.IO.Stores;
using osu.Request.Resources;
using osuTK;

namespace osu.Request.Game
{
    public class OsuRequestGameBase : osu.Framework.Game
    {
        protected override Container<Drawable> Content { get; }

        protected OsuRequestGameBase()
        {
            base.Content.Add(Content = new DrawSizePreservingFillContainer
            {
                TargetDrawSize = new Vector2(1366, 768)
            });
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Resources.AddStore(new DllResourceStore(typeof(OsuRequestResources).Assembly));
        }
    }
}
