using Pokebot.Factories.Versions;
using Pokebot.Models.Pokemons;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pokebot.Models
{
    public class PokemonWatcher
    {
        public delegate void OpponentChangedEventHandler(Pokemon? pokemon);
        public event OpponentChangedEventHandler? OpponentChanged;

        public delegate void PartyChangedEventHandler(IReadOnlyCollection<Pokemon> pokemons);
        public event PartyChangedEventHandler? PartyChanged;

        public GameVersion GameVersion { get; }
        public string CodeParty { get; private set; }
        public string CodeOpponent { get; private set; }
        public GameState LastState { get; private set; }

        public PokemonWatcher(GameVersion gameVersion)
        {
            GameVersion = gameVersion;
            CodeParty = string.Empty;
            CodeOpponent = string.Empty;
            LastState = GameState.Unknow;
        }

        public void Execute(GameState state)
        {
            if (state != LastState)
            {
                LastState = state;

                //Opponent
                if (state == GameState.Battle)
                {
                    var opponent = GameVersion.Memory.GetOpponent();

                    if (opponent.PID.ToString("X") != CodeOpponent)
                    {
                        CodeOpponent = opponent.PID.ToString("X");
                        OpponentChanged?.Invoke(opponent);
                    }
                }
                else
                {
                    OpponentChanged?.Invoke(null);
                }

                //Party
                var party = GameVersion.Memory.GetParty();
                var code = string.Join("-", party.Select(x => x.PID.ToString("X")));
                if (code != CodeParty)
                {
                    CodeParty = code;
                    PartyChanged?.Invoke(party);
                }
            }
        }
    }
}
