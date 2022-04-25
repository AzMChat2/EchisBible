using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;

namespace Bible.Presentation
{
	[ComVisible(true)]
	[SuppressMessage("Microsoft.Interoperability", "CA1409")]
	public class ScriptingObject
	{
		private IMainView _mainView;

		public ScriptingObject(IMainView mainView)
		{
			_mainView = mainView;
		}

		public void ShowDefinitions(int versionId, int strongsId, string word)
		{
			_mainView.DisplayStrongsEntry(strongsId, versionId);
			_mainView.DisplayDefinition(word);
		}

		public void ShowCrossRef(int versionId, int bookId, int chapterId, int verseId)
		{
			_mainView.DisplayCrossRef(versionId, bookId, chapterId, verseId);
		}

		public string GetStrongsPopupText(int strongsId)
		{
			Strongs strongs = BibleStore.Strongs.Find(item => item.StrongsId == strongsId);
			return StrongsRenderer.RenderPopup(strongs);
		}

		public void AddVerseRefToNotes(int verseNumber)
		{
			_mainView.AddVerseRefToNotes(verseNumber);
		}
	}
}
