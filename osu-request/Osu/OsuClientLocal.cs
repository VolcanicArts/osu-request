using System;
using System.Threading.Tasks;
using volcanicarts.osu.NET.Client;

namespace osu_request.Osu
{
    public class OsuClientLocal
    {
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
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}