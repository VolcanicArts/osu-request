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
        private readonly TwitchClient _twitchClient = new();
        public Action<ChatMessage> OnChatMessage;

        public bool Init(ConnectionCredentials credentials)
        {
            try
            {
                _twitchClient.Initialize(credentials, credentials.TwitchUsername);
            }
            catch (Exception)
            {
                return false;
            }

            _twitchClient.OnMessageReceived += (_, args) => _messages.Add(args);
            _twitchClient.OnLog += (_, args) => Logger.Log($"[TwitchClient]: {args.Data}");

            _twitchClient.Connect();

            return true;
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