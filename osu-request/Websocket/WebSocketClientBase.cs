using System;
using System.Net.Sockets;
using System.Text;
using NetCoreServer;
using Newtonsoft.Json;
using osu.Framework.Logging;
using osu_request.Config;
using osu_request_server;

namespace osu_request.Websocket;

public class WebSocketClientBase : WsClient
{
    private const string log_prefix = "[WebSocket]";
    private readonly OsuRequestConfig OsuRequestConfig;

    protected WebSocketClientBase(OsuRequestConfig osuRequestConfig) : base("127.0.0.1", 8080)
    {
        OsuRequestConfig = osuRequestConfig;
    }

    public override void OnWsConnecting(HttpRequest request)
    {
        request.SetBegin("GET", "/");
        request.SetHeader("Host", Address);
        request.SetHeader("Origin", $"http://{Address}");
        request.SetHeader("Upgrade", "websocket");
        request.SetHeader("Connection", "Upgrade");
        request.SetHeader("Sec-WebSocket-Key", Convert.ToBase64String(WsNonce));
        request.SetHeader("Sec-WebSocket-Protocol", "chat, superchat");
        request.SetHeader("Sec-WebSocket-Version", "13");
        request.SetHeader("Auth-Username", OsuRequestConfig.Get<string>(OsuRequestSetting.Username));
        request.SetHeader("Auth-Code", OsuRequestConfig.Get<string>(OsuRequestSetting.Passcode));
        request.SetBody();
    }

    public override bool ConnectAsync()
    {
        Logger.Log($"{log_prefix} WebSocket server address: {Address}");
        Logger.Log($"{log_prefix} WebSocket server port: {Port}");
        return base.ConnectAsync();
    }

    public override void OnWsConnected(HttpResponse response)
    {
        Logger.Log($"{log_prefix} WebSocketClient connected a new session with Id {Id}");
    }

    public override void OnWsDisconnected()
    {
        Logger.Log($"{log_prefix} WebSocketClient disconnected a session with Id {Id}");
    }

    public override void OnWsReceived(byte[] buffer, long offset, long size)
    {
        var message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
        Logger.Log($"{log_prefix} Incoming: {message}");
        var webSocketMessage = JsonConvert.DeserializeObject<IncomingMessageBase>(message);
        OnMessage(webSocketMessage, message);
    }

    public override void OnWsError(SocketError error)
    {
        Logger.Log($"{log_prefix} WebSocketClient caught an error with code {error}");
    }

    protected virtual void OnMessage(IncomingMessageBase message, string rawMessage) { }
}