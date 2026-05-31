namespace Pokebot.Panels
{
    partial class PathControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code genere par le Concepteur de composants

        private void InitializeComponent()
        {
            this._filterPanel = new System.Windows.Forms.Panel();
            this._pathPreviewLabel = new System.Windows.Forms.Label();
            this._upButton = new System.Windows.Forms.Button();
            this._leftButton = new System.Windows.Forms.Button();
            this._downButton = new System.Windows.Forms.Button();
            this._rightButton = new System.Windows.Forms.Button();
            this._aButton = new System.Windows.Forms.Button();
            this._clearButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _filterPanel
            // 
            this._filterPanel.Location = new System.Drawing.Point(2, 52);
            this._filterPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._filterPanel.Name = "_filterPanel";
            this._filterPanel.Size = new System.Drawing.Size(558, 173);
            this._filterPanel.TabIndex = 0;
            // 
            // _pathPreviewLabel
            // 
            this._pathPreviewLabel.AutoSize = true;
            this._pathPreviewLabel.Location = new System.Drawing.Point(2, 6);
            this._pathPreviewLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._pathPreviewLabel.Name = "_pathPreviewLabel";
            this._pathPreviewLabel.Size = new System.Drawing.Size(29, 13);
            this._pathPreviewLabel.TabIndex = 1;
            this._pathPreviewLabel.Text = "Path";
            // 
            // _upButton
            // 
            this._upButton.Location = new System.Drawing.Point(199, 24);
            this._upButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._upButton.Name = "_upButton";
            this._upButton.Size = new System.Drawing.Size(61, 23);
            this._upButton.TabIndex = 2;
            this._upButton.Text = "Up";
            this._upButton.UseVisualStyleBackColor = true;
            this._upButton.Click += new System.EventHandler(this._upButton_Click);
            // 
            // _leftButton
            // 
            this._leftButton.Location = new System.Drawing.Point(5, 24);
            this._leftButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._leftButton.Name = "_leftButton";
            this._leftButton.Size = new System.Drawing.Size(61, 23);
            this._leftButton.TabIndex = 3;
            this._leftButton.Text = "Left";
            this._leftButton.UseVisualStyleBackColor = true;
            this._leftButton.Click += new System.EventHandler(this._leftButton_Click);
            // 
            // _downButton
            // 
            this._downButton.Location = new System.Drawing.Point(135, 24);
            this._downButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._downButton.Name = "_downButton";
            this._downButton.Size = new System.Drawing.Size(60, 23);
            this._downButton.TabIndex = 4;
            this._downButton.Text = "Down";
            this._downButton.UseVisualStyleBackColor = true;
            this._downButton.Click += new System.EventHandler(this._downButton_Click);
            // 
            // _rightButton
            // 
            this._rightButton.Location = new System.Drawing.Point(70, 24);
            this._rightButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._rightButton.Name = "_rightButton";
            this._rightButton.Size = new System.Drawing.Size(61, 23);
            this._rightButton.TabIndex = 5;
            this._rightButton.Text = "Right";
            this._rightButton.UseVisualStyleBackColor = true;
            this._rightButton.Click += new System.EventHandler(this._rightButton_Click);
            // 
            // _aButton
            // 
            this._aButton.Location = new System.Drawing.Point(264, 24);
            this._aButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._aButton.Name = "_aButton";
            this._aButton.Size = new System.Drawing.Size(60, 23);
            this._aButton.TabIndex = 6;
            this._aButton.Text = "A";
            this._aButton.UseVisualStyleBackColor = true;
            this._aButton.Click += new System.EventHandler(this._aButton_Click);
            // 
            // _clearButton
            // 
            this._clearButton.Location = new System.Drawing.Point(328, 24);
            this._clearButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._clearButton.Name = "_clearButton";
            this._clearButton.Size = new System.Drawing.Size(60, 23);
            this._clearButton.TabIndex = 7;
            this._clearButton.Text = "Clear";
            this._clearButton.UseVisualStyleBackColor = true;
            this._clearButton.Click += new System.EventHandler(this._clearButton_Click);
            // 
            // PathControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._clearButton);
            this.Controls.Add(this._aButton);
            this.Controls.Add(this._rightButton);
            this.Controls.Add(this._downButton);
            this.Controls.Add(this._leftButton);
            this.Controls.Add(this._upButton);
            this.Controls.Add(this._pathPreviewLabel);
            this.Controls.Add(this._filterPanel);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "PathControl";
            this.Size = new System.Drawing.Size(562, 228);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel _filterPanel;
        private System.Windows.Forms.Label _pathPreviewLabel;
        private System.Windows.Forms.Button _upButton;
        private System.Windows.Forms.Button _leftButton;
        private System.Windows.Forms.Button _downButton;
        private System.Windows.Forms.Button _rightButton;
        private System.Windows.Forms.Button _aButton;
        private System.Windows.Forms.Button _clearButton;
    }
}
