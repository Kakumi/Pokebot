using Pokebot.Models;
using Pokebot.Models.Player;
using Pokebot.Models.Pokemons;
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
        bool UseDelay();
        void Start();
        void Stop();
        void Execute(PlayerData playerData, GameState state);
        void UpdateUI(GameState state);
        UserControl GetPanel();
    }
}
