using JetBrains.Annotations;
using osu.Framework.Logging;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Events;

namespace osu_request.Twitch
{
    public class TwitchClientLocal : TwitchClient
    {
        private const string channelName = "";
        private const string oAuthToken = "";
        private const string logPrefix = "[TwitchClientLocal]: ";

        private readonly bool _enableVerboseLogging;

        public TwitchClientLocal(bool enableVerboseLogging)
        {
            _enableVerboseLogging = enableVerboseLogging;
        }

        public new void Connect()
        {
            InitClient();
            InitEvents();
            base.Connect();
        }

        private void InitClient()
        {
            ConnectionCredentials credentials = new(channelName, oAuthToken);
            Initialize(credentials, channelName);
        }

        private void InitEvents()
        {
            if (_enableVerboseLogging) OnLog += OnLogEvent;
            OnConnected += OnConnectedEvent;
            OnDisconnected += OnDisconnectedEvent;
            OnMessageReceived += OnMessageReceivedEvent;
        }

        private void OnLogEvent([CanBeNull] object sender, OnLogArgs e)
        {
            Logger.Log($"{logPrefix}{e.Data}");
        }

        private void OnConnectedEvent([CanBeNull] object sender, OnConnectedArgs e)
        {
            Logger.Log($"{logPrefix}Twitch client connected successfully");
        }

        private void OnDisconnectedEvent([CanBeNull] object sender, OnDisconnectedEventArgs e)
        {
            Logger.Log($"{logPrefix}Twitch client disconnected successfully");
        }

        private void OnMessageReceivedEvent([CanBeNull] object sender, OnMessageReceivedArgs e)
        {
            Logger.Log($"{logPrefix}{e.ChatMessage.Username}: {e.ChatMessage.Message}");
        }
    }
}