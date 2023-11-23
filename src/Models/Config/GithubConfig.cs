using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Models.Config
{
    public class GithubConfig
    {
        public string Url { get; }
        public string Owner { get; }
        public string Repository { get; }

        [JsonConstructor]
        public GithubConfig(string url, string owner, string repository)
        {
            Url = url;
            Owner = owner;
            Repository = repository;
        }
    }
}
