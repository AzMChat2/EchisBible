using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bible;
using System.Xml.Serialization;
using System.IO;
using Echis.Data.Interfaces;
using System.Data;
using Echis.Data;
using System.Text.RegularExpressions;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;
using BibleVersion = Bible.Version;

namespace XmlGenerator
{
	class Program
	{
		private static string rootPath = "C:\\Bible\\Xml\\";

		static void Main(string[] args)
		{
			BibleStore.Initialize();
			Console.WriteLine("Bible Store Initialized");

			//SerializeBookList();
			//SerializeVersionList();
			//SerializeStrongsConcordance();
			//SerializeStrongsCrossRefs();
			//SerializeBooks();

			//DownloadDictionary();

			//BinaryFormatter serializer = new BinaryFormatter();

			//FixDictionary();
			SeparateVersions();

			Console.WriteLine("Done");

			Console.ReadLine();
		}

		private static void SeparateVersions()
		{
			BibleStore.Versions.ForEach(WriteVersionFile);
		}

		private static XmlSerializer versionSerializer = new XmlSerializer(typeof(BibleVersion));
		private static void WriteVersionFile(BibleVersion version)
		{
			Console.WriteLine(version.VersionName);
			string fileName = string.Format(@"C:\VisualStudio\Projects\Bible\Bible.Xml.{0}\Resources\Version.dat", version.VersionName);
			using (Stream stream = File.Open(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
			{
				using (DeflateStream def = new DeflateStream(stream, CompressionMode.Compress))
				{
					versionSerializer.Serialize(def, version);
				}
			}
		}

		private static void FixDictionary()
		{
			XmlSerializer serializer = new XmlSerializer(typeof(List<WordDictionaryEntry>));

			List<WordDictionaryEntry> list = (List<WordDictionaryEntry>)LoadFile(@"C:\VisualStudio\Projects\Bible\Bible.Xml\Xml\Dictionary.xml", serializer);
			list.ForEach(FixEntry);
			SaveFile(@"C:\VisualStudio\Projects\Bible\Bible.Xml\Xml\Dictionary.xml", serializer, list);
		}

		private static void FixEntry(WordDictionaryEntry item)
		{
			item.Definitions.ForEach(FixDefinition);
		}

		private static void FixDefinition(WordDefinition item)
		{
			item.Text = item.Text.Replace("''", string.Empty);
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
				serializer.Serialize(stream, obj);
			}
		}


		private static Regex splitter = new Regex("<[pP]>", RegexOptions.Compiled);
		private static Regex scrubber = new Regex("<[^>]*>", RegexOptions.Compiled);
		private static Regex nbspFinder = new Regex("&[nN][bB][sS][pP];", RegexOptions.Compiled);

		private static Regex header = new Regex("<[bB]>", RegexOptions.Compiled);
		private static Regex example = new Regex("<[dD][dD]><[dD][dD]>", RegexOptions.Compiled);

		private static void ProcessFile(WordDictionaryEntry word)
		{
			string fileName = string.Format("C:\\Bible\\Dictionary\\{0}\\{1}\\{2}.htm", word.Word.Substring(0, 1), word.Word.Substring(1, 1), word.Word);
			string rawHtml = FindDefinition(fileName);

			if (!string.IsNullOrEmpty(rawHtml))
			{
				string[] parts = splitter.Split(rawHtml);
				foreach (string part in parts)
				{
					string scrubbed = nbspFinder.Replace(part, " ");
					scrubbed = scrubber.Replace(scrubbed, string.Empty);
					scrubbed = scrubbed.Trim();

					if (!string.IsNullOrEmpty(scrubbed))
					{
						WordDefinition def = new WordDefinition();

						def.Text = scrubbed;
						def.IsHeader = header.IsMatch(part);
						def.IsExample = example.IsMatch(part);

						word.Definitions.Add(def);
					}
				}
			}
		}


		private static string FindDefinition(string fileName)
		{
			StringBuilder retVal = null;

			using (StreamReader reader = new StreamReader(fileName))
			{
				bool foundMainDiv = false;

				while (!reader.EndOfStream)
				{
					string line = reader.ReadLine().Trim();

					if (foundMainDiv)
					{
						if ((retVal == null) && (line.IndexOf("<p>", StringComparison.OrdinalIgnoreCase) != -1))
						{
							retVal = new StringBuilder();
						}

						if (retVal != null)
						{
							int idx = line.IndexOf("</div>", StringComparison.OrdinalIgnoreCase);
							if (idx == -1)
							{
								retVal.Append(line);
							}
							else
							{
								retVal.Append(line.Substring(0, idx));
								break;
							}
						}
					}
					else
					{
						foundMainDiv = (line.IndexOf("<div id=\"main\">", StringComparison.OrdinalIgnoreCase) != -1);
					}
				}
			}

			return (retVal == null) ? null : retVal.ToString();
		}


		private static void DownloadDictionary()
		{
			//Console.WriteLine("Loading Word Dictionary");

			//IDataLoaderCommand<WordLoader> command = DataLoaderCommand.CreateSqlCommand(new WordLoader(), "Bible", "SELECT DISTINCT Word FROM dbo.Words");
			//DataAccess.ExecuteDataLoader(command);

			XmlSerializer serializer = new XmlSerializer(typeof(List<WordDictionaryEntry>));
			List<WordDictionaryEntry> list = null;

			using (Stream stream = File.Open("C:\\Bible\\Dictionary.xml", FileMode.Open, FileAccess.Read, FileShare.None))
			{
				list = (List<WordDictionaryEntry>)serializer.Deserialize(stream);
			}

			Console.WriteLine("Word Dictionary has been loaded: {0} total words", list.Count);

			//list.ForEach(DownloadDefinition);
			list.ForEach(ProcessFile);
			list.RemoveAll(item => (item.Definitions.Count == 0));

			Console.WriteLine("All words processed: {0} total words", list.Count);

			using (Stream stream = File.Open("C:\\Bible\\DictionaryDone.xml", FileMode.Create, FileAccess.Write, FileShare.None))
			{
				serializer.Serialize(stream, list);
			}

			Console.WriteLine("Dictionary File Saved.");

		}

		private static int wordCount = 0;
		private static WebClient webClient = new WebClient();
		private static void DownloadDefinition(WordDictionaryEntry item)
		{
			if ((++wordCount % 100) == 0) Console.WriteLine("{0} words downloaded", wordCount);
			
			string fileName = string.Format("C:\\Bible\\Dictionary\\{0}\\{1}\\{2}.htm", item.Word.Substring(0, 1), item.Word.Substring(1, 1), item.Word);
			string url = string.Format("http://machaut.uchicago.edu/?resource=Webster%27s&word={0}&use1828=on", item.Word);
			
			string path = Path.GetDirectoryName(fileName);
			if (!Directory.Exists(path)) Directory.CreateDirectory(path);

			webClient.DownloadFile(url, fileName);
		}

		private static void SerializeBooks()
		{
			BibleStore.Books.ForEach(SerializeBook);
			Console.WriteLine("All Books Serialized");
		}

		private static void SerializeStrongsCrossRefs()
		{
			CrossRefCollection crossRefs = LoadCrossRefCollection();
			XmlSerializer crossRefSerializer = new XmlSerializer(typeof(CrossRefCollection));
			using (Stream stream = File.Open(rootPath + "CrosRefs.xml", FileMode.Create, FileAccess.Write, FileShare.None))
			{
				crossRefSerializer.Serialize(stream, crossRefs);
			}
			Console.WriteLine("Strongs/Verse CrossRefs serialized");
		}

		private static void SerializeStrongsConcordance()
		{
			XmlSerializer strongsSerializer = new XmlSerializer(typeof(StrongsCollection));
			using (Stream stream = File.Open(rootPath + "Strongs.xml", FileMode.Create, FileAccess.Write, FileShare.None))
			{
				strongsSerializer.Serialize(stream, BibleStore.Strongs);
			}
			Console.WriteLine("Strongs dictionary serialized");
		}

		private static void SerializeVersionList()
		{
			XmlSerializer versionSerializer = new XmlSerializer(typeof(VersionCollection));
			using (Stream stream = File.Open(rootPath + "Versions.xml", FileMode.Create, FileAccess.Write, FileShare.None))
			{
				versionSerializer.Serialize(stream, BibleStore.Versions);
			}
			Console.WriteLine("Version list serialized");
		}

		private static void SerializeBookList()
		{
			XmlSerializer booksSerializer = new XmlSerializer(typeof(BookCollection));
			using (Stream stream = File.Open(rootPath + "Books.xml", FileMode.Create, FileAccess.Write, FileShare.None))
			{
				booksSerializer.Serialize(stream, BibleStore.Books);
			}
			Console.WriteLine("Book list serialized");
		}

		private static XmlSerializer bookSerializer = new XmlSerializer(typeof(Book));
		public static void SerializeBook(Book book)
		{
			book.Chapters.ForEach(item => BibleStore.BibleService.LoadChapter(book, item, 6));

			string path = rootPath + (book.IsNewTestament ? "NewTestament\\" : "OldTestament\\");
			string fileName = path + book.BookName + ".xml";

			using (Stream stream = File.Open(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
			{
				bookSerializer.Serialize(stream, book);
				Console.WriteLine(book.BookName + " serialized");
			}
		}

		private static CrossRefCollection LoadCrossRefCollection()
		{
			IDataLoaderCommand<CrossRefLoader> command = DataLoaderCommand.CreateSqlCommand(new CrossRefLoader(), "Bible", "SELECT CrossRefId, StrongsId, VerseId FROM dbo.CrossRefs");
			DataAccess.ExecuteDataLoader(command);
			return command.DataLoader.Data;
		}
	}

	public class CrossRefLoader : IDataLoader
	{
		public CrossRefCollection Data { get; private set; }

		public void ReadData(IDataReader reader)
		{
			Data = new CrossRefCollection();

			while (reader.Read())
			{
				CrossRef item = new CrossRef();

				item.CrossRefId = reader.GetInt("CrossRefId");
				item.StrongsId = reader.GetInt("StrongsId");
				item.VerseId = reader.GetInt("VerseId");

				Data.Add(item);
			}
		}
	}

	public class WordLoader : IDataLoader
	{
		public WordDictionary Data { get; private set; }

		public void ReadData(IDataReader reader)
		{
			Data = new WordDictionary();

			while (reader.Read())
			{
				string word = ScrubWord(reader.GetString("Word"));
				if (!string.IsNullOrEmpty(word) && (word.Length >= 2))
				{
					if (!Data.ContainsKey(word))
					{
						WordDictionaryEntry item = new WordDictionaryEntry();
						item.Word = word;
						Data.Add(word, item);
					}
				}
			}
		}

		private static Regex wordScrubber = new Regex("[^a-z]", RegexOptions.Compiled);
		private static string ScrubWord(string word)
		{
			return wordScrubber.Replace(word.ToLowerInvariant(), string.Empty);
		}
	}
}
