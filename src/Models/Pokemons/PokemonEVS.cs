using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Models.Pokemons
{
    public class PokemonEVS
    {
        public int HP { get; }
        public int Attack { get; }
        public int Defense { get; }
        public int Speed { get; }
        public int spAttack { get; }
        public int spDefense { get; }

        public PokemonEVS(int hP, int attack, int defense, int speed, int spAttack, int spDefense)
        {
            HP = hP;
            Attack = attack;
            Defense = defense;
            Speed = speed;
            this.spAttack = spAttack;
            this.spDefense = spDefense;
        }
    }
}
