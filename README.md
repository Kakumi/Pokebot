# Pokebot
Pokebot is a tool for [BizHawk](https://github.com/TASEmulators/BizHawk) that automates certain actions to find the perfect legitimate pokemons for supported pokemon games.

Want to know how this tool has been developed and how it works ? [Visit my website](https://damienbrebion.com/#blogs)

[![(latest) release | GitHub](https://img.shields.io/github/release/Kakumi/Pokebot.svg?logo=github&logoColor=333333&sort=semver&style=popout)](https://github.com/Kakumi/Pokebot/releases/latest)
[![GitHub open issues counter](https://img.shields.io/github/issues-raw/Kakumi/Pokebot.svg?logo=github&logoColor=333333&style=popout)](https://github.com/Kakumi/Pokebot/issues)

---

Jump to:
* [Dependencies](#dependencies)
* [Installation](#install)
* [Supported Games](#supported-games)
* [Overview](#overview)
  * [Video](#overview-video)
  * [Features](#overview-features)
  * [Bots](#overview-bots)
* [Code Documentation](#code-documentation)
* [Todo & Ideas](#todo)
* [Sources](#sources)
* [License](#license)

# <a name="dependencies"></a>Dependencies
* Download [BizHawk](https://github.com/TASEmulators/BizHawk) emulator
* Download [this tool](https://github.com/Kakumi/Pokebot/releases/latest)

# <a name="install"></a>Installation
Place the file `Pokebot.dll` inside `BizHawk/ExternalTools/` folder. Then, open BizHawk and click on "Tools" -> "External Tools" -> "Pokebot". A window should open. Just load your pokemon officiel ROM in BizHawk and Pokebot will automatically update the window if the game is [supported](#supported-games).

# <a name="supported-games"></a>Supported Games
* Pokemon Emerald (D,I,F,J,S)
* Pokemon Ruby (D,I,F,J,S)
* Pokemon Sapphire (D,I,F,J,S)
* Pokemon Fire Red (D,I,F,J,S)
* Pokemon Leaf Green (D,I,F,J,S)

# <a name="overview"></a>
## <a name="overview-video"></a>Video

## <a name="overview-features"></a>Features
* Settings
  * Configure the emulator through Pokebot such as speed and sound.
* Logs
  * View all logs inside the application.
* Statistiques
  * View pokemon statistics, used to see how many pokemons have been encountered since the beginning with the shiny ratio.
* Bot
  * Configure and start any [supported bot](#overview-bots)
* Viewer
  * View opponent pokemon stats such as hidden power, moves, IVs.

## <a name="overview-bots"></a>Bots
* Starter
  * This bot will choose your selected starter and see if it matches your set filters. If not, it will change the seed and reload the game in a loop.
* Spin
  * This bot will spin your character on the map and when the battle start, it will check if the opponent pokemon matches your set filters. If not, it will escape the fight and try again.

# <a name="code-documentation"></a>Code Documentation
The code documentation is available on [Wiki](https://github.com/Kakumi/Pokebot/wiki). You will find explanations on how to add supported games, bots or even features.

# <a name="todo"></a>Todo & Ideas
* Add feature that works with [PokeFinder](https://github.com/Admiral-Fish/PokeFinder)
* Add bot for eggs
* Add bot for legendary
* Add bot for fishing
* Show trainer ID & Secret in the view
* Inject custom seed
* Discord Webhook
* Save & load settings to/from config file

# <a name="sources"></a>Sources
* [BizHawk Documentation](https://github.com/TASEmulators/BizHawk)
* [Pokemon Decompilation Source Code](https://github.com/pret/pokeemerald)
* [40Cakes Source Code](https://github.com/40Cakes/pokebot-gen3)
* [Bulbapedia Pokemon Data Structure](https://bulbapedia.bulbagarden.net/wiki/Pok%C3%A9mon_data_structure_(Generation_III))
* [PokéAPI](https://pokeapi.co/)
* [Hexa to Decimal Converter](https://www.rapidtables.com/convert/number/hex-to-decimal.html)
* [GBA Backup Tool](https://www.gamebrew.org/wiki/GBA_Backup_Tool)

# <a name="license"></a>License
Pokebot can be used by anyone for any purpose allowed by the permissive MIT License.
