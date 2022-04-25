using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bible.Presentation
{
	public interface IDisplayView
	{
		void ShowChapterInNewTab(Version version, Book book, Chapter chapter, Verse verse);
		void CloseTab(IChapterView chapterDisplay);
		void CloseTab(ISearchResultsView chapterDisplay);
		void DisplayNotesForChapter(Book book, Chapter chapter);
	}
}
