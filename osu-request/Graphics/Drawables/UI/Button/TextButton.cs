using System;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Graphics;

namespace osu_request.Drawables
{
    public class TextButton : OsuRequestButton
    {
        protected internal string Text { get; init; } = string.Empty;
        protected internal float FontSize { get; init; } = 30;

        [BackgroundDependencyLoader]
        private void Load()
        {
            TextFlowContainer _text;

            AddInternal(_text = new TextFlowContainer
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                TextAnchor = Anchor.Centre,
                RelativeSizeAxes = Axes.Both
            });

            _text.AddText(Text, t =>
            {
                t.Font = OsuRequestFonts.Regular.With(size: FontSize);
                t.Shadow = true;
                t.ShadowColour = Color4.Black.Opacity(0.5f);
                t.ShadowOffset = new Vector2(0.0f, 0.025f);
            });
        }
    }
}