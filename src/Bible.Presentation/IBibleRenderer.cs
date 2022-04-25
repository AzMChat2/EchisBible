using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bible.Presentation
{
	public interface IBibleRenderer
	{
		string RenderChapter(Version version, Book book, Chapter chapter);
	}
}
