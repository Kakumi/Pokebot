namespace Pokebot.Panels
{
    partial class PokeFinderControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PokeFinderControl));
            this._labelBotInfo = new System.Windows.Forms.Label();
            this._labelInputFrame = new System.Windows.Forms.Label();
            this._inputFrame = new System.Windows.Forms.NumericUpDown();
            this._inputPID = new System.Windows.Forms.TextBox();
            this._labelPID = new System.Windows.Forms.Label();
            this._labelTIDSID = new System.Windows.Forms.Label();
            this._hitFrameField = new System.Windows.Forms.NumericUpDown();
            this._hitFrameLabel = new System.Windows.Forms.Label();
            this._retryButton = new System.Windows.Forms.Button();
            this._frameLabel = new System.Windows.Forms.Label();
            this._pokefinderTypesComboBox = new System.Windows.Forms.ComboBox();
            this._pokefinderTypeLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._inputFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._hitFrameField)).BeginInit();
            this.SuspendLayout();
            // 
            // _labelBotInfo
            // 
            this._labelBotInfo.AutoSize = true;
            this._labelBotInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelBotInfo.Location = new System.Drawing.Point(3, 6);
            this._labelBotInfo.MaximumSize = new System.Drawing.Size(740, 0);
            this._labelBotInfo.Name = "_labelBotInfo";
            this._labelBotInfo.Size = new System.Drawing.Size(740, 48);
            this._labelBotInfo.TabIndex = 0;
            this._labelBotInfo.Text = resources.GetString("_labelBotInfo.Text");
            // 
            // _labelInputFrame
            // 
            this._labelInputFrame.AutoSize = true;
            this._labelInputFrame.Location = new System.Drawing.Point(138, 133);
            this._labelInputFrame.Name = "_labelInputFrame";
            this._labelInputFrame.Size = new System.Drawing.Size(46, 16);
            this._labelInputFrame.TabIndex = 2;
            this._labelInputFrame.Text = "Frame";
            // 
            // _inputFrame
            // 
            this._inputFrame.Location = new System.Drawing.Point(6, 131);
            this._inputFrame.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this._inputFrame.Name = "_inputFrame";
            this._inputFrame.Size = new System.Drawing.Size(126, 22);
            this._inputFrame.TabIndex = 4;
            // 
            // _inputPID
            // 
            this._inputPID.Enabled = false;
            this._inputPID.Location = new System.Drawing.Point(6, 187);
            this._inputPID.Name = "_inputPID";
            this._inputPID.Size = new System.Drawing.Size(178, 22);
            this._inputPID.TabIndex = 6;
            // 
            // _labelPID
            // 
            this._labelPID.AutoSize = true;
            this._labelPID.Location = new System.Drawing.Point(190, 190);
            this._labelPID.Name = "_labelPID";
            this._labelPID.Size = new System.Drawing.Size(29, 16);
            this._labelPID.TabIndex = 7;
            this._labelPID.Text = "PID";
            // 
            // _labelTIDSID
            // 
            this._labelTIDSID.AutoSize = true;
            this._labelTIDSID.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelTIDSID.Location = new System.Drawing.Point(3, 59);
            this._labelTIDSID.MaximumSize = new System.Drawing.Size(740, 0);
            this._labelTIDSID.Name = "_labelTIDSID";
            this._labelTIDSID.Size = new System.Drawing.Size(93, 16);
            this._labelTIDSID.TabIndex = 8;
            this._labelTIDSID.Text = "TID : 0 - SID : 0";
            // 
            // _hitFrameField
            // 
            this._hitFrameField.Location = new System.Drawing.Point(6, 159);
            this._hitFrameField.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this._hitFrameField.Name = "_hitFrameField";
            this._hitFrameField.Size = new System.Drawing.Size(126, 22);
            this._hitFrameField.TabIndex = 10;
            this._hitFrameField.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // _hitFrameLabel
            // 
            this._hitFrameLabel.AutoSize = true;
            this._hitFrameLabel.Location = new System.Drawing.Point(138, 161);
            this._hitFrameLabel.Name = "_hitFrameLabel";
            this._hitFrameLabel.Size = new System.Drawing.Size(65, 16);
            this._hitFrameLabel.TabIndex = 9;
            this._hitFrameLabel.Text = "Hit Frame";
            // 
            // _retryButton
            // 
            this._retryButton.Enabled = false;
            this._retryButton.Location = new System.Drawing.Point(6, 215);
            this._retryButton.Name = "_retryButton";
            this._retryButton.Size = new System.Drawing.Size(129, 26);
            this._retryButton.TabIndex = 11;
            this._retryButton.Text = "Retry";
            this._retryButton.UseVisualStyleBackColor = true;
            this._retryButton.Click += new System.EventHandler(this._retryButton_Click);
            // 
            // _frameLabel
            // 
            this._frameLabel.AutoSize = true;
            this._frameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._frameLabel.Location = new System.Drawing.Point(3, 82);
            this._frameLabel.MaximumSize = new System.Drawing.Size(740, 0);
            this._frameLabel.Name = "_frameLabel";
            this._frameLabel.Size = new System.Drawing.Size(97, 16);
            this._frameLabel.TabIndex = 12;
            this._frameLabel.Text = "Current Frame :";
            // 
            // _pokefinderTypesComboBox
            // 
            this._pokefinderTypesComboBox.FormattingEnabled = true;
            this._pokefinderTypesComboBox.Location = new System.Drawing.Point(6, 101);
            this._pokefinderTypesComboBox.Name = "_pokefinderTypesComboBox";
            this._pokefinderTypesComboBox.Size = new System.Drawing.Size(178, 24);
            this._pokefinderTypesComboBox.TabIndex = 13;
            this._pokefinderTypesComboBox.SelectedIndexChanged += new System.EventHandler(this._pokefinderTypesComboBox_SelectedIndexChanged);
            // 
            // _pokefinderTypeLabel
            // 
            this._pokefinderTypeLabel.AutoSize = true;
            this._pokefinderTypeLabel.Location = new System.Drawing.Point(190, 104);
            this._pokefinderTypeLabel.Name = "_pokefinderTypeLabel";
            this._pokefinderTypeLabel.Size = new System.Drawing.Size(110, 16);
            this._pokefinderTypeLabel.TabIndex = 14;
            this._pokefinderTypeLabel.Text = "Type (facultative)";
            // 
            // PokeFinderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._pokefinderTypeLabel);
            this.Controls.Add(this._pokefinderTypesComboBox);
            this.Controls.Add(this._frameLabel);
            this.Controls.Add(this._retryButton);
            this.Controls.Add(this._hitFrameField);
            this.Controls.Add(this._hitFrameLabel);
            this.Controls.Add(this._labelTIDSID);
            this.Controls.Add(this._labelPID);
            this.Controls.Add(this._inputPID);
            this.Controls.Add(this._inputFrame);
            this.Controls.Add(this._labelInputFrame);
            this.Controls.Add(this._labelBotInfo);
            this.Name = "PokeFinderControl";
            this.Size = new System.Drawing.Size(744, 274);
            ((System.ComponentModel.ISupportInitialize)(this._inputFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._hitFrameField)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _labelBotInfo;
        private System.Windows.Forms.Label _labelInputFrame;
        private System.Windows.Forms.NumericUpDown _inputFrame;
        private System.Windows.Forms.TextBox _inputPID;
        private System.Windows.Forms.Label _labelPID;
        private System.Windows.Forms.Label _labelTIDSID;
        private System.Windows.Forms.NumericUpDown _hitFrameField;
        private System.Windows.Forms.Label _hitFrameLabel;
        private System.Windows.Forms.Button _retryButton;
        private System.Windows.Forms.Label _frameLabel;
        private System.Windows.Forms.ComboBox _pokefinderTypesComboBox;
        private System.Windows.Forms.Label _pokefinderTypeLabel;
    }
}
