namespace Bible
{
	partial class SearchForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchForm));
			this.grpSearchResults = new System.Windows.Forms.GroupBox();
			this.lblTab = new System.Windows.Forms.Label();
			this.lblWindows = new System.Windows.Forms.Label();
			this.cboTabs = new System.Windows.Forms.ComboBox();
			this.cboWindows = new System.Windows.Forms.ComboBox();
			this.lblLocationInstruction = new System.Windows.Forms.Label();
			this.grpSearch = new System.Windows.Forms.GroupBox();
			this.lblVersions = new System.Windows.Forms.Label();
			this.lstVersions = new System.Windows.Forms.ListBox();
			this.lblNewTestament = new System.Windows.Forms.Label();
			this.lstNewTestament = new System.Windows.Forms.TreeView();
			this.lblOldTestament = new System.Windows.Forms.Label();
			this.lstOldTestament = new System.Windows.Forms.TreeView();
			this.rbnMatchRegex = new System.Windows.Forms.RadioButton();
			this.rbnMatchExact = new System.Windows.Forms.RadioButton();
			this.rbnMatchAll = new System.Windows.Forms.RadioButton();
			this.rbnMatchAny = new System.Windows.Forms.RadioButton();
			this.txtSearch = new System.Windows.Forms.TextBox();
			this.lblSearch = new System.Windows.Forms.Label();
			this.btnSearch = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.grpSearchResults.SuspendLayout();
			this.grpSearch.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpSearchResults
			// 
			this.grpSearchResults.Controls.Add(this.lblTab);
			this.grpSearchResults.Controls.Add(this.lblWindows);
			this.grpSearchResults.Controls.Add(this.cboTabs);
			this.grpSearchResults.Controls.Add(this.cboWindows);
			this.grpSearchResults.Controls.Add(this.lblLocationInstruction);
			this.grpSearchResults.Dock = System.Windows.Forms.DockStyle.Top;
			this.grpSearchResults.Location = new System.Drawing.Point(5, 5);
			this.grpSearchResults.Name = "grpSearchResults";
			this.grpSearchResults.Size = new System.Drawing.Size(724, 74);
			this.grpSearchResults.TabIndex = 0;
			this.grpSearchResults.TabStop = false;
			this.grpSearchResults.Text = "Search Results Location";
			// 
			// lblTab
			// 
			this.lblTab.AutoSize = true;
			this.lblTab.Location = new System.Drawing.Point(383, 44);
			this.lblTab.Name = "lblTab";
			this.lblTab.Size = new System.Drawing.Size(29, 13);
			this.lblTab.TabIndex = 4;
			this.lblTab.Text = "Tab:";
			// 
			// lblWindows
			// 
			this.lblWindows.AutoSize = true;
			this.lblWindows.Location = new System.Drawing.Point(6, 44);
			this.lblWindows.Name = "lblWindows";
			this.lblWindows.Size = new System.Drawing.Size(49, 13);
			this.lblWindows.TabIndex = 3;
			this.lblWindows.Text = "Window:";
			// 
			// cboTabs
			// 
			this.cboTabs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTabs.FormattingEnabled = true;
			this.cboTabs.Location = new System.Drawing.Point(418, 41);
			this.cboTabs.Name = "cboTabs";
			this.cboTabs.Size = new System.Drawing.Size(300, 21);
			this.cboTabs.TabIndex = 2;
			// 
			// cboWindows
			// 
			this.cboWindows.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboWindows.FormattingEnabled = true;
			this.cboWindows.Location = new System.Drawing.Point(61, 41);
			this.cboWindows.Name = "cboWindows";
			this.cboWindows.Size = new System.Drawing.Size(300, 21);
			this.cboWindows.TabIndex = 1;
			this.cboWindows.SelectedIndexChanged += new System.EventHandler(this.cboWindows_SelectedIndexChanged);
			// 
			// lblLocationInstruction
			// 
			this.lblLocationInstruction.AutoSize = true;
			this.lblLocationInstruction.Location = new System.Drawing.Point(6, 16);
			this.lblLocationInstruction.Name = "lblLocationInstruction";
			this.lblLocationInstruction.Size = new System.Drawing.Size(360, 13);
			this.lblLocationInstruction.TabIndex = 0;
			this.lblLocationInstruction.Text = "Select the location where you would like the search results to be displayed.";
			// 
			// grpSearch
			// 
			this.grpSearch.Controls.Add(this.lblVersions);
			this.grpSearch.Controls.Add(this.lstVersions);
			this.grpSearch.Controls.Add(this.lblNewTestament);
			this.grpSearch.Controls.Add(this.lstNewTestament);
			this.grpSearch.Controls.Add(this.lblOldTestament);
			this.grpSearch.Controls.Add(this.lstOldTestament);
			this.grpSearch.Controls.Add(this.rbnMatchRegex);
			this.grpSearch.Controls.Add(this.rbnMatchExact);
			this.grpSearch.Controls.Add(this.rbnMatchAll);
			this.grpSearch.Controls.Add(this.rbnMatchAny);
			this.grpSearch.Controls.Add(this.txtSearch);
			this.grpSearch.Controls.Add(this.lblSearch);
			this.grpSearch.Dock = System.Windows.Forms.DockStyle.Top;
			this.grpSearch.Location = new System.Drawing.Point(5, 79);
			this.grpSearch.Name = "grpSearch";
			this.grpSearch.Size = new System.Drawing.Size(724, 331);
			this.grpSearch.TabIndex = 1;
			this.grpSearch.TabStop = false;
			this.grpSearch.Text = "Search Paramters";
			// 
			// lblVersions
			// 
			this.lblVersions.AutoSize = true;
			this.lblVersions.Location = new System.Drawing.Point(515, 77);
			this.lblVersions.Name = "lblVersions";
			this.lblVersions.Size = new System.Drawing.Size(94, 13);
			this.lblVersions.TabIndex = 11;
			this.lblVersions.Text = "Version to Search:";
			// 
			// lstVersions
			// 
			this.lstVersions.FormattingEnabled = true;
			this.lstVersions.Location = new System.Drawing.Point(518, 93);
			this.lstVersions.Name = "lstVersions";
			this.lstVersions.Size = new System.Drawing.Size(200, 225);
			this.lstVersions.TabIndex = 10;
			// 
			// lblNewTestament
			// 
			this.lblNewTestament.AutoSize = true;
			this.lblNewTestament.Location = new System.Drawing.Point(259, 77);
			this.lblNewTestament.Name = "lblNewTestament";
			this.lblNewTestament.Size = new System.Drawing.Size(167, 13);
			this.lblNewTestament.TabIndex = 9;
			this.lblNewTestament.Text = "New Testament Books to Search:";
			// 
			// lstNewTestament
			// 
			this.lstNewTestament.CheckBoxes = true;
			this.lstNewTestament.Location = new System.Drawing.Point(262, 93);
			this.lstNewTestament.Name = "lstNewTestament";
			this.lstNewTestament.Size = new System.Drawing.Size(250, 225);
			this.lstNewTestament.TabIndex = 8;
			this.lstNewTestament.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.AfterNodeCheck);
			// 
			// lblOldTestament
			// 
			this.lblOldTestament.AutoSize = true;
			this.lblOldTestament.Location = new System.Drawing.Point(6, 77);
			this.lblOldTestament.Name = "lblOldTestament";
			this.lblOldTestament.Size = new System.Drawing.Size(161, 13);
			this.lblOldTestament.TabIndex = 7;
			this.lblOldTestament.Text = "Old Testament Books to Search:";
			// 
			// lstOldTestament
			// 
			this.lstOldTestament.CheckBoxes = true;
			this.lstOldTestament.Location = new System.Drawing.Point(6, 93);
			this.lstOldTestament.Name = "lstOldTestament";
			this.lstOldTestament.Size = new System.Drawing.Size(250, 225);
			this.lstOldTestament.TabIndex = 6;
			this.lstOldTestament.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.AfterNodeCheck);
			// 
			// rbnMatchRegex
			// 
			this.rbnMatchRegex.AutoSize = true;
			this.rbnMatchRegex.Location = new System.Drawing.Point(525, 45);
			this.rbnMatchRegex.Name = "rbnMatchRegex";
			this.rbnMatchRegex.Size = new System.Drawing.Size(185, 17);
			this.rbnMatchRegex.TabIndex = 5;
			this.rbnMatchRegex.Text = "Use Regular Expression Matching";
			this.rbnMatchRegex.UseVisualStyleBackColor = true;
			// 
			// rbnMatchExact
			// 
			this.rbnMatchExact.AutoSize = true;
			this.rbnMatchExact.Location = new System.Drawing.Point(375, 45);
			this.rbnMatchExact.Name = "rbnMatchExact";
			this.rbnMatchExact.Size = new System.Drawing.Size(121, 17);
			this.rbnMatchExact.TabIndex = 4;
			this.rbnMatchExact.Text = "Match Exact Phrase";
			this.rbnMatchExact.UseVisualStyleBackColor = true;
			// 
			// rbnMatchAll
			// 
			this.rbnMatchAll.AutoSize = true;
			this.rbnMatchAll.Location = new System.Drawing.Point(225, 45);
			this.rbnMatchAll.Name = "rbnMatchAll";
			this.rbnMatchAll.Size = new System.Drawing.Size(103, 17);
			this.rbnMatchAll.TabIndex = 3;
			this.rbnMatchAll.Text = "Match All Words";
			this.rbnMatchAll.UseVisualStyleBackColor = true;
			// 
			// rbnMatchAny
			// 
			this.rbnMatchAny.AutoSize = true;
			this.rbnMatchAny.Checked = true;
			this.rbnMatchAny.Location = new System.Drawing.Point(75, 45);
			this.rbnMatchAny.Name = "rbnMatchAny";
			this.rbnMatchAny.Size = new System.Drawing.Size(105, 17);
			this.rbnMatchAny.TabIndex = 2;
			this.rbnMatchAny.TabStop = true;
			this.rbnMatchAny.Text = "Match Any Word";
			this.rbnMatchAny.UseVisualStyleBackColor = true;
			// 
			// txtSearch
			// 
			this.txtSearch.Location = new System.Drawing.Point(78, 19);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Size = new System.Drawing.Size(640, 20);
			this.txtSearch.TabIndex = 1;
			// 
			// lblSearch
			// 
			this.lblSearch.AutoSize = true;
			this.lblSearch.Location = new System.Drawing.Point(10, 22);
			this.lblSearch.Name = "lblSearch";
			this.lblSearch.Size = new System.Drawing.Size(62, 13);
			this.lblSearch.TabIndex = 0;
			this.lblSearch.Text = "Search For:";
			// 
			// btnSearch
			// 
			this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnSearch.Location = new System.Drawing.Point(570, 416);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(75, 23);
			this.btnSearch.TabIndex = 2;
			this.btnSearch.Text = "Search";
			this.btnSearch.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(651, 416);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// SearchForm
			// 
			this.AcceptButton = this.btnSearch;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(734, 446);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnSearch);
			this.Controls.Add(this.grpSearch);
			this.Controls.Add(this.grpSearchResults);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "SearchForm";
			this.Padding = new System.Windows.Forms.Padding(5);
			this.ShowInTaskbar = false;
			this.Text = "Bible Search";
			this.Load += new System.EventHandler(this.SearchForm_Load);
			this.grpSearchResults.ResumeLayout(false);
			this.grpSearchResults.PerformLayout();
			this.grpSearch.ResumeLayout(false);
			this.grpSearch.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox grpSearchResults;
		private System.Windows.Forms.Label lblTab;
		private System.Windows.Forms.Label lblWindows;
		private System.Windows.Forms.ComboBox cboTabs;
		private System.Windows.Forms.ComboBox cboWindows;
		private System.Windows.Forms.Label lblLocationInstruction;
		private System.Windows.Forms.GroupBox grpSearch;
		private System.Windows.Forms.RadioButton rbnMatchRegex;
		private System.Windows.Forms.RadioButton rbnMatchExact;
		private System.Windows.Forms.RadioButton rbnMatchAll;
		private System.Windows.Forms.RadioButton rbnMatchAny;
		private System.Windows.Forms.TextBox txtSearch;
		private System.Windows.Forms.Label lblSearch;
		private System.Windows.Forms.Label lblOldTestament;
		private System.Windows.Forms.TreeView lstOldTestament;
		private System.Windows.Forms.Label lblNewTestament;
		private System.Windows.Forms.TreeView lstNewTestament;
		private System.Windows.Forms.Label lblVersions;
		private System.Windows.Forms.ListBox lstVersions;
		private System.Windows.Forms.Button btnSearch;
		private System.Windows.Forms.Button btnCancel;
	}
}