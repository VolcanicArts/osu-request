using volcanicarts.osu.NET.Client;

namespace osu_request.Osu
{
    public class OsuClientLocal : OsuClient
    {
        private const string clientId = "";
        private const string clientSecret = "";

        public OsuClientLocal() : base(new OsuClientCredentials(clientId, clientSecret))
        {
        }
    }
}