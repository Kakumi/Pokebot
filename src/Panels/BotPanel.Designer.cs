namespace Pokebot.Panels
{
    partial class BotPanel
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
            this._stopBotButton = new System.Windows.Forms.Button();
            this._statusBot = new System.Windows.Forms.Label();
            this._startBotButton = new System.Windows.Forms.Button();
            this._botPanel = new System.Windows.Forms.Panel();
            this._botComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // _stopBotButton
            // 
            this._stopBotButton.Location = new System.Drawing.Point(175, 5);
            this._stopBotButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._stopBotButton.Name = "_stopBotButton";
            this._stopBotButton.Size = new System.Drawing.Size(70, 23);
            this._stopBotButton.TabIndex = 8;
            this._stopBotButton.Text = "Stop";
            this._stopBotButton.UseVisualStyleBackColor = true;
            this._stopBotButton.Click += new System.EventHandler(this._stopBotButton_Click);
            // 
            // _statusBot
            // 
            this._statusBot.AutoSize = true;
            this._statusBot.Location = new System.Drawing.Point(4, 27);
            this._statusBot.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._statusBot.Name = "_statusBot";
            this._statusBot.Size = new System.Drawing.Size(52, 13);
            this._statusBot.TabIndex = 4;
            this._statusBot.Text = "botStatus";
            // 
            // _startBotButton
            // 
            this._startBotButton.Location = new System.Drawing.Point(100, 5);
            this._startBotButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._startBotButton.Name = "_startBotButton";
            this._startBotButton.Size = new System.Drawing.Size(70, 23);
            this._startBotButton.TabIndex = 7;
            this._startBotButton.Text = "Start";
            this._startBotButton.UseVisualStyleBackColor = true;
            this._startBotButton.Click += new System.EventHandler(this._startBotButton_Click);
            // 
            // _botPanel
            // 
            this._botPanel.Location = new System.Drawing.Point(4, 42);
            this._botPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._botPanel.Name = "_botPanel";
            this._botPanel.Size = new System.Drawing.Size(567, 230);
            this._botPanel.TabIndex = 6;
            // 
            // _botComboBox
            // 
            this._botComboBox.FormattingEnabled = true;
            this._botComboBox.Location = new System.Drawing.Point(4, 5);
            this._botComboBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._botComboBox.Name = "_botComboBox";
            this._botComboBox.Size = new System.Drawing.Size(92, 21);
            this._botComboBox.TabIndex = 5;
            this._botComboBox.SelectedIndexChanged += new System.EventHandler(this._botComboBox_SelectedIndexChanged);
            // 
            // BotPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._stopBotButton);
            this.Controls.Add(this._statusBot);
            this.Controls.Add(this._startBotButton);
            this.Controls.Add(this._botPanel);
            this.Controls.Add(this._botComboBox);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "BotPanel";
            this.Size = new System.Drawing.Size(576, 277);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _stopBotButton;
        private System.Windows.Forms.Label _statusBot;
        private System.Windows.Forms.Button _startBotButton;
        private System.Windows.Forms.Panel _botPanel;
        private System.Windows.Forms.ComboBox _botComboBox;
    }
}
