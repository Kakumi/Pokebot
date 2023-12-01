using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Pokebot.Services.DiscordWebhook.Models
{
    public class DiscordWebhookEmbed
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("color")]
        public int Color { get; set; }
        [JsonProperty("fields")]
        public List<DiscordWebhookField> Fields { get; set; }
        [JsonProperty("image")]
        public DiscordWebhookImage? Image { get; set; }
        [JsonProperty("thumbnail")]
        public DiscordWebhookImage? Thumbnail { get; set; }
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }


        public DiscordWebhookEmbed(string title)
        {
            Title = title;
            Color = 2326507;
            Fields = new List<DiscordWebhookField>();
            Timestamp = DateTime.Now;
        }
    }
}
