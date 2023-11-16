namespace Pokebot.Models.Pokemons
{
    public class PokemonRibbon
    {
        public PokemonRibbonRarity CoolRibbon { get; }
        public PokemonRibbonRarity BeautyRibbon { get; }
        public PokemonRibbonRarity CuteRibbon { get; }
        public PokemonRibbonRarity SmartRibbon { get; }
        public PokemonRibbonRarity ToughRibbon { get; }
        public bool HasChampionRibbon { get; }
        public bool HasWinningRibbon { get; }
        public bool HasVictoryRibbon { get; }
        public bool HasArtistRibbon { get; }
        public bool HasEffortRibbon { get; }
        public bool HasBattleChampionRibbon { get; }
        public bool HasRegionalChampionRibbon { get; }
        public bool HasNationalChampionRibbon { get; }
        public bool HasCountryRibbon { get; }
        public bool HasNationalRibbon { get; }
        public bool HasEarthRibbon { get; }
        public bool HasWorldRibbon { get; }

        public PokemonRibbon(PokemonRibbonRarity coolRibbon, PokemonRibbonRarity beautyRibbon, PokemonRibbonRarity cuteRibbon, PokemonRibbonRarity smartRibbon, PokemonRibbonRarity toughRibbon, bool hasChampionRibbon, bool hasWinningRibbon, bool hasVictoryRibbon, bool hasArtistRibbon, bool hasEffortRibbon, bool hasBattleChampionRibbon, bool hasRegionalChampionRibbon, bool hasNationalChampionRibbon, bool hasCountryRibbon, bool hasNationalRibbon, bool hasEarthRibbon, bool hasWorldRibbon)
        {
            CoolRibbon = coolRibbon;
            BeautyRibbon = beautyRibbon;
            CuteRibbon = cuteRibbon;
            SmartRibbon = smartRibbon;
            ToughRibbon = toughRibbon;
            HasChampionRibbon = hasChampionRibbon;
            HasWinningRibbon = hasWinningRibbon;
            HasVictoryRibbon = hasVictoryRibbon;
            HasArtistRibbon = hasArtistRibbon;
            HasEffortRibbon = hasEffortRibbon;
            HasBattleChampionRibbon = hasBattleChampionRibbon;
            HasRegionalChampionRibbon = hasRegionalChampionRibbon;
            HasNationalChampionRibbon = hasNationalChampionRibbon;
            HasCountryRibbon = hasCountryRibbon;
            HasNationalRibbon = hasNationalRibbon;
            HasEarthRibbon = hasEarthRibbon;
            HasWorldRibbon = hasWorldRibbon;
        }
    }
}
