using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bible;
using System.IO;
using System.Xml.Serialization;
using System.IO.Compression;

namespace DatGenerator
{
	public class Program
	{
		private static string rootPath = @"C:\VisualStudio\Projects\Bible\";

		static void Main(string[] args)
		{
			//ConvertVersions();
			//ConvertBooks();
			//ConvertStrongs();
			//ConvertCrossRefs();
			ConvertDictionary();
			//ConvertChapters();
		}

		private static object LoadFile(string fileName, XmlSerializer serializer)
		{
			using (Stream stream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.None))
			{
				return serializer.Deserialize(stream);
			}
		}

		private static void SaveFile(string fileName, XmlSerializer serializer, object obj)
		{
			string path = Path.GetDirectoryName(fileName);
			if (!Directory.Exists(path)) Directory.CreateDirectory(path);

			using (Stream stream = File.Open(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
			{
				using (DeflateStream def = new DeflateStream(stream, CompressionMode.Compress))
				{
					serializer.Serialize(def, obj);
				}
			}
		}

		private static void ConvertFile(string basePath, Type type)
		{
			string xmlPath = string.Format(basePath, "Xml", "xml");
			string datPath = string.Format(basePath, "Resources", "dat");

			XmlSerializer serializer = new XmlSerializer(type);
			object obj = LoadFile(xmlPath, serializer);
			SaveFile(datPath, serializer, obj);
		}

		private static XmlSerializer bookSerializer = new XmlSerializer(typeof(Book));
		private static void ConvertBookFile(string basePath)
		{
			string xmlPath = string.Format(basePath, "Xml", "xml");
			string datPath = string.Format(basePath, "Resources", "dat");

			object obj = LoadFile(xmlPath, bookSerializer);
			SaveFile(datPath, bookSerializer, obj);
		}

		private static void ConvertDictionary()
		{
			Console.WriteLine("Converting Dictionary");
			ConvertFile(rootPath + @"Bible.Xml\{0}\Dictionary.{1}", typeof(List<WordDictionaryEntry>));
		}

		private static void ConvertCrossRefs()
		{
			Console.WriteLine("Converting Cross Refs");
			ConvertFile(rootPath + @"Bible.Xml\{0}\CrossRefs.{1}", typeof(CrossRefCollection));
		}

		private static void ConvertStrongs()
		{
			Console.WriteLine("Converting Strongs");
			ConvertFile(rootPath + @"Bible.Xml\{0}\Strongs.{1}", typeof(StrongsCollection));
		}

		private static void ConvertBooks()
		{
			Console.WriteLine("Converting Books");
			ConvertFile(rootPath + @"Bible.Xml\{0}\Books.{1}", typeof(BookCollection));
		}

		private static void ConvertVersions()
		{
			Console.WriteLine("Converting Versions");
			ConvertFile(rootPath + @"Bible.Xml\{0}\Versions.{1}", typeof(VersionCollection));
		}

		private static void ConvertChapters()
		{
			Console.WriteLine("Converting King James Chapters");
			ConvertChapters(rootPath + @"Bible.Xml.KJ\");

			Console.WriteLine("Converting 21st Century King James Chapters");
			ConvertChapters(rootPath + @"Bible.Xml.KJ21\");

			Console.WriteLine("Converting New American Standard Bible Chapters");
			ConvertChapters(rootPath + @"Bible.Xml.NASB\");

			Console.WriteLine("Converting New International Version Chapters");
			ConvertChapters(rootPath + @"Bible.Xml.NIV\");

			Console.WriteLine("Converting New King James Version Chapters");
			ConvertChapters(rootPath + @"Bible.Xml.NKJV\");

			Console.WriteLine("Converting Young Literal Translation Chapters");
			ConvertChapters(rootPath + @"Bible.Xml.YLT\");
		}

		private static void ConvertChapters(string path)
		{
			ConvertOldTestament(path);
			ConvertNewTestament(path);
		}

		private static void ConvertNewTestament(string path)
		{
			Console.WriteLine("Converting New Testament");
			ConvertBooks(path + @"{0}\NewTestament\");
		}

		private static void ConvertOldTestament(string path)
		{
			Console.WriteLine("Converting Old Testament");
			ConvertBooks(path + @"{0}\OldTestament\");
		}

		private static void ConvertBooks(string path)
		{
			string xmlPath = string.Format(path, "Xml");
			string[] files = Directory.GetFiles(xmlPath, "*.xml");

			foreach (string file in files)
			{
				string fileName = Path.GetFileNameWithoutExtension(file);
				ConvertBookFile(path + fileName + ".{1}");
			}
		}
	}
}
