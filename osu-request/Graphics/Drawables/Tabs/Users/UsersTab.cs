using osu.Framework.Allocation;
using osu.Framework.Graphics;

namespace osu_request.Drawables.Users
{
    public class UsersTab : GenericTab
    {
        [BackgroundDependencyLoader]
        private void Load()
        {
            Padding = new MarginPadding(20);

            Child = new UsersList
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both
            };
        }
    }
}