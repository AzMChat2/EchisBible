using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Echis.Data.Interfaces;
using System.Data;

namespace Bible.Database
{
	internal class VersionLoader : IDataLoader
	{

		public VersionCollection Data { get; private set; }

		public void ReadData(IDataReader reader)
		{
			Data = new VersionCollection();

			while (reader.Read())
			{
				Version item = new Version();

				item.VersionId = reader.GetInt("VersionId");
				item.VersionName = reader.GetString("VersionName");
				item.LongName = reader.GetString("LongName");
				item.Copyright = reader.GetString("Copyright");

				Data.Add(item);
			}
		}
	}
}
