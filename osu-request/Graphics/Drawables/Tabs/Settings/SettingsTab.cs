using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
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
        private Container ErrorContainer;
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
            TextFlowContainer _text;

            Children = new Drawable[]
            {
                new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.8f, 0.8f),
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
                        CornerRadius = 5,
                        BorderThickness = 2
                    }
                },
                ErrorContainer = new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    RelativePositionAxes = Axes.Both,
                    Size = new Vector2(0.3f, 0.1f),
                    Position = new Vector2(0.0f, -1.0f),
                    Masking = true,
                    CornerRadius = 10,
                    Children = new Drawable[]
                    {
                        new BackgroundColour
                        {
                            Colour = OsuRequestColour.Red.Darken(0.8f)
                        },
                        new Container
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            Padding = new MarginPadding(5),
                            Child = new Container
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                RelativeSizeAxes = Axes.Both,
                                Masking = true,
                                CornerRadius = 10,
                                Children = new Drawable[]
                                {
                                    new BackgroundColour
                                    {
                                        Colour = OsuRequestColour.GreyLime
                                    },
                                    new Container
                                    {
                                        Anchor = Anchor.Centre,
                                        Origin = Anchor.Centre,
                                        RelativeSizeAxes = Axes.Both,
                                        Size = new Vector2(0.9f, 1.0f),
                                        Child = _text = new TextFlowContainer
                                        {
                                            Anchor = Anchor.Centre,
                                            Origin = Anchor.Centre,
                                            TextAnchor = Anchor.Centre,
                                            RelativeSizeAxes = Axes.Both
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            _text.AddText("Incorrect Information!", t => t.Font = OsuRequestFonts.Regular.With(size: 20));

            _clientManager.OnFailed += () => Scheduler.AddOnce(AnimateError);
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

        private void AnimateError()
        {
            ErrorContainer.MoveTo(new Vector2(0.0f), 250, Easing.OutCubic)
                .Delay(2000)
                .MoveTo(new Vector2(0.0f, 1.0f), 250, Easing.InCubic)
                .Then()
                .MoveTo(new Vector2(0.0f, -1.0f));
        }
    }
}