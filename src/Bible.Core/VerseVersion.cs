using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Bible
{
	[Serializable]
	public class VerseVersion
	{
		public VerseVersion()
		{
			Words = new WordCollection();
		}

		[XmlAttribute]
		public int VerseId { get; set; }
		[XmlAttribute]
		public int VersionId { get; set; }
		[XmlElement]
		public VerseHeader Header { get; set; }
		[XmlElement("Word")]
		public WordCollection Words { get; set; }

		private string _text;
		[XmlIgnore]
		public string Text
		{
			get
			{
				CheckText();
				return _text;
			}
		}
		internal void CheckText()
		{
			if (_text == null) _text = GetText(Words);
		}

		private static string GetText(WordCollection words)
		{
			StringBuilder sb = new StringBuilder();

			words.ForEach(item => AddWordToText(sb, item));

			return sb.ToString(0, sb.Length - 1);
		}

		private static Regex scrubber = new Regex("[^A-Za-z]", RegexOptions.Compiled);
		private static void AddWordToText(StringBuilder sb, Word item)
		{
			sb.Append(scrubber.Replace(item.WordText, string.Empty).ToLower(CultureInfo.CurrentCulture));
			sb.Append(" ");
		}


	}

	[Serializable]
	public class VerseVersionCollection : ListEx<VerseVersion>
	{
	}

}
