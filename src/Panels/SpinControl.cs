using Pokebot.Models.Config;
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
    public partial class SpinControl : UserControl
    {
        public PokemonFilterPanel FilterPanel { get; private set; }

        public SpinControl()
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
        }
    }
}
