using System;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Textures;
using osu.Framework.Platform;
using osu_request.Structures;
using osu_request.Websocket;
using osuTK;
using TwitchLib.Client.Models;

namespace osu_request.Drawables
{
    public class BeatmapsetRequestButtons : Container
    {
        private readonly WorkingBeatmapset WorkingBeatmapset;

        public BeatmapsetRequestButtons(WorkingBeatmapset workingBeatmapset)
        {
            WorkingBeatmapset = workingBeatmapset;
        }

        [Resolved]
        private BindableBool ShouldDispose { get; set; }

        [BackgroundDependencyLoader]
        private void Load(TextureStore textureStore, GameHost host, WebSocketClient webSocketClient)
        {
            Masking = true;
            CornerRadius = 10;

            SpriteButton _openExternally;
            SpriteButton _openDirect;
            SpriteButton _ban;
            SpriteButton _banUser;

            Children = new Drawable[]
            {
                new Box
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Colour = OsuRequestColour.Gray2
                },
                new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding(3),
                    Children = new Drawable[]
                    {
                        new Container
                        {
                            Anchor = Anchor.TopRight,
                            Origin = Anchor.TopRight,
                            RelativeSizeAxes = Axes.Both,
                            Size = new Vector2(0.5f),
                            Padding = new MarginPadding(2),
                            Child = _openExternally = new SpriteButton
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                RelativeSizeAxes = Axes.Both,
                                CornerRadius = 5,
                                BackgroundColour = OsuRequestColour.BlueDark,
                                Texture = textureStore.Get("open-externally")
                            }
                        },
                        new Container
                        {
                            Anchor = Anchor.TopLeft,
                            Origin = Anchor.TopLeft,
                            RelativeSizeAxes = Axes.Both,
                            Size = new Vector2(0.5f),
                            Padding = new MarginPadding(2),
                            Child = _openDirect = new SpriteButton
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                RelativeSizeAxes = Axes.Both,
                                CornerRadius = 5,
                                BackgroundColour = OsuRequestColour.BlueDark,
                                Texture = textureStore.Get("download")
                            }
                        },
                        new Container
                        {
                            Anchor = Anchor.BottomLeft,
                            Origin = Anchor.BottomLeft,
                            RelativeSizeAxes = Axes.Both,
                            Size = new Vector2(0.5f),
                            Padding = new MarginPadding(2),
                            Child = _ban = new SpriteButton
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                RelativeSizeAxes = Axes.Both,
                                CornerRadius = 5,
                                BackgroundColour = OsuRequestColour.RedDark,
                                Texture = textureStore.Get("ban")
                            }
                        },
                        new Container
                        {
                            Anchor = Anchor.BottomRight,
                            Origin = Anchor.BottomRight,
                            RelativeSizeAxes = Axes.Both,
                            Size = new Vector2(0.5f),
                            Padding = new MarginPadding(2),
                            Child = _banUser = new SpriteButton
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                RelativeSizeAxes = Axes.Both,
                                CornerRadius = 5,
                                BackgroundColour = OsuRequestColour.RedDark,
                                Texture = textureStore.Get("ban-user")
                            }
                        }
                    }
                }
            };

            _openExternally.OnButtonClicked += () => host.OpenUrlExternally($"https://osu.ppy.sh/beatmapsets/{WorkingBeatmapset.Beatmapset.Id}");
            _openDirect.OnButtonClicked += () => host.OpenUrlExternally($"osu://b/{WorkingBeatmapset.Beatmapset.Id}");
            _ban.OnButtonClicked += () => webSocketClient.BanBeatmapset(WorkingBeatmapset.Beatmapset.Id.ToString());
            _banUser.OnButtonClicked += () => Console.WriteLine("User ban button clicked");
        }
    }
}