using System;
using System.Collections.Generic;
using BizHawk.Emulation.Common;
using Newtonsoft.Json;
using Pokebot.Factories.Versions;
using Pokebot.Models;
using Pokebot.Models.Pokemons;

namespace Pokebot.Services.DiscordWebhook.Models
{
    public class DiscordWebhook
    {
        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("avatar_url", NullValueHandling = NullValueHandling.Ignore)]
        public string AvatarUrl { get; set; }

        [JsonProperty("embeds")]
        public List<DiscordWebhookEmbed> Embeds { get; set; }

        public DiscordWebhook(string username, string content, string avatarUrl = null)
        {
            Content = content;
            Username = username;
            AvatarUrl = avatarUrl;
            Embeds = new List<DiscordWebhookEmbed>();
        }

        public DiscordWebhook(string content, Pokemon pokemon, EncounterStats stats, GameVersion gameVersion, IGameInfo gameInfo)
            : this(Messages.AppName, content)
        {
            var isShiny = pokemon.IsShiny;
            var spriteUrl = isShiny
                ? $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/shiny/{pokemon.DexId}.png"
                : $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{pokemon.DexId}.png";

            if (AvatarUrl == null)
            {
                AvatarUrl = spriteUrl;
            }

            var embed = new DiscordWebhookEmbed
            {
                Title = $"{(isShiny ? "✨ " : "")}{pokemon.RealName} • Lv. {pokemon.MetLevel}",
                Description = BuildDescription(pokemon),
                Color = isShiny ? 0xFFD700 : 0x5865F2,
                Thumbnail = new DiscordWebhookImage(spriteUrl),
                Footer = new DiscordWebhookFooter($"{gameInfo.Name} • {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC"),
                Timestamp = DateTimeOffset.UtcNow.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ"),
            };

            // Basic info
            embed.Fields.Add(new DiscordWebhookField("Ability", pokemon.Ability ?? "N/A", true));
            embed.Fields.Add(new DiscordWebhookField("Nature", pokemon.Nature?.Name ?? "N/A", true));
            embed.Fields.Add(new DiscordWebhookField("Gender", pokemon.GetGenderMessage(), true));

            // Held item / shiny / trainer
            embed.Fields.Add(new DiscordWebhookField("Held Item", pokemon.HeldItem?.Name ?? Messages.Item_Nothing, true));
            embed.Fields.Add(new DiscordWebhookField("Shiny", isShiny ? "Yes ✨" : "No", true));
            embed.Fields.Add(new DiscordWebhookField("Trainer", pokemon.OriginalTrainer?.Name ?? "N/A", true));

            // IVs
            embed.Fields.Add(new DiscordWebhookField("HP", pokemon.IVs.HP.ToString(), true));
            embed.Fields.Add(new DiscordWebhookField("Atk", pokemon.IVs.Attack.ToString(), true));
            embed.Fields.Add(new DiscordWebhookField("Def", pokemon.IVs.Defense.ToString(), true));
            embed.Fields.Add(new DiscordWebhookField("SpA", pokemon.IVs.SpAttack.ToString(), true));
            embed.Fields.Add(new DiscordWebhookField("SpD", pokemon.IVs.SpDefense.ToString(), true));
            embed.Fields.Add(new DiscordWebhookField("Spe", pokemon.IVs.Speed.ToString(), true));

            // Encounter stats
            embed.Fields.Add(new DiscordWebhookField("Game", gameInfo.Name, true));
            embed.Fields.Add(new DiscordWebhookField("Encounters", stats.Encountered.ToString(), true));
            embed.Fields.Add(new DiscordWebhookField("Shiny Encounters", stats.ShinyEncountered.ToString(), true));
            embed.Fields.Add(new DiscordWebhookField("Ratio", stats.Ratio, true));

            // Optional extra info if available in your Pokemon model
            AddOptionalFields(embed, pokemon);

            Embeds.Add(embed);
        }

        private static string BuildDescription(Pokemon pokemon)
        {
            var parts = new List<string> { $"**Dex ID:** #{pokemon.DexId}", $"**Level Met:** {pokemon.MetLevel}" };

            return string.Join("\n", parts);
        }

        private static void AddOptionalFields(DiscordWebhookEmbed embed, Pokemon pokemon)
        {
            try
            {
                if (pokemon.EVs != null)
                {
                    embed.Fields.Add(
                        new DiscordWebhookField(
                            "EVs",
                            $"HP: {pokemon.EVs.HP} | Atk: {pokemon.EVs.Attack} | Def: {pokemon.EVs.Defense}\n"
                                + $"SpA: {pokemon.EVs.spAttack} | SpD: {pokemon.EVs.spDefense} | Spe: {pokemon.EVs.Speed}",
                            false
                        )
                    );
                }
            }
            catch { }

            try
            {
                if (pokemon.OriginalTrainer != null)
                {
                    var otInfo = pokemon.OriginalTrainer.Name ?? "N/A";
                    otInfo += $"\nID: {pokemon.OriginalTrainer.Id}";
                    otInfo += $"\nSID: {pokemon.OriginalTrainer.SecretId}";
                }
            }
            catch { }
        }
    }
}
