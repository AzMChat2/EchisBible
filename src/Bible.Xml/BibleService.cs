using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO.Compression;

namespace Bible.Xml
{
	public class BibleService : IBibleService
	{
		private Assembly common;
		private Dictionary<int, Assembly> versions;
		private CrossRefCollection crossRefs;

		public BibleService()
		{
			common = Assembly.Load("Bible.Xml");
			versions = new Dictionary<int, Assembly>();
		}

		private static object LoadObject(Assembly assembly, string resourceName, Type type)
		{
			return LoadObject(assembly, resourceName, new XmlSerializer(type));
		}

		private static object LoadObject(Assembly assembly, string resourceName, XmlSerializer serializer)
		{
			using (Stream stream = assembly.GetManifestResourceStream(resourceName))
			{
				using (DeflateStream def = new DeflateStream(stream, CompressionMode.Decompress))
				{
					return serializer.Deserialize(def);
				}
			}
		}

		public StrongsCollection GetStrongsCollection()
		{
			crossRefs = (CrossRefCollection)LoadObject(common, "Bible.Xml.Resources.CrossRefs.dat", typeof(CrossRefCollection));
			return (StrongsCollection)LoadObject(common, "Bible.Xml.Resources.Strongs.dat", typeof(StrongsCollection));
		}

		public BookCollection GetBookCollection()
		{
			return (BookCollection)LoadObject(common, "Bible.Xml.Resources.Books.dat", typeof(BookCollection));
		}

		public WordDictionary GetWordDictionary()
		{
			List<WordDictionaryEntry> list = (List<WordDictionaryEntry>)LoadObject(common, "Bible.Xml.Resources.Dictionary.dat", typeof(List<WordDictionaryEntry>));

			WordDictionary retVal = new WordDictionary();
			list.ForEach(item => retVal.Add(item.Word, item));

			return retVal;
		}

		public VersionCollection GetAvailableVersions()
		{
			VersionCollection versionList = new VersionCollection();

			string[] potentialVersions = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "Bible.Xml.*.dll");

			foreach (string potentialVersion in potentialVersions)
			{
				Version version = LoadAssembly(potentialVersion);
				if (version != null) versionList.Add(version);
			}

			return versionList;
		}

		[SuppressMessage("Microsoft.Reliability", "CA2001", Justification = "This method is testing for a missing assembly file.")]
		[SuppressMessage("Microsoft.Design", "CA1031",
			Justification = "This method is attempting to find a Version resource in a satellite assembly, various exceptions may occur. " + 
			"The assembly is simply assumed to be invalid if an exception occurs.")]
		private Version LoadAssembly(string potentialVersion)
		{
			Version retVal = null;
			Assembly assembly = null;

			try
			{
				assembly = Assembly.LoadFile(potentialVersion);
				retVal = (Version)LoadObject(assembly, "Bible.Xml.Resources.Version.dat", typeof(Version));
			}
			catch { }
			
			if ((assembly != null) && (retVal != null))
			{
				versions.Add(retVal.VersionId, assembly);
			}

			return retVal;
		}

		private static XmlSerializer chapterSerializer = new XmlSerializer(typeof(Book));
		public void LoadChapter(Book book, Chapter chapter, int versionId)
		{
			lock (book)
			{
				string testamentName = book.IsNewTestament ? "NewTestament" : "OldTestament";
				string resourceName = string.Format(CultureInfo.InvariantCulture, "Bible.Xml.Resources.{0}.{1}.dat", testamentName, book.BookName);

				Book bookRes = (Book)LoadObject(versions[versionId], resourceName, chapterSerializer);

				bookRes.Chapters.ForEach(item => CopyChapter(book, item, versionId));
			}
		}

		private static void CopyChapter(Book book, Chapter source, int versionId)
		{
		  Chapter chapter =	book.Chapters.Find(item => item.ChapterId == source.ChapterId);
			if (chapter != null)
			{
				if (!chapter.LoadedVersions.Contains(versionId))
				{
					source.Verses.ForEach(item => CopyVerse(chapter, item));
					chapter.LoadedVersions.Add(versionId);
				}
			}
		}

		private static void CopyVerse(Chapter chapter, Verse source)
		{
			Verse verse = chapter.Verses.Find(item => item.VerseId == source.VerseId);
			if (verse == null)
			{
				chapter.Verses.Add(source);
			}
			else
			{
				source.Versions.ForEach(item => CopyVerseVersion(verse, item));
			}
		}

		private static void CopyVerseVersion(Verse verse, VerseVersion source)
		{
			VerseVersion target = verse.Versions.Find(item => item.VersionId == source.VersionId);
			if (target == null)
			{
				verse.Versions.Add(source);
			}
		}

		public void LoadCrossRefs(Strongs strongs)
		{
			strongs.CrossRefs.AddRange(crossRefs.FindAll(item => item.StrongsId == strongs.StrongsId));
		}
	}
}
