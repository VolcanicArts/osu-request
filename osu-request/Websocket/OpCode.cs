namespace osu_request.Websocket;

public enum IncomingOpCode
{
    AUTH_INVALID_USERNAME = 1000,
    AUTH_INVALID_CODE = 1008,
    CONNECTED = 1001,
    LOGGEDIN = 1002,
    REQUEST = 1003,
    BEATMAPSETBAN = 1004,
    USERBAN = 1005,
    BEATMAPSETUNBAN = 1006,
    USERUNBAN = 1007,
    ALLBEATMAPSETBANS = 1009,
    ALLUSERBANS = 1010
}
public enum OutgoingOpCode
{
    AUTH = 0,
    BANBEATMAPSET = 1,
    BANUSER = 2,
    UNBANBEATMAPSET = 3,
    UNBANUSER = 4,
}