using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bible.Presentation
{
	public class MainController
	{
		private IMainView _view;
		private NotesController _notesController;

		public MainController(IMainView view, NotesController notesController)
		{
			_view = view;
			_notesController = notesController;

			ObjectForScripting = new ScriptingObject(view);
			StandardRenderer = new StandardBibleRenderer();
		}

		public ScriptingObject ObjectForScripting { get; private set; }
		public IBibleRenderer StandardRenderer { get; private set; }

		public static List<string> GetWordVariations(string word)
		{
			List<string> retVal = new List<string>();

			if (word.EndsWith("th", StringComparison.OrdinalIgnoreCase) ||
				word.EndsWith("ed", StringComparison.OrdinalIgnoreCase) ||
				word.EndsWith("es", StringComparison.OrdinalIgnoreCase) ||
				word.EndsWith("st", StringComparison.OrdinalIgnoreCase))
			{
				retVal.Add(word.Substring(0, word.Length - 2));
			}

			if (word.EndsWith("eth", StringComparison.OrdinalIgnoreCase) ||
				word.EndsWith("ing", StringComparison.OrdinalIgnoreCase) ||
				word.EndsWith("est", StringComparison.OrdinalIgnoreCase))
			{
				retVal.Add(word.Substring(0, word.Length - 3));
			}

			if (word.EndsWith("d", StringComparison.OrdinalIgnoreCase) ||
				word.EndsWith("s", StringComparison.OrdinalIgnoreCase))
			{
				retVal.Add(word.Substring(0, word.Length - 1));
			}

			if (word.EndsWith("ied", StringComparison.OrdinalIgnoreCase))
			{
				retVal.Add(word.Substring(0, word.Length - 3) + "y");
			}

			return retVal;
		}

		public void DisplayNotesForChapter(int bookId, int chapterId)
		{
			_notesController.BookId = bookId;
			_notesController.ChapterId = chapterId;
			_notesController.DisplayNotesForChapter();
		}
	}
}
