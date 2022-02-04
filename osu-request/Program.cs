using osu.Framework;
using osu.Framework.Platform;

namespace osu_request;

internal static class Program
{
    private static void Main()
    {
        using GameHost host = Host.GetSuitableDesktopHost(@"osu!request");
        using Game game = new OsuRequest();
        host.Run(game);
    }
}