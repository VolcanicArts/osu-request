using Newtonsoft.Json;
using volcanicarts.osu.NET.Structures;

namespace osu_request_server;

public class AuthAllBeatmapsetBansMessage : IncomingMessageBase
{
    [JsonProperty("beatmapsets")]
    public Beatmapset[] Beatmapsets = null!;
}