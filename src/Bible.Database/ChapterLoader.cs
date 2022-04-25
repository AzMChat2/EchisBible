using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Echis.Data.Interfaces;
using System.Data;

namespace Bible.Database
{
	internal class ChapterLoader : IDataLoader
	{
		private Chapter Data;

		public ChapterLoader(Chapter data)
		{
			Data = data;
		}

		public void ReadData(IDataReader reader)
		{
			// Verses
			while (reader.Read())
			{
				Verse item = new Verse();

				item.VerseId = reader.GetInt("VerseId");
				item.VerseNumber = reader.GetInt("Verse");

				Data.Verses.Add(item);
			}

			if (reader.NextResult())
			{
				// Headers
				while (reader.Read())
				{
					int verseId = reader.GetInt("VerseId");
					Verse verse = Data.Verses.Find(item => item.VerseId == verseId);

					if (verse != null)
					{
						VerseVersion verseVersion = GetVerseVersion(verse, reader.GetInt("VersionId"));
						verseVersion.Header = new VerseHeader();

						verseVersion.Header.HeaderId = reader.GetInt("HeaderId");
						verseVersion.Header.HeaderText = reader.GetString("HeaderText");
					}
				}

				if (reader.NextResult())
				{
					// Words
					while (reader.Read())
					{
						int verseId = reader.GetInt("VerseId");
						Verse verse = Data.Verses.Find(item => item.VerseId == verseId);

						if (verse != null)
						{
							VerseVersion verseVersion = GetVerseVersion(verse, reader.GetInt("VersionId"));
							Word item = new Word();

							item.WordId = reader.GetInt("WordId");
							item.WordIndex = reader.GetInt("WordIndex");
							item.WordText = reader.GetString("Word");

							verseVersion.Words.Add(item);
						}
					}

					if (reader.NextResult())
					{
						// Strongs
						while (reader.Read())
						{
							int wordId = reader.GetInt("WordId");
							int strongsId = reader.GetInt("StrongsId");

							Word word = FindWord(wordId);

							if (word != null)
							{
								word.Strongs.Add(strongsId);
							}
						}
					}
				}
			}
		}

		private Word FindWord(int wordId)
		{
			foreach (Verse verse in Data.Verses)
			{
				foreach (VerseVersion version in verse.Versions)
				{
					Word word = version.Words.Find(item => item.WordId == wordId);
					if (word != null) return word;
				}
			}
			return null;
		}

		private static VerseVersion GetVerseVersion(Verse verse, int versionId)
		{
			VerseVersion retVal = verse.Versions.Find(item => item.VersionId == versionId);

			if (retVal == null)
			{
				retVal = new VerseVersion();

				retVal.VersionId = versionId;
				retVal.VerseId = verse.VerseId;
				
				verse.Versions.Add(retVal);
			}

			return retVal;
		}
	}
}
