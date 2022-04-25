using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Echis.Data.Interfaces;
using System.Data;

namespace Bible.Database
{
	internal class BookLoader : IDataLoader
	{
		public BookCollection Data { get; private set; }

		public void ReadData(IDataReader reader)
		{
			Data = new BookCollection();

			// Load Books
			while (reader.Read())
			{
				Book item = new Book();

				item.BookId = reader.GetInt("BookId");
				item.BookName = reader.GetString("BookName");
				item.Author = reader.GetString("Author");
				item.YearWritten = reader.GetString("YearWritten");
				item.IsNewTestament = reader.GetBoolean("IsNewTestament");

				Data.Add(item);
			}

			// Load Chapters
			if (reader.NextResult())
			{
				while (reader.Read())
				{
					int bookId = reader.GetInt("BookId");

					Book book = Data.Find(item => item.BookId == bookId);

					if (book != null)
					{
						Chapter item = new Chapter();

						item.ChapterId = reader.GetInt("ChapterId");
						item.ChapterNumber = reader.GetInt("Chapter");

						book.Chapters.Add(item);
					}
				}
			}
		}
	}
}
