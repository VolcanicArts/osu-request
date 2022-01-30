using Newtonsoft.Json;

namespace osu_request.Websocket.Structures;

public class BanBeatmapsetMessage
{
    [JsonProperty("d")]
    public BanBeatmapsetData Data;

    [JsonProperty("op")]
    public OutgoingOpCode Op { get; } = OutgoingOpCode.REQUEST_BEATMAPSET_BAN;
}

public class BanBeatmapsetData
{
    [JsonProperty("beatmapsetId")]
    public string BeatmapsetId;
}