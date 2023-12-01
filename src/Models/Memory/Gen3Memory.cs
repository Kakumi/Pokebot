using BizHawk.Client.Common;
using Pokebot.Models.Config;
using Pokebot.Models.Player;
using Pokebot.Models.Pokemons;
using Pokebot.Symbols;
using Pokebot.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pokebot.Models.Memory
{
    public class Gen3Memory : IGameMemory
    {
        public ApiContainer APIContainer { get; }
        public IReadOnlyList<Symbol> Symbols { get; }
        public VersionInfo VersionInfo { get; }
        public HashData HashData { get; }
        public GenerationInfo GenerationInfo { get; }


        private static readonly string[] _subStructureTypes = new string[]
        {
            "GAEM", "GAME", "GEAM", "GEMA", "GMAE", "GMEA", "AGEM", "AGME", "AEGM", "AEMG", "AMGE", "AMEG",
            "EGAM", "EGMA", "EAGM", "EAMG", "EMGA", "EMAG", "MGAE", "MGEA", "MAGE", "MAEG", "MEGA", "MEAG"
        };

        public Gen3Memory(ApiContainer apiContainer, VersionInfo versionInfo, HashData hashData, GenerationInfo generationInfo)
        {
            APIContainer = apiContainer;
            VersionInfo = versionInfo;
            HashData = hashData;
            GenerationInfo = generationInfo;
            Symbols = LoadSymbols(hashData);
        }

        private IReadOnlyList<Symbol> LoadSymbols(HashData hashData)
        {
            var symbols = new List<Symbol>();

            //Load main symbol file
            var mainFileData = ResourceSymbols.ResourceManager.GetObject(hashData.Symbols.Main);
            if (mainFileData is byte[] mainBytes)
            {
                symbols = SymbolUtil.Load(mainBytes).ToList();

                //Load and replace extra symbols if they exists
                foreach (var file in hashData.Symbols.Patches)
                {
                    var data = ResourceSymbols.ResourceManager.GetObject(file);
                    if (data is byte[] bytes)
                    {
                        var tempSymbols = SymbolUtil.Load(bytes);
                        foreach (var tempSymbol in tempSymbols)
                        {
                            var symbolFound = symbols.FirstOrDefault(x => x.Name == tempSymbol.Name);
                            if (symbolFound != null)
                            {
                                symbols.Remove(symbolFound);

                                long address = tempSymbol.Address;
                                char letter = symbolFound.Letter;
                                string name = symbolFound.Name;
                                int size;
                                if (tempSymbol.Letter == 'c') //c is custom, no changes except for address
                                {
                                    size = symbolFound.Size;
                                }
                                else
                                {
                                    size = tempSymbol.Size;
                                }

                                symbols.Add(new Symbol(address, letter, size, name));
                            }
                        }
                    }
                }
            }

            return symbols;
        }

        private string GetBytesText(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var b in bytes)
            {
                sb.Append(GenerationInfo.Characters[b]);
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
            var OTID = bytesPokemon.Skip(4).Take(2).ToUInt16(); //without 0xFFFF it can be negative and it's an error -> https://coderanch.com/t/662971/java/bytes-array-positive-values-decimal
            var OTSID = bytesPokemon.Skip(6).Take(2).ToUInt16();

            //Get nickname
            var nickname = GetBytesText(bytesPokemon.Skip(8).Take(10).ToArray());
            //Get language
            var language = (PokemonLanguageCode)bytesPokemon[18];
            //Get egg name
            var eggName = (PokemonEggNameType)bytesPokemon[19];
            //Get marks
            var marks = (PokemonMark)bytesPokemon[27];
            //Get checksum
            var checksum = bytesPokemon.Skip(28).Take(2).ToUInt16();
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
                    calculatedChecksum += bytesToAdd.ToUInt16();
                    calculatedChecksum &= 0xFFFF;
                }
            }

            var valid = calculatedChecksum == checksum;
            if (!valid)
            {
                throw new Exception(Messages.Pokemon_NotValid);
            }

            #endregion

            #region Growth Info
            //https://bulbapedia.bulbagarden.net/wiki/Pok%C3%A9mon_data_substructures_(Generation_III)

            var species = subStructuresData['G'].Take(2).ToUInt16();
            var heldItem = GenerationInfo.Items.First(x => x.Id == subStructuresData['G'].Skip(2).Take(2).ToUInt16());
            var experience = subStructuresData['G'].Skip(4).Take(4).ToUInt32();
            var ppBonuses = (int)subStructuresData['G'][8];
            var friendship = (int)subStructuresData['G'][9];

            #endregion

            #region Attacks Info
            //https://bulbapedia.bulbagarden.net/wiki/Pok%C3%A9mon_data_substructures_(Generation_III)

            var attacks = new List<PokemonAttack>();
            for (var i = 0; i < 4; i++)
            {
                var codeMove = subStructuresData['A'].Skip(i * 2).Take(2).ToUInt16();
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
            var metLocation = GenerationInfo.Locations.First(x => x.Id == subStructuresData['M'][1]);
            var originsInfo = subStructuresData['M'].Skip(2).Take(2).ToUInt16();
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

            var pokemonConfig = GenerationInfo.Pokemons.First(x => x.Id == species);
            var nationalDexId = pokemonConfig.DexId;
            var ability = abilityId < pokemonConfig.Abilities.Count ? pokemonConfig.Abilities[(int)abilityId] : pokemonConfig.Abilities[0];
            var types = GenerationInfo.Types.Where(x =>
            {
                return pokemonConfig.Types.Any(y => y.Equals(x.Name, StringComparison.InvariantCultureIgnoreCase));
            }).ToList();
            var realName = char.ToUpper(pokemonConfig.Name[0]) + pokemonConfig.Name.Substring(1);

            var nature = GenerationInfo.Natures.First(x => x.Id == PID % 25);
            var hiddenPowerCalculated = Math.Floor((double)(
                (ivHP % 2 +
                2 * (ivAttack % 2) +
                4 * (ivDefense % 2) +
                8 * (ivSpeed % 2) +
                16 * (ivSpAttack % 2) +
                32 * (ivSpDefense % 2)) * 15
            ) / 63);
            var hiddenPower = GenerationInfo.HiddenPowers.First(x => x.Id == hiddenPowerCalculated);
            var hiddenPowerDamage = (int)Math.Floor(
                (Math.Floor(ivHP / (double)2) % 2 + //0
                2 * (Math.Floor(ivAttack / (double)2) % 2) + //1
                4 * (Math.Floor(ivDefense / (double)2) % 2) + //0
                8 * (Math.Floor(ivSpeed / (double)2) % 2) + //1
                16 * (Math.Floor(ivSpAttack / (double)2) % 2) + //1
                32 * (Math.Floor(ivSpDefense / (double)2) % 2)) * 40 //0
             / 63) + 30; //2 + 8 + 16
            var shinyValue = OTID ^ OTSID ^ bytesPokemon.Take(2).ToUInt16() ^ bytesPokemon.Skip(2).Take(2).ToUInt16();
            var shiny = shinyValue < 8;

            #endregion

            #region Create Objects

            var originalTrainer = new PokemonOriginalTrainer(OTID, OTSID, OTName);
            var evs = new PokemonEVS(evHP, evAttack, evDefense, evSpeed, evSpAttack, evSpDefense);
            var condition = new PokemonCondition(coolness, beauty, cuteness, smartness, toughness, feel);
            var pokerus = new PokemonPokerus(pokerusDays, pokerusStrain);
            var originInfo = new PokemonOriginInfo(trainerGenderMale, pokeball, game, isHatched);
            var ivs = new PokemonIVS((int)ivHP, (int)ivAttack, (int)ivDefense, (int)ivSpeed, (int)ivSpAttack, (int)ivSpDefense);
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
                var statusCondition = (PokemonStatusType)bytesPokemon.Skip(80).Take(4).ToUInt32();
                var level = (int)bytesPokemon[84];
                var hasPokerus = bytesPokemon[85] == 0; //0 is cured
                var currentHP = bytesPokemon.Skip(86).Take(2).ToUInt16();
                var totalHP = bytesPokemon.Skip(88).Take(2).ToUInt16();
                var attack = bytesPokemon.Skip(90).Take(2).ToUInt16();
                var defense = bytesPokemon.Skip(92).Take(2).ToUInt16();
                var speed = bytesPokemon.Skip(94).Take(2).ToUInt16();
                var spAttack = bytesPokemon.Skip(96).Take(2).ToUInt16();
                var spDefense = bytesPokemon.Skip(98).Take(2).ToUInt16();

                return new PartyPokemon(
                    PID,
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
                PID,
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

        public virtual PlayerData ParsePlayer(byte[] bytesGPlayer, byte[] bytesObjects)
        {
            var runningState = (PlayerRunningState)bytesGPlayer[2];
            var transitionState = (TileTransitionState)bytesGPlayer[3];
            var gender = bytesGPlayer[7] == 0;
            var currentX = bytesObjects.Skip(0x10).Take(2).ToUInt16();
            var currentY = bytesObjects.Skip(0x12).Take(2).ToUInt16();
            var previousX = bytesObjects.Skip(0x14).Take(2).ToUInt16();
            var previousY = bytesObjects.Skip(0x16).Take(2).ToUInt16();
            var currentPosition = new Position(currentX, currentY);
            var prevPosition = new Position(previousX, previousY);
            var facingDirection = (PlayerFacingDirection)bytesObjects[0x18];

            return new PlayerData(currentPosition, prevPosition, runningState, transitionState, gender, facingDirection);
        }

        public virtual PlayerData GetPlayer()
        {
            var avatarSymbol = Symbols.First(x => x.Name == "gPlayerAvatar");
            var objectSymbol = Symbols.First(x => x.Name == "gObjectEvents");

            var bytesAvatar = SymbolUtil.Read(APIContainer, avatarSymbol).ToArray();
            var bytesObjects = SymbolUtil.Read(APIContainer, objectSymbol).ToArray();

            return ParsePlayer(bytesAvatar, bytesObjects);
        }

        public virtual GameState GetGameState()
        {
            var gMain = Symbols.First(x => x.Name == "gMain");
            var cb2Address = SymbolUtil.Read(APIContainer!, gMain.Address, 4, 4).ToUInt32();
            var reverseSymbol = Symbols.FirstOrDefault(x => x.Address == cb2Address - 1);
            if (reverseSymbol != null)
            {
                string cb2Name = reverseSymbol.Name.ToUpper();
                switch (cb2Name)
                {
                    case "CB2_INITCOPYRIGHTSCREENAFTERBOOTUP":
                    case "CB2_WAITFADEBEFORESETUPINTRO":
                    case "CB2_SETUPINTRO":
                    case "CB2_INTRO":
                    case "CB2_INITTITLESCREEN":
                    case "CB2_TITLESCREENRUN":
                    case "CB2_INITCOPYRIGHTSCREENAFTERTITLESCREEN":
                    case "CB2_INITMAINMENU":
                    case "MAINCB2":
                    case "MAINCB2_INTRO":
                        return GameState.TitleScreen;
                    case "CB2_MAINMENU":
                        return GameState.MainMenu;
                    case "CB2_STARTERCHOOSE":
                        return GameState.ChooseStarter;
                    case "CB2_BAGMENURUN":
                    case "SUB_80A3118":
                    case "CB2_BAGMENUFROMBATTLE":
                    case "CB2_BAGMENUFROMSTARTMENU":
                        return GameState.BagMenu;
                    case "CB2_INITBATTLE":
                    case "CB2_HANDLESTARTBATTLE":
                        return GameState.StartBattle;
                    case "BATTLEMAINCB2":
                        return GameState.Battle;
                    case "CB2_ENDWILDBATTLE":
                        return GameState.EndBattle;
                    case "CB2_UPDATEPARTYMENU":
                    case "CB2_PARTYMENUMAIN":
                        return GameState.PartyMenu;
                    case "CB2_OVERWORLD":
                        return GameState.Overworld;
                    case "CB2_LOADNAMINGSCREEN":
                    case "CB2_NAMINGSCREEN":
                        return GameState.NamingScreen;
                    case "CB2_LOADMAP":
                    case "CB2_LOADMAP2":
                    case "CB2_DOCHANGEMAP":
                    case "SUB_810CC80":
                        return GameState.ChangeMap;
                }
            }

            return GameState.Unknow;
        }

        public virtual int GetPartyCount()
        {
            var partyCountSYM = Symbols.First(x => x.Name == "gPlayerPartyCount");
            return SymbolUtil.Read(APIContainer, partyCountSYM, 0, 1)[0];
        }

        public virtual IReadOnlyList<Pokemon> GetParty()
        {
            var list = new List<Pokemon>();
            var partySYM = Symbols.First(x => x.Name == "gPlayerParty");

            for (int i = 0; i < GetPartyCount(); i++)
            {
                var offset = i * 100;
                var bytesPokemon = SymbolUtil.Read(APIContainer, partySYM, offset, 100);

                list.Add(ParsePokemon(bytesPokemon.ToArray()));
            }

            return list;
        }

        public virtual Pokemon GetOpponent()
        {
            var list = new List<Pokemon>();
            var partySYM = Symbols.First(x => x.Name == "gEnemyParty");

            var bytesPokemon = SymbolUtil.Read(APIContainer, partySYM, 0, 100);

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

        public ICollection<GTask> GetTasks()
        {
            var tasks = new List<GTask>();
            var gTasks = Symbols.First(x => x.Name == "gTasks");
            var bytesTask = SymbolUtil.Read(APIContainer, gTasks);
            for (int i = 0; i < 16; i++)
            {
                var offset = i * 40;
                var isActive = bytesTask[offset + 4] == 1;
                if (isActive)
                {
                    var bytesName = bytesTask.Skip(offset).Take(4);
                    var addr = bytesName.ToUInt32() - 1;
                    var taskSymbol = Symbols.FirstOrDefault(x => x.Address == addr);
                    string taskName;
                    if (taskSymbol == null)
                    {
                        taskName = addr.ToString();
                    }
                    else
                    {
                        taskName = taskSymbol.Name;
                    }

                    var prev = (int)bytesTask[offset + 5];
                    var next = (int)bytesTask[offset + 6];
                    var priority = (int)bytesTask[offset + 7];
                    var data = bytesTask.Skip(offset + 8).Take(32).ToArray();

                    tasks.Add(new GTask(
                        taskName,
                        isActive,
                        prev,
                        next,
                        priority,
                        data
                    ));
                }
            }

            return tasks;
        }

        public virtual int GetActionSelectionCursor()
        {
            var symbol = Symbols.First(x => x.Name == "gActionSelectionCursor");
            var bytes = SymbolUtil.Read(APIContainer, symbol);

            return bytes[0];
        }

        public virtual uint GetRandomRNG()
        {
            var symbol = Symbols.First(x => x.Name == "gRngValue");
            return SymbolUtil.Read(APIContainer, symbol, 0, 4).ToUInt32();
        }

        public virtual uint SetRandomRNG()
        {
            Random random = new Random();
            var bytes = new byte[4];
            random.NextBytes(bytes);
            uint randomNumber = bytes.ToUInt32();

            var symbol = Symbols.First(x => x.Name == "gRngValue");
            SymbolUtil.Write(APIContainer, symbol, bytes);

            return randomNumber;
        }

        public virtual uint SetRandomSeed()
        {
            Random random = new Random();
            var bytes = new byte[4];
            random.NextBytes(bytes);
            uint randomNumber = bytes.ToUInt32();

            SetSeed(randomNumber);

            return randomNumber;
        }

        public virtual void SetSeed(uint seed)
        {
            var bytes = BitConverter.GetBytes(seed);
            var symbol = Symbols.First(x => x.Name == "SeedRng");
            SymbolUtil.Write(APIContainer, symbol, bytes);
        }

        public virtual uint GetSeed()
        {
            var symbol = Symbols.First(x => x.Name == "SeedRng");
            return SymbolUtil.Read(APIContainer, symbol, 0, 4).ToUInt32();
        }

        //With Gen 3 you should follow the save block in memory using pointer
        //https://bulbapedia.bulbagarden.net/wiki/Save_data_structure_(Generation_III)#Game_save_A.2C_Game_save_B
        public int GetTID()
        {
            var symbol = Symbols.First(x => x.Name == "gSaveblock2");
            var symbolPtr = Symbols.First(x => x.Name == "gSaveBlock2Ptr");
            var ptr = SymbolUtil.Read(APIContainer, symbolPtr).ToUInt32();
            //var bytes = SymbolUtil.Read(APIContainer, ptr, 0, symbol.Size);
            return SymbolUtil.Read(APIContainer, ptr, 0x0A, 2).ToUInt16();
        }

        //With Gen 3 you should follow the save block in memory using pointer
        //https://bulbapedia.bulbagarden.net/wiki/Save_data_structure_(Generation_III)#Game_save_A.2C_Game_save_B
        public int GetSID()
        {
            var symbol = Symbols.First(x => x.Name == "gSaveblock2");
            var symbolPtr = Symbols.First(x => x.Name == "gSaveBlock2Ptr");
            var ptr = SymbolUtil.Read(APIContainer, symbolPtr).ToUInt32();
            //var bytes = SymbolUtil.Read(APIContainer, ptr, 0, symbol.Size);
            return SymbolUtil.Read(APIContainer, ptr, 0x0C, 2).ToUInt16();
        }
    }
}
