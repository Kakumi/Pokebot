using BizHawk.Client.Common;
using BizHawk.Common.IOExtensions;
using Pokebot.Factories.Versions;
using Pokebot.Models;
using Pokebot.Models.Player;
using Pokebot.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BizHawk.Client.EmuHawk.BatchRunner;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Pokebot
{
    public partial class PokebotDebug : Form
    {
        public ApiContainer APIContainer { get; set; }
        public GameVersion? GameVersion { get; private set; }
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

        public void SetGameVersion(GameVersion gameVersion)
        {
            GameVersion = gameVersion;
        }

        public void Execute(GameState state)
        {
            if (GameVersion != null)
            {
                _stateLabel.Text = state.ToString();

                PlayerData player = GameVersion.Memory.GetPlayer();
                if (player != null)
                {
                    SetPlayerInfo(player);
                }

                var tasks = GameVersion.Memory.GetTasks();
                StringBuilder sb = new StringBuilder();
                foreach(var task in tasks)
                {
                    var bytesArray = string.Join("-", task.Data);
                    var bytesTxt = Encoding.UTF8.GetString(task.Data).Replace("\0", "");
                    sb.AppendLine($"{task.Name} - active: {(task.IsActive ? "Yes" : "No")} - data: {bytesArray} - string: {bytesTxt}");
                }

                _tasksLabel.Text = sb.ToString();

                UpdateFinderList();
            }
        }

        public void SetPlayerInfo(PlayerData player)
        {
            _playerX.Text = $"X: {player.Position.X}";
            _playerY.Text = $"Y: {player.Position.Y}";
            _playerFacing.Text = $"Direction: {player.FacingDirection.ToString()}";
        }

        private void UpdateFinderList()
        {
            if (_finderList.Items.Count > 0)
            {
                foreach (var item in _finderList.Items)
                {
                    if (item is ListViewItem lvi)
                    {
                        int address = int.Parse(lvi.Text, System.Globalization.NumberStyles.HexNumber);
                        int size = int.Parse(lvi.SubItems[3].Text);
                        int offset = int.Parse(lvi.SubItems[4].Text);
                        var bytes = SymbolUtil.Read(APIContainer, address, offset, size);
                        string value;
                        if (size == 1)
                        {
                            value = bytes[0].ToString();
                        }
                        else if (size == 2)
                        {
                            value = bytes.ToUInt16().ToString();
                        }
                        else if (size == 4)
                        {
                            value = bytes.ToUInt32().ToString();
                        }
                        else
                        {
                            value = Encoding.UTF8.GetString(bytes.ToArray());
                        }

                        lvi.SubItems[1].Text = value;
                        lvi.SubItems[5].Text = string.Join("-", bytes);

                        if (value != lvi.SubItems[2].Text)
                        {
                            lvi.BackColor = Color.Yellow;
                        } else
                        {
                            lvi.BackColor = Color.White;
                        }
                    }
                }
            }
        }

        private void runFinderButton_Click(object sender, EventArgs e)
        {
            try
            {
                var symbol = _finderSymbolsCB.SelectedItem as Symbol;
                int address = (int)symbol!.Address;
                var result = MemoryAddressFinder.Search(GameVersion!.APIContainer, address, (int)_finderIterationUpDown.Value, _finderValueTextBox.Text, (int)_finderSize.Value);
                if (result.Count == 0)
                {
                    MessageBox.Show($"Not found");
                }
                else
                {
                    _finderList.Items.AddRange(result.Select(x =>
                    {
                        var lvi = new ListViewItem(x);
                        lvi.SubItems.Add("_");
                        lvi.SubItems.Add(_finderValueTextBox.Text);
                        lvi.SubItems.Add(_finderSize.Value.ToString());
                        lvi.SubItems.Add("0");
                        lvi.SubItems.Add("_");

                        return lvi;
                    }).ToArray());
                }
            } catch(Exception ex)
            {
                MessageBox.Show($"Error: " + ex.Message);
            }
        }

        private void _finderListenButton_Click(object sender, EventArgs e)
        {
            var worker = new BackgroundWorker();
            worker.DoWork += async (s, e) =>
            {
                await Task.Delay(3000);
                _finderSymbolsCB.Invoke(() => runFinderButton_Click(s, e));
            };

            worker.RunWorkerAsync();
        }

        private void _finderClear_Click(object sender, EventArgs e)
        {
            _finderList.Items.Clear();
        }

        private void _finderSymbolsCB_Click(object sender, EventArgs e)
        {
            if (_finderSymbolsCB.Items.Count == 0)
            {
                _finderSymbolsCB.Items.AddRange(GameVersion.Memory.GetSymbols().ToArray());
                _finderSymbolsCB.DisplayMember = nameof(Symbol.Name);
                _finderSymbolsCB.SelectedIndex = 0;
            }
        }

        private void _filterAdd_Click(object sender, EventArgs e)
        {
            var symbol = _finderSymbolsCB.SelectedItem as Symbol;
            int address = (int)symbol!.Address;
            var lvi = new ListViewItem(address.ToString());
            lvi.SubItems.Add("_");
            lvi.SubItems.Add(_finderValueTextBox.Text);
            lvi.SubItems.Add(_finderSize.Value.ToString());
            lvi.SubItems.Add(_finderOffsetUpDown.Value.ToString());
            lvi.SubItems.Add("_");
            _finderList.Items.Add(lvi);
        }

        private void _finderInspectReverse_Click(object sender, EventArgs e)
        {
            var symbol = _finderSymbolsCB.SelectedItem as Symbol;
            int address = (int)symbol!.Address;
            var bytes = SymbolUtil.Read(APIContainer, address, (int)_finderOffsetUpDown.Value, (int)_finderSize.Value);
            var cb2Address = bytes.ToUInt32();
            MessageBox.Show($"Reverse address for {symbol.Name} is {(cb2Address - 1).ToString("X")}");
        }

        private void SearchGMain()
        {
            var result = SearchTest();

            _finderList.Items.AddRange(result.Select(x =>
            {
                var lvi = new ListViewItem(x.Item1.ToString("X"));
                lvi.SubItems.Add("_");
                lvi.SubItems.Add(x.Item2 + "");
                lvi.SubItems.Add("4");
                lvi.SubItems.Add("0");
                lvi.SubItems.Add("_");

                return lvi;
            }).ToArray());
        }

        private List<Tuple<int, uint>> SearchTest()
        {
            var potentials = new List<Tuple<int, uint>>();
            int minAddr = 0x03000000;
            int maxAddr = 0x03007FFF;
            int size = maxAddr - minAddr;
            int length = 4;

            var bytes = SymbolUtil.Read(APIContainer, minAddr, 0, size);

            for (int i = 0; i < size - length; i++) { 
                var value = bytes.Skip(i).Take(length).ToUInt32();
                var addresses = new List<uint>()
                {
                    134571444,
                    134267604
                };
                if (addresses.Contains(value) || addresses.Contains(value - 1) || addresses.Contains(value + 1) || value.ToString("X").StartsWith("08") || value.ToString("X").StartsWith("8"))
                {
                    if (value >= 0x08000000)
                    {
                        potentials.Add(new Tuple<int, uint>(minAddr + i, value));
                    }
                }
            }

            return potentials;
        }

        private void SearchTasks()
        {
            try
            {
                int minAddr = 0x03000000;
                int maxAddr = 0x03007FFF;
                int size = maxAddr - minAddr;
                int length = 0x280;

                var bytes = SymbolUtil.Read(APIContainer, minAddr, 0, size);

                for (int i = 0; i < size; i++)
                {
                    var bytesTask = bytes.Skip(i).Take(length).ToArray();
                    var tasks = new List<GTask>();
                    for (int j = 0; j < 16; j++)
                    {
                        var offset = j * 40;
                        var isActive = bytesTask[offset + 4] == 1;
                        if (isActive)
                        {
                            var bytesName = bytesTask.Skip(offset).Take(4);
                            var addr = bytesName.ToUInt32() - 1;
                            string taskName = addr.ToString();

                            var prev = (int)bytesTask[offset + 5];
                            var next = (int)bytesTask[offset + 6];
                            var priority = (int)bytesTask[offset + 7];
                            var data = bytesTask.Skip(offset + 8).Take(32).ToArray();

                            tasks.Add(new GTask(
                                taskName,
                                isActive,
                                prev,
                                next,
                                priority,
                                data
                            ));
                        }
                    }

                    if (tasks.Count == 3)
                    {
                        if (tasks[0].Data.All(x => x == 0) && tasks[1].Data.All(x => x == 0) && tasks[2].Data[0] == 0 && tasks[2].Data[1] == 0 && tasks[2].Data[2] == 3)
                        {
                            int target = 0x03005090;
                            int current = minAddr + i;
                            if (current == target)
                            {

                            }
                        }
                    }
                }
            }
            catch { }
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
