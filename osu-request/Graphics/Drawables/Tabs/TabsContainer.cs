using System;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu_request.Drawables.Bans;
using osu_request.Drawables.Users;
using osuTK;

namespace osu_request.Drawables
{
    public class TabsContainer : Container
    {
        [Cached]
        private readonly BindableBool Locked = new();

        private Drawable[] _tabs;
        private Toolbar _toolbar;

        private Tabs CurrentTab;

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
                new RequestsTab
                {
                    RelativePositionAxes = Axes.Both,
                    Position = new Vector2(0.0f, 0.0f)
                },
                new BansTab
                {
                    RelativePositionAxes = Axes.Both,
                    Position = new Vector2(1.0f, 0.0f)
                },
                new UsersTab
                {
                    RelativePositionAxes = Axes.Both,
                    Position = new Vector2(2.0f, 0.0f)
                },
                new SettingsTab
                {
                    RelativePositionAxes = Axes.Both,
                    Position = new Vector2(3.0f, 0.0f)
                }
            };

            Children = new Drawable[]
            {
                new TrianglesBackground
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    ColourLight = OsuRequestColour.Gray7,
                    ColourDark = OsuRequestColour.Gray4,
                    TriangleScale = 4
                },
                _toolbar = new Toolbar
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    RelativeSizeAxes = Axes.X,
                    Size = new Vector2(1.0f, 60.0f)
                },
                new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding
                    {
                        Top = 60
                    },
                    Children = _tabs
                }
            };

            _toolbar.NewSelectionEvent += id => Select((Tabs)id);
        }

        public void Override(Tabs tab)
        {
            Locked.Value = false;
            Select(tab, true);
            Locked.Value = true;
        }

        public void Release()
        {
            Locked.Value = false;
            Select(CurrentTab, false);
        }

        public void ReleaseAndSelect(Tabs tab)
        {
            Locked.Value = false;
            Select(tab, false);
        }

        public void Select(Tabs tab)
        {
            Select(tab, false);
        }

        private void Select(Tabs tab, bool overriding)
        {
            if (Locked.Value) return;
            if (!overriding) CurrentTab = tab;

            var id = Convert.ToInt32(tab);
            _toolbar.Select(id);
            AnimateTabs(id);
        }

        private void AnimateTabs(int id)
        {
            for (var i = 0; i < _tabs.Length; i++)
            {
                var tab = _tabs[i];
                var pos = i - id;
                tab.MoveTo(new Vector2(pos, 0.0f), 200, Easing.InOutQuart);
            }
        }
    }
}