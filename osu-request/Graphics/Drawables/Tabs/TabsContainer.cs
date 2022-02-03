using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu_request.Drawables.Bans;
using osu_request.Drawables.Users;
using osuTK;

namespace osu_request.Drawables;

public class TabsContainer : Container
{
    private GenericTab[] Tabs;
    private Toolbar Toolbar;
    private int CurrentTabId;

    [BackgroundDependencyLoader]
    private void Load()
    {
        InternalChildren = new Drawable[]
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
            Toolbar = new Toolbar
            {
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                RelativeSizeAxes = Axes.X,
                Size = new Vector2(1.0f, 60.0f),
                NewSelectionEvent = id => Select((Tabs)id)
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
                Children = Tabs = new GenericTab[]
                {
                    new RequestsTab
                    {
                        Position = new Vector2(0.0f, 0.0f)
                    },
                    new BeatmapsetBansTab
                    {
                        Position = new Vector2(-1.0f, 0.0f)
                    },
                    new UserBansTab
                    {
                        Position = new Vector2(-1.0f, 0.0f)
                    },
                    new SettingsTab
                    {
                        Position = new Vector2(-1.0f, 0.0f)
                    }
                }
            }
        };
    }

    public void Select(Tabs tab)
    {
        Scheduler.Add(() => select(tab));
    }

    private void select(Tabs tab)
    {
        var id = Convert.ToInt32(tab);
        Toolbar.Select(id);
        AnimateTabs(id);
        CurrentTabId = id;
    }

    private void AnimateTabs(int id)
    {
        if (id == CurrentTabId) return;

        var newPosition = id > CurrentTabId ? 1 : -1;
        Tabs[id].MoveToX(newPosition).Then().MoveToX(0.0f, 200, Easing.InOutQuart);
        Tabs[CurrentTabId].MoveToX(-newPosition, 200, Easing.InOutQuart);
    }
}