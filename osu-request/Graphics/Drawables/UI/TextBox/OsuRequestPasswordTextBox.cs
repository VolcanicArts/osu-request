using osu.Framework.Graphics;

namespace osu_request.Drawables;

public sealed class OsuRequestPasswordTextBox : OsuRequestTextBox
{
    private static char MaskCharacter => '*';

    protected override bool AllowClipboardExport => true;

    protected override bool AllowWordNavigation => false;

    protected override Drawable AddCharacterToFlow(char c)
    {
        return base.AddCharacterToFlow(MaskCharacter);
    }
}