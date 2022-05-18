// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Request.Game.Configuration;
using osu.Request.Game.Graphics.UI.Text;
using osuTK;

namespace osu.Request.Game.Graphics.Tabs.Settings;

public class CredentialsContainer : Container
{
    public string Username => usernameSetting.SettingValue;
    public string Passcode => passcodeSetting.SettingValue;

    private SettingContainer usernameSetting;
    private SettingContainer passcodeSetting;

    [BackgroundDependencyLoader]
    private void load()
    {
        Masking = true;
        CornerRadius = 10;

        Children = new Drawable[]
        {
            new Box
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                Colour = OsuRequestColour.Gray2
            },
            new FillFlowContainer
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                AutoSizeAxes = Axes.Both,
                Direction = FillDirection.Vertical,
                Children = new Drawable[]
                {
                    new OsuRequestTextFlowContainer(t => t.Font = OsuRequestFonts.REGULAR.With(size: 60))
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        TextAnchor = Anchor.TopCentre,
                        AutoSizeAxes = Axes.Y,
                        RelativeSizeAxes = Axes.X,
                        Padding = new MarginPadding
                        {
                            Top = 7
                        },
                        Text = "Credentials"
                    },
                    new Container
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Size = new Vector2(800, 75),
                        Child = usernameSetting = new SettingContainer
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            Placeholder = "Username",
                            Setting = OsuRequestSetting.Username
                        }
                    },
                    new Container
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Size = new Vector2(800, 75),
                        Child = passcodeSetting = new MaskedSettingContainer
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            Placeholder = "Passcode",
                            Setting = OsuRequestSetting.Passcode
                        }
                    }
                }
            }
        };
    }
}
