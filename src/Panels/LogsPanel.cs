using Pokebot.Models;
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
    public partial class LogsPanel : UserControl
    {
        public LogsPanel()
        {
            InitializeComponent();

            Log.LogReceived += Log_LogReceived;
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

            _logsListView.Items.Add(item);

            for (int i = 0; i < _logsListView.Columns.Count; i++)
            {
                _logsListView.Columns[i].Width = -1;
            }
        }
    }
}
