using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Textures;
using osu_request.Clients;
using volcanicarts.osu.NET.Structures;

namespace osu_request.Drawables
{
    public class BeatmapsetBanButtons : Container
    {
        private readonly Beatmapset Beatmapset;

        public BeatmapsetBanButtons(Beatmapset beatmapset)
        {
            Beatmapset = beatmapset;
        }

        [BackgroundDependencyLoader]
        private void Load(TextureStore textureStore, BeatmapsetBanManager banManager)
        {
            Masking = true;
            CornerRadius = 10;

            SpriteButton _unban;

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
                    Padding = new MarginPadding(5),
                    Child = _unban = new SpriteButton
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        BackgroundColour = OsuRequestColour.BlueDark,
                        Texture = textureStore.Get("undo")
                    }
                }
            };

            _unban.OnButtonClicked += () => banManager.UnBan(Beatmapset.Id.ToString());
        }
    }
}