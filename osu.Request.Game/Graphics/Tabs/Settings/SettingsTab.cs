// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Request.Game.Configuration;
using osu.Request.Game.Graphics.UI.Button;
using osu.Request.Game.Graphics.UI.Text;
using osu.Request.Game.Remote;
using osuTK;

namespace osu.Request.Game.Graphics.Tabs.Settings;

public class SettingsTab : BaseTab
{
    private CredentialsContainer credentials;

    [Resolved]
    private OsuRequestConfig OsuRequestConfig { get; set; }

    [Resolved]
    private WebSocketClient WebSocketClient { get; set; }

    [BackgroundDependencyLoader]
    private void load()
    {
        Child = new Container
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            RelativeSizeAxes = Axes.Both,
            Padding = new MarginPadding(10),
            Child = new GridContainer
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                RowDimensions = new[]
                {
                    new Dimension(GridSizeMode.Absolute, 50),
                    new Dimension(),
                    new Dimension(GridSizeMode.Absolute, 300)
                },
                Content = new[]
                {
                    new Drawable[]
                    {
                        new OsuRequestTextFlowContainer(t => t.Font = OsuRequestFonts.REGULAR.With(size: 100))
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            TextAnchor = Anchor.Centre,
                            Text = "Settings",
                        }
                    },
                    new Drawable[]
                    {
                        credentials = new CredentialsContainer
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            AutoSizeAxes = Axes.Both
                        }
                    },
                    new Drawable[]
                    {
                        new TextButton
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Size = new Vector2(200, 50),
                            Colour = OsuRequestColour.Blue,
                            Text = "Save Settings",
                            Action = saveSettings
                        }
                    }
                }
            }
        };
    }

    private void saveSettings()
    {
        OsuRequestConfig.GetBindable<string>(OsuRequestSetting.Username).Value = credentials.Username;
        OsuRequestConfig.GetBindable<string>(OsuRequestSetting.Passcode).Value = credentials.Passcode;
        OsuRequestConfig.Save();
        WebSocketClient.ConnectOrReconnect();
    }
}
