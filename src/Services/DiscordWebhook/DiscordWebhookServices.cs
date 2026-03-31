using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BizHawk.Emulation.Common;
using Newtonsoft.Json;
using Pokebot.Factories.Versions;
using Pokebot.Models;
using Pokebot.Models.Pokemons;
using Pokebot.Utils;

namespace Pokebot.Services.DiscordWebhook
{
    public class DiscordWebhookServices
    {
        public string Url { get; }
        public string AdsUrl { get; }
        public string UserID { get; }

        public DiscordWebhookServices(string url, string adsUrl, string userID)
        {
            Url = url;
            UserID = userID;
            AdsUrl = adsUrl;
        }

        public void SendPokemonWebhook(Pokemon pokemon, EncounterStats stats, GameVersion gameVersion, IGameInfo gameInfo)
        {
            try
            {
                string content;
                if (string.IsNullOrWhiteSpace(UserID))
                {
                    content = Messages.Discord_Content;
                }
                else
                {
                    var pingUserID = $"<@{UserID}>";
                    content = string.Format(Messages.Discord_ContentWithUser, pingUserID, Messages.Discord_Content);
                }

                var webhook = new Models.DiscordWebhook(content, pokemon, stats, gameVersion, gameInfo);
                var json = JsonConvert.SerializeObject(webhook);

                if (!string.IsNullOrWhiteSpace(Url))
                {
                    SendWebhook(json, Url);
                }

                if (Url != AdsUrl)
                {
                    SendWebhook(json, AdsUrl);
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format(Messages.DiscordWebhook_Failed, ex.Message));
            }
        }

        private void SendWebhook(string json, string url)
        {
            Task.Run(async () =>
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        await client.PostAsync(url, content);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(string.Format(Messages.DiscordWebhook_Failed, ex.Message));
                }
            });
        }
    }
}
