using BizHawk.Client.Common;
using BizHawk.Emulation.Common;
using Pokebot.Models.Config;
using Pokebot.Models.Pokemons;
using Pokebot.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokebot.Models.Versions
{
    public class GameVersion : IGameVersion
    {
        public ApiContainer APIContainer { get; }
        public IReadOnlyList<Symbol> Symbols { get; }
        public GenerationInfo GenerationInfo { get; }

        private static readonly string[] _subStructureTypes = new string[]
        {
            "GAEM", "GAME", "GEAM", "GEMA", "GMAE", "GMEA", "AGEM", "AGME", "AEGM", "AEMG", "AMGE", "AMEG",
            "EGAM", "EGMA", "EAGM", "EAMG", "EMGA", "EMAG", "MGAE", "MGEA", "MAGE", "MAEG", "MEGA", "MEAG"
        };

        public GameVersion(ApiContainer apiContainer, GenerationInfo generationInfo, byte[] resource)
        {
            APIContainer = apiContainer;
            GenerationInfo = generationInfo;
            Symbols = SymbolReader.LoadSymbols(resource);
        }

        protected virtual List<string> GetCharacters()
        {
            //All characters for HEXADECIMAL values (0-255)
            return new List<string>()
            {	 	 	 
                "","À","Á","Â","Ç","È","É","Ê","Ë","Ì","","Î","Ï","Ò","Ó","Ô",
                "Œ","Ù","Ú","Û","Ñ","ß","à","á","","ç","è","é","ê","ë","ì","",
                "î","ï","ò","ó","ô","œ","ù","ú","û","ñ","º","ª","ᵉʳ","&","+","",
                "","","","","Lv","=",";","","","","","","","","","",
                "","","","","","","","","","","","","","","","",
                "▯","¿","¡","PK","MN","PO","Ké","","","","Í","%","(",")","","",
                "","","","","","","","","â","","","","","","","",
                "","","","","","","","","","⬆","⬇","⬅","➡","*","*","*",
                "*","*","*","*","ᵉ","<",">","","","","","","","","","",
                "","","","","","","","","","","","","","","","",
                "ʳᵉ","0","1","2","3","4","5","6","7","8","9","!","?",".","-","・",
                "…","“","”","‘","’","♂","♀","$",",","×","/","A","B","C","D","E",
                "F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U",
                "V","W","X","Y","Z","a","b","c","d","e","f","g","h","i","j","k",
                "l","m","n","o","p","q","r","s","t","u","v","w","x","y","z","▶",
                ":","Ä","Ö","Ü","ä","ö","ü","","","","","","","","",""
            };
        }

        private string GetBytesText(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            var dic = GetCharacters();

            foreach (var b in bytes) {
                sb.Append(dic[b]);
            }

            return sb.ToString();
        }

        public virtual Pokemon ParsePokemon(byte[] bytesPokemon)
        {
            #region Main Info
            //https://bulbapedia.bulbagarden.net/wiki/Pok%C3%A9mon_data_structure_(Generation_III)

            var PID = bytesPokemon.Take(4).ToUInt32();

            //Get original trainer
            var OT = bytesPokemon.Skip(4).Take(4).ToUInt32();
            var OTID = bytesPokemon.Skip(4).Take(2).ToInt16() & 0xFFFF; //without 0xFFFF it can be negative and it's an error -> https://coderanch.com/t/662971/java/bytes-array-positive-values-decimal
            var OTSID = bytesPokemon.Skip(6).Take(2).ToInt16() & 0xFFFF;

            //Get nickname
            var nickname = GetBytesText(bytesPokemon.Skip(8).Take(10).ToArray());
            //Get language
            var language = (PokemonLanguageCode)bytesPokemon[18];
            //Get egg name
            var eggName = (PokemonEggNameType)bytesPokemon[19];
            //Get marks
            var marks = (PokemonMark)bytesPokemon[27];
            //Get checksum
            var checksum = bytesPokemon.Skip(28).Take(2).ToInt16();
            //Get pokemon data inside party or box
            var pokemonData = bytesPokemon.Skip(32).Take(80);
            //Get Trainer name
            var OTName = GetBytesText(bytesPokemon.Skip(20).Take(7).ToArray());

            //Define subStructures
            var decryptKey = PID ^ OT;
            var order = PID % 0x18;
            var subStructureType = _subStructureTypes[order];
            var subStructuresData = new Dictionary<char, byte[]>();
            int calculatedChecksum = 0;

            for (int i = 0; i < 4; i++)
            {
                var subStructure = subStructureType[i];
                var subStructureData = pokemonData.Skip(i * 12).Take(12).ToArray();
                subStructuresData[subStructure] = DecryptSubStructure(subStructureData, decryptKey);

                for (int k = 0; k < 6; k++)
                {
                    byte[] bytesToAdd = subStructuresData[subStructure].Skip(k * 2).Take(2).ToArray();
                    calculatedChecksum += bytesToAdd.ToInt16();
                    calculatedChecksum &= 0xFFFF;
                }
            }

            var valid = calculatedChecksum == checksum;
            if (!valid)
            {
                throw new Exception($"Pokemon is not valid");
            }

            #endregion

            #region Growth Info
            //https://bulbapedia.bulbagarden.net/wiki/Pok%C3%A9mon_data_substructures_(Generation_III)

            var species = subStructuresData['G'].Take(2).ToInt16();
            var heldItem = GenerationInfo.Items.First(x => x.Id == subStructuresData['G'].Skip(2).Take(2).ToInt16());
            var experience = subStructuresData['G'].Skip(4).Take(4).ToUInt32();
            var ppBonuses = (int)subStructuresData['G'][8];
            var friendship = (int)subStructuresData['G'][9];

            #endregion

            #region Attacks Info
            //https://bulbapedia.bulbagarden.net/wiki/Pok%C3%A9mon_data_substructures_(Generation_III)

            var attacks = new List<PokemonAttack>();
            for (var i = 0; i < 4; i++)
            {
                var codeMove = subStructuresData['A'].Skip(i * 2).Take(2).ToInt16();
                if (codeMove != 0)
                {
                    var move = GenerationInfo.Moves.First(x => x.Id == codeMove);
                    var ppMove = (int)subStructuresData['A'][8 + i];
                    attacks.Add(new PokemonAttack(move, ppMove));
                }
            }

            #endregion

            #region EVs & Condition
            //https://bulbapedia.bulbagarden.net/wiki/Pok%C3%A9mon_data_substructures_(Generation_III)

            var evHP = (int)subStructuresData['E'][0];
            var evAttack = (int)subStructuresData['E'][1];
            var evDefense = (int)subStructuresData['E'][2];
            var evSpeed = (int)subStructuresData['E'][3];
            var evSpAttack = (int)subStructuresData['E'][4];
            var evSpDefense = (int)subStructuresData['E'][5];
            var coolness = (int)subStructuresData['E'][6];
            var beauty = (int)subStructuresData['E'][7];
            var cuteness = (int)subStructuresData['E'][8];
            var smartness = (int)subStructuresData['E'][9];
            var toughness = (int)subStructuresData['E'][10];
            var feel = (int)subStructuresData['E'][11];

            #endregion

            #region Miscellaneous
            //https://bulbapedia.bulbagarden.net/wiki/Pok%C3%A9mon_data_substructures_(Generation_III)

            var pokerusDays = subStructuresData['M'][0] & 0xF;
            var pokerusStrain = subStructuresData['M'][0] >> 0x4;
            var metLocation = GenerationInfo.Locations.First(x => x.Id == (int)subStructuresData['M'][1]);
            var originsInfo = subStructuresData['M'].Skip(2).Take(2).ToInt16();
            var trainerGenderMale = originsInfo >> 0xF == 0; //Move 15 bits
            var metLevel = originsInfo & 0x7F; //Compare 7 bits to 01111111
            var isHatched = metLevel == 0;
            var game = GenerationInfo.GameOfOrigin.First(x => x.Id == (originsInfo >> 0x7 & 0xF)); //Move 7 bits to the right and compare with 1111
            var pokeball = GenerationInfo.Items.FirstOrDefault(x => x.Id == (originsInfo >> 0xB & 0xF)); //Move 11 bits to the right and compare with 1111

            var IVsEggAbilityInfo = subStructuresData['M'].Skip(4).Take(4).ToUInt32();
            var ivHP = IVsEggAbilityInfo & 0x1F;
            var ivAttack = IVsEggAbilityInfo >> 5 & 0x1F;
            var ivDefense = IVsEggAbilityInfo >> 10 & 0x1F;
            var ivSpeed = IVsEggAbilityInfo >> 15 & 0x1F;
            var ivSpAttack = IVsEggAbilityInfo >> 20 & 0x1F;
            var ivSpDefense = IVsEggAbilityInfo >> 25 & 0x1F;
            var isEgg = (IVsEggAbilityInfo >> 30 & 0x1) == 1;
            var abilityId = IVsEggAbilityInfo >> 31 & 0x1;

            var ribbonsAndObedienceInfo = subStructuresData['M'].Skip(8).Take(4).ToUInt32();
            var coolRibbon = (PokemonRibbonRarity)(ribbonsAndObedienceInfo & 0x7);
            var beautyRibbon = (PokemonRibbonRarity)(ribbonsAndObedienceInfo >> 0x3 & 0x7);
            var cuteRibbon = (PokemonRibbonRarity)(ribbonsAndObedienceInfo >> 0x6 & 0x7);
            var smartRibbon = (PokemonRibbonRarity)(ribbonsAndObedienceInfo >> 0x9 & 0x7);
            var toughRibbon = (PokemonRibbonRarity)(ribbonsAndObedienceInfo >> 0xC & 0x7);
            var hasChampionRibbon = (ribbonsAndObedienceInfo >> 0xF & 0x1) == 1;
            var hasWinningRibbon = (ribbonsAndObedienceInfo >> 0x10 & 0x1) == 1;
            var hasVictoryRibbon = (ribbonsAndObedienceInfo >> 0x11 & 0x1) == 1;
            var hasArtistRibbon = (ribbonsAndObedienceInfo >> 0x12 & 0x1) == 1;
            var hasEffortRibbon = (ribbonsAndObedienceInfo >> 0x13 & 0x1) == 1;
            var hasBattleChampionRibbon = (ribbonsAndObedienceInfo >> 0x14 & 0x1) == 1;
            var hasRegionalChampionRibbon = (ribbonsAndObedienceInfo >> 0x15 & 0x1) == 1;
            var hasNationalChampionRibbon = (ribbonsAndObedienceInfo >> 0x16 & 0x1) == 1;
            var hasCountryRibbon = (ribbonsAndObedienceInfo >> 0x17 & 0x1) == 1;
            var hasNationalRibbon = (ribbonsAndObedienceInfo >> 0x18 & 0x1) == 1;
            var hasEarthRibbon = (ribbonsAndObedienceInfo >> 0x19 & 0x1) == 1;
            var hasWorldRibbon = (ribbonsAndObedienceInfo >> 0x1A & 0x1) == 1;

            #endregion

            #region Others Info
            //https://bulbapedia.bulbagarden.net/wiki/Pok%C3%A9mon_species_data_structure_(Generation_III)

            //TODO
            var pokemonConfig = GenerationInfo.Pokemons.First(x => x.Id == species);
            var nationalDexId = pokemonConfig.DexId;
            var ability = abilityId < pokemonConfig.Abilities.Count ? pokemonConfig.Abilities[(int) abilityId] : pokemonConfig.Abilities[0];
            var types = pokemonConfig.Types;
            var realName = char.ToUpper(pokemonConfig.Name[0]) + pokemonConfig.Name.Substring(1);

            var nature = GenerationInfo.Natures.First(x => x.Id == (PID % 25));
            var hiddenPowerCalculated = Math.Floor((double)(
                ((ivHP % 2) +
                (2 * (ivAttack % 2)) +
                (4 * (ivDefense % 2)) +
                (8 * (ivSpeed % 2)) +
                (16 * (ivSpAttack % 2)) +
                (32 * (ivSpDefense % 2))) * 15
            ) / 63);
            var hiddenPower = GenerationInfo.HiddenPowers.First(x => x.Id == hiddenPowerCalculated);
            var hiddenPowerDamage = (int) Math.Floor((
                ((Math.Floor(ivHP / (double)2) % 2) + //0
                (2 * (Math.Floor(ivAttack / (double) 2) % 2)) + //1
                (4 * (Math.Floor(ivDefense / (double)2) % 2)) + //0
                (8 * (Math.Floor(ivSpeed / (double)2) % 2)) + //1
                (16 * (Math.Floor(ivSpAttack / (double)2) % 2)) + //1
                (32 * (Math.Floor(ivSpDefense / (double)2) % 2))) * 40 //0
            ) / 63) + 30; //2 + 8 + 16
            var shinyValue = (OTID ^ OTSID ^ (bytesPokemon.Take(2).ToInt16() & 0xFFFF) ^ (bytesPokemon.Skip(2).Take(2).ToInt16() & 0xFFFF));
            var shiny = shinyValue < 8;

            #endregion

            #region Create Objects

            var originalTrainer = new PokemonOriginalTrainer(OTID, OTSID, OTName);
            var evs = new PokemonEVS(evHP, evAttack, evDefense, evSpeed, evSpAttack, evSpDefense);
            var condition = new PokemonCondition(coolness, beauty, cuteness, smartness, toughness, feel);
            var pokerus = new PokemonPokerus(pokerusDays, pokerusStrain);
            var originInfo = new PokemonOriginInfo(trainerGenderMale, pokeball, game, isHatched);
            var ivs = new PokemonIVS((int) ivHP, (int) ivAttack, (int) ivDefense, (int) ivSpeed, (int) ivSpAttack, (int) ivSpDefense);
            var ribbons = new PokemonRibbon(
                coolRibbon,
                beautyRibbon,
                cuteRibbon,
                smartRibbon,
                toughRibbon,
                hasChampionRibbon,
                hasWinningRibbon,
                hasVictoryRibbon,
                hasArtistRibbon,
                hasEffortRibbon,
                hasBattleChampionRibbon,
                hasRegionalChampionRibbon,
                hasNationalChampionRibbon,
                hasCountryRibbon,
                hasNationalRibbon,
                hasEarthRibbon,
                hasWorldRibbon
            );

            #endregion

            if (bytesPokemon.Length > 80)
            {
                //Only inside party not PC
                var statusCondition = (PokemonStatusType)bytesPokemon.Skip(80).Take(4).ToUInt32(); //TODO Tester
                var level = (int)bytesPokemon[84];
                var hasPokerus = bytesPokemon[85] == 0; //0 is cured
                var currentHP = bytesPokemon.Skip(86).Take(2).ToInt16();
                var totalHP = bytesPokemon.Skip(88).Take(2).ToInt16();
                var attack = bytesPokemon.Skip(90).Take(2).ToInt16();
                var defense = bytesPokemon.Skip(92).Take(2).ToInt16();
                var speed = bytesPokemon.Skip(94).Take(2).ToInt16();
                var spAttack = bytesPokemon.Skip(96).Take(2).ToInt16();
                var spDefense = bytesPokemon.Skip(98).Take(2).ToInt16();

                return new PartyPokemon(
                    originalTrainer,
                    nickname,
                    language,
                    eggName,
                    marks,
                    checksum,
                    calculatedChecksum,
                    species,
                    heldItem,
                    experience,
                    ppBonuses,
                    friendship,
                    attacks,
                    evs,
                    condition,
                    pokerus,
                    metLocation,
                    metLevel,
                    originInfo,
                    ivs,
                    isEgg,
                    ribbons,
                    realName,
                    nationalDexId,
                    shiny,
                    nature,
                    types,
                    ability,
                    hiddenPower,
                    hiddenPowerDamage,
                    statusCondition,
                    level,
                    hasPokerus, currentHP,
                    totalHP,
                    attack,
                    defense,
                    speed,
                    spAttack,
                    spDefense
                );
            }

            return new Pokemon(
                originalTrainer,
                nickname,
                language,
                eggName,
                marks,
                checksum,
                calculatedChecksum,
                species,
                heldItem,
                experience,
                ppBonuses,
                friendship,
                attacks,
                evs,
                condition,
                pokerus,
                metLocation,
                metLevel,
                originInfo,
                ivs,
                isEgg,
                ribbons,
                realName,
                nationalDexId,
                shiny,
                nature,
                types,
                ability,
                hiddenPower,
                hiddenPowerDamage
            );
        }

        public virtual IReadOnlyList<Pokemon> GetParty()
        {
            var list = new List<Pokemon>();
            var partyCountSYM = Symbols.First(x => x.Name == "gPlayerPartyCount");
            var partySYM = Symbols.First(x => x.Name == "gPlayerParty");

            var nbPokemons = (int) SymbolReader.ReadSymbol(APIContainer, partyCountSYM, 0, 1)[0];
            for(int i = 0; i < nbPokemons; i++)
            {
                var offset = i * 100;
                var bytesPokemon = SymbolReader.ReadSymbol(APIContainer, partySYM, offset, 100);

                list.Add(ParsePokemon(bytesPokemon.ToArray()));
            }

            return list;
        }

        public virtual Pokemon GetOpponent()
        {
            var list = new List<Pokemon>();
            var partySYM = Symbols.First(x => x.Name == "gEnemyParty");
            
            var bytesPokemon = SymbolReader.ReadSymbol(APIContainer, partySYM, 0, 100);

            return ParsePokemon(bytesPokemon.ToArray());
        }

        protected virtual byte[] DecryptSubStructure(byte[] data, uint key)
        {
            uint[] decryptedValues = new uint[3];

            for (int i = 0; i < 3; i++)
            {
                uint value = BitConverter.ToUInt32(data, i * 4) ^ key;
                decryptedValues[i] = value;
            }

            byte[] result = new byte[12];
            Buffer.BlockCopy(decryptedValues, 0, result, 0, 12);

            return result;
        }
    }
}
