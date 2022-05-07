// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using osu.Framework;
using osu.Framework.Platform;
using osu.Request.Game;

namespace osu.Request.Desktop
{
    public static class Program
    {
        public static void Main()
        {
            using GameHost host = Host.GetSuitableHost(@"osu!request");
            using Framework.Game game = new OsuRequestGame();
            host.Run(game);
        }
    }
}
