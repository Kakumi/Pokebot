using Pokebot.Images;
using Pokebot.Models.Config;
using Pokebot.Models.Pokemons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokebot.Panels
{
    public partial class PokemonViewerPanel : UserControl
    {
        public PokemonViewerPanel()
        {
            InitializeComponent();
        }

        public void SetPokemon(Pokemon pokemon)
        {
            StringBuilder sb = new StringBuilder(pokemon.RealName);
            if (pokemon is PartyPokemon partyPokemon)
            {
                sb.Append(" (").Append(partyPokemon.Level).Append(")");
            }
            _pokemonName.Text = sb.ToString();
            _natureLabel.Text = $"Nature: {pokemon.Nature.Name}";
            _abilityLabel.Text = $"Ability: {pokemon.Ability}";
            _hiddenPowerType.Text = $"Hidden power type: {pokemon.HiddenPower.Name}";
            _hiddenPowerDamage.Text = $"Hidden power damage: {pokemon.HiddenPowerDamage}";
            _itemLabel.Text = $"Item: {pokemon.HeldItem?.Name ?? "Nothing"}";
            var imageName = pokemon.RealName.ToLower().Replace("-", "_");
            if (pokemon.IsShiny)
            {
                _pokemonPictureBox.Image = (Image)ResourcesPokemonShiny.ResourceManager.GetObject(imageName);
                _shinyLabel.Text = $"Shiny";
                _shinyLabel.ForeColor = Color.Green;
            } else
            {
                _pokemonPictureBox.Image = (Image)ResourcesPokemon.ResourceManager.GetObject(imageName);
                _shinyLabel.Text = $"Not Shiny";
                _shinyLabel.ForeColor = Color.Red;
            }
            _ivHPLabel.Text = $"IV: {pokemon.IVs.HP}";
            _ivSpeedLabel.Text = $"Speed: {pokemon.IVs.Speed}";
            _ivAttackLabel.Text = $"Attack: {pokemon.IVs.Attack}";
            _ivDefenseLabel.Text = $"Defense: {pokemon.IVs.Defense}";
            _ivSpAttackLabel.Text = $"Sp. Attack: {pokemon.IVs.SpAttack}";
            _ivSpDefenseLabel.Text = $"Sp. Defense: {pokemon.IVs.SpDefense}";
            SetMove(_move1Label, 0, pokemon);
            SetMove(_move2Label, 1, pokemon);
            SetMove(_move3Label, 2, pokemon);
            SetMove(_move4Label, 3, pokemon);
        }

        private void SetMove(Label moveLabel, int index, Pokemon pokemon)
        {
            if (index < pokemon.Moves.Count)
            {
                var move = pokemon.Moves[index];
                moveLabel.Text = $"{move.MoveInfo.Name} ({move.PP}/{move.MoveInfo.PP})";
            } else
            {
                moveLabel.Text = "/";
            }
        }
    }
}
