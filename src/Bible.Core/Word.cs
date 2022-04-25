using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Bible
{
	[Serializable]
	public class Word
	{
		public Word()
		{
			Strongs = new List<int>();
		}

		[XmlAttribute]
		public int WordId { get; set; }
		[XmlAttribute]
		public int WordIndex { get; set; }
		[XmlAttribute]
		public string WordText { get; set; }
		[XmlAttribute]
		public bool IsNIT { get; set; }
		[XmlAttribute]
		public bool IsWOC { get; set; }

		[XmlElement("StrongsId")]
		public List<int> Strongs { get; set; }
	}

	[Serializable]
	public class WordCollection : ListEx<Word>
	{
	}
}
