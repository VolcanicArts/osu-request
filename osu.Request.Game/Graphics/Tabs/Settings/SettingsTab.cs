// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Request.Game.Configuration;
using osu.Request.Game.Graphics.UI.Button;
using osuTK;

namespace osu.Request.Game.Graphics.Tabs.Settings;

public class SettingsTab : BaseTab
{
    private SettingContainer UsernameSetting;
    private MaskedSettingContainer PasscodeSetting;

    [Resolved]
    private OsuRequestConfig OsuRequestConfig { get; set; }

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
                        new TextFlowContainer(t => t.Font = OsuRequestFonts.REGULAR.With(size: 100))
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
                        new FillFlowContainer
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            Direction = FillDirection.Vertical,
                            Spacing = new Vector2(0.0f, 10.0f),
                            Children = new Drawable[]
                            {
                                new Container
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    Size = new Vector2(800, 100),
                                    Child = UsernameSetting = new SettingContainer
                                    {
                                        Anchor = Anchor.Centre,
                                        Origin = Anchor.Centre,
                                        RelativeSizeAxes = Axes.Both,
                                        Title = "Username",
                                        Setting = OsuRequestSetting.Username
                                    }
                                },
                                new Container
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    Size = new Vector2(800, 100),
                                    Child = PasscodeSetting = new MaskedSettingContainer
                                    {
                                        Anchor = Anchor.Centre,
                                        Origin = Anchor.Centre,
                                        RelativeSizeAxes = Axes.Both,
                                        Title = "Passcode",
                                        Setting = OsuRequestSetting.Passcode
                                    }
                                }
                            }
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
        OsuRequestConfig.GetBindable<string>(OsuRequestSetting.Username).Value = UsernameSetting.SettingValue;
        OsuRequestConfig.GetBindable<string>(OsuRequestSetting.Passcode).Value = PasscodeSetting.SettingValue;
        OsuRequestConfig.Save();
    }
}
