using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Pokebot.Models;

namespace Pokebot.Theme
{
    public static class ThemeManager
    {
        private const int CheckBoxGlyphSize = 14;
        private const string UxThemeDll = "uxtheme.dll";
        private const int WmPaint = 0x000F;
        private const int WmNcPaint = 0x0085;

        [DllImport(UxThemeDll, CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hWnd, string? pszSubAppName, string? pszSubIdList);

        private static readonly Dictionary<ComboBox, ComboBoxAdorner> ComboBoxAdorners = new Dictionary<ComboBox, ComboBoxAdorner>();
        private static readonly Dictionary<NumericUpDown, NumericUpDownAdorner> NumericUpDownAdorners =
            new Dictionary<NumericUpDown, NumericUpDownAdorner>();

        public static ThemeConfig Current { get; private set; } = ThemeConfig.Load();

        public static event Action? ThemeChanged;

        public static void Apply(ThemeConfig config)
        {
            Current = config;
            config.Save();
            ThemeChanged?.Invoke();
        }

        public static void ApplyTo(Control root)
        {
            ApplyToControl(root);
            root.Refresh();
        }

        // Basic reset
        // Removes all explicit color/style overrides so Windows visual styles
        // and system colors take full control again.
        private static void ResetControl(Control control)
        {
            if (control is Button btn)
            {
                btn.FlatStyle = FlatStyle.Standard;
                btn.FlatAppearance.BorderSize = 1;
                btn.UseVisualStyleBackColor = true;
                btn.ResetBackColor();
                btn.ResetForeColor();
            }
            else if (control is TabControl tc)
            {
                tc.DrawMode = TabDrawMode.Normal;
                tc.Appearance = TabAppearance.Normal;
                tc.DrawItem -= TabControl_DrawItem;
                tc.Paint -= TabControl_Paint;
                tc.HandleCreated -= TabControl_HandleCreated;
                tc.HandleCreated += TabControl_HandleCreated;
                ApplyNativeTheme(tc, false);
                tc.ResetBackColor();
                tc.ResetForeColor();
            }
            else if (control is TabPage tp)
            {
                tp.UseVisualStyleBackColor = true;
                tp.ResetBackColor();
                tp.ResetForeColor();
            }
            else if (control is TextBox txt)
            {
                txt.BorderStyle = BorderStyle.Fixed3D;
                txt.ResetBackColor();
                txt.ResetForeColor();
            }
            else if (control is ListView lv)
            {
                lv.OwnerDraw = false;
                lv.DrawColumnHeader -= ListView_DrawColumnHeader;
                lv.DrawItem -= ListView_DrawItem;
                lv.DrawSubItem -= ListView_DrawSubItem;
                lv.BorderStyle = BorderStyle.Fixed3D;
                lv.ResetBackColor();
                lv.ResetForeColor();
            }
            else if (control is NumericUpDown nud)
            {
                ((UpDownBase)nud).BorderStyle = BorderStyle.Fixed3D;
                nud.HandleCreated -= NumericUpDown_HandleCreated;
                nud.HandleCreated += NumericUpDown_HandleCreated;
                ApplyNativeTheme(nud, false);
                DetachNumericUpDownAdorner(nud);
                nud.ResetBackColor();
                nud.ResetForeColor();
            }
            else if (control is ComboBox cmb)
            {
                cmb.FlatStyle = FlatStyle.Standard;
                cmb.DrawMode = DrawMode.Normal;
                cmb.DrawItem -= ComboBox_DrawItem;
                cmb.HandleCreated -= ComboBox_HandleCreated;
                cmb.HandleCreated += ComboBox_HandleCreated;
                ApplyNativeTheme(cmb, false);
                DetachComboBoxAdorner(cmb);
                cmb.ResetBackColor();
                cmb.ResetForeColor();
            }
            else if (control is CheckBox chk)
            {
                chk.Paint -= CheckBox_Paint;
                chk.UseVisualStyleBackColor = true;
                chk.FlatStyle = FlatStyle.Standard;
                chk.ResetForeColor();
                chk.ResetBackColor();
            }
            else if (control is GroupBox gb)
            {
                gb.Paint -= GroupBox_Paint;
                gb.ResetBackColor();
                gb.ResetForeColor();
            }
            else
            {
                control.ResetBackColor();
                control.ResetForeColor();
            }

            foreach (Control child in control.Controls)
            {
                ResetControl(child);
            }
        }

        // Themed application
        private static void ApplyToControl(Control control)
        {
            if (Current.IsBasic)
            {
                ResetControl(control);
                return;
            }

            if (control is Button themedBtn)
            {
                // Color-swatch buttons in ThemePanel carry an integer Tag: skip them
                // so they keep displaying the color they represent.
                if (themedBtn.Tag is int)
                {
                    return;
                }

                themedBtn.FlatStyle = FlatStyle.Flat;
                themedBtn.FlatAppearance.BorderSize = 0;
                themedBtn.BackColor = Current.AccentColorValue;
                themedBtn.ForeColor = Current.ButtonTextColorValue;
                themedBtn.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Current.AccentColorValue, 0.15f);
                themedBtn.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(Current.AccentColorValue, 0.1f);
            }
            else if (control is TabControl tc)
            {
                tc.BackColor = Current.ControlBackColorValue;
                tc.ForeColor = Current.TextColorValue;
                tc.Appearance = TabAppearance.Normal;
                tc.DrawMode = TabDrawMode.OwnerDrawFixed;
                tc.DrawItem -= TabControl_DrawItem;
                tc.DrawItem += TabControl_DrawItem;
                tc.Paint -= TabControl_Paint;
                tc.Paint += TabControl_Paint;
                tc.HandleCreated -= TabControl_HandleCreated;
                tc.HandleCreated += TabControl_HandleCreated;
                ApplyNativeTheme(tc, true);
            }
            else if (control is TabPage tp)
            {
                tp.UseVisualStyleBackColor = false;
                tp.BackColor = Current.ControlBackColorValue;
                tp.ForeColor = Current.TextColorValue;
            }
            else if (control is TextBox txt)
            {
                txt.BorderStyle = BorderStyle.FixedSingle;
                txt.BackColor = Current.InputBackColorValue;
                txt.ForeColor = Current.TextColorValue;
            }
            else if (control is RichTextBox)
            {
                control.BackColor = Current.InputBackColorValue;
                control.ForeColor = Current.TextColorValue;
            }
            else if (control is NumericUpDown nud)
            {
                ((UpDownBase)nud).BorderStyle = BorderStyle.None;
                nud.BackColor = Current.InputBackColorValue;
                nud.ForeColor = Current.TextColorValue;
                nud.HandleCreated -= NumericUpDown_HandleCreated;
                nud.HandleCreated += NumericUpDown_HandleCreated;
                ApplyNativeTheme(nud, true);
                AttachNumericUpDownAdorner(nud);
            }
            else if (control is ComboBox cmb)
            {
                cmb.FlatStyle = FlatStyle.Flat;
                cmb.BackColor = Current.InputBackColorValue;
                cmb.ForeColor = Current.TextColorValue;
                cmb.DrawMode = DrawMode.OwnerDrawFixed;
                cmb.DrawItem -= ComboBox_DrawItem;
                cmb.DrawItem += ComboBox_DrawItem;
                cmb.HandleCreated -= ComboBox_HandleCreated;
                cmb.HandleCreated += ComboBox_HandleCreated;
                ApplyNativeTheme(cmb, true);
                AttachComboBoxAdorner(cmb);
            }
            else if (control is ListView lv)
            {
                lv.BorderStyle = BorderStyle.None;
                lv.BackColor = Current.InputBackColorValue;
                lv.ForeColor = Current.TextColorValue;
                lv.OwnerDraw = true;
                lv.DrawColumnHeader -= ListView_DrawColumnHeader;
                lv.DrawColumnHeader += ListView_DrawColumnHeader;
                lv.DrawItem -= ListView_DrawItem;
                lv.DrawItem += ListView_DrawItem;
                lv.DrawSubItem -= ListView_DrawSubItem;
                lv.DrawSubItem += ListView_DrawSubItem;
            }
            else if (control is CheckBox chk)
            {
                chk.UseVisualStyleBackColor = false;
                chk.FlatStyle = FlatStyle.Flat;
                chk.BackColor = Current.ControlBackColorValue;
                chk.ForeColor = Current.TextColorValue;
                chk.Paint -= CheckBox_Paint;
                chk.Paint += CheckBox_Paint;
            }
            else if (control is Label)
            {
                control.BackColor = Color.Transparent;
                control.ForeColor = Current.TextColorValue;
            }
            else if (control is GroupBox)
            {
                control.BackColor = Current.ControlBackColorValue;
                control.ForeColor = Current.TextColorValue;
                control.Paint -= GroupBox_Paint;
                control.Paint += GroupBox_Paint;
            }
            else
            {
                control.BackColor = Current.ControlBackColorValue;
                control.ForeColor = Current.TextColorValue;
            }

            foreach (Control child in control.Controls)
            {
                ApplyToControl(child);
            }
        }

        private static void TabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            var tc = (TabControl)sender;
            TabPage tabPage = tc.TabPages[e.Index];
            bool isSelected = e.Index == tc.SelectedIndex;

            Color bg = isSelected ? Current.ControlBackColorValue : ControlPaint.Dark(Current.ControlBackColorValue, 0.15f);

            Rectangle rect = isSelected ? new Rectangle(e.Bounds.X - 2, e.Bounds.Y - 2, e.Bounds.Width + 4, e.Bounds.Height + 4) : e.Bounds;

            using (var bgBrush = new SolidBrush(bg))
            using (var borderPen = new Pen(bg))
            {
                e.Graphics.FillRectangle(bgBrush, rect);
                e.Graphics.DrawRectangle(borderPen, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
            }

            TextRenderer.DrawText(
                e.Graphics,
                tabPage.Text,
                e.Font ?? tc.Font,
                e.Bounds,
                Current.TextColorValue,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine
            );
        }

        private static void TabControl_Paint(object sender, PaintEventArgs e)
        {
            var tc = (TabControl)sender;
            if (tc.Width <= 0 || tc.Height <= 0)
            {
                return;
            }

            using (var bgBrush = new SolidBrush(Current.ControlBackColorValue))
            {
                if (tc.TabCount > 0)
                {
                    int headerBottom = 0;
                    for (int i = 0; i < tc.TabCount; i++)
                    {
                        Rectangle tabRect = tc.GetTabRect(i);
                        if (tabRect.Bottom > headerBottom)
                        {
                            headerBottom = tabRect.Bottom;
                        }
                    }

                    if (headerBottom > 0)
                    {
                        Rectangle headerRect = new Rectangle(0, 0, tc.Width, headerBottom + 2);
                        using (var region = new Region(headerRect))
                        {
                            for (int i = 0; i < tc.TabCount; i++)
                            {
                                region.Exclude(tc.GetTabRect(i));
                            }
                            e.Graphics.FillRegion(bgBrush, region);
                        }
                    }
                }

                Rectangle display = tc.DisplayRectangle;
                if (display.Width > 0 && display.Height > 0)
                {
                    e.Graphics.FillRectangle(bgBrush, display);
                }
            }

            Rectangle border = tc.DisplayRectangle;
            if (border.Width <= 0 || border.Height <= 0)
            {
                return;
            }

            using (var borderPen = new Pen(Current.ControlBackColorValue, 2f))
            {
                e.Graphics.DrawRectangle(borderPen, border.X - 2, border.Y - 2, border.Width + 3, border.Height + 3);
            }
        }

        private static void ListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            Color borderColor = ControlPaint.Dark(Current.AccentColorValue, 0.25f);

            using (var backBrush = new SolidBrush(Current.AccentColorValue))
            using (var borderPen = new Pen(borderColor))
            {
                e.Graphics.FillRectangle(backBrush, e.Bounds);
                e.Graphics.DrawRectangle(borderPen, e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1);
            }

            TextRenderer.DrawText(
                e.Graphics,
                e.Header.Text,
                e.Font,
                e.Bounds,
                Current.ButtonTextColorValue,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.NoPadding
            );
        }

        private static void ListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            var lv = (ListView)sender;
            if (lv.View != View.Details)
            {
                bool selected = (e.State & ListViewItemStates.Selected) != 0;
                Color bg = selected ? ControlPaint.Dark(Current.AccentColorValue, 0.20f) : Current.InputBackColorValue;
                Color fg = selected ? Current.ButtonTextColorValue : e.Item.ForeColor;

                using (var backBrush = new SolidBrush(bg))
                {
                    e.Graphics.FillRectangle(backBrush, e.Bounds);
                }

                TextRenderer.DrawText(
                    e.Graphics,
                    e.Item.Text,
                    lv.Font,
                    e.Bounds,
                    fg,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.NoPadding
                );
            }
        }

        private static void ListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            var lv = (ListView)sender;
            if (lv.View != View.Details)
            {
                return;
            }

            bool selected = (e.ItemState & ListViewItemStates.Selected) != 0;
            Color bg = selected ? ControlPaint.Dark(Current.AccentColorValue, 0.20f) : Current.InputBackColorValue;
            Color gridColor = ControlPaint.Dark(Current.ControlBackColorValue, 0.25f);
            Color textColor = selected ? Current.ButtonTextColorValue : (e.SubItem.ForeColor.IsEmpty ? e.Item.ForeColor : e.SubItem.ForeColor);

            using (var backBrush = new SolidBrush(bg))
            using (var gridPen = new Pen(gridColor))
            {
                e.Graphics.FillRectangle(backBrush, e.Bounds);
                e.Graphics.DrawLine(gridPen, e.Bounds.Left, e.Bounds.Bottom - 1, e.Bounds.Right, e.Bounds.Bottom - 1);
                e.Graphics.DrawLine(gridPen, e.Bounds.Right - 1, e.Bounds.Top, e.Bounds.Right - 1, e.Bounds.Bottom);
            }

            Rectangle textBounds = new Rectangle(e.Bounds.X + 4, e.Bounds.Y, Math.Max(0, e.Bounds.Width - 8), e.Bounds.Height);
            TextRenderer.DrawText(
                e.Graphics,
                e.SubItem.Text,
                lv.Font,
                textBounds,
                textColor,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.NoPadding
            );
        }

        private static void CheckBox_Paint(object sender, PaintEventArgs e)
        {
            var chk = (CheckBox)sender;

            e.Graphics.Clear(Current.ControlBackColorValue);

            int glyphY = (chk.Height - CheckBoxGlyphSize) / 2;
            Rectangle glyphRect = new Rectangle(0, Math.Max(0, glyphY), CheckBoxGlyphSize, CheckBoxGlyphSize);
            Color glyphBack = chk.Checked ? Current.AccentColorValue : Current.InputBackColorValue;
            Color glyphBorder = chk.Checked
                ? ControlPaint.Dark(Current.AccentColorValue, 0.2f)
                : ControlPaint.Dark(Current.ControlBackColorValue, 0.3f);

            using (var glyphBrush = new SolidBrush(glyphBack))
            using (var glyphBorderPen = new Pen(glyphBorder))
            {
                e.Graphics.FillRectangle(glyphBrush, glyphRect);
                e.Graphics.DrawRectangle(glyphBorderPen, glyphRect);
            }

            if (chk.Checked)
            {
                using (var tickPen = new Pen(Current.ButtonTextColorValue, 2f))
                {
                    tickPen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                    tickPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                    e.Graphics.DrawLines(
                        tickPen,
                        new[]
                        {
                            new Point(glyphRect.X + 3, glyphRect.Y + 7),
                            new Point(glyphRect.X + 6, glyphRect.Y + 10),
                            new Point(glyphRect.X + 11, glyphRect.Y + 4),
                        }
                    );
                }
            }

            Rectangle textRect = new Rectangle(glyphRect.Right + 6, 0, Math.Max(0, chk.Width - glyphRect.Right - 6), chk.Height);

            TextRenderer.DrawText(
                e.Graphics,
                chk.Text,
                chk.Font,
                textRect,
                chk.Enabled ? Current.TextColorValue : SystemColors.GrayText,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine
            );
        }

        private static void GroupBox_Paint(object sender, PaintEventArgs e)
        {
            var gb = (GroupBox)sender;
            e.Graphics.Clear(Current.ControlBackColorValue);

            Size textSize = TextRenderer.MeasureText(gb.Text, gb.Font);
            int textLeft = 8;
            int borderY = textSize.Height / 2;
            int textPad = 6;

            using (var borderPen = new Pen(Current.TextColorValue))
            {
                e.Graphics.DrawLine(borderPen, 0, borderY, textLeft - 2, borderY);
                e.Graphics.DrawLine(borderPen, textLeft + textSize.Width + textPad, borderY, gb.Width - 1, borderY);
                e.Graphics.DrawLine(borderPen, 0, borderY, 0, gb.Height - 1);
                e.Graphics.DrawLine(borderPen, gb.Width - 1, borderY, gb.Width - 1, gb.Height - 1);
                e.Graphics.DrawLine(borderPen, 0, gb.Height - 1, gb.Width - 1, gb.Height - 1);
            }

            TextRenderer.DrawText(e.Graphics, gb.Text, gb.Font, new Point(textLeft, 0), Current.TextColorValue, Current.ControlBackColorValue);
        }

        private static void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            var cmb = (ComboBox)sender;
            if (e.Index < 0 && string.IsNullOrEmpty(cmb.Text))
            {
                return;
            }

            bool selected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            Color bg = selected ? ControlPaint.Dark(Current.AccentColorValue, 0.1f) : Current.InputBackColorValue;
            Color fg = selected ? Current.ButtonTextColorValue : Current.TextColorValue;

            using (var bgBrush = new SolidBrush(bg))
            {
                e.Graphics.FillRectangle(bgBrush, e.Bounds);
            }

            string text = e.Index >= 0 ? cmb.GetItemText(cmb.Items[e.Index]) : cmb.Text;
            Rectangle textBounds = new Rectangle(e.Bounds.X + 3, e.Bounds.Y, Math.Max(0, e.Bounds.Width - 6), e.Bounds.Height);
            TextRenderer.DrawText(
                e.Graphics,
                text,
                cmb.Font,
                textBounds,
                fg,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine
            );
            e.DrawFocusRectangle();
        }

        private static void TabControl_HandleCreated(object sender, EventArgs e)
        {
            ApplyNativeTheme((Control)sender, !Current.IsBasic);
        }

        private static void ComboBox_HandleCreated(object sender, EventArgs e)
        {
            ApplyNativeTheme((Control)sender, !Current.IsBasic);
        }

        private static void NumericUpDown_HandleCreated(object sender, EventArgs e)
        {
            ApplyNativeTheme((Control)sender, !Current.IsBasic);
        }

        private static void ApplyNativeTheme(Control control, bool disableVisualStyles)
        {
            if (!control.IsHandleCreated)
            {
                return;
            }

            if (disableVisualStyles)
            {
                SetWindowTheme(control.Handle, string.Empty, string.Empty);
            }
            else
            {
                SetWindowTheme(control.Handle, null, null);
            }
        }

        private static void AttachComboBoxAdorner(ComboBox cmb)
        {
            if (ComboBoxAdorners.ContainsKey(cmb))
            {
                return;
            }

            var adorner = new ComboBoxAdorner(cmb);
            ComboBoxAdorners[cmb] = adorner;
        }

        private static void DetachComboBoxAdorner(ComboBox cmb)
        {
            if (!ComboBoxAdorners.TryGetValue(cmb, out var adorner))
            {
                return;
            }

            adorner.Dispose();
            ComboBoxAdorners.Remove(cmb);
        }

        private static void AttachNumericUpDownAdorner(NumericUpDown nud)
        {
            if (NumericUpDownAdorners.ContainsKey(nud))
            {
                return;
            }

            var adorner = new NumericUpDownAdorner(nud);
            NumericUpDownAdorners[nud] = adorner;
        }

        private static void DetachNumericUpDownAdorner(NumericUpDown nud)
        {
            if (!NumericUpDownAdorners.TryGetValue(nud, out var adorner))
            {
                return;
            }

            adorner.Dispose();
            NumericUpDownAdorners.Remove(nud);
        }

        private sealed class ComboBoxAdorner : NativeWindow, IDisposable
        {
            private readonly ComboBox _combo;

            public ComboBoxAdorner(ComboBox combo)
            {
                _combo = combo;
                _combo.HandleCreated += Combo_HandleCreated;
                _combo.HandleDestroyed += Combo_HandleDestroyed;
                _combo.Disposed += Combo_Disposed;

                if (_combo.IsHandleCreated)
                {
                    AssignHandle(_combo.Handle);
                }
            }

            private void Combo_HandleCreated(object sender, EventArgs e)
            {
                if (_combo.IsHandleCreated)
                {
                    AssignHandle(_combo.Handle);
                }
            }

            private void Combo_HandleDestroyed(object sender, EventArgs e)
            {
                ReleaseHandle();
            }

            private void Combo_Disposed(object sender, EventArgs e)
            {
                Dispose();
            }

            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);

                if (Current.IsBasic || _combo.DropDownStyle == ComboBoxStyle.Simple)
                {
                    return;
                }

                if (m.Msg == WmPaint || m.Msg == WmNcPaint)
                {
                    DrawComboArrow();
                }
            }

            private void DrawComboArrow()
            {
                if (!_combo.IsHandleCreated || _combo.Width <= 0 || _combo.Height <= 0)
                {
                    return;
                }

                using (Graphics g = Graphics.FromHwnd(_combo.Handle))
                {
                    using (var outerPen = new Pen(Current.ControlBackColorValue))
                    {
                        g.DrawRectangle(outerPen, 0, 0, Math.Max(0, _combo.Width - 1), Math.Max(0, _combo.Height - 1));
                    }

                    int buttonWidth = Math.Max(18, SystemInformation.VerticalScrollBarWidth);
                    Rectangle buttonRect = new Rectangle(_combo.Width - buttonWidth - 1, 1, buttonWidth, Math.Max(0, _combo.Height - 2));
                    Color borderColor = ControlPaint.Dark(Current.AccentColorValue, 0.2f);

                    using (var accentBrush = new SolidBrush(Current.AccentColorValue))
                    using (var borderPen = new Pen(borderColor))
                    {
                        g.FillRectangle(accentBrush, buttonRect);
                        g.DrawRectangle(borderPen, buttonRect.X, buttonRect.Y, buttonRect.Width - 1, buttonRect.Height - 1);
                    }

                    int cx = buttonRect.Left + (buttonRect.Width / 2);
                    int cy = buttonRect.Top + (buttonRect.Height / 2) + 1;
                    Point[] arrow = { new Point(cx - 4, cy - 2), new Point(cx + 4, cy - 2), new Point(cx, cy + 2) };
                    using (var arrowBrush = new SolidBrush(Current.ButtonTextColorValue))
                    {
                        g.FillPolygon(arrowBrush, arrow);
                    }
                }
            }

            public void Dispose()
            {
                _combo.HandleCreated -= Combo_HandleCreated;
                _combo.HandleDestroyed -= Combo_HandleDestroyed;
                _combo.Disposed -= Combo_Disposed;
                ReleaseHandle();
            }
        }

        private sealed class NumericUpDownAdorner : NativeWindow, IDisposable
        {
            private readonly NumericUpDown _nud;

            public NumericUpDownAdorner(NumericUpDown nud)
            {
                _nud = nud;
                _nud.HandleCreated += NUD_HandleCreated;
                _nud.HandleDestroyed += NUD_HandleDestroyed;
                _nud.Disposed += NUD_Disposed;

                if (_nud.IsHandleCreated)
                {
                    AssignHandle(_nud.Handle);
                }
            }

            private void NUD_HandleCreated(object sender, EventArgs e)
            {
                if (_nud.IsHandleCreated)
                {
                    AssignHandle(_nud.Handle);
                }
            }

            private void NUD_HandleDestroyed(object sender, EventArgs e)
            {
                ReleaseHandle();
            }

            private void NUD_Disposed(object sender, EventArgs e)
            {
                Dispose();
            }

            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);

                if (Current.IsBasic)
                {
                    return;
                }

                if (m.Msg == WmPaint || m.Msg == WmNcPaint)
                {
                    DrawButtons();
                }
            }

            private void DrawButtons()
            {
                if (!_nud.IsHandleCreated || _nud.Width <= 0 || _nud.Height <= 0)
                {
                    return;
                }

                using (Graphics g = Graphics.FromHwnd(_nud.Handle))
                {
                    using (var outerPen = new Pen(Current.ControlBackColorValue))
                    {
                        g.DrawRectangle(outerPen, 0, 0, Math.Max(0, _nud.Width - 1), Math.Max(0, _nud.Height - 1));
                    }

                    int buttonWidth = Math.Max(16, SystemInformation.VerticalScrollBarWidth - 1);
                    Rectangle buttonRect = new Rectangle(_nud.Width - buttonWidth - 1, 1, buttonWidth, Math.Max(0, _nud.Height - 2));
                    int midY = buttonRect.Top + (buttonRect.Height / 2);
                    Color borderColor = ControlPaint.Dark(Current.AccentColorValue, 0.2f);

                    using (var accentBrush = new SolidBrush(Current.AccentColorValue))
                    using (var borderPen = new Pen(borderColor))
                    using (var splitPen = new Pen(Current.ButtonTextColorValue))
                    {
                        g.FillRectangle(accentBrush, buttonRect);
                        g.DrawRectangle(borderPen, buttonRect.X, buttonRect.Y, buttonRect.Width - 1, buttonRect.Height - 1);
                        g.DrawLine(splitPen, buttonRect.Left + 2, midY, buttonRect.Right - 3, midY);
                    }

                    int cx = buttonRect.Left + (buttonRect.Width / 2);
                    Point[] upArrow =
                    {
                        new Point(cx - 3, buttonRect.Top + 6),
                        new Point(cx + 3, buttonRect.Top + 6),
                        new Point(cx, buttonRect.Top + 3),
                    };
                    Point[] downArrow =
                    {
                        new Point(cx - 3, buttonRect.Bottom - 7),
                        new Point(cx + 3, buttonRect.Bottom - 7),
                        new Point(cx, buttonRect.Bottom - 4),
                    };

                    using (var arrowBrush = new SolidBrush(Current.ButtonTextColorValue))
                    {
                        g.FillPolygon(arrowBrush, upArrow);
                        g.FillPolygon(arrowBrush, downArrow);
                    }
                }
            }

            public void Dispose()
            {
                _nud.HandleCreated -= NUD_HandleCreated;
                _nud.HandleDestroyed -= NUD_HandleDestroyed;
                _nud.Disposed -= NUD_Disposed;
                ReleaseHandle();
            }
        }
    }
}
