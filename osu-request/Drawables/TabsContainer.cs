using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace osu_request.Drawables
{
    public class TabsContainer : Container
    {
        private Container _content;
        private int _selectedTab;
        private Drawable[] _tabs;
        private Toolbar _toolbar;
        [Cached] protected internal BindableBool Locked { get; } = new();

        [BackgroundDependencyLoader]
        private void Load()
        {
            InitSelf();
            InitChildren();
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
                    ItemNames = new[] { "Requests", "Settings" },
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

        public void Select(int id)
        {
            if (Locked.Value) return;
            _toolbar.Select(id);
            AnimateTabs(id);
        }

        private void AnimateTabs(int id)
        {
            if (Locked.Value) return;
            var diff = -(id - _selectedTab);
            _selectedTab = id;
            foreach (var tab in _tabs) tab.MoveToOffset(new Vector2(diff, 0.0f), 200, Easing.InOutQuart);
        }
    }
}