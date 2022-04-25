using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Bible
{
	[Serializable]
	public class Version
	{
		[XmlAttribute]
		public int VersionId { get; set; }
		[XmlAttribute]
		public string VersionName { get; set; }
		[XmlAttribute]
		public string LongName { get; set; }
		[XmlAttribute]
		public string Copyright { get; set; }
	}

	[Serializable]
	public class VersionCollection : ListEx<Version>
	{
	}
}
