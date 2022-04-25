namespace Bible
{
	partial class ChapterDisplay
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChapterDisplay));
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.cboVersions = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.cboBooks = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.cboChapters = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.btnPreviousBook = new System.Windows.Forms.ToolStripButton();
			this.btnPrevChapter = new System.Windows.Forms.ToolStripButton();
			this.btnNextChapter = new System.Windows.Forms.ToolStripButton();
			this.btnNextBook = new System.Windows.Forms.ToolStripButton();
			this.closeTabToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.pnlDisplay = new System.Windows.Forms.Panel();
			this.webDisplay = new System.Windows.Forms.WebBrowser();
			this.toolStrip.SuspendLayout();
			this.pnlDisplay.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip
			// 
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cboVersions,
            this.toolStripSeparator1,
            this.cboBooks,
            this.toolStripSeparator2,
            this.cboChapters,
            this.toolStripSeparator3,
            this.btnPreviousBook,
            this.btnPrevChapter,
            this.btnNextChapter,
            this.btnNextBook,
            this.closeTabToolStripButton});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(871, 25);
			this.toolStrip.TabIndex = 0;
			this.toolStrip.Text = "toolStrip1";
			// 
			// cboVersions
			// 
			this.cboVersions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboVersions.Name = "cboVersions";
			this.cboVersions.Size = new System.Drawing.Size(121, 25);
			this.cboVersions.SelectedIndexChanged += new System.EventHandler(this.cboVersions_SelectedIndexChanged);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// cboBooks
			// 
			this.cboBooks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboBooks.Name = "cboBooks";
			this.cboBooks.Size = new System.Drawing.Size(121, 25);
			this.cboBooks.SelectedIndexChanged += new System.EventHandler(this.cboBooks_SelectedIndexChanged);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// cboChapters
			// 
			this.cboChapters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboChapters.Name = "cboChapters";
			this.cboChapters.Size = new System.Drawing.Size(121, 25);
			this.cboChapters.SelectedIndexChanged += new System.EventHandler(this.cboChapters_SelectedIndexChanged);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// btnPreviousBook
			// 
			this.btnPreviousBook.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnPreviousBook.Image = ((System.Drawing.Image)(resources.GetObject("btnPreviousBook.Image")));
			this.btnPreviousBook.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnPreviousBook.Name = "btnPreviousBook";
			this.btnPreviousBook.Size = new System.Drawing.Size(23, 22);
			this.btnPreviousBook.ToolTipText = "Previous Book";
			this.btnPreviousBook.Click += new System.EventHandler(this.btnPreviousBook_Click);
			// 
			// btnPrevChapter
			// 
			this.btnPrevChapter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnPrevChapter.Image = ((System.Drawing.Image)(resources.GetObject("btnPrevChapter.Image")));
			this.btnPrevChapter.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnPrevChapter.Name = "btnPrevChapter";
			this.btnPrevChapter.Size = new System.Drawing.Size(23, 22);
			this.btnPrevChapter.ToolTipText = "Previous Chapter";
			this.btnPrevChapter.Click += new System.EventHandler(this.btnPrevChapter_Click);
			// 
			// btnNextChapter
			// 
			this.btnNextChapter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnNextChapter.Image = ((System.Drawing.Image)(resources.GetObject("btnNextChapter.Image")));
			this.btnNextChapter.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnNextChapter.Name = "btnNextChapter";
			this.btnNextChapter.Size = new System.Drawing.Size(23, 22);
			this.btnNextChapter.ToolTipText = "Next Chapter";
			this.btnNextChapter.Click += new System.EventHandler(this.btnNextChapter_Click);
			// 
			// btnNextBook
			// 
			this.btnNextBook.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnNextBook.Image = ((System.Drawing.Image)(resources.GetObject("btnNextBook.Image")));
			this.btnNextBook.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnNextBook.Name = "btnNextBook";
			this.btnNextBook.Size = new System.Drawing.Size(23, 22);
			this.btnNextBook.ToolTipText = "Next Book";
			this.btnNextBook.Click += new System.EventHandler(this.btnNextBook_Click);
			// 
			// closeTabToolStripButton
			// 
			this.closeTabToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.closeTabToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.closeTabToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("closeTabToolStripButton.Image")));
			this.closeTabToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.closeTabToolStripButton.Name = "closeTabToolStripButton";
			this.closeTabToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.closeTabToolStripButton.Text = "Close Tab";
			this.closeTabToolStripButton.Click += new System.EventHandler(this.closeTabToolStripButton_Click);
			// 
			// pnlDisplay
			// 
			this.pnlDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlDisplay.Controls.Add(this.webDisplay);
			this.pnlDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlDisplay.Location = new System.Drawing.Point(0, 25);
			this.pnlDisplay.Name = "pnlDisplay";
			this.pnlDisplay.Size = new System.Drawing.Size(871, 585);
			this.pnlDisplay.TabIndex = 1;
			// 
			// webDisplay
			// 
			this.webDisplay.AllowWebBrowserDrop = false;
			this.webDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
			this.webDisplay.Location = new System.Drawing.Point(0, 0);
			this.webDisplay.MinimumSize = new System.Drawing.Size(20, 20);
			this.webDisplay.Name = "webDisplay";
			this.webDisplay.ScriptErrorsSuppressed = true;
			this.webDisplay.Size = new System.Drawing.Size(867, 581);
			this.webDisplay.TabIndex = 0;
			// 
			// ChapterDisplay
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pnlDisplay);
			this.Controls.Add(this.toolStrip);
			this.Name = "ChapterDisplay";
			this.Size = new System.Drawing.Size(871, 610);
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.pnlDisplay.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripComboBox cboVersions;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripComboBox cboBooks;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripComboBox cboChapters;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton btnPrevChapter;
		private System.Windows.Forms.ToolStripButton btnNextChapter;
		private System.Windows.Forms.ToolStripButton btnPreviousBook;
		private System.Windows.Forms.ToolStripButton btnNextBook;
		private System.Windows.Forms.Panel pnlDisplay;
		private System.Windows.Forms.WebBrowser webDisplay;
		private System.Windows.Forms.ToolStripButton closeTabToolStripButton;
	}
}
