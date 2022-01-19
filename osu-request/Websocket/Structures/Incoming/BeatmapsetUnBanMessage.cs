using Newtonsoft.Json;

namespace osu_request.Websocket.Structures;

public class BeatmapsetUnBanMessage
{
    [JsonProperty("d")]
    public BeatmapsetUnBanArgs Data;
}

public class BeatmapsetUnBanArgs
{
    [JsonProperty("beatmapsetId")]
    public string BeatmapsetId;
}