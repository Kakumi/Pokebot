using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Pokebot.Controls;
using Pokebot.Models;

namespace Pokebot.Theme
{
    public static class ThemeManager
    {
        private const int CheckBoxGlyphSize = 14;
        private const string UxThemeDll = "uxtheme.dll";
        private const string UpDownClassName = "msctls_updown32";
        private const int WmPaint = 0x000F;
        private const int WmNcPaint = 0x0085;
        private const int LvmFirst = 0x1000;
        private const int LvmSetBkColor = LvmFirst + 1;
        private const int LvmGetHeader = LvmFirst + 31;
        private const int LvmSetTextColor = LvmFirst + 36;
        private const int LvmSetTextBkColor = LvmFirst + 38;

        [DllImport(UxThemeDll, CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hWnd, string? pszSubAppName, string? pszSubIdList);

        [DllImport("user32.dll")]
        private static extern bool EnumChildWindows(IntPtr hWndParent, EnumChildProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetClassName(IntPtr hWnd, System.Text.StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll")]
        private static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        private delegate bool EnumChildProc(IntPtr hWnd, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        private static readonly Dictionary<ComboBox, ComboBoxAdorner> ComboBoxAdorners = new Dictionary<ComboBox, ComboBoxAdorner>();
        private static readonly Dictionary<NumericUpDown, NumericUpDownAdorner> NumericUpDownAdorners =
            new Dictionary<NumericUpDown, NumericUpDownAdorner>();
        private static readonly Dictionary<NumericUpDown, NumericUpDownOverlay> NumericUpDownOverlays =
            new Dictionary<NumericUpDown, NumericUpDownOverlay>();
        private static readonly HashSet<NumericUpDown> NumericUpDownStyled = new HashSet<NumericUpDown>();

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
            if (!Current.IsBasic)
            {
                RefreshNumericUpDownOverlays(root);
            }
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
                lv.HandleCreated -= ListView_HandleCreated;
                lv.HandleCreated += ListView_HandleCreated;
                ApplyNativeTheme(lv, false);
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
                DetachNumericUpDownOverlay(nud);
                nud.ResetBackColor();
                nud.ResetForeColor();
            }
            else if (control is ThemedNumericUpDown themedNud)
            {
                themedNud.BackColor = SystemColors.Window;
                themedNud.ForeColor = SystemColors.WindowText;
                themedNud.BorderColor = SystemColors.WindowFrame;
                themedNud.ButtonBackColor = SystemColors.Control;
                themedNud.ButtonForeColor = SystemColors.ControlText;
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
                RegisterNumericUpDownHooks(nud);
                StyleNumericUpDownChildren(nud);
                nud.HandleCreated -= NumericUpDown_HandleCreated;
                nud.HandleCreated += NumericUpDown_HandleCreated;
                ApplyNativeTheme(nud, false);
                DetachNumericUpDownAdorner(nud);
                AttachNumericUpDownOverlay(nud);
            }
            else if (control is ThemedNumericUpDown themedNud)
            {
                themedNud.BackColor = Current.InputBackColorValue;
                themedNud.ForeColor = Current.TextColorValue;
                themedNud.BorderColor = Current.AccentColorValue;
                themedNud.ButtonBackColor = Current.AccentColorValue;
                themedNud.ButtonForeColor = Current.ButtonTextColorValue;
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
                lv.GridLines = false;
                lv.OwnerDraw = true;
                lv.DrawColumnHeader -= ListView_DrawColumnHeader;
                lv.DrawColumnHeader += ListView_DrawColumnHeader;
                lv.DrawItem -= ListView_DrawItem;
                lv.DrawItem += ListView_DrawItem;
                lv.DrawSubItem -= ListView_DrawSubItem;
                lv.DrawSubItem += ListView_DrawSubItem;
                lv.HandleCreated -= ListView_HandleCreated;
                lv.HandleCreated += ListView_HandleCreated;
                ApplyNativeTheme(lv, true);
                ApplyListViewNativeColors(lv);
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

        private static void RefreshNumericUpDownOverlays(Control root)
        {
            if (root is NumericUpDown nud)
            {
                AttachNumericUpDownOverlay(nud);
                if (NumericUpDownOverlays.TryGetValue(nud, out var overlay))
                {
                    overlay.RefreshBounds();
                    overlay.BringToFront();
                    overlay.Invalidate();
                }
            }

            foreach (Control child in root.Controls)
            {
                RefreshNumericUpDownOverlays(child);
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

        private static void ListView_HandleCreated(object sender, EventArgs e)
        {
            var lv = (ListView)sender;
            ApplyNativeTheme(lv, !Current.IsBasic);
            if (!Current.IsBasic)
            {
                ApplyListViewNativeColors(lv);
            }
        }

        private static void NumericUpDown_HandleCreated(object sender, EventArgs e)
        {
            // Keep native visual style enabled for NumericUpDown to avoid
            // VisualStyleRenderer failures in UpDownButtons.OnPaint.
            var nud = (NumericUpDown)sender;
            ApplyNativeTheme(nud, false);
            if (!Current.IsBasic)
            {
                nud.BackColor = Current.InputBackColorValue;
                nud.ForeColor = Current.TextColorValue;
                StyleNumericUpDownChildren(nud);
                if (NumericUpDownOverlays.TryGetValue(nud, out var overlay))
                {
                    overlay.RefreshBounds();
                    overlay.Invalidate();
                }
            }

            // Child edit/buttons can be created slightly after the parent handle.
            if (!nud.IsDisposed)
            {
                nud.BeginInvoke(
                    new Action(() =>
                    {
                        if (nud.IsDisposed)
                        {
                            return;
                        }

                        nud.BackColor = Current.InputBackColorValue;
                        nud.ForeColor = Current.TextColorValue;
                        StyleNumericUpDownChildren(nud);
                        if (NumericUpDownOverlays.TryGetValue(nud, out var overlay))
                        {
                            overlay.RefreshBounds();
                            overlay.BringToFront();
                            overlay.Invalidate();
                        }
                    })
                );
            }
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

        private static void RegisterNumericUpDownHooks(NumericUpDown nud)
        {
            if (NumericUpDownStyled.Contains(nud))
            {
                return;
            }

            nud.ControlAdded += NumericUpDown_ControlAdded;
            nud.Disposed += NumericUpDown_Disposed;
            NumericUpDownStyled.Add(nud);
        }

        private static void NumericUpDown_Disposed(object sender, EventArgs e)
        {
            if (sender is NumericUpDown nud)
            {
                nud.ControlAdded -= NumericUpDown_ControlAdded;
                nud.Disposed -= NumericUpDown_Disposed;
                NumericUpDownStyled.Remove(nud);
            }
        }

        private static void NumericUpDown_ControlAdded(object sender, ControlEventArgs e)
        {
            if (sender is NumericUpDown nud)
            {
                StyleNumericUpDownChild(e.Control);
                if (NumericUpDownOverlays.TryGetValue(nud, out var overlay))
                {
                    overlay.RefreshBounds();
                    overlay.BringToFront();
                    overlay.Invalidate();
                }
            }
        }

        private static void StyleNumericUpDownChildren(NumericUpDown nud)
        {
            foreach (Control child in nud.Controls)
            {
                StyleNumericUpDownChild(child);
            }
        }

        private static void StyleNumericUpDownChild(Control child)
        {
            child.BackColor = Current.InputBackColorValue;
            child.ForeColor = Current.TextColorValue;

            if (child is TextBoxBase tb)
            {
                tb.BorderStyle = BorderStyle.None;
            }
        }

        private static IntPtr FindUpDownChildHandle(IntPtr parentHandle)
        {
            IntPtr found = IntPtr.Zero;
            IntPtr firstChild = IntPtr.Zero;
            EnumChildWindows(
                parentHandle,
                (child, lParam) =>
                {
                    if (firstChild == IntPtr.Zero)
                    {
                        firstChild = child;
                    }

                    var className = new System.Text.StringBuilder(64);
                    _ = GetClassName(child, className, className.Capacity);
                    string name = className.ToString();
                    if (
                        string.Equals(name, UpDownClassName, StringComparison.OrdinalIgnoreCase)
                        || name.IndexOf("updown", StringComparison.OrdinalIgnoreCase) >= 0
                    )
                    {
                        found = child;
                        return false;
                    }

                    return true;
                },
                IntPtr.Zero
            );
            return found != IntPtr.Zero ? found : firstChild;
        }

        private static IntPtr ToColorRef(Color color)
        {
            int colorRef = color.R | (color.G << 8) | (color.B << 16);
            return (IntPtr)colorRef;
        }

        private static void ApplyListViewNativeColors(ListView lv)
        {
            if (!lv.IsHandleCreated)
            {
                return;
            }

            IntPtr back = ToColorRef(Current.InputBackColorValue);
            IntPtr text = ToColorRef(Current.TextColorValue);

            _ = SendMessage(lv.Handle, LvmSetBkColor, IntPtr.Zero, back);
            _ = SendMessage(lv.Handle, LvmSetTextBkColor, IntPtr.Zero, back);
            _ = SendMessage(lv.Handle, LvmSetTextColor, IntPtr.Zero, text);

            IntPtr headerHandle = SendMessage(lv.Handle, LvmGetHeader, IntPtr.Zero, IntPtr.Zero);
            if (headerHandle != IntPtr.Zero)
            {
                SetWindowTheme(headerHandle, string.Empty, string.Empty);
            }
        }

        private static void DrawSpinButtonsHandle(IntPtr spinHandle)
        {
            if (spinHandle == IntPtr.Zero || !GetClientRect(spinHandle, out var rc))
            {
                return;
            }

            int width = Math.Max(0, rc.Right - rc.Left);
            int height = Math.Max(0, rc.Bottom - rc.Top);
            if (width == 0 || height == 0)
            {
                return;
            }

            var buttonRect = new Rectangle(0, 0, width, height);
            int midY = buttonRect.Top + (buttonRect.Height / 2);
            Color borderColor = ControlPaint.Dark(Current.AccentColorValue, 0.2f);

            using (Graphics g = Graphics.FromHwnd(spinHandle))
            {
                using (var accentBrush = new SolidBrush(Current.AccentColorValue))
                using (var borderPen = new Pen(borderColor))
                using (var splitPen = new Pen(Current.ButtonTextColorValue))
                {
                    g.FillRectangle(accentBrush, buttonRect);
                    g.DrawRectangle(borderPen, buttonRect.X, buttonRect.Y, buttonRect.Width - 1, buttonRect.Height - 1);
                    g.DrawLine(splitPen, buttonRect.Left + 2, midY, buttonRect.Right - 3, midY);
                }

                int cx = buttonRect.Left + (buttonRect.Width / 2);
                Point[] upArrow = { new Point(cx - 3, buttonRect.Top + 6), new Point(cx + 3, buttonRect.Top + 6), new Point(cx, buttonRect.Top + 3) };
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

        private static void AttachNumericUpDownOverlay(NumericUpDown nud)
        {
            if (NumericUpDownOverlays.ContainsKey(nud))
            {
                NumericUpDownOverlays[nud].RefreshBounds();
                return;
            }

            var overlay = new NumericUpDownOverlay(nud);
            NumericUpDownOverlays[nud] = overlay;
        }

        private static void DetachNumericUpDownOverlay(NumericUpDown nud)
        {
            if (!NumericUpDownOverlays.TryGetValue(nud, out var overlay))
            {
                return;
            }

            overlay.Dispose();
            NumericUpDownOverlays.Remove(nud);
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

        private sealed class NumericUpDownOverlay : Control
        {
            private readonly NumericUpDown _nud;

            public NumericUpDownOverlay(NumericUpDown nud)
            {
                _nud = nud;
                SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
                Enabled = true;
                Cursor = Cursors.Hand;
                TabStop = false;
                Visible = false;

                _nud.ParentChanged += NUD_LayoutChanged;
                _nud.LocationChanged += NUD_LayoutChanged;
                _nud.SizeChanged += NUD_LayoutChanged;
                _nud.VisibleChanged += NUD_LayoutChanged;
                _nud.Disposed += NUD_Disposed;

                AttachToParent();
                RefreshBounds();
            }

            private void NUD_Disposed(object sender, EventArgs e)
            {
                Dispose();
            }

            private void NUD_LayoutChanged(object sender, EventArgs e)
            {
                AttachToParent();
                RefreshBounds();
            }

            private void AttachToParent()
            {
                if (_nud.Parent == null)
                {
                    return;
                }

                if (Parent != _nud.Parent)
                {
                    Parent = _nud.Parent;
                    BringToFront();
                }
            }

            public void RefreshBounds()
            {
                if (_nud.Parent == null || !_nud.Visible || Current.IsBasic)
                {
                    Visible = false;
                    return;
                }

                int width = Math.Max(28, Math.Min(34, _nud.Width - 6));
                int x = _nud.Left + _nud.Width - width;
                int y = _nud.Top;
                int height = Math.Max(0, _nud.Height);
                Bounds = new Rectangle(x, y, width, height);
                Visible = true;
                BringToFront();
                Invalidate();
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                if (Width <= 0 || Height <= 0 || Current.IsBasic)
                {
                    return;
                }

                Rectangle rect = ClientRectangle;
                Rectangle inner = Rectangle.Inflate(rect, -1, -1);
                if (inner.Width <= 0 || inner.Height <= 0)
                {
                    return;
                }

                int midY = inner.Top + (inner.Height / 2);
                Color borderColor = ControlPaint.Dark(Current.AccentColorValue, 0.2f);

                using (var seamPen = new Pen(Current.ControlBackColorValue))
                {
                    e.Graphics.DrawLine(seamPen, rect.Left, rect.Top, rect.Left, rect.Bottom - 1);
                }

                using (var accentBrush = new SolidBrush(Current.AccentColorValue))
                using (var borderPen = new Pen(borderColor))
                using (var splitPen = new Pen(Current.ButtonTextColorValue))
                {
                    e.Graphics.FillRectangle(accentBrush, inner);
                    e.Graphics.DrawRectangle(borderPen, inner.X, inner.Y, inner.Width - 1, inner.Height - 1);
                    e.Graphics.DrawLine(splitPen, inner.Left + 2, midY, inner.Right - 3, midY);
                }

                int cx = inner.Left + (inner.Width / 2);
                int upBaseY = inner.Top + 8;
                int upTipY = inner.Top + 3;
                int downBaseY = inner.Bottom - 9;
                int downTipY = inner.Bottom - 4;
                Point[] upArrow = { new Point(cx - 5, upBaseY), new Point(cx + 5, upBaseY), new Point(cx, upTipY) };
                Point[] downArrow = { new Point(cx - 5, downBaseY), new Point(cx + 5, downBaseY), new Point(cx, downTipY) };
                using (var arrowBrush = new SolidBrush(Current.ButtonTextColorValue))
                {
                    e.Graphics.FillPolygon(arrowBrush, upArrow);
                    e.Graphics.FillPolygon(arrowBrush, downArrow);
                }
            }

            protected override void OnMouseDown(MouseEventArgs e)
            {
                base.OnMouseDown(e);

                if (!_nud.Enabled)
                {
                    return;
                }

                if (e.Y < Height / 2)
                {
                    _nud.UpButton();
                }
                else
                {
                    _nud.DownButton();
                }
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    _nud.ParentChanged -= NUD_LayoutChanged;
                    _nud.LocationChanged -= NUD_LayoutChanged;
                    _nud.SizeChanged -= NUD_LayoutChanged;
                    _nud.VisibleChanged -= NUD_LayoutChanged;
                    _nud.Disposed -= NUD_Disposed;
                }

                base.Dispose(disposing);
            }
        }

        private sealed class NumericUpDownAdorner : NativeWindow, IDisposable
        {
            private readonly NumericUpDown _nud;
            private IntPtr _spinHandle = IntPtr.Zero;
            private SpinButtonsWindow? _spinWindow;

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
                    EnsureSpinWindow();
                }
            }

            private void NUD_HandleDestroyed(object sender, EventArgs e)
            {
                ReleaseSpinWindow();
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

                EnsureSpinWindow();

                using (Graphics g = Graphics.FromHwnd(_nud.Handle))
                using (var outerPen = new Pen(Current.ControlBackColorValue))
                {
                    g.DrawRectangle(outerPen, 0, 0, Math.Max(0, _nud.Width - 1), Math.Max(0, _nud.Height - 1));
                }
            }

            private void EnsureSpinWindow()
            {
                if (!_nud.IsHandleCreated)
                {
                    return;
                }

                IntPtr handle = FindUpDownChildHandle(_nud.Handle);
                if (handle == IntPtr.Zero)
                {
                    return;
                }

                if (handle == _spinHandle && _spinWindow != null)
                {
                    return;
                }

                ReleaseSpinWindow();
                SetWindowTheme(handle, string.Empty, string.Empty);
                _spinHandle = handle;
                _spinWindow = new SpinButtonsWindow(handle);
                DrawSpinButtonsHandle(handle);
            }

            private void ReleaseSpinWindow()
            {
                _spinWindow?.Dispose();
                _spinWindow = null;
                _spinHandle = IntPtr.Zero;
            }

            public void Dispose()
            {
                ReleaseSpinWindow();
                _nud.HandleCreated -= NUD_HandleCreated;
                _nud.HandleDestroyed -= NUD_HandleDestroyed;
                _nud.Disposed -= NUD_Disposed;
                ReleaseHandle();
            }

            private sealed class SpinButtonsWindow : NativeWindow, IDisposable
            {
                public SpinButtonsWindow(IntPtr handle)
                {
                    AssignHandle(handle);
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
                        DrawSpinButtonsHandle(Handle);
                    }
                }

                public void Dispose()
                {
                    ReleaseHandle();
                }
            }
        }
    }
}
