using Pokebot.Models;
using Pokebot.Models.ActionRunners;
using Pokebot.Models.Config;
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
        VersionInfo VersionInfo { get; }
        HashData HashData { get; }
        GenerationInfo GenerationInfo { get; }
        IActionRunner ActionRunner { get; }

        int GetPartyCount();
        IReadOnlyList<Pokemon> GetParty();
        Pokemon GetOpponent();
        Pokemon ParsePokemon(byte[] bytesPokemon);
        PlayerData ParsePlayer(byte[] bytesGPlayer, byte[] bytesGPlayerPosition);
        PlayerData GetPlayer();
        GameState GetGameState();
        ICollection<GTask> GetTasks();
        int GetActionSelectionCursor();
        uint SetRandomSeed();
        void SetSeed(uint seed);
    }
}
