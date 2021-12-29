using System;
using System.Collections.Generic;
using osu.Framework.Logging;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace osu_request.Twitch
{
    public class TwitchClientLocal
    {
        private readonly List<OnMessageReceivedArgs> _messages = new();
        private readonly TwitchClient _twitchClient;
        public Action<ChatMessage> OnChatMessage;

        public TwitchClientLocal(ConnectionCredentials credentials)
        {
            _twitchClient = new TwitchClient();
            _twitchClient.Initialize(credentials, credentials.TwitchUsername);
            Init();
        }

        private void Init()
        {
            _twitchClient.OnMessageReceived += (o, args) => _messages.Add(args);
            _twitchClient.OnLog += (o, args) => Logger.Log($"[TwitchClient]: {args.Data}");
        }

        public void Connect()
        {
            _twitchClient.Connect();
        }

        public void Update()
        {
            lock (_messages)
            {
                foreach (var message in _messages) OnChatMessage?.Invoke(message.ChatMessage);
                _messages.Clear();
            }
        }
    }
}