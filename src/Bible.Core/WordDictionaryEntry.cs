using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Bible
{
	[Serializable]
	public class WordDictionaryEntry
	{
		public WordDictionaryEntry()
		{
			Definitions = new List<WordDefinition>();
		}

		[XmlAttribute]
		public string Word { get; set; }
		[XmlElement("Definition")]
		public List<WordDefinition> Definitions { get; set; }
	}

	[Serializable]
	public class WordDefinition
	{
		public string Text { get; set; }
		public bool IsHeader { get; set; }
		public bool IsExample { get; set; }
	}

	[Serializable]
	public class WordDictionary : Dictionary<string, WordDictionaryEntry>
	{
		public WordDictionary() { }
		protected WordDictionary(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
