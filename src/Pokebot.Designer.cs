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
            this._tabControl = new System.Windows.Forms.TabControl();
            this._versionLabel = new System.Windows.Forms.Label();
            this._delayTooltip = new System.Windows.Forms.ToolTip(this.components);
            this._newVersionLabel = new System.Windows.Forms.Label();
            this._bugButton = new System.Windows.Forms.Button();
            this._joinDiscordButton = new System.Windows.Forms.Button();
            this._testedStatus = new System.Windows.Forms.Label();
            this._openDashboardButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _statusLabel
            // 
            this._statusLabel.AutoSize = true;
            this._statusLabel.Location = new System.Drawing.Point(9, 30);
            this._statusLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Size = new System.Drawing.Size(84, 13);
            this._statusLabel.TabIndex = 0;
            this._statusLabel.Text = "No ROM loaded";
            // 
            // _tabControl
            // 
            this._tabControl.Location = new System.Drawing.Point(9, 62);
            this._tabControl.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._tabControl.Name = "_tabControl";
            this._tabControl.SelectedIndex = 0;
            this._tabControl.Size = new System.Drawing.Size(582, 309);
            this._tabControl.TabIndex = 3;
            // 
            // _versionLabel
            // 
            this._versionLabel.AutoSize = true;
            this._versionLabel.Location = new System.Drawing.Point(9, 7);
            this._versionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._versionLabel.Name = "_versionLabel";
            this._versionLabel.Size = new System.Drawing.Size(56, 13);
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
            this._newVersionLabel.Location = new System.Drawing.Point(111, 7);
            this._newVersionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._newVersionLabel.Name = "_newVersionLabel";
            this._newVersionLabel.Size = new System.Drawing.Size(97, 13);
            this._newVersionLabel.TabIndex = 4;
            this._newVersionLabel.Text = "new version check";
            // 
            // _bugButton
            // 
            this._bugButton.Location = new System.Drawing.Point(535, 28);
            this._bugButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._bugButton.Name = "_bugButton";
            this._bugButton.Size = new System.Drawing.Size(56, 23);
            this._bugButton.TabIndex = 5;
            this._bugButton.Text = "Bug";
            this._bugButton.UseVisualStyleBackColor = true;
            this._bugButton.Click += new System.EventHandler(this._bugButton_Click);
            // 
            // _joinDiscordButton
            // 
            this._joinDiscordButton.Location = new System.Drawing.Point(428, 28);
            this._joinDiscordButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._joinDiscordButton.Name = "_joinDiscordButton";
            this._joinDiscordButton.Size = new System.Drawing.Size(102, 23);
            this._joinDiscordButton.TabIndex = 6;
            this._joinDiscordButton.Text = "Join Discord";
            this._joinDiscordButton.UseVisualStyleBackColor = true;
            this._joinDiscordButton.Click += new System.EventHandler(this._joinDiscordButton_Click);
            // 
            // _testedStatus
            // 
            this._testedStatus.AutoSize = true;
            this._testedStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._testedStatus.ForeColor = System.Drawing.Color.DarkGray;
            this._testedStatus.Location = new System.Drawing.Point(11, 47);
            this._testedStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._testedStatus.Name = "_testedStatus";
            this._testedStatus.Size = new System.Drawing.Size(82, 13);
            this._testedStatus.TabIndex = 7;
            this._testedStatus.Text = "No ROM loaded";
            // 
            // _openDashboardButton
            // 
            this._openDashboardButton.Location = new System.Drawing.Point(319, 28);
            this._openDashboardButton.Margin = new System.Windows.Forms.Padding(2);
            this._openDashboardButton.Name = "_openDashboardButton";
            this._openDashboardButton.Size = new System.Drawing.Size(104, 23);
            this._openDashboardButton.TabIndex = 8;
            this._openDashboardButton.Text = "Dashboard";
            this._openDashboardButton.UseVisualStyleBackColor = true;
            this._openDashboardButton.Click += new System.EventHandler(this._openDashboardButton_Click);
            // 
            // Pokebot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 382);
            this.Controls.Add(this._openDashboardButton);
            this.Controls.Add(this._testedStatus);
            this.Controls.Add(this._joinDiscordButton);
            this.Controls.Add(this._bugButton);
            this.Controls.Add(this._newVersionLabel);
            this.Controls.Add(this._tabControl);
            this.Controls.Add(this._versionLabel);
            this.Controls.Add(this._statusLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Pokebot";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _statusLabel;
        private System.Windows.Forms.TabControl _tabControl;
        private System.Windows.Forms.Label _versionLabel;
        private System.Windows.Forms.ToolTip _delayTooltip;
        private System.Windows.Forms.Label _newVersionLabel;
        private System.Windows.Forms.Button _bugButton;
        private System.Windows.Forms.Button _joinDiscordButton;
        private System.Windows.Forms.Label _testedStatus;
        private System.Windows.Forms.Button _openDashboardButton;
    }
}
