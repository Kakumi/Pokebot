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
            this._statusLabel = new System.Windows.Forms.Label();
            this._loadButton = new System.Windows.Forms.Button();
            this._accelerateCheckbox = new System.Windows.Forms.CheckBox();
            this._tabControl = new System.Windows.Forms.TabControl();
            this._tabSettingsPage = new System.Windows.Forms.TabPage();
            this._pauseCheckbox = new System.Windows.Forms.CheckBox();
            this._soundCheckbox = new System.Windows.Forms.CheckBox();
            this._tabLogsPage = new System.Windows.Forms.TabPage();
            this._logsListView = new System.Windows.Forms.ListView();
            this.level = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.message = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._tabBotPage = new System.Windows.Forms.TabPage();
            this._stopBotButton = new System.Windows.Forms.Button();
            this._statusBot = new System.Windows.Forms.Label();
            this._startBotButton = new System.Windows.Forms.Button();
            this._botPanel = new System.Windows.Forms.Panel();
            this._botComboBox = new System.Windows.Forms.ComboBox();
            this._tabStatsPage = new System.Windows.Forms.TabPage();
            this._statsListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this._tabPagePokemon = new System.Windows.Forms.TabPage();
            this._tabControl.SuspendLayout();
            this._tabSettingsPage.SuspendLayout();
            this._tabLogsPage.SuspendLayout();
            this._tabBotPage.SuspendLayout();
            this._tabStatsPage.SuspendLayout();
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
            // _loadButton
            // 
            this._loadButton.Location = new System.Drawing.Point(171, 6);
            this._loadButton.Name = "_loadButton";
            this._loadButton.Size = new System.Drawing.Size(92, 23);
            this._loadButton.TabIndex = 1;
            this._loadButton.Text = "Load (test)";
            this._loadButton.UseVisualStyleBackColor = true;
            this._loadButton.Click += new System.EventHandler(this.LoadTestClick);
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
            this._tabControl.Controls.Add(this._tabLogsPage);
            this._tabControl.Controls.Add(this._tabBotPage);
            this._tabControl.Controls.Add(this._tabStatsPage);
            this._tabControl.Controls.Add(this._tabPagePokemon);
            this._tabControl.Location = new System.Drawing.Point(12, 68);
            this._tabControl.Name = "_tabControl";
            this._tabControl.SelectedIndex = 0;
            this._tabControl.Size = new System.Drawing.Size(776, 370);
            this._tabControl.TabIndex = 3;
            // 
            // _tabSettingsPage
            // 
            this._tabSettingsPage.Controls.Add(this._pauseCheckbox);
            this._tabSettingsPage.Controls.Add(this._soundCheckbox);
            this._tabSettingsPage.Controls.Add(this._accelerateCheckbox);
            this._tabSettingsPage.Location = new System.Drawing.Point(4, 25);
            this._tabSettingsPage.Name = "_tabSettingsPage";
            this._tabSettingsPage.Padding = new System.Windows.Forms.Padding(3);
            this._tabSettingsPage.Size = new System.Drawing.Size(768, 341);
            this._tabSettingsPage.TabIndex = 0;
            this._tabSettingsPage.Text = "Paramètres";
            this._tabSettingsPage.UseVisualStyleBackColor = true;
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
            // _tabLogsPage
            // 
            this._tabLogsPage.Controls.Add(this._logsListView);
            this._tabLogsPage.Location = new System.Drawing.Point(4, 25);
            this._tabLogsPage.Name = "_tabLogsPage";
            this._tabLogsPage.Padding = new System.Windows.Forms.Padding(3);
            this._tabLogsPage.Size = new System.Drawing.Size(768, 341);
            this._tabLogsPage.TabIndex = 1;
            this._tabLogsPage.Text = "Logs";
            this._tabLogsPage.UseVisualStyleBackColor = true;
            // 
            // _logsListView
            // 
            this._logsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.level,
            this.message});
            this._logsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._logsListView.GridLines = true;
            this._logsListView.HideSelection = false;
            this._logsListView.Location = new System.Drawing.Point(3, 3);
            this._logsListView.MultiSelect = false;
            this._logsListView.Name = "_logsListView";
            this._logsListView.Size = new System.Drawing.Size(762, 335);
            this._logsListView.TabIndex = 0;
            this._logsListView.UseCompatibleStateImageBehavior = false;
            this._logsListView.View = System.Windows.Forms.View.Details;
            // 
            // level
            // 
            this.level.Text = "Type";
            // 
            // message
            // 
            this.message.Text = "Message";
            this.message.Width = 31;
            // 
            // _tabBotPage
            // 
            this._tabBotPage.Controls.Add(this._stopBotButton);
            this._tabBotPage.Controls.Add(this._statusBot);
            this._tabBotPage.Controls.Add(this._startBotButton);
            this._tabBotPage.Controls.Add(this._botPanel);
            this._tabBotPage.Controls.Add(this._botComboBox);
            this._tabBotPage.Location = new System.Drawing.Point(4, 25);
            this._tabBotPage.Name = "_tabBotPage";
            this._tabBotPage.Padding = new System.Windows.Forms.Padding(3);
            this._tabBotPage.Size = new System.Drawing.Size(768, 341);
            this._tabBotPage.TabIndex = 2;
            this._tabBotPage.Text = "Bot";
            this._tabBotPage.UseVisualStyleBackColor = true;
            // 
            // _stopBotButton
            // 
            this._stopBotButton.Location = new System.Drawing.Point(214, 6);
            this._stopBotButton.Name = "_stopBotButton";
            this._stopBotButton.Size = new System.Drawing.Size(75, 23);
            this._stopBotButton.TabIndex = 3;
            this._stopBotButton.Text = "Stop";
            this._stopBotButton.UseVisualStyleBackColor = true;
            this._stopBotButton.Click += new System.EventHandler(this.StopBotClicked);
            // 
            // _statusBot
            // 
            this._statusBot.AutoSize = true;
            this._statusBot.Location = new System.Drawing.Point(6, 33);
            this._statusBot.Name = "_statusBot";
            this._statusBot.Size = new System.Drawing.Size(63, 16);
            this._statusBot.TabIndex = 0;
            this._statusBot.Text = "botStatus";
            // 
            // _startBotButton
            // 
            this._startBotButton.Location = new System.Drawing.Point(133, 6);
            this._startBotButton.Name = "_startBotButton";
            this._startBotButton.Size = new System.Drawing.Size(75, 23);
            this._startBotButton.TabIndex = 2;
            this._startBotButton.Text = "Start";
            this._startBotButton.UseVisualStyleBackColor = true;
            this._startBotButton.Click += new System.EventHandler(this.StartBotClicked);
            // 
            // _botPanel
            // 
            this._botPanel.Location = new System.Drawing.Point(6, 52);
            this._botPanel.Name = "_botPanel";
            this._botPanel.Size = new System.Drawing.Size(756, 283);
            this._botPanel.TabIndex = 1;
            // 
            // _botComboBox
            // 
            this._botComboBox.FormattingEnabled = true;
            this._botComboBox.Location = new System.Drawing.Point(6, 6);
            this._botComboBox.Name = "_botComboBox";
            this._botComboBox.Size = new System.Drawing.Size(121, 24);
            this._botComboBox.TabIndex = 0;
            this._botComboBox.SelectedIndexChanged += new System.EventHandler(this.BotSelectionChanged);
            // 
            // _tabStatsPage
            // 
            this._tabStatsPage.Controls.Add(this._statsListView);
            this._tabStatsPage.Location = new System.Drawing.Point(4, 25);
            this._tabStatsPage.Name = "_tabStatsPage";
            this._tabStatsPage.Size = new System.Drawing.Size(768, 341);
            this._tabStatsPage.TabIndex = 3;
            this._tabStatsPage.Text = "Statistiques";
            this._tabStatsPage.UseVisualStyleBackColor = true;
            // 
            // _statsListView
            // 
            this._statsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this._statsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._statsListView.GridLines = true;
            this._statsListView.HideSelection = false;
            this._statsListView.Location = new System.Drawing.Point(0, 0);
            this._statsListView.MultiSelect = false;
            this._statsListView.Name = "_statsListView";
            this._statsListView.Size = new System.Drawing.Size(768, 341);
            this._statsListView.TabIndex = 1;
            this._statsListView.UseCompatibleStateImageBehavior = false;
            this._statsListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Pokemon";
            this.columnHeader1.Width = 78;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Encounters";
            this.columnHeader2.Width = 89;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Shiny encounters";
            this.columnHeader3.Width = 125;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Ratio";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Pokébot v0.0.1";
            // 
            // _tabPagePokemon
            // 
            this._tabPagePokemon.Location = new System.Drawing.Point(4, 25);
            this._tabPagePokemon.Name = "_tabPagePokemon";
            this._tabPagePokemon.Size = new System.Drawing.Size(768, 341);
            this._tabPagePokemon.TabIndex = 4;
            this._tabPagePokemon.Text = "Viewer";
            this._tabPagePokemon.UseVisualStyleBackColor = true;
            // 
            // Pokebot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this._tabControl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._statusLabel);
            this.Controls.Add(this._loadButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Pokebot";
            this._tabControl.ResumeLayout(false);
            this._tabSettingsPage.ResumeLayout(false);
            this._tabSettingsPage.PerformLayout();
            this._tabLogsPage.ResumeLayout(false);
            this._tabBotPage.ResumeLayout(false);
            this._tabBotPage.PerformLayout();
            this._tabStatsPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _statusLabel;
        private System.Windows.Forms.Button _loadButton;
        private System.Windows.Forms.CheckBox _accelerateCheckbox;
        private System.Windows.Forms.TabControl _tabControl;
        private System.Windows.Forms.TabPage _tabSettingsPage;
        private System.Windows.Forms.TabPage _tabLogsPage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage _tabBotPage;
        private System.Windows.Forms.TabPage _tabStatsPage;
        private System.Windows.Forms.ComboBox _botComboBox;
        private System.Windows.Forms.CheckBox _soundCheckbox;
        private System.Windows.Forms.ListView _logsListView;
        private System.Windows.Forms.ColumnHeader message;
        private System.Windows.Forms.ColumnHeader level;
        private System.Windows.Forms.Panel _botPanel;
        private System.Windows.Forms.CheckBox _pauseCheckbox;
        private System.Windows.Forms.ListView _statsListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button _startBotButton;
        private System.Windows.Forms.Label _statusBot;
        private System.Windows.Forms.Button _stopBotButton;
        private System.Windows.Forms.TabPage _tabPagePokemon;
    }
}