namespace Pokebot
{
    partial class Pokebot
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._statusLabel = new System.Windows.Forms.Label();
            this._accelerateCheckbox = new System.Windows.Forms.CheckBox();
            this._tabControl = new System.Windows.Forms.TabControl();
            this._tabSettingsPage = new System.Windows.Forms.TabPage();
            this._delayLabel = new System.Windows.Forms.Label();
            this._delayUpDown = new System.Windows.Forms.NumericUpDown();
            this._discordWebhookLabel = new System.Windows.Forms.Label();
            this._discordWebhookText = new System.Windows.Forms.TextBox();
            this._injectSeedButton = new System.Windows.Forms.Button();
            this._seedText = new System.Windows.Forms.NumericUpDown();
            this._pauseCheckbox = new System.Windows.Forms.CheckBox();
            this._soundCheckbox = new System.Windows.Forms.CheckBox();
            this._versionLabel = new System.Windows.Forms.Label();
            this._delayTooltip = new System.Windows.Forms.ToolTip(this.components);
            this._newVersionLabel = new System.Windows.Forms.Label();
            this._tabControl.SuspendLayout();
            this._tabSettingsPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._delayUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._seedText)).BeginInit();
            this.SuspendLayout();
            // 
            // _statusLabel
            // 
            this._statusLabel.AutoSize = true;
            this._statusLabel.Location = new System.Drawing.Point(12, 37);
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Size = new System.Drawing.Size(105, 16);
            this._statusLabel.TabIndex = 0;
            this._statusLabel.Text = "No ROM loaded";
            // 
            // _accelerateCheckbox
            // 
            this._accelerateCheckbox.AutoSize = true;
            this._accelerateCheckbox.Location = new System.Drawing.Point(6, 6);
            this._accelerateCheckbox.Name = "_accelerateCheckbox";
            this._accelerateCheckbox.Size = new System.Drawing.Size(70, 20);
            this._accelerateCheckbox.TabIndex = 2;
            this._accelerateCheckbox.Text = "Speed";
            this._accelerateCheckbox.UseVisualStyleBackColor = true;
            this._accelerateCheckbox.CheckedChanged += new System.EventHandler(this.AccelerateCheckChanged);
            // 
            // _tabControl
            // 
            this._tabControl.Controls.Add(this._tabSettingsPage);
            this._tabControl.Location = new System.Drawing.Point(12, 68);
            this._tabControl.Name = "_tabControl";
            this._tabControl.SelectedIndex = 0;
            this._tabControl.Size = new System.Drawing.Size(776, 370);
            this._tabControl.TabIndex = 3;
            // 
            // _tabSettingsPage
            // 
            this._tabSettingsPage.Controls.Add(this._delayLabel);
            this._tabSettingsPage.Controls.Add(this._delayUpDown);
            this._tabSettingsPage.Controls.Add(this._discordWebhookLabel);
            this._tabSettingsPage.Controls.Add(this._discordWebhookText);
            this._tabSettingsPage.Controls.Add(this._injectSeedButton);
            this._tabSettingsPage.Controls.Add(this._seedText);
            this._tabSettingsPage.Controls.Add(this._pauseCheckbox);
            this._tabSettingsPage.Controls.Add(this._soundCheckbox);
            this._tabSettingsPage.Controls.Add(this._accelerateCheckbox);
            this._tabSettingsPage.Location = new System.Drawing.Point(4, 25);
            this._tabSettingsPage.Name = "_tabSettingsPage";
            this._tabSettingsPage.Padding = new System.Windows.Forms.Padding(3);
            this._tabSettingsPage.Size = new System.Drawing.Size(768, 341);
            this._tabSettingsPage.TabIndex = 0;
            this._tabSettingsPage.Text = "Settings";
            this._tabSettingsPage.UseVisualStyleBackColor = true;
            // 
            // _delayLabel
            // 
            this._delayLabel.AutoSize = true;
            this._delayLabel.Location = new System.Drawing.Point(132, 143);
            this._delayLabel.Name = "_delayLabel";
            this._delayLabel.Size = new System.Drawing.Size(219, 16);
            this._delayLabel.TabIndex = 11;
            this._delayLabel.Text = "Seconds of delay between bot input";
            // 
            // _delayUpDown
            // 
            this._delayUpDown.DecimalPlaces = 1;
            this._delayUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this._delayUpDown.Location = new System.Drawing.Point(6, 141);
            this._delayUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this._delayUpDown.Name = "_delayUpDown";
            this._delayUpDown.Size = new System.Drawing.Size(120, 22);
            this._delayUpDown.TabIndex = 10;
            this._delayUpDown.ValueChanged += new System.EventHandler(this.DelayUpDownChanged);
            // 
            // _discordWebhookLabel
            // 
            this._discordWebhookLabel.AutoSize = true;
            this._discordWebhookLabel.Location = new System.Drawing.Point(253, 116);
            this._discordWebhookLabel.Name = "_discordWebhookLabel";
            this._discordWebhookLabel.Size = new System.Drawing.Size(116, 16);
            this._discordWebhookLabel.TabIndex = 9;
            this._discordWebhookLabel.Text = "Discord Webhook";
            // 
            // _discordWebhookText
            // 
            this._discordWebhookText.Location = new System.Drawing.Point(6, 113);
            this._discordWebhookText.Name = "_discordWebhookText";
            this._discordWebhookText.Size = new System.Drawing.Size(241, 22);
            this._discordWebhookText.TabIndex = 8;
            this._discordWebhookText.TextChanged += new System.EventHandler(this.DiscordWebhookTextChanged);
            // 
            // _injectSeedButton
            // 
            this._injectSeedButton.Location = new System.Drawing.Point(132, 84);
            this._injectSeedButton.Name = "_injectSeedButton";
            this._injectSeedButton.Size = new System.Drawing.Size(115, 23);
            this._injectSeedButton.TabIndex = 7;
            this._injectSeedButton.Text = "Inject Seed";
            this._injectSeedButton.UseVisualStyleBackColor = true;
            this._injectSeedButton.Click += new System.EventHandler(this.InjectSeedClicked);
            // 
            // _seedText
            // 
            this._seedText.Location = new System.Drawing.Point(6, 84);
            this._seedText.Name = "_seedText";
            this._seedText.Size = new System.Drawing.Size(120, 22);
            this._seedText.TabIndex = 6;
            // 
            // _pauseCheckbox
            // 
            this._pauseCheckbox.AutoSize = true;
            this._pauseCheckbox.Location = new System.Drawing.Point(6, 58);
            this._pauseCheckbox.Name = "_pauseCheckbox";
            this._pauseCheckbox.Size = new System.Drawing.Size(68, 20);
            this._pauseCheckbox.TabIndex = 5;
            this._pauseCheckbox.Text = "Pause";
            this._pauseCheckbox.UseVisualStyleBackColor = true;
            this._pauseCheckbox.CheckedChanged += new System.EventHandler(this.PauseCheckboxChanged);
            // 
            // _soundCheckbox
            // 
            this._soundCheckbox.AutoSize = true;
            this._soundCheckbox.Location = new System.Drawing.Point(6, 32);
            this._soundCheckbox.Name = "_soundCheckbox";
            this._soundCheckbox.Size = new System.Drawing.Size(68, 20);
            this._soundCheckbox.TabIndex = 4;
            this._soundCheckbox.Text = "Sound";
            this._soundCheckbox.UseVisualStyleBackColor = true;
            this._soundCheckbox.CheckedChanged += new System.EventHandler(this.SoundCheckboxChanged);
            // 
            // _versionLabel
            // 
            this._versionLabel.AutoSize = true;
            this._versionLabel.Location = new System.Drawing.Point(12, 9);
            this._versionLabel.Name = "_versionLabel";
            this._versionLabel.Size = new System.Drawing.Size(68, 16);
            this._versionLabel.TabIndex = 3;
            this._versionLabel.Text = "Pokebot v";
            // 
            // _delayTooltip
            // 
            this._delayTooltip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // _newVersionLabel
            // 
            this._newVersionLabel.AutoSize = true;
            this._newVersionLabel.ForeColor = System.Drawing.Color.Red;
            this._newVersionLabel.Location = new System.Drawing.Point(148, 9);
            this._newVersionLabel.Name = "_newVersionLabel";
            this._newVersionLabel.Size = new System.Drawing.Size(117, 16);
            this._newVersionLabel.TabIndex = 4;
            this._newVersionLabel.Text = "new version check";
            // 
            // Pokebot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this._newVersionLabel);
            this.Controls.Add(this._tabControl);
            this.Controls.Add(this._versionLabel);
            this.Controls.Add(this._statusLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Pokebot";
            this._tabControl.ResumeLayout(false);
            this._tabSettingsPage.ResumeLayout(false);
            this._tabSettingsPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._delayUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._seedText)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _statusLabel;
        private System.Windows.Forms.CheckBox _accelerateCheckbox;
        private System.Windows.Forms.TabControl _tabControl;
        private System.Windows.Forms.TabPage _tabSettingsPage;
        private System.Windows.Forms.Label _versionLabel;
        private System.Windows.Forms.CheckBox _soundCheckbox;
        private System.Windows.Forms.CheckBox _pauseCheckbox;
        private System.Windows.Forms.Button _injectSeedButton;
        private System.Windows.Forms.NumericUpDown _seedText;
        private System.Windows.Forms.TextBox _discordWebhookText;
        private System.Windows.Forms.Label _discordWebhookLabel;
        private System.Windows.Forms.Label _delayLabel;
        private System.Windows.Forms.NumericUpDown _delayUpDown;
        private System.Windows.Forms.ToolTip _delayTooltip;
        private System.Windows.Forms.Label _newVersionLabel;
    }
}