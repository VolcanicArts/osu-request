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
            SetDefault(OsuRequestSetting.Username, string.Empty);
            SetDefault(OsuRequestSetting.Passcode, string.Empty);
        }
    }

    public enum OsuRequestSetting
    {
        Username,
        Passcode
    }
}