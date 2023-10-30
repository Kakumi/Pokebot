namespace Pokebot.Panels
{
    partial class PokemonFilterPanel
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
            this._shinyCheckbox = new System.Windows.Forms.CheckBox();
            this._ivHpBox = new System.Windows.Forms.NumericUpDown();
            this._statsGroup = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this._ivSpDefenseBox = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this._ivSpAttackBox = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this._ivSpeedBox = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this._ivDefenseBox = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this._ivAttackBox = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this._pokemonComboBox = new System.Windows.Forms.ComboBox();
            this._heldItemComboBox = new System.Windows.Forms.ComboBox();
            this._naturesComboBox = new System.Windows.Forms.ComboBox();
            this._typesComboBox = new System.Windows.Forms.ComboBox();
            this._exactIVCheckbox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this._ivHpBox)).BeginInit();
            this._statsGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._ivSpDefenseBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._ivSpAttackBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._ivSpeedBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._ivDefenseBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._ivAttackBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _shinyCheckbox
            // 
            this._shinyCheckbox.AutoSize = true;
            this._shinyCheckbox.Location = new System.Drawing.Point(0, 3);
            this._shinyCheckbox.Name = "_shinyCheckbox";
            this._shinyCheckbox.Size = new System.Drawing.Size(62, 20);
            this._shinyCheckbox.TabIndex = 0;
            this._shinyCheckbox.Text = "Shiny";
            this._shinyCheckbox.UseVisualStyleBackColor = true;
            this._shinyCheckbox.CheckedChanged += new System.EventHandler(this.ShinyCheckChanged);
            // 
            // _ivHpBox
            // 
            this._ivHpBox.Location = new System.Drawing.Point(60, 21);
            this._ivHpBox.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this._ivHpBox.Name = "_ivHpBox";
            this._ivHpBox.Size = new System.Drawing.Size(47, 22);
            this._ivHpBox.TabIndex = 1;
            this._ivHpBox.ValueChanged += new System.EventHandler(this.IvHp_ValueChanged);
            // 
            // _statsGroup
            // 
            this._statsGroup.Controls.Add(this.label6);
            this._statsGroup.Controls.Add(this._ivSpDefenseBox);
            this._statsGroup.Controls.Add(this.label5);
            this._statsGroup.Controls.Add(this._ivSpAttackBox);
            this._statsGroup.Controls.Add(this.label4);
            this._statsGroup.Controls.Add(this._ivSpeedBox);
            this._statsGroup.Controls.Add(this.label3);
            this._statsGroup.Controls.Add(this._ivDefenseBox);
            this._statsGroup.Controls.Add(this.label2);
            this._statsGroup.Controls.Add(this._ivAttackBox);
            this._statsGroup.Controls.Add(this.label1);
            this._statsGroup.Controls.Add(this._ivHpBox);
            this._statsGroup.Location = new System.Drawing.Point(186, 29);
            this._statsGroup.Name = "_statsGroup";
            this._statsGroup.Size = new System.Drawing.Size(484, 103);
            this._statsGroup.TabIndex = 2;
            this._statsGroup.TabStop = false;
            this._statsGroup.Text = "IVs";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(344, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = "Sp. Defense";
            // 
            // _ivSpDefenseBox
            // 
            this._ivSpDefenseBox.Location = new System.Drawing.Point(431, 60);
            this._ivSpDefenseBox.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this._ivSpDefenseBox.Name = "_ivSpDefenseBox";
            this._ivSpDefenseBox.Size = new System.Drawing.Size(47, 22);
            this._ivSpDefenseBox.TabIndex = 11;
            this._ivSpDefenseBox.ValueChanged += new System.EventHandler(this.IvSpDefense_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(160, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Sp. Attack";
            // 
            // _ivSpAttackBox
            // 
            this._ivSpAttackBox.Location = new System.Drawing.Point(233, 60);
            this._ivSpAttackBox.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this._ivSpAttackBox.Name = "_ivSpAttackBox";
            this._ivSpAttackBox.Size = new System.Drawing.Size(47, 22);
            this._ivSpAttackBox.TabIndex = 9;
            this._ivSpAttackBox.ValueChanged += new System.EventHandler(this.IvSpAttack_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Speed";
            // 
            // _ivSpeedBox
            // 
            this._ivSpeedBox.Location = new System.Drawing.Point(60, 60);
            this._ivSpeedBox.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this._ivSpeedBox.Name = "_ivSpeedBox";
            this._ivSpeedBox.Size = new System.Drawing.Size(47, 22);
            this._ivSpeedBox.TabIndex = 7;
            this._ivSpeedBox.ValueChanged += new System.EventHandler(this.IvSpeed_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(367, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Defense";
            // 
            // _ivDefenseBox
            // 
            this._ivDefenseBox.Location = new System.Drawing.Point(431, 21);
            this._ivDefenseBox.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this._ivDefenseBox.Name = "_ivDefenseBox";
            this._ivDefenseBox.Size = new System.Drawing.Size(47, 22);
            this._ivDefenseBox.TabIndex = 5;
            this._ivDefenseBox.ValueChanged += new System.EventHandler(this.IvDefense_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(183, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Attack";
            // 
            // _ivAttackBox
            // 
            this._ivAttackBox.Location = new System.Drawing.Point(233, 21);
            this._ivAttackBox.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this._ivAttackBox.Name = "_ivAttackBox";
            this._ivAttackBox.Size = new System.Drawing.Size(47, 22);
            this._ivAttackBox.TabIndex = 3;
            this._ivAttackBox.ValueChanged += new System.EventHandler(this.IvAttack_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "HP";
            // 
            // _pokemonComboBox
            // 
            this._pokemonComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this._pokemonComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this._pokemonComboBox.FormattingEnabled = true;
            this._pokemonComboBox.Location = new System.Drawing.Point(0, 29);
            this._pokemonComboBox.Name = "_pokemonComboBox";
            this._pokemonComboBox.Size = new System.Drawing.Size(121, 24);
            this._pokemonComboBox.TabIndex = 3;
            this._pokemonComboBox.SelectedIndexChanged += new System.EventHandler(this.PokemonSelectionChanged);
            // 
            // _heldItemComboBox
            // 
            this._heldItemComboBox.FormattingEnabled = true;
            this._heldItemComboBox.Location = new System.Drawing.Point(0, 59);
            this._heldItemComboBox.Name = "_heldItemComboBox";
            this._heldItemComboBox.Size = new System.Drawing.Size(121, 24);
            this._heldItemComboBox.TabIndex = 4;
            this._heldItemComboBox.SelectedIndexChanged += new System.EventHandler(this.HeldItemSelection_SelectedIndexChanged);
            // 
            // _naturesComboBox
            // 
            this._naturesComboBox.FormattingEnabled = true;
            this._naturesComboBox.Location = new System.Drawing.Point(0, 89);
            this._naturesComboBox.Name = "_naturesComboBox";
            this._naturesComboBox.Size = new System.Drawing.Size(121, 24);
            this._naturesComboBox.TabIndex = 5;
            this._naturesComboBox.SelectedIndexChanged += new System.EventHandler(this.NatureSelectionChanged);
            // 
            // _typesComboBox
            // 
            this._typesComboBox.FormattingEnabled = true;
            this._typesComboBox.Location = new System.Drawing.Point(0, 119);
            this._typesComboBox.Name = "_typesComboBox";
            this._typesComboBox.Size = new System.Drawing.Size(121, 24);
            this._typesComboBox.TabIndex = 6;
            this._typesComboBox.SelectedIndexChanged += new System.EventHandler(this.TypeSelectionChanged);
            // 
            // _exactIVCheckbox
            // 
            this._exactIVCheckbox.AutoSize = true;
            this._exactIVCheckbox.Location = new System.Drawing.Point(186, 3);
            this._exactIVCheckbox.Name = "_exactIVCheckbox";
            this._exactIVCheckbox.Size = new System.Drawing.Size(195, 20);
            this._exactIVCheckbox.TabIndex = 7;
            this._exactIVCheckbox.Text = "Exact IV (minimal by default)";
            this._exactIVCheckbox.UseVisualStyleBackColor = true;
            this._exactIVCheckbox.CheckedChanged += new System.EventHandler(this.ExactIVCheckChanged);
            // 
            // PokemonFilterPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._exactIVCheckbox);
            this.Controls.Add(this._typesComboBox);
            this.Controls.Add(this._naturesComboBox);
            this.Controls.Add(this._heldItemComboBox);
            this.Controls.Add(this._pokemonComboBox);
            this.Controls.Add(this._statsGroup);
            this.Controls.Add(this._shinyCheckbox);
            this.Name = "PokemonFilterPanel";
            this.Size = new System.Drawing.Size(750, 280);
            ((System.ComponentModel.ISupportInitialize)(this._ivHpBox)).EndInit();
            this._statsGroup.ResumeLayout(false);
            this._statsGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._ivSpDefenseBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._ivSpAttackBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._ivSpeedBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._ivDefenseBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._ivAttackBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox _shinyCheckbox;
        private System.Windows.Forms.NumericUpDown _ivHpBox;
        private System.Windows.Forms.GroupBox _statsGroup;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown _ivSpDefenseBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown _ivSpAttackBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown _ivSpeedBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown _ivDefenseBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown _ivAttackBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox _pokemonComboBox;
        private System.Windows.Forms.ComboBox _heldItemComboBox;
        private System.Windows.Forms.ComboBox _naturesComboBox;
        private System.Windows.Forms.ComboBox _typesComboBox;
        private System.Windows.Forms.CheckBox _exactIVCheckbox;
    }
}
