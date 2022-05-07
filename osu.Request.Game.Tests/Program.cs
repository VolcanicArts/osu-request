// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using osu.Framework;
using osu.Framework.Platform;

namespace osu.Request.Game.Tests
{
    public static class Program
    {
        public static void Main()
        {
            using GameHost host = Host.GetSuitableHost("visual-tests");
            using var game = new OsuRequestTestBrowser();
            host.Run(game);
        }
    }
}
