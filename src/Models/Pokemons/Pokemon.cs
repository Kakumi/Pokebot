using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Models.Pokemons
{
    public class Pokemon
    {
        public PokemonOriginalTrainer OriginalTrainer { get; }
        public string Nickname { get; }
        public PokemonLanguageCode Language { get; }
        public PokemonEggNameType EggName { get; }
        public PokemonMark Markings { get; }
        public int Checksum { get; }
        public int CalculatedChecksum { get; }

        //Growth
        public int Species { get; }
        public PokemonItem? HeldItem { get; }
        public uint Experience { get; }
        public int PPBonuses { get; }
        public int Friendship { get; }

        //Attacks
        public List<PokemonAttack> Moves { get; }

        //EVs & Condition
        public PokemonEVS EVs { get; }
        public PokemonCondition Conditions { get; }

        //Miscellaneous
        public PokemonPokerus Pokerus { get; }
        public PokemonLocation MetLocation { get; }
        public int MetLevel { get; }
        public PokemonOriginInfo Origin { get; }
        public PokemonIVS IVs { get; }
        public bool IsEgg { get; }
        public PokemonRibbon Ribbons { get; }

        //Others ?
        public string RealName { get; }
        public int DexId { get; }
        public bool IsShiny { get; }
        public PokemonNature Nature { get; }
        public List<PokemonType> Types { get; }
        public string Ability { get; }
        public PokemonType HiddenPower { get; }
        public int HiddenPowerDamage { get; }

        public Pokemon(PokemonOriginalTrainer originalTrainer, string nickname, PokemonLanguageCode language, PokemonEggNameType eggName, PokemonMark markings, int checksum, int calculatedChecksum, int species, PokemonItem? heldItem, uint experience, int pPBonuses, int friendship, List<PokemonAttack> moves, PokemonEVS eVs, PokemonCondition conditions, PokemonPokerus pokerus, PokemonLocation metLocation, int metLevel, PokemonOriginInfo origin, PokemonIVS iVs, bool isEgg, PokemonRibbon ribbons, string realName, int dexId, bool isShiny, PokemonNature nature, List<PokemonType> types, string ability, PokemonType hiddenPower, int hiddenPowerDamage)
        {
            OriginalTrainer = originalTrainer;
            Nickname = nickname;
            Language = language;
            EggName = eggName;
            Markings = markings;
            Checksum = checksum;
            CalculatedChecksum = calculatedChecksum;
            Species = species;
            HeldItem = heldItem;
            Experience = experience;
            PPBonuses = pPBonuses;
            Friendship = friendship;
            Moves = moves;
            EVs = eVs;
            Conditions = conditions;
            Pokerus = pokerus;
            MetLocation = metLocation;
            MetLevel = metLevel;
            Origin = origin;
            IVs = iVs;
            IsEgg = isEgg;
            Ribbons = ribbons;
            RealName = realName;
            DexId = dexId;
            IsShiny = isShiny;
            Nature = nature;
            Types = types;
            Ability = ability;
            HiddenPower = hiddenPower;
            HiddenPowerDamage = hiddenPowerDamage;
        }
    }
}
