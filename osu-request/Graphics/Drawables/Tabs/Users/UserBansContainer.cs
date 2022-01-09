﻿using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu_request.Twitch;
using osuTK;
using osuTK.Graphics;
using TwitchLib.Client.Models;

namespace osu_request.Drawables.Users
{
    public class UserBansContainer : Container
    {
        private FillFlowContainer<UserCard> _fillFlowContainer;

        [BackgroundDependencyLoader]
        private void Load(TwitchClientLocal localTwitchClient)
        {
            localTwitchClient.OnChatMessage += HandleTwitchMessage;

            Masking = true;
            CornerRadius = 10;
            BorderThickness = 2;
            BorderColour = Color4.Black;
            EdgeEffect = OsuRequestEdgeEffects.BasicShadow;
            Children = new Drawable[]
            {
                new BackgroundColour()
                {
                    Colour = OsuRequestColour.GreyLimeDarker
                },
                new BasicScrollContainer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    ClampExtension = 20.0f,
                    ScrollbarVisible = false,
                    Child = _fillFlowContainer = new FillFlowContainer<UserCard>
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                        Direction = FillDirection.Vertical,
                        Spacing = new Vector2(2),
                        Padding = new MarginPadding(10.0f)
                    }
                }
            };
        }

        private void HandleTwitchMessage(ChatMessage message)
        {
            if (message.Message.StartsWith("!rq") && _fillFlowContainer.Children.All(card => card.Username != message.Username))
            {
                Scheduler.Add(() => _fillFlowContainer.Add(new UserCard(message.Username)
                    {
                        Anchor = Anchor.TopLeft,
                        Origin = Anchor.TopLeft,
                        RelativeSizeAxes = Axes.X,
                        Size = new Vector2(1.0f, 30.0f)
                    }
                ));
            }
        }
    }
}