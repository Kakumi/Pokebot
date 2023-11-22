using Pokebot.Models.Config;
using Pokebot.Models.Player;
using Pokebot.Models.Pokemons;
using System.Collections.Generic;

namespace Pokebot.Models.Memory
{
    public interface IGameMemory
    {
        VersionInfo VersionInfo { get; }
        HashData HashData { get; }
        GenerationInfo GenerationInfo { get; }

        int GetPartyCount();
        IReadOnlyList<Pokemon> GetParty();
        Pokemon GetOpponent();
        Pokemon ParsePokemon(byte[] bytesPokemon);
        PlayerData ParsePlayer(byte[] bytesGPlayer, byte[] bytesGPlayerPosition);
        PlayerData GetPlayer();
        GameState GetGameState();
        ICollection<GTask> GetTasks();
        int GetActionSelectionCursor();
        uint SetRandomRNG();
        uint SetRandomSeed();
        void SetSeed(uint seed);
        uint GetSeed();
        int GetTID();
        int GetSID();
    }
}
