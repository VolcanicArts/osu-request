using JetBrains.Annotations;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osu.Framework.Logging;
using osu_request.Config;
using osuTK;
using osuTK.Graphics;

namespace osu_request.Drawables
{
    public class SettingsTab : Container
    {
        private BasicTextBox _osuClientIdTextBox;
        private BasicTextBox _osuClientSecretTextBox;
        private BasicTextBox _twitchChannelNameTextBox;
        private BasicTextBox _twitchOAuthTokenTextBox;

        private BasicCallbackButton _saveButton;

        private OsuRequestConfig _osuRequestConfig;
        private AutoSizingSpriteText _savedText;

        [BackgroundDependencyLoader]
        private void Load(OsuRequestConfig osuRequestConfig)
        {
            _osuRequestConfig = osuRequestConfig;
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
                new Container()
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Y,
                    AutoSizeAxes = Axes.X,
                    Height = 0.1f,
                    RelativeAnchorPosition = new Vector2(0.5f, 0.1f),
                    Child = new AutoSizingSpriteText
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Text = { Value = "Settings" },
                        Font = new FontUsage("Roboto", weight: "Regular"),
                        Shadow = true
                    }
                },
                new FillFlowContainer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Direction = FillDirection.Vertical,
                    Children = new Drawable[]
                    {
                        _osuClientIdTextBox = new SettingsTextBox
                        {
                            PlaceholderText = "Enter osu! client Id",
                            Text = _osuRequestConfig.Get<string>(OsuRequestSetting.OsuClientId)
                        },
                        _osuClientSecretTextBox = new SettingsTextBox
                        {
                            PlaceholderText = "Enter osu! client secret",
                            Text = _osuRequestConfig.Get<string>(OsuRequestSetting.OsuClientSecret)
                        },
                        _twitchChannelNameTextBox = new SettingsTextBox
                        {
                            PlaceholderText = "Enter Twitch channel name",
                            Text = _osuRequestConfig.Get<string>(OsuRequestSetting.TwitchChannelName)
                        },
                        _twitchOAuthTokenTextBox = new SettingsTextBox
                        {
                            PlaceholderText = "Enter Twitch OAuth token",
                            Text = _osuRequestConfig.Get<string>(OsuRequestSetting.TwitchOAuthToken)
                        }
                    }
                },
                _saveButton = new BasicCallbackButton
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.2f, 0.1f),
                    RelativeAnchorPosition = new Vector2(0.8f, 0.9f),
                    Text = "Save Settings",
                    Masking = true,
                    CornerRadius = 10,
                    BorderColour = Color4.Black,
                    BorderThickness = 6,
                    Enabled = { Value = true }
                },
                new Container()
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Y,
                    AutoSizeAxes = Axes.X,
                    Height = 0.1f,
                    RelativeAnchorPosition = new Vector2(0.5f, 0.8f),
                    Child = _savedText = new AutoSizingSpriteText
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Font = new FontUsage("Roboto", weight: "Regular"),
                        Shadow = true
                    }
                },
            };

            _saveButton.OnButtonClick += SaveButtonClicked;
            _savedText.Text.BindValueChanged(_ => _savedText.FadeInFromZero(1000).Delay(4000).FadeOutFromOne(1000));
        }

        private void SaveButtonClicked(ClickEvent e)
        {
            _osuRequestConfig.GetBindable<string>(OsuRequestSetting.OsuClientId).Value = _osuClientIdTextBox.Text;
            _osuRequestConfig.GetBindable<string>(OsuRequestSetting.OsuClientSecret).Value = _osuClientSecretTextBox.Text;
            _osuRequestConfig.GetBindable<string>(OsuRequestSetting.TwitchChannelName).Value = _twitchChannelNameTextBox.Text;
            _osuRequestConfig.GetBindable<string>(OsuRequestSetting.TwitchOAuthToken).Value = _twitchOAuthTokenTextBox.Text;
            var saveComplete = _osuRequestConfig.Save();
            _savedText.Text.Value = saveComplete ? "Save Complete!" : "Save Failed";
        }
    }
}