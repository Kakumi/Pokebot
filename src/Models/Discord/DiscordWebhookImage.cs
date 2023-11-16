﻿using Newtonsoft.Json;

namespace Pokebot.Models.Discord
{
    public class DiscordWebhookImage
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        public DiscordWebhookImage(string url)
        {
            Url = url;
        }
    }
}
