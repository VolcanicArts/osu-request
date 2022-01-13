using Newtonsoft.Json;
using volcanicarts.osu.NET.Structures;

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