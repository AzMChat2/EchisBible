using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Bible
{
	public enum BibleSearchTypes
	{
		MatchAny,
		MatchAll,
		MatchExact,
		MatchRegex
	}

	public class BibleSearcher
	{
		public BibleSearcher()
		{
			SearchBooks = new List<int>();
		}

		public string SearchText { get; set; }
		public List<int> SearchBooks { get; private set; }
		public int SearchVersion { get; set; }
		public BibleSearchTypes SearchType { get; set; }

		private delegate bool MatchHandler(Verse verse);

		public SearchResultCollection PerformSearch()
		{
			SearchResultCollection retVal = new SearchResultCollection();

			MatchHandler isMatch = GetMatchMethod();

			SearchResult[] results = (from book in BibleStore.Books
																from chapter in book.Chapters
																from verse in chapter.Verses
																where IsBookMatch(book, chapter) && isMatch(verse)
																select new SearchResult(book.BookId, chapter.ChapterId, verse.VerseId, SearchVersion)).ToArray();

			retVal.AddRange(results);

			return retVal;
		}

		private MatchHandler GetMatchMethod()
		{
			MatchHandler retVal;

			switch (SearchType)
			{
				case BibleSearchTypes.MatchExact:
					retVal = new MatchHandler(IsExactMatch);
					break;
				case BibleSearchTypes.MatchRegex:
					retVal = new MatchHandler(IsRegexMatch);
					break;
				default:
					retVal = new MatchHandler(IsAnyAllMatch);
					break;
			}

			return retVal;
		}

		private bool IsBookMatch(Book book, Chapter chapter)
		{
			bool retVal = false;

			if (SearchBooks.Contains(book.BookId))
			{
				retVal = true;
				// Book is in the search list, make sure the version being searched is loaded.
				if (!chapter.LoadedVersions.Contains(SearchVersion))
				{
					BibleStore.BibleService.LoadChapter(book, chapter, SearchVersion);
				}
			}

			return retVal;
		}

		private List<string> excludes;
		private List<string> includes;
		private List<string> required;

		private Regex phrase = new Regex("\"[^\"]*\"", RegexOptions.Compiled);
		private Regex scrubber = new Regex("[^A-Za-z ]", RegexOptions.Compiled);

		private void ParseSearchText()
		{
			excludes = new List<string>();
			includes = new List<string>();
			required = new List<string>();

			string searchText = SearchText.ToLower(CultureInfo.CurrentCulture);

			// Process Phrases...
			MatchCollection matches = phrase.Matches(SearchText);
			foreach (Match match in matches)
			{
				string matchText = scrubber.Replace(match.Value, string.Empty);
				if ((match.Index > 0) && (searchText[match.Index - 1] == '-'))
				{
					excludes.Add(matchText);
				}
				else if ((SearchType == BibleSearchTypes.MatchAll) ||
					((match.Index > 0) && (searchText[match.Index - 1] == '+')))
				{
					required.Add(matchText);
				}
				else
				{
					includes.Add(matchText);
				}
			}
			searchText = phrase.Replace(searchText, string.Empty);

			// Process Words
			string[] words = searchText.Split(' ');
			foreach (string word in words)
			{
				string wordText = scrubber.Replace(word, string.Empty);
				if (!string.IsNullOrEmpty(wordText))
				{
					if (word[0] == '-')
					{
						excludes.Add(wordText);
					}
					else if ((SearchType == BibleSearchTypes.MatchAll) || (word[0] == '+'))
					{
						required.Add(wordText);
					}
					else
					{
						includes.Add(wordText);
					}
				}
			}
		}

		private bool TextFoundInVerse(string includeText, string verseText)
		{
			return verseText.Contains(includeText);
		}

		private bool HasAnInclude(string verseText)
		{
			return (includes.Count == 0) || includes.Exists(item => TextFoundInVerse(item, verseText));
		}

		private bool HasAllRequired(string verseText)
		{
			return required.All(item => TextFoundInVerse(item, verseText));
		}

		private bool HasNoExcludes(string verseText)
		{
			return !excludes.Exists(item => TextFoundInVerse(item, verseText));
		}

		private bool IsAnyAllMatch(Verse verse)
		{
			VerseVersion version = verse.Versions.Find(item => item.VersionId == SearchVersion);
			if (includes == null) ParseSearchText();

			bool hasNoExcludes = HasNoExcludes(version.Text);
			bool hasAllRequired = HasAllRequired(version.Text);
			bool hasAnInclude = HasAnInclude(version.Text);

			return (hasNoExcludes && hasAllRequired && hasAnInclude);
		}

		private bool IsExactMatch(Verse verse)
		{
			VerseVersion version = verse.Versions.Find(item => item.VersionId == SearchVersion);
			string matchText = scrubber.Replace(SearchText, string.Empty).ToLower(CultureInfo.CurrentCulture);
			return TextFoundInVerse(matchText, version.Text);
		}

		private Regex searchRegex;
		private bool IsRegexMatch(Verse verse)
		{
			if (searchRegex == null) searchRegex = new Regex(SearchText, RegexOptions.Compiled);
			VerseVersion version = verse.Versions.Find(item => item.VersionId == SearchVersion);
			return searchRegex.IsMatch(version.Text);
		}
	}
}
