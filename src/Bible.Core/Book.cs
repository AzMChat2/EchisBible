using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Bible
{
	[Serializable]
	public class Book
	{
		public Book()
		{
			Chapters = new ChapterCollection();
		}

		[XmlAttribute]
		public int BookId { get; set; }
		[XmlAttribute]
		public string BookName { get; set; }
		[XmlAttribute]
		public string Author { get; set; }
		[XmlAttribute]
		public string YearWritten { get; set; }
		[XmlAttribute]
		public bool IsNewTestament { get; set; }

		[XmlElement("Chapter")]
		public ChapterCollection Chapters { get; set; }

	}

	[Serializable]
	public class BookCollection : ListEx<Book>
	{
		internal void CheckText()
		{
			ForEach(item => item.Chapters.CheckText());
		}
	}
}
