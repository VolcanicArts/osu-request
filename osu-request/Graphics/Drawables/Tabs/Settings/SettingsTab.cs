using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu_request.Config;
using osu_request.Drawables.Notifications;
using osuTK;

namespace osu_request.Drawables
{
    public class SettingsTab : GenericTab
    {
        private OsuRequestButton _saveButton;
        
        private SettingContainer ChannelNameContainer;
        private SettingContainer PasscodeContainer;

        [Resolved]
        private OsuRequestConfig OsuRequestConfig { get; set; }

        [Resolved]
        private NotificationContainer NotificationContainer { get; set; }

        [BackgroundDependencyLoader]
        private void Load()
        {
            Children = new Drawable[]
            {
                new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.8f),
                    Margin = new MarginPadding
                    {
                        Bottom = 50
                    },
                    Child = new FillFlowContainer
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Direction = FillDirection.Vertical,
                        Spacing = new Vector2(0.0f, 10.0f),
                        Children = new Drawable[]
                        {
                            ChannelNameContainer = new SettingContainer
                            {
                                Setting = OsuRequestSetting.OsuClientId,
                                Prompt = "osu! client ID"
                            },
                            PasscodeContainer = new SecretSettingContainer
                            {
                                Setting = OsuRequestSetting.OsuClientSecret,
                                Prompt = "osu! client secret"
                            }
                        }
                    }
                },
                new Container
                {
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    RelativeSizeAxes = Axes.X,
                    Size = new Vector2(1.0f, 150.0f),
                    Child = _saveButton = new OsuRequestButton
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Size = new Vector2(0.2f, 0.4f),
                        Text = "Save Settings",
                        FontSize = 30,
                        CornerRadius = 5
                    }
                }
            };

            _saveButton.OnButtonClicked += SaveButtonClicked;
        }

        private void SaveButtonClicked()
        {
            OsuRequestConfig.GetBindable<string>(OsuRequestSetting.TwitchChannelName).Value = ChannelNameContainer.TextBox.Text;
            OsuRequestConfig.GetBindable<string>(OsuRequestSetting.TwitchOAuthToken).Value = PasscodeContainer.TextBox.Text;
            OsuRequestConfig.Save();
        }
    }
}