using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace osu_request.Drawables
{
    public class TabsContainer : Container
    {
        private Toolbar _toolbar;
        private Container _content;
        private Drawable[] _tabs;
        private int _selectedTab;

        [BackgroundDependencyLoader]
        private void Load()
        {
            InitSelf();
            InitChildren();
            Select(0);
        }

        private void InitSelf()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;
        }

        private void InitChildren()
        {
            _tabs = new Drawable[]
            {
                new RequestsListingTab
                {
                    RelativePositionAxes = Axes.Both,
                    Position = new Vector2(0.0f, 0.0f)
                },
                new SettingsTab
                {
                    RelativePositionAxes = Axes.Both,
                    Position = new Vector2(1.0f, 0.0f)
                }
            };
            
            Children = new Drawable[]
            {
                _toolbar = new Toolbar
                {
                    ItemNames = new[] {"Requests", "Settings"},
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    RelativeSizeAxes = Axes.X,
                    Size = new Vector2(1.0f, 60.0f)
                },
                _content = new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding
                    {
                        Top = 60.0f
                    },
                    Children = _tabs
                }
            };

            _toolbar.NewSelectionEvent += Select;
        }

        private void Select(int id)
        {
            var diff = -(id - _selectedTab);
            _selectedTab = id;
            foreach (var tab in _tabs)
            {
                tab.MoveToOffset(new Vector2(diff, 0.0f), 200, Easing.InOutQuart);
            }
        }
    }
}