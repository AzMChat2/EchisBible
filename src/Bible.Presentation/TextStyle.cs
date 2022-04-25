using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Drawing;

namespace Bible.Presentation
{
	public class TextStyle : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public TextStyle()
		{
			Visible = true;
			FontName = "Arial";
			FontSize = "12";
			Italic = false;
			Bold = false;
			Underline = false;
			Superscript = false;
			TextAlign = "Left";
			TextColor = new SerializableColor() { Value = Color.Black };
			HighliteColor = new SerializableColor() { Value = Color.White };
		}

		private bool _visible;
		[XmlAttribute]
		public bool Visible
		{
			get { return _visible; }
			set
			{
				_visible = value;
				OnPropertyChanged("Visible");
			}
		}

		private string _fontName;
		[XmlAttribute]
		public string FontName
		{
			get { return _fontName; }
			set
			{
				_fontName = value;
				OnPropertyChanged("FontName");
			}
		}

		private string _fontSize;
		[XmlAttribute]
		public string FontSize
		{
			get { return _fontSize; }
			set
			{
				_fontSize = value;
				OnPropertyChanged("FontSize");
			}
		}

		private bool _italic;
		[XmlAttribute]
		public bool Italic
		{
			get { return _italic; }
			set
			{
				_italic = value;
				OnPropertyChanged("Italic");
			}
		}

		private bool _bold;
		[XmlAttribute]
		public bool Bold
		{
			get { return _bold; }
			set
			{
				_bold = value;
				OnPropertyChanged("Bold");
			}
		}

		private bool _underline;
		[XmlAttribute]
		public bool Underline
		{
			get { return _underline; }
			set
			{
				_underline = value;
				OnPropertyChanged("Underline");
			}
		}

		private bool _superscript;
		[XmlAttribute]
		public bool Superscript
		{
			get { return _superscript; }
			set
			{
				_superscript = value;
				OnPropertyChanged("Superscript");
			}
		}

		private string _textAlign;
		[XmlAttribute]
		public string TextAlign
		{
			get { return _textAlign; }
			set
			{
				_textAlign = value;
				OnPropertyChanged("TextAlign");
			}
		}

		private SerializableColor _textColor;
		[XmlElement]
		public SerializableColor TextColor
		{
			get { return _textColor; }
			set
			{
				_textColor = value;
				OnPropertyChanged("TextColor");
			}
		}

		private SerializableColor _highliteColor;
		[XmlElement]
		public SerializableColor HighliteColor
		{
			get { return _highliteColor; }
			set
			{
				_highliteColor = value;
				OnPropertyChanged("HighliteColor");
			}
		}

		private string _cursor;
		[XmlAttribute]
		public string Cursor
		{
			get { return _cursor; }
			set
			{
				_cursor = value;
				OnPropertyChanged("Cursor");
			}
		}

		public string GetStyle(string name)
		{
			StringBuilder sb = new StringBuilder();

			sb.Append(".");
			sb.AppendLine(name);
			sb.AppendLine("{");

			if (Visible)
			{
				sb.Append("font-family: ");
				sb.Append(FontName);
				sb.AppendLine(";");

				sb.Append("font-size: ");
				sb.Append(FontSize);
				sb.AppendLine("pt;");

				sb.Append("text-align: ");
				sb.Append(TextAlign);
				sb.AppendLine(";");

				if (Italic) sb.AppendLine("font-style: italic;");

				if (Bold) sb.AppendLine("font-weight: bold;");

				if (Underline) sb.AppendLine("text-decoration: underline;");

				if (Superscript) sb.AppendLine("vertical-align: super;");

				if (!string.IsNullOrEmpty(Cursor))
				{
					sb.Append("cursor: ");
					sb.Append(Cursor);
					sb.AppendLine(";");
				}

				sb.Append("color: ");
				sb.Append(TextColor.StyleValue);
				sb.AppendLine(";");

				sb.Append("background-color: ");
				sb.Append(HighliteColor.StyleValue);
				sb.AppendLine(";");
			}
			else
			{
				sb.AppendLine("display: none;");
				sb.AppendLine("visibility: hidden;");
			}

			sb.AppendLine("}");
			sb.AppendLine();

			return sb.ToString();
		}
	}

}
