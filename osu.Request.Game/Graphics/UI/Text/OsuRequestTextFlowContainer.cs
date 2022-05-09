// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;

namespace osu.Request.Game.Graphics.UI.Text;

public class OsuRequestTextFlowContainer : TextFlowContainer
{
    public OsuRequestTextFlowContainer(Action<SpriteText> defaultCreationParameters = null)
        : base(defaultCreationParameters)
    {
    }

    protected override SpriteText CreateSpriteText()
    {
        return base.CreateSpriteText().With(d => d.Shadow = true);
    }
}
