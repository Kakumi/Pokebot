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
            // _tabControl
            // 
            this._tabControl.Location = new System.Drawing.Point(12, 68);
            this._tabControl.Name = "_tabControl";
            this._tabControl.SelectedIndex = 0;
            this._tabControl.Size = new System.Drawing.Size(776, 370);
            this._tabControl.TabIndex = 3;
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _statusLabel;
        private System.Windows.Forms.TabControl _tabControl;
        private System.Windows.Forms.Label _versionLabel;
        private System.Windows.Forms.ToolTip _delayTooltip;
        private System.Windows.Forms.Label _newVersionLabel;
    }
}