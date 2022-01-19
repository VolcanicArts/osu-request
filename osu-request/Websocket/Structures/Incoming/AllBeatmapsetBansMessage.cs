using Newtonsoft.Json;
using volcanicarts.osu.NET.Structures;

namespace osu_request.Websocket.Structures;

public class AllBeatmapsetBansMessage
{
    [JsonProperty("d")]
    public AllBeatmapsetBansArgs Data;
}

public class AllBeatmapsetBansArgs
{
    [JsonProperty("beatmapsets")]
    public Beatmapset[] Beatmapsets;
}