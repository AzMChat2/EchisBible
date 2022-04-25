using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics.CodeAnalysis;

namespace Bible.Presentation
{
	public class UserPreferences
	{
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "I stands for 'Instance' and is abreviated to keep consuming code cleaner.")]
		public static UserPreferences I { get; set; }

		#region Static Load and Save methods
		private static XmlSerializer _serializer = new XmlSerializer(typeof(UserPreferences));
		private static string _filename = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\EchisBible\UserPref.xml";

		public static void Load()
		{
			if (File.Exists(_filename))
			{
				using (Stream stream = File.Open(_filename, FileMode.Open, FileAccess.Read, FileShare.None))
				{
					I = (UserPreferences)_serializer.Deserialize(stream);
				}
			}
			else
			{
				I = CreateNewPreferences();
				Save();
			}
		}

		public static UserPreferences CreateNewPreferences()
		{
			UserPreferences retVal = new UserPreferences();

			retVal.PreferredVersion = 1;
			retVal.VersionHeader = new TextStyle() { Visible = false, Bold = true, FontSize = "14" };
			retVal.BookHeader = new TextStyle() { Bold = true, FontSize = "16" };
			retVal.ChapterHeader = new TextStyle() { Bold = true, FontSize = "14" };
			retVal.VerseHeader = new TextStyle() { Bold = true };
			retVal.VerseNumber = new TextStyle() { Cursor = "Hand", Bold = true, FontSize = "8", Superscript = true };
			retVal.NormalText = new TextStyle() { Cursor = "Hand" };
			retVal.ConcordanceText = new TextStyle() { Cursor = "Hand" };
			retVal.ConcordanceText.HighliteColor.Value = Color.FromArgb(255, 255, 210);
			retVal.NotInText = new TextStyle() { Italic = true };
			retVal.WordsOfChrist = new TextStyle();
			retVal.WordsOfChrist.TextColor.Value = Color.Red;
			retVal.DictionaryHeader = new TextStyle() { Bold = true };
			retVal.DictionaryExample = new TextStyle() { Italic = true };
			retVal.DictionaryText = new TextStyle();
			retVal.HebrewText = new TextStyle();
			retVal.GreekText = new TextStyle();

			retVal.NotesPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			retVal.AudioPath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);

			retVal.StatusStripVisible = true;
			retVal.ToolStripVisible = true;
			retVal.MainFormWindowState = FormWindowState.Maximized;

			return retVal;
		}

		public static void Save()
		{
			string path = Path.GetDirectoryName(_filename);
			if (!Directory.Exists(path)) Directory.CreateDirectory(path);

			using(Stream stream = File.Open(_filename, FileMode.Create, FileAccess.Write, FileShare.None))
			{
				_serializer.Serialize(stream, I);
			}
		}
		#endregion

		private UserPreferences()
		{
			Notes = new ListEx<string>();
		}

		#region Main Form Visual Settings
		[XmlAttribute]
		public FormWindowState MainFormWindowState { get; set; }

		[XmlAttribute]
		public int MainFormWidth { get; set; }

		[XmlAttribute]
		public int MainFormHeight { get; set; }

		[XmlAttribute]
		public int DefinitionsHeight { get; set; }

		[XmlAttribute]
		public int NotesWidth { get; set; }
	
		[XmlAttribute]
		public int DictionaryWidth { get; set; }

		[XmlAttribute]
		public bool ToolStripVisible { get; set; }

		[XmlAttribute]
		public bool StatusStripVisible { get; set; }

		[XmlAttribute]
		public int PreferredVersion { get; set; }
		#endregion

		#region Notes
		[XmlElement("Note")]
		[SuppressMessage("Microsoft.Usage", "CA2227", Justification = "Setter is used by XmlSerializer when deserializing.")]
		public ListEx<string> Notes { get; set; }

		[XmlAttribute]
		public string ActiveNotes { get; set; }

		[XmlAttribute]
		public string LastFont { get; set; }

		[XmlAttribute]
		public string LastFontSize { get; set; }
		#endregion

		#region Font Styles
		[XmlElement]
		public TextStyle VersionHeader { get; set; }

		[XmlElement]
		public TextStyle BookHeader { get; set; }

		[XmlElement]
		public TextStyle ChapterHeader { get; set; }

		[XmlElement]
		public TextStyle VerseHeader { get; set; }

		[XmlElement]
		public TextStyle VerseNumber { get; set; }

		[XmlElement]
		public TextStyle NormalText { get; set; }

		[XmlElement]
		public TextStyle ConcordanceText { get; set; }

		[XmlElement]
		public TextStyle NotInText { get; set; }

		[XmlElement]
		public TextStyle WordsOfChrist { get; set; }

		[XmlElement]
		public TextStyle DictionaryHeader { get; set; }

		[XmlElement]
		public TextStyle DictionaryExample { get; set; }

		[XmlElement]
		public TextStyle DictionaryText { get; set; }

		[XmlElement]
		public TextStyle HebrewText { get; set; }

		[XmlElement]
		public TextStyle GreekText { get; set; }
		#endregion

		#region Paths
		[XmlElement]
		public string NotesPath { get; set; }

		[XmlElement]
		public string AudioPath { get; set; }
		#endregion

		#region Methods
		public void WriteTextStyles(XmlWriter writer)
		{
			writer.WriteValue(VersionHeader.GetStyle("VersionHeader"));
			writer.WriteValue(BookHeader.GetStyle("BookHeader"));
			writer.WriteValue(ChapterHeader.GetStyle("ChapterHeader"));
			writer.WriteValue(VerseHeader.GetStyle("VerseHeader"));
			writer.WriteValue(VerseNumber.GetStyle("VerseNumber"));
			writer.WriteValue(NormalText.GetStyle("Normal"));
			writer.WriteValue(ConcordanceText.GetStyle("Concordance"));
			writer.WriteValue(NotInText.GetStyle("NotInText"));
			writer.WriteValue(WordsOfChrist.GetStyle("WordsOfChrist"));
			writer.WriteValue(DictionaryHeader.GetStyle("DictionaryHeader"));
			writer.WriteValue(DictionaryExample.GetStyle("DictionaryExample"));
			writer.WriteValue(DictionaryText.GetStyle("DictionaryText"));
			writer.WriteValue(HebrewText.GetStyle("Hebrew"));
			writer.WriteValue(GreekText.GetStyle("Greek"));
			writer.WriteValue(GetSpecialStyle(WordsOfChrist, ConcordanceText, "WordsOfChristConcordance"));
			writer.WriteValue(GetSpecialStyle(WordsOfChrist, NotInText, "WordsOfChristNotInText"));
		}

		private static string GetSpecialStyle(TextStyle primary, TextStyle secondary, string name)
		{
			StringBuilder sb = new StringBuilder();

			sb.Append(".");
			sb.AppendLine(name);
			sb.AppendLine("{");

			if (primary.Visible || secondary.Visible)
			{
				sb.Append("font-family: ");
				sb.Append(primary.FontName);
				sb.AppendLine(";");

				sb.Append("font-size: ");
				sb.Append(primary.FontSize);
				sb.AppendLine("pt;");

				sb.Append("text-align: ");
				sb.Append(primary.TextAlign);
				sb.AppendLine(";");

				if (primary.Italic || secondary.Italic) sb.AppendLine("font-style: italic;");

				if (primary.Bold || secondary.Bold) sb.AppendLine("font-weight: bold;");

				if (primary.Underline || secondary.Underline) sb.AppendLine("text-decoration: underline;");

				if (primary.Superscript || secondary.Superscript) sb.AppendLine("vertical-align: super;");

				if (!string.IsNullOrEmpty(primary.Cursor))
				{
					sb.Append("cursor: ");
					sb.Append(primary.Cursor);
					sb.AppendLine(";");
				}
				else if (!string.IsNullOrEmpty(secondary.Cursor))
				{
					sb.Append("cursor: ");
					sb.Append(secondary.Cursor);
					sb.AppendLine(";");
				}

				sb.Append("color: ");
				if (primary.TextColor.Value == Color.Black)
				{
					sb.Append(secondary.TextColor.StyleValue);
				}
				else
				{
					sb.Append(primary.TextColor.StyleValue);
				}
				sb.AppendLine(";");

				sb.Append("background-color: ");
				if (primary.HighliteColor.Value == Color.White)
				{
					sb.Append(secondary.HighliteColor.StyleValue);
				}
				else
				{
					sb.Append(primary.HighliteColor.StyleValue);
				}
				sb.AppendLine(";");
			}
			else
			{
				sb.AppendLine("display: none;");
				sb.AppendLine("visibility: hidden;");
			}

			sb.AppendLine("}");
			sb.AppendLine();

			return sb.ToString();
		}
		#endregion
	}
}
