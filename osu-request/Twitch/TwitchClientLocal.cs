using System;
using System.Net.Http;
using JetBrains.Annotations;
using osu.Framework.Logging;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Events;
using volcanicarts.osu.NET.Structures;

namespace osu_request.Twitch
{
    public class TwitchClientLocal : TwitchClient
    {
        private const string logPrefix = "[TwitchClientLocal]: ";

        private readonly ConnectionCredentials _connectionCredentials;
        private readonly bool _enableVerboseLogging;

        public Action<Beatmapset> ScheduleBeatmapAddition;

        public TwitchClientLocal(ConnectionCredentials connectionCredentials, bool enableVerboseLogging)
        {
            _connectionCredentials = connectionCredentials;
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
            Initialize(_connectionCredentials, _connectionCredentials.TwitchUsername);
        }

        private void InitEvents()
        {
            if (_enableVerboseLogging) OnLog += OnLogEvent;
            OnConnected += OnConnectedEvent;
            OnDisconnected += OnDisconnectedEvent;
            OnMessageReceived += OnMessageReceivedEvent;
            OnChatCommandReceived += OnChatCommandReceivedEvent;
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

        private void OnChatCommandReceivedEvent([CanBeNull] object sender, OnChatCommandReceivedArgs e)
        {
            var command = e.Command.CommandText.ToLower();
            var argument = e.Command.ArgumentsAsString;

            switch (command)
            {
                case "rq":
                    HandleRequestCommand(argument);
                    break;
            }
        }

        private async void HandleRequestCommand(string beatmapId)
        {
            if (string.IsNullOrEmpty(beatmapId))
            {
                SendMessage(_connectionCredentials.TwitchUsername, "Please enter a beatmap Id");
                return;
            }

            Beatmap beatmap;
            try
            {
                beatmap = await Application.osuClient.GetBeatmapAsync(beatmapId);
            }
            catch (HttpRequestException exception)
            {
                SendMessage(_connectionCredentials.TwitchUsername, "Invalid beatmap Id provided");
                return;
            }

            var beatmapset = await beatmap.GetBeatmapsetAsync();
            SendMessage(_connectionCredentials.TwitchUsername, $"Requested {beatmapset.Title}");
            ScheduleBeatmapAddition.Invoke(beatmapset);
        }
    }
}