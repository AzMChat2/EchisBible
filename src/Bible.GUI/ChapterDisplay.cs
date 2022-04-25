using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using Bible.Presentation;

namespace Bible
{
	public partial class ChapterDisplay : UserControl, INotifyPropertyChanged, IChapterView
	{
		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected IDisplayView ParentPage { get; private set; }
		protected MainController ParentController { get; private set; }
		protected ChapterController Controller { get; private set; }

		public string DisplayName { get; private set;}

		private Book _lastBook;

		protected virtual IBibleRenderer Renderer
		{
			get { return ParentController.StandardRenderer; }
		}

		public ChapterDisplay(MainController mainController, IDisplayView parentPage)
		{
			ParentPage = parentPage;
			ParentController = mainController;
			Controller = new ChapterController(this);

			InitializeComponent();
			InitializeComboBoxes();
			InitializeWebDisplay();
		}

		public Version CurrentVersion { get { return Controller.CurrentVersion; } }
		public Book CurrentBook { get { return Controller.CurrentBook; } }
		public Chapter CurrentChapter { get { return Controller.CurrentChapter; } }

		private void InitializeWebDisplay()
		{
			webDisplay.ObjectForScripting = ParentController.ObjectForScripting;
#if DEBUG
			webDisplay.ScriptErrorsSuppressed = false;
#endif
		}

		private void InitializeComboBoxes()
		{
			cboVersions.ComboBox.DisplayMember = "VersionName";
			cboVersions.ComboBox.ValueMember = "VersionId";

			cboBooks.ComboBox.DisplayMember = "BookName";
			cboBooks.ComboBox.ValueMember = "BookId";

			cboChapters.ComboBox.DisplayMember = "ChapterNumber";
			cboChapters.ComboBox.FormatString = "Chapter {0}";
			cboChapters.ComboBox.ValueMember = "ChapterId";

			cboVersions.ComboBox.DataSource = BibleStore.Versions;
			cboBooks.ComboBox.DataSource = BibleStore.Books;

			cboVersions.ComboBox.SelectedValue = UserPreferences.I.PreferredVersion;
		}

		private bool _updating;
		public void UpdateDisplay()
		{
			if ((Controller.CurrentBook != null) && ((_lastBook == null) || (_lastBook != Controller.CurrentBook)))
			{
				cboChapters.ComboBox.DataSource = Controller.CurrentBook.Chapters;
				_lastBook = Controller.CurrentBook;
			}

			if (Controller.IsValid)
			{
				webDisplay.DocumentText = Controller.RenderChapter(Renderer);
				DisplayName = Controller.DisplayName;
				OnPropertyChanged("DisplayName");

				btnPreviousBook.Enabled = (Controller.PrevBook != null);
				btnPreviousBook.ToolTipText = Controller.PrevBookToolTip;

				btnNextBook.Enabled = (Controller.NextBook != null);
				btnNextBook.ToolTipText = Controller.NextBookToolTip;

				btnPrevChapter.Enabled = (Controller.PrevChapter != null);
				btnPrevChapter.ToolTipText = Controller.PrevChapterToolTip;

				btnNextChapter.Enabled = (Controller.NextChapter != null);
				btnNextChapter.ToolTipText = Controller.NextChapterToolTip;

				_updating = true;
				cboChapters.ComboBox.Text = Controller.CurrentChapter.ChapterNumber.ToString(CultureInfo.CurrentCulture);
				cboBooks.ComboBox.Text = Controller.CurrentBook.BookName;
				_updating = false;

				if (ParentPage != null) ParentPage.DisplayNotesForChapter(Controller.CurrentBook, Controller.CurrentChapter);
			}
		}

		private void cboVersions_SelectedIndexChanged(object sender, EventArgs e)
		{
			Controller.ChangeVersion((int)cboVersions.ComboBox.SelectedValue);
		}

		private void cboBooks_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!_updating) Controller.MoveToBook((int)cboBooks.ComboBox.SelectedValue);
		}

		private void cboChapters_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!_updating) Controller.MoveToChapter((int)cboChapters.ComboBox.SelectedValue);
		}

		private void btnPreviousBook_Click(object sender, EventArgs e)
		{
			Controller.MoveToPrevBook();
		}

		private void btnPrevChapter_Click(object sender, EventArgs e)
		{
			Controller.MoveToPrevChapter();
		}

		private void btnNextChapter_Click(object sender, EventArgs e)
		{
			Controller.MoveToNextChapter();
		}

		private void btnNextBook_Click(object sender, EventArgs e)
		{
			Controller.MoveToNextBook();
		}

		public void DisplayChapter(Version version, Book book, Chapter chapter, Verse verse)
		{
			Controller.MoveTo(version, book, chapter);

			if (verse != null)
			{
				Application.DoEvents();
				HtmlElement element = webDisplay.Document.GetElementById(verse.VerseNumber.ToString(CultureInfo.InvariantCulture));
				if (element != null) element.ScrollIntoView(true);
			}
		}

		private void closeTabToolStripButton_Click(object sender, EventArgs e)
		{
			if (ParentPage != null) ParentPage.CloseTab(this);
		}

		public void Close()
		{
			ParentPage = null;
		}

		public void PreviewChapter()
		{
			webDisplay.ShowPrintPreviewDialog();
		}

		public void PrintChapter()
		{
			webDisplay.ShowPrintDialog();
		}
	}
}
