using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Bible
{
	[Serializable]
	public class Verse
	{
		public Verse()
		{
			Versions = new VerseVersionCollection();
		}

		[XmlAttribute]
		public int VerseId { get; set; }
		[XmlAttribute]
		public int VerseNumber { get; set; }
		[XmlElement("VerseVersion")]
		public VerseVersionCollection Versions { get; set; }

		internal void CheckText(int versionId)
		{
			VerseVersion verseVersion = Versions.Find(item => item.VersionId == versionId);
			if (verseVersion != null) verseVersion.CheckText();
		}
	}

	[Serializable]
	public class VerseCollection : ListEx<Verse>
	{
		internal void CheckText(int versionId)
		{
			ForEach(item => item.CheckText(versionId));
		}
	}
}
