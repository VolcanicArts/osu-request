using Newtonsoft.Json;
using osu_request.Websocket;

namespace osu_request_server;

public class RequestBeatmapsetUnBanMessage : OutgoingMessageBase
{
    [JsonProperty("beatmapsetId")]
    public string BeatmapsetId = null!;

    public RequestBeatmapsetUnBanMessage()
    {
        Op = OutgoingOpCode.REQUEST_BEATMAPSET_UNBAN;
    }
}