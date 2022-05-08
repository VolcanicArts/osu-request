// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using System.Threading.Tasks;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Textures;
using osu.Request.Game.Beatmaps;
using osuTK;

namespace osu.Request.Game.Graphics.Tabs.Requests;

public class RequestsTab : BaseTab
{
    [Resolved]
    private TextureStore textureStore { get; set; }

    [Resolved]
    private AudioManager audioManager { get; set; }

    private FillFlowContainer<RequestEntry> entryFlow;

    [BackgroundDependencyLoader]
    private void load()
    {
        Add(new Container
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            RelativeSizeAxes = Axes.Both,
            Padding = new MarginPadding(10),
            Child = new BasicScrollContainer
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                ClampExtension = 20,
                ScrollbarVisible = false,
                Child = entryFlow = new FillFlowContainer<RequestEntry>
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Direction = FillDirection.Vertical,
                    Spacing = new Vector2(10),
                    Padding = new MarginPadding(10)
                }
            }
        });
    }

    public void AddRequest(WorkingBeatmapset beatmapset)
    {
        Task.Run(() =>
        {
            // TODO Fix osu.NET side to allow for manual setting of covers for testing
            var texture = textureStore.Get("https://assets.ppy.sh/beatmaps/236292/covers/cover.jpg?1631509201");
            var preview = audioManager.GetTrackStore().Get($"https:{beatmapset.Beatmapset.PreviewUrl}");
            beatmapset.CoverTexture = texture;
            beatmapset.PreviewAudio = preview;

            Scheduler.Add(() =>
            {
                entryFlow.Add(new RequestEntry
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    SourceBeatmapset = beatmapset
                });
            });
        }).ConfigureAwait(false);
    }
}
