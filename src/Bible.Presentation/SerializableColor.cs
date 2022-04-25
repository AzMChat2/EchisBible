using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Drawing;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;

namespace Bible.Presentation
{
	public class SerializableColor
	{
		private Color _value;
		public SerializableColor()
		{
			_value = Color.Black;
		}

		[XmlAttribute]
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "The name is the same as the property of the Color struct which this class wraps.")]
		public byte R
		{
			get { return _value.R; }
			set
			{
				if (_value.R != value) _value = Color.FromArgb(value, _value.G, _value.B);
			}
		}

		[XmlAttribute]
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "The name is the same as the property of the Color struct which this class wraps.")]
		public byte G
		{
			get { return _value.G; }
			set
			{
				if (_value.G != value) _value = Color.FromArgb(_value.R, value, _value.B);
			}
		}

		[XmlAttribute]
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "The name is the same as the property of the Color struct which this class wraps.")]
		public byte B
		{
			get { return _value.B; }
			set
			{
				if (_value.B != value) _value = Color.FromArgb(_value.R, _value.G, value);
			}
		}

		[XmlIgnore]
		public bool IsNamedColor
		{
			get { return _value.IsNamedColor; }
		}

		[XmlAttribute]
		public string Name
		{
			get
			{
				if (_value.IsNamedColor)
				{
					return _value.Name;
				}
				else
				{
					return string.Empty;
				}
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					_value = Color.FromName(value);
				}
			}
		}

		[XmlIgnore]
		public Color Value
		{
			get { return _value; }
			set { _value = value; }
		}

		[XmlIgnore]
		public string StyleValue
		{
			get { return string.Format(CultureInfo.CurrentCulture, "#{0:x2}{1:x2}{2:x2}", R, G, B); }
		}
	}

}
