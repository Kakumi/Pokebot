using System.Collections.Generic;
using Newtonsoft.Json;

namespace Pokebot.Services.DiscordWebhook.Models
{
    public class DiscordWebhookEmbed
    {
        [JsonProperty("title")]
        public string Title { get; set; } = string.Empty;

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; } = string.Empty;

        [JsonProperty("color")]
        public int Color { get; set; }

        [JsonProperty("fields")]
        public List<DiscordWebhookField> Fields { get; set; } = new();

        [JsonProperty("thumbnail", NullValueHandling = NullValueHandling.Ignore)]
        public DiscordWebhookImage? Thumbnail { get; set; }

        [JsonProperty("image", NullValueHandling = NullValueHandling.Ignore)]
        public DiscordWebhookImage? Image { get; set; }

        [JsonProperty("footer", NullValueHandling = NullValueHandling.Ignore)]
        public DiscordWebhookFooter? Footer { get; set; }

        [JsonProperty("timestamp", NullValueHandling = NullValueHandling.Ignore)]
        public string? Timestamp { get; set; }
    }
}
