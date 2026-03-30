using Pokebot.Models;
using Pokebot.Theme;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pokebot.Panels
{
    public class ThemePanel : UserControl
    {
        private static readonly string[] PresetNames =
        {
            "Basic (System Default)",
            "Dark (Pokémon)",
            "Pikachu",
            "Gengar",
            "Dragonite",
            "Charizard",
            "Blastoise",
            "Venusaur",
            "Mew",
            "Mewtwo"
        };

        private static readonly string[] ColorFieldLabels =
        {
            "Background",        // 0  col1
            "Input Background",  // 1  col1
            "Text Color",        // 2  col1
            "Accent Color",      // 3  col1
            "Text Button Color", // 4  col2
            "Warning Color",     // 5  col2
            "Error Color",       // 6  col2
            "Success Color"      // 7  col2
        };

        private const int FieldCount = 8;

        // Fields 0-3 go in column 1, fields 4-7 go in column 2.
        private const int Col1Start = 6;
        private const int Col2Start = 294;
        private const int SwatchOffset = 130;
        private const int HexOffset = SwatchOffset + 24 + 4;

        private ComboBox _presetComboBox;
        private readonly Button[] _colorButtons = new Button[FieldCount];
        private readonly TextBox[] _hexInputs = new TextBox[FieldCount];
        private Button _applyButton;

        private bool _suppressUpdate;

        public ThemePanel()
        {
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            Margin = new Padding(2);
            Name = "ThemePanel";
            // Height: header(38) + 4 rows × 28px + 14px bottom padding
            Size = new Size(576, 174);

            BuildLayout();
            LoadFromCurrent();
        }

        private void BuildLayout()
        {
            // ── Row 1: Preset selector + Apply button (inline) ──────────
            Controls.Add(new Label { Text = "Preset:", AutoSize = true, Location = new Point(6, 11) });

            _presetComboBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                FormattingEnabled = true,
                Location = new Point(56, 8),
                Size = new Size(160, 21),
                Name = "_presetComboBox"
            };
            _presetComboBox.Items.AddRange(PresetNames);
            _presetComboBox.SelectedIndexChanged += PresetComboBox_SelectedIndexChanged;
            Controls.Add(_presetComboBox);

            _applyButton = new Button
            {
                Text = "Apply",
                Size = new Size(70, 23),
                Location = new Point(224, 7)
            };
            _applyButton.Click += ApplyButton_Click;
            Controls.Add(_applyButton);

            // ── Separator ────────────────────────────────────────────────
            Controls.Add(new Panel
            {
                BackColor = SystemColors.ControlDark,
                Location = new Point(6, 38),
                Size = new Size(560, 1)
            });

            // ── Color rows (2-column layout) ─────────────────────────────
            const int rowStartY = 48;
            const int rowHeight = 28;

            for (int i = 0; i < FieldCount; i++)
            {
                bool isCol2 = i >= 4;
                int rowInCol = isCol2 ? i - 4 : i;
                int colX = isCol2 ? Col2Start : Col1Start;
                int y = rowStartY + rowInCol * rowHeight;

                Controls.Add(new Label
                {
                    Text = ColorFieldLabels[i] + ":",
                    AutoSize = true,
                    Location = new Point(colX, y + 5)
                });

                // Swatch button — Tag is an int so ThemeManager skips it.
                var btn = new Button
                {
                    Size = new Size(24, 24),
                    Location = new Point(colX + SwatchOffset, y),
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand,
                    Tag = i
                };
                btn.FlatAppearance.BorderColor = Color.Gray;
                btn.Click += ColorButton_Click;
                _colorButtons[i] = btn;
                Controls.Add(btn);

                var hex = new TextBox
                {
                    Size = new Size(80, 22),
                    Location = new Point(colX + HexOffset, y + 1),
                    MaxLength = 7,
                    Tag = i,
                    Font = new Font("Consolas", 8.25f)
                };
                hex.TextChanged += HexInput_TextChanged;
                _hexInputs[i] = hex;
                Controls.Add(hex);
            }
        }

        private void LoadFromCurrent()
        {
            _suppressUpdate = true;
            try
            {
                ThemeConfig t = ThemeManager.Current.IsBasic ? ThemeConfig.CreateDark() : ThemeManager.Current;

                SetColorRow(0, t.BackColorValue,       t.BackgroundColor);
                SetColorRow(1, t.InputBackColorValue,  t.InputBackgroundColor);
                SetColorRow(2, t.TextColorValue,       t.TextColor);
                SetColorRow(3, t.AccentColorValue,     t.AccentColor);
                SetColorRow(4, t.ButtonTextColorValue, t.ButtonTextColor);
                SetColorRow(5, t.WarningColorValue,    t.WarningColor);
                SetColorRow(6, t.ErrorColorValue,      t.ErrorColor);
                SetColorRow(7, t.SuccessColorValue,    t.SuccessColor);

                _presetComboBox.SelectedIndex = DetectPresetIndex(ThemeManager.Current);
            }
            finally
            {
                _suppressUpdate = false;
            }
        }

        /// <summary>Returns the preset index matching the current config, or -1 for custom.</summary>
        private static int DetectPresetIndex(ThemeConfig config)
        {
            if (config.IsBasic)
            {
                return 0;
            }

            for (int i = 1; i < PresetNames.Length; i++)
            {
                ThemeConfig preset = GetPresetConfig(i);
                if (preset != null && config.BackgroundColor == preset.BackgroundColor &&
                    config.AccentColor == preset.AccentColor)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>Returns the ThemeConfig for a preset index, or null for index 0 (Basic).</summary>
        private static ThemeConfig GetPresetConfig(int index)
        {
            switch (index)
            {
                case 1:  return ThemeConfig.CreateDark();
                case 2:  return ThemeConfig.CreatePikachu();
                case 3:  return ThemeConfig.CreateGengar();
                case 4:  return ThemeConfig.CreateDragonite();
                case 5:  return ThemeConfig.CreateCharizard();
                case 6:  return ThemeConfig.CreateBlastoise();
                case 7:  return ThemeConfig.CreateVenusaur();
                case 8:  return ThemeConfig.CreateMew();
                case 9:  return ThemeConfig.CreateMewtwo();
                default: return null;
            }
        }

        private void SetColorRow(int index, Color color, string hex)
        {
            _colorButtons[index].BackColor = color;
            _colorButtons[index].ForeColor = GetContrastColor(color);
            _colorButtons[index].FlatAppearance.BorderColor = Color.Gray;
            _hexInputs[index].Text = hex;
        }

        private void PresetComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressUpdate)
            {
                return;
            }

            int idx = _presetComboBox.SelectedIndex;
            if (idx <= 0)
            {
                return;
            }

            ThemeConfig preview = GetPresetConfig(idx);
            if (preview == null)
            {
                return;
            }

            _suppressUpdate = true;
            try
            {
                SetColorRow(0, preview.BackColorValue,      preview.BackgroundColor);
                SetColorRow(1, preview.InputBackColorValue, preview.InputBackgroundColor);
                SetColorRow(2, preview.TextColorValue,      preview.TextColor);
                SetColorRow(3, preview.AccentColorValue,    preview.AccentColor);
                SetColorRow(4, preview.ButtonTextColorValue, preview.ButtonTextColor);
                SetColorRow(5, preview.WarningColorValue,    preview.WarningColor);
                SetColorRow(6, preview.ErrorColorValue,      preview.ErrorColor);
                SetColorRow(7, preview.SuccessColorValue,    preview.SuccessColor);
            }
            finally
            {
                _suppressUpdate = false;
            }
        }

        private void ColorButton_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            int index = (int)btn.Tag;

            using (var dialog = new ColorDialog())
            {
                dialog.Color = btn.BackColor;
                dialog.FullOpen = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    SetColorRow(index, dialog.Color, ColorTranslator.ToHtml(dialog.Color));
                    _suppressUpdate = true;
                    try { _presetComboBox.SelectedIndex = -1; }
                    finally { _suppressUpdate = false; }
                }
            }
        }

        private void HexInput_TextChanged(object sender, EventArgs e)
        {
            if (_suppressUpdate)
            {
                return;
            }

            var txt = (TextBox)sender;
            int index = (int)txt.Tag;

            try
            {
                Color color = ColorTranslator.FromHtml(txt.Text.Trim());
                _suppressUpdate = true;
                try
                {
                    _colorButtons[index].BackColor = color;
                    _colorButtons[index].ForeColor = GetContrastColor(color);
                    _colorButtons[index].FlatAppearance.BorderColor = Color.Gray;
                    _presetComboBox.SelectedIndex = -1;
                }
                finally
                {
                    _suppressUpdate = false;
                }
            }
            catch
            {
                // Invalid hex — leave swatch as-is
            }
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            ThemeConfig config = _presetComboBox.SelectedIndex == 0
                ? ThemeConfig.CreateBasic()
                : BuildConfigFromFields();

            ThemeManager.Apply(config);

            // ThemeChanged → Pokebot.ApplyTo(this) ran and visited our swatch buttons.
            // Restore their correct display colors.
            LoadFromCurrent();
        }

        private ThemeConfig BuildConfigFromFields()
        {
            return new ThemeConfig
            {
                BackgroundColor =      _hexInputs[0].Text.Trim(),
                InputBackgroundColor = _hexInputs[1].Text.Trim(),
                TextColor =            _hexInputs[2].Text.Trim(),
                AccentColor =          _hexInputs[3].Text.Trim(),
                ButtonTextColor =      _hexInputs[4].Text.Trim(),
                WarningColor =         _hexInputs[5].Text.Trim(),
                ErrorColor =           _hexInputs[6].Text.Trim(),
                SuccessColor =         _hexInputs[7].Text.Trim()
            };
        }

        private static Color GetContrastColor(Color bg)
        {
            double lum = 0.299 * (bg.R / 255.0) + 0.587 * (bg.G / 255.0) + 0.114 * (bg.B / 255.0);
            return lum > 0.5 ? Color.Black : Color.White;
        }
    }
}
