using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.CodeAnalysis;

namespace Bible
{
	public interface IBibleService
	{
		[SuppressMessage("Microsoft.Design", "CA1024", Justification = "Factory method, a property is inappropriate")]
		StrongsCollection GetStrongsCollection();

		[SuppressMessage("Microsoft.Design", "CA1024", Justification = "Factory method, a property is inappropriate")]
		VersionCollection GetAvailableVersions();

		[SuppressMessage("Microsoft.Design", "CA1024", Justification = "Factory method, a property is inappropriate")]
		BookCollection GetBookCollection();

		[SuppressMessage("Microsoft.Design", "CA1024", Justification = "Factory method, a property is inappropriate")]
		WordDictionary GetWordDictionary();

		void LoadChapter(Book book, Chapter chapter, int versionId);

		void LoadCrossRefs(Strongs strongs);
	}
}
