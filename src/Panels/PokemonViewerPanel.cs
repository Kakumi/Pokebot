using Pokebot.Images;
using Pokebot.Models.Pokemons;
using System.Drawing;
using System.Text;
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
            _pid.Text = string.Format(Messages.Viewer_PID, pokemon.PID.ToString("X"));
            _natureLabel.Text = string.Format(Messages.Viewer_Nature, pokemon.Nature.Name);
            _abilityLabel.Text = string.Format(Messages.Viewer_Ability, pokemon.Ability);
            _hiddenPowerType.Text = string.Format(Messages.Viewer_HiddenPowerType, pokemon.HiddenPower.Name);
            _hiddenPowerDamage.Text = string.Format(Messages.Viewer_HiddenPowerDamage, pokemon.HiddenPowerDamage);
            _itemLabel.Text = string.Format(Messages.Viewer_Item, pokemon.HeldItem?.Name ?? Messages.Item_Nothing);
            var imageName = pokemon.RealName.ToLower().Replace("-", "_");
            if (pokemon.IsShiny)
            {
                _pokemonPictureBox.Image = (Image)ResourcesPokemonShiny.ResourceManager.GetObject(imageName);
                _shinyLabel.Text = Messages.Viewer_Shiny;
                _shinyLabel.ForeColor = Color.Green;
            }
            else
            {
                _pokemonPictureBox.Image = (Image)ResourcesPokemon.ResourceManager.GetObject(imageName);
                _shinyLabel.Text = Messages.Viewer_NotShiny;
                _shinyLabel.ForeColor = Color.Red;
            }
            _ivHPLabel.Text = string.Format(Messages.Viewer_HP, pokemon.IVs.HP);
            _ivSpeedLabel.Text = string.Format(Messages.Viewer_Speed, pokemon.IVs.Speed);
            _ivAttackLabel.Text = string.Format(Messages.Viewer_Attack, pokemon.IVs.Attack);
            _ivDefenseLabel.Text = string.Format(Messages.Viewer_Defense, pokemon.IVs.Defense);
            _ivSpAttackLabel.Text = string.Format(Messages.Viewer_SpAttack, pokemon.IVs.SpAttack);
            _ivSpDefenseLabel.Text = string.Format(Messages.Viewer_SpDefense, pokemon.IVs.SpDefense);
            _gender.Text = string.Format(Messages.Viewer_Gender, pokemon.GetGenderMessage());
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
                moveLabel.Text = string.Format(Messages.Viewer_Move, move.MoveInfo.Name, move.PP, move.MoveInfo.PP);
            }
            else
            {
                moveLabel.Text = Messages.Viewer_NoMove;
            }
        }
    }
}
