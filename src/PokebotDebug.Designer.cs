namespace Pokebot
{
    partial class PokebotDebug
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
            this._tabControl = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this._statesComboBox = new System.Windows.Forms.ComboBox();
            this._quickStartButton = new System.Windows.Forms.Button();
            this._status = new System.Windows.Forms.Label();
            this._tabControl.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // _tabControl
            // 
            this._tabControl.Controls.Add(this.tabPage3);
            this._tabControl.Location = new System.Drawing.Point(0, 0);
            this._tabControl.Name = "_tabControl";
            this._tabControl.SelectedIndex = 0;
            this._tabControl.Size = new System.Drawing.Size(798, 446);
            this._tabControl.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this._status);
            this.tabPage3.Controls.Add(this._statesComboBox);
            this.tabPage3.Controls.Add(this._quickStartButton);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(790, 417);
            this.tabPage3.TabIndex = 1;
            this.tabPage3.Text = "Tools";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // _statesComboBox
            // 
            this._statesComboBox.FormattingEnabled = true;
            this._statesComboBox.Location = new System.Drawing.Point(300, 165);
            this._statesComboBox.Name = "_statesComboBox";
            this._statesComboBox.Size = new System.Drawing.Size(186, 24);
            this._statesComboBox.TabIndex = 1;
            // 
            // _quickStartButton
            // 
            this._quickStartButton.Location = new System.Drawing.Point(300, 195);
            this._quickStartButton.Name = "_quickStartButton";
            this._quickStartButton.Size = new System.Drawing.Size(186, 23);
            this._quickStartButton.TabIndex = 0;
            this._quickStartButton.Text = "Quick Start";
            this._quickStartButton.UseVisualStyleBackColor = true;
            this._quickStartButton.Click += new System.EventHandler(this._quickStartButton_Click);
            // 
            // _status
            // 
            this._status.ForeColor = System.Drawing.Color.Red;
            this._status.Location = new System.Drawing.Point(8, 124);
            this._status.MaximumSize = new System.Drawing.Size(780, 0);
            this._status.Name = "_status";
            this._status.Size = new System.Drawing.Size(780, 20);
            this._status.TabIndex = 2;
            this._status.Text = "debug";
            this._status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PokebotDebug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this._tabControl);
            this.Name = "PokebotDebug";
            this.Text = "PokebotDebug";
            this._tabControl.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl _tabControl;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button _quickStartButton;
        private System.Windows.Forms.ComboBox _statesComboBox;
        private System.Windows.Forms.Label _status;
    }
}