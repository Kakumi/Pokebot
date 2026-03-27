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
            this._checkOpponentBtn = new System.Windows.Forms.Button();
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
            this._tabScanner = new System.Windows.Forms.TabPage();
            this._scannerDropdown = new System.Windows.Forms.ComboBox();
            this._scannerStartBtn = new System.Windows.Forms.Button();
            this._scannerPanelContainer = new System.Windows.Forms.Panel();
            this._scannerResultsText = new System.Windows.Forms.TextBox();
            this._scannerCopyBtn = new System.Windows.Forms.Button();
            this._finderInspectReverse = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this._finderOffsetUpDown = new System.Windows.Forms.NumericUpDown();
            this._filterAdd = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._finderSymbolsCB = new System.Windows.Forms.ComboBox();
            this._finderList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._finderClear = new System.Windows.Forms.Button();
            this._finderListenButton = new System.Windows.Forms.Button();
            this._finderSize = new System.Windows.Forms.NumericUpDown();
            this._finderValueTextBox = new System.Windows.Forms.TextBox();
            this._finderIterationUpDown = new System.Windows.Forms.NumericUpDown();
            this._runFinderButton = new System.Windows.Forms.Button();
            this._tabControl.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this._playerTab.SuspendLayout();
            this._tabTasks.SuspendLayout();
            this._tabFinder.SuspendLayout();
            this._tabScanner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._finderOffsetUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._finderSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._finderIterationUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // _tabControl
            // 
            this._tabControl.Controls.Add(this.tabPage3);
            this._tabControl.Controls.Add(this._playerTab);
            this._tabControl.Controls.Add(this._tabTasks);
            this._tabControl.Controls.Add(this._tabFinder);
            this._tabControl.Controls.Add(this._tabScanner);
            this._tabControl.Location = new System.Drawing.Point(0, 0);
            this._tabControl.Margin = new System.Windows.Forms.Padding(2);
            this._tabControl.Name = "_tabControl";
            this._tabControl.SelectedIndex = 0;
            this._tabControl.Size = new System.Drawing.Size(598, 362);
            this._tabControl.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this._checkOpponentBtn);
            this.tabPage3.Controls.Add(this._status);
            this.tabPage3.Controls.Add(this._statesComboBox);
            this.tabPage3.Controls.Add(this._quickStartButton);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(590, 336);
            this.tabPage3.TabIndex = 1;
            this.tabPage3.Text = "Tools";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // _checkOpponentBtn
            // 
            this._checkOpponentBtn.Location = new System.Drawing.Point(225, 269);
            this._checkOpponentBtn.Margin = new System.Windows.Forms.Padding(2);
            this._checkOpponentBtn.Name = "_checkOpponentBtn";
            this._checkOpponentBtn.Size = new System.Drawing.Size(140, 19);
            this._checkOpponentBtn.TabIndex = 3;
            this._checkOpponentBtn.Text = "Trigger Opponent";
            this._checkOpponentBtn.UseVisualStyleBackColor = true;
            this._checkOpponentBtn.Click += new System.EventHandler(this._checkOpponentBtn_Click);
            // 
            // _status
            // 
            this._status.ForeColor = System.Drawing.Color.Red;
            this._status.Location = new System.Drawing.Point(6, 101);
            this._status.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._status.MaximumSize = new System.Drawing.Size(585, 0);
            this._status.Name = "_status";
            this._status.Size = new System.Drawing.Size(585, 0);
            this._status.TabIndex = 2;
            this._status.Text = "debug";
            this._status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _statesComboBox
            // 
            this._statesComboBox.FormattingEnabled = true;
            this._statesComboBox.Location = new System.Drawing.Point(225, 134);
            this._statesComboBox.Margin = new System.Windows.Forms.Padding(2);
            this._statesComboBox.Name = "_statesComboBox";
            this._statesComboBox.Size = new System.Drawing.Size(140, 21);
            this._statesComboBox.TabIndex = 1;
            // 
            // _quickStartButton
            // 
            this._quickStartButton.Location = new System.Drawing.Point(225, 158);
            this._quickStartButton.Margin = new System.Windows.Forms.Padding(2);
            this._quickStartButton.Name = "_quickStartButton";
            this._quickStartButton.Size = new System.Drawing.Size(140, 19);
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
            this._playerTab.Location = new System.Drawing.Point(4, 22);
            this._playerTab.Margin = new System.Windows.Forms.Padding(2);
            this._playerTab.Name = "_playerTab";
            this._playerTab.Size = new System.Drawing.Size(590, 336);
            this._playerTab.TabIndex = 2;
            this._playerTab.Text = "Player";
            this._playerTab.UseVisualStyleBackColor = true;
            // 
            // _playerFacing
            // 
            this._playerFacing.AutoSize = true;
            this._playerFacing.Location = new System.Drawing.Point(6, 36);
            this._playerFacing.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._playerFacing.Name = "_playerFacing";
            this._playerFacing.Size = new System.Drawing.Size(35, 13);
            this._playerFacing.TabIndex = 2;
            this._playerFacing.Text = "label3";
            // 
            // _playerY
            // 
            this._playerY.AutoSize = true;
            this._playerY.Location = new System.Drawing.Point(6, 23);
            this._playerY.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._playerY.Name = "_playerY";
            this._playerY.Size = new System.Drawing.Size(35, 13);
            this._playerY.TabIndex = 1;
            this._playerY.Text = "label2";
            // 
            // _playerX
            // 
            this._playerX.AutoSize = true;
            this._playerX.Location = new System.Drawing.Point(6, 10);
            this._playerX.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._playerX.Name = "_playerX";
            this._playerX.Size = new System.Drawing.Size(35, 13);
            this._playerX.TabIndex = 0;
            this._playerX.Text = "label1";
            // 
            // _tabTasks
            // 
            this._tabTasks.Controls.Add(this._tasksLabel);
            this._tabTasks.Controls.Add(this._stateLabel);
            this._tabTasks.Location = new System.Drawing.Point(4, 22);
            this._tabTasks.Margin = new System.Windows.Forms.Padding(2);
            this._tabTasks.Name = "_tabTasks";
            this._tabTasks.Size = new System.Drawing.Size(590, 336);
            this._tabTasks.TabIndex = 3;
            this._tabTasks.Text = "Tasks";
            this._tabTasks.UseVisualStyleBackColor = true;
            // 
            // _tasksLabel
            // 
            this._tasksLabel.Location = new System.Drawing.Point(6, 20);
            this._tasksLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._tasksLabel.Name = "_tasksLabel";
            this._tasksLabel.Size = new System.Drawing.Size(582, 325);
            this._tasksLabel.TabIndex = 1;
            this._tasksLabel.Text = "label1";
            // 
            // _stateLabel
            // 
            this._stateLabel.AutoSize = true;
            this._stateLabel.Location = new System.Drawing.Point(6, 6);
            this._stateLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._stateLabel.Name = "_stateLabel";
            this._stateLabel.Size = new System.Drawing.Size(35, 13);
            this._stateLabel.TabIndex = 0;
            this._stateLabel.Text = "label1";
            // 
            // _tabFinder
            // 
            this._tabFinder.Controls.Add(this._finderInspectReverse);
            this._tabFinder.Controls.Add(this.label5);
            this._tabFinder.Controls.Add(this._finderOffsetUpDown);
            this._tabFinder.Controls.Add(this._filterAdd);
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
            this._tabFinder.Location = new System.Drawing.Point(4, 22);
            this._tabFinder.Margin = new System.Windows.Forms.Padding(2);
            this._tabFinder.Name = "_tabFinder";
            this._tabFinder.Size = new System.Drawing.Size(590, 336);
            this._tabFinder.TabIndex = 4;
            this._tabFinder.Text = "Finder";
            this._tabFinder.UseVisualStyleBackColor = true;
            // 
            // _finderInspectReverse
            // 
            this._finderInspectReverse.Location = new System.Drawing.Point(427, 8);
            this._finderInspectReverse.Margin = new System.Windows.Forms.Padding(2);
            this._finderInspectReverse.Name = "_finderInspectReverse";
            this._finderInspectReverse.Size = new System.Drawing.Size(158, 19);
            this._finderInspectReverse.TabIndex = 16;
            this._finderInspectReverse.Text = "inspect reverse addr";
            this._finderInspectReverse.UseVisualStyleBackColor = true;
            this._finderInspectReverse.Click += new System.EventHandler(this._finderInspectReverse_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(465, 80);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Offset";
            // 
            // _finderOffsetUpDown
            // 
            this._finderOffsetUpDown.Location = new System.Drawing.Point(326, 78);
            this._finderOffsetUpDown.Margin = new System.Windows.Forms.Padding(2);
            this._finderOffsetUpDown.Name = "_finderOffsetUpDown";
            this._finderOffsetUpDown.Size = new System.Drawing.Size(134, 20);
            this._finderOffsetUpDown.TabIndex = 14;
            // 
            // _filterAdd
            // 
            this._filterAdd.Location = new System.Drawing.Point(326, 101);
            this._filterAdd.Margin = new System.Windows.Forms.Padding(2);
            this._filterAdd.Name = "_filterAdd";
            this._filterAdd.Size = new System.Drawing.Size(134, 19);
            this._filterAdd.TabIndex = 13;
            this._filterAdd.Text = "add symbol";
            this._filterAdd.UseVisualStyleBackColor = true;
            this._filterAdd.Click += new System.EventHandler(this._filterAdd_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(217, 80);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Size";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(217, 58);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Expected";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(217, 34);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Number of try";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(217, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Symbol";
            // 
            // _finderSymbolsCB
            // 
            this._finderSymbolsCB.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this._finderSymbolsCB.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this._finderSymbolsCB.FormattingEnabled = true;
            this._finderSymbolsCB.Location = new System.Drawing.Point(6, 8);
            this._finderSymbolsCB.Margin = new System.Windows.Forms.Padding(2);
            this._finderSymbolsCB.Name = "_finderSymbolsCB";
            this._finderSymbolsCB.Size = new System.Drawing.Size(207, 21);
            this._finderSymbolsCB.TabIndex = 8;
            this._finderSymbolsCB.Click += new System.EventHandler(this._finderSymbolsCB_Click);
            // 
            // _finderList
            // 
            this._finderList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this._finderList.GridLines = true;
            this._finderList.HideSelection = false;
            this._finderList.Location = new System.Drawing.Point(6, 124);
            this._finderList.Margin = new System.Windows.Forms.Padding(2);
            this._finderList.Name = "_finderList";
            this._finderList.Size = new System.Drawing.Size(583, 212);
            this._finderList.TabIndex = 7;
            this._finderList.UseCompatibleStateImageBehavior = false;
            this._finderList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Address";
            this.columnHeader1.Width = 161;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Value";
            this.columnHeader2.Width = 162;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Expected";
            this.columnHeader3.Width = 130;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Size";
            this.columnHeader4.Width = 65;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Offset";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Value Raw";
            this.columnHeader6.Width = 180;
            // 
            // _finderClear
            // 
            this._finderClear.Location = new System.Drawing.Point(465, 101);
            this._finderClear.Margin = new System.Windows.Forms.Padding(2);
            this._finderClear.Name = "_finderClear";
            this._finderClear.Size = new System.Drawing.Size(120, 19);
            this._finderClear.TabIndex = 6;
            this._finderClear.Text = "clear";
            this._finderClear.UseVisualStyleBackColor = true;
            this._finderClear.Click += new System.EventHandler(this._finderClear_Click);
            // 
            // _finderListenButton
            // 
            this._finderListenButton.Location = new System.Drawing.Point(166, 101);
            this._finderListenButton.Margin = new System.Windows.Forms.Padding(2);
            this._finderListenButton.Name = "_finderListenButton";
            this._finderListenButton.Size = new System.Drawing.Size(156, 19);
            this._finderListenButton.TabIndex = 5;
            this._finderListenButton.Text = "run (3s delay)";
            this._finderListenButton.UseVisualStyleBackColor = true;
            this._finderListenButton.Click += new System.EventHandler(this._finderListenButton_Click);
            // 
            // _finderSize
            // 
            this._finderSize.Location = new System.Drawing.Point(6, 78);
            this._finderSize.Margin = new System.Windows.Forms.Padding(2);
            this._finderSize.Name = "_finderSize";
            this._finderSize.Size = new System.Drawing.Size(206, 20);
            this._finderSize.TabIndex = 4;
            this._finderSize.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // _finderValueTextBox
            // 
            this._finderValueTextBox.Location = new System.Drawing.Point(6, 55);
            this._finderValueTextBox.Margin = new System.Windows.Forms.Padding(2);
            this._finderValueTextBox.Name = "_finderValueTextBox";
            this._finderValueTextBox.Size = new System.Drawing.Size(207, 20);
            this._finderValueTextBox.TabIndex = 3;
            // 
            // _finderIterationUpDown
            // 
            this._finderIterationUpDown.Location = new System.Drawing.Point(6, 32);
            this._finderIterationUpDown.Margin = new System.Windows.Forms.Padding(2);
            this._finderIterationUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this._finderIterationUpDown.Name = "_finderIterationUpDown";
            this._finderIterationUpDown.Size = new System.Drawing.Size(206, 20);
            this._finderIterationUpDown.TabIndex = 1;
            this._finderIterationUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // _runFinderButton
            // 
            this._runFinderButton.Location = new System.Drawing.Point(6, 101);
            this._runFinderButton.Margin = new System.Windows.Forms.Padding(2);
            this._runFinderButton.Name = "_runFinderButton";
            this._runFinderButton.Size = new System.Drawing.Size(155, 19);
            this._runFinderButton.TabIndex = 0;
            this._runFinderButton.Text = "run";
            this._runFinderButton.UseVisualStyleBackColor = true;
            this._runFinderButton.Click += new System.EventHandler(this.runFinderButton_Click);
            // 
            // PokebotDebug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this._tabControl);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "PokebotDebug";
            this.Text = "PokebotDebug";
            //
            // _tabScanner
            //
            this._tabScanner.Controls.Add(this._scannerDropdown);
            this._tabScanner.Controls.Add(this._scannerStartBtn);
            this._tabScanner.Controls.Add(this._scannerPanelContainer);
            this._tabScanner.Controls.Add(this._scannerResultsText);
            this._tabScanner.Controls.Add(this._scannerCopyBtn);
            this._tabScanner.Location = new System.Drawing.Point(4, 22);
            this._tabScanner.Margin = new System.Windows.Forms.Padding(2);
            this._tabScanner.Name = "_tabScanner";
            this._tabScanner.Size = new System.Drawing.Size(590, 336);
            this._tabScanner.TabIndex = 5;
            this._tabScanner.Text = "Scanner";
            this._tabScanner.UseVisualStyleBackColor = true;
            //
            // _scannerDropdown
            //
            this._scannerDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._scannerDropdown.FormattingEnabled = true;
            this._scannerDropdown.Location = new System.Drawing.Point(6, 6);
            this._scannerDropdown.Name = "_scannerDropdown";
            this._scannerDropdown.Size = new System.Drawing.Size(240, 21);
            this._scannerDropdown.TabIndex = 0;
            this._scannerDropdown.SelectedIndexChanged += new System.EventHandler(this._scannerDropdown_SelectedIndexChanged);
            //
            // _scannerStartBtn
            //
            this._scannerStartBtn.Location = new System.Drawing.Point(252, 5);
            this._scannerStartBtn.Name = "_scannerStartBtn";
            this._scannerStartBtn.Size = new System.Drawing.Size(80, 23);
            this._scannerStartBtn.TabIndex = 1;
            this._scannerStartBtn.Text = "Start";
            this._scannerStartBtn.UseVisualStyleBackColor = true;
            this._scannerStartBtn.Click += new System.EventHandler(this._scannerStartBtn_Click);
            //
            // _scannerPanelContainer
            //
            this._scannerPanelContainer.Location = new System.Drawing.Point(6, 34);
            this._scannerPanelContainer.Name = "_scannerPanelContainer";
            this._scannerPanelContainer.Size = new System.Drawing.Size(578, 90);
            this._scannerPanelContainer.TabIndex = 2;
            //
            // _scannerResultsText
            //
            this._scannerResultsText.Location = new System.Drawing.Point(6, 130);
            this._scannerResultsText.Multiline = true;
            this._scannerResultsText.Name = "_scannerResultsText";
            this._scannerResultsText.ReadOnly = true;
            this._scannerResultsText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._scannerResultsText.Size = new System.Drawing.Size(578, 188);
            this._scannerResultsText.TabIndex = 3;
            //
            // _scannerCopyBtn
            //
            this._scannerCopyBtn.Location = new System.Drawing.Point(6, 322);
            this._scannerCopyBtn.Name = "_scannerCopyBtn";
            this._scannerCopyBtn.Size = new System.Drawing.Size(120, 22);
            this._scannerCopyBtn.TabIndex = 4;
            this._scannerCopyBtn.Text = "Copy results";
            this._scannerCopyBtn.UseVisualStyleBackColor = true;
            this._scannerCopyBtn.Click += new System.EventHandler(this._scannerCopyBtn_Click);
            //
            this._tabControl.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this._playerTab.ResumeLayout(false);
            this._playerTab.PerformLayout();
            this._tabTasks.ResumeLayout(false);
            this._tabTasks.PerformLayout();
            this._tabFinder.ResumeLayout(false);
            this._tabFinder.PerformLayout();
            this._tabScanner.ResumeLayout(false);
            this._tabScanner.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._finderOffsetUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._finderSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._finderIterationUpDown)).EndInit();
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
        private System.Windows.Forms.Button _filterAdd;
        private System.Windows.Forms.NumericUpDown _finderOffsetUpDown;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button _finderInspectReverse;
        private System.Windows.Forms.Button _checkOpponentBtn;
        private System.Windows.Forms.TabPage _tabScanner;
        private System.Windows.Forms.ComboBox _scannerDropdown;
        private System.Windows.Forms.Button _scannerStartBtn;
        private System.Windows.Forms.Panel _scannerPanelContainer;
        private System.Windows.Forms.TextBox _scannerResultsText;
        private System.Windows.Forms.Button _scannerCopyBtn;
    }
}