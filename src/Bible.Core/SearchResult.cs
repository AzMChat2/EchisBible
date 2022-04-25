using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bible
{
	public class SearchResult
	{
		public SearchResult(int bookId, int chapterId, int verseId, int versionId)
		{
			BookId = bookId;
			ChapterId = chapterId;
			VerseId = verseId;
			VersionId = versionId;
		}

		public int BookId { get; private set; }
		public int ChapterId { get; private set; }
		public int VerseId { get; private set; }
		public int VersionId { get; private set; }
	}

	public class SearchResultCollection : ListEx<SearchResult> { }
}
