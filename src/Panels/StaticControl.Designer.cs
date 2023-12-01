namespace Pokebot.Panels
{
    partial class StaticControl
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
            this._filterPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // _filterPanel
            // 
            this._filterPanel.Location = new System.Drawing.Point(3, 3);
            this._filterPanel.Name = "_filterPanel";
            this._filterPanel.Size = new System.Drawing.Size(744, 274);
            this._filterPanel.TabIndex = 2;
            // 
            // StarterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._filterPanel);
            this.Name = "StarterControl";
            this.Size = new System.Drawing.Size(750, 280);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel _filterPanel;
    }
}
