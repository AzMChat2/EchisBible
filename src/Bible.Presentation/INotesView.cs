using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bible.Presentation
{
	public interface INotesView : IView
	{
		bool IsNotesEmpty { get; }
		string RtfText { get; set; }

		void PopulateNotesCombo();
		bool PromptForNotesFileName(NotesCollection notes);
		bool PromptToSaveNote(NotesCollection note);
	}
}
