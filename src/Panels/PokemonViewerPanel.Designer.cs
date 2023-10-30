namespace Pokebot.Panels
{
    partial class PokemonViewerPanel
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
            this._pokemonName = new System.Windows.Forms.Label();
            this._natureLabel = new System.Windows.Forms.Label();
            this._pokemonPictureBox = new System.Windows.Forms.PictureBox();
            this._abilityLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._hiddenPowerType = new System.Windows.Forms.Label();
            this._hiddenPowerDamage = new System.Windows.Forms.Label();
            this._itemLabel = new System.Windows.Forms.Label();
            this._move1Label = new System.Windows.Forms.Label();
            this._move2Label = new System.Windows.Forms.Label();
            this._move3Label = new System.Windows.Forms.Label();
            this._move4Label = new System.Windows.Forms.Label();
            this._ivHPLabel = new System.Windows.Forms.Label();
            this._ivSpeedLabel = new System.Windows.Forms.Label();
            this._ivAttackLabel = new System.Windows.Forms.Label();
            this._ivDefenseLabel = new System.Windows.Forms.Label();
            this._ivSpAttackLabel = new System.Windows.Forms.Label();
            this._ivSpDefenseLabel = new System.Windows.Forms.Label();
            this._shinyLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._pokemonPictureBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _pokemonName
            // 
            this._pokemonName.AutoSize = true;
            this._pokemonName.Location = new System.Drawing.Point(137, 3);
            this._pokemonName.Name = "_pokemonName";
            this._pokemonName.Size = new System.Drawing.Size(44, 16);
            this._pokemonName.TabIndex = 0;
            this._pokemonName.Text = "label1";
            // 
            // _natureLabel
            // 
            this._natureLabel.AutoSize = true;
            this._natureLabel.Location = new System.Drawing.Point(137, 19);
            this._natureLabel.Name = "_natureLabel";
            this._natureLabel.Size = new System.Drawing.Size(44, 16);
            this._natureLabel.TabIndex = 1;
            this._natureLabel.Text = "label2";
            // 
            // _pokemonPictureBox
            // 
            this._pokemonPictureBox.Location = new System.Drawing.Point(3, 3);
            this._pokemonPictureBox.Name = "_pokemonPictureBox";
            this._pokemonPictureBox.Size = new System.Drawing.Size(128, 128);
            this._pokemonPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this._pokemonPictureBox.TabIndex = 2;
            this._pokemonPictureBox.TabStop = false;
            // 
            // _abilityLabel
            // 
            this._abilityLabel.AutoSize = true;
            this._abilityLabel.Location = new System.Drawing.Point(137, 35);
            this._abilityLabel.Name = "_abilityLabel";
            this._abilityLabel.Size = new System.Drawing.Size(44, 16);
            this._abilityLabel.TabIndex = 3;
            this._abilityLabel.Text = "label3";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._move4Label);
            this.groupBox1.Controls.Add(this._move3Label);
            this.groupBox1.Controls.Add(this._move2Label);
            this.groupBox1.Controls.Add(this._move1Label);
            this.groupBox1.Location = new System.Drawing.Point(3, 137);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(348, 140);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Moves";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this._ivSpDefenseLabel);
            this.groupBox2.Controls.Add(this._ivSpAttackLabel);
            this.groupBox2.Controls.Add(this._ivDefenseLabel);
            this.groupBox2.Controls.Add(this._ivAttackLabel);
            this.groupBox2.Controls.Add(this._ivSpeedLabel);
            this.groupBox2.Controls.Add(this._ivHPLabel);
            this.groupBox2.Location = new System.Drawing.Point(357, 137);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(390, 140);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "IVs";
            // 
            // _hiddenPowerType
            // 
            this._hiddenPowerType.AutoSize = true;
            this._hiddenPowerType.Location = new System.Drawing.Point(137, 51);
            this._hiddenPowerType.Name = "_hiddenPowerType";
            this._hiddenPowerType.Size = new System.Drawing.Size(44, 16);
            this._hiddenPowerType.TabIndex = 6;
            this._hiddenPowerType.Text = "label3";
            // 
            // _hiddenPowerDamage
            // 
            this._hiddenPowerDamage.AutoSize = true;
            this._hiddenPowerDamage.Location = new System.Drawing.Point(137, 67);
            this._hiddenPowerDamage.Name = "_hiddenPowerDamage";
            this._hiddenPowerDamage.Size = new System.Drawing.Size(44, 16);
            this._hiddenPowerDamage.TabIndex = 7;
            this._hiddenPowerDamage.Text = "label3";
            // 
            // _itemLabel
            // 
            this._itemLabel.AutoSize = true;
            this._itemLabel.Location = new System.Drawing.Point(137, 83);
            this._itemLabel.Name = "_itemLabel";
            this._itemLabel.Size = new System.Drawing.Size(44, 16);
            this._itemLabel.TabIndex = 8;
            this._itemLabel.Text = "label3";
            // 
            // _move1Label
            // 
            this._move1Label.AutoSize = true;
            this._move1Label.Location = new System.Drawing.Point(6, 18);
            this._move1Label.Name = "_move1Label";
            this._move1Label.Size = new System.Drawing.Size(44, 16);
            this._move1Label.TabIndex = 9;
            this._move1Label.Text = "label3";
            // 
            // _move2Label
            // 
            this._move2Label.AutoSize = true;
            this._move2Label.Location = new System.Drawing.Point(6, 34);
            this._move2Label.Name = "_move2Label";
            this._move2Label.Size = new System.Drawing.Size(44, 16);
            this._move2Label.TabIndex = 10;
            this._move2Label.Text = "label3";
            // 
            // _move3Label
            // 
            this._move3Label.AutoSize = true;
            this._move3Label.Location = new System.Drawing.Point(6, 50);
            this._move3Label.Name = "_move3Label";
            this._move3Label.Size = new System.Drawing.Size(44, 16);
            this._move3Label.TabIndex = 11;
            this._move3Label.Text = "label3";
            // 
            // _move4Label
            // 
            this._move4Label.AutoSize = true;
            this._move4Label.Location = new System.Drawing.Point(6, 66);
            this._move4Label.Name = "_move4Label";
            this._move4Label.Size = new System.Drawing.Size(44, 16);
            this._move4Label.TabIndex = 12;
            this._move4Label.Text = "label3";
            // 
            // _ivHPLabel
            // 
            this._ivHPLabel.AutoSize = true;
            this._ivHPLabel.Location = new System.Drawing.Point(6, 18);
            this._ivHPLabel.Name = "_ivHPLabel";
            this._ivHPLabel.Size = new System.Drawing.Size(44, 16);
            this._ivHPLabel.TabIndex = 13;
            this._ivHPLabel.Text = "label3";
            // 
            // _ivSpeedLabel
            // 
            this._ivSpeedLabel.AutoSize = true;
            this._ivSpeedLabel.Location = new System.Drawing.Point(6, 34);
            this._ivSpeedLabel.Name = "_ivSpeedLabel";
            this._ivSpeedLabel.Size = new System.Drawing.Size(44, 16);
            this._ivSpeedLabel.TabIndex = 14;
            this._ivSpeedLabel.Text = "label3";
            // 
            // _ivAttackLabel
            // 
            this._ivAttackLabel.AutoSize = true;
            this._ivAttackLabel.Location = new System.Drawing.Point(6, 50);
            this._ivAttackLabel.Name = "_ivAttackLabel";
            this._ivAttackLabel.Size = new System.Drawing.Size(44, 16);
            this._ivAttackLabel.TabIndex = 15;
            this._ivAttackLabel.Text = "label3";
            // 
            // _ivDefenseLabel
            // 
            this._ivDefenseLabel.AutoSize = true;
            this._ivDefenseLabel.Location = new System.Drawing.Point(6, 66);
            this._ivDefenseLabel.Name = "_ivDefenseLabel";
            this._ivDefenseLabel.Size = new System.Drawing.Size(44, 16);
            this._ivDefenseLabel.TabIndex = 16;
            this._ivDefenseLabel.Text = "label3";
            // 
            // _ivSpAttackLabel
            // 
            this._ivSpAttackLabel.AutoSize = true;
            this._ivSpAttackLabel.Location = new System.Drawing.Point(6, 82);
            this._ivSpAttackLabel.Name = "_ivSpAttackLabel";
            this._ivSpAttackLabel.Size = new System.Drawing.Size(44, 16);
            this._ivSpAttackLabel.TabIndex = 17;
            this._ivSpAttackLabel.Text = "label3";
            // 
            // _ivSpDefenseLabel
            // 
            this._ivSpDefenseLabel.AutoSize = true;
            this._ivSpDefenseLabel.Location = new System.Drawing.Point(6, 98);
            this._ivSpDefenseLabel.Name = "_ivSpDefenseLabel";
            this._ivSpDefenseLabel.Size = new System.Drawing.Size(44, 16);
            this._ivSpDefenseLabel.TabIndex = 18;
            this._ivSpDefenseLabel.Text = "label3";
            // 
            // _shinyLabel
            // 
            this._shinyLabel.AutoSize = true;
            this._shinyLabel.Location = new System.Drawing.Point(137, 99);
            this._shinyLabel.Name = "_shinyLabel";
            this._shinyLabel.Size = new System.Drawing.Size(44, 16);
            this._shinyLabel.TabIndex = 9;
            this._shinyLabel.Text = "label3";
            // 
            // PokemonViewerPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._shinyLabel);
            this.Controls.Add(this._itemLabel);
            this.Controls.Add(this._hiddenPowerDamage);
            this.Controls.Add(this._hiddenPowerType);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this._abilityLabel);
            this.Controls.Add(this._pokemonPictureBox);
            this.Controls.Add(this._natureLabel);
            this.Controls.Add(this._pokemonName);
            this.Name = "PokemonViewerPanel";
            this.Size = new System.Drawing.Size(750, 280);
            ((System.ComponentModel.ISupportInitialize)(this._pokemonPictureBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _pokemonName;
        private System.Windows.Forms.Label _natureLabel;
        private System.Windows.Forms.PictureBox _pokemonPictureBox;
        private System.Windows.Forms.Label _abilityLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label _hiddenPowerType;
        private System.Windows.Forms.Label _hiddenPowerDamage;
        private System.Windows.Forms.Label _itemLabel;
        private System.Windows.Forms.Label _move4Label;
        private System.Windows.Forms.Label _move3Label;
        private System.Windows.Forms.Label _move2Label;
        private System.Windows.Forms.Label _move1Label;
        private System.Windows.Forms.Label _ivSpDefenseLabel;
        private System.Windows.Forms.Label _ivSpAttackLabel;
        private System.Windows.Forms.Label _ivDefenseLabel;
        private System.Windows.Forms.Label _ivAttackLabel;
        private System.Windows.Forms.Label _ivSpeedLabel;
        private System.Windows.Forms.Label _ivHPLabel;
        private System.Windows.Forms.Label _shinyLabel;
    }
}
