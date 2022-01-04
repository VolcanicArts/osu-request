using osu.Framework.Graphics;

namespace osu_request.Drawables
{
    public sealed class OsuRequestPasswordTextBox : OsuRequestTextBox
    {
        private char MaskCharacter => '*';

        protected override bool AllowClipboardExport => true;

        protected override bool AllowWordNavigation => false;

        protected override Drawable AddCharacterToFlow(char c) => base.AddCharacterToFlow(MaskCharacter);
    }
}