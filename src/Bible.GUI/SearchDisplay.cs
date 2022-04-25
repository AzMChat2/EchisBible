using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bible.Presentation;

namespace Bible
{
	public partial class SearchDisplay : UserControl, ISearchResultsView
	{
		protected IDisplayView ParentPage { get; private set; }
		protected MainController ParentController { get; private set; }

		public SearchDisplay(MainController mainController, IDisplayView parentPage)
		{
			ParentPage = parentPage;
			ParentController = mainController;

			InitializeComponent();
			InitializeWebDisplay();
		}

		private void InitializeWebDisplay()
		{
			webDisplay.ObjectForScripting = ParentController.ObjectForScripting;
#if DEBUG
			webDisplay.ScriptErrorsSuppressed = false;
#endif
		}
		
		private void closeTabButton_Click(object sender, EventArgs e)
		{
			if (ParentPage != null) ParentPage.CloseTab(this);
		}

		public void Close()
		{
			ParentPage = null;
		}

		public void UpdateDisplay(SearchResultCollection searchResults)
		{
			webDisplay.DocumentText = SearchResultsRenderer.Render(searchResults);
		}
	}
}
