﻿using Pokebot.Models.Pokemons;
using System.Collections.Generic;

namespace Pokebot.Models.Config
{
    public class GenerationInfo
    {
        public int Code { get; set; }
        public List<PokemonItem> Items { get; set; }
        public List<PokemonLocation> Locations { get; set; }
        public List<PokemonMove> Moves { get; set; }
        public List<PokemonGameOfOrigin> GameOfOrigin { get; set; }
        public List<PokemonType> Types { get; set; }
        public List<PokemonNature> Natures { get; set; }
        public List<PokemonType> HiddenPowers { get; set; }
        public List<PokemonConfig> Pokemons { get; set; }
        public List<string> Characters { get; set; }
        public List<string> CharactersJP { get; set; }
    }
}
