using System;
using System.IO;
using System.Net.Sockets;
using JetBrains.Annotations;
using osu.Framework.Logging;

namespace osu_request.Twitch
{
    public class TwitchClient
    {
        private readonly string _channel;
        private readonly StreamReader _inputStream;
        private readonly StreamWriter _outputStream;
        private readonly string _password;

        private readonly TcpClient _tcpClient;
        private readonly string _username;

        [CanBeNull]
        public Action<string> OnMessage;

        public TwitchClient(string username, string password, string channel, string ip = "irc.chat.twitch.tv",
            int port = 6667)
        {
            _username = username.ToLower();
            _password = password;
            _channel = channel.ToLower();

            _tcpClient = new TcpClient(ip, port);
            _inputStream = new StreamReader(_tcpClient.GetStream());
            _outputStream = new StreamWriter(_tcpClient.GetStream());
        }

        public void JoinChannel()
        {
            _outputStream.WriteLine($"PASS {_password}");
            _outputStream.WriteLine($"NICK {_username}");
            _outputStream.WriteLine($"JOIN #{_channel}");
            _outputStream.Flush();
        }

        private void SendIrcMessage(string message)
        {
            _outputStream.WriteLine(message);
            _outputStream.Flush();
        }

        public void ReadMessage()
        {
            if (!_tcpClient.GetStream().DataAvailable) return;
            var rawIrcMessage = _inputStream.ReadLine();
            if (rawIrcMessage == null) return;
            Logger.Log(rawIrcMessage, LoggingTarget.Network);
            ParseRawIrcMessage(rawIrcMessage);
        }

        private void ParseRawIrcMessage(string rawIrcMessage)
        {
            if (rawIrcMessage.StartsWith("PING")) SendIrcMessage("PONG :tmi.twitch.tv");
            if (rawIrcMessage.Contains("PRIVMSG")) ParseMessage(rawIrcMessage);
        }

        private void ParseMessage(string rawIrcMessage)
        {
            var message = rawIrcMessage.Split(" :")[1];
            OnMessage?.Invoke(message);
        }

        public void SendChatMessage(string message)
        {
            SendIrcMessage($"PRIVMSG #{_channel} :{message}");
        }
    }
}