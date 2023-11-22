namespace Pokebot.Panels
{
    partial class PartyPokemonViewer
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
            this._panelViewer = new System.Windows.Forms.Panel();
            this._partyBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // _panelViewer
            // 
            this._panelViewer.Location = new System.Drawing.Point(3, 33);
            this._panelViewer.Name = "_panelViewer";
            this._panelViewer.Size = new System.Drawing.Size(762, 305);
            this._panelViewer.TabIndex = 3;
            // 
            // _partyBox
            // 
            this._partyBox.FormattingEnabled = true;
            this._partyBox.Location = new System.Drawing.Point(3, 3);
            this._partyBox.Name = "_partyBox";
            this._partyBox.Size = new System.Drawing.Size(121, 24);
            this._partyBox.TabIndex = 2;
            this._partyBox.SelectedIndexChanged += new System.EventHandler(this._partyBox_SelectedIndexChanged);
            // 
            // PartyPokemonViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._panelViewer);
            this.Controls.Add(this._partyBox);
            this.Name = "PartyPokemonViewer";
            this.Size = new System.Drawing.Size(768, 341);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _panelViewer;
        private System.Windows.Forms.ComboBox _partyBox;
    }
}
