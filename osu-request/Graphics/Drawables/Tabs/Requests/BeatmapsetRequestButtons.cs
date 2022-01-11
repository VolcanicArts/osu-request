using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Textures;
using osu.Framework.Platform;
using osu_request.Clients;
using osu_request.Structures;
using osuTK;

namespace osu_request.Drawables
{
    public class BeatmapsetRequestButtons : Container
    {
        private readonly WorkingBeatmapset WorkingBeatmapset;

        [Resolved]
        private BindableBool ShouldDispose { get; set; }

        public BeatmapsetRequestButtons(WorkingBeatmapset workingBeatmapset)
        {
            WorkingBeatmapset = workingBeatmapset;
        }

        [BackgroundDependencyLoader]
        private void Load(TextureStore textureStore, GameHost host, BeatmapsetBanManager banManager)
        {
            Masking = true;
            CornerRadius = 10;

            SpriteButton _openExternally;
            SpriteButton _openDirect;
            SpriteButton _ban;
            SpriteButton _check;

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
                            }
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
                            }
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
            };

            _openExternally.OnButtonClicked += () => host.OpenUrlExternally($"https://osu.ppy.sh/beatmapsets/{WorkingBeatmapset.Beatmapset.Id}");
            _openDirect.OnButtonClicked += () => host.OpenUrlExternally($"osu://b/{WorkingBeatmapset.Beatmapset.Id}");
            _check.OnButtonClicked += () => ShouldDispose.Value = true;
            _ban.OnButtonClicked += () => banManager.Ban(WorkingBeatmapset.Beatmapset.Id.ToString());
        }
    }
}