using osu.Framework.Configuration;
using osu.Framework.Platform;

namespace osu_request.Config
{
    public class OsuRequestConfig : IniConfigManager<OsuRequestSetting>
    {
        public OsuRequestConfig(Storage storage) : base(storage) { }

        protected override string Filename => @"config.ini";

        protected override void InitialiseDefaults()
        {
            SetDefault(OsuRequestSetting.OsuClientId, string.Empty);
            SetDefault(OsuRequestSetting.OsuClientSecret, string.Empty);
            SetDefault(OsuRequestSetting.TwitchChannelName, string.Empty);
            SetDefault(OsuRequestSetting.TwitchOAuthToken, string.Empty);
        }
    }

    public enum OsuRequestSetting
    {
        OsuClientId,
        OsuClientSecret,
        TwitchChannelName,
        TwitchOAuthToken
    }
}