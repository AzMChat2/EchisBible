using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Globalization;

namespace Bible
{
	[Serializable]
	public class Strongs
	{
		public Strongs()
		{
			Definitions = new List<string>();
			CrossRefs = new CrossRefCollection();
		}

		[XmlAttribute]
		public int StrongsId { get; set; }
		[XmlAttribute]
		public int StrongsNumber { get; set; }
		[XmlAttribute]
		public bool IsGreek { get; set; }
		[XmlAttribute]
		public string OriginalWord { get; set; }
		[XmlAttribute]
		public string WordOrigin { get; set; }
		[XmlAttribute]
		public string Transliteration { get; set; }
		[XmlAttribute]
		public string Phonetic { get; set; }
		[XmlAttribute]
		public string SpeachPart { get; set; }
		[XmlAttribute]
		public string WordUsage { get; set; }
		[XmlElement("Definition")]
		public List<string> Definitions { get; set; }
		[XmlElement("CrossRef")]
		public CrossRefCollection CrossRefs { get; set; }
		[XmlIgnore]
		public string DisplayNumber
		{
			get { return (IsGreek ? "G" : "H") + StrongsNumber.ToString(CultureInfo.CurrentCulture); }
		}
	}

	[Serializable]
	public class StrongsCollection : ListEx<Strongs>
	{
		private Dictionary<int, int> index;
		public Strongs GetByStrongsId(int strongsId)
		{
			if (index == null)
			{
				InitializeIndex();
			}

			return this[index[strongsId]];
		}

		private void InitializeIndex()
		{
			index = new Dictionary<int, int>();
			for (int idx = 0; idx < Count; idx++)
			{
				index.Add(this[idx].StrongsId, idx);
			}
		}
	}
}
