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
            this._tasksLabel = new System.Windows.Forms.Label();
            this._stateLabel = new System.Windows.Forms.Label();
            this._tabFinder = new System.Windows.Forms.TabPage();
            this._runFinderButton = new System.Windows.Forms.Button();
            this._finderIterationUpDown = new System.Windows.Forms.NumericUpDown();
            this._finderValueTextBox = new System.Windows.Forms.TextBox();
            this._finderSize = new System.Windows.Forms.NumericUpDown();
            this._finderListenButton = new System.Windows.Forms.Button();
            this._finderClear = new System.Windows.Forms.Button();
            this._finderList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._finderSymbolsCB = new System.Windows.Forms.ComboBox();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._tabControl.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this._playerTab.SuspendLayout();
            this._tabTasks.SuspendLayout();
            this._tabFinder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._finderIterationUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._finderSize)).BeginInit();
            this.SuspendLayout();
            // 
            // _tabControl
            // 
            this._tabControl.Controls.Add(this.tabPage3);
            this._tabControl.Controls.Add(this._playerTab);
            this._tabControl.Controls.Add(this._tabTasks);
            this._tabControl.Controls.Add(this._tabFinder);
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
            // _tasksLabel
            // 
            this._tasksLabel.Location = new System.Drawing.Point(8, 24);
            this._tasksLabel.Name = "_tasksLabel";
            this._tasksLabel.Size = new System.Drawing.Size(776, 400);
            this._tasksLabel.TabIndex = 1;
            this._tasksLabel.Text = "label1";
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
            // _tabFinder
            // 
            this._tabFinder.Controls.Add(this.label4);
            this._tabFinder.Controls.Add(this.label3);
            this._tabFinder.Controls.Add(this.label2);
            this._tabFinder.Controls.Add(this.label1);
            this._tabFinder.Controls.Add(this._finderSymbolsCB);
            this._tabFinder.Controls.Add(this._finderList);
            this._tabFinder.Controls.Add(this._finderClear);
            this._tabFinder.Controls.Add(this._finderListenButton);
            this._tabFinder.Controls.Add(this._finderSize);
            this._tabFinder.Controls.Add(this._finderValueTextBox);
            this._tabFinder.Controls.Add(this._finderIterationUpDown);
            this._tabFinder.Controls.Add(this._runFinderButton);
            this._tabFinder.Location = new System.Drawing.Point(4, 25);
            this._tabFinder.Name = "_tabFinder";
            this._tabFinder.Size = new System.Drawing.Size(790, 417);
            this._tabFinder.TabIndex = 4;
            this._tabFinder.Text = "Finder";
            this._tabFinder.UseVisualStyleBackColor = true;
            // 
            // _runFinderButton
            // 
            this._runFinderButton.Location = new System.Drawing.Point(8, 124);
            this._runFinderButton.Name = "_runFinderButton";
            this._runFinderButton.Size = new System.Drawing.Size(275, 23);
            this._runFinderButton.TabIndex = 0;
            this._runFinderButton.Text = "run";
            this._runFinderButton.UseVisualStyleBackColor = true;
            this._runFinderButton.Click += new System.EventHandler(this.runFinderButton_Click);
            // 
            // _finderIterationUpDown
            // 
            this._finderIterationUpDown.Location = new System.Drawing.Point(8, 40);
            this._finderIterationUpDown.Name = "_finderIterationUpDown";
            this._finderIterationUpDown.Size = new System.Drawing.Size(275, 22);
            this._finderIterationUpDown.TabIndex = 1;
            this._finderIterationUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // _finderValueTextBox
            // 
            this._finderValueTextBox.Location = new System.Drawing.Point(8, 68);
            this._finderValueTextBox.Name = "_finderValueTextBox";
            this._finderValueTextBox.Size = new System.Drawing.Size(275, 22);
            this._finderValueTextBox.TabIndex = 3;
            // 
            // _finderSize
            // 
            this._finderSize.Location = new System.Drawing.Point(8, 96);
            this._finderSize.Name = "_finderSize";
            this._finderSize.Size = new System.Drawing.Size(275, 22);
            this._finderSize.TabIndex = 4;
            this._finderSize.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // _finderListenButton
            // 
            this._finderListenButton.Location = new System.Drawing.Point(289, 124);
            this._finderListenButton.Name = "_finderListenButton";
            this._finderListenButton.Size = new System.Drawing.Size(275, 23);
            this._finderListenButton.TabIndex = 5;
            this._finderListenButton.Text = "run (3s delay)";
            this._finderListenButton.UseVisualStyleBackColor = true;
            this._finderListenButton.Click += new System.EventHandler(this._finderListenButton_Click);
            // 
            // _finderClear
            // 
            this._finderClear.Location = new System.Drawing.Point(570, 124);
            this._finderClear.Name = "_finderClear";
            this._finderClear.Size = new System.Drawing.Size(214, 23);
            this._finderClear.TabIndex = 6;
            this._finderClear.Text = "clear";
            this._finderClear.UseVisualStyleBackColor = true;
            this._finderClear.Click += new System.EventHandler(this._finderClear_Click);
            // 
            // _finderList
            // 
            this._finderList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this._finderList.GridLines = true;
            this._finderList.HideSelection = false;
            this._finderList.Location = new System.Drawing.Point(8, 153);
            this._finderList.Name = "_finderList";
            this._finderList.Size = new System.Drawing.Size(776, 260);
            this._finderList.TabIndex = 7;
            this._finderList.UseCompatibleStateImageBehavior = false;
            this._finderList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Address";
            this.columnHeader1.Width = 326;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Value";
            this.columnHeader2.Width = 302;
            // 
            // _finderSymbolsCB
            // 
            this._finderSymbolsCB.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this._finderSymbolsCB.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this._finderSymbolsCB.FormattingEnabled = true;
            this._finderSymbolsCB.Location = new System.Drawing.Point(8, 10);
            this._finderSymbolsCB.Name = "_finderSymbolsCB";
            this._finderSymbolsCB.Size = new System.Drawing.Size(275, 24);
            this._finderSymbolsCB.TabIndex = 8;
            this._finderSymbolsCB.Click += new System.EventHandler(this._finderSymbolsCB_Click);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Expected";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Size";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(289, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Symbol";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(289, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "Number of try";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(289, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "Expected";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(289, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "Size";
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
            this._tabFinder.ResumeLayout(false);
            this._tabFinder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._finderIterationUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._finderSize)).EndInit();
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
        private System.Windows.Forms.TabPage _tabFinder;
        private System.Windows.Forms.Button _runFinderButton;
        private System.Windows.Forms.TextBox _finderValueTextBox;
        private System.Windows.Forms.NumericUpDown _finderIterationUpDown;
        private System.Windows.Forms.NumericUpDown _finderSize;
        private System.Windows.Forms.Button _finderListenButton;
        private System.Windows.Forms.Button _finderClear;
        private System.Windows.Forms.ListView _finderList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ComboBox _finderSymbolsCB;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}