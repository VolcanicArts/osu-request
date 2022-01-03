using System.Collections.Generic;
using osu.Framework.Configuration;
using osu.Framework.Platform;

namespace osu_request.Config
{
    public class OsuRequestConfig : IniConfigManager<OsuRequestSetting>
    {
        protected override string Filename => @"config.ini";

        public OsuRequestConfig(Storage storage) : base(storage)
        {
        }

        protected override void InitialiseDefaults()
        {
            SetDefault(OsuRequestSetting.OsuClientId, string.Empty);
            SetDefault(OsuRequestSetting.OsuClientSecret, string.Empty);
            SetDefault(OsuRequestSetting.TwitchClientId, string.Empty);
            SetDefault(OsuRequestSetting.TwitchClientOAuth, string.Empty);
        }
    }

    public enum OsuRequestSetting
    {
        OsuClientId,
        OsuClientSecret,
        TwitchClientId,
        TwitchClientOAuth,
    }
}