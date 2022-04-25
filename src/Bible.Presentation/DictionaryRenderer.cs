using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Globalization;

namespace Bible.Presentation
{
	public class DictionaryRenderer : HtmlRendererBase
	{
		public static string RenderDefinition(WordDictionaryEntry word)
		{
			return Render(writer => RenderDefinition(writer, word));
		}

		private static void RenderDefinition(XmlTextWriter writer, WordDictionaryEntry word)
		{
			writer.WriteStartElement("table");
			writer.WriteAttributeString("width", "100%");

			word.Definitions.ForEach(item => WriteDefinition(writer, item, word.Word.ToUpper(CultureInfo.CurrentCulture)));

			writer.WriteEndElement(); // table
		}

		private static void WriteDefinition(XmlTextWriter writer, WordDefinition item, string word)
		{
			writer.WriteStartElement("tr");
			writer.WriteStartElement("td");

			if (item.IsHeader)
			{
				writer.WriteAttributeString("class", "DictionaryText");

				string replacement = string.Format(CultureInfo.CurrentCulture, "<span class=\"DictionaryHeader\">{0}</span>", word);
				writer.WriteRaw(item.Text.Replace(word, replacement));

			}
			else if (item.IsExample)
			{
				writer.WriteAttributeString("class", "DictionaryExample");
				writer.WriteString(item.Text);
			}
			else
			{
				writer.WriteAttributeString("class", "DictionaryText");
				writer.WriteString(item.Text);
			}


			writer.WriteEndElement(); // td
			writer.WriteEndElement(); // tr
		}
	}
}
