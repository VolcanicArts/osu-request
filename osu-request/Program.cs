using osu.Framework;
using osu.Framework.Platform;

namespace osu_request
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            using GameHost host = Host.GetSuitableHost(@"osu!request");
            using Game game = new OsuRequest();
            host.Run(game);
        }
    }
}