// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.IO.Stores;
using osu.Framework.Platform;
using osu.Request.Game.Configuration;
using osu.Request.Resources;
using osuTK;

namespace osu.Request.Game
{
    public class OsuRequestGameBase : Framework.Game
    {
        protected override Container<Drawable> Content { get; }

        protected new DependencyContainer Dependencies;

        private OsuRequestConfig OsuRequestConfig;

        protected OsuRequestGameBase()
        {
            base.Content.Add(Content = new DrawSizePreservingFillContainer
            {
                TargetDrawSize = new Vector2(1366, 768)
            });
        }

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent)
        {
            return Dependencies = new DependencyContainer(base.CreateChildDependencies(parent));
        }

        [BackgroundDependencyLoader]
        private void load(Storage storage)
        {
            OsuRequestConfig = new OsuRequestConfig(storage);
            Dependencies.CacheAs(OsuRequestConfig);
            Resources.AddStore(new DllResourceStore(typeof(OsuRequestResources).Assembly));
        }
    }
}
