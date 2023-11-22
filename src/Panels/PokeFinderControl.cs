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

        public PokeFinderControl()
        {
            InitializeComponent();
        }

        public long GetInputFrame()
        {
            return (long) _inputFrame.Value;
        }

        public string GetPID()
        {
            return _inputPID.Text;
        }

        public void SetInfo(int tid, int sid)
        {
            _labelTIDSID.Text = string.Format(Messages.BotPokeFinder_TrainerInfo, tid, sid);
        }

        public long GetHitFrame()
        {
            return (long)_hitFrameField.Value;
        }

        public long GetTargetFrame()
        {
            if (GetHitFrame() != 0)
            {
                return GetInputFrame() - Math.Abs(GetHitFrame() - GetInputFrame());
            }

            return GetInputFrame();
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
    }
}
