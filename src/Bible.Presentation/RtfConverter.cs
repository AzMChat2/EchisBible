using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;

namespace Bible.Presentation
{
	internal abstract class RtfConverter
	{
		protected RichTextBox Document { get; private set; }
		public string RtfText { get { return Document.Rtf; } }

		protected RtfConverter()
		{
			Document = new RichTextBox();
		}

		protected void ChangeFont(TextStyle style)
		{
			ChangeFont(style.FontName, style.FontSize, style.Bold, style.Italic, style.Underline, style.TextColor.Value);
		}

		private void ChangeFont(string fontName, string sizeStr, bool bold, bool italics, bool underline, Color textColor)
		{
			float size = float.Parse(sizeStr, CultureInfo.InvariantCulture);
			FontStyle style = FontStyle.Regular;

			if (bold) style |= FontStyle.Bold;
			if (italics) style |= FontStyle.Italic;
			if (underline) style |= FontStyle.Underline;

			Document.SelectionFont = new Font(fontName, size, style);
			Document.SelectionColor = textColor;
		}

	}

	internal class NotesCollectionToRtfConverter : RtfConverter
	{
		public static string ConvertNotes(NotesCollection notes)
		{
			return new NotesCollectionToRtfConverter(notes).RtfText;
		}

		public static void ExportNotes(string fileName, NotesCollection notes)
		{
			NotesCollectionToRtfConverter converter = new NotesCollectionToRtfConverter(notes);
			converter.Document.SaveFile(fileName, RichTextBoxStreamType.RichText);
		}

		private NotesCollection _notes;

		private NotesCollectionToRtfConverter(NotesCollection notes)
		{
			_notes = notes;
			Convert();
		}

		private void Convert()
		{
			_notes.ForEach(Convert);
		}

		private void Convert(Notes note)
		{
			NoteToRtfConverter converter = new NoteToRtfConverter(note);
			Clipboard.SetText(converter.RtfText, TextDataFormat.Rtf);
			Document.Paste();
			Document.AppendText(Environment.NewLine);
		}
	}


	internal class NoteToRtfConverter : RtfConverter
	{
		private Notes _note;

		public NoteToRtfConverter(Notes note)
		{
			_note = note;
			Convert();
		}

		private void Convert()
		{
			Book book = BibleStore.Books.Find(item => item.BookId == _note.BookId);
			if (book != null)
			{
				Chapter chapter = book.Chapters.Find(item => item.ChapterId == _note.ChapterId);
				if (chapter != null)
				{
					ChapterHeaderToRtfConverter header = new ChapterHeaderToRtfConverter(book, chapter);

					Clipboard.SetText(header.RtfText, TextDataFormat.Rtf);
					Document.Paste();

					Clipboard.SetText(_note.NotesText, TextDataFormat.Rtf);
					Document.Paste();

					for (int idx = 0; idx < Document.TextLength; idx++)
					{
						Document.SelectionStart = idx;
						Document.SelectionLength = 1;
						if (Document.SelectedText == "[")
						{
							int start = idx;
							int end = 0;
							while (end == 0)
							{
								idx++;
								Document.SelectionStart = idx;
								Document.SelectionLength = 1;
								if (Document.SelectedText == "]")
								{
									end = idx + 1;
								}
							}

							Document.SelectionStart = start;
							Document.SelectionLength = end - start;
							string verseRef = Document.SelectedText;

							VerseReferenceToRtfConverter verseConverter = new VerseReferenceToRtfConverter(book, chapter, verseRef);
							Clipboard.SetText(verseConverter.RtfText, TextDataFormat.Rtf);
							Document.Paste();
						}
					}
				}
			}
		}
	}

	internal class ChapterHeaderToRtfConverter : RtfConverter
	{
		private Book _book;
		private Chapter _chapter;

		public ChapterHeaderToRtfConverter(Book book, Chapter chapter)
		{
			_book = book;
			_chapter = chapter;
			Convert();
		}

		private void Convert()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(_book.BookName);
			sb.Append(" ");
			sb.Append(_chapter.ChapterNumber.ToString(CultureInfo.CurrentCulture));
			sb.AppendLine();
			sb.AppendLine();

			ChangeFont(UserPreferences.I.ChapterHeader);
			Document.AppendText(sb.ToString());
		}
	}

	internal class VerseReferenceToRtfConverter : RtfConverter
	{
		private Book _book;
		private Chapter _chapter;
		private string _reference;
		private TextStyle _currentFont;
		private TextStyle _wocnitFont;


		public VerseReferenceToRtfConverter(Book book, Chapter chapter, string reference)
		{
			_wocnitFont = UserPreferences.I.NotInText.Clone();
			_wocnitFont.TextColor = UserPreferences.I.WordsOfChrist.TextColor.Clone();

			_book = book;
			_chapter = chapter;
			_reference = reference.Substring(1, reference.Length - 2); // get rid of the '[' at the start and ']' at the end.
			Convert();
		}

		private void Convert()
		{
			Version version = GetVersion();
			AddHeader(version);
			string[] parts = _reference.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
			foreach (string part in parts)
			{
				AddVerse(part, version.VersionId);
			}
		}

		private void AddHeader(Version version)
		{
			StringBuilder sb = new StringBuilder();

			sb.Append(_book.BookName);
			sb.Append(" ");
			sb.Append(_chapter.ChapterNumber);
			sb.Append(":");
			sb.Append(_reference);

			if (version.VersionId != UserPreferences.I.PreferredVersion)
			{
				if (!_chapter.LoadedVersions.Contains(version.VersionId))
				{
					BibleStore.BibleService.LoadChapter(_book, _chapter, version.VersionId);
				}

				sb.Append(" (");
				sb.Append(version.VersionName);
				sb.Append(")");
			}
			sb.AppendLine();

			ChangeFont(UserPreferences.I.ChapterHeader);
			Document.AppendText(sb.ToString());
		}

		private void AddVerse(string verseRef, int versionId)
		{
			if (verseRef.Contains('-'))
			{
				string[] parts = verseRef.Split('-');
				try
				{
					int begin = int.Parse(parts[0], CultureInfo.CurrentCulture);
					int end = int.Parse(parts[1], CultureInfo.CurrentCulture);
					for (int idx = begin; idx <= end; idx++)
					{
						AddVerse(idx, versionId);
					}
				}
				catch (ArgumentNullException) { }
				catch (FormatException) { }
				catch (OverflowException) { }
			}
			else
			{
				try
				{
					int verseNum = int.Parse(verseRef, CultureInfo.CurrentCulture);
					AddVerse(verseNum, versionId);
				}
				catch (ArgumentNullException) { }
				catch (FormatException) { }
				catch (OverflowException) { }
			}
		}

		private void AddVerse(int verseNumber, int versionId)
		{
			ChangeFont(UserPreferences.I.VerseNumber);
			Document.AppendText(verseNumber.ToString(CultureInfo.CurrentCulture));
			Document.AppendText("\t");

			Verse verse = _chapter.Verses.Find(item => item.VerseNumber == verseNumber);
			if (verse != null)
			{
				VerseVersion version = verse.Versions.Find(item => item.VersionId == versionId);
				if (version != null)
				{
					_currentFont = null;
					version.Words.ForEach(AddWord);
				}
			}

			Document.AppendText(Environment.NewLine);
		}

		private void AddWord(Word item)
		{
			TextStyle style = _currentFont;

			if (item.IsNIT)
			{
				if (item.IsWOC)
				{
					style = _wocnitFont;
				}
				else
				{
					style = UserPreferences.I.NotInText;
				}
			}
			else if (item.IsWOC)
			{
				style = UserPreferences.I.WordsOfChrist;
			}
			else
			{
				style = UserPreferences.I.NormalText;
			}

			if (style != _currentFont)
			{
				_currentFont = style;
				ChangeFont(style);
			}

			Document.AppendText(item.WordText);
			Document.AppendText(" ");
		}

		private Version GetVersion()
		{
			Version retVal = null;
			string[] parts = _reference.Split(':');

			if (parts.Length > 1)
			{
				_reference = parts[1];
				retVal = BibleStore.Versions.Find(item => item.VersionName.Equals(parts[0], StringComparison.OrdinalIgnoreCase));
			}
			else
			{
				retVal = BibleStore.Versions.Find(item => item.VersionId == UserPreferences.I.PreferredVersion);
			}

			return retVal;
		}
	}
}