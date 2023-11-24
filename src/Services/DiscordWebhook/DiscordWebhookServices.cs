using Newtonsoft.Json;
using Pokebot.Factories.Versions;
using Pokebot.Models.Pokemons;
using Pokebot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Pokebot.Models.Player;
using Pokebot.Utils;

namespace Pokebot.Services.DiscordWebhook
{
    public class DiscordWebhookServices
    {
        public string Url { get; }
        public DiscordWebhookServices(string url) {
            Url = url;
        }

        public void SendPokemonWebhook(Pokemon pokemon, PlayerData trainer)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Url))
                {
                    var webhook = new Models.DiscordWebhook(pokemon);
                    var json = JsonConvert.SerializeObject(webhook);

                    Task.Run(async () =>
                    {
                        try
                        {
                            using (var client = new HttpClient())
                            {
                                var content = new StringContent(json, Encoding.UTF8, "application/json");
                                await client.PostAsync(Url, content);
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Error(string.Format(Messages.DiscordWebhook_Failed, ex.Message));
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format(Messages.DiscordWebhook_Failed, ex.Message));
            }
        }
    }
}
