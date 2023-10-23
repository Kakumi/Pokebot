using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Models.Pokemons
{
    public class PokemonOriginInfo
    {
        public bool IsMale { get; }
        public PokemonItem? Pokeball { get; } //null if not catched
        public PokemonGameOfOrigin Game { get; }
        public bool IsHatched { get; }

        public PokemonOriginInfo(bool isMale, PokemonItem? pokeball, PokemonGameOfOrigin game, bool isHatched)
        {
            IsMale = isMale;
            Pokeball = pokeball;
            Game = game;
            IsHatched = isHatched;
        }
    }
}
