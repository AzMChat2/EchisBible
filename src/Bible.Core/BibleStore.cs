using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Bible
{
	public static class BibleStore
	{
		public static VersionCollection Versions { get; private set; }
		public static BookCollection Books { get; private set; }
		public static StrongsCollection Strongs { get; private set; }
		public static WordDictionary Dictionary { get; private set; }
		public static IBibleService BibleService { get; private set; }

		public static void Initialize()
		{
			BibleService = IOC.Instance.GetObject<IBibleService>("BibleService");

			Versions = BibleService.GetAvailableVersions();
			Books = BibleService.GetBookCollection();
			Strongs = BibleService.GetStrongsCollection();
			Dictionary = BibleService.GetWordDictionary();
		}

		public static Chapter FindChapter(int verseId)
		{
			Chapter retVal = null;

			foreach (Book book in Books)
			{
				retVal = book.Chapters.Find(item => item.Verses.Exists(verse => verse.VerseId == verseId));
				if (retVal != null) break;
			}

			return retVal;
		}

		public static Book FindBook(int chapterId)
		{
			return Books.Find(item => item.Chapters.Exists(chapter => chapter.ChapterId == chapterId));
		}
	}
}
