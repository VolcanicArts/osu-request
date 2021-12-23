using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;
using osuTK.Graphics;
using volcanicarts.osu.NET.Structures;

namespace osu_request.Drawables
{
    public class BeatmapsetListContainer : Container
    {
        private readonly List<BeatmapsetContainer> _containers = new();
        private FillFlowContainer _fillFlowContainer;

        public void AddBeatmapset(Beatmapset beatmapset)
        {
            var beatmapsetContainer = new BeatmapsetContainer(beatmapset);
            beatmapsetContainer.ContainerClicked += RemoveBeatmap;
            _containers.Add(beatmapsetContainer);
            _fillFlowContainer.Add(beatmapsetContainer);
        }

        private void RemoveBeatmap(BeatmapsetContainer beatmapsetContainer)
        {
            _containers.Remove(beatmapsetContainer);
            _fillFlowContainer.Remove(beatmapsetContainer);
        }

        [BackgroundDependencyLoader]
        private void Load()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;
            Size = new Vector2(0.5f, 1.0f);
            Children = new Drawable[]
            {
                new Box
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(1.0f),
                    Colour = Color4.Aqua
                },
                _fillFlowContainer = new FillFlowContainer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(1.0f),
                    Direction = FillDirection.Vertical,
                    Padding = new MarginPadding(20.0f)
                }
            };
        }
    }
}