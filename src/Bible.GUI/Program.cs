using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using Bible.Presentation;

namespace Bible
{
	static class Program
	{
		private static MainForm _mainForm;
		private static BackgroundLoader _loader;

		[STAThread]
		static void Main()
		{
			Application.SetCompatibleTextRenderingDefault(false);

			SplashForm splash = new SplashForm();
			splash.Show();
			Application.DoEvents();

			UserPreferences.Load();
			BibleStore.Initialize();

			_loader = new BackgroundLoader(UserPreferences.I.PreferredVersion);
			_loader.ChapterLoadException += new EventHandler<ExceptionEventArgs>(loader_ChapterLoadException);
			_loader.ProcessException += new EventHandler<ExceptionEventArgs>(loader_ProcessException);
			_loader.Completed += new EventHandler(loader_Completed);
			_loader.Start();

			Application.EnableVisualStyles();

			_mainForm = new MainForm(splash);
			Application.Run(_mainForm);
			UserPreferences.Save();

			_mainForm.Dispose();
			if (_loader.IsRunning) _loader.StopProcess();
		}

		private static void loader_Completed(object sender, EventArgs e)
		{
			_mainForm.PreferredVersionLoaded();
		}

		private static void loader_ProcessException(object sender, ExceptionEventArgs e)
		{
			MessageBox.Show(_mainForm, "Error while background loading preferred version.\r\n" + e.Exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		private static void loader_ChapterLoadException(object sender, ExceptionEventArgs e)
		{
			MessageBox.Show(_mainForm, "Error while background loading a chapter for the preferred version.\r\n" + e.Exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}
	}
}
