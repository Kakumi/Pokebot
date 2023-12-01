using Pokebot.Models.Config;
using System.Windows.Forms;

namespace Pokebot.Panels
{
    public partial class StaticControl : UserControl
    {
        public PokemonFilterPanel FilterPanel { get; private set; }

        public StaticControl()
        {
            InitializeComponent();
            FilterPanel = new PokemonFilterPanel();
        }

        public void SetFilterPanel(GenerationInfo generationInfo)
        {
            FilterPanel.Dock = DockStyle.Fill;

            _filterPanel.Controls.Clear();
            _filterPanel.Controls.Add(FilterPanel);

            FilterPanel.Initialize(generationInfo);
            FilterPanel.HidePokemons();
        }
    }
}
