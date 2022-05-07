// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

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
