using BizHawk.Client.Common;
using Pokebot.Models.Config;
using Pokebot.Models.Player;
using Pokebot.Models.Pokemons;
using Pokebot.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pokebot.Models.Memory
{
    internal class Gen2Memory : CommonGenMemory
    {
        private const int ConfirmedOpponentReadsRequired = 2;

        private string? _pendingOpponentSignature;
        private int _pendingOpponentReads;

        public Gen2Memory(ApiContainer apiContainer, VersionInfo versionInfo, HashData hashData, GenerationInfo generationInfo) :
            base(apiContainer, versionInfo, hashData, generationInfo)
        { }

        public override void TrySetEscape()
        {
            var wMenuCursorY = GetSymbol("wMenuCursorY");
            var wMenuCursorX = GetSymbol("wMenuCursorX");
            SymbolUtil.Write(APIContainer, wMenuCursorY, new byte[] { 0x02 });
            SymbolUtil.Write(APIContainer, wMenuCursorX, new byte[] { 0x02 });
        }

        public override int GetActionSelectionCursor()
        {
            // Read cursor positions
            var wMenuCursorY = GetSymbol("wMenuCursorY");
            var wMenuCursorX = GetSymbol("wMenuCursorX");
            var wBattleCursorY = SymbolUtil.Read(APIContainer, wMenuCursorY)[0];
            var wBattleCursorX = SymbolUtil.Read(APIContainer, wMenuCursorX)[0];

            // Use a 2D array to map cursor positions to actions
            // Indexing: [Y - 1][X - 1]
            int[,] cursorMap = {
                { (int)BattleActionSelectionCursor.Moves, (int)BattleActionSelectionCursor.Team },
                { (int)BattleActionSelectionCursor.Bag,   (int)BattleActionSelectionCursor.Escape }
            };

            if (wBattleCursorX >= 1 && wBattleCursorX <= 2 && wBattleCursorY >= 1 && wBattleCursorY <= 2)
            {
                return cursorMap[wBattleCursorY - 1, wBattleCursorX - 1];
            }

            return (int)BattleActionSelectionCursor.Escape;
        }

        public override GameState GetGameState()
        {
            var wBattleMode = GetSymbol("wBattleMode");
            var battleType = SymbolUtil.Read(APIContainer, wBattleMode)[0];
            if (battleType != 0)
            {
                if (!TryGetStableOpponent(out var opponent, out var signature))
                {
                    return GameState.StartBattle;
                }

                if (_pendingOpponentSignature != signature)
                {
                    _pendingOpponentSignature = signature;
                    _pendingOpponentReads = 1;
                    return GameState.StartBattle;
                }

                _pendingOpponentReads++;
                if (_pendingOpponentReads < ConfirmedOpponentReadsRequired)
                {
                    return GameState.StartBattle;
                }

                return GameState.Battle;
            }

            ResetBattleState();

            var wMenuSelection = GetSymbol("wMenuSelection");
            var menuCursorId = SymbolUtil.Read(APIContainer, wMenuSelection)[0];
            if (menuCursorId != 0)
            {
                return GameState.BagMenu;
            }

            var wNamingScreenType = GetSymbol("wNamingScreenType");
            var wNamingScreenMaxNameLength = GetSymbol("wNamingScreenMaxNameLength");
            var namingScreenId = SymbolUtil.Read(APIContainer, wNamingScreenType)[0];
            var namingScreenLength = SymbolUtil.Read(APIContainer, wNamingScreenMaxNameLength)[0];
            if (namingScreenId == 0 && namingScreenLength <= 11)
            {
                return GameState.NamingScreen;
            }

            var player = GetPlayer();
            if (player.Position.X != 0 && player.Position.Y != 0 && player.MapGroup != 0 && player.MapNumber != 0)
            {
                return GameState.Overworld;
            }

            return GameState.TitleScreen;
        }

        public override Pokemon GetOpponent()
        {
            if (!TryGetStableOpponent(out var opponent, out _))
            {
                throw new InvalidOperationException("Opponent data is not ready in memory.");
            }

            return opponent;
        }

        public override IReadOnlyList<Pokemon> GetParty()
        {
            var list = new List<Pokemon>();
            for (int i = 0; i < GetPartyCount(); i++)
            {
                var pokemon = GetPartyPokemon(i);
                if (pokemon == null)
                {
                    break;
                }

                list.Add(pokemon);
            }

            return list;
        }

        private Pokemon? GetPartyPokemon(int index)
        {
            var wPartyMon1 = GetSymbol("wPartyMon1");
            var wPartyMon1Nickname = GetSymbol("wPartyMon1Nickname");
            var wPartyMonOTs = GetSymbol("wPartyMonOTs");
            var size = wPartyMon1.Size; // Size of a single Pokemon structure in bytes
            var offset = index * size;
            var bytesPokemon = SymbolUtil.Read(APIContainer, wPartyMon1.Address, offset, size);
            var bytesNickname = SymbolUtil.Read(APIContainer, wPartyMon1Nickname.Address, index * wPartyMon1Nickname.Size, wPartyMon1Nickname.Size).TakeWhile(x => x != 0x50);
            var bytesOT = SymbolUtil.Read(APIContainer, wPartyMonOTs.Address, index * wPartyMonOTs.Size, wPartyMonOTs.Size).TakeWhile(x => x != 0x50);
            return ParsePokemon(bytesPokemon.ToArray(), bytesNickname.ToArray(), bytesOT.ToArray(), new MemoryLocation(wPartyMon1.Address, offset, size));
        }

        private PokemonGender GetGender(byte genderRatio, int atkDV)
        {
            if (genderRatio == 0xFF) return PokemonGender.Genderless;
            if (genderRatio == 0xFE) return PokemonGender.Female;
            if (genderRatio == 0x00) return PokemonGender.Male;

            int femaleMaxAtkDV = genderRatio >> 4;

            return (atkDV <= femaleMaxAtkDV) ? PokemonGender.Female : PokemonGender.Male;
        }

        protected Pokemon ParseOpponentPokemon(byte[] bytesPokemon, byte[] bytesNickname, MemoryLocation memoryLocation)
        {
            var speciesId = bytesPokemon[0x00];

            var wSpeciesInfo = GetSymbol("wSpeciesInfo");
            var speciesInfo = SymbolUtil.Read(APIContainer, wSpeciesInfo, (speciesId - 1) * wSpeciesInfo.Size, wSpeciesInfo.Size, wSpeciesInfo.Domain);

            var species = GenerationInfo.Pokemons.FirstOrDefault(x => x.Id == speciesId);
            if (species == null)
            {
                throw new Exception("Species not found for id " + speciesId);
            }

            var heldItem = GenerationInfo.Items.FirstOrDefault(x => x.Id == bytesPokemon[0x01]);
            var t = SymbolUtil.Read(APIContainer, 0xd0f1, 0, 1)[0];
            var move1 = GenerationInfo.Moves.FirstOrDefault(x => x.Id == bytesPokemon[0x02]);
            var move2 = GenerationInfo.Moves.FirstOrDefault(x => x.Id == bytesPokemon[0x03]);
            var move3 = GenerationInfo.Moves.FirstOrDefault(x => x.Id == bytesPokemon[0x04]);
            var move4 = GenerationInfo.Moves.FirstOrDefault(x => x.Id == bytesPokemon[0x05]);
            var evs = new PokemonEVS(
                0,
                0,
                0,
                0,
                0,
                0
            );
            var atkDefDVs = bytesPokemon[0x06];
            var spdSpcDVs = bytesPokemon[0x07];
            var attackIV = atkDefDVs >> 4;
            var defenseIV = atkDefDVs & 0x0F;
            var speedIV = spdSpcDVs >> 4;
            var specialIV = spdSpcDVs & 0x0F;
            var hpIV = ((attackIV % 2) * 8) + ((defenseIV % 2) * 4) + ((speedIV % 2) * 2) + (specialIV % 2);
            var ivs = new PokemonIVS(
                hpIV,
                attackIV,
                defenseIV,
                speedIV,
                specialIV,
                specialIV
            );
            var movePP1 = bytesPokemon[0x08];
            var movePP2 = bytesPokemon[0x09];
            var movePP3 = bytesPokemon[0x0A];
            var movePP4 = bytesPokemon[0x0B];
            var friendship = bytesPokemon[0x0C];
            var level = bytesPokemon[0x0D];
            var status = bytesPokemon.Skip(0x0E).Take(2).ToBE16();
            PokemonStatusType statusCondition = PokemonStatusType.None;
            if (status > 0 && status <= 2)
            {
                statusCondition = PokemonStatusType.Sleep;
            }
            else if (status == 3)
            {
                statusCondition = PokemonStatusType.Poison;
            }
            else if (status == 4)
            {
                statusCondition = PokemonStatusType.Burn;
            }
            else if (status == 5)
            {
                statusCondition = PokemonStatusType.Freeze;
            }
            else if (status == 6)
            {
                statusCondition = PokemonStatusType.Paralysis;
            }
            var currentHp = bytesPokemon.Skip(0x10).Take(2).ToBE16();
            var maxHp = bytesPokemon.Skip(0x12).Take(2).ToBE16();
            var attack = bytesPokemon.Skip(0x14).Take(2).ToBE16();
            var defense = bytesPokemon.Skip(0x16).Take(2).ToBE16();
            var speed = bytesPokemon.Skip(0x18).Take(2).ToBE16();
            var specialAttack = bytesPokemon.Skip(0x1A).Take(2).ToBE16();
            var specialDefense = bytesPokemon.Skip(0x1C).Take(2).ToBE16();
            var nickname = GetBytesText(bytesNickname);
            var isShiny = defenseIV == 0xA && speedIV == 0xA && specialIV == 0xA &&
               (attackIV == 0x2 || attackIV == 0x3 || attackIV == 0x6 || attackIV == 0x7 ||
                attackIV == 0xA || attackIV == 0xB || attackIV == 0xE || attackIV == 0xF);
            var types = GenerationInfo.Types.Where(t => species.Types.Any(y => t.Name.ToLower() == y.ToLower())).ToList();
            var genderRatio = speciesInfo[0x0D];
            PokemonGender gender = GetGender(genderRatio, attackIV);

            var moves = new List<PokemonAttack>
            {
                new PokemonAttack(move1, movePP1),
                new PokemonAttack(move2, movePP2),
                new PokemonAttack(move3, movePP3),
                new PokemonAttack(move4, movePP4)
            };

            return new PartyPokemon(
                speciesId,
                null,
                nickname,
                PokemonLanguageCode.Unknow,
                PokemonEggNameType.None,
                PokemonMark.None,
                0,
                0,
                speciesId,
                heldItem,
                (uint)0,
                0,
                0,
                moves.Where(x => x != null && x.MoveInfo != null).ToList(),
                evs,
                null,
                null,
                null,
                level,
                null,
                ivs,
                false,
                null,
                species.Name,
                species.DexId,
                isShiny,
                null,
                types,
                null,
                null,
                0,
                gender,
                statusCondition,
                level,
                false,
                currentHp,
                maxHp,
                attack,
                defense,
                speed,
                specialAttack,
                specialDefense,
                memoryLocation
            );
        }

        private bool TryGetStableOpponent(out Pokemon opponent, out string signature)
        {
            opponent = null!;
            signature = string.Empty;

            var wEnemyMon = GetSymbol("wEnemyMon");
            var wEnemyMonNickname = GetSymbol("wEnemyMonNickname");

            var firstPokemon = SymbolUtil.Read(APIContainer, wEnemyMon).ToArray();
            var firstNickname = SymbolUtil.Read(APIContainer, wEnemyMonNickname).ToArray();
            var secondPokemon = SymbolUtil.Read(APIContainer, wEnemyMon).ToArray();
            var secondNickname = SymbolUtil.Read(APIContainer, wEnemyMonNickname).ToArray();

            if (!firstPokemon.SequenceEqual(secondPokemon) || !firstNickname.SequenceEqual(secondNickname))
            {
                return false;
            }

            if (!IsOpponentDataValid(firstPokemon, firstNickname))
            {
                return false;
            }

            var nickname = firstNickname.TakeWhile(x => x != 0x50).ToArray();
            signature = BuildOpponentSignature(firstPokemon, nickname);
            opponent = ParseOpponentPokemon(firstPokemon, nickname, new MemoryLocation(wEnemyMon.Address, 0, wEnemyMon.Size));
            return true;
        }

        private bool IsOpponentDataValid(byte[] bytesPokemon, byte[] bytesNickname)
        {
            if (bytesPokemon.Length < 0x1E || bytesNickname.Length == 0)
            {
                return false;
            }

            var speciesId = bytesPokemon[0x00];
            var species = GenerationInfo.Pokemons.FirstOrDefault(x => x.Id == speciesId);
            if (species == null)
            {
                return false;
            }

            var level = bytesPokemon[0x0D];
            if (level == 0 || level > 100)
            {
                return false;
            }

            var currentHp = bytesPokemon.Skip(0x10).Take(2).ToBE16();
            var maxHp = bytesPokemon.Skip(0x12).Take(2).ToBE16();
            if (maxHp <= 0 || currentHp < 0 || currentHp > maxHp)
            {
                return false;
            }

            var attack = bytesPokemon.Skip(0x14).Take(2).ToBE16();
            var defense = bytesPokemon.Skip(0x16).Take(2).ToBE16();
            var speed = bytesPokemon.Skip(0x18).Take(2).ToBE16();
            var specialAttack = bytesPokemon.Skip(0x1A).Take(2).ToBE16();
            var specialDefense = bytesPokemon.Skip(0x1C).Take(2).ToBE16();
            if (attack <= 0 || defense <= 0 || speed <= 0 || specialAttack <= 0 || specialDefense <= 0)
            {
                return false;
            }

            var hasKnownMove = false;
            for (int i = 0; i < 4; i++)
            {
                var moveId = bytesPokemon[0x02 + i];
                if (moveId == 0)
                {
                    continue;
                }

                if (GenerationInfo.Moves.Any(x => x.Id == moveId))
                {
                    hasKnownMove = true;
                    break;
                }
            }

            if (!hasKnownMove)
            {
                return false;
            }

            var nicknameLength = bytesNickname.TakeWhile(x => x != 0x50).Count();
            return nicknameLength > 0;
        }

        private string BuildOpponentSignature(byte[] bytesPokemon, byte[] nickname)
        {
            var speciesId = bytesPokemon[0x00];
            var level = bytesPokemon[0x0D];
            var currentHp = bytesPokemon.Skip(0x10).Take(2).ToBE16();
            var maxHp = bytesPokemon.Skip(0x12).Take(2).ToBE16();
            return $"{speciesId:X2}-{level:D3}-{currentHp:D5}-{maxHp:D5}-{GetBytesText(nickname)}";
        }

        private void ResetBattleState()
        {
            _pendingOpponentSignature = null;
            _pendingOpponentReads = 0;
        }

        protected Pokemon? ParsePokemon(byte[] bytesPokemon, byte[] bytesNickname, byte[] bytesOT, MemoryLocation memoryLocation)
        {
            var speciesId = bytesPokemon[0x00];
            if (speciesId == 0)
            {
                return null;
            }

            var wSpeciesInfo = GetSymbol("wSpeciesInfo");
            var speciesInfo = SymbolUtil.Read(APIContainer, wSpeciesInfo, (speciesId - 1) * wSpeciesInfo.Size, wSpeciesInfo.Size, wSpeciesInfo.Domain);

            var species = GenerationInfo.Pokemons.FirstOrDefault(x => x.Id == speciesId);
            if (species == null)
            {
                throw new Exception("Species not found for id " + speciesId);
            }

            var heldItem = GenerationInfo.Items.FirstOrDefault(x => x.Id == bytesPokemon[0x01]);
            var move1 = GenerationInfo.Moves.FirstOrDefault(x => x.Id == bytesPokemon[0x02]);
            var move2 = GenerationInfo.Moves.FirstOrDefault(x => x.Id == bytesPokemon[0x03]);
            var move3 = GenerationInfo.Moves.FirstOrDefault(x => x.Id == bytesPokemon[0x04]);
            var move4 = GenerationInfo.Moves.FirstOrDefault(x => x.Id == bytesPokemon[0x05]);
            var otid = bytesPokemon.Skip(0x06).Take(2).ToBE16();
            var experience = bytesPokemon.Skip(0x08).Take(3).toBE24();
            var hpEV = bytesPokemon.Skip(0x0B).Take(2).ToBE16();
            var attackEV = bytesPokemon.Skip(0x0D).Take(2).ToBE16();
            var defenseEV = bytesPokemon.Skip(0x0F).Take(2).ToBE16();
            var speedEV = bytesPokemon.Skip(0x11).Take(2).ToBE16();
            var specialEV = bytesPokemon.Skip(0x13).Take(2).ToBE16();
            var evs = new PokemonEVS(
                hpEV,
                attackEV,
                defenseEV,
                speedEV,
                specialEV,
                specialEV
            );
            var atkDefDVs = bytesPokemon[0x15];
            var spdSpcDVs = bytesPokemon[0x16];
            var attackIV = atkDefDVs >> 4;
            var defenseIV = atkDefDVs & 0x0F;
            var speedIV = spdSpcDVs >> 4;
            var specialIV = spdSpcDVs & 0x0F;
            var hpIV = ((attackIV % 2) * 8) + ((defenseIV % 2) * 4) + ((speedIV % 2) * 2) + (specialIV % 2);
            var ivs = new PokemonIVS(
                hpIV,
                attackIV,
                defenseIV,
                speedIV,
                specialIV,
                specialIV
            );
            var movePP1 = bytesPokemon[0x17];
            var movePP2 = bytesPokemon[0x18];
            var movePP3 = bytesPokemon[0x19];
            var movePP4 = bytesPokemon[0x1A];
            var friendship = bytesPokemon[0x1B];
            var pokerus = bytesPokemon[0x1C];
            var pokerusStatus = new PokemonPokerus(pokerus & 0xF, (pokerus >> 4) & 0xF);
            var caughtData = bytesPokemon.Skip(0x1D).Take(2).ToBE16();
            var level = bytesPokemon[0x1F];
            var status = bytesPokemon[0x20];
            PokemonStatusType statusCondition = PokemonStatusType.None;
            if (status > 0 && status <= 2)
            {
                statusCondition = PokemonStatusType.Sleep;
            }
            else if (status == 3)
            {
                statusCondition = PokemonStatusType.Poison;
            }
            else if (status == 4)
            {
                statusCondition = PokemonStatusType.Burn;
            }
            else if (status == 5)
            {
                statusCondition = PokemonStatusType.Freeze;
            }
            else if (status == 6)
            {
                statusCondition = PokemonStatusType.Paralysis;
            }
            var currentHp = bytesPokemon.Skip(0x22).Take(2).ToBE16();
            var maxHp = bytesPokemon.Skip(0x24).Take(2).ToBE16();
            var attack = bytesPokemon.Skip(0x26).Take(2).ToBE16();
            var defense = bytesPokemon.Skip(0x28).Take(2).ToBE16();
            var speed = bytesPokemon.Skip(0x2A).Take(2).ToBE16();
            var specialAttack = bytesPokemon.Skip(0x2C).Take(2).ToBE16();
            var specialDefense = bytesPokemon.Skip(0x2E).Take(2).ToBE16();
            var nickname = GetBytesText(bytesNickname);
            var nature = GenerationInfo.Natures[experience % GenerationInfo.Natures.Count];
            var isShiny = defenseIV == 0xA && speedIV == 0xA && specialIV == 0xA &&
               (attackIV == 0x2 || attackIV == 0x3 || attackIV == 0x6 || attackIV == 0x7 ||
                attackIV == 0xA || attackIV == 0xB || attackIV == 0xE || attackIV == 0xF);
            //var metLevel = (caughtData >> 8) & 0x3F;
            //var metLocation = GenerationInfo.Locations[(caughtData & 0x7F)];
            var types = GenerationInfo.Types.Where(t => species.Types.Any(y => t.Name.ToLower() == y.ToLower())).ToList();
            var genderRatio = speciesInfo[0x0D];
            PokemonGender gender = GetGender(genderRatio, attackIV);

            var moves = new List<PokemonAttack>
            {
                new PokemonAttack(move1, movePP1),
                new PokemonAttack(move2, movePP2),
                new PokemonAttack(move3, movePP3),
                new PokemonAttack(move4, movePP4)
            };

            return new PartyPokemon(
                speciesId,
                new PokemonOriginalTrainer(otid, 0, GetBytesText(bytesOT)),
                nickname,
                PokemonLanguageCode.Unknow,
                PokemonEggNameType.None,
                PokemonMark.None,
                0,
                0,
                speciesId,
                heldItem,
                (uint)experience,
                0,
                friendship,
                moves.Where(x => x != null && x.MoveInfo != null).ToList(),
                evs,
                null,
                pokerusStatus,
                null,
                level,
                null,
                ivs,
                false,
                null,
                species.Name,
                species.DexId,
                isShiny,
                null,
                types,
                null,
                null,
                0,
                gender,
                statusCondition,
                level,
                pokerusStatus.Days > 0,
                currentHp,
                maxHp,
                attack,
                defense,
                speed,
                specialAttack,
                specialDefense,
                memoryLocation
            );
        }

        public override int GetPartyCount()
        {
            var wPartyCount = GetSymbol("wPartyCount");
            return SymbolUtil.Read(APIContainer, wPartyCount)[0];
        }

        public override PlayerData GetPlayer()
        {
            var wPlayerMapX = GetSymbol("wPlayerMapX");
            var wPlayerMapY = GetSymbol("wPlayerMapY");
            var wPlayerLastMapX = GetSymbol("wPlayerLastMapX");
            var wPlayerLastMapY = GetSymbol("wPlayerLastMapY");
            var wWalkingDirection = GetSymbol("wWalkingDirection");
            var wPlayerDirection = GetSymbol("wPlayerDirection");
            var wPlayerID = GetSymbol("wPlayerID");
            var wPlayerName = GetSymbol("wPlayerName");
            var wMapGroup = GetSymbol("wMapGroup");
            var wMapNumber = GetSymbol("wMapNumber");

            var playerMapX = SymbolUtil.Read(APIContainer, wPlayerMapX)[0];
            var playerMapY = SymbolUtil.Read(APIContainer, wPlayerMapY)[0];
            var playerLastMapX = SymbolUtil.Read(APIContainer, wPlayerLastMapX)[0];
            var playerLastMapY = SymbolUtil.Read(APIContainer, wPlayerLastMapY)[0];
            var direction = SymbolUtil.Read(APIContainer, wWalkingDirection)[0];
            var playerDirection = SymbolUtil.Read(APIContainer, wPlayerDirection)[0];
            var playerId = SymbolUtil.Read(APIContainer, wPlayerID).ToBE16();
            var playerName = GetBytesText(SymbolUtil.Read(APIContainer, wPlayerName).TakeWhile(x => x != 0x50).ToArray());
            var moving = direction != 0xFF;
            PlayerFacingDirection playerFacingDirection = PlayerFacingDirection.Down;
            switch (playerDirection)
            {
                case 0:
                    playerFacingDirection = PlayerFacingDirection.Down;
                    break;
                case 4:
                    playerFacingDirection = PlayerFacingDirection.Up;
                    break;
                case 8:
                    playerFacingDirection = PlayerFacingDirection.Left;
                    break;
                case 12:
                    playerFacingDirection = PlayerFacingDirection.Right;
                    break;
            }

            var position = new Position(playerMapX, playerMapY);
            var lastPosition = new Position(playerLastMapX, playerLastMapY);

            return new PlayerData(
                position,
                lastPosition,
                moving ? PlayerRunningState.Moving : PlayerRunningState.NotMoving,
                moving ? TileTransitionState.KeepMoving : TileTransitionState.NotMoving,
                true,
                playerFacingDirection,
                SymbolUtil.Read(APIContainer, wMapGroup)[0],
                SymbolUtil.Read(APIContainer, wMapNumber)[0],
                playerId,
                playerName
            );
        }

        public override int GetSID()
        {
            return 0;
        }

        public override ICollection<GTask> GetTasks()
        {
            return new List<GTask>();
        }

        public override int GetTID()
        {
            var wPlayerID = GetSymbol("wPlayerID");
            return SymbolUtil.Read(APIContainer, wPlayerID).ToBE16();
        }

        public override uint GetCurrentSeed()
        {
            var hRandomAdd = GetSymbol("hRandomAdd");
            var hRandomSub = GetSymbol("hRandomSub");

            var addSeed = SymbolUtil.Read(APIContainer, hRandomAdd)[0];
            var subSeed = SymbolUtil.Read(APIContainer, hRandomSub)[0];

            return ((uint)subSeed << 8) | addSeed;
        }

        public override uint RandomizeCurrentSeed()
        {
            var rng = new Random();
            uint newSeed = (uint)rng.Next(0, 0x10000); // 0x0000 <=> 0xFFFF
            var hRandomAdd = GetSymbol("hRandomAdd");
            var hRandomSub = GetSymbol("hRandomSub");

            // Découper en deux octets
            byte addSeed = (byte)(newSeed & 0xFF);       // low byte
            byte subSeed = (byte)((newSeed >> 8) & 0xFF); // high byte
            SymbolUtil.Write(APIContainer, hRandomAdd, new byte[] { addSeed });
            SymbolUtil.Write(APIContainer, hRandomSub, new byte[] { subSeed });

            return ((uint)subSeed << 8) | addSeed;
        }

        public override FishingState GetFishingResult()
        {
            return FishingState.None;
        }

        public override bool CanSetShiny()
        {
            return true;
        }

        public override Pokemon SetShiny(Pokemon pokemon)
        {
            int attackIV = 0xF;
            int defenseIV = 0xA;
            int speedIV = 0xA;
            int specialIV = 0xA;

            byte atkDef = (byte)((attackIV << 4) | (defenseIV & 0x0F));
            byte spdSpc = (byte)((speedIV << 4) | (specialIV & 0x0F));

            var wEnemyMon = GetSymbol("wEnemyMon");
            var opponentPokemon = pokemon.MemoryLocation.Address == wEnemyMon.Address;

            long dvAddr = pokemon.MemoryLocation.Address + (opponentPokemon ? 0x06 : 0x15);
            SymbolUtil.Write(APIContainer, dvAddr, new byte[] { atkDef, spdSpc });

            // Verification
            if (opponentPokemon)
            {
                return GetOpponent();
            }

            return GetPartyPokemon(pokemon.MemoryLocation.Offset / pokemon.MemoryLocation.Size) ?? pokemon;
        }
    }
}
