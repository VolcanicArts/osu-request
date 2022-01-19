using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Textures;
using osu_request.Websocket;
using osuTK;
using TwitchLib.Api.Helix.Models.Users.GetUsers;

namespace osu_request.Drawables.Users
{
    public class UserBanEntry : Container
    {
        public readonly User User;

        public UserBanEntry(User user)
        {
            User = user;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            this.FadeInFromZero(200, Easing.OutCubic);
        }

        [BackgroundDependencyLoader]
        private void Load(TextureStore textureStore, WebSocketClient webSocketClient)
        {
            Alpha = 0;
            Masking = true;
            CornerRadius = 5;

            TextFlowContainer _text;
            SpriteButton _unban;

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
                    Child = _unban = new SpriteButton
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Size = new Vector2(2.0f),
                        Scale = new Vector2(0.5f),
                        CornerRadius = 5,
                        BackgroundColour = OsuRequestColour.BlueDark,
                        Texture = textureStore.Get("undo")
                    }
                }
            };

            _unban.OnButtonClicked += () => webSocketClient.UnBanUser(User.Id);
            _text.AddText(User.Login, t => t.Font = OsuRequestFonts.Regular.With(size: 20));
        }

        protected internal void DisposeGracefully()
        {
            this.FadeOutFromOne(500, Easing.OutQuad).Finally(t => t.RemoveAndDisposeImmediately());
        }
    }
}