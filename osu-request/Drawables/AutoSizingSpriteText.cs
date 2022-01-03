using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using osuTK;
using osuTK.Graphics;

namespace osu_request.Drawables
{
    /// <summary>
    /// Auto-sizes the text set to the parent of this container 
    /// </summary>
    public class AutoSizingSpriteText : Container
    {
        protected internal Anchor SpriteAnchor { get; init; } = Anchor.Centre;
        protected internal Anchor SpriteOrigin { get; init; } = Anchor.Centre;
        protected internal Bindable<LocalisableString> Text { get; init; } = new();
        protected internal FontUsage Font { get; init; }
        protected internal bool Shadow { get; init; }

        private SpriteText _spriteText;

        [BackgroundDependencyLoader]
        private void Load()
        {
            Child = _spriteText = new SpriteText()
            {
                Anchor = SpriteAnchor,
                Origin = SpriteOrigin,
                Text = Text.Value,
                Font = Font,
                Shadow = Shadow,
                ShadowColour = Color4.Black.Opacity(0.75f),
                ShadowOffset = new Vector2(0.05f)
            };
            
            Text.BindValueChanged(e => _spriteText.Text = e.NewValue);
        }

        private float CalculateTextSize()
        {
            return DrawSize.X > DrawSize.Y
                ? DrawSize.Y - (Padding.Top + Padding.Bottom)
                : DrawSize.X - (Padding.Top + Padding.Bottom);
        }

        protected override void UpdateAfterAutoSize()
        {
            base.UpdateAfterAutoSize();
            _spriteText.Font = _spriteText.Font.With(size: CalculateTextSize());
        }
    }
}