using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace Bible.Presentation
{
	public delegate void RenderHandler(XmlTextWriter writer);

	public abstract class HtmlRendererBase
	{
		protected static string Render(RenderHandler renderer)
		{
			using (MemoryStream stream = new MemoryStream())
			{
				using (XmlTextWriter writer = new XmlTextWriter(stream, UnicodeEncoding.UTF8))
				{
					writer.Formatting = Formatting.Indented;
					writer.WriteStartElement("html");
					WriteHeader(writer);
					writer.WriteStartElement("body");

					renderer.Invoke(writer);

					writer.WriteEndElement();
					writer.WriteEndElement();

					writer.Flush();
					writer.Close();
				}
				return UnicodeEncoding.UTF8.GetString(stream.ToArray());
			}
		}

		private static void WriteHeader(XmlTextWriter writer)
		{
			writer.WriteStartElement("head");

			writer.WriteStartElement("meta");
			writer.WriteAttributeString("http-equiv", "content-type");
			writer.WriteAttributeString("content", "text/html; charset=utf-8");
			writer.WriteEndElement(); // meta

			writer.WriteStartElement("style");
			writer.WriteAttributeString("type", "text/css");
			writer.WriteValue("\r\n");

			writer.WriteString("body {");
			writer.WriteString("background-color: #ffffff;");
			writer.WriteString("margin-top: 0;");
			writer.WriteString("margin-left: 5px;");
			writer.WriteString("margin-right: 5px;");
			writer.WriteString("margin-bottom: 0; }");
			writer.WriteValue("\r\n");

			UserPreferences.I.WriteTextStyles(writer);
			writer.WriteValue("\r\n");

			writer.WriteEndElement(); // stytle
			writer.WriteEndElement(); // head
		}
	}
}
