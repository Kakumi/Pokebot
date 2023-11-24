using BizHawk.Client.Common;
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
    public partial class DebugPanel : UserControl
    {
        public ApiContainer APIContainer { get; }

        public DebugPanel(ApiContainer apiContainer)
        {
            InitializeComponent();
            APIContainer = apiContainer;
        }

        private void _loadButton_Click(object sender, EventArgs e)
        {
            var success = APIContainer?.EmuClient.OpenRom(@$"D:\VisualStudioProjects\Pokebot\BizHawk\Roms\Pokemon - Version Emeraude (FR).gba");
            if (success ?? false)
            {
                APIContainer?.EmuClient.LoadState("Emerald_pokefinder"); //starter | wild
            }
        }
    }
}
