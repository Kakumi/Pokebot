using Pokebot.Utils;
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
    public partial class StarterControl : UserControl
    {
        public delegate void StarterChangedEventHandler(ComboBox e, int index);

        public event StarterChangedEventHandler? StarterChanged;

        public StarterControl()
        {
            InitializeComponent();
        }

        public void SetStarters(IEnumerable<string> starters)
        {
            _starterBox.Items.AddRange(starters.ToArray());
            _starterBox.SelectedIndex = 0;
        }

        private void StarterSelectionChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                StarterChanged?.Invoke(comboBox, comboBox.SelectedIndex);
            }
        }
    }
}
