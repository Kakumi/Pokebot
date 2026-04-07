using Pokebot.Models;
using Pokebot.Models.Pokemons;
using System.Windows.Forms;

namespace Pokebot.Panels
{
    public partial class EncounterStatsPanel : UserControl
    {
        public EncounterStatsPanel()
        {
            InitializeComponent();
            ApplyTranslations();
        }

        private void ApplyTranslations()
        {
            columnHeader1.Text = Messages.Stats_HeaderPokemon;
            columnHeader2.Text = Messages.Stats_HeaderEncounters;
            columnHeader3.Text = Messages.Stats_HeaderShiny;
            columnHeader4.Text = Messages.Stats_HeaderRatio;
        }

        public void Clear()
        {
            _statsListView.Items.Clear();
        }

        public EncounterStats GetPokemonStat(Pokemon pokemon)
        {
            for (int i = 0; i < _statsListView.Items.Count; i++)
            {
                var item = _statsListView.Items[i];
                if (item != null && item.Text == pokemon.RealName)
                {
                    if (int.TryParse(item.SubItems[1].Text, out int encounters) && int.TryParse(item.SubItems[2].Text, out int shinyEncounters))
                    {
                        return new EncounterStats(encounters, shinyEncounters, GetRatio(shinyEncounters, encounters), pokemon);
                    }
                }
            }

            return new EncounterStats(0, 0, GetRatio(0, 0), pokemon);
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
                        item.SubItems[3].Text = GetRatio(shinyEncounters, encounters);
                    }

                    found = true;
                }
            }

            if (!found)
            {
                var item = new ListViewItem(pokemon.RealName);
                item.SubItems.Add(encounters.ToString());
                item.SubItems.Add(shinyEncounters.ToString());
                item.SubItems.Add(GetRatio(shinyEncounters, encounters));

                _statsListView.Items.Add(item);
            }
        }

        private string GetRatio(int shiny, int normal)
        {
            return ((double)shiny / normal).ToString("0.########%");
        }
    }
}
