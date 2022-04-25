using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Bible.Presentation
{
	public class ChapterController
	{
		private const string ToolTipFormat = "{0} Chapter {1}";

		private IChapterView _view;

		public ChapterController(IChapterView view)
		{
			_view = view;
		}

		public Version CurrentVersion { get; private set; }

		public Book CurrentBook { get; private set; }
		public Book PrevBook { get; private set; }
		public Book NextBook { get; private set; }

		public Chapter CurrentChapter { get; private set; }
		public Chapter PrevChapter { get; private set; }
		public Chapter NextChapter { get; private set; }

		public string DisplayName
		{
			get { return string.Format(CultureInfo.CurrentCulture, ToolTipFormat, CurrentBook.BookName, CurrentChapter.ChapterNumber); }
		}

		public string PrevBookToolTip
		{
			get { return PrevBook == null ? string.Empty : string.Format(CultureInfo.CurrentCulture, ToolTipFormat, PrevBook.BookName, 1); }
		}

		public string NextBookToolTip
		{
			get { return NextBook == null ? string.Empty : string.Format(CultureInfo.CurrentCulture, ToolTipFormat, NextBook.BookName, 1); }
		}

		public string PrevChapterToolTip
		{
			get
			{
				string retVal = string.Empty;

				if (PrevChapter != null)
				{
					Book book = CurrentBook.Chapters.Contains(PrevChapter) ? CurrentBook : PrevBook;
					retVal = string.Format(CultureInfo.CurrentCulture, ToolTipFormat, book.BookName, PrevChapter.ChapterNumber);
				}

				return retVal;
			}
		}

		public string NextChapterToolTip
		{
			get
			{
				string retVal = string.Empty;

				if (NextChapter != null)
				{
					Book book = CurrentBook.Chapters.Contains(NextChapter) ? CurrentBook : NextBook;
					retVal = string.Format(CultureInfo.CurrentCulture, ToolTipFormat, book.BookName, NextChapter.ChapterNumber);
				}

				return retVal;
			}
		}

		public void ChangeVersion(int versionId)
		{
			CurrentVersion = BibleStore.Versions.Find(item => item.VersionId == versionId);
			CheckVersionLoaded();
			_view.UpdateDisplay();
		}

		public bool IsValid
		{
			get { return (CurrentChapter != null) && (CurrentBook != null) && (CurrentVersion != null); }
		}

		public void MoveTo(Version version, Book book, Chapter chapter)
		{
			CurrentVersion = version;
			CurrentBook = book;
			CurrentChapter = chapter;
			Update();
		}

		public void MoveToBook(int bookId)
		{
			CurrentBook = BibleStore.Books.Find(item => item.BookId == bookId);
			MoveToFirstChapter();
		}

		public void MoveToNextBook()
		{
			CurrentBook = NextBook;
			MoveToFirstChapter();
		}

		public void MoveToPrevBook()
		{
			CurrentBook = PrevBook;
			MoveToFirstChapter();
		}

		private void MoveToFirstChapter()
		{
			CurrentChapter = CurrentBook.Chapters.Find(item => item.ChapterNumber == 1);
			Update();
		}

		public void MoveToChapter(int chapterId)
		{
			CurrentChapter = CurrentBook.Chapters.Find(item => item.ChapterId == chapterId);
			Update();
		}

		public void MoveToNextChapter()
		{
			CurrentChapter = NextChapter;
			if (!CurrentBook.Chapters.Contains(CurrentChapter)) CurrentBook = NextBook;
			Update();
		}

		public void MoveToPrevChapter()
		{
			CurrentChapter = PrevChapter;
			if (!CurrentBook.Chapters.Contains(CurrentChapter)) CurrentBook = PrevBook;
			Update();
		}

		private void Update()
		{
			int prevBookId = CurrentBook.BookId - 1;
			int nextBookId = CurrentBook.BookId + 1;

			PrevBook = BibleStore.Books.Find(item => item.BookId == prevBookId);
			NextBook = BibleStore.Books.Find(item => item.BookId == nextBookId);

			int prevChapterId = CurrentChapter.ChapterId - 1;
			int nextChapterId = CurrentChapter.ChapterId + 1;

			PrevChapter = CurrentBook.Chapters.Find(item => item.ChapterId == prevChapterId);
			if ((PrevChapter == null) && (PrevBook != null))
			{
				PrevChapter = PrevBook.Chapters.Find(item => item.ChapterId == prevChapterId);
			}

			NextChapter = CurrentBook.Chapters.Find(item => item.ChapterId == nextChapterId);
			if ((NextChapter == null) && (NextBook != null))
			{
				NextChapter = NextBook.Chapters.Find(item => item.ChapterId == nextChapterId);
			}

			CheckVersionLoaded();
			_view.UpdateDisplay();
		}

		private void CheckVersionLoaded()
		{
			if (IsValid)
			{
				if (!CurrentChapter.LoadedVersions.Contains(CurrentVersion.VersionId))
				{
					BibleStore.BibleService.LoadChapter(CurrentBook, CurrentChapter, CurrentVersion.VersionId);
				}
			}
		}

		public string RenderChapter(IBibleRenderer renderer)
		{
			return renderer.RenderChapter(CurrentVersion, CurrentBook, CurrentChapter);
		}
	}
}
