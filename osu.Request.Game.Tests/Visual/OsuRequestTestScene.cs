// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using osu.Framework.Testing;

namespace osu.Request.Game.Tests.Visual
{
    public class OsuRequestTestScene : TestScene
    {
        protected override ITestSceneTestRunner CreateRunner() => new OsuRequestTestSceneTestRunner();

        private class OsuRequestTestSceneTestRunner : OsuRequestGameBase, ITestSceneTestRunner
        {
            private TestSceneTestRunner.TestRunner runner;

            protected override void LoadAsyncComplete()
            {
                base.LoadAsyncComplete();
                Add(runner = new TestSceneTestRunner.TestRunner());
            }

            public void RunTestBlocking(TestScene test) => runner.RunTestBlocking(test);
        }
    }
}
