using osu.Framework.Allocation;
using osu.Framework.Audio.Track;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Textures;
using osu.Framework.Platform;
using osuTK;
using TwitchLib.Client.Models;
using volcanicarts.osu.NET.Structures;

namespace osu_request.Drawables
{
    public class BeatmapsetRequestContainer : Container
    {
        private readonly Beatmapset Beatmapset;
        public readonly string BeatmapsetId;
        private readonly Texture CoverTexture;
        private readonly ChatMessage Message;
        private readonly Track PreviewMp3;

        public BeatmapsetRequestContainer(Beatmapset beatmapset, Track previewMp3, Texture coverTexture, ChatMessage message)
        {
            BeatmapsetId = beatmapset.Id.ToString();
            Beatmapset = beatmapset;
            PreviewMp3 = previewMp3;
            CoverTexture = coverTexture;
            Message = message;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            this.FadeInFromZero(1000, Easing.InQuad);
        }

        [BackgroundDependencyLoader]
        private void Load(TextureStore textureStore, GameHost host)
        {
            InitSelf();
            InitChildren();
        }

        private void InitSelf()
        {
            Alpha = 0;
            Masking = true;
            CornerRadius = 10;
        }

        private void InitChildren()
        {
            Children = new Drawable[]
            {
                new BackgroundColour
                {
                    Colour = OsuRequestColour.Gray4
                },
                new Container
                {
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.85f, 1.0f),
                    Padding = new MarginPadding
                    {
                        Left = 5,
                        Right = 2.5f,
                        Top = 5,
                        Bottom = 5
                    },
                    Child = new BeatmapsetRequestCard(Beatmapset, CoverTexture, PreviewMp3, Message)
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both
                    }
                },
                new BeatmapsetButtonsContainer(Beatmapset)
                {
                    Anchor = Anchor.CentreRight,
                    Origin = Anchor.CentreRight,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.15f, 1.0f),
                    Padding = new MarginPadding
                    {
                        Left = 2.5f,
                        Right = 5,
                        Top = 5,
                        Bottom = 5
                    }
                }
            };
        }

        protected internal void DisposeGracefully()
        {
            this.FadeOutFromOne(500, Easing.OutQuad).Finally(t => t.RemoveAndDisposeImmediately());
        }
    }
}