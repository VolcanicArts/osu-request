using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;
using osuTK.Graphics;

namespace osu_request.Drawables.Bans
{
    public class BansTab : GenericTab
    {
        [BackgroundDependencyLoader]
        private void Load()
        {
            Padding = new MarginPadding(20);
            
            Child = new BeatmapsetBansList
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both
            };
        }
    }
}