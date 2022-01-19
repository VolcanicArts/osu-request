using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Platform;
using osu_request.Config;
using osu_request.Drawables.Notifications;
using osu_request.Websocket;
using osuTK;

namespace osu_request.Drawables
{
    public class SettingsTab : GenericTab
    {
        private const string twitchLoginUrl =
            "https://id.twitch.tv/oauth2/authorize?response_type=code&client_id=w1j4lbtlp30d1z1whtutav6mgshzd3&redirect_uri=http://localhost/redirect&scope=user:read:email+chat:read+chat:edit+moderation:read";

        private SettingContainer ChannelNameContainer;
        private SettingContainer PasscodeContainer;

        [Resolved]
        private WebSocketClient WebSocketClient { get; set; }

        [Resolved]
        private OsuRequestConfig OsuRequestConfig { get; set; }

        [Resolved]
        private NotificationContainer NotificationContainer { get; set; }

        [BackgroundDependencyLoader]
        private void Load(GameHost host)
        {
            OsuRequestButton _saveButton;
            OsuRequestButton _loginWithTwitch;

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
                                Setting = OsuRequestSetting.Username,
                                Prompt = "Username"
                            },
                            PasscodeContainer = new SecretSettingContainer
                            {
                                Setting = OsuRequestSetting.Passcode,
                                Prompt = "Passcode"
                            }
                        }
                    }
                },
                new Container
                {
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    RelativeSizeAxes = Axes.X,
                    Size = new Vector2(1.0f, 250.0f),
                    Padding = new MarginPadding(50),
                    Children = new Drawable[]
                    {
                        _loginWithTwitch = new OsuRequestButton
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            RelativeSizeAxes = Axes.Both,
                            Size = new Vector2(0.2f, 0.4f),
                            Text = "Login with Twitch",
                            FontSize = 25,
                            CornerRadius = 5
                        },
                        _saveButton = new OsuRequestButton
                        {
                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.BottomCentre,
                            RelativeSizeAxes = Axes.Both,
                            Size = new Vector2(0.2f, 0.4f),
                            Text = "Save and Connect",
                            FontSize = 25,
                            CornerRadius = 5
                        }
                    }
                }
            };

            _loginWithTwitch.OnButtonClicked += () => host.OpenUrlExternally(twitchLoginUrl);
            _saveButton.OnButtonClicked += SaveButtonClicked;
        }

        private void SaveButtonClicked()
        {
            OsuRequestConfig.GetBindable<string>(OsuRequestSetting.Username).Value = ChannelNameContainer.TextBox.Text;
            OsuRequestConfig.GetBindable<string>(OsuRequestSetting.Passcode).Value = PasscodeContainer.TextBox.Text;
            OsuRequestConfig.Save();
            WebSocketClient.SendAuth(OsuRequestConfig);
        }
    }
}