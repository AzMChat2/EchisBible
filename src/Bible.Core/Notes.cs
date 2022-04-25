using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Bible
{
	public class Notes
	{
		[XmlAttribute]
		public int BookId { get; set; }
		[XmlAttribute]
		public int ChapterId { get; set; }
		[XmlElement]
		public string NotesText { get; set; }
		[XmlIgnore]
		public bool Dirty { get; set; }

	}

	public class NotesCollection : ListEx<Notes>
	{
		private static XmlSerializer serializer = new XmlSerializer(typeof(NotesCollection));
		public static NotesCollection Load(string fileName)
		{
			using (Stream stream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.None))
			{
				NotesCollection retVal = (NotesCollection)serializer.Deserialize(stream);
				retVal.FileName = fileName;
				retVal.ForEach(item => item.Dirty = false);
				return retVal;
			}
		}

		public void Save()
		{
			using (Stream stream = File.Open(FileName, FileMode.Create, FileAccess.Write, FileShare.None))
			{
				serializer.Serialize(stream, this);
				this.ForEach(item => item.Dirty = false);
			}
		}

		[XmlIgnore]
		public string FileName { get; set; }

		[XmlIgnore]
		public string DisplayName
		{
			get { return Path.GetFileNameWithoutExtension(FileName); }
		}

		[XmlIgnore]
		public bool Dirty
		{
			get { return Exists(item => item.Dirty); }
		}

		public override string ToString()
		{
			return DisplayName;
		}
	}
}
