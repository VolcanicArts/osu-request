using Newtonsoft.Json;

namespace osu_request.Websocket.Structures;

public class UnBanBeatmapsetMessage
{
    [JsonProperty("d")]
    public UnBanBeatmapsetData Data;

    [JsonProperty("op")]
    public OutgoingOpCode Op { get; } = OutgoingOpCode.REQUEST_BEATMAPSET_UNBAN;
}

public class UnBanBeatmapsetData
{
    [JsonProperty("beatmapsetId")]
    public string BeatmapsetId;
}