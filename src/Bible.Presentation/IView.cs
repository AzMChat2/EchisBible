using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bible.Presentation
{
	public interface IView
	{
		DialogResult ShowMessage(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon);
	}
}
