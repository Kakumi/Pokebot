namespace Pokebot.Panels
{
    partial class SettingsPanel
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._delayLabel = new System.Windows.Forms.Label();
            this._delayUpDown = new System.Windows.Forms.NumericUpDown();
            this._discordWebhookLabel = new System.Windows.Forms.Label();
            this._discordWebhookText = new System.Windows.Forms.TextBox();
            this._pauseCheckbox = new System.Windows.Forms.CheckBox();
            this._soundCheckbox = new System.Windows.Forms.CheckBox();
            this._accelerateCheckbox = new System.Windows.Forms.CheckBox();
            this._delayTooltip = new System.Windows.Forms.ToolTip(this.components);
            this._discordUserIDLabel = new System.Windows.Forms.Label();
            this._discordUserID = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this._delayUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // _delayLabel
            // 
            this._delayLabel.AutoSize = true;
            this._delayLabel.Location = new System.Drawing.Point(128, 138);
            this._delayLabel.Name = "_delayLabel";
            this._delayLabel.Size = new System.Drawing.Size(219, 16);
            this._delayLabel.TabIndex = 20;
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
            this._delayUpDown.Location = new System.Drawing.Point(2, 136);
            this._delayUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this._delayUpDown.Name = "_delayUpDown";
            this._delayUpDown.Size = new System.Drawing.Size(120, 22);
            this._delayUpDown.TabIndex = 19;
            this._delayUpDown.ValueChanged += new System.EventHandler(this._delayUpDown_ValueChanged);
            // 
            // _discordWebhookLabel
            // 
            this._discordWebhookLabel.AutoSize = true;
            this._discordWebhookLabel.Location = new System.Drawing.Point(250, 84);
            this._discordWebhookLabel.Name = "_discordWebhookLabel";
            this._discordWebhookLabel.Size = new System.Drawing.Size(116, 16);
            this._discordWebhookLabel.TabIndex = 18;
            this._discordWebhookLabel.Text = "Discord Webhook";
            // 
            // _discordWebhookText
            // 
            this._discordWebhookText.Location = new System.Drawing.Point(3, 81);
            this._discordWebhookText.Name = "_discordWebhookText";
            this._discordWebhookText.Size = new System.Drawing.Size(241, 22);
            this._discordWebhookText.TabIndex = 17;
            this._discordWebhookText.TextChanged += new System.EventHandler(this._discordWebhookText_TextChanged);
            // 
            // _pauseCheckbox
            // 
            this._pauseCheckbox.AutoSize = true;
            this._pauseCheckbox.Location = new System.Drawing.Point(3, 55);
            this._pauseCheckbox.Name = "_pauseCheckbox";
            this._pauseCheckbox.Size = new System.Drawing.Size(68, 20);
            this._pauseCheckbox.TabIndex = 14;
            this._pauseCheckbox.Text = "Pause";
            this._pauseCheckbox.UseVisualStyleBackColor = true;
            this._pauseCheckbox.CheckedChanged += new System.EventHandler(this._pauseCheckbox_CheckedChanged);
            // 
            // _soundCheckbox
            // 
            this._soundCheckbox.AutoSize = true;
            this._soundCheckbox.Location = new System.Drawing.Point(3, 29);
            this._soundCheckbox.Name = "_soundCheckbox";
            this._soundCheckbox.Size = new System.Drawing.Size(68, 20);
            this._soundCheckbox.TabIndex = 13;
            this._soundCheckbox.Text = "Sound";
            this._soundCheckbox.UseVisualStyleBackColor = true;
            this._soundCheckbox.CheckedChanged += new System.EventHandler(this._soundCheckbox_CheckedChanged);
            // 
            // _accelerateCheckbox
            // 
            this._accelerateCheckbox.AutoSize = true;
            this._accelerateCheckbox.Location = new System.Drawing.Point(3, 3);
            this._accelerateCheckbox.Name = "_accelerateCheckbox";
            this._accelerateCheckbox.Size = new System.Drawing.Size(70, 20);
            this._accelerateCheckbox.TabIndex = 12;
            this._accelerateCheckbox.Text = "Speed";
            this._accelerateCheckbox.UseVisualStyleBackColor = true;
            this._accelerateCheckbox.CheckedChanged += new System.EventHandler(this._accelerateCheckbox_CheckedChanged);
            // 
            // _discordUserIDLabel
            // 
            this._discordUserIDLabel.AutoSize = true;
            this._discordUserIDLabel.Location = new System.Drawing.Point(250, 111);
            this._discordUserIDLabel.Name = "_discordUserIDLabel";
            this._discordUserIDLabel.Size = new System.Drawing.Size(173, 16);
            this._discordUserIDLabel.TabIndex = 22;
            this._discordUserIDLabel.Text = "Discord User ID (facultative)";
            // 
            // _discordUserID
            // 
            this._discordUserID.Location = new System.Drawing.Point(3, 108);
            this._discordUserID.Name = "_discordUserID";
            this._discordUserID.Size = new System.Drawing.Size(241, 22);
            this._discordUserID.TabIndex = 21;
            this._discordUserID.TextChanged += new System.EventHandler(this._discordUserID_TextChanged);
            // 
            // SettingsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._discordUserIDLabel);
            this.Controls.Add(this._discordUserID);
            this.Controls.Add(this._delayLabel);
            this.Controls.Add(this._delayUpDown);
            this.Controls.Add(this._discordWebhookLabel);
            this.Controls.Add(this._discordWebhookText);
            this.Controls.Add(this._pauseCheckbox);
            this.Controls.Add(this._soundCheckbox);
            this.Controls.Add(this._accelerateCheckbox);
            this.Name = "SettingsPanel";
            this.Size = new System.Drawing.Size(768, 341);
            ((System.ComponentModel.ISupportInitialize)(this._delayUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _delayLabel;
        private System.Windows.Forms.NumericUpDown _delayUpDown;
        private System.Windows.Forms.Label _discordWebhookLabel;
        private System.Windows.Forms.TextBox _discordWebhookText;
        private System.Windows.Forms.CheckBox _pauseCheckbox;
        private System.Windows.Forms.CheckBox _soundCheckbox;
        private System.Windows.Forms.CheckBox _accelerateCheckbox;
        private System.Windows.Forms.ToolTip _delayTooltip;
        private System.Windows.Forms.Label _discordUserIDLabel;
        private System.Windows.Forms.TextBox _discordUserID;
    }
}
