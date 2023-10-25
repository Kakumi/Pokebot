using Pokebot.Models;
using Pokebot.Models.Player;
using Pokebot.Models.Pokemons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Factories.Versions
{
    public interface IGameVersion
    {
        int GetPartyCount();
        IReadOnlyList<Pokemon> GetParty();
        Pokemon GetOpponent();
        Pokemon ParsePokemon(byte[] bytesPokemon);
        PlayerData ParsePlayer(byte[] bytesGPlayer, byte[] bytesGPlayerPosition);
        PlayerData GetPlayer();
        GameState GetGameState();
        ICollection<GTask> GetTasks();
        int GetActionSelectionCursor();
        IReadOnlyList<string> GetStarters();
        string GetVersionName();
        bool ExecuteStarter(int indexStarter);
        uint SetRandomSeed();
    }
}
