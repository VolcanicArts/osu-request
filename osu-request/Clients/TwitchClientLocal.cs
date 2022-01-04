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
        private TwitchClient _twitchClient;
        public Action<ChatMessage> OnChatMessage;
        public Action OnFailed;
        public Action OnSuccess;

        public void Init(ConnectionCredentials credentials)
        {
            _twitchClient = new TwitchClient();
            _twitchClient.Initialize(credentials, credentials.TwitchUsername);
            _twitchClient.OnMessageReceived += (_, args) => _messages.Add(args);
            _twitchClient.OnLog += (_, args) => Logger.Log($"[TwitchClient]: {args.Data}");
            _twitchClient.OnIncorrectLogin += (_, _) => OnFailed?.Invoke();
            _twitchClient.OnJoinedChannel += (_, _) => OnSuccess?.Invoke();
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