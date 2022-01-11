using System;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu_request.Config;
using osu_request.Drawables.Notifications;
using osuTK;

namespace osu_request.Drawables
{
    public class SettingsTab : GenericTab
    {
        private ClientManager _clientManager;
        private OsuRequestConfig _osuRequestConfig;
        private OsuRequestButton _saveButton;
        private NotificationContainer NotificationContainer;
        private SettingContainer OsuClientIDContainer;
        private SettingContainer OsuClientSecretContainer;
        private SettingContainer TwitchClientChannelNameContainer;
        private SettingContainer TwitchClientOAuthTokenContainer;

        [BackgroundDependencyLoader]
        private void Load(OsuRequestConfig osuRequestConfig, ClientManager clientManager, NotificationContainer notificationContainer)
        {
            _osuRequestConfig = osuRequestConfig;
            _clientManager = clientManager;
            NotificationContainer = notificationContainer;

            TextFlowContainer _text;

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
                            OsuClientIDContainer = new SettingContainer
                            {
                                Setting = OsuRequestSetting.OsuClientId,
                                Prompt = "osu! client ID"
                            },
                            OsuClientSecretContainer = new SecretSettingContainer
                            {
                                Setting = OsuRequestSetting.OsuClientSecret,
                                Prompt = "osu! client secret"
                            },
                            TwitchClientChannelNameContainer = new SettingContainer
                            {
                                Setting = OsuRequestSetting.TwitchChannelName,
                                Prompt = "Twitch channel name"
                            },
                            TwitchClientOAuthTokenContainer = new SecretSettingContainer
                            {
                                Setting = OsuRequestSetting.TwitchOAuthToken,
                                Prompt = "Twitch OAuth code"
                            }
                        }
                    }
                },
                new Container
                {
                    Anchor = Anchor.BottomRight,
                    Origin = Anchor.BottomRight,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.2f, 0.1f),
                    Margin = new MarginPadding(20),
                    Child = _saveButton = new OsuRequestButton
                    {
                        Anchor = Anchor.BottomRight,
                        Origin = Anchor.BottomRight,
                        RelativeSizeAxes = Axes.Both,
                        Size = new Vector2(0.9f),
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
            _clientManager.OnFailed += ClientManagerFail;
            _clientManager.OnSuccess += ClientManagerSuccess;
            
            _osuRequestConfig.GetBindable<string>(OsuRequestSetting.OsuClientId).Value = OsuClientIDContainer.TextBox.Text;
            _osuRequestConfig.GetBindable<string>(OsuRequestSetting.OsuClientSecret).Value = OsuClientSecretContainer.TextBox.Text;
            _osuRequestConfig.GetBindable<string>(OsuRequestSetting.TwitchChannelName).Value = TwitchClientChannelNameContainer.TextBox.Text;
            _osuRequestConfig.GetBindable<string>(OsuRequestSetting.TwitchOAuthToken).Value = TwitchClientOAuthTokenContainer.TextBox.Text;
            _osuRequestConfig.Save();
            _clientManager.TryConnectClients(_osuRequestConfig);
        }

        private void ClientManagerFail()
        {
            NotificationContainer.Notify("Invalid Settings", "Please enter valid settings to allow this app to work");
            _clientManager.OnFailed -= ClientManagerFail;
            _clientManager.OnSuccess -= ClientManagerSuccess;
        }

        private void ClientManagerSuccess()
        {
            NotificationContainer.Notify("Valid Settings", "Settings accepted. Requests incoming!");
            _clientManager.OnFailed -= ClientManagerFail;
            _clientManager.OnSuccess -= ClientManagerSuccess;
        }
    }
}