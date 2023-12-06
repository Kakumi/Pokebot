# Pokebot
Pokebot is a tool for [BizHawk](https://github.com/TASEmulators/BizHawk) that automates certain actions to find the perfect legitimate pokemons for supported pokemon games.

Want to know how this tool has been developed and how it works ? [Visit my website](https://damienbrebion.com/#blogs)

[![(latest) release | GitHub](https://img.shields.io/github/release/Kakumi/Pokebot.svg?logo=github&logoColor=333333&sort=semver&style=popout)](https://github.com/Kakumi/Pokebot/releases/latest)
[![GitHub open issues counter](https://img.shields.io/github/issues-raw/Kakumi/Pokebot.svg?logo=github&logoColor=333333&style=popout)](https://github.com/Kakumi/Pokebot/issues)

<a href="https://discord.gg/wvQKYmuMnK"><img src="https://discordapp.com/api/guilds/1178966789477126176/widget.png?style=banner2" alt="Discord server"></a>

---

Jump to:
* [Dependencies](#dependencies)
* [Installation](#install)
* [Supported Games](#supported-games)
* [Overview](#overview)
  * [Video](#overview-video)
  * [Features](#overview-features)
  * [Bots](#overview-bots)
* [How to export save file](#export-save)
* [Code Documentation](#code-documentation)
* [Todo & Ideas](#todo)
* [Known bugs](#known-bugs)
* [Sources](#sources)
* [License](#license)

# <a name="dependencies"></a>Dependencies
* Download [BizHawk](https://github.com/TASEmulators/BizHawk) emulator
* Download [this tool](https://github.com/Kakumi/Pokebot/releases/latest)

# <a name="install"></a>Installation
Place the file `Pokebot.dll` inside `BizHawk/ExternalTools/` folder. Then, open BizHawk and click on "Tools" -> "External Tool" -> "Pokebot". A window should open. Just load your pokemon officiel ROM in BizHawk and Pokebot will automatically update the window if the game is [supported](#supported-games).

# <a name="supported-games"></a>Supported Games
<details>
  <summary>Pokemon Emerald</summary>

 | Nom          | Hash                                     | Supported | Tested |
 |--------------|------------------------------------------|-----------|--------|
 | USA (Europe) | f3ae088181bf583e55daf962a92bb46f4f1d07b7 |     ✅     |    ✅   |
 | French       | ca666651374d89ca439007bed54d839eb7bd14d0 |     ✅     |    ✅   |
 | Deutch       | 61c2eb2b380b1a75f0c94b767a2d4c26cd7ce4e3 |     ✅     |    ❌   |
 | Italian      | 1692db322400c3141c5de2db38469913ceb1f4d4 |     ✅     |    ❌   |
 | Japanese     | d7cf8f156ba9c455d164e1ea780a6bf1945465c2 |     ✅     |    ❌   |
 | Spanish      | fe1558a3dcb0360ab558969e09b690888b846dd9 |     ✅     |    ❌   |
</details>

<details>
  <summary>Pokemon Ruby</summary>

 | Nom                | Hash                                     | Supported | Tested |
 |--------------------|------------------------------------------|-----------|--------|
 | USA (Europe)       | f28b6ffc97847e94a6c21a63cacf633ee5c8df1e |     ✅     |    ✅   |
 | USA (Europe) rev 1 | 610b96a9c9a7d03d2bafb655e7560ccff1a6d894 |     ✅     |    ❌   |
 | USA (Europe) rev 2 | 5b64eacf892920518db4ec664e62a086dd5f5bc8 |     ✅     |    ❌   |
 | French             | a6ee94202bec0641c55d242757e84dc89336d4cb |     ✅     |    ✅   |
 | French rev 1       | ba888dfba231a231cbd60fe228e894b54fb1ed79 |     ✅     |    ❌   |
 | Deutch             | 1c2a53332382e14dab8815e3a6dd81ad89534050 |     ✅     |    ❌   |
 | Deutch rev 1       | 424740be1fc67a5ddb954794443646e6aeee2c1b |     ✅     |    ❌   |
 | Italian            | 2b3134224392f58da00f802faa1bf4b5cf6270be |     ✅     |    ❌   |
 | Italian rev 1      | 015a5d380afe316a2a6fcc561798ebff9dfb3009 |     ✅     |    ❌   |
 | Japanese           | 5c5e546720300b99ae45d2aa35c646c8b8ff5c56 |     ✅     |    ❌   |
 | Japanese rev 1     | 971e0d670a95e5b32240b2deed20405b8daddf47 |     ✅     |    ❌   |
 | Spanish            | 1f49f7289253dcbfecbc4c5ba3e67aa0652ec83c |     ✅     |    ❌   |
 | Spanish rev 1      | 9ac73481d7f5d150a018309bba91d185ce99fb7c |     ✅     |    ❌   |
</details>

<details>
  <summary>Pokemon Sapphire</summary>

 | Nom                | Hash                                     | Supported | Tested |
 |--------------------|------------------------------------------|-----------|--------|
 | USA (Europe)       | 3ccbbd45f8553c36463f13b938e833f652b793e4 |     ✅     |    ✅   |
 | USA (Europe) rev 1 | 4722efb8cd45772ca32555b98fd3b9719f8e60a9 |     ✅     |    ❌   |
 | USA (Europe) rev 2 | 89b45fb172e6b55d51fc0e61989775187f6fe63c |     ✅     |    ❌   |
 | French             | c269b5692b2d0e5800ba1ddf117fda95ac648634 |     ✅     |    ✅   |
 | French rev 1       | 860e93f5ea44f4278132f6c1ee5650d07b852fd8 |     ✅     |    ❌   |
 | Deutch             | 5a087835009d552d4c5c1f96be3be3206e378153 |     ✅     |    ❌   |
 | Deutch rev 1       | 7e6e034f9cdca6d2c4a270fdb50a94def5883d17 |     ✅     |    ❌   |
 | Italian            | f729dd571fb2c09e72c5c1d68fe0a21e72713d34 |     ✅     |    ❌   |
 | Italian rev 1      | 73edf67b9b82ff12795622dca412733755d2c0fe |     ✅     |    ❌   |
 | Japanese           | 3233342c2f3087e6ffe6c1791cd5867db07df842 |     ✅     |    ❌   |
 | Japanese rev 1     | 01f509671445965236ac4c6b5a354fe2f1e69f13 |     ✅     |    ❌   |
 | Spanish            | 3a6489189e581c4b29914071b79207883b8c16d8 |     ✅     |    ❌   |
 | Spanish rev 1      | 0fe9ad1e602e2fafa090aee25e43d6980625173c |     ✅     |    ❌   |
</details>

<details>
  <summary>Pokemon Fire Red</summary>

 | Nom                | Hash                                     | Supported | Tested |
 |--------------------|------------------------------------------|-----------|--------|
 | USA (Europe)       | 41cb23d8dccc8ebd7c649cd8fbb58eeace6e2fdc |     ✅     |    ✅   |
 | USA (Europe) rev 1 | dd5945db9b930750cb39d00c84da8571feebf417 |     ✅     |    ❌   |
 | French             | fc663907256f06a3a09e2d6b967bc9af4919f111 |     ✅     |    ✅   |
 | Deutch             | 18a3758ceeef2c77b315144be2c3910d6f1f69fe |     ✅     |    ❌   |
 | Italian            | 66a9d415205321376b4318534c0dce5f69d28362 |     ✅     |    ❌   |
 | Japanese           | 04139887b6cd8f53269aca098295b006ddba6cfe |     ✅     |    ❌   |
 | Japanese rev 1     | 7c7107b87c3ccf6e3dbceb9cf80ceeffb25a1857 |     ✅     |    ❌   |
 | Spanish            | ab8f6bfe0ccdaf41188cd015c8c74c314d02296a |     ✅     |    ❌   |
</details>

<details>
  <summary>Pokemon Leaf Green</summary>

 | Nom                | Hash                                     | Supported | Tested |
 |--------------------|------------------------------------------|-----------|--------|
 | USA (Europe)       | 574fa542ffebb14be69902d1d36f1ec0a4afd71e |     ✅     |    ✅   |
 | USA (Europe) rev 1 | 7862c67bdecbe21d1d69ce082ce34327e1c6ed5e |     ✅     |    ❌   |
 | French             | 4b5758c14d0a07b70ef3ef0bd7fa5e7ce6978672 |     ✅     |    ❌   |
 | Deutch             | 0802d1fb185ee3ed48d9a22afb25e66424076dac |     ✅     |    ❌   |
 | Italian            | a1dfea1493d26d1f024be8ba1de3d193fcfc651e |     ✅     |    ❌   |
 | Japanese           | 5946f1b59e8d71cc61249661464d864185c92a5f |     ✅     |    ❌   |
 | Japanese rev 1     | de9d5a844f9bfb63a4448cccd4a2d186ecf455c3 |     ✅     |    ❌   |
 | Spanish            | f9ebee5d228cb695f18ef2ced41630a09fa9eb05 |     ✅     |    ❌   |
</details>

# <a name="overview"></a>Overview
## <a name="overview-video"></a>Video

## <a name="overview-features"></a>Features
* Settings
  * Configure the emulator and game through Pokebot such as speed and sound.
* Logs
  * View all logs inside the application.
* Statistiques
  * View pokemon statistics, used to see how many pokemons have been encountered since the beginning with the shiny ratio.
* Bot
  * Configure and start any [supported bot](#overview-bots)
* Viewer
  * View opponent pokemon & party stats such as hidden power, moves, IVs.
* Version Checker
  * A popup will appear when a new version is available.
* Discord Webhook
  * Get a notification in any discord server using webhooks.

## <a name="overview-bots"></a>Bots
* Starter
  * This bot will choose your selected starter and see if it matches your set filters. If not, it will change the seed and reload the game in a loop.
* Spin
  * This bot will spin your character on the map and when the battle start, it will check if the opponent pokemon matches your set filters. If not, it will escape the fight and try again.
* Static
  * This bot will start a battle against a static pokemon and check if it matches your set filters. If not, it will change the seed and reload the game in a loop.
* PokeFinder
  * This bot will press A at the specified frame. Sometimes the hit frame is different from the specified frame. In this case, you can use PokeFinder to find the hit frame and include it in the bot for adjustment. (To use this bot, your emulator must simulate dead battery)

## <a name="export-save"></a>How to export save file
BizHawk saves game data in the `GBA/SaveRam` directory. To export this save as a `.sav` extension for most games, you can simply rename the file extension to `.sav` without having to make any changes. 
Sometimes, however, the file may be invalid, so you need to remove the very last line using a hexadecimal editor ([HxD](https://mh-nexus.de/en/hxd/)). 
You can follow this [tutorial](https://gbatemp.net/threads/cant-make-pokemon-emerald-sav-file-to-work-on-vba.631681/#post-10136922).

# <a name="code-documentation"></a>Code Documentation
The code documentation is available on [Wiki](https://github.com/Kakumi/Pokebot/wiki). You will find explanations on how to add supported games, bots or even features.

# <a name="todo"></a>Todo & Ideas (implementation not comfirmed)
* Add bot for eggs
* Add bot for fishing
* Support translations
* Add feature to PokeFinder bot to save before the specified frame and not at start.
* Read seed
* Transform SaveRam to sav file automatically

<details>
	<summary>Done</summary>
	
* Add feature that works with [PokeFinder](https://github.com/Admiral-Fish/PokeFinder) ✔️
* Show trainer ID & Secret in the view (PokeFinder bot) ✔️
* Ability to execute bot every x seconds instead of every frame ✔️
* Discord Webhook ✔️
* Add bot for static pokemon️ ✔️
* Save & load settings to/from config file ✔️
</details>

# <a name="known-bugs"></a> Known bugs
_Yeah! There's nothing here!_

# <a name="sources"></a>Sources
* [BizHawk Documentation](https://github.com/TASEmulators/BizHawk)
* [Pokemon Decompilation Source Code](https://github.com/pret/pokeemerald)
* [40Cakes Source Code](https://github.com/40Cakes/pokebot-gen3)
* [Bulbapedia Pokemon Data Structure](https://bulbapedia.bulbagarden.net/wiki/Pok%C3%A9mon_data_structure_(Generation_III))
* [PokéAPI](https://pokeapi.co/)
* [PokeFinder](https://github.com/Admiral-Fish/PokeFinder)
* [Hexa to Decimal Converter](https://www.rapidtables.com/convert/number/hex-to-decimal.html)
* [GBA Backup Tool](https://www.gamebrew.org/wiki/GBA_Backup_Tool)
* [GBA Memory Domains](https://corrupt.wiki/systems/gameboy-advance/bizhawk-memory-domains)

# <a name="license"></a>License
Pokebot can be used by anyone for any purpose allowed by the permissive MIT License.
