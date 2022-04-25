using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Bible.Presentation
{
	public class NotesController
	{
		private const string UntitledNote = "Untitled";

		private INotesView _view;

		private int _untitledNotes;

		public NotesController(INotesView view)
		{
			_view = view;
			LoadedNotes = new ListEx<NotesCollection>();
		}

		public int BookId { get; set; }
		public int ChapterId { get; set; }

		public ListEx<NotesCollection> LoadedNotes { get; private set; }
		public NotesCollection ActiveNotes {get; private set;}
		public Notes ActiveNote { get; private set; }

		private void SetActiveNote()
		{
			ActiveNote = ActiveNotes.Find(item => ((item.BookId == BookId) && (item.ChapterId == ChapterId)));

			if (ActiveNote == null)
			{
				ActiveNote = new Notes();
				ActiveNote.BookId = BookId;
				ActiveNote.ChapterId = ChapterId;
				ActiveNotes.Add(ActiveNote);
			}
		}

		public void LoadNotes()
		{
			LoadedNotes = new ListEx<NotesCollection>();

			UserPreferences.I.Notes.ForEach(item => LoadNote(item));
			ActiveNotes = LoadedNotes.Find(item => item.FileName == UserPreferences.I.ActiveNotes);

			if ((ActiveNotes == null) && (!UserPreferences.I.Notes.Contains(UserPreferences.I.ActiveNotes)))
			{
				// For some reason the active notes weren't in the list try loading them manually
				ActiveNotes = LoadNote(UserPreferences.I.ActiveNotes);
			}

			if (ActiveNotes == null)
			{
				if (LoadedNotes.Count == 0)
				{
					// No active notes were saved, or user deleted the file, create a new notes collection
					CreateNewNotes();
				}
				else
				{
					// No active note found, but notes were loaded, pick from the top of the list
					ActiveNotes = LoadedNotes[0];
				}
			}
		}

		public void CreateNewNotes()
		{
			ApplyNoteChanges();

			string fileName = string.Format(CultureInfo.InvariantCulture, "{0}\\{2}{1}.ebn", UserPreferences.I.NotesPath, ++_untitledNotes, UntitledNote);
			ActiveNotes = new NotesCollection();
			ActiveNotes.FileName = fileName;
			LoadedNotes.Add(ActiveNotes);
			_view.PopulateNotesCombo();

			DisplayNotesForChapter();
		}

		[SuppressMessage("Microsoft.Design", "CA1031")]
		private NotesCollection LoadNote(string filename)
		{
			NotesCollection retVal = null;

			if (!string.IsNullOrEmpty(filename))
			{
				try
				{
					retVal = NotesCollection.Load(filename);
					LoadedNotes.Add(retVal);
					ActiveNotes = retVal;
				}
				catch (Exception ex)
				{
					if (!Path.GetFileNameWithoutExtension(filename).StartsWith(UntitledNote, StringComparison.OrdinalIgnoreCase))
					{
						string msg = string.Format(CultureInfo.CurrentCulture, "Unable to load notes file '{0}'\r\n{1}", filename, ex.Message);
						_view.ShowMessage(msg, "Notes Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}

			return retVal;
		}

		private static Regex lineScrubber = new Regex("[\r\n]", RegexOptions.Compiled);
		private void ApplyNoteChanges()
		{
			if (ActiveNote != null)
			{
				if (_view.IsNotesEmpty)
				{
					if (!string.IsNullOrEmpty(ActiveNote.NotesText))
					{
						ActiveNote.NotesText = string.Empty;
						ActiveNote.Dirty = true;
					}
				}
				else
				{
					// Ignore carriage returns and line feeds... 
					string notesText = lineScrubber.Replace(ActiveNote.NotesText, string.Empty);
					string rtfText = lineScrubber.Replace(_view.RtfText, string.Empty);
					if (notesText != rtfText)
					{
						ActiveNote.NotesText = _view.RtfText;
						ActiveNote.Dirty = true;
					}
				}
			}
		}

		public void OpenNotes(string fileName)
		{
			if ((Path.GetFileNameWithoutExtension(ActiveNotes.FileName).StartsWith(UntitledNote, StringComparison.OrdinalIgnoreCase)) &&
				(ActiveNotes.Count == 0))
			{
				LoadedNotes.Remove(ActiveNotes);
				ActiveNotes = null;
			}

			ActiveNotes = LoadNote(fileName);

			if (ActiveNotes == null) CreateNewNotes();

			_view.PopulateNotesCombo();
			DisplayNotesForChapter();
		}

		public void DisplayNotesForChapter()
		{
			ApplyNoteChanges();
			SetActiveNote();
			_view.RtfText = ActiveNote.NotesText;
		}

		public void SaveNotes(bool promptForFileName)
		{
			ApplyNoteChanges();
			SaveNotes(promptForFileName, ActiveNotes);
		}
		public bool SaveNotes(bool promptForFileName, NotesCollection note)
		{
			bool retVal = false;

			promptForFileName |= note.DisplayName.StartsWith(UntitledNote, StringComparison.OrdinalIgnoreCase);

			if (!promptForFileName || _view.PromptForNotesFileName(note))
			{
				note.RemoveAll(item => string.IsNullOrEmpty(item.NotesText));
				note.Save();
				if (promptForFileName) _view.PopulateNotesCombo();
				retVal = true;
			}

			return retVal;
		}

		public bool OkToClose()
		{
			bool retVal = true;

			ApplyNoteChanges();

			if (LoadedNotes.Exists(item => item.Dirty))
			{
				foreach (NotesCollection note in LoadedNotes)
				{
					if ((note.Count != 0) && (note.Dirty))
					{
						if (!_view.PromptToSaveNote(note))
						{
							retVal = false;
							break;
						}
					}
				}
			}

			return retVal;
		}

		public void CloseNotes()
		{
			ApplyNoteChanges();

			if (!ActiveNotes.Dirty || _view.PromptToSaveNote(ActiveNotes))
			{
				LoadedNotes.Remove(ActiveNotes);
				if (LoadedNotes.Count == 0)
				{
					CreateNewNotes();
				}
				else
				{
					ActiveNotes = LoadedNotes[0];
					_view.PopulateNotesCombo();
					DisplayNotesForChapter();
				}
			}
		}

		public string ConvertNotes()
		{
			ApplyNoteChanges();
			return NotesCollectionToRtfConverter.ConvertNotes(ActiveNotes);
		}

		public void ChangeNotes(string selected)
		{
			ApplyNoteChanges();
			NotesCollection activeNotes = LoadedNotes.Find(item => item.FileName == selected);
			if (activeNotes != null)
			{
				ActiveNotes = activeNotes;
				DisplayNotesForChapter();
			}
		}

		public void ExportNotes(string fileName)
		{
			NotesCollectionToRtfConverter.ExportNotes(fileName, ActiveNotes);
		}
	}
}
