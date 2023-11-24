namespace Pokebot.Panels
{
    partial class LogsPanel
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
            this._logsListView = new System.Windows.Forms.ListView();
            this.level = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.message = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // _logsListView
            // 
            this._logsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.level,
            this.message});
            this._logsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._logsListView.GridLines = true;
            this._logsListView.HideSelection = false;
            this._logsListView.Location = new System.Drawing.Point(0, 0);
            this._logsListView.MultiSelect = false;
            this._logsListView.Name = "_logsListView";
            this._logsListView.Size = new System.Drawing.Size(768, 341);
            this._logsListView.TabIndex = 1;
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
            // LogsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._logsListView);
            this.Name = "LogsPanel";
            this.Size = new System.Drawing.Size(768, 341);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView _logsListView;
        private System.Windows.Forms.ColumnHeader level;
        private System.Windows.Forms.ColumnHeader message;
    }
}
