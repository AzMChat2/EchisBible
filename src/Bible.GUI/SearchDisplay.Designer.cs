namespace Bible
{
	partial class SearchDisplay
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchDisplay));
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.closeTabButton = new System.Windows.Forms.ToolStripButton();
			this.webDisplay = new System.Windows.Forms.WebBrowser();
			this.toolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip
			// 
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeTabButton});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(669, 25);
			this.toolStrip.TabIndex = 0;
			this.toolStrip.Text = "toolStrip1";
			// 
			// closeTabButton
			// 
			this.closeTabButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.closeTabButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.closeTabButton.Image = ((System.Drawing.Image)(resources.GetObject("closeTabButton.Image")));
			this.closeTabButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.closeTabButton.Name = "closeTabButton";
			this.closeTabButton.Size = new System.Drawing.Size(23, 22);
			this.closeTabButton.Text = "toolStripButton1";
			this.closeTabButton.Click += new System.EventHandler(this.closeTabButton_Click);
			// 
			// webDisplay
			// 
			this.webDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
			this.webDisplay.Location = new System.Drawing.Point(0, 25);
			this.webDisplay.MinimumSize = new System.Drawing.Size(20, 20);
			this.webDisplay.Name = "webDisplay";
			this.webDisplay.Size = new System.Drawing.Size(669, 551);
			this.webDisplay.TabIndex = 1;
			// 
			// SearchDisplay
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.webDisplay);
			this.Controls.Add(this.toolStrip);
			this.Name = "SearchDisplay";
			this.Size = new System.Drawing.Size(669, 576);
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripButton closeTabButton;
		private System.Windows.Forms.WebBrowser webDisplay;
	}
}
