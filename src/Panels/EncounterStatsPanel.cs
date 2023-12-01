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
    public partial class EncounterStatsPanel : UserControl
    {
        public EncounterStatsPanel()
        {
            InitializeComponent();
        }

        public void Clear()
        {
            _statsListView.Items.Clear();
        }

        public void AddPokemonStat(Pokemon pokemon)
        {
            var encounters = 1;
            var shinyEncounters = pokemon.IsShiny ? 1 : 0;
            var found = false;

            for (int i = 0; i < _statsListView.Items.Count; i++)
            {
                var item = _statsListView.Items[i];
                if (item != null && item.Text == pokemon.RealName)
                {
                    if (int.TryParse(item.SubItems[1].Text, out encounters) && int.TryParse(item.SubItems[2].Text, out int currentShinyEncounters))
                    {
                        encounters += 1;
                        currentShinyEncounters += shinyEncounters;

                        item.SubItems[1].Text = encounters.ToString();
                        item.SubItems[2].Text = currentShinyEncounters.ToString();
                        item.SubItems[3].Text = $"{(shinyEncounters / encounters) * 100}%";
                    }

                    found = true;
                }
            }

            if (!found)
            {
                var item = new ListViewItem(pokemon.RealName);
                item.SubItems.Add(encounters.ToString());
                item.SubItems.Add(shinyEncounters.ToString());
                item.SubItems.Add($"{(shinyEncounters / encounters) * 100}%");

                _statsListView.Items.Add(item);
            }
        }
    }
}
