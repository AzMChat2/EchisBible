using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using Bible.Presentation;

namespace Bible
{
	public partial class DisplayForm : Form, IDisplayView
	{
		private const string SearchTabFormat = "Search Results {0}";

		private TabPage _newPage;
		private MainController _mainController;
		private Dictionary<TabPage, ChapterDisplay> _chapterDisplays = new Dictionary<TabPage, ChapterDisplay>();
		private Dictionary<TabPage, SearchDisplay> _searchDisplays = new Dictionary<TabPage, SearchDisplay>();

		private int _searchTabCount;

		public DisplayForm(Form mdiParent, MainController mainController) : this(mdiParent, mainController, null) { }
		public DisplayForm(Form mdiParent, MainController mainController, SearchResultCollection searchResults)
		{
			MdiParent = mdiParent;
			_mainController = mainController;

			InitializeComponent();

			if (searchResults == null)
			{
				InitializeTab(tabPage1);
			}
			else
			{
				InitializeTab(tabPage1, searchResults);
			}

			_newPage = tabPage2;
		}

		public string[] GetSearchDisplayNames()
		{
			List<string> displays = new List<string>();
			_searchDisplays.Keys.ForEach(item => displays.Add(item.Text));
			return displays.ToArray();
		}

		public void DisplaySearchResults(string tabText, SearchResultCollection searchResults)
		{
			TabPage page = GetTabPage(tabText);

			if ((page == null) || (!_searchDisplays.ContainsKey(page)))
			{
				OpenNewTab(searchResults);
			}
			else
			{
				_searchDisplays[page].UpdateDisplay(searchResults);
				tabControl.SelectedTab = page;
			}
		}

		private TabPage GetTabPage(string tabText)
		{
			TabPage retVal = null;

			foreach (TabPage page in tabControl.TabPages)
			{
				if (page.Text == tabText)
				{
					retVal = page;
					break;
				}
			}

			return retVal;
		}

		private void OpenNewTab(SearchResultCollection searchResults)
		{
			TabPage page = _newPage;
			InitializeTab(page, searchResults);
			_newPage = new TabPage();
			_newPage.Text = "New";
			tabControl.TabPages.Add(_newPage);
			tabControl.SelectedTab = page;
		}

		private void InitializeTab(TabPage page, SearchResultCollection searchResults)
		{
			SearchDisplay display = new SearchDisplay(_mainController, this);

			display.Dock = DockStyle.Fill;
			page.Controls.Add(display);
			page.Text = string.Format(CultureInfo.CurrentCulture, SearchTabFormat, ++_searchTabCount);

			_searchDisplays.Add(page, display);
			UpdateTitleBar();
			
			Application.DoEvents();
			display.UpdateDisplay(searchResults);
		}

		private void OpenNewTab()
		{
			InitializeTab(_newPage);
			_newPage = new TabPage();
			_newPage.Text = "New";
			tabControl.TabPages.Add(_newPage);
		}

		private void InitializeTab(TabPage page)
		{
			ChapterDisplay display = new ChapterDisplay(_mainController, this);

			display.Dock = DockStyle.Fill;
			display.PropertyChanged += new PropertyChangedEventHandler(display_PropertyChanged);
			page.Controls.Add(display);
			page.DataBindings.Add("Text", display, "DisplayName");

			_chapterDisplays.Add(page, display);
			UpdateTitleBar();

			Application.DoEvents();
			display.UpdateDisplay();
		}

		private void UpdateTitleBar()
		{
			if (_chapterDisplays.ContainsKey(tabControl.SelectedTab))
			{
				ChapterDisplay display = _chapterDisplays[tabControl.SelectedTab];

				if ((display.CurrentBook != null) && (display.CurrentChapter != null) && (display.CurrentVersion != null))
				{
					Text = string.Format(CultureInfo.CurrentCulture, "{0} Chapter {1} - {2}", display.CurrentBook.BookName, display.CurrentChapter.ChapterNumber, display.CurrentVersion.LongName);
				}
			}
			else
			{
				Text = tabControl.SelectedTab.Text;
			}
		}

		private void display_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			UpdateTitleBar();
		}

		private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (tabControl.SelectedTab == _newPage)
			{
				OpenNewTab();
			}

			UpdateTitleBar();

			if (_chapterDisplays.ContainsKey(tabControl.SelectedTab))
			{
				ChapterDisplay chapterDisplay = _chapterDisplays[tabControl.SelectedTab];
				DisplayNotesForChapter(chapterDisplay.CurrentBook, chapterDisplay.CurrentChapter);
			}
		}

		public void ShowChapterInNewTab(Version version, Book book, Chapter chapter, Verse verse)
		{
			TabPage tab = _newPage;
			OpenNewTab();
			_chapterDisplays[tab].DisplayChapter(version, book, chapter, verse);
			tabControl.SelectedTab = tab;
		}

		public void CloseTab(IChapterView chapterDisplay)
		{
			TabPage page = null;

			foreach (KeyValuePair<TabPage, ChapterDisplay> item in _chapterDisplays)
			{
				if (item.Value == chapterDisplay)
				{
					page = item.Key;
					break;
				}
			}

			if (page != null)
			{
				_chapterDisplays.Remove(page);
				tabControl.TabPages.Remove(page);
			}
		}

		public void CloseTab(ISearchResultsView searchDisplay)
		{
			TabPage page = null;

			foreach (KeyValuePair<TabPage, SearchDisplay> item in _searchDisplays)
			{
				if (item.Value == searchDisplay)
				{
					page = item.Key;
					break;
				}
			}

			if (page != null)
			{
				_searchDisplays.Remove(page);
				tabControl.TabPages.Remove(page);
			}
		}

		public void DisplayNotesForChapter(Book book, Chapter chapter)
		{
			_mainController.DisplayNotesForChapter(book.BookId, chapter.ChapterId);
		}

		private void DisplayForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			_chapterDisplays.ForEach(item => item.Value.Close());
		}

		private ChapterDisplay ActiveChapterDisplay
		{
			get
			{
				ChapterDisplay retVal = null;
				if (_chapterDisplays.ContainsKey(tabControl.SelectedTab))
				{
					retVal = _chapterDisplays[tabControl.SelectedTab];
				}
				return retVal;
			}
		}

		public void PreviewChapter()
		{
			ActiveChapterDisplay.PreviewChapter();
		}

		public void PrintChapter()
		{
			ActiveChapterDisplay.PrintChapter();
		}
	}
}
