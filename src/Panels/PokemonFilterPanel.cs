using Pokebot.Models;
using Pokebot.Models.Config;
using Pokebot.Models.Pokemons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Pokebot.Panels
{
    public partial class PokemonFilterPanel : UserControl
    {
        public PokemonComparator Comparator { get; }

        public PokemonFilterPanel()
        {
            InitializeComponent();

            Comparator = new PokemonComparator();
        }

        public void Initialize(GenerationInfo generationInfo)
        {
            SetPokemons(generationInfo.Pokemons);
            SetNatures(generationInfo.Natures);
            SetTypes(generationInfo.Types);
            SetItems(generationInfo.Items);
            SetGenders();
        }

        public void SetPokemons(IEnumerable<PokemonConfig> pokemons, bool any = true)
        {
            var items = new List<ComboBoxItem>();
            if (any)
            {
                items.Add(new ComboBoxItem(-1, Messages.Filter_AnyPokemon));
            }
            items.AddRange(pokemons.Select(x => new ComboBoxItem(x.Id, x.Name)));
            InitializeDefaultComboBox(_pokemonComboBox, items);
        }

        public void HidePokemons()
        {
            _pokemonComboBox.Hide();
        }

        public void SetNatures(IEnumerable<PokemonNature> natures)
        {
            var items = new List<ComboBoxItem>();
            items.Add(new ComboBoxItem(-1, Messages.Filter_AnyNature));
            items.AddRange(natures.Select(x => new ComboBoxItem(x.Id, x.Name)));
            InitializeDefaultComboBox(_naturesComboBox, items);
        }

        public void SetTypes(IEnumerable<PokemonType> types)
        {
            var items = new List<ComboBoxItem>();
            items.Add(new ComboBoxItem(-1, Messages.Filter_AnyType));
            items.AddRange(types.Select(x => new ComboBoxItem(x.Id, x.Name)));
            InitializeDefaultComboBox(_typesComboBox, items);
        }

        public void SetItems(IEnumerable<PokemonItem> pokemonItems)
        {
            var items = new List<ComboBoxItem>();
            items.Add(new ComboBoxItem(-1, Messages.Filter_AnyItem));
            items.AddRange(pokemonItems.Select(x => new ComboBoxItem(x.Id, x.Name)));
            InitializeDefaultComboBox(_heldItemComboBox, items);
        }

        public void SetGenders()
        {
            var items = new List<ComboBoxItem>
            {
                new ComboBoxItem(-1, Messages.Filter_AnyGender),
                new ComboBoxItem((int)PokemonGender.Male, Messages.Gender_Male),
                new ComboBoxItem((int)PokemonGender.Female, Messages.Gender_Female),
                new ComboBoxItem((int)PokemonGender.Genderless, Messages.Gender_Genderless)
            };
            InitializeDefaultComboBox(_genderComboBox, items);
        }

        private void InitializeDefaultComboBox(ComboBox comboBox, List<ComboBoxItem> items)
        {
            comboBox.Items.Clear();
            comboBox.Items.AddRange(items.ToArray());
            comboBox.DisplayMember = nameof(ComboBoxItem.Name);
            comboBox.ValueMember = nameof(ComboBoxItem.Id);
            comboBox.SelectedIndex = 0;
            comboBox.Show();
        }

        private void IvSpDefense_ValueChanged(object sender, EventArgs e)
        {
            Comparator.IVSpDefense = (int)Math.Round(((NumericUpDown)sender).Value);
        }

        private void IvDefense_ValueChanged(object sender, EventArgs e)
        {
            Comparator.IVDefense = (int)Math.Round(((NumericUpDown)sender).Value);
        }

        private void IvSpAttack_ValueChanged(object sender, EventArgs e)
        {
            Comparator.IVSpAttack = (int)Math.Round(((NumericUpDown)sender).Value);
        }

        private void IvAttack_ValueChanged(object sender, EventArgs e)
        {
            Comparator.IVAttack = (int)Math.Round(((NumericUpDown)sender).Value);
        }

        private void IvSpeed_ValueChanged(object sender, EventArgs e)
        {
            Comparator.IVSpeed = (int)Math.Round(((NumericUpDown)sender).Value);
        }

        private void IvHp_ValueChanged(object sender, EventArgs e)
        {
            Comparator.IVHP = (int)Math.Round(((NumericUpDown)sender).Value);
        }

        private void ShinyCheckChanged(object sender, EventArgs e)
        {
            Comparator.IsShiny = ((CheckBox)sender).Checked;
        }

        private void ExactIVCheckChanged(object sender, EventArgs e)
        {
            Comparator.ExactIV = ((CheckBox)sender).Checked;
        }

        private void PokemonSelectionChanged(object sender, EventArgs e)
        {
            Comparator.IndexPokemon = ((ComboBoxItem)((ComboBox)sender).SelectedItem).Id;
        }

        private void HeldItemSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            Comparator.IndexHeldItem = ((ComboBoxItem)((ComboBox)sender).SelectedItem).Id;
        }

        private void NatureSelectionChanged(object sender, EventArgs e)
        {
            Comparator.IndexNature = ((ComboBoxItem)((ComboBox)sender).SelectedItem).Id;
        }

        private void TypeSelectionChanged(object sender, EventArgs e)
        {
            Comparator.IndexType = ((ComboBoxItem)((ComboBox)sender).SelectedItem).Id;
        }

        private void GenderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Comparator.PokemonGender = ((ComboBoxItem)((ComboBox)sender).SelectedItem).Id;
        }
    }
}
