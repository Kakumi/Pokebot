using BizHawk.Client.Common;
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

namespace Pokebot
{
    public partial class PokebotDebug : Form
    {
        public ApiContainer APIContainer { get; }
        public string BizhawkPath { get; }

        public PokebotDebug(ApiContainer APIContainer)
        {
            InitializeComponent();
            this.APIContainer = APIContainer;
            BizhawkPath = @"D:\VisualStudioProjects\Pokebot\BizHawk";

            var saveStates = GetSaveStates();
            if (saveStates.Count() > 0 )
            {
                _statesComboBox.Items.AddRange(saveStates);
                _statesComboBox.DisplayMember = nameof(SaveStateFile.Filename);
                _statesComboBox.ValueMember = nameof(SaveStateFile.FullPath);
                _statesComboBox.SelectedIndex = 0;
            }
        }

        public SaveStateFile[] GetSaveStates()
        {
            var saveStatesPath = Path.Combine(BizhawkPath, "GBA", "State");
            return Directory.GetFiles(saveStatesPath, "*.State").Select(x => new SaveStateFile(x)).ToArray();
        }

        private void _quickStartButton_Click(object sender, EventArgs e)
        {
            try
            {
                var success = APIContainer.EmuClient.OpenRom(@$"D:\VisualStudioProjects\Pokebot\BizHawk\Roms\Pokemon - Version Emeraude (FR).gba");
                if (success)
                {
                    if (_statesComboBox.SelectedItem is SaveStateFile saveStateFile)
                    {
                        APIContainer.EmuClient.LoadState(@saveStateFile.FullPath.Replace(".State", ""));
                    }
                }
            } catch(Exception ex)
            {
                SetStatus(ex.Message);
            }
        }

        private void SetStatus(string status)
        {
            if (status == null)
            {
                _status.Hide();
            } else
            {
                _status.Show();
                _status.Text = status;
            }
        }
    }

    public class SaveStateFile
    {
        public string FullPath { get; }
        public string Filename { get; }

        public SaveStateFile(string path)
        {
            FullPath = path;
            Filename = Path.GetFileNameWithoutExtension(path);
        }
    }
}
