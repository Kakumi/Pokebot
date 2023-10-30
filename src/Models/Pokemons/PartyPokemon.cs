using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Models.Pokemons
{
    public class PartyPokemon : Pokemon
    {
        public PartyPokemon(PokemonOriginalTrainer originalTrainer, string nickname, PokemonLanguageCode language, PokemonEggNameType eggName, PokemonMark markings, int checksum, int calculatedChecksum, int species, PokemonItem? heldItem, uint experience, int pPBonuses, int friendship, List<PokemonAttack> moves, PokemonEVS eVs, PokemonCondition conditions, PokemonPokerus pokerus, PokemonLocation metLocation, int metLevel, PokemonOriginInfo origin, PokemonIVS iVs, bool isEgg, PokemonRibbon ribbons, string realName, int dexId, bool shiny, PokemonNature nature, List<PokemonType> types, string ability, PokemonType hiddenPower, int hiddenPowerDamage, PokemonStatusType status, int level, bool hasPokerus, int currentHP, int hP, int attack, int defense, int speed, int spAttack, int spDefense)
            : base(originalTrainer, nickname, language, eggName, markings, checksum, calculatedChecksum, species, heldItem, experience, pPBonuses, friendship, moves, eVs, conditions, pokerus, metLocation, metLevel, origin, iVs, isEgg, ribbons, realName, dexId, shiny, nature, types, ability, hiddenPower, hiddenPowerDamage)
        {
            Status = status;
            Level = level;
            HasPokerus = hasPokerus;
            CurrentHP = currentHP;
            HP = hP;
            Attack = attack;
            Defense = defense;
            Speed = speed;
            SpAttack = spAttack;
            SpDefense = spDefense;
        }

        public PokemonStatusType Status { get; }
        public int Level { get; }
        public bool HasPokerus { get; }
        public int CurrentHP { get; }
        public int HP { get; }
        public int Attack { get; }
        public int Defense { get; }
        public int Speed { get; }
        public int SpAttack { get; }
        public int SpDefense { get; }
    }
}
