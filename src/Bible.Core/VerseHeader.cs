using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Bible
{
	[Serializable]
	public class VerseHeader
	{
		[XmlAttribute]
		public int HeaderId { get; set; }
		[XmlAttribute]
		public string HeaderText { get; set; }
	}
}
