using Newtonsoft.Json;

namespace Pokebot.Services.DiscordWebhook.Models
{
    public class DiscordWebhookImage
    {
        [JsonProperty("url")]
        public string Url { get; set; } = string.Empty;

        public DiscordWebhookImage(string url)
        {
            Url = url;
        }
    }
}
