using Newtonsoft.Json;
using osu_request.Websocket;

namespace osu_request_server;

public class RequestBeatmapsetBanMessage : OutgoingMessageBase
{
    [JsonProperty("beatmapsetId")]
    public string BeatmapsetId = null!;

    public RequestBeatmapsetBanMessage()
    {
        Op = OutgoingOpCode.REQUEST_BEATMAPSET_BAN;
    }
}