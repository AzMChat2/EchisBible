namespace Bible
{
	partial class OptionsForm
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
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Preferred Version");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Version Header");
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Book Header");
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Chapter Header");
			System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Section Header");
			System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Verse Number");
			System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Normal Text");
			System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Concordance Text");
			System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Not in Original Language");
			System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Words of Christ");
			System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Main Display", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10});
			System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Dictionary Header");
			System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Dictionary Example");
			System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Normal Text");
			System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Hebrew");
			System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Greek");
			System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Dictionary Display", new System.Windows.Forms.TreeNode[] {
            treeNode12,
            treeNode13,
            treeNode14,
            treeNode15,
            treeNode16});
			System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Fonts and Colors", new System.Windows.Forms.TreeNode[] {
            treeNode11,
            treeNode17});
			System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Preferences", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode18});
			System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("File Locations");
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
			this.colorDialog = new System.Windows.Forms.ColorDialog();
			this.treeView = new System.Windows.Forms.TreeView();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.grpPreferredVersion = new System.Windows.Forms.GroupBox();
			this.grpSelectedVersion = new System.Windows.Forms.GroupBox();
			this.txtVersionCopyright = new System.Windows.Forms.TextBox();
			this.lblVersionCopyright = new System.Windows.Forms.Label();
			this.txtVersionAbrev = new System.Windows.Forms.TextBox();
			this.lblVersionAbreviation = new System.Windows.Forms.Label();
			this.txtVersionName = new System.Windows.Forms.TextBox();
			this.lblVersionName = new System.Windows.Forms.Label();
			this.cboPreferredVersion = new System.Windows.Forms.ComboBox();
			this.lblPreferredVersion = new System.Windows.Forms.Label();
			this.lblPrefVersionDescription = new System.Windows.Forms.Label();
			this.grpFileLocations = new System.Windows.Forms.GroupBox();
			this.grpAudio = new System.Windows.Forms.GroupBox();
			this.btnAudio = new System.Windows.Forms.Button();
			this.txtAudioPath = new System.Windows.Forms.TextBox();
			this.lblAudioPath = new System.Windows.Forms.Label();
			this.lblAudioDescription = new System.Windows.Forms.Label();
			this.grpNotes = new System.Windows.Forms.GroupBox();
			this.btnNotes = new System.Windows.Forms.Button();
			this.txtNotesPath = new System.Windows.Forms.TextBox();
			this.lblNotesPath = new System.Windows.Forms.Label();
			this.lblNotesDescription = new System.Windows.Forms.Label();
			this.grpTextStyle = new System.Windows.Forms.GroupBox();
			this.pnlSample = new System.Windows.Forms.Panel();
			this.webSample = new System.Windows.Forms.WebBrowser();
			this.grpAlignment = new System.Windows.Forms.GroupBox();
			this.rbnRight = new System.Windows.Forms.RadioButton();
			this.rbnCenter = new System.Windows.Forms.RadioButton();
			this.rbnLeft = new System.Windows.Forms.RadioButton();
			this.grpColor = new System.Windows.Forms.GroupBox();
			this.btnHighlite = new System.Windows.Forms.Button();
			this.lblHighlight = new System.Windows.Forms.Label();
			this.cboHighlite = new System.Windows.Forms.ComboBox();
			this.btnColor = new System.Windows.Forms.Button();
			this.lblColor = new System.Windows.Forms.Label();
			this.cboColor = new System.Windows.Forms.ComboBox();
			this.lblSampleText = new System.Windows.Forms.Label();
			this.grpFontAttributes = new System.Windows.Forms.GroupBox();
			this.chkSuper = new System.Windows.Forms.CheckBox();
			this.chkUnderline = new System.Windows.Forms.CheckBox();
			this.chkItalics = new System.Windows.Forms.CheckBox();
			this.chkBold = new System.Windows.Forms.CheckBox();
			this.lblFontSize = new System.Windows.Forms.Label();
			this.lblFont = new System.Windows.Forms.Label();
			this.cboSize = new System.Windows.Forms.ComboBox();
			this.cboFont = new System.Windows.Forms.ComboBox();
			this.chkVisible = new System.Windows.Forms.CheckBox();
			this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.btnReset = new System.Windows.Forms.Button();
			this.grpPreferredVersion.SuspendLayout();
			this.grpSelectedVersion.SuspendLayout();
			this.grpFileLocations.SuspendLayout();
			this.grpAudio.SuspendLayout();
			this.grpNotes.SuspendLayout();
			this.grpTextStyle.SuspendLayout();
			this.pnlSample.SuspendLayout();
			this.grpAlignment.SuspendLayout();
			this.grpColor.SuspendLayout();
			this.grpFontAttributes.SuspendLayout();
			this.SuspendLayout();
			// 
			// colorDialog
			// 
			this.colorDialog.SolidColorOnly = true;
			// 
			// treeView
			// 
			this.treeView.Location = new System.Drawing.Point(12, 12);
			this.treeView.Name = "treeView";
			treeNode1.Name = "nodeVersion";
			treeNode1.Text = "Preferred Version";
			treeNode1.ToolTipText = "Preferred Version";
			treeNode2.Name = "nodeVersionHeader";
			treeNode2.Text = "Version Header";
			treeNode2.ToolTipText = "Version Header";
			treeNode3.Name = "nodeBookHeader";
			treeNode3.Text = "Book Header";
			treeNode3.ToolTipText = "Book Header";
			treeNode4.Name = "nodeChapterHeader";
			treeNode4.Text = "Chapter Header";
			treeNode4.ToolTipText = "Chapter Header";
			treeNode5.Name = "nodeSectionHeader";
			treeNode5.Text = "Section Header";
			treeNode5.ToolTipText = "Section Header";
			treeNode6.Name = "nodeVerseNumber";
			treeNode6.Text = "Verse Number";
			treeNode6.ToolTipText = "Verse Number";
			treeNode7.Name = "nodeNormalText";
			treeNode7.Text = "Normal Text";
			treeNode7.ToolTipText = "Normal Bible Text";
			treeNode8.Name = "nodeConcordance";
			treeNode8.Text = "Concordance Text";
			treeNode8.ToolTipText = "Concordance Text";
			treeNode9.Name = "nodeNotInOriginal";
			treeNode9.Text = "Not in Original Language";
			treeNode9.ToolTipText = "Not in Original Language Text";
			treeNode10.Name = "nodeWordsOfChrist";
			treeNode10.Text = "Words of Christ";
			treeNode10.ToolTipText = "Words of Christ";
			treeNode11.Name = "nodeMainDisplay";
			treeNode11.Text = "Main Display";
			treeNode11.ToolTipText = "Main Display";
			treeNode12.Name = "nodeDictionaryHeader";
			treeNode12.Text = "Dictionary Header";
			treeNode12.ToolTipText = "Dictionary Header";
			treeNode13.Name = "nodeDictionaryExample";
			treeNode13.Text = "Dictionary Example";
			treeNode13.ToolTipText = "Dictionary Example";
			treeNode14.Name = "nodeDictionaryText";
			treeNode14.Text = "Normal Text";
			treeNode14.ToolTipText = "Normal Dictionary Text";
			treeNode15.Name = "nodeHebrewText";
			treeNode15.Text = "Hebrew";
			treeNode15.ToolTipText = "Hebrew";
			treeNode16.Name = "nodeGreekText";
			treeNode16.Text = "Greek";
			treeNode16.ToolTipText = "Greek";
			treeNode17.Name = "nodeDictionary";
			treeNode17.Text = "Dictionary Display";
			treeNode17.ToolTipText = "Dictionary Display";
			treeNode18.Name = "nodeFontsAndColors";
			treeNode18.Text = "Fonts and Colors";
			treeNode18.ToolTipText = "Fonts and Colors";
			treeNode19.Name = "nodePreferences";
			treeNode19.Text = "Preferences";
			treeNode19.ToolTipText = "User Preferences";
			treeNode20.Name = "nodeFileLocations";
			treeNode20.Text = "File Locations";
			treeNode20.ToolTipText = "File Locations";
			this.treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode19,
            treeNode20});
			this.treeView.ShowNodeToolTips = true;
			this.treeView.Size = new System.Drawing.Size(225, 315);
			this.treeView.TabIndex = 0;
			this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
			// 
			// btnOk
			// 
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(424, 336);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 4;
			this.btnOk.Text = "Ok";
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(505, 336);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// grpPreferredVersion
			// 
			this.grpPreferredVersion.Controls.Add(this.grpSelectedVersion);
			this.grpPreferredVersion.Controls.Add(this.cboPreferredVersion);
			this.grpPreferredVersion.Controls.Add(this.lblPreferredVersion);
			this.grpPreferredVersion.Controls.Add(this.lblPrefVersionDescription);
			this.grpPreferredVersion.Location = new System.Drawing.Point(243, 12);
			this.grpPreferredVersion.Name = "grpPreferredVersion";
			this.grpPreferredVersion.Size = new System.Drawing.Size(339, 315);
			this.grpPreferredVersion.TabIndex = 1;
			this.grpPreferredVersion.TabStop = false;
			this.grpPreferredVersion.Text = "Preferred Version";
			this.grpPreferredVersion.Visible = false;
			// 
			// grpSelectedVersion
			// 
			this.grpSelectedVersion.Controls.Add(this.txtVersionCopyright);
			this.grpSelectedVersion.Controls.Add(this.lblVersionCopyright);
			this.grpSelectedVersion.Controls.Add(this.txtVersionAbrev);
			this.grpSelectedVersion.Controls.Add(this.lblVersionAbreviation);
			this.grpSelectedVersion.Controls.Add(this.txtVersionName);
			this.grpSelectedVersion.Controls.Add(this.lblVersionName);
			this.grpSelectedVersion.Location = new System.Drawing.Point(9, 151);
			this.grpSelectedVersion.Name = "grpSelectedVersion";
			this.grpSelectedVersion.Size = new System.Drawing.Size(322, 150);
			this.grpSelectedVersion.TabIndex = 3;
			this.grpSelectedVersion.TabStop = false;
			this.grpSelectedVersion.Text = "Selected Version Information";
			// 
			// txtVersionCopyright
			// 
			this.txtVersionCopyright.Location = new System.Drawing.Point(6, 98);
			this.txtVersionCopyright.Multiline = true;
			this.txtVersionCopyright.Name = "txtVersionCopyright";
			this.txtVersionCopyright.ReadOnly = true;
			this.txtVersionCopyright.Size = new System.Drawing.Size(310, 46);
			this.txtVersionCopyright.TabIndex = 5;
			// 
			// lblVersionCopyright
			// 
			this.lblVersionCopyright.AutoSize = true;
			this.lblVersionCopyright.Location = new System.Drawing.Point(7, 82);
			this.lblVersionCopyright.Name = "lblVersionCopyright";
			this.lblVersionCopyright.Size = new System.Drawing.Size(92, 13);
			this.lblVersionCopyright.TabIndex = 4;
			this.lblVersionCopyright.Text = "Version Copyright:";
			// 
			// txtVersionAbrev
			// 
			this.txtVersionAbrev.Location = new System.Drawing.Point(116, 53);
			this.txtVersionAbrev.Name = "txtVersionAbrev";
			this.txtVersionAbrev.ReadOnly = true;
			this.txtVersionAbrev.Size = new System.Drawing.Size(200, 20);
			this.txtVersionAbrev.TabIndex = 3;
			// 
			// lblVersionAbreviation
			// 
			this.lblVersionAbreviation.AutoSize = true;
			this.lblVersionAbreviation.Location = new System.Drawing.Point(9, 56);
			this.lblVersionAbreviation.Name = "lblVersionAbreviation";
			this.lblVersionAbreviation.Size = new System.Drawing.Size(101, 13);
			this.lblVersionAbreviation.TabIndex = 2;
			this.lblVersionAbreviation.Text = "Version Abreviation:";
			// 
			// txtVersionName
			// 
			this.txtVersionName.Location = new System.Drawing.Point(116, 27);
			this.txtVersionName.Name = "txtVersionName";
			this.txtVersionName.ReadOnly = true;
			this.txtVersionName.Size = new System.Drawing.Size(200, 20);
			this.txtVersionName.TabIndex = 1;
			// 
			// lblVersionName
			// 
			this.lblVersionName.AutoSize = true;
			this.lblVersionName.Location = new System.Drawing.Point(34, 30);
			this.lblVersionName.Name = "lblVersionName";
			this.lblVersionName.Size = new System.Drawing.Size(76, 13);
			this.lblVersionName.TabIndex = 0;
			this.lblVersionName.Text = "Version Name:";
			// 
			// cboPreferredVersion
			// 
			this.cboPreferredVersion.FormattingEnabled = true;
			this.cboPreferredVersion.Location = new System.Drawing.Point(165, 106);
			this.cboPreferredVersion.Name = "cboPreferredVersion";
			this.cboPreferredVersion.Size = new System.Drawing.Size(121, 21);
			this.cboPreferredVersion.TabIndex = 2;
			this.cboPreferredVersion.SelectedIndexChanged += new System.EventHandler(this.cboPreferredVersion_SelectedIndexChanged);
			// 
			// lblPreferredVersion
			// 
			this.lblPreferredVersion.AutoSize = true;
			this.lblPreferredVersion.Location = new System.Drawing.Point(35, 109);
			this.lblPreferredVersion.Name = "lblPreferredVersion";
			this.lblPreferredVersion.Size = new System.Drawing.Size(124, 13);
			this.lblPreferredVersion.TabIndex = 1;
			this.lblPreferredVersion.Text = "Select Preferred Version:";
			// 
			// lblPrefVersionDescription
			// 
			this.lblPrefVersionDescription.Location = new System.Drawing.Point(6, 16);
			this.lblPrefVersionDescription.Name = "lblPrefVersionDescription";
			this.lblPrefVersionDescription.Size = new System.Drawing.Size(325, 73);
			this.lblPrefVersionDescription.TabIndex = 0;
			this.lblPrefVersionDescription.Text = resources.GetString("lblPrefVersionDescription.Text");
			// 
			// grpFileLocations
			// 
			this.grpFileLocations.Controls.Add(this.grpAudio);
			this.grpFileLocations.Controls.Add(this.grpNotes);
			this.grpFileLocations.Location = new System.Drawing.Point(243, 12);
			this.grpFileLocations.Name = "grpFileLocations";
			this.grpFileLocations.Size = new System.Drawing.Size(339, 315);
			this.grpFileLocations.TabIndex = 2;
			this.grpFileLocations.TabStop = false;
			this.grpFileLocations.Text = "File Locations";
			this.grpFileLocations.Visible = false;
			// 
			// grpAudio
			// 
			this.grpAudio.Controls.Add(this.btnAudio);
			this.grpAudio.Controls.Add(this.txtAudioPath);
			this.grpAudio.Controls.Add(this.lblAudioPath);
			this.grpAudio.Controls.Add(this.lblAudioDescription);
			this.grpAudio.Location = new System.Drawing.Point(6, 164);
			this.grpAudio.Name = "grpAudio";
			this.grpAudio.Size = new System.Drawing.Size(325, 145);
			this.grpAudio.TabIndex = 1;
			this.grpAudio.TabStop = false;
			this.grpAudio.Text = "Audio Files";
			// 
			// btnAudio
			// 
			this.btnAudio.Location = new System.Drawing.Point(294, 109);
			this.btnAudio.Name = "btnAudio";
			this.btnAudio.Size = new System.Drawing.Size(25, 23);
			this.btnAudio.TabIndex = 3;
			this.btnAudio.Text = "...";
			this.btnAudio.UseVisualStyleBackColor = true;
			this.btnAudio.Click += new System.EventHandler(this.btnAudio_Click);
			// 
			// txtAudioPath
			// 
			this.txtAudioPath.Location = new System.Drawing.Point(9, 111);
			this.txtAudioPath.Name = "txtAudioPath";
			this.txtAudioPath.Size = new System.Drawing.Size(279, 20);
			this.txtAudioPath.TabIndex = 2;
			// 
			// lblAudioPath
			// 
			this.lblAudioPath.AutoSize = true;
			this.lblAudioPath.Location = new System.Drawing.Point(6, 95);
			this.lblAudioPath.Name = "lblAudioPath";
			this.lblAudioPath.Size = new System.Drawing.Size(105, 13);
			this.lblAudioPath.TabIndex = 1;
			this.lblAudioPath.Text = "Audio Files Location:";
			// 
			// lblAudioDescription
			// 
			this.lblAudioDescription.Location = new System.Drawing.Point(6, 16);
			this.lblAudioDescription.Name = "lblAudioDescription";
			this.lblAudioDescription.Size = new System.Drawing.Size(313, 66);
			this.lblAudioDescription.TabIndex = 0;
			this.lblAudioDescription.Text = resources.GetString("lblAudioDescription.Text");
			// 
			// grpNotes
			// 
			this.grpNotes.Controls.Add(this.btnNotes);
			this.grpNotes.Controls.Add(this.txtNotesPath);
			this.grpNotes.Controls.Add(this.lblNotesPath);
			this.grpNotes.Controls.Add(this.lblNotesDescription);
			this.grpNotes.Location = new System.Drawing.Point(6, 28);
			this.grpNotes.Name = "grpNotes";
			this.grpNotes.Size = new System.Drawing.Size(325, 117);
			this.grpNotes.TabIndex = 0;
			this.grpNotes.TabStop = false;
			this.grpNotes.Text = "Notes Files";
			// 
			// btnNotes
			// 
			this.btnNotes.Location = new System.Drawing.Point(294, 83);
			this.btnNotes.Name = "btnNotes";
			this.btnNotes.Size = new System.Drawing.Size(25, 23);
			this.btnNotes.TabIndex = 3;
			this.btnNotes.Text = "...";
			this.btnNotes.UseVisualStyleBackColor = true;
			this.btnNotes.Click += new System.EventHandler(this.btnNotes_Click);
			// 
			// txtNotesPath
			// 
			this.txtNotesPath.Location = new System.Drawing.Point(9, 85);
			this.txtNotesPath.Name = "txtNotesPath";
			this.txtNotesPath.Size = new System.Drawing.Size(279, 20);
			this.txtNotesPath.TabIndex = 2;
			// 
			// lblNotesPath
			// 
			this.lblNotesPath.AutoSize = true;
			this.lblNotesPath.Location = new System.Drawing.Point(6, 69);
			this.lblNotesPath.Name = "lblNotesPath";
			this.lblNotesPath.Size = new System.Drawing.Size(119, 13);
			this.lblNotesPath.TabIndex = 1;
			this.lblNotesPath.Text = "Default Notes Location:";
			// 
			// lblNotesDescription
			// 
			this.lblNotesDescription.Location = new System.Drawing.Point(6, 16);
			this.lblNotesDescription.Name = "lblNotesDescription";
			this.lblNotesDescription.Size = new System.Drawing.Size(313, 44);
			this.lblNotesDescription.TabIndex = 0;
			this.lblNotesDescription.Text = "The Echis Bible Study application allows you to associate notes to specific chapt" +
					"ers.  The notes path is the default location where these notes will be saved.";
			// 
			// grpTextStyle
			// 
			this.grpTextStyle.Controls.Add(this.pnlSample);
			this.grpTextStyle.Controls.Add(this.grpAlignment);
			this.grpTextStyle.Controls.Add(this.grpColor);
			this.grpTextStyle.Controls.Add(this.lblSampleText);
			this.grpTextStyle.Controls.Add(this.grpFontAttributes);
			this.grpTextStyle.Controls.Add(this.lblFontSize);
			this.grpTextStyle.Controls.Add(this.lblFont);
			this.grpTextStyle.Controls.Add(this.cboSize);
			this.grpTextStyle.Controls.Add(this.cboFont);
			this.grpTextStyle.Controls.Add(this.chkVisible);
			this.grpTextStyle.Location = new System.Drawing.Point(243, 12);
			this.grpTextStyle.Name = "grpTextStyle";
			this.grpTextStyle.Size = new System.Drawing.Size(339, 315);
			this.grpTextStyle.TabIndex = 3;
			this.grpTextStyle.TabStop = false;
			this.grpTextStyle.Text = "Text Style";
			this.grpTextStyle.Visible = false;
			// 
			// pnlSample
			// 
			this.pnlSample.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlSample.Controls.Add(this.webSample);
			this.pnlSample.Location = new System.Drawing.Point(6, 208);
			this.pnlSample.Name = "pnlSample";
			this.pnlSample.Size = new System.Drawing.Size(325, 100);
			this.pnlSample.TabIndex = 9;
			// 
			// webSample
			// 
			this.webSample.Dock = System.Windows.Forms.DockStyle.Fill;
			this.webSample.Location = new System.Drawing.Point(0, 0);
			this.webSample.MinimumSize = new System.Drawing.Size(20, 20);
			this.webSample.Name = "webSample";
			this.webSample.Size = new System.Drawing.Size(321, 96);
			this.webSample.TabIndex = 0;
			// 
			// grpAlignment
			// 
			this.grpAlignment.Controls.Add(this.rbnRight);
			this.grpAlignment.Controls.Add(this.rbnCenter);
			this.grpAlignment.Controls.Add(this.rbnLeft);
			this.grpAlignment.Location = new System.Drawing.Point(101, 157);
			this.grpAlignment.Name = "grpAlignment";
			this.grpAlignment.Size = new System.Drawing.Size(230, 41);
			this.grpAlignment.TabIndex = 7;
			this.grpAlignment.TabStop = false;
			this.grpAlignment.Text = "Text Alignment";
			// 
			// rbnRight
			// 
			this.rbnRight.AutoSize = true;
			this.rbnRight.Location = new System.Drawing.Point(168, 18);
			this.rbnRight.Name = "rbnRight";
			this.rbnRight.Size = new System.Drawing.Size(50, 17);
			this.rbnRight.TabIndex = 2;
			this.rbnRight.TabStop = true;
			this.rbnRight.Text = "Right";
			this.rbnRight.UseVisualStyleBackColor = true;
			this.rbnRight.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
			// 
			// rbnCenter
			// 
			this.rbnCenter.AutoSize = true;
			this.rbnCenter.Location = new System.Drawing.Point(81, 18);
			this.rbnCenter.Name = "rbnCenter";
			this.rbnCenter.Size = new System.Drawing.Size(56, 17);
			this.rbnCenter.TabIndex = 1;
			this.rbnCenter.TabStop = true;
			this.rbnCenter.Text = "Center";
			this.rbnCenter.UseVisualStyleBackColor = true;
			this.rbnCenter.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
			// 
			// rbnLeft
			// 
			this.rbnLeft.AutoSize = true;
			this.rbnLeft.Location = new System.Drawing.Point(6, 18);
			this.rbnLeft.Name = "rbnLeft";
			this.rbnLeft.Size = new System.Drawing.Size(43, 17);
			this.rbnLeft.TabIndex = 0;
			this.rbnLeft.TabStop = true;
			this.rbnLeft.Text = "Left";
			this.rbnLeft.UseVisualStyleBackColor = true;
			this.rbnLeft.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
			// 
			// grpColor
			// 
			this.grpColor.Controls.Add(this.btnHighlite);
			this.grpColor.Controls.Add(this.lblHighlight);
			this.grpColor.Controls.Add(this.cboHighlite);
			this.grpColor.Controls.Add(this.btnColor);
			this.grpColor.Controls.Add(this.lblColor);
			this.grpColor.Controls.Add(this.cboColor);
			this.grpColor.Location = new System.Drawing.Point(101, 69);
			this.grpColor.Name = "grpColor";
			this.grpColor.Size = new System.Drawing.Size(230, 82);
			this.grpColor.TabIndex = 6;
			this.grpColor.TabStop = false;
			this.grpColor.Text = "Font Colors";
			// 
			// btnHighlite
			// 
			this.btnHighlite.Location = new System.Drawing.Point(199, 47);
			this.btnHighlite.Name = "btnHighlite";
			this.btnHighlite.Size = new System.Drawing.Size(25, 23);
			this.btnHighlite.TabIndex = 5;
			this.btnHighlite.Text = "...";
			this.btnHighlite.UseVisualStyleBackColor = true;
			this.btnHighlite.Click += new System.EventHandler(this.btnHighlite_Click);
			// 
			// lblHighlight
			// 
			this.lblHighlight.AutoSize = true;
			this.lblHighlight.Location = new System.Drawing.Point(6, 52);
			this.lblHighlight.Name = "lblHighlight";
			this.lblHighlight.Size = new System.Drawing.Size(69, 13);
			this.lblHighlight.TabIndex = 3;
			this.lblHighlight.Text = "Highlite Color";
			// 
			// cboHighlite
			// 
			this.cboHighlite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboHighlite.FormattingEnabled = true;
			this.cboHighlite.Items.AddRange(new object[] {
            "8",
            "9",
            "10",
            "11",
            "12",
            "14",
            "16",
            "18",
            "20",
            "22",
            "24",
            "28",
            "32",
            "36",
            "40"});
			this.cboHighlite.Location = new System.Drawing.Point(81, 49);
			this.cboHighlite.Name = "cboHighlite";
			this.cboHighlite.Size = new System.Drawing.Size(112, 21);
			this.cboHighlite.TabIndex = 4;
			this.cboHighlite.SelectedIndexChanged += new System.EventHandler(this.cboHighlite_SelectedIndexChanged);
			// 
			// btnColor
			// 
			this.btnColor.Location = new System.Drawing.Point(199, 20);
			this.btnColor.Name = "btnColor";
			this.btnColor.Size = new System.Drawing.Size(25, 23);
			this.btnColor.TabIndex = 2;
			this.btnColor.Text = "...";
			this.btnColor.UseVisualStyleBackColor = true;
			this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
			// 
			// lblColor
			// 
			this.lblColor.AutoSize = true;
			this.lblColor.Location = new System.Drawing.Point(20, 25);
			this.lblColor.Name = "lblColor";
			this.lblColor.Size = new System.Drawing.Size(55, 13);
			this.lblColor.TabIndex = 0;
			this.lblColor.Text = "Text Color";
			// 
			// cboColor
			// 
			this.cboColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboColor.FormattingEnabled = true;
			this.cboColor.Items.AddRange(new object[] {
            "8",
            "9",
            "10",
            "11",
            "12",
            "14",
            "16",
            "18",
            "20",
            "22",
            "24",
            "28",
            "32",
            "36",
            "40"});
			this.cboColor.Location = new System.Drawing.Point(81, 22);
			this.cboColor.Name = "cboColor";
			this.cboColor.Size = new System.Drawing.Size(112, 21);
			this.cboColor.TabIndex = 1;
			this.cboColor.SelectedIndexChanged += new System.EventHandler(this.cboColor_SelectedIndexChanged);
			// 
			// lblSampleText
			// 
			this.lblSampleText.AutoSize = true;
			this.lblSampleText.Location = new System.Drawing.Point(2, 191);
			this.lblSampleText.Name = "lblSampleText";
			this.lblSampleText.Size = new System.Drawing.Size(66, 13);
			this.lblSampleText.TabIndex = 8;
			this.lblSampleText.Text = "Sample Text";
			// 
			// grpFontAttributes
			// 
			this.grpFontAttributes.Controls.Add(this.chkSuper);
			this.grpFontAttributes.Controls.Add(this.chkUnderline);
			this.grpFontAttributes.Controls.Add(this.chkItalics);
			this.grpFontAttributes.Controls.Add(this.chkBold);
			this.grpFontAttributes.Location = new System.Drawing.Point(6, 69);
			this.grpFontAttributes.Name = "grpFontAttributes";
			this.grpFontAttributes.Size = new System.Drawing.Size(89, 109);
			this.grpFontAttributes.TabIndex = 5;
			this.grpFontAttributes.TabStop = false;
			this.grpFontAttributes.Text = "Effects";
			// 
			// chkSuper
			// 
			this.chkSuper.AutoSize = true;
			this.chkSuper.Location = new System.Drawing.Point(6, 88);
			this.chkSuper.Name = "chkSuper";
			this.chkSuper.Size = new System.Drawing.Size(79, 17);
			this.chkSuper.TabIndex = 3;
			this.chkSuper.Text = "Superscript";
			this.chkSuper.UseVisualStyleBackColor = true;
			this.chkSuper.CheckedChanged += new System.EventHandler(this.chkSuper_CheckedChanged);
			// 
			// chkUnderline
			// 
			this.chkUnderline.AutoSize = true;
			this.chkUnderline.Location = new System.Drawing.Point(6, 65);
			this.chkUnderline.Name = "chkUnderline";
			this.chkUnderline.Size = new System.Drawing.Size(71, 17);
			this.chkUnderline.TabIndex = 2;
			this.chkUnderline.Text = "Underline";
			this.chkUnderline.UseVisualStyleBackColor = true;
			this.chkUnderline.CheckedChanged += new System.EventHandler(this.chkUnderline_CheckedChanged);
			// 
			// chkItalics
			// 
			this.chkItalics.AutoSize = true;
			this.chkItalics.Location = new System.Drawing.Point(6, 42);
			this.chkItalics.Name = "chkItalics";
			this.chkItalics.Size = new System.Drawing.Size(53, 17);
			this.chkItalics.TabIndex = 1;
			this.chkItalics.Text = "Italics";
			this.chkItalics.UseVisualStyleBackColor = true;
			this.chkItalics.CheckedChanged += new System.EventHandler(this.chkItalics_CheckedChanged);
			// 
			// chkBold
			// 
			this.chkBold.AutoSize = true;
			this.chkBold.Location = new System.Drawing.Point(6, 19);
			this.chkBold.Name = "chkBold";
			this.chkBold.Size = new System.Drawing.Size(47, 17);
			this.chkBold.TabIndex = 0;
			this.chkBold.Text = "Bold";
			this.chkBold.UseVisualStyleBackColor = true;
			this.chkBold.CheckedChanged += new System.EventHandler(this.chkBold_CheckedChanged);
			// 
			// lblFontSize
			// 
			this.lblFontSize.AutoSize = true;
			this.lblFontSize.Location = new System.Drawing.Point(245, 45);
			this.lblFontSize.Name = "lblFontSize";
			this.lblFontSize.Size = new System.Drawing.Size(27, 13);
			this.lblFontSize.TabIndex = 3;
			this.lblFontSize.Text = "Size";
			// 
			// lblFont
			// 
			this.lblFont.AutoSize = true;
			this.lblFont.Location = new System.Drawing.Point(6, 45);
			this.lblFont.Name = "lblFont";
			this.lblFont.Size = new System.Drawing.Size(28, 13);
			this.lblFont.TabIndex = 1;
			this.lblFont.Text = "Font";
			// 
			// cboSize
			// 
			this.cboSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSize.FormattingEnabled = true;
			this.cboSize.Items.AddRange(new object[] {
            "8",
            "9",
            "10",
            "11",
            "12",
            "14",
            "16",
            "18",
            "20",
            "22",
            "24",
            "28",
            "32",
            "36",
            "40"});
			this.cboSize.Location = new System.Drawing.Point(278, 42);
			this.cboSize.Name = "cboSize";
			this.cboSize.Size = new System.Drawing.Size(55, 21);
			this.cboSize.TabIndex = 4;
			this.cboSize.SelectedIndexChanged += new System.EventHandler(this.cboSize_SelectedIndexChanged);
			// 
			// cboFont
			// 
			this.cboFont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboFont.FormattingEnabled = true;
			this.cboFont.Location = new System.Drawing.Point(40, 42);
			this.cboFont.Name = "cboFont";
			this.cboFont.Size = new System.Drawing.Size(199, 21);
			this.cboFont.TabIndex = 2;
			this.cboFont.SelectedIndexChanged += new System.EventHandler(this.cboFont_SelectedIndexChanged);
			// 
			// chkVisible
			// 
			this.chkVisible.AutoSize = true;
			this.chkVisible.Location = new System.Drawing.Point(6, 19);
			this.chkVisible.Name = "chkVisible";
			this.chkVisible.Size = new System.Drawing.Size(134, 17);
			this.chkVisible.TabIndex = 0;
			this.chkVisible.Text = "Show [TextStyleName]";
			this.chkVisible.UseVisualStyleBackColor = true;
			this.chkVisible.CheckedChanged += new System.EventHandler(this.chkVisible_CheckedChanged);
			// 
			// folderBrowserDialog
			// 
			this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.Personal;
			// 
			// btnReset
			// 
			this.btnReset.Location = new System.Drawing.Point(243, 336);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(95, 23);
			this.btnReset.TabIndex = 6;
			this.btnReset.Text = "Reset Options";
			this.btnReset.UseVisualStyleBackColor = true;
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// OptionsForm
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(594, 373);
			this.Controls.Add(this.btnReset);
			this.Controls.Add(this.grpTextStyle);
			this.Controls.Add(this.grpFileLocations);
			this.Controls.Add(this.grpPreferredVersion);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.treeView);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OptionsForm";
			this.Text = "OptionsForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OptionsForm_FormClosing);
			this.grpPreferredVersion.ResumeLayout(false);
			this.grpPreferredVersion.PerformLayout();
			this.grpSelectedVersion.ResumeLayout(false);
			this.grpSelectedVersion.PerformLayout();
			this.grpFileLocations.ResumeLayout(false);
			this.grpAudio.ResumeLayout(false);
			this.grpAudio.PerformLayout();
			this.grpNotes.ResumeLayout(false);
			this.grpNotes.PerformLayout();
			this.grpTextStyle.ResumeLayout(false);
			this.grpTextStyle.PerformLayout();
			this.pnlSample.ResumeLayout(false);
			this.grpAlignment.ResumeLayout(false);
			this.grpAlignment.PerformLayout();
			this.grpColor.ResumeLayout(false);
			this.grpColor.PerformLayout();
			this.grpFontAttributes.ResumeLayout(false);
			this.grpFontAttributes.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ColorDialog colorDialog;
		private System.Windows.Forms.TreeView treeView;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.GroupBox grpPreferredVersion;
		private System.Windows.Forms.Label lblPrefVersionDescription;
		private System.Windows.Forms.GroupBox grpSelectedVersion;
		private System.Windows.Forms.TextBox txtVersionAbrev;
		private System.Windows.Forms.Label lblVersionAbreviation;
		private System.Windows.Forms.TextBox txtVersionName;
		private System.Windows.Forms.Label lblVersionName;
		private System.Windows.Forms.ComboBox cboPreferredVersion;
		private System.Windows.Forms.Label lblPreferredVersion;
		private System.Windows.Forms.TextBox txtVersionCopyright;
		private System.Windows.Forms.Label lblVersionCopyright;
		private System.Windows.Forms.GroupBox grpFileLocations;
		private System.Windows.Forms.GroupBox grpTextStyle;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
		private System.Windows.Forms.GroupBox grpAudio;
		private System.Windows.Forms.GroupBox grpNotes;
		private System.Windows.Forms.Button btnNotes;
		private System.Windows.Forms.TextBox txtNotesPath;
		private System.Windows.Forms.Label lblNotesPath;
		private System.Windows.Forms.Label lblNotesDescription;
		private System.Windows.Forms.Button btnAudio;
		private System.Windows.Forms.TextBox txtAudioPath;
		private System.Windows.Forms.Label lblAudioPath;
		private System.Windows.Forms.Label lblAudioDescription;
		private System.Windows.Forms.CheckBox chkVisible;
		private System.Windows.Forms.ComboBox cboFont;
		private System.Windows.Forms.Label lblFontSize;
		private System.Windows.Forms.Label lblFont;
		private System.Windows.Forms.ComboBox cboSize;
		private System.Windows.Forms.GroupBox grpFontAttributes;
		private System.Windows.Forms.CheckBox chkBold;
		private System.Windows.Forms.CheckBox chkItalics;
		private System.Windows.Forms.CheckBox chkSuper;
		private System.Windows.Forms.CheckBox chkUnderline;
		private System.Windows.Forms.Label lblSampleText;
		private System.Windows.Forms.GroupBox grpColor;
		private System.Windows.Forms.GroupBox grpAlignment;
		private System.Windows.Forms.Button btnHighlite;
		private System.Windows.Forms.Label lblHighlight;
		private System.Windows.Forms.ComboBox cboHighlite;
		private System.Windows.Forms.Button btnColor;
		private System.Windows.Forms.Label lblColor;
		private System.Windows.Forms.ComboBox cboColor;
		private System.Windows.Forms.RadioButton rbnRight;
		private System.Windows.Forms.RadioButton rbnCenter;
		private System.Windows.Forms.RadioButton rbnLeft;
		private System.Windows.Forms.Panel pnlSample;
		private System.Windows.Forms.WebBrowser webSample;
		private System.Windows.Forms.Button btnReset;
	}
}