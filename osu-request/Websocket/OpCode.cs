namespace osu_request.Websocket;

public enum IncomingOpCode
{
    REQUEST = 1003,
    BEATMAPSETBAN = 1004,
}
public enum OutgoingOpCode
{
    AUTH = 0,
    BANBEATMAPSET = 1,
}