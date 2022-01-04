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
            if (_twitchClient != null)
            {
                _twitchClient.OnMessageReceived -= MessageReceived;
                _twitchClient.OnLog -= OnLog;
                _twitchClient.OnIncorrectLogin -= IncorrectLogin;
                _twitchClient.OnJoinedChannel -= JoinedChannel;
            }

            _twitchClient = new TwitchClient();
            _twitchClient.Initialize(credentials, credentials.TwitchUsername);
            _twitchClient.OnMessageReceived += MessageReceived;
            _twitchClient.OnLog += OnLog;
            _twitchClient.OnIncorrectLogin += IncorrectLogin;
            _twitchClient.OnJoinedChannel += JoinedChannel;
            _twitchClient.Connect();
        }

        private void JoinedChannel(object o, OnJoinedChannelArgs onJoinedChannelArgs)
        {
            OnSuccess?.Invoke();
        }

        private void IncorrectLogin(object o, OnIncorrectLoginArgs onIncorrectLoginArgs)
        {
            OnFailed?.Invoke();
        }

        private void OnLog(object o, OnLogArgs args)
        {
            Logger.Log($"[TwitchClient]: {args.Data}");
        }

        private void MessageReceived(object o, OnMessageReceivedArgs args)
        {
            _messages.Add(args);
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