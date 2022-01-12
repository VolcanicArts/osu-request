using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu_request.Drawables.Bans;
using osu_request.Drawables.Users;
using osuTK;

namespace osu_request.Drawables
{
    public class TabsContainer : Container
    {
        private GenericTab[] _tabs;
        private Toolbar _toolbar;

        [BackgroundDependencyLoader]
        private void Load()
        {
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
                    Children = _tabs = new GenericTab[]
                    {
                        new RequestsTab
                        {
                            Position = new Vector2(0.0f, 0.0f)
                        },
                        new BeatmapsetBansTab
                        {
                            Position = new Vector2(1.0f, 0.0f)
                        },
                        new UserBansTab
                        {
                            Position = new Vector2(2.0f, 0.0f)
                        },
                        new SettingsTab
                        {
                            Position = new Vector2(3.0f, 0.0f)
                        }
                    }
                }
            };

            _toolbar.NewSelectionEvent += id => Select((Tabs)id);
        }

        public void Select(Tabs tab)
        {
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