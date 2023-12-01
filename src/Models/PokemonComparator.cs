using Pokebot.Models.Pokemons;
using System.Linq;

namespace Pokebot.Models
{
    public class PokemonComparator
    {
        public bool IsShiny { get; set; }
        public int IndexPokemon { get; set; }
        public int IndexNature { get; set; }
        public int IndexType { get; set; }
        public int IndexHeldItem { get; set; }
        public bool ExactIV { get; set; }
        public int IVHP { get; set; }
        public int IVSpeed { get; set; }
        public int IVAttack { get; set; }
        public int IVDefense { get; set; }
        public int IVSpAttack { get; set; }
        public int IVSpDefense { get; set; }

        public PokemonComparator()
        {
            IsShiny = false;
            IndexPokemon = -1;
            IndexNature = -1;
            IndexType = -1;
            IndexHeldItem = -1;
            ExactIV = false;
            IVHP = -1;
            IVSpeed = -1;
            IVAttack = -1;
            IVDefense = -1;
            IVSpAttack = -1;
            IVSpDefense = -1;
        }

        public bool Compare(Pokemon pokemon)
        {
            if (IsShiny && !pokemon.IsShiny)
            {
                return false;
            }

            if (IndexPokemon != -1 && pokemon.Species != IndexPokemon)
            {
                return false;
            }

            if (IndexNature != -1 && pokemon.Nature.Id != IndexNature)
            {
                return false;
            }

            if (IndexType != -1 && pokemon.Types.All(x => x.Id != IndexType))
            {
                return false;
            }

            if (IndexHeldItem != -1 && pokemon.HeldItem?.Id != IndexHeldItem)
            {
                return false;
            }

            if (!CheckIV(IVHP, pokemon.IVs.HP))
            {
                return false;
            }

            if (!CheckIV(IVSpeed, pokemon.IVs.Speed))
            {
                return false;
            }

            if (!CheckIV(IVAttack, pokemon.IVs.Attack))
            {
                return false;
            }

            if (!CheckIV(IVDefense, pokemon.IVs.Defense))
            {
                return false;
            }

            if (!CheckIV(IVSpAttack, pokemon.IVs.SpAttack))
            {
                return false;
            }

            if (!CheckIV(IVSpDefense, pokemon.IVs.SpDefense))
            {
                return false;
            }

            return true;
        }

        private bool CheckIV(int value, int current)
        {
            if (ExactIV && value == current)
            {
                return true;
            }

            return current >= value;
        }
    }
}
