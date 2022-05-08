// Copyright (c) VolcanicArts. Licensed under the GPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using osu.Framework.Configuration;
using osu.Framework.Platform;

namespace osu.Request.Game.Configuration;

public class OsuRequestConfig : IniConfigManager<OsuRequestSetting>
{
    public OsuRequestConfig(Storage storage)
        : base(storage)
    {
    }

    protected override string Filename => @"config.ini";

    protected override void InitialiseDefaults()
    {
        SetDefault(OsuRequestSetting.Username, string.Empty);
        SetDefault(OsuRequestSetting.Passcode, string.Empty);
    }
}

public enum OsuRequestSetting
{
    Username,
    Passcode
}
