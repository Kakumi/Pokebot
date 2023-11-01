using Pokebot.Models;
using Pokebot.Models.Player;
using Pokebot.Models.Pokemons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokebot.Factories.Bots
{
    public interface IBot
    {
        delegate void PokemonEncounterEventHandler(Pokemon pokemon);
        event PokemonEncounterEventHandler? PokemonEncountered;

        delegate void PokemonFoundEventHandler(Pokemon pokemon);
        event PokemonFoundEventHandler? PokemonFound;

        delegate void StateChangedEventHandler(bool enabled);
        event StateChangedEventHandler? StateChanged;

        bool Enabled { get; }
        void Start();
        void Stop();
        void Execute(PlayerData playerData, GameState state);
        UserControl GetPanel();
    }
}
