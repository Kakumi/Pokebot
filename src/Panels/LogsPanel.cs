using Pokebot.Models;
using Pokebot.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pokebot.Panels
{
    public partial class LogsPanel : UserControl
    {
        private ContextMenuStrip _contextMenu;
        private ToolStripMenuItem _copyStackTraceMenuItem;

        public LogsPanel()
        {
            InitializeComponent();
            ApplyTranslations();

            Log.LogReceived += Log_LogReceived;
            _contextMenu = new ContextMenuStrip();
            _copyStackTraceMenuItem = new ToolStripMenuItem(Messages.Logs_CopyStackTrace);
            _copyStackTraceMenuItem.Click += CopyStackTraceMenuItem_Click;
            _contextMenu.Items.Add(_copyStackTraceMenuItem);
            _logsListView.ContextMenuStrip = _contextMenu;

            _logsListView.MouseUp += LogsListView_MouseUp;
        }

        // Modify Log_LogReceived to store the Exception in the Tag property
        private void ApplyTranslations()
        {
            level.Text = Messages.Logs_HeaderType;
            message.Text = Messages.Logs_HeaderMessage;
        }

        private void Log_LogReceived(LogEventArgs e)
        {
            var item = new ListViewItem(e.Level.ToString());
            switch (e.Level)
            {
                case LogLevel.Debug:
                case LogLevel.Info:
                    item.ForeColor = Color.Black;
                    break;
                case LogLevel.Warn:
                    item.ForeColor = Color.Orange;
                    break;
                case LogLevel.Error:
                case LogLevel.Fatal:
                    item.ForeColor = Color.Red;
                    break;
            }
            item.SubItems.Add(e.Message);
            item.Tag = e.Exception;

            _logsListView.Items.Add(item);
            for (int i = 0; i < _logsListView.Columns.Count; i++)
            {
                _logsListView.Columns[i].Width = -1;
            }
        }

        // Add these event handlers to the LogsPanel class
        private void LogsListView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var info = _logsListView.HitTest(e.Location);
                if (info.Item != null)
                {
                    info.Item.Selected = true;
                    _copyStackTraceMenuItem.Enabled = info.Item.Tag is Exception;
                    _contextMenu.Show(_logsListView, e.Location);
                }
                else
                {
                    _copyStackTraceMenuItem.Enabled = false;
                }
            }
        }

        private void CopyStackTraceMenuItem_Click(object sender, EventArgs e)
        {
            if (_logsListView.SelectedItems.Count > 0)
            {
                var item = _logsListView.SelectedItems[0];
                if (item.Tag is Exception ex)
                {
                    Clipboard.SetText(ex.ToString());
                }
            }
        }
    }
}
