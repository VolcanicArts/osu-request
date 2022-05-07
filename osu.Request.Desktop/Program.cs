using osu.Framework.Platform;
using osu.Framework;
using osu.Request.Game;

namespace osu.Request.Desktop
{
    public static class Program
    {
        public static void Main()
        {
            using GameHost host = Host.GetSuitableHost(@"osu!request");
            using osu.Framework.Game game = new OsuRequestGame();
            host.Run(game);
        }
    }
}
