using Newtonsoft.Json;

namespace Pokebot.Services.DiscordWebhook.Models
{
    public class DiscordWebhookField
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
        [JsonProperty("inline")]
        public bool Inline { get; set; }

        public DiscordWebhookField(string name, string value, bool inline)
        {
            Name = name;
            Value = value;
            Inline = inline;
        }
    }
}
