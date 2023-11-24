using Pokebot.Models.Pokemons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokebot.Panels
{
    public partial class PartyPokemonViewer : UserControl
    {
        public PokemonViewerPanel PokemonViewerPanel { get; }

        public PartyPokemonViewer()
        {
            InitializeComponent();

            PokemonViewerPanel = new PokemonViewerPanel();
            PokemonViewerPanel.Dock = DockStyle.Fill;
            PokemonViewerPanel.Hide();

            _partyBox.Items.Clear();
            _panelViewer.Controls.Clear();
            _panelViewer.Controls.Add(PokemonViewerPanel);
        }

        public void SetParty(List<Pokemon> pokemons)
        {
            _partyBox.Items.Clear();

            if (pokemons.Count == 0)
            {
                PokemonViewerPanel.Hide();
            } else
            {
                _partyBox.Items.AddRange(pokemons.ToArray());
                _partyBox.DisplayMember = nameof(Pokemon.RealName);
                _partyBox.SelectedIndex = 0;
            }
        }

        public void Clear()
        {
            _partyBox.Items.Clear();
            _partyBox.SelectedItem = null;
            _partyBox.SelectedValue = null;
            _partyBox.Text = null;
            PokemonViewerPanel.Hide();
        }

        private void _partyBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_partyBox.SelectedItem is Pokemon pokemon)
            {
                PokemonViewerPanel.SetPokemon(pokemon);
                PokemonViewerPanel.Show();
            }
        }
    }
}
