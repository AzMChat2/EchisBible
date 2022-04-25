using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Bible.Presentation
{
	public class StandardBibleRenderer : HtmlRendererBase, IBibleRenderer
	{
		public string RenderChapter(Version version, Book book, Chapter chapter)
		{
			return Render(writer => Render(writer, version, book, chapter));
		}

		protected static void Render(XmlWriter writer, Version version, Book book, Chapter chapter)
		{
			WriteScripts(writer);
			WriteVersion(version, writer);
			WriteBook(book, writer);
			WriteChapter(chapter, writer, version.VersionId);
		}

		protected static void WriteScripts(XmlWriter writer)
		{
			writer.WriteStartElement("script");
			writer.WriteAttributeString("language", "javascript");
			writer.WriteAttributeString("type", "text/javascript");
			writer.WriteString(Environment.NewLine);
			writer.WriteRaw(string.Format(CultureInfo.InvariantCulture, "var imagePath = '{0}Images';", AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "\\\\")));
			writer.WriteString(Environment.NewLine);
			writer.WriteRaw(ScriptResources.BalloonScript);
			writer.WriteString(Environment.NewLine);
			writer.WriteRaw(ScriptResources.ToolTipScript);
			writer.WriteString(Environment.NewLine);
			writer.WriteEndElement();

		}

		protected static void WriteVersion(Version version, XmlWriter writer)
		{
			writer.WriteStartElement("p");

			writer.WriteStartElement("span");
			writer.WriteAttributeString("class", "VersionHeader");
			writer.WriteValue(string.Format(CultureInfo.CurrentCulture, "{0} ({1})", version.LongName, version.VersionName));
			writer.WriteEndElement();

			writer.WriteEndElement();
		}

		protected static void WriteBook(Book book, XmlWriter writer)
		{
			writer.WriteStartElement("p");

			writer.WriteStartElement("span");
			writer.WriteAttributeString("class", "BookHeader");
			writer.WriteValue(book.BookName);
			writer.WriteEndElement();

			writer.WriteEndElement();
		}

		protected static void WriteChapter(Chapter chapter, XmlWriter writer, int versionId)
		{
			writer.WriteStartElement("p");

			writer.WriteStartElement("span");
			writer.WriteAttributeString("class", "ChapterHeader");
			writer.WriteValue(string.Format(CultureInfo.CurrentCulture, "Chapter {0}", chapter.ChapterNumber));
			writer.WriteEndElement();

			writer.WriteEndElement();

			writer.WriteStartElement("div");
			writer.WriteAttributeString("class", "normal");

			writer.WriteStartElement("p");
			chapter.Verses.ForEach(item => WriteVerse(item, writer, versionId));
			writer.WriteEndElement();

			writer.WriteEndElement();

		}

		protected static void WriteVerse(Verse verse, XmlWriter writer, int versionId)
		{
			VerseVersion version = verse.Versions.Find(item => item.VersionId == versionId);

			if (version == null)
			{
				writer.WriteStartElement("p");
				writer.WriteValue("Verse not loaded");
				writer.WriteEndElement();
			}
			else
			{
				if (version.Header != null) WriteVerseHeader(version.Header, writer);

				writer.WriteStartElement("p");
				writer.WriteAttributeString("name", verse.VerseNumber.ToString(CultureInfo.InvariantCulture));
				writer.WriteAttributeString("id", verse.VerseNumber.ToString(CultureInfo.InvariantCulture));
				WriteVerseNumber(verse.VerseNumber, writer);
				version.Words.ForEach(item => WriteWord(item, writer, versionId));
				writer.WriteEndElement();
			}
		}

		protected static void WriteVerseNumber(int verseNumber, XmlWriter writer)
		{
			string verseRefScript = string.Format(CultureInfo.InvariantCulture, "window.external.AddVerseRefToNotes({0});", verseNumber);

			writer.WriteStartElement("span");
			writer.WriteAttributeString("class", "VerseNumber");
			writer.WriteAttributeString("onclick", verseRefScript);
			writer.WriteValue(verseNumber);
			writer.WriteEndElement();
		}

		private static Regex scrubber = new Regex("[^A-Za-z]", RegexOptions.Compiled);
		protected static void WriteWord(Word word, XmlWriter writer, int versionId)
		{
			writer.WriteValue(" ");
			writer.WriteStartElement("span");

			string scrubbedWord = scrubber.Replace(word.WordText, string.Empty).ToLower(CultureInfo.CurrentCulture);
			int strongsId = (word.Strongs.Count == 0) ? 0 : word.Strongs[0];

			string clickScript = string.Format(CultureInfo.InvariantCulture, "window.external.ShowDefinitions({0}, {1}, '{2}');", versionId, strongsId, scrubbedWord);
			string hoverScript = string.Format(CultureInfo.InvariantCulture, "popupStrongs(event, {0});", strongsId);

			writer.WriteAttributeString("class", GetWordClass(word));
			writer.WriteAttributeString("onmouseover", hoverScript);
			writer.WriteAttributeString("onclick", clickScript);

			writer.WriteValue(word.WordText);
			writer.WriteEndElement();

			if (word.Strongs.Count > 1)
			{
				writer.WriteValue(" ");
				for (int idx = 1; idx < word.Strongs.Count; idx++)
				{
					writer.WriteStartElement("span");
					writer.WriteAttributeString("class", "Concordance");

					clickScript = string.Format(CultureInfo.InvariantCulture, "window.external.ShowDefinitions({0}, {1}, '{2}');", versionId, word.Strongs[idx], string.Empty);
					hoverScript = string.Format(CultureInfo.InvariantCulture, "popupStrongs(event, {0});", word.Strongs[idx]);

					writer.WriteAttributeString("onmouseover", hoverScript);
					writer.WriteAttributeString("onclick", clickScript);

					writer.WriteValue("*");
					writer.WriteEndElement();
				}
			}
		}

		protected static string GetWordClass(Word word)
		{
			string retVal = word.IsWOC ? "WordsOfChrist" : string.Empty;

			if (word.Strongs.Count == 0)
			{
				if (word.IsNIT) retVal += "NotInText";
			}
			else
			{
				retVal += "Concordance";
			}

			return string.IsNullOrEmpty(retVal) ? "Normal" : retVal;
		}

		protected static void WriteVerseHeader(VerseHeader verseHeader, XmlWriter writer)
		{
			writer.WriteStartElement("p");

			writer.WriteStartElement("span");
			writer.WriteAttributeString("class", "VerseHeader");
			writer.WriteValue(verseHeader.HeaderText);
			writer.WriteEndElement();

			writer.WriteEndElement();
		}
	}
}
