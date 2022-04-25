using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace Bible.Presentation
{
	public static class ScriptResources
	{
		private static string _toolTipScript;
		public static string ToolTipScript
		{
			get
			{
				if (_toolTipScript == null)
				{
					using (StreamReader reader = new StreamReader(GetAssemblyResourceStream("Bible.Presentation.Scripts.tooltip.js")))
					{
						_toolTipScript = reader.ReadToEnd();
					}
				}
				return _toolTipScript;
			}
		}

		private static string _balloonScript;
		public static string BalloonScript
		{
			get
			{
				if (_balloonScript == null)
				{
					using (StreamReader reader = new StreamReader(GetAssemblyResourceStream("Bible.Presentation.Scripts.balloon.js")))
					{
						_balloonScript = reader.ReadToEnd();
					}
				}
				return _balloonScript;
			}
		}

		private static Stream GetAssemblyResourceStream(string resourceName)
		{
			Assembly assembly = Assembly.Load("Bible.Presentation");
			return assembly.GetManifestResourceStream(resourceName);
		}
	}
}
