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
    public partial class PokeFinderControl : UserControl
    {
        public delegate void RetryEventHandler();
        public event RetryEventHandler RetryClick;

        public delegate void PokeFinderTypeEventHandler(PokeFinderType type);
        public event PokeFinderTypeEventHandler PokeFinderTypeChanged;

        public int FrameAdvance { get; private set; }

        public PokeFinderControl()
        {
            InitializeComponent();

            FrameAdvance = 0;
        }

        public long GetInputFrame()
        {
            return (long) _inputFrame.Value;
        }

        public string GetPID()
        {
            return _inputPID.Text;
        }

        public string GetOpponentPID()
        {
            return _opponentPID.Text;
        }

        public void SetInfo(int tid, int sid)
        {
            _labelTIDSID.Text = string.Format(Messages.BotPokeFinder_TrainerInfo, tid, sid);
        }

        public void SetPokeFinderTypes(List<PokeFinderType> types)
        {
            _pokefinderTypesComboBox.Items.Clear();
            _pokefinderTypesComboBox.Items.AddRange(types.ToArray());
            _pokefinderTypesComboBox.DisplayMember = nameof(PokeFinderType.Name);
            _pokefinderTypesComboBox.SelectedIndex = 0;
        }

        public void SetCurrentFrame(int frame)
        {
            _frameLabel.Text = string.Format(Messages.PokeFinder_CurrentFrame, frame.ToString());
        }

        public long GetHitFrame()
        {
            return (long)_hitFrameField.Value;
        }

        public long GetTargetFrame()
        {
            long targetFrame;
            if (GetHitFrame() != 0)
            {
                targetFrame = GetInputFrame() - Math.Abs(GetHitFrame() - GetInputFrame());
            } else
            {
                targetFrame = GetInputFrame();
            }

            return targetFrame - FrameAdvance;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            _retryButton.Enabled = _hitFrameField.Value != 0;
        }

        private void _retryButton_Click(object sender, EventArgs e)
        {
            RetryClick?.Invoke();
        }

        internal void SetPID(string pid)
        {
            _inputPID.Text = pid;
        }

        internal void SetOpponentPID(string pid)
        {
            _opponentPID.Text = pid;
        }

        private void _pokefinderTypesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_pokefinderTypesComboBox.SelectedItem is PokeFinderType pokeFinderType)
            {
                FrameAdvance = pokeFinderType.FrameAdvance;
                PokeFinderTypeChanged?.Invoke(pokeFinderType);
            }
        }
    }
}
