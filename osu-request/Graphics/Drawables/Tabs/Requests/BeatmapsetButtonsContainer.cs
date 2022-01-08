using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Textures;
using osu.Framework.Platform;
using osu_request.Clients;
using osuTK;
using volcanicarts.osu.NET.Structures;

namespace osu_request.Drawables
{
    public class BeatmapsetButtonsContainer : Container
    {
        private readonly Beatmapset Beatmapset;

        public BeatmapsetButtonsContainer(Beatmapset beatmapset)
        {
            Beatmapset = beatmapset;
        }

        [BackgroundDependencyLoader]
        private void Load(TextureStore textureStore, GameHost host, BeatmapsetBanManager banManager)
        {
            Masking = true;

            SpriteButton _openExternally;
            SpriteButton _openDirect;
            SpriteButton _ban;
            SpriteButton _check;

            Child = new Container
            {
                Anchor = Anchor.CentreRight,
                Origin = Anchor.CentreRight,
                RelativeSizeAxes = Axes.Both,
                Masking = true,
                CornerRadius = 10,
                Children = new Drawable[]
                {
                    new BackgroundColour
                    {
                        Colour = OsuRequestColour.GreyLimeDarker
                    },
                    new Container
                    {
                        Anchor = Anchor.TopLeft,
                        Origin = Anchor.TopLeft,
                        RelativeSizeAxes = Axes.Both,
                        Padding = new MarginPadding(3),
                        Children = new Drawable[]
                        {
                            new Container
                            {
                                Anchor = Anchor.TopLeft,
                                Origin = Anchor.TopLeft,
                                RelativeSizeAxes = Axes.Both,
                                Size = new Vector2(0.5f),
                                Padding = new MarginPadding(2),
                                Child = _openExternally = new SpriteButton
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    RelativeSizeAxes = Axes.Both,
                                    BackgroundColour = OsuRequestColour.BlueDark,
                                    Texture = textureStore.Get("open-externally")
                                }
                            },
                            new Container
                            {
                                Anchor = Anchor.TopRight,
                                Origin = Anchor.TopRight,
                                RelativeSizeAxes = Axes.Both,
                                Size = new Vector2(0.5f),
                                Padding = new MarginPadding(2),
                                Child = _openDirect = new SpriteButton
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    RelativeSizeAxes = Axes.Both,
                                    BackgroundColour = OsuRequestColour.BlueDark,
                                    Texture = textureStore.Get("download")
                                },
                            },
                            new Container
                            {
                                Anchor = Anchor.BottomRight,
                                Origin = Anchor.BottomRight,
                                RelativeSizeAxes = Axes.Both,
                                Size = new Vector2(0.5f),
                                Padding = new MarginPadding(2),
                                Child = _ban = new SpriteButton
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    RelativeSizeAxes = Axes.Both,
                                    BackgroundColour = OsuRequestColour.RedDark,
                                    Texture = textureStore.Get("ban")
                                },
                            },
                            new Container
                            {
                                Anchor = Anchor.BottomLeft,
                                Origin = Anchor.BottomLeft,
                                RelativeSizeAxes = Axes.Both,
                                Size = new Vector2(0.5f),
                                Padding = new MarginPadding(2),
                                Child = _check = new SpriteButton
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    RelativeSizeAxes = Axes.Both,
                                    BackgroundColour = OsuRequestColour.GreenDark,
                                    Texture = textureStore.Get("check")
                                }
                            }
                        }
                    }
                }
            };

            _openExternally.OnButtonClicked += () => host.OpenUrlExternally($"https://osu.ppy.sh/beatmapsets/{Beatmapset.Id}");
            _openDirect.OnButtonClicked += () => host.OpenUrlExternally($"osu://b/{Beatmapset.Id}");
            _check.OnButtonClicked += DisposeGracefully;
            _ban.OnButtonClicked += () =>
            {
                banManager.Ban(Beatmapset.Id.ToString());
                DisposeGracefully();
            };
        }

        private void DisposeGracefully()
        {
            ((BeatmapsetRequestContainer)Parent).DisposeGracefully();
        }
    }
}