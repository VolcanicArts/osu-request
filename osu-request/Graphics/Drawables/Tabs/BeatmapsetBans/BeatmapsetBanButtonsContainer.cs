using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Textures;
using osu.Framework.Platform;
using osu_request.Clients;
using volcanicarts.osu.NET.Structures;

namespace osu_request.Drawables
{
    public class BeatmapsetBanButtonsContainer : Container
    {
        private readonly Beatmapset Beatmapset;

        public BeatmapsetBanButtonsContainer(Beatmapset beatmapset)
        {
            Beatmapset = beatmapset;
        }

        [BackgroundDependencyLoader]
        private void Load(TextureStore textureStore, GameHost host, BeatmapsetBanManager banManager)
        {
            Masking = true;

            SpriteButton _unban;

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
                        Colour = OsuRequestColour.Gray2
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
                                Anchor = Anchor.TopRight,
                                Origin = Anchor.TopRight,
                                RelativeSizeAxes = Axes.Both,
                                Padding = new MarginPadding(2),
                                Child = _unban = new SpriteButton
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    RelativeSizeAxes = Axes.Both,
                                    BackgroundColour = OsuRequestColour.BlueDark,
                                    Texture = textureStore.Get("undo")
                                }
                            }
                        }
                    }
                }
            };

            _unban.OnButtonClicked += () => banManager.UnBan(Beatmapset.Id.ToString());
        }
    }
}