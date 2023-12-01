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
            this._status = new System.Windows.Forms.Label();
            this._statesComboBox = new System.Windows.Forms.ComboBox();
            this._quickStartButton = new System.Windows.Forms.Button();
            this._playerTab = new System.Windows.Forms.TabPage();
            this._playerFacing = new System.Windows.Forms.Label();
            this._playerY = new System.Windows.Forms.Label();
            this._playerX = new System.Windows.Forms.Label();
            this._tabTasks = new System.Windows.Forms.TabPage();
            this._stateLabel = new System.Windows.Forms.Label();
            this._tasksLabel = new System.Windows.Forms.Label();
            this._tabControl.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this._playerTab.SuspendLayout();
            this._tabTasks.SuspendLayout();
            this.SuspendLayout();
            // 
            // _tabControl
            // 
            this._tabControl.Controls.Add(this.tabPage3);
            this._tabControl.Controls.Add(this._playerTab);
            this._tabControl.Controls.Add(this._tabTasks);
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
            // _status
            // 
            this._status.ForeColor = System.Drawing.Color.Red;
            this._status.Location = new System.Drawing.Point(8, 124);
            this._status.MaximumSize = new System.Drawing.Size(780, 0);
            this._status.Name = "_status";
            this._status.Size = new System.Drawing.Size(780, 0);
            this._status.TabIndex = 2;
            this._status.Text = "debug";
            this._status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // _playerTab
            // 
            this._playerTab.Controls.Add(this._playerFacing);
            this._playerTab.Controls.Add(this._playerY);
            this._playerTab.Controls.Add(this._playerX);
            this._playerTab.Location = new System.Drawing.Point(4, 25);
            this._playerTab.Name = "_playerTab";
            this._playerTab.Size = new System.Drawing.Size(790, 417);
            this._playerTab.TabIndex = 2;
            this._playerTab.Text = "Player";
            this._playerTab.UseVisualStyleBackColor = true;
            // 
            // _playerFacing
            // 
            this._playerFacing.AutoSize = true;
            this._playerFacing.Location = new System.Drawing.Point(8, 44);
            this._playerFacing.Name = "_playerFacing";
            this._playerFacing.Size = new System.Drawing.Size(44, 16);
            this._playerFacing.TabIndex = 2;
            this._playerFacing.Text = "label3";
            // 
            // _playerY
            // 
            this._playerY.AutoSize = true;
            this._playerY.Location = new System.Drawing.Point(8, 28);
            this._playerY.Name = "_playerY";
            this._playerY.Size = new System.Drawing.Size(44, 16);
            this._playerY.TabIndex = 1;
            this._playerY.Text = "label2";
            // 
            // _playerX
            // 
            this._playerX.AutoSize = true;
            this._playerX.Location = new System.Drawing.Point(8, 12);
            this._playerX.Name = "_playerX";
            this._playerX.Size = new System.Drawing.Size(44, 16);
            this._playerX.TabIndex = 0;
            this._playerX.Text = "label1";
            // 
            // _tabTasks
            // 
            this._tabTasks.Controls.Add(this._tasksLabel);
            this._tabTasks.Controls.Add(this._stateLabel);
            this._tabTasks.Location = new System.Drawing.Point(4, 25);
            this._tabTasks.Name = "_tabTasks";
            this._tabTasks.Size = new System.Drawing.Size(790, 417);
            this._tabTasks.TabIndex = 3;
            this._tabTasks.Text = "Tasks";
            this._tabTasks.UseVisualStyleBackColor = true;
            // 
            // _stateLabel
            // 
            this._stateLabel.AutoSize = true;
            this._stateLabel.Location = new System.Drawing.Point(8, 8);
            this._stateLabel.Name = "_stateLabel";
            this._stateLabel.Size = new System.Drawing.Size(44, 16);
            this._stateLabel.TabIndex = 0;
            this._stateLabel.Text = "label1";
            // 
            // _tasksLabel
            // 
            this._tasksLabel.Location = new System.Drawing.Point(8, 24);
            this._tasksLabel.Name = "_tasksLabel";
            this._tasksLabel.Size = new System.Drawing.Size(776, 400);
            this._tasksLabel.TabIndex = 1;
            this._tasksLabel.Text = "label1";
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
            this._playerTab.ResumeLayout(false);
            this._playerTab.PerformLayout();
            this._tabTasks.ResumeLayout(false);
            this._tabTasks.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl _tabControl;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button _quickStartButton;
        private System.Windows.Forms.ComboBox _statesComboBox;
        private System.Windows.Forms.Label _status;
        private System.Windows.Forms.TabPage _playerTab;
        private System.Windows.Forms.Label _playerFacing;
        private System.Windows.Forms.Label _playerY;
        private System.Windows.Forms.Label _playerX;
        private System.Windows.Forms.TabPage _tabTasks;
        private System.Windows.Forms.Label _stateLabel;
        private System.Windows.Forms.Label _tasksLabel;
    }
}