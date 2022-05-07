using osu.Framework.Allocation;
using osu.Framework.Platform;

namespace osu.Request.Game.Tests.Visual
{
    public class TestSceneOsuRequestGame : OsuRequestTestScene
    {
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
