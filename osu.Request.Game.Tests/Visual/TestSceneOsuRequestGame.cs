using osu.Framework.Allocation;
using osu.Framework.Platform;

namespace osu.Request.Game.Tests.Visual
{
    public class TestSceneOsuRequestGame : OsuRequestTestScene
    {
        // Add visual tests to ensure correct behaviour of your game: https://github.com/ppy/osu-framework/wiki/Development-and-Testing
        // You can make changes to classes associated with the tests and they will recompile and update immediately.

        private OsuRequestGame game;

        [BackgroundDependencyLoader]
        private void load(GameHost host)
        {
            game = new OsuRequestGame();
            game.SetHost(host);

            AddGame(game);
        }
    }
}
