using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Globalization;

namespace Bible.Presentation
{
	public class SearchResultsRenderer : HtmlRendererBase
	{
		public static string Render(SearchResultCollection searchResults)
		{
			return Render(writer => RenderResults(writer, searchResults));
		}

		private static void RenderResults(XmlTextWriter writer, SearchResultCollection searchResults)
		{
			searchResults.ForEach(item => RenderResult(writer, item));
		}

		private static void RenderResult(XmlTextWriter writer, SearchResult result)
		{
			Book book = BibleStore.Books.Find(item => item.BookId == result.BookId);
			if (book != null)
			{
				Chapter chapter = book.Chapters.Find(item => item.ChapterId == result.ChapterId);
				if (chapter != null)
				{
					Verse verse = chapter.Verses.Find(item => item.VerseId == result.VerseId);
					if (verse != null)
					{
						VerseVersion version = verse.Versions.Find(item => item.VersionId == result.VersionId);
						if (version != null)
						{
							RenderVerse(writer, book, chapter, verse, version);
						}
					}
				}
			}
		}

		private static Version ActualVersion;

		private static void RenderVerse(XmlTextWriter writer, Book book, Chapter chapter, Verse verse, VerseVersion version)
		{
			if ((ActualVersion == null) || (ActualVersion.VersionId != version.VersionId))
			{
				ActualVersion = BibleStore.Versions.Find(item => item.VersionId == version.VersionId);
			}

			string headerFormat = (version.VersionId == UserPreferences.I.PreferredVersion) ? "{0} {1}:{2}" : "{0} {1}:{2} ({3})";
			string clickScript = string.Format(CultureInfo.InvariantCulture, "window.external.ShowCrossRef({0}, {1}, {2}, {3});", version.VersionId, book.BookId, chapter.ChapterId, verse.VerseId);

			writer.WriteStartElement("div");

			writer.WriteStartElement("div");
			writer.WriteAttributeString("class", "ChapterHeader");
			writer.WriteAttributeString("style", "cursor: Hand;");
			writer.WriteAttributeString("onclick", clickScript);
			writer.WriteString(string.Format(CultureInfo.CurrentCulture, headerFormat, book.BookName, chapter.ChapterNumber, verse.VerseNumber, ActualVersion.VersionName));
			writer.WriteEndElement();

			writer.WriteStartElement("div");
			writer.WriteStartElement("span");
			writer.WriteAttributeString("class", "VerseNumber");
			writer.WriteRaw("&nbsp;&nbsp;");
			writer.WriteEndElement();
			writer.WriteString(" ");
			writer.WriteStartElement("span");
			writer.WriteAttributeString("class", "Normal");
			version.Words.ForEach(item => RenderWord(writer, item));
			writer.WriteRaw("<br />&nbsp;");
			writer.WriteEndElement();

		}

		private static void RenderWord(XmlTextWriter writer, Word item)
		{
			writer.WriteString(item.WordText);
			writer.WriteString(" ");
		}
	}
}
