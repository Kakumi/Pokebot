using Newtonsoft.Json;

namespace Pokebot.Models.Config
{
    public class PokemonBaseStats
    {
        public int HP { get; }
        public int Attack { get; }
        public int Defense { get; }
        public int SpecialAttack { get; }
        public int SpecialDefense { get; }
        public int Speed { get; }

        [JsonConstructor]
        public PokemonBaseStats(int hP, int attack, int defense, int specialAttack, int specialDefense, int speed)
        {
            HP = hP;
            Attack = attack;
            Defense = defense;
            SpecialAttack = specialAttack;
            SpecialDefense = specialDefense;
            Speed = speed;
        }
    }
}
