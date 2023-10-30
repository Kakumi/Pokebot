namespace Pokebot.Panels
{
    partial class StarterControl
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
            this._starterBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // _starterBox
            // 
            this._starterBox.FormattingEnabled = true;
            this._starterBox.Location = new System.Drawing.Point(3, 3);
            this._starterBox.Name = "_starterBox";
            this._starterBox.Size = new System.Drawing.Size(121, 24);
            this._starterBox.TabIndex = 1;
            this._starterBox.SelectedIndexChanged += new System.EventHandler(this.StarterSelectionChanged);
            // 
            // StarterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._starterBox);
            this.Name = "StarterControl";
            this.Size = new System.Drawing.Size(490, 280);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox _starterBox;
    }
}
