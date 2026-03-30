using Pokebot.Models;
using Pokebot.Theme;
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

        // Stores the semantic level and optional exception for each list item so we
        // can re-color all entries whenever the theme changes.
        private sealed class LogItemTag
        {
            public LogLevel Level { get; }
            public Exception Exception { get; }

            public LogItemTag(LogLevel level, Exception exception)
            {
                Level = level;
                Exception = exception;
            }
        }

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
            _logsListView.Resize += (s, e) => AdjustColumns();

            ThemeManager.ThemeChanged += OnThemeChanged;
            Disposed += (s, e) => ThemeManager.ThemeChanged -= OnThemeChanged;

            AdjustColumns();
        }

        private void ApplyTranslations()
        {
            level.Text = Messages.Logs_HeaderType;
            message.Text = Messages.Logs_HeaderMessage;
        }

        private void Log_LogReceived(LogEventArgs e)
        {
            var item = new ListViewItem(e.Level.ToString());
            item.ForeColor = GetColorForLevel(e.Level);
            item.SubItems.Add(e.Message);
            item.Tag = new LogItemTag(e.Level, e.Exception);

            _logsListView.Items.Add(item);
            AdjustColumns();
        }

        private void OnThemeChanged()
        {
            foreach (ListViewItem item in _logsListView.Items)
            {
                if (item.Tag is LogItemTag tag)
                {
                    item.ForeColor = GetColorForLevel(tag.Level);
                }
            }
            _logsListView.Invalidate();
            AdjustColumns();
        }

        private void AdjustColumns()
        {
            if (_logsListView.Columns.Count < 2 || _logsListView.ClientSize.Width <= 0)
            {
                return;
            }

            int headerTypeWidth = TextRenderer.MeasureText(level.Text, _logsListView.Font).Width + 20;
            _logsListView.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);
            int contentTypeWidth = _logsListView.Columns[0].Width + 14;
            int typeWidth = Math.Max(headerTypeWidth, contentTypeWidth);
            typeWidth = Math.Min(typeWidth, Math.Max(90, _logsListView.ClientSize.Width / 3));

            int messageWidth = Math.Max(120, _logsListView.ClientSize.Width - typeWidth - 6);

            _logsListView.Columns[0].Width = typeWidth;
            _logsListView.Columns[1].Width = messageWidth;
        }

        private static Color GetColorForLevel(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Warn:
                    return ThemeManager.Current.WarningColorValue;
                case LogLevel.Error:
                case LogLevel.Fatal:
                    return ThemeManager.Current.ErrorColorValue;
                default:
                    return ThemeManager.Current.TextColorValue;
            }
        }

        private void LogsListView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var info = _logsListView.HitTest(e.Location);
                if (info.Item != null)
                {
                    info.Item.Selected = true;
                    _copyStackTraceMenuItem.Enabled = info.Item.Tag is LogItemTag t && t.Exception != null;
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
                if (item.Tag is LogItemTag tag && tag.Exception != null)
                {
                    Clipboard.SetText(tag.Exception.ToString());
                }
            }
        }
    }
}
