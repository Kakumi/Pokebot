using System.Collections.Generic;
using System.Linq;
using System.Text;
using BizHawk.Client.Common;
using Pokebot.Models.Config;
using Pokebot.Models.Player;
using Pokebot.Models.Pokemons;
using Pokebot.Symbols;
using Pokebot.Utils;

namespace Pokebot.Models.Memory
{
    public abstract class CommonGenMemory : IGameMemory
    {
        public ApiContainer APIContainer { get; }
        public VersionInfo VersionInfo { get; }
        public HashData HashData { get; }
        public GenerationInfo GenerationInfo { get; }

        public CommonGenMemory(ApiContainer apiContainer, VersionInfo versionInfo, HashData hashData, GenerationInfo generationInfo)
        {
            APIContainer = apiContainer;
            VersionInfo = versionInfo;
            HashData = hashData;
            GenerationInfo = generationInfo;
            Symbols = LoadSymbols(hashData);
        }

        public IReadOnlyList<Symbol> Symbols { get; }

        public abstract int GetActionSelectionCursor();
        public abstract void TrySetEscape();
        public abstract uint GetCurrentSeed();
        public abstract GameState GetGameState();
        public abstract Pokemon GetOpponent();
        public abstract IReadOnlyList<Pokemon> GetParty();
        public abstract int GetPartyCount();
        public abstract PlayerData GetPlayer();
        public abstract int GetSID();
        public abstract ICollection<GTask> GetTasks();
        public abstract int GetTID();
        public abstract uint RandomizeCurrentSeed();
        public abstract FishingState GetFishingResult();

        public IReadOnlyList<Symbol> GetSymbols()
        {
            return Symbols;
        }

        public Symbol GetSymbol(string name)
        {
            return Symbols.FirstOrDefault(x => x.Name == name);
        }

        protected IReadOnlyList<Symbol> LoadSymbols(HashData hashData)
        {
            var symbols = new List<Symbol>();

            Log.Debug($"Loading main symbol: {hashData.Symbols.Main}");
            //Load main symbol file
            var mainFileData = ResourceSymbols.ResourceManager.GetObject(hashData.Symbols.Main);
            if (mainFileData is byte[] mainBytes)
            {
                symbols = SymbolUtil.Load(mainBytes).ToList();

                //Load and replace extra symbols if they exists
                foreach (var file in hashData.Symbols.Patches)
                {
                    Log.Debug($"Loading patch symbol: {file}");
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
                            else if (tempSymbol.Letter == 'a') //add custom line
                            {
                                symbols.Add(tempSymbol);
                            }
                        }
                    }
                    else
                    {
                        Log.Error($"Failed to load patch: {file}");
                    }
                }
            }

            return symbols;
        }

        protected string GetBytesText(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var b in bytes)
            {
                sb.Append(GenerationInfo.Characters[b]);
            }

            return sb.ToString();
        }

        public virtual bool CanSetShiny()
        {
            return false;
        }

        public virtual Pokemon SetShiny(Pokemon pokemon)
        {
            throw new System.NotImplementedException();
        }
    }
}
