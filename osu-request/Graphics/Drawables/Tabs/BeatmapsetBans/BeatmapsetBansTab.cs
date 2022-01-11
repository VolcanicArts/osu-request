using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu_request.Graphics.Drawables.Tabs.BeatmapsetBans;
using osuTK;

namespace osu_request.Drawables.Bans
{
    public class BansTab : GenericTab
    {
        [BackgroundDependencyLoader]
        private void Load()
        {
            Padding = new MarginPadding(20);

            Children = new Drawable[]
            {
                new BeatmapsetBansTextBox
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    RelativeSizeAxes = Axes.X,
                    Size = new Vector2(1.0f, 80.0f)
                },
                new BeatmapsetBansList
                {
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding
                    {
                        Top = 90
                    }
                }
            };
        }
    }
}