using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;

namespace Bible.Presentation
{
	public class StrongsRenderer : HtmlRendererBase
	{
		public static string RenderDefinition(Strongs strongs, int versionId)
		{
			return Render(writer => RenderDefinition(writer, strongs, versionId));
		}

		public static string RenderPopup(Strongs strongs)
		{
			using (MemoryStream stream = new MemoryStream())
			{
				using (XmlTextWriter writer = new XmlTextWriter(stream, UnicodeEncoding.UTF8))
				{
					string className = strongs.IsGreek ? "Greek" : "Hebrew";

					writer.WriteStartElement("span");
					writer.WriteStartElement("span");
					writer.WriteAttributeString("class", "DictionaryHeader");
					writer.WriteString(strongs.DisplayNumber);
					writer.WriteEndElement();
					writer.WriteRaw("&nbsp;&nbsp");
					writer.WriteStartElement("span");
					writer.WriteAttributeString("class", className);
					writer.WriteString(string.Format(CultureInfo.CurrentCulture, "({0})", strongs.OriginalWord));
					writer.WriteEndElement();

					writer.WriteStartElement("br");
					writer.WriteEndElement();

					writer.WriteStartElement("span");
					writer.WriteAttributeString("class", "DictionaryText");
					writer.WriteString(strongs.Transliteration);
					writer.WriteEndElement();
					writer.WriteRaw("&nbsp;&nbsp");
					writer.WriteStartElement("span");
					writer.WriteAttributeString("class", "DictionaryText");
					writer.WriteString(string.Format(CultureInfo.CurrentCulture, "({0})", strongs.Phonetic));
					writer.WriteEndElement();

					writer.WriteStartElement("hr");

					strongs.Definitions.ForEach(item => WriteDefinition(writer, item));
					writer.WriteEndElement();

					writer.WriteEndElement(); // span

					writer.Flush();
					writer.Close();
				}
				return UnicodeEncoding.UTF8.GetString(stream.ToArray());
			}
		}

		private static void WriteDefinition(XmlTextWriter writer, string definition)
		{
			writer.WriteStartElement("span");
			writer.WriteAttributeString("class", "DictionaryText");
			writer.WriteString(definition);
			writer.WriteEndElement();

			writer.WriteStartElement("br");
			writer.WriteEndElement();
		}

		private static void RenderDefinition(XmlTextWriter writer, Strongs strongs, int versionId)
		{
			writer.WriteStartElement("table");
			writer.WriteAttributeString("width", "100%");

			writer.WriteStartElement("tr");
			writer.WriteStartElement("td");
			WriteDefinitionsTable(writer, strongs);
			writer.WriteEndElement();
			writer.WriteEndElement(); // tr

			writer.WriteStartElement("tr");
			writer.WriteStartElement("td");
			writer.WriteAttributeString("class", "DictionaryHeader");
			writer.WriteString("Word Usages");
			writer.WriteEndElement();
			writer.WriteEndElement(); // tr

			writer.WriteStartElement("tr");
			writer.WriteStartElement("td");
			writer.WriteAttributeString("class", "DictionaryText");
			writer.WriteString(strongs.WordUsage);
			writer.WriteEndElement();
			writer.WriteEndElement(); // tr

			writer.WriteStartElement("tr");
			writer.WriteStartElement("td");
			writer.WriteAttributeString("class", "DictionaryHeader");
			writer.WriteString("Cross References");
			writer.WriteEndElement();
			writer.WriteEndElement(); // tr

			strongs.CrossRefs.ForEach(item => WriteCrossRefs(writer, item, versionId));

			writer.WriteEndElement(); // table
		}

		private static void WriteDefinitionsTable(XmlTextWriter writer, Strongs strongs)
		{
			writer.WriteStartElement("table");

			#region Strongs Number
			writer.WriteStartElement("tr");
			writer.WriteStartElement("td");
			writer.WriteAttributeString("class", "DictionaryHeader");
			writer.WriteString("Strongs #");
			writer.WriteEndElement();

			writer.WriteStartElement("td");
			writer.WriteAttributeString("class", "DictionaryText");
			writer.WriteString(strongs.DisplayNumber);
			writer.WriteEndElement();
			writer.WriteEndElement(); // tr
			#endregion

			#region Original Word
			writer.WriteStartElement("tr");
			writer.WriteStartElement("td");
			writer.WriteAttributeString("class", "DictionaryHeader");
			writer.WriteString("Original Word");
			writer.WriteEndElement();

			writer.WriteStartElement("td");
			writer.WriteAttributeString("class", "Hebrew");
			writer.WriteString(strongs.OriginalWord);
			writer.WriteEndElement();
			writer.WriteEndElement(); //tr
			#endregion

			#region Transliteration
			writer.WriteStartElement("tr");
			writer.WriteStartElement("td");
			writer.WriteAttributeString("class", "DictionaryHeader");
			writer.WriteString("Transliteration");
			writer.WriteEndElement();

			writer.WriteStartElement("td");
			writer.WriteAttributeString("class", "DictionaryText");
			writer.WriteString(strongs.Transliteration);
			writer.WriteEndElement();
			writer.WriteEndElement(); //tr
			#endregion

			#region Phonetic
			writer.WriteStartElement("tr");
			writer.WriteStartElement("td");
			writer.WriteAttributeString("class", "DictionaryHeader");
			writer.WriteString("Phonetic");
			writer.WriteEndElement();

			writer.WriteStartElement("td");
			writer.WriteAttributeString("class", "DictionaryText");
			writer.WriteString(strongs.Phonetic);
			writer.WriteEndElement();
			writer.WriteEndElement(); // tr
			#endregion

			#region Speech Part
			writer.WriteStartElement("tr");
			writer.WriteStartElement("td");
			writer.WriteAttributeString("class", "DictionaryHeader");
			writer.WriteString("Part of Speech");
			writer.WriteEndElement();

			writer.WriteStartElement("td");
			writer.WriteAttributeString("class", "DictionaryText");
			writer.WriteString(strongs.SpeachPart);
			writer.WriteEndElement();
			writer.WriteEndElement(); //tr
			#endregion

			#region Word Origin
			writer.WriteStartElement("tr");
			writer.WriteStartElement("td");
			writer.WriteAttributeString("class", "DictionaryHeader");
			writer.WriteString("Word Origin");
			writer.WriteEndElement();

			writer.WriteStartElement("td");
			writer.WriteAttributeString("class", "DictionaryText");
			writer.WriteString(strongs.WordOrigin);
			writer.WriteEndElement();
			writer.WriteEndElement(); //tr
			#endregion

			#region Definitions
			writer.WriteStartElement("tr");
			writer.WriteStartElement("td");
			writer.WriteAttributeString("colspan", "2");
			writer.WriteAttributeString("class", "DictionaryHeader");
			writer.WriteString("Definitions");
			writer.WriteEndElement();
			writer.WriteEndElement(); // tr

			strongs.Definitions.ForEach(item => WriteDefinitions(writer, item));
			#endregion

			writer.WriteEndElement(); // table
		}

		private static void WriteDefinitions(XmlTextWriter writer, string definition)
		{
			writer.WriteStartElement("tr");
			writer.WriteStartElement("td");
			writer.WriteAttributeString("class", "DictionaryText");
			writer.WriteAttributeString("colspan", "2");
			writer.WriteString(definition);
			writer.WriteEndElement();
			writer.WriteEndElement(); // tr
		}

		private static void WriteCrossRefs(XmlTextWriter writer, CrossRef crossRef, int versionId)
		{
			Chapter chapter = BibleStore.FindChapter(crossRef.VerseId);
			Book book = BibleStore.FindBook(chapter.ChapterId);
			Verse verse = chapter.Verses.Find(item => item.VerseId == crossRef.VerseId);

			string showCrossRefScript = string.Format(CultureInfo.InvariantCulture, "window.external.ShowCrossRef({0}, {1}, {2}, {3});", versionId, book.BookId, chapter.ChapterId, verse.VerseId);

			writer.WriteStartElement("tr");
			writer.WriteStartElement("td");
			writer.WriteAttributeString("onclick", showCrossRefScript);
			writer.WriteAttributeString("class", "DictionaryText");
			writer.WriteAttributeString("style", "cursor: hand;");

			string crossRefText = string.Format(CultureInfo.CurrentCulture, "{0} {1}:{2}", book.BookName, chapter.ChapterNumber, verse.VerseNumber);
			writer.WriteString(crossRefText);

			writer.WriteEndElement();
			writer.WriteEndElement(); // tr
		}
	}
}
