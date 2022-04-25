using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Bible
{
	[Serializable]
	public class CrossRef
	{
		[XmlAttribute]
		public int CrossRefId { get; set; }
		[XmlAttribute]
		public int StrongsId { get; set; }
		[XmlAttribute]
		public int VerseId { get; set; }
	}

	[Serializable]
	public class CrossRefCollection : ListEx<CrossRef>
	{
	}
}
