using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Textures;
using osuTK;
using TwitchLib.Client.Models;

namespace osu_request.Drawables.Users
{
    public class UserCard : Container
    {
        public readonly string Username;
        private readonly string DisplayName;

        public UserCard(ChatMessage message)
        {
            Username = message.Username;
            DisplayName = message.DisplayName;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            this.FadeInFromZero(200, Easing.OutCubic);
        }

        [BackgroundDependencyLoader]
        private void Load(TextureStore textureStore)
        {
            Alpha = 0;
            Masking = true;
            CornerRadius = 5;

            TextFlowContainer _text;
            SpriteButton _ban;

            Children = new Drawable[]
            {
                new Box
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Colour = OsuRequestColour.Gray4
                },
                _text = new TextFlowContainer
                {
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    TextAnchor = Anchor.CentreLeft,
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding
                    {
                        Left = 5
                    }
                },
                new Container
                {
                    Anchor = Anchor.CentreRight,
                    Origin = Anchor.CentreRight,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.1f, 1.0f),
                    Padding = new MarginPadding(5),
                    Child = _ban = new SpriteButton
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Size = new Vector2(2.0f),
                        Scale = new Vector2(0.5f),
                        CornerRadius = 5,
                        BackgroundColour = OsuRequestColour.RedDark,
                        Texture = textureStore.Get("ban")
                    }
                }
            };

            _text.AddText(DisplayName, t => t.Font = OsuRequestFonts.Regular.With(size: 20));
        }
    }
}