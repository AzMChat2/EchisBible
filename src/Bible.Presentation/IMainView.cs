using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bible.Presentation
{
	public interface IMainView
	{
		void DisplayStrongsEntry(int strongsId, int versionId);
		void DisplayDefinition(string word);
		void DisplayCrossRef(int versionId, int bookId, int chapterId, int verseId);
		void AddVerseRefToNotes(int verseNumber);
	}
}
