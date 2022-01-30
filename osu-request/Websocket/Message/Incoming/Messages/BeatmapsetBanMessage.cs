using Newtonsoft.Json;
using volcanicarts.osu.NET.Structures;

namespace osu_request_server;

public class BeatmapsetBanMessage : IncomingMessageBase
{
    [JsonProperty("beatmapset")]
    public Beatmapset Beatmapset = null!;
}