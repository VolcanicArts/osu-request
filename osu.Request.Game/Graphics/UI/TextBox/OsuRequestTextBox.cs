// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Platform;
using osuTK.Graphics;

namespace osu.Request.Game.Graphics.UI.TextBox;

public class OsuRequestTextBox : BasicTextBox
{
    protected override bool AllowClipboardExport => true;

    public OsuRequestTextBox()
    {
        BackgroundFocused = OsuRequestColour.Gray6;
        BackgroundUnfocused = OsuRequestColour.Gray4;
    }

    [BackgroundDependencyLoader]
    private void load(GameHost host)
    {
        host.Window.Resized += () => Scheduler.AddOnce(KillFocus);
    }

    protected override SpriteText CreatePlaceholder()
    {
        return base.CreatePlaceholder().With(d => d.Colour = Color4.White.Opacity(0.5f));
    }
}
