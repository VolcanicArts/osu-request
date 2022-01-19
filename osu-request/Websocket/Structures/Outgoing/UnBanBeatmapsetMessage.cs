using Newtonsoft.Json;

namespace osu_request.Websocket.Structures;

public class UnBanBeatmapsetMessage
{
    [JsonProperty("op")]
    public OutgoingOpCode Op { get; } = OutgoingOpCode.UNBANBEATMAPSET;

    [JsonProperty("d")]
    public UnBanBeatmapsetData Data;
}

public class UnBanBeatmapsetData
{
    [JsonProperty("beatmapsetId")]
    public string BeatmapsetId;
}
