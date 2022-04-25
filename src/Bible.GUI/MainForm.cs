using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Drawing.Text;
using System.Diagnostics.CodeAnalysis;
using Bible.Presentation;

namespace Bible
{
	public partial class MainForm : Form, IMainView, INotesView
	{
		private const string UntitledNote = "Untitled";

		private int _notesWidth;
		private int _definitionsHeight;
		private int _dictionaryWidth;
		private float _dictionaryPctWidth;

		private NotesController _notesController;
		private RtfPrintDocument _printDocument;
		private SplashForm _splash;

		public MainController Controller { get; private set; }

		public MainForm(SplashForm splash)
		{
			_splash = splash;
			_notesController = new NotesController(this);
			Controller = new MainController(this, _notesController);

			InitializeComponent();

			RestoreWindowState();
			LoadFonts();
			InitializePrintDocument();
			InitializeStatusText();

			_notesController.LoadNotes();

			webStrongs.ObjectForScripting = Controller.ObjectForScripting;
		}

		private void InitializeStatusText()
		{
			Version version = BibleStore.Versions.Find(item => item.VersionId == UserPreferences.I.PreferredVersion);
			if (version != null)
			{
				lblStatus.Text = "Loading " + version.LongName;
			}
		}

		private void InitializePrintDocument()
		{
			_printDocument = new RtfPrintDocument();
			printDialog.Document = _printDocument;
			printPreviewDialog.Document = _printDocument;
			pageSetupDialog.Document = _printDocument;
		}

		private void LoadFonts()
		{
			InstalledFontCollection fonts = new InstalledFontCollection();

			foreach (FontFamily font in fonts.Families)
			{
				cboFonts.ComboBox.Items.Add(font.Name);
			}

			cboFonts.ComboBox.Text = UserPreferences.I.LastFont == null ? "Arial" : UserPreferences.I.LastFont;
			cboFontSizes.ComboBox.Text = UserPreferences.I.LastFontSize == null ? "12" : UserPreferences.I.LastFontSize;
			txtNotes.Font = txtNotes.SelectionFont;
		}

		private void RestoreWindowState()
		{
			WindowState = UserPreferences.I.MainFormWindowState;
			Size = new Size(UserPreferences.I.MainFormWidth, UserPreferences.I.MainFormHeight);

			RestoreDefinitionsState();

			pnlNotes.Width = UserPreferences.I.NotesWidth;
			notesMenuItem.Checked = (pnlNotes.Width != 0);
			splitterNotes.Enabled = notesMenuItem.Checked;

			toolStripBrowsers.Visible = UserPreferences.I.ToolStripVisible;
			toolBarMenuItem.Checked = UserPreferences.I.ToolStripVisible;

			statusStrip.Visible = UserPreferences.I.StatusStripVisible;
			statusBarMenuItem.Checked = UserPreferences.I.StatusStripVisible;

		}

		private void RestoreDefinitionsState()
		{
			pnlDefinitions.Height = UserPreferences.I.DefinitionsHeight;
			pnlDictionary.Width = UserPreferences.I.DictionaryWidth;

			if (pnlDictionary.Width > 0 && pnlDictionary.Width < (pnlDefinitions.Width - splitterStrongsDictionary.Width))
			{
				if (pnlDefinitions.Height != 0)
				{
					dictionaryMenuItem.Checked = true;
					strongsMenuItem.Checked = true;
				}
				_dictionaryWidth = pnlDictionary.Width;
				_dictionaryPctWidth = Convert.ToSingle(pnlDictionary.Width) / Convert.ToSingle(pnlDefinitions.Width);
			}
			else
			{
				if (pnlDefinitions.Height != 0)
				{
					if (pnlDictionary.Width == 0)
					{
						strongsMenuItem.Checked = true;
					}
					else
					{
						dictionaryMenuItem.Checked = true;
					}
				}

				_dictionaryWidth = pnlDefinitions.Width / 2;
				_dictionaryPctWidth = 0.5f;
			}

			splitterDefinitions.Enabled = (dictionaryMenuItem.Checked || strongsMenuItem.Checked);
		}

		#region Notes
		private void StartNewNotes(object sender, EventArgs e)
		{
			_notesController.CreateNewNotes();
		}

		private void OpenNotes(object sender, EventArgs e)
		{
			openFileDialog.InitialDirectory = UserPreferences.I.NotesPath;
			openFileDialog.Filter = "Bible Notes|*.ebn";
			openFileDialog.FileName = string.Empty;

			if (openFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				_notesController.OpenNotes(openFileDialog.FileName);
			}
		}

		private void SaveNotesAs(object sender, EventArgs e)
		{
			_notesController.SaveNotes(true);
		}
		
		private void SaveNotes(object sender, EventArgs e)
		{
			_notesController.SaveNotes(false);
		}

		private void ExportNotes(object sender, EventArgs e)
		{
			saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			saveFileDialog.Filter = "Rich Text Documents|*.rtf";
			saveFileDialog.DefaultExt = "rtf";
			saveFileDialog.Title = "Export Notes";

			if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				_notesController.ExportNotes(saveFileDialog.FileName);
			}
		}

		public bool PromptForNotesFileName(NotesCollection notes)
		{
			bool retVal = false;

			saveFileDialog.InitialDirectory = Path.GetDirectoryName(notes.FileName);
			saveFileDialog.Filter = "Bible Notes|*.ebn";
			saveFileDialog.DefaultExt = "ebn";
			saveFileDialog.Title = "Save Notes";
			// saveFileDialog.FileName = note.FileName;

			if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				notes.FileName = saveFileDialog.FileName;
				retVal = true;
			}

			return retVal;
		}

		private void NotesPrintPreview(object sender, EventArgs e)
		{
			UseWaitCursor = true;
			_printDocument.RtfText = _notesController.ConvertNotes();

			Rectangle R = ClientRectangle;
			printPreviewDialog.SetBounds(R.Left, R.Top, R.Width, R.Height - 10);

			printPreviewDialog.ShowDialog(this);
			UseWaitCursor = false;
		}

		private void NotesPrint(object sender, EventArgs e)
		{
			UseWaitCursor = true;
			_printDocument.RtfText = _notesController.ConvertNotes();
			printDialog.ShowDialog(this);
			UseWaitCursor = false;
		}

		private void NotesPrintSetup(object sender, EventArgs e)
		{
			UseWaitCursor = true;
			_printDocument.RtfText = _notesController.ConvertNotes();
			pageSetupDialog.ShowDialog(this);
			UseWaitCursor = false;
		}

		private void NotesUndo(object sender, EventArgs e)
		{
			if (txtNotes.CanUndo) txtNotes.Undo();
		}

		private void NotesRedo(object sender, EventArgs e)
		{
			if (txtNotes.CanRedo) txtNotes.Redo();
		}

		private void NotesSelectAll(object sender, EventArgs e)
		{
			txtNotes.SelectAll();
		}

		private void NotesCut(object sender, EventArgs e)
		{
			txtNotes.Cut();
		}

		private void NotesCopy(object sender, EventArgs e)
		{
			txtNotes.Copy();
		}

		private void NotesPaste(object sender, EventArgs e)
		{
			txtNotes.Paste();
		}

		private void btnCloseNotes_Click(object sender, EventArgs e)
		{
			_notesController.CloseNotes();
		}

		public bool PromptToSaveNote(NotesCollection note)
		{
			bool retVal = true;

			string msg = string.Format(CultureInfo.CurrentCulture, "{0} Notes has not been saved.\r\nSave now?", note.DisplayName);
			DialogResult result = MessageBox.Show(this, msg, "Save Notes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

			if (result == DialogResult.Cancel)
			{
				retVal = false;
			}
			else if (result == DialogResult.Yes)
			{
				retVal = _notesController.SaveNotes(false, note);
			}

			return retVal;
		}

		private bool loadingNotesCombo;
		public void PopulateNotesCombo()
		{
			loadingNotesCombo = true;

			cboNotes.ComboBox.DataSource = null;
			cboNotes.ComboBox.DisplayMember = "DisplayName";
			cboNotes.ComboBox.ValueMember = "FileName";
			cboNotes.ComboBox.DataSource = _notesController.LoadedNotes;
			cboNotes.ComboBox.Text = _notesController.ActiveNotes.DisplayName;

			loadingNotesCombo = false;
		}

		public void AddVerseRefToNotes(int verseNumber)
		{
			// TODO: Version Name,
			// TODO: Check for previous inclusions nearby.

			Clipboard.SetText(string.Format(CultureInfo.InvariantCulture, "[{0}]", verseNumber));
			txtNotes.Paste();
		}

		[SuppressMessage("Microsoft.Design", "CA1031")]
		private void ChangeFont(object sender, EventArgs e)
		{
			try
			{
				float size = float.Parse(cboFontSizes.Text, CultureInfo.InvariantCulture);
				FontStyle style = FontStyle.Regular;

				if (boldButton.Checked) style |= FontStyle.Bold;
				if (italicsButton.Checked) style |= FontStyle.Italic;
				if (underlineButton.Checked) style |= FontStyle.Underline;

				txtNotes.SelectionFont = new Font(cboFonts.ComboBox.Text, size, style);
			}
			catch { }
		}

		public bool IsNotesEmpty
		{
			get { return string.IsNullOrEmpty(txtNotes.Text); }
		}

		public string RtfText
		{
			get { return txtNotes.Rtf; }
			set
			{
				txtNotes.Clear();
				txtNotes.Rtf = value;
				txtNotes.ClearUndo();
				ChangeFont(null, null);
			}
		}

		#endregion

		private void Exit(object sender, EventArgs e)
		{
			Close();
		}

		#region Window Menu Item Handlers
		private void Cascade(object sender, EventArgs e)
		{
			LayoutMdi(MdiLayout.Cascade);
		}

		private void TileVertical(object sender, EventArgs e)
		{
			LayoutMdi(MdiLayout.TileVertical);
		}

		private void TileHorizontal(object sender, EventArgs e)
		{
			LayoutMdi(MdiLayout.TileHorizontal);
		}

		private void ArrangeIcons(object sender, EventArgs e)
		{
			LayoutMdi(MdiLayout.ArrangeIcons);
		}

		private void CloseAllWindows(object sender, EventArgs e)
		{
			foreach (Form childForm in MdiChildren)
			{
				childForm.Close();
			}
		}
		#endregion

		#region View Menu Item Handlers
		private void ShowToolBar(object sender, EventArgs e)
		{
			toolStripBrowsers.Visible = toolBarMenuItem.Checked;
		}

		private void ShowStatusBar(object sender, EventArgs e)
		{
			statusStrip.Visible = statusBarMenuItem.Checked;
		}

		private void ShowNotesWindow(object sender, EventArgs e)
		{
			MainForm_ResizeBegin(sender, e);

			if (notesMenuItem.Checked)
			{
				if (_notesWidth == 0)
				{
					pnlNotes.Width = (ClientRectangle.Width * 4) / 10;
				}
				else
				{
					pnlNotes.Width = _notesWidth;
				}
				splitterNotes.Enabled = true;
			}
			else
			{
				splitterNotes.Enabled = false;
				_notesWidth = pnlNotes.Width;
				pnlNotes.Width = 0;
			}

			MainForm_Resize(sender, e);
		}

		private void ShowStrongsWindow(object sender, EventArgs e)
		{
			SetDefinitionsHeight(strongsMenuItem.Checked, dictionaryMenuItem.Checked);
		}

		private void ShowDictionaryWindow(object sender, EventArgs e)
		{
			SetDefinitionsHeight(dictionaryMenuItem.Checked, strongsMenuItem.Checked);
		}
		#endregion

		private void SetDefinitionsHeight(bool changedValue, bool unchangedValue)
		{
			if (!unchangedValue)
			{
				if (changedValue)
				{
					if (_definitionsHeight == 0)
					{
						pnlDefinitions.Height = (ClientRectangle.Height / 5);
					}
					else
					{
						pnlDefinitions.Height = _definitionsHeight;
					}
					splitterDefinitions.Enabled = true;
				}
				else
				{
					splitterDefinitions.Enabled = false;
					_definitionsHeight = pnlDefinitions.Height;
					pnlDefinitions.Height = 0;
				}
			}

			ResizeDefinitions();
		}

		private void ResizeDefinitions()
		{
			bool strongsVisible = strongsMenuItem.Checked;
			bool dictionaryVisible = dictionaryMenuItem.Checked;

			if (strongsVisible || dictionaryVisible)
			{
				if (strongsVisible && dictionaryVisible)
				{
					pnlDictionary.Width = _dictionaryWidth;
				}
				else if (strongsVisible)
				{
					_dictionaryWidth = pnlDictionary.Width;
					pnlDictionary.Width = 0;
				}
				else
				{
					_dictionaryWidth = pnlDictionary.Width;
					pnlDictionary.Width = (pnlDefinitions.Width - splitterStrongsDictionary.Width);
				}
			}
		}

		private void MainForm_ResizeBegin(object sender, EventArgs e)
		{
			bool strongsVisible = strongsMenuItem.Checked;
			bool dictionaryVisible = dictionaryMenuItem.Checked;

			if (strongsVisible && dictionaryVisible)
			{
				_dictionaryPctWidth = Convert.ToSingle(pnlDictionary.Width) / Convert.ToSingle(pnlDefinitions.Width);
			}
			else if (_dictionaryWidth != 0)
			{
				_dictionaryPctWidth = Convert.ToSingle(_dictionaryWidth) / Convert.ToSingle(pnlDefinitions.Width);
			}
		}

		private void MainForm_Resize(object sender, EventArgs e)
		{
			bool strongsVisible = strongsMenuItem.Checked;
			bool dictionaryVisible = dictionaryMenuItem.Checked;

			_dictionaryWidth = Convert.ToInt32(pnlDefinitions.Width * _dictionaryPctWidth);

			if (strongsVisible && dictionaryVisible)
			{
				pnlDictionary.Width = _dictionaryWidth;
			}
			else
			{
				if (strongsVisible || dictionaryVisible)
				{
					pnlDictionary.Width = strongsVisible ? 0 : (pnlDefinitions.Width - splitterStrongsDictionary.Width);
				}
				else
				{
					pnlDictionary.Width = _dictionaryWidth;
				}
			}
		}

		public void PreferredVersionLoaded()
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(PreferredVersionLoaded));
			}
			else
			{
				lblStatus.Text = "Ready";
			}
		}

		private void OpenNewBibleBrowser(object sender, EventArgs e)
		{
			DisplayForm form = new DisplayForm(this, Controller);
			form.WindowState = FormWindowState.Maximized;
			form.Show();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			PopulateNotesCombo();
			OpenNewBibleBrowser(sender, e);

			_splash.Close();
			_splash.Dispose();
			_splash = null;
		}

		private void ShowOptions(object sender, EventArgs e)
		{
			OptionsForm.ShowOptions(this);
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (_notesController.OkToClose())
			{
				SaveWindowState();
				SaveNotesState();
			}
			else
			{
				e.Cancel = true;
			}
		}

		private void SaveNotesState()
		{
			UserPreferences.I.Notes.Clear();
			_notesController.LoadedNotes.RemoveAll(item => item.Count == 0);
			_notesController.LoadedNotes.ForEach(item => UserPreferences.I.Notes.Add(item.FileName));
			if (_notesController.ActiveNotes.Count == 0)
			{
				if (_notesController.LoadedNotes.Count == 0)
				{
					UserPreferences.I.ActiveNotes = null;
				}
				else
				{
					UserPreferences.I.ActiveNotes = _notesController.LoadedNotes[0].FileName;
				}
			}
			else
			{
				UserPreferences.I.ActiveNotes = _notesController.ActiveNotes.FileName;
			}

			UserPreferences.I.LastFont = cboFonts.ComboBox.Text;
			UserPreferences.I.LastFontSize = cboFontSizes.ComboBox.Text;
		}

		private void SaveWindowState()
		{
			if (WindowState != FormWindowState.Minimized)
			{
				UserPreferences.I.MainFormWindowState = WindowState;
				UserPreferences.I.MainFormWidth = Width;
				UserPreferences.I.MainFormHeight = Height;
			}

			UserPreferences.I.NotesWidth = pnlNotes.Width;
			UserPreferences.I.DefinitionsHeight = pnlDefinitions.Height;
			UserPreferences.I.DictionaryWidth = pnlDictionary.Width;
			UserPreferences.I.ToolStripVisible = toolStripBrowsers.Visible;
			UserPreferences.I.StatusStripVisible = statusStrip.Visible;
		}

		public void DisplayStrongsEntry(int strongsId, int versionId)
		{
			if (strongsId != 0)
			{
				Strongs strongs = BibleStore.Strongs.Find(item => item.StrongsId == strongsId);

				if ((strongs != null) && (strongs.CrossRefs.Count == 0))
				{
					BibleStore.BibleService.LoadCrossRefs(strongs);
				}

				webStrongs.DocumentText = StrongsRenderer.RenderDefinition(strongs, versionId);
			}
		}

		public void DisplayCrossRef(int versionId, int bookId, int chapterId, int verseId)
		{
			Version version = BibleStore.Versions.Find(item => item.VersionId == versionId);
			Book book = BibleStore.Books.Find(item => item.BookId == bookId);

			if ((version != null) && (book != null))
			{
				Chapter chapter = book.Chapters.Find(item => item.ChapterId == chapterId);
				if (chapter != null)
				{
					Verse verse = chapter.Verses.Find(item => item.VerseId == verseId);
					IDisplayView view = (IDisplayView)ActiveMdiChild;
					view.ShowChapterInNewTab(version, book, chapter, verse);
				}
			}
		}

		public void DisplayDefinition(string word)
		{
			if (!string.IsNullOrEmpty(word))
			{
				if (BibleStore.Dictionary.ContainsKey(word))
				{
					webDictionary.DocumentText = DictionaryRenderer.RenderDefinition(BibleStore.Dictionary[word]);
				}
				else
				{
					List<string> variations = MainController.GetWordVariations(word);
					foreach (string variation in variations)
					{
						if (BibleStore.Dictionary.ContainsKey(variation))
						{
							webDictionary.DocumentText = DictionaryRenderer.RenderDefinition(BibleStore.Dictionary[variation]);
							break;
						}
					}
				}
			}
		}

		private void cboNotes_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!loadingNotesCombo)
			{
				string selected = (string)cboNotes.ComboBox.SelectedValue;
				_notesController.ChangeNotes(selected);
			}
		}

		private void aboutMenuItem_Click(object sender, EventArgs e)
		{
			AboutBox.ShowAboutBox(this, "Echis Bible");
		}


		public DialogResult ShowMessage(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			return MessageBox.Show(this, message, caption, buttons, icon);
		}

		private void SearchBible(object sender, EventArgs e)
		{
			SearchForm.ShowSearchDialog(this);
		}

		private void previewBibleButton_Click(object sender, EventArgs e)
		{
			DisplayForm form = ActiveMdiChild as DisplayForm;
			if (form != null)
			{
				form.PreviewChapter();
			}
		}

		private void printBibleButton_Click(object sender, EventArgs e)
		{
			DisplayForm form = ActiveMdiChild as DisplayForm;
			if (form != null)
			{
				form.PrintChapter();
			}
		}

		private static Random random = new Random();

		private void ViewRandomVerse(object sender, EventArgs e)
		{
			Version version = BibleStore.Versions.Find(item => item.VersionId == UserPreferences.I.PreferredVersion);

			Book book = BibleStore.Books[random.Next(BibleStore.Books.Count)];
			Chapter chapter = book.Chapters[random.Next(book.Chapters.Count)];
			Verse verse = chapter.Verses[random.Next(chapter.Verses.Count)];

			IDisplayView view = (IDisplayView)ActiveMdiChild;
			view.ShowChapterInNewTab(version, book, chapter, verse);
		}
	}
}
