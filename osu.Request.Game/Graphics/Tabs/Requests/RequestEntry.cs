// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

#nullable enable
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Request.Game.Beatmaps;
using osu.Request.Game.Graphics.Beatmaps;
using osuTK;

namespace osu.Request.Game.Graphics.Tabs.Requests;

public sealed class RequestEntry : Container
{
    private const int padding = 3;

    public WorkingBeatmapset SourceBeatmapset { get; init; }

    private DependencyContainer? requestDependencies;

    public RequestEntry()
    {
        Size = new Vector2(800, 100);
        Masking = true;
        CornerRadius = 10;
    }

    protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent)
    {
        return requestDependencies = new DependencyContainer(base.CreateChildDependencies(parent));
    }

    [BackgroundDependencyLoader]
    private void load()
    {
        requestDependencies?.CacheAs(SourceBeatmapset);

        Children = new Drawable[]
        {
            new Box
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                Colour = OsuRequestColour.Gray4
            },
            new Container
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                Padding = new MarginPadding(padding),
                Child = new GridContainer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    ColumnDimensions = new[]
                    {
                        new Dimension(GridSizeMode.Absolute, 40),
                        new Dimension(GridSizeMode.Distributed),
                        new Dimension(GridSizeMode.Absolute, 100)
                    },
                    Content = new[]
                    {
                        new Drawable[]
                        {
                            new RequestEntryAction
                            {
                                Padding = new MarginPadding
                                {
                                    Right = padding / 2f
                                },
                                CornerRadius = 10,
                                Colour = OsuRequestColour.Green,
                                Icon = FontAwesome.Solid.Check,
                                Action = () =>
                                {
                                    // TODO Implement removal of self
                                }
                            },
                            new Container
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                RelativeSizeAxes = Axes.Both,
                                Padding = new MarginPadding
                                {
                                    Horizontal = padding / 2f
                                },
                                Child = new BeatmapsetCard
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    RelativeSizeAxes = Axes.Both,
                                    CornerRadius = 10
                                }
                            },
                            new Container
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                RelativeSizeAxes = Axes.Both,
                                Padding = new MarginPadding
                                {
                                    Left = padding / 2f
                                },
                                Child = new RequestEntryActionsGrid()
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    RelativeSizeAxes = Axes.Both,
                                    Masking = true,
                                    CornerRadius = 10
                                }
                            },
                        }
                    }
                }
            }
        };
    }
}
