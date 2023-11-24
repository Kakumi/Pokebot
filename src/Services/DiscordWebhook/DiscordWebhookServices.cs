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
        public string UserID { get; }
        public DiscordWebhookServices(string url, string userID) {
            Url = url;
            UserID = userID;
        }

        public void SendPokemonWebhook(Pokemon pokemon)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Url))
                {
                    string content;
                    if (string.IsNullOrWhiteSpace(UserID))
                    {
                        content = Messages.Discord_Content;
                    } else
                    {
                        var pingUserID = $"<@{UserID}>";
                        content = string.Format(Messages.Discord_ContentWithUser, pingUserID, Messages.Discord_Content);
                    }

                    var webhook = new Models.DiscordWebhook(content, pokemon);
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
