namespace osu_request.Websocket;

public enum IncomingOpCode
{
    REQUEST = 1003,
    BEATMAPSETBAN = 1004,
    USERBAN = 1005,
    BEATMAPSETUNBAN = 1006,
}
public enum OutgoingOpCode
{
    AUTH = 0,
    BANBEATMAPSET = 1,
    BANUSER = 2,
    UNBANBEATMAPSET = 3,
}