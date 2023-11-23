using Newtonsoft.Json;
using Pokebot.Exceptions;
using Pokebot.Services.Github.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Services.Github
{
    public class GithubServices
    {
        public string Url { get; }

        public GithubServices(string url)
        {
            Url = url;
        }

        public async Task<ReleaseResponse> GetLatestRelease(string owner, string repo)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                client.DefaultRequestHeaders.Add("User-Agent", "Pokebot");
                var response = await client.GetAsync($"{Url}/repos/{owner}/{repo}/releases/latest");
                if (response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync();
                    var releaseResponse = JsonConvert.DeserializeObject<ReleaseResponse>(body);
                    if (releaseResponse != null)
                    {
                        return releaseResponse;
                    }
                }

                throw new PokebotException($"{Messages.API_GithubError} ({response.StatusCode})");
            }

            throw new PokebotException(Messages.API_GithubError);
        }
    }
}
