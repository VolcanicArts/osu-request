using Newtonsoft.Json;

namespace osu_request.Websocket.Structures;

public class BanBeatmapsetMessage
{
    [JsonProperty("op")]
    public OutgoingOpCode Op { get; } = OutgoingOpCode.BANBEATMAPSET;

    [JsonProperty("d")]
    public BanBeatmapsetData Data;
}

public class BanBeatmapsetData
{
    [JsonProperty("beatmapsetId")]
    public string BeatmapsetId;
}
