using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Services.Github.Models
{
    public class ReleaseResponse
    {
        [JsonProperty("html_url")]
        public string Url { get; }

        [JsonProperty("tag_name")]
        public string TagName { get; }

        [JsonProperty("name")]
        public string Name { get; }

        [JsonConstructor]
        public ReleaseResponse(string url, string tagName, string name)
        {
            Url = url;
            TagName = tagName;
            Name = name;
        }
    }
}
