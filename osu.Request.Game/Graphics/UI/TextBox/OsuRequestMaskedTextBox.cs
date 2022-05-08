// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using osu.Framework.Graphics;

namespace osu.Request.Game.Graphics.UI.TextBox;

public sealed class OsuRequestMaskedTextBox : OsuRequestTextBox
{
    private const char mask_character = '*';

    protected override bool AllowWordNavigation => false;

    protected override Drawable AddCharacterToFlow(char c)
    {
        return base.AddCharacterToFlow(mask_character);
    }
}
