using System;
using System.Threading.Tasks;
using volcanicarts.osu.NET.Client;

namespace osu_request.Osu
{
    public class OsuClientLocal
    {
        protected internal bool IsReady { get; private set; }
        protected internal OsuClient OsuClient { get; private set; }

        public void SetClientCredentials(OsuClientCredentials clientCredentials)
        {
            OsuClient = new OsuClient(clientCredentials);
        }

        public async Task<bool> LoginAsync()
        {
            try
            {
                await OsuClient.LoginAsync();
                IsReady = true;
                return true;
            }
            catch (Exception)
            {
                IsReady = false;
                return false;
            }
        }
    }
}