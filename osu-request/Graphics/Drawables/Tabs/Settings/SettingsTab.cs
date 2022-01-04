using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu_request.Config;
using osuTK;

namespace osu_request.Drawables
{
    public class SettingsTab : Container
    {
        private ClientManager _clientManager;
        private OsuRequestConfig _osuRequestConfig;
        private OsuRequestButton _saveButton;
        private SettingContainer OsuClientIDContainer;
        private SettingContainer OsuClientSecretContainer;
        private SettingContainer TwitchClientChannelNameContainer;
        private SettingContainer TwitchClientOAuthTokenContainer;

        [BackgroundDependencyLoader]
        private void Load(OsuRequestConfig osuRequestConfig, ClientManager clientManager)
        {
            _osuRequestConfig = osuRequestConfig;
            _clientManager = clientManager;
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
            Children = new Drawable[]
            {
                new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.8f, 0.4f),
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
                        Spacing = new Vector2(0, 10),
                        Children = new Drawable[]
                        {
                            OsuClientIDContainer = new SettingContainer
                            {
                                Setting = OsuRequestSetting.OsuClientId,
                                Prompt = "Enter your osu! client ID"
                            },
                            OsuClientSecretContainer = new SecretSettingContainer
                            {
                                Setting = OsuRequestSetting.OsuClientSecret,
                                Prompt = "Enter your osu! client secret"
                            },
                            TwitchClientChannelNameContainer = new SettingContainer
                            {
                                Setting = OsuRequestSetting.TwitchChannelName,
                                Prompt = "Enter your Twitch channel name"
                            },
                            TwitchClientOAuthTokenContainer = new SecretSettingContainer
                            {
                                Setting = OsuRequestSetting.TwitchOAuthToken,
                                Prompt = "Enter your Twitch OAuth code"
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
                        Text = "Save Settings"
                    }
                }
            };

            _saveButton.OnButtonClicked += SaveButtonClicked;
        }

        private void SaveButtonClicked()
        {
            _osuRequestConfig.GetBindable<string>(OsuRequestSetting.OsuClientId).Value = OsuClientIDContainer.TextBox.Text;
            _osuRequestConfig.GetBindable<string>(OsuRequestSetting.OsuClientSecret).Value = OsuClientSecretContainer.TextBox.Text;
            _osuRequestConfig.GetBindable<string>(OsuRequestSetting.TwitchChannelName).Value = TwitchClientChannelNameContainer.TextBox.Text;
            _osuRequestConfig.GetBindable<string>(OsuRequestSetting.TwitchOAuthToken).Value = TwitchClientOAuthTokenContainer.TextBox.Text;
            _osuRequestConfig.Save();
            _clientManager.TryConnectClients(_osuRequestConfig);
        }
    }
}