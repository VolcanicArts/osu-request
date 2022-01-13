using Newtonsoft.Json;
using volcanicarts.osu.NET.Structures;

namespace osu_request.Websocket.Structures;

public class BeatmapsetBanMessage
{
    [JsonProperty("d")]
    public BeatmapsetBanArgs Data;
}
public class BeatmapsetBanArgs
{
    [JsonProperty("beatmapset")]
    public Beatmapset Beatmapset;
}