// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Platform;
using osu.Request.Game.Beatmaps;

namespace osu.Request.Game.Graphics.Tabs.Requests;

public class RequestEntryActionsGrid : Container
{
    [Resolved]
    private WorkingBeatmapset WorkingBeatmapset { get; set; }

    [BackgroundDependencyLoader]
    private void load(TextureStore textureStore, GameHost host)
    {
        Children = new Drawable[]
        {
            new Box
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                Colour = OsuRequestColour.Gray2
            },
            new Container
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                Padding = new MarginPadding(2),
                Child = new GridContainer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    ColumnDimensions = new[]
                    {
                        new Dimension(),
                        new Dimension()
                    },
                    RowDimensions = new[]
                    {
                        new Dimension(),
                        new Dimension()
                    },
                    Content = new[]
                    {
                        new Drawable[]
                        {
                            new RequestEntryAction
                            {
                                Padding = new MarginPadding(2),
                                CornerRadius = 5,
                                Colour = OsuRequestColour.BlueDark,
                                Icon = FontAwesome.Solid.Download,
                                Action = () => host.OpenUrlExternally(WorkingBeatmapset.DirectUrl)
                            },
                            new RequestEntryAction
                            {
                                Padding = new MarginPadding(2),
                                CornerRadius = 5,
                                Colour = OsuRequestColour.BlueDark,
                                Icon = FontAwesome.Solid.ExternalLinkAlt,
                                Action = () => host.OpenUrlExternally(WorkingBeatmapset.ExternalUrl)
                            }
                        },
                        new Drawable[]
                        {
                            new RequestEntryAction
                            {
                                Padding = new MarginPadding(2),
                                CornerRadius = 5,
                                Colour = OsuRequestColour.RedDark,
                                Icon = FontAwesome.Solid.Ban,
                                Action = () =>
                                {
                                    // TODO send ban to websocket
                                }
                            },
                            new RequestEntryAction
                            {
                                Padding = new MarginPadding(2),
                                CornerRadius = 5,
                                Colour = OsuRequestColour.RedDark,
                                Icon = FontAwesome.Solid.UserSlash,
                                Action = () =>
                                {
                                    // TODO send ban to websocket
                                }
                            }
                        }
                    }
                }
            }
        };
    }
}
