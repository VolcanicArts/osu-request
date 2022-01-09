using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics.Effects;
using osuTK;
using osuTK.Graphics;

namespace osu_request
{
    public static class OsuRequestEdgeEffects
    {
        public static readonly EdgeEffectParameters NoShadow = new()
        {
            Colour = new Color4(0, 0, 0, 0),
            Radius = 0,
            Type = EdgeEffectType.Shadow,
            Offset = new Vector2(0)
        };

        public static readonly EdgeEffectParameters BasicShadow = new()
        {
            Colour = Color4.Black.Opacity(0.6f),
            Radius = 2.5f,
            Type = EdgeEffectType.Shadow,
            Offset = new Vector2(0.0f, 1.5f)
        };
    }
}