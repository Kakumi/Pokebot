using Newtonsoft.Json;

namespace Pokebot.Services.DiscordWebhook.Models
{
    public class DiscordWebhookFooter
    {
        [JsonProperty("text")]
        public string Text { get; set; } = string.Empty;

        public DiscordWebhookFooter(string text)
        {
            Text = text;
        }
    }
}
