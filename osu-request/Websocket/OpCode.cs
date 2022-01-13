namespace osu_request.Websocket;

public enum OpCode
{
    //Outgoing
    AUTH = 0,
    BANBEATMAPSET = 1,
    
    //Incoming
    REQUEST = 1003,
    BEATMAPSETBAN = 1004,
}