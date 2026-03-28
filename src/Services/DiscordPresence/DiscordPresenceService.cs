using System;
using DiscordRPC;

namespace Pokebot.Services.DiscordPresence
{
    public class DiscordPresenceService : IDisposable
    {
        private readonly DiscordRpcClient _client;
        private readonly DateTime _startTime;
        private readonly Button[] _buttons;

        private string _clientId;
        private string _githubUrl;
        private string? _romName;
        private string? _botType;
        private int _encountered;
        private bool _botRunning;

        public DiscordPresenceService(string clientId, string githubUrl)
        {
            _clientId = clientId;
            _githubUrl = githubUrl;
            _startTime = DateTime.UtcNow;
            _buttons = new[]
            {
                new Button { Label = "GitHub", Url = githubUrl },
            };

            _client = new DiscordRpcClient(clientId);
            _client.Initialize();

            UpdatePresence();
        }

        public void SetWaiting()
        {
            _romName = null;
            _botType = null;
            _encountered = 0;
            _botRunning = false;
            UpdatePresence();
        }

        public void SetPlaying(string romName)
        {
            _romName = romName;
            _botRunning = false;
            _encountered = 0;
            UpdatePresence();
        }

        public void SetBotType(string botType)
        {
            _botType = botType;
        }

        public void SetBotStarted()
        {
            _botRunning = true;
            _encountered = 0;
            UpdatePresence();
        }

        public void SetBotStopped()
        {
            _botRunning = false;
            UpdatePresence();
        }

        public void IncrementEncountered()
        {
            _encountered++;
            if (_botRunning)
            {
                UpdatePresence();
            }
        }

        private void UpdatePresence()
        {
            string details;
            string? state = null;

            if (_romName == null)
            {
                details = "Waiting for ROM...";
            }
            else
            {
                details = $"Playing {_romName}";
                if (_botRunning && _botType != null)
                {
                    state = $"{_botType} · {_encountered} encountered";
                }
            }

            _client.SetPresence(
                new RichPresence
                {
                    Details = details,
                    State = state,
                    Timestamps = new Timestamps { Start = _startTime },
                    Buttons = _buttons,
                }
            );
        }

        public void Dispose()
        {
            _client.ClearPresence();
            _client.Dispose();
        }
    }
}
