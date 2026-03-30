using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Pokebot.Controls
{
    [DefaultEvent("ValueChanged")]
    public class ThemedNumericUpDown : UserControl, ISupportInitialize
    {
        private readonly TextBox _textBox;
        private readonly Button _upButton;
        private readonly Button _downButton;

        private decimal _minimum = 0m;
        private decimal _maximum = 100m;
        private decimal _increment = 1m;
        private decimal _value = 0m;
        private int _decimalPlaces = 0;
        private bool _internalTextUpdate;
        private Color _borderColor = SystemColors.WindowFrame;
        private Color _buttonBackColor = SystemColors.Control;
        private Color _buttonForeColor = SystemColors.ControlText;

        public event EventHandler? ValueChanged;

        public ThemedNumericUpDown()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = SystemColors.Window;
            ForeColor = SystemColors.WindowText;
            Size = new Size(60, 20);
            MinimumSize = new Size(40, 20);
            TabStop = true;

            _textBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Location = new Point(3, 3),
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right,
                TextAlign = HorizontalAlignment.Left
            };

            _upButton = new Button
            {
                Text = string.Empty,
                FlatStyle = FlatStyle.Flat,
                TabStop = false
            };
            _upButton.FlatAppearance.BorderSize = 0;

            _downButton = new Button
            {
                Text = string.Empty,
                FlatStyle = FlatStyle.Flat,
                TabStop = false
            };
            _downButton.FlatAppearance.BorderSize = 0;

            Controls.Add(_textBox);
            Controls.Add(_upButton);
            Controls.Add(_downButton);

            _upButton.Click += (_, __) => Step(+1);
            _downButton.Click += (_, __) => Step(-1);
            _upButton.Paint += UpButton_Paint;
            _downButton.Paint += DownButton_Paint;
            _textBox.Leave += (_, __) => CommitText();
            _textBox.KeyDown += TextBox_KeyDown;
            _textBox.TextChanged += TextBox_TextChanged;

            LayoutChildren();
            ApplyButtonColors();
            UpdateTextFromValue();
        }

        [DefaultValue(0)]
        public int DecimalPlaces
        {
            get => _decimalPlaces;
            set
            {
                int next = Math.Max(0, Math.Min(8, value));
                if (_decimalPlaces == next)
                {
                    return;
                }

                _decimalPlaces = next;
                Value = Clamp(_value);
                UpdateTextFromValue();
            }
        }

        [DefaultValue(typeof(decimal), "1")]
        public decimal Increment
        {
            get => _increment;
            set
            {
                if (value <= 0m)
                {
                    value = 1m;
                }

                _increment = value;
            }
        }

        [DefaultValue(typeof(decimal), "0")]
        public decimal Minimum
        {
            get => _minimum;
            set
            {
                _minimum = value;
                if (_maximum < _minimum)
                {
                    _maximum = _minimum;
                }

                Value = Clamp(_value);
            }
        }

        [DefaultValue(typeof(decimal), "100")]
        public decimal Maximum
        {
            get => _maximum;
            set
            {
                _maximum = value;
                if (_minimum > _maximum)
                {
                    _minimum = _maximum;
                }

                Value = Clamp(_value);
            }
        }

        [DefaultValue(typeof(decimal), "0")]
        public decimal Value
        {
            get => _value;
            set
            {
                decimal next = Clamp(Round(value));
                if (_value == next)
                {
                    UpdateTextFromValue();
                    return;
                }

                _value = next;
                UpdateTextFromValue();
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        [Browsable(true)]
        [DefaultValue(typeof(Color), "WindowFrame")]
        public Color BorderColor
        {
            get => _borderColor;
            set
            {
                if (_borderColor == value)
                {
                    return;
                }

                _borderColor = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [DefaultValue(typeof(Color), "Control")]
        public Color ButtonBackColor
        {
            get => _buttonBackColor;
            set
            {
                if (_buttonBackColor == value)
                {
                    return;
                }

                _buttonBackColor = value;
                ApplyButtonColors();
            }
        }

        [Browsable(true)]
        [DefaultValue(typeof(Color), "ControlText")]
        public Color ButtonForeColor
        {
            get => _buttonForeColor;
            set
            {
                if (_buttonForeColor == value)
                {
                    return;
                }

                _buttonForeColor = value;
                ApplyButtonColors();
            }
        }

        public void BeginInit() { }

        public void EndInit()
        {
            Value = Clamp(_value);
            LayoutChildren();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            LayoutChildren();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            if (_textBox == null || _upButton == null || _downButton == null)
            {
                return;
            }

            _textBox.Enabled = Enabled;
            _upButton.Enabled = Enabled;
            _downButton.Enabled = Enabled;
            ApplyButtonColors();
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            if (_textBox == null)
            {
                return;
            }

            _textBox.BackColor = BackColor;
            Invalidate();
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            if (_textBox == null)
            {
                return;
            }

            _textBox.ForeColor = ForeColor;
            Invalidate();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            if (_textBox == null || _upButton == null || _downButton == null)
            {
                return;
            }

            _textBox.Font = Font;
            _upButton.Font = Font;
            _downButton.Font = Font;
            LayoutChildren();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (var borderPen = new Pen(_borderColor))
            {
                e.Graphics.DrawRectangle(borderPen, 0, 0, Math.Max(0, Width - 1), Math.Max(0, Height - 1));
            }
        }

        private void TextBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                Step(+1);
                e.Handled = true;
                return;
            }

            if (e.KeyCode == Keys.Down)
            {
                Step(-1);
                e.Handled = true;
                return;
            }

            if (e.KeyCode == Keys.Enter)
            {
                CommitText();
                e.Handled = true;
            }
        }

        private void TextBox_TextChanged(object? sender, EventArgs e)
        {
            if (_internalTextUpdate)
            {
                return;
            }
        }

        private void Step(int direction)
        {
            decimal delta = direction > 0 ? _increment : -_increment;
            Value += delta;
        }

        private void CommitText()
        {
            if (_internalTextUpdate)
            {
                return;
            }

            if (TryParseText(_textBox.Text, out decimal parsed))
            {
                Value = parsed;
                return;
            }

            UpdateTextFromValue();
        }

        private bool TryParseText(string text, out decimal value)
        {
            if (decimal.TryParse(text, NumberStyles.Number, CultureInfo.CurrentCulture, out value))
            {
                value = Clamp(Round(value));
                return true;
            }

            if (decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out value))
            {
                value = Clamp(Round(value));
                return true;
            }

            value = _value;
            return false;
        }

        private decimal Clamp(decimal input)
        {
            if (input < _minimum)
            {
                return _minimum;
            }

            if (input > _maximum)
            {
                return _maximum;
            }

            return input;
        }

        private decimal Round(decimal value)
        {
            return Math.Round(value, _decimalPlaces, MidpointRounding.AwayFromZero);
        }

        private void UpdateTextFromValue()
        {
            _internalTextUpdate = true;
            _textBox.Text = _value.ToString("F" + _decimalPlaces, CultureInfo.CurrentCulture);
            _internalTextUpdate = false;
        }

        private void LayoutChildren()
        {
            if (_textBox == null || _upButton == null || _downButton == null)
            {
                return;
            }

            int buttonWidth = Math.Max(18, Width / 3);
            int buttonX = Width - buttonWidth - 1;
            int contentHeight = Math.Max(2, Height - 2);
            int topHeight = contentHeight / 2;
            int bottomHeight = contentHeight - topHeight;

            int textY = Math.Max(0, (Height - _textBox.PreferredHeight) / 2 - 2);
            _textBox.Location = new Point(0, textY);
            _textBox.Size = new Size(Math.Max(8, buttonX - 1), _textBox.PreferredHeight);

            _upButton.Location = new Point(buttonX, 1);
            _upButton.Size = new Size(buttonWidth, Math.Max(8, topHeight));

            _downButton.Location = new Point(buttonX, 1 + topHeight);
            _downButton.Size = new Size(buttonWidth, Math.Max(8, bottomHeight));
        }

        private void ApplyButtonColors()
        {
            if (_upButton == null || _downButton == null)
            {
                return;
            }

            Color back = Enabled ? _buttonBackColor : ControlPaint.Dark(_buttonBackColor, 0.2f);
            Color fore = Enabled ? _buttonForeColor : ControlPaint.Dark(_buttonForeColor, 0.2f);
            _upButton.BackColor = back;
            _downButton.BackColor = back;
            _upButton.ForeColor = fore;
            _downButton.ForeColor = fore;
        }

        private void UpButton_Paint(object? sender, PaintEventArgs e)
        {
            DrawArrow(e.Graphics, ((Button)sender!).ClientRectangle, true);
        }

        private void DownButton_Paint(object? sender, PaintEventArgs e)
        {
            DrawArrow(e.Graphics, ((Button)sender!).ClientRectangle, false);
        }

        private void DrawArrow(Graphics g, Rectangle rect, bool up)
        {
            int cx = rect.Left + (rect.Width / 2);
            int cy = rect.Top + (rect.Height / 2);
            int size = Math.Max(2, Math.Min(rect.Width, rect.Height) / 4);
            Color arrowColor = Enabled ? _buttonForeColor : ControlPaint.Dark(_buttonForeColor, 0.2f);

            Point[] points;
            if (up)
            {
                points = new[]
                {
                    new Point(cx - size, cy + (size / 2)),
                    new Point(cx + size, cy + (size / 2)),
                    new Point(cx, cy - size)
                };
            }
            else
            {
                points = new[]
                {
                    new Point(cx - size, cy - (size / 2)),
                    new Point(cx + size, cy - (size / 2)),
                    new Point(cx, cy + size)
                };
            }

            using (var brush = new SolidBrush(arrowColor))
            {
                g.FillPolygon(brush, points);
            }
        }
    }
}
