// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics.Effects;
using osuTK;
using osuTK.Graphics;

namespace osu.Request.Game.Graphics;

public static class OsuRequestEdgeEffects
{
    public static readonly EdgeEffectParameters NO_SHADOW = new()
    {
        Colour = OsuRequestColour.Invisible,
        Radius = 0,
        Type = EdgeEffectType.Shadow,
        Offset = Vector2.Zero
    };

    public static readonly EdgeEffectParameters BASIC_SHADOW = new()
    {
        Colour = Color4.Black.Opacity(0.6f),
        Radius = 2.5f,
        Type = EdgeEffectType.Shadow,
        Offset = new Vector2(0.0f, 1.5f)
    };

    public static readonly EdgeEffectParameters DISPERSED_SHADOW = new()
    {
        Colour = Color4.Black.Opacity(0.25f),
        Radius = 20,
        Type = EdgeEffectType.Shadow,
        Offset = new Vector2(0.0f, 5.0f)
    };
}
