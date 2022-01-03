using JetBrains.Annotations;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osu.Framework.Logging;
using osuTK;
using osuTK.Graphics;

namespace osu_request.Drawables
{
    public class SettingsTab : Container
    {
        private BasicTextBox _osuClientIdTextBox;
        private BasicTextBox _osuClientSecretTextBox;
        private BasicTextBox _twitchClientIdTextBox;
        private BasicTextBox _twitchClientOAuthTextBox;

        private Container TitleContainer;
        private AutoSizingSpriteText Title;
        private BasicCallbackButton _saveButton;

        [BackgroundDependencyLoader]
        private void Load()
        {
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
                TitleContainer = new Container()
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.2f, 0.1f),
                    RelativeAnchorPosition = new Vector2(0.5f, 0.1f),
                    Child = Title = new AutoSizingSpriteText
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Text = "Settings",
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
                        _osuClientIdTextBox = new SettingsTextBox("Enter osu! client Id"),
                        _osuClientSecretTextBox = new SettingsTextBox("Enter osu! client secret"),
                        _twitchClientIdTextBox = new SettingsTextBox("Enter Twitch client Id"),
                        _twitchClientOAuthTextBox = new SettingsTextBox("Enter Twitch client OAuth")
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
                }
            };

            _saveButton.OnButtonClick += SaveButtonClicked;
        }

        private void SaveButtonClicked(ClickEvent e)
        {
            
        }
    }
}