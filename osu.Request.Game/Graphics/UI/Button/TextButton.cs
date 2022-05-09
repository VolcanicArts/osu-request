// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Request.Game.Graphics.UI.Text;

namespace osu.Request.Game.Graphics.UI.Button;

public class TextButton : OsuRequestButton
{
    public string Text { get; init; }

    [BackgroundDependencyLoader]
    private void load()
    {
        Masking = true;
        CornerRadius = 5;

        Add(new Container
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            RelativeSizeAxes = Axes.Both,
            Padding = new MarginPadding(5),
            Child = new OsuRequestTextFlowContainer(t => t.Font = OsuRequestFonts.REGULAR.With(size: 30))
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                TextAnchor = Anchor.Centre,
                Text = Text
            }
        });
    }
}
