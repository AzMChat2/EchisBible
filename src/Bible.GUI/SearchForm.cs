using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bible.Presentation;
using System.Globalization;
using System.Diagnostics;

namespace Bible
{
	public partial class SearchForm : Form
	{
		private static SearchForm _searchForm;
		public static void ShowSearchDialog(MainForm mainForm)
		{
			if (_searchForm == null) _searchForm = new SearchForm(mainForm);
			if (_searchForm.ShowDialog(mainForm) == DialogResult.OK)
			{
				// Perform Search...
				BibleSearcher searcher = new BibleSearcher();

				searcher.SearchText = _searchForm.txtSearch.Text;
				searcher.SearchType = _searchForm.GetSearchType();
				searcher.SearchVersion = (int)_searchForm.lstVersions.SelectedValue;
				_searchForm.AddSearchBooks(searcher.SearchBooks);

				SearchResultCollection searchResults = searcher.PerformSearch();

				DisplayForm displayForm = _searchForm.GetDisplayForm();
				if (displayForm == null)
				{
					displayForm = new DisplayForm(mainForm, mainForm.Controller, searchResults);
					displayForm.Show();
				}
				else
				{
					displayForm.DisplaySearchResults(_searchForm.cboTabs.Text, searchResults);
				}
			}
		}

		#region Book Categories
		private static readonly int[] _law = new int[] { 1, 2, 3, 4, 5 };
		private static readonly int[] _history = new int[] { 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };
		private static readonly int[] _wisdom = new int[] { 18, 19, 20, 21, 22 };
		private static readonly int[] _major = new int[] { 23, 24, 25, 26, 27 };
		private static readonly int[] _minor = new int[] { 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39 };

		private static readonly int[] _gospels = new int[] { 40, 41, 42, 43 };
		private static readonly int[] _acts = new int[] { 44 };
		private static readonly int[] _paul = new int[] { 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57 };
		private static readonly int[] _letters = new int[] { 58, 59, 60, 61, 62, 62, 64, 65 };
		private static readonly int[] _revelation = new int[] { 66 };
		#endregion

		private MainForm _mainForm;
		private Dictionary<string, DisplayForm> _mdiChildren;

		private SearchForm(MainForm mainForm)
		{
			_mainForm = mainForm;

			InitializeComponent();

			PopulateOldTestament();
			PopulateNewTestament();
			PopulateVersions();
		}

		private DisplayForm GetDisplayForm()
		{
			DisplayForm retVal = null;

			if (_mdiChildren.ContainsKey(cboWindows.Text))
			{
				retVal = _mdiChildren[cboWindows.Text];
			}

			return retVal;
		}

		private BibleSearchTypes GetSearchType()
		{
			BibleSearchTypes retVal = BibleSearchTypes.MatchAny;

			if (rbnMatchAll.Checked) retVal = BibleSearchTypes.MatchAll;
			else if (rbnMatchExact.Checked) retVal = BibleSearchTypes.MatchExact;
			else if (rbnMatchRegex.Checked) retVal = BibleSearchTypes.MatchRegex;

			return retVal;
		}

		private void AddSearchBooks(List<int> list)
		{
			AddSearchBooks(list, lstNewTestament.Nodes);
			AddSearchBooks(list, lstOldTestament.Nodes);
		}

		private void AddSearchBooks(List<int> list, TreeNodeCollection nodes)
		{
			foreach (TreeNode node in nodes)
			{
				if (node.Nodes.Count == 0)
				{
					if (node.Checked) list.Add((int)node.Tag);
				}
				else
				{
					AddSearchBooks(list, node.Nodes);
				}
			}
		}


		#region Populate Lists
		private void PopulateVersions()
		{
			lstVersions.DisplayMember = "VersionName";
			lstVersions.ValueMember = "VersionId";
			lstVersions.DataSource = BibleStore.Versions;
		}

		private void PopulateOldTestament()
		{
			TreeNode otNode = new TreeNode("Old Testament");
			otNode.Nodes.Add(GenerateLawNode());
			otNode.Nodes.Add(GenerateHistoryNode());
			otNode.Nodes.Add(GenerateWisdomNode());
			otNode.Nodes.Add(GenerateMajorNode());
			otNode.Nodes.Add(GenerateMinorNode());

			lstOldTestament.Nodes.Add(otNode);
			otNode.Checked = true;
		}

		private void PopulateNewTestament()
		{
			TreeNode ntNode = new TreeNode("New Testament");
			ntNode.Nodes.Add(GenerateGospelNode());
			ntNode.Nodes.Add(GenerateActsNode());
			ntNode.Nodes.Add(GeneratePaulNode());
			ntNode.Nodes.Add(GenerateLettersNode());
			ntNode.Nodes.Add(GenerateRevelationNode());

			lstNewTestament.Nodes.Add(ntNode);
			ntNode.Checked = true;
		}

		private TreeNode GenerateBookNode(int bookId)
		{
			Book book = BibleStore.Books.Find(item => item.BookId == bookId);
			TreeNode retVal = new TreeNode(book.BookName);
			retVal.Tag = bookId;
			return retVal;
		}

		private TreeNode GenerateLawNode()
		{
			TreeNode retVal = new TreeNode("Law");
			_law.ForEach(item => retVal.Nodes.Add(GenerateBookNode(item)));
			return retVal;
		}

		private TreeNode GenerateHistoryNode()
		{
			TreeNode retVal = new TreeNode("History");
			_history.ForEach(item => retVal.Nodes.Add(GenerateBookNode(item)));
			return retVal;
		}

		private TreeNode GenerateWisdomNode()
		{
			TreeNode retVal = new TreeNode("Poetry and Wisdom");
			_wisdom.ForEach(item => retVal.Nodes.Add(GenerateBookNode(item)));
			return retVal;
		}

		private TreeNode GenerateMajorNode()
		{
			TreeNode retVal = new TreeNode("Major Prophets");
			_major.ForEach(item => retVal.Nodes.Add(GenerateBookNode(item)));
			return retVal;
		}

		private TreeNode GenerateMinorNode()
		{
			TreeNode retVal = new TreeNode("Minor Prophets");
			_minor.ForEach(item => retVal.Nodes.Add(GenerateBookNode(item)));
			return retVal;
		}

		private TreeNode GenerateGospelNode()
		{
			TreeNode retVal = new TreeNode("Gospels");
			_gospels.ForEach(item => retVal.Nodes.Add(GenerateBookNode(item)));
			return retVal;
		}

		private TreeNode GenerateActsNode()
		{
			TreeNode retVal = new TreeNode("History");
			_acts.ForEach(item => retVal.Nodes.Add(GenerateBookNode(item)));
			return retVal;
		}

		private TreeNode GeneratePaulNode()
		{
			TreeNode retVal = new TreeNode("Epistles of Paul");
			_paul.ForEach(item => retVal.Nodes.Add(GenerateBookNode(item)));
			return retVal;
		}

		private TreeNode GenerateLettersNode()
		{
			TreeNode retVal = new TreeNode("General Epistles");
			_letters.ForEach(item => retVal.Nodes.Add(GenerateBookNode(item)));
			return retVal;
		}

		private TreeNode GenerateRevelationNode()
		{
			TreeNode retVal = new TreeNode("Prophecy");
			_revelation.ForEach(item => retVal.Nodes.Add(GenerateBookNode(item)));
			return retVal;
		}
		#endregion

		private void PopulateWindowsCombo()
		{
			string selectedItem = "New Bible Browser";

			cboWindows.Items.Clear();
			cboWindows.Items.Add(selectedItem);

			_mdiChildren = new Dictionary<string, DisplayForm>();

			int idx = 0;
			foreach (Form form in _mainForm.MdiChildren)
			{
				if (form is DisplayForm)
				{
					string windowTitle = string.Format(CultureInfo.CurrentCulture, "{0} ({1})", form.Text, ++idx);
					cboWindows.Items.Add(windowTitle);

					if (form == _mainForm.ActiveMdiChild) selectedItem = windowTitle;

					_mdiChildren.Add(windowTitle, (DisplayForm)form);
				}
			}

			cboWindows.Text = selectedItem;
		}

		private void PopulateTabsCombo()
		{
			string selectedItem = "New Search Results Tab";

			cboTabs.Items.Clear();
			cboTabs.Items.Add(selectedItem);

			if (_mdiChildren.ContainsKey(cboWindows.Text))
			{
				DisplayForm form = _mdiChildren[cboWindows.Text];
				string[] tabs = form.GetSearchDisplayNames();
				if (tabs.Length != 0)
				{
					foreach (string tab in tabs)
					{
						cboTabs.Items.Add(tab);
					}

					selectedItem = tabs[tabs.Length - 1];
				}
			}

			cboTabs.Text = selectedItem;
		}

		#region Event Handlers
		private void AfterNodeCheck(object sender, TreeViewEventArgs e)
		{
			if (e.Node.Nodes.Count != 0)
			{
				foreach (TreeNode node in e.Node.Nodes)
				{
					node.Checked = e.Node.Checked;
				}
			}
		}

		private void SearchForm_Load(object sender, EventArgs e)
		{
			lstOldTestament.Nodes[0].Expand();
			lstNewTestament.Nodes[0].Expand();

			PopulateWindowsCombo();
		}

		private void cboWindows_SelectedIndexChanged(object sender, EventArgs e)
		{
			PopulateTabsCombo();
		}
		#endregion
	}
}
