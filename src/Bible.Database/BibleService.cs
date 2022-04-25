using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Echis.Data.Interfaces;
using Echis.Data;
using System.Threading;

namespace Bible.Database
{
	public class BibleService : IBibleService
	{
		private const string DbName = "Bible";

		public VersionCollection GetAvailableVersions()
		{
			IDataLoaderCommand<VersionLoader> command = DataLoaderCommand.CreateStoredProcCommand(new VersionLoader(), DbName, "GetVersions");
			DataAccess.ExecuteDataLoader(command);
			return command.DataLoader.Data;
		}

		public BookCollection GetBookCollection()
		{
			IDataLoaderCommand<BookLoader> command = DataLoaderCommand.CreateStoredProcCommand(new BookLoader(), DbName, "GetBooks");
			DataAccess.ExecuteDataLoader(command);
			return command.DataLoader.Data;
		}

		public void LoadChapter(Book book, Chapter chapter, int versionId)
		{
			lock (chapter)
			{
				if (!chapter.LoadedVersions.Contains(versionId))
				{
					IDataLoaderCommand<ChapterLoader> command = DataLoaderCommand.CreateStoredProcCommand(new ChapterLoader(chapter), DbName, "GetChapter",
						new QueryParameter("ChapterId", chapter.ChapterId), new QueryParameter("VersionId", versionId));
					DataAccess.ExecuteDataLoader(command);

					chapter.LoadedVersions.Add(versionId);
				}
			}
		}

		public StrongsCollection GetStrongsCollection()
		{
			IDataLoaderCommand<StrongsLoader> command = DataLoaderCommand.CreateStoredProcCommand(new StrongsLoader(), DbName, "GetStrongs");
			DataAccess.ExecuteDataLoader(command);
			return command.DataLoader.Data;
		}

		public void LoadCrossRefs(Strongs strongs)
		{
			// TODO... 
		}


		public WordDictionary GetWordDictionary()
		{
			return null;
		}
	}
}
