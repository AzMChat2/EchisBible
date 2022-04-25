using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics.CodeAnalysis;

namespace Bible
{
	public class BackgroundLoader : WorkerThread
	{
		private int _versionId;
		private int _errorCount;

		public BackgroundLoader(int versionId)
		{
			_versionId = versionId;
		}

		public event EventHandler<ExceptionEventArgs> ChapterLoadException;
		private void OnChapterLoadException(Exception ex)
		{
			if (ChapterLoadException != null) ChapterLoadException.Invoke(null, new ExceptionEventArgs(ex));
		}

		protected override void Execute()
		{
			BibleStore.Books.ForEach(LoadBook);
			BibleStore.Books.CheckText();
		}

		private void LoadBook(Book book)
		{
			if (!Stopping)
			{
				book.Chapters.ForEach(chapter => LoadChapter(book, chapter, _versionId));
			}
		}

		[SuppressMessage("Microsoft.Design", "CA1031", Justification = "Implementation of IBibleService is unknown, impossible to tell what type of exception might be caught.")]
		private void LoadChapter(Book book, Chapter chapter, int versionId)
		{
			if (!Stopping)
			{
				if (!chapter.LoadedVersions.Contains(versionId))
				{
					try
					{
						BibleStore.BibleService.LoadChapter(book, chapter, versionId);
					}
					catch (Exception ex)
					{
						_errorCount++;
						if (_errorCount > 3)
						{
							throw new ApplicationException("Multiple errors encountered while background loading preferred version. Unable to load preferred version.", ex);
						}
						else
						{
							OnChapterLoadException(ex);
						}
					}
				}
			}
		}
	}
}
