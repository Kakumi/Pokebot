namespace Pokebot.Models.Pokemons
{
    public class PokemonIVS
    {
        public int HP { get; }
        public int Attack { get; }
        public int Defense { get; }
        public int Speed { get; }
        public int SpAttack { get; }
        public int SpDefense { get; }

        public PokemonIVS(int hP, int attack, int defense, int speed, int spAttack, int spDefense)
        {
            HP = hP;
            Attack = attack;
            Defense = defense;
            Speed = speed;
            this.SpAttack = spAttack;
            this.SpDefense = spDefense;
        }
    }
}
