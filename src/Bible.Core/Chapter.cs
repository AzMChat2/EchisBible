using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Bible
{
	[Serializable]
	public class Chapter
	{
		public Chapter()
		{
			Verses = new VerseCollection();
			LoadedVersions = new List<int>();
		}

		[XmlAttribute]
		public int ChapterId { get; set; }
		[XmlAttribute]
		public int ChapterNumber { get; set; }

		[XmlElement("Verse")]
		public VerseCollection Verses { get; set; }

		[XmlIgnore]
		public List<int> LoadedVersions { get; private set; }

		internal void CheckText()
		{
			LoadedVersions.ForEach(Verses.CheckText);
		}
	}

	[Serializable]
	public class ChapterCollection : ListEx<Chapter>
	{
		internal void CheckText()
		{
			ForEach(item => item.CheckText());
		}
	}
}
