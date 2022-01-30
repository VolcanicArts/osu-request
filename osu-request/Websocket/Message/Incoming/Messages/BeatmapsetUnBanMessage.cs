using Newtonsoft.Json;

namespace osu_request_server;

public class BeatmapsetUnBanMessage : IncomingMessageBase
{
    [JsonProperty("beatmapsetId")]
    public string BeatmapsetId = null!;
}