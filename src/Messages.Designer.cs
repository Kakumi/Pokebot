﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Pokebot {
    using System;
    
    
    /// <summary>
    ///   Une classe de ressource fortement typée destinée, entre autres, à la consultation des chaînes localisées.
    /// </summary>
    // Cette classe a été générée automatiquement par la classe StronglyTypedResourceBuilder
    // à l'aide d'un outil, tel que ResGen ou Visual Studio.
    // Pour ajouter ou supprimer un membre, modifiez votre fichier .ResX, puis réexécutez ResGen
    // avec l'option /str ou régénérez votre projet VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Retourne l'instance ResourceManager mise en cache utilisée par cette classe.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Pokebot.Messages", typeof(Messages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Remplace la propriété CurrentUICulture du thread actuel pour toutes
        ///   les recherches de ressources à l'aide de cette classe de ressource fortement typée.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à An error occurred while calling the Github API..
        /// </summary>
        internal static string API_GithubError {
            get {
                return ResourceManager.GetString("API_GithubError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Pokebot.
        /// </summary>
        internal static string AppName {
            get {
                return ResourceManager.GetString("AppName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à A save state already exists for this bot. Do you want to use it ?.
        /// </summary>
        internal static string Bot_FileExistReplaceMessage {
            get {
                return ResourceManager.GetString("Bot_FileExistReplaceMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à File already exists.
        /// </summary>
        internal static string Bot_FileExistReplaceTitle {
            get {
                return ResourceManager.GetString("Bot_FileExistReplaceTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Bot is not running.
        /// </summary>
        internal static string Bot_NotRunning {
            get {
                return ResourceManager.GetString("Bot_NotRunning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Bot is running....
        /// </summary>
        internal static string Bot_Running {
            get {
                return ResourceManager.GetString("Bot_Running", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Bot type is not supported.
        /// </summary>
        internal static string BotFactory_NotSupported {
            get {
                return ResourceManager.GetString("BotFactory_NotSupported", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à This frame has already passed. Save the game and reload it (without save state)..
        /// </summary>
        internal static string BotPokeFinder_InvalidFrame {
            get {
                return ResourceManager.GetString("BotPokeFinder_InvalidFrame", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Bot is unable to start because the save state doesn&apos;t exists anymore..
        /// </summary>
        internal static string BotPokeFinder_InvalidSaveState {
            get {
                return ResourceManager.GetString("BotPokeFinder_InvalidSaveState", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à All frames has been tested and the pokemon with PID {0} has not been found..
        /// </summary>
        internal static string BotPokeFinder_PokemonNotFound {
            get {
                return ResourceManager.GetString("BotPokeFinder_PokemonNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à TID : {0} - SID : {1}.
        /// </summary>
        internal static string BotPokeFinder_TrainerInfo {
            get {
                return ResourceManager.GetString("BotPokeFinder_TrainerInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Bot is unable to start because the save state doesn&apos;t exists anymore..
        /// </summary>
        internal static string BotStarter_InvalidSaveState {
            get {
                return ResourceManager.GetString("BotStarter_InvalidSaveState", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à This bot cannot be used because the party is not empty..
        /// </summary>
        internal static string BotStarter_PartyNotEmpty {
            get {
                return ResourceManager.GetString("BotStarter_PartyNotEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Bot is unable to start because the player is not in front of starters..
        /// </summary>
        internal static string BotStarter_WrongStartPosition {
            get {
                return ResourceManager.GetString("BotStarter_WrongStartPosition", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Ability.
        /// </summary>
        internal static string Discord_Ability {
            get {
                return ResourceManager.GetString("Discord_Ability", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à The bot stopped because a pokemon with your filters has been encountered ! :partying_face:.
        /// </summary>
        internal static string Discord_Content {
            get {
                return ResourceManager.GetString("Discord_Content", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Hey {0} ! {1}.
        /// </summary>
        internal static string Discord_ContentWithUser {
            get {
                return ResourceManager.GetString("Discord_ContentWithUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Item.
        /// </summary>
        internal static string Discord_Item {
            get {
                return ResourceManager.GetString("Discord_Item", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à IV Attack.
        /// </summary>
        internal static string Discord_IVAttack {
            get {
                return ResourceManager.GetString("Discord_IVAttack", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à IV Defense.
        /// </summary>
        internal static string Discord_IVDefense {
            get {
                return ResourceManager.GetString("Discord_IVDefense", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à IV Hp.
        /// </summary>
        internal static string Discord_IVHp {
            get {
                return ResourceManager.GetString("Discord_IVHp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à IV Sp. Attack.
        /// </summary>
        internal static string Discord_IVSpAttack {
            get {
                return ResourceManager.GetString("Discord_IVSpAttack", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à IV Sp. Defense.
        /// </summary>
        internal static string Discord_IVSpDefense {
            get {
                return ResourceManager.GetString("Discord_IVSpDefense", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à IV Speed.
        /// </summary>
        internal static string Discord_IVSpeed {
            get {
                return ResourceManager.GetString("Discord_IVSpeed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Nature.
        /// </summary>
        internal static string Discord_Nature {
            get {
                return ResourceManager.GetString("Discord_Nature", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à {0} ({1}).
        /// </summary>
        internal static string Discord_PokemonName {
            get {
                return ResourceManager.GetString("Discord_PokemonName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Shiny.
        /// </summary>
        internal static string Discord_Shiny {
            get {
                return ResourceManager.GetString("Discord_Shiny", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à No :x:.
        /// </summary>
        internal static string Discord_ShinyNo {
            get {
                return ResourceManager.GetString("Discord_ShinyNo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Yes :white_check_mark:.
        /// </summary>
        internal static string Discord_ShinyYes {
            get {
                return ResourceManager.GetString("Discord_ShinyYes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Trainer.
        /// </summary>
        internal static string Discord_Trainer {
            get {
                return ResourceManager.GetString("Discord_Trainer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Failed to send discord webhook: {0}.
        /// </summary>
        internal static string DiscordWebhook_Failed {
            get {
                return ResourceManager.GetString("DiscordWebhook_Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Error occurred while reading bot type..
        /// </summary>
        internal static string Error_ReadingBotType {
            get {
                return ResourceManager.GetString("Error_ReadingBotType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Any item.
        /// </summary>
        internal static string Filter_AnyItem {
            get {
                return ResourceManager.GetString("Filter_AnyItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Any nature.
        /// </summary>
        internal static string Filter_AnyNature {
            get {
                return ResourceManager.GetString("Filter_AnyNature", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Any pokemon.
        /// </summary>
        internal static string Filter_AnyPokemon {
            get {
                return ResourceManager.GetString("Filter_AnyPokemon", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Any type.
        /// </summary>
        internal static string Filter_AnyType {
            get {
                return ResourceManager.GetString("Filter_AnyType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Nothing.
        /// </summary>
        internal static string Item_Nothing {
            get {
                return ResourceManager.GetString("Item_Nothing", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Do you want to download the version {0} ?.
        /// </summary>
        internal static string NewVersion_Message {
            get {
                return ResourceManager.GetString("NewVersion_Message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à The version {0} is now available on Github !.
        /// </summary>
        internal static string NewVersion_MessageLabel {
            get {
                return ResourceManager.GetString("NewVersion_MessageLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à New version available.
        /// </summary>
        internal static string NewVersion_Title {
            get {
                return ResourceManager.GetString("NewVersion_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Pokemon with filters found..
        /// </summary>
        internal static string Pokemon_Found {
            get {
                return ResourceManager.GetString("Pokemon_Found", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Pokemon with filters found. Catch it manually..
        /// </summary>
        internal static string Pokemon_FoundCatch {
            get {
                return ResourceManager.GetString("Pokemon_FoundCatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Pokemon is not valid.
        /// </summary>
        internal static string Pokemon_NotValid {
            get {
                return ResourceManager.GetString("Pokemon_NotValid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à ROM {0} loaded.
        /// </summary>
        internal static string Rom_Loaded {
            get {
                return ResourceManager.GetString("Rom_Loaded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à No ROM loaded.
        /// </summary>
        internal static string Rom_NotLoaded {
            get {
                return ResourceManager.GetString("Rom_NotLoaded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à This ROM is not supported.
        /// </summary>
        internal static string Rom_NotSupported {
            get {
                return ResourceManager.GetString("Rom_NotSupported", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Pause.
        /// </summary>
        internal static string Settings_Pause {
            get {
                return ResourceManager.GetString("Settings_Pause", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Sound.
        /// </summary>
        internal static string Settings_Sound {
            get {
                return ResourceManager.GetString("Settings_Sound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Speed.
        /// </summary>
        internal static string Settings_Speed {
            get {
                return ResourceManager.GetString("Settings_Speed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Start this bot while you are on the map.
        /// </summary>
        internal static string SpinBot_StartOnlyMap {
            get {
                return ResourceManager.GetString("SpinBot_StartOnlyMap", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Can&apos;t spin the player, next move is unknown.
        /// </summary>
        internal static string SpinBot_UnknowNextMove {
            get {
                return ResourceManager.GetString("SpinBot_UnknowNextMove", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à No symbols found for this game..
        /// </summary>
        internal static string Symbols_Empty {
            get {
                return ResourceManager.GetString("Symbols_Empty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Bot.
        /// </summary>
        internal static string Tab_BotPanel {
            get {
                return ResourceManager.GetString("Tab_BotPanel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Bot Stats.
        /// </summary>
        internal static string Tab_EncounterStats {
            get {
                return ResourceManager.GetString("Tab_EncounterStats", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Logs.
        /// </summary>
        internal static string Tab_LogsPanel {
            get {
                return ResourceManager.GetString("Tab_LogsPanel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Settings.
        /// </summary>
        internal static string Tab_SettingsPanel {
            get {
                return ResourceManager.GetString("Tab_SettingsPanel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Opponent.
        /// </summary>
        internal static string Tab_ViewerOpponentName {
            get {
                return ResourceManager.GetString("Tab_ViewerOpponentName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Party.
        /// </summary>
        internal static string Tab_ViewerPartyName {
            get {
                return ResourceManager.GetString("Tab_ViewerPartyName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à It&apos;s better to have some delay between each actions to avoid the bot to freeze while doing input. 0.1 is recommanded..
        /// </summary>
        internal static string Tooltip_Delay {
            get {
                return ResourceManager.GetString("Tooltip_Delay", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Ability: {0}.
        /// </summary>
        internal static string Viewer_Ability {
            get {
                return ResourceManager.GetString("Viewer_Ability", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Attack: {0}.
        /// </summary>
        internal static string Viewer_Attack {
            get {
                return ResourceManager.GetString("Viewer_Attack", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Defense: {0}.
        /// </summary>
        internal static string Viewer_Defense {
            get {
                return ResourceManager.GetString("Viewer_Defense", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Hidden Power Damage: {0}.
        /// </summary>
        internal static string Viewer_HiddenPowerDamage {
            get {
                return ResourceManager.GetString("Viewer_HiddenPowerDamage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Hidden Power: {0}.
        /// </summary>
        internal static string Viewer_HiddenPowerType {
            get {
                return ResourceManager.GetString("Viewer_HiddenPowerType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à HP: {0}.
        /// </summary>
        internal static string Viewer_HP {
            get {
                return ResourceManager.GetString("Viewer_HP", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Item: {0}.
        /// </summary>
        internal static string Viewer_Item {
            get {
                return ResourceManager.GetString("Viewer_Item", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à {0} ({1}/{2}).
        /// </summary>
        internal static string Viewer_Move {
            get {
                return ResourceManager.GetString("Viewer_Move", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Nature: {0}.
        /// </summary>
        internal static string Viewer_Nature {
            get {
                return ResourceManager.GetString("Viewer_Nature", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à /.
        /// </summary>
        internal static string Viewer_NoMove {
            get {
                return ResourceManager.GetString("Viewer_NoMove", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Not shiny.
        /// </summary>
        internal static string Viewer_NotShiny {
            get {
                return ResourceManager.GetString("Viewer_NotShiny", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Shiny.
        /// </summary>
        internal static string Viewer_Shiny {
            get {
                return ResourceManager.GetString("Viewer_Shiny", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Sp. Attack: {0}.
        /// </summary>
        internal static string Viewer_SpAttack {
            get {
                return ResourceManager.GetString("Viewer_SpAttack", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Sp. Defense: {0}.
        /// </summary>
        internal static string Viewer_SpDefense {
            get {
                return ResourceManager.GetString("Viewer_SpDefense", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Speed: {0}.
        /// </summary>
        internal static string Viewer_Speed {
            get {
                return ResourceManager.GetString("Viewer_Speed", resourceCulture);
            }
        }
    }
}
