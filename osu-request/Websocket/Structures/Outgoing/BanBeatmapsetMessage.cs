using Newtonsoft.Json;

namespace osu_request.Websocket.Structures;

public class BanBeatmapsetMessage
{
    [JsonProperty("d")]
    public BanBeatmapsetData Data;

    [JsonProperty("op")]
    public OutgoingOpCode Op { get; } = OutgoingOpCode.BANBEATMAPSET;
}

public class BanBeatmapsetData
{
    [JsonProperty("beatmapsetId")]
    public string BeatmapsetId;
}