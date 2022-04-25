using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Echis.Data.Interfaces;
using System.Data;

namespace Bible.Database
{
	internal class StrongsLoader : IDataLoader
	{
		public StrongsCollection Data { get; private set; }
		public void ReadData(IDataReader reader)
		{
			Data = new StrongsCollection();

			while (reader.Read())
			{
				Strongs item = new Strongs();

				item.StrongsId = reader.GetInt("StrongsId");
				item.StrongsNumber = reader.GetInt("StrongsNumber");
				item.IsGreek = reader.GetBoolean("IsGreek");
				item.OriginalWord = reader.GetString("OriginalWord");
				item.Phonetic = reader.GetString("Phonetic");
				item.SpeachPart = reader.GetString("SpeachPart");
				item.Transliteration = reader.GetString("Transliteration");
				item.WordOrigin = reader.GetString("WordOrigin");
				item.WordUsage = reader.GetString("WordUsage");
				item.Definitions.AddRange(reader.GetStringArray("Definitions", '|'));

				Data.Add(item);
			}

			if (reader.NextResult())
			{
				while (reader.Read())
				{
					int strongsId = reader.GetInt("StrongsId");
					Strongs strongs = Data.Find(item => item.StrongsId == strongsId);
					if (strongs != null)
					{
						CrossRef item = new CrossRef();

						item.StrongsId = strongsId;
						item.CrossRefId = reader.GetInt("CrossRefId");
						item.VerseId = reader.GetInt("VerseId");

						strongs.CrossRefs.Add(item);
					}
				}
			}
		}
	}
}
