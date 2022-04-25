using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Drawing.Text;
using System.Diagnostics;
using Bible.Presentation;

namespace Bible
{
	public partial class OptionsForm : Form
	{
		public static void ShowOptions(IWin32Window owner)
		{
			OptionsForm form = new OptionsForm(UserPreferences.I.Clone());

			if (form.ShowDialog(owner) == DialogResult.OK)
			{
				UserPreferences.I = form._dataSource;
				UserPreferences.Save();
			}
		}

		private UserPreferences _dataSource;
		private TextStyle _textStyleSource;
		private SerializableColor _textColor;
		private SerializableColor _highliteColor;
		private string _sampleText;
		private PropertyChangedEventHandler _propertyChangedHandler;

		public OptionsForm(UserPreferences dataSource)
		{
			_dataSource = dataSource;
			InitializeComponent();
			treeView.ExpandAll();

			cboPreferredVersion.DisplayMember = "VersionName";
			cboPreferredVersion.ValueMember = "VersionId";
			cboPreferredVersion.DataSource = BibleStore.Versions;
			cboPreferredVersion.DataBindings.Add("SelectedValue", _dataSource, "PreferredVersion");
 
			txtNotesPath.DataBindings.Add("Text", dataSource, "NotesPath");
			txtAudioPath.DataBindings.Add("Text", dataSource, "AudioPath");

			PopulateFonts();
			PopulateColors();

			_propertyChangedHandler = new PropertyChangedEventHandler(TextStyle_PropertyChanged);
		}

		private void TextStyle_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(UpdateSampleDisplay));
			}
			else
			{
				UpdateSampleDisplay();
			}
		}

		private void UpdateSampleDisplay()
		{
			Application.DoEvents();
			using (MemoryStream stream = new MemoryStream())
			{
				using (XmlTextWriter writer = new XmlTextWriter(stream, UnicodeEncoding.UTF8))
				{
					writer.WriteStartElement("html");
					writer.WriteStartElement("head");

					writer.WriteStartElement("meta");
					writer.WriteAttributeString("http-equiv", "content-type");
					writer.WriteAttributeString("content", "text/html; charset=utf-8");
					writer.WriteEndElement(); // meta

					writer.WriteStartElement("style");
					writer.WriteAttributeString("type", "text/css");

					writer.WriteString("body {");
					writer.WriteString("background-color: #ffffff;");
					writer.WriteString("margin-top: 0;");
					writer.WriteString("margin-left: 5px;");
					writer.WriteString("margin-right: 5px;");
					writer.WriteString("margin-bottom: 0; }");

					writer.WriteString(Environment.NewLine);
					writer.WriteString(_textStyleSource.GetStyle("Sample"));
					writer.WriteString(_dataSource.NormalText.GetStyle("Normal"));

					writer.WriteEndElement(); // style
					writer.WriteEndElement(); // head

					writer.WriteStartElement("body");

					writer.WriteStartElement("p");
					writer.WriteRaw(_sampleText);
					writer.WriteEndElement(); // p

					writer.WriteEndElement(); // body
					writer.WriteEndElement(); // html

					writer.Flush();
					writer.Close();
				}
				string html = UnicodeEncoding.UTF8.GetString(stream.ToArray());
				Trace.WriteLine(html);
				webSample.DocumentText = html;
			}
		}

		private void PopulateColors()
		{
			cboColor.Items.Clear();
			cboHighlite.Items.Clear();

			cboColor.Items.Add("Custom");
			cboHighlite.Items.Add("Custom");

			string[] colors = Enum.GetNames(typeof(KnownColor));

			foreach (string color in colors)
			{
				cboColor.Items.Add(color);
				cboHighlite.Items.Add(color);
			}
		}

		private void PopulateFonts()
		{
			cboFont.Items.Clear();
			InstalledFontCollection fonts = new InstalledFontCollection();

			foreach (FontFamily font in fonts.Families)
			{
				cboFont.Items.Add(font.Name);
			}
		}

		private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			switch (e.Node.Name)
			{
				case "nodeVersion":
					DisplayPreferredVersion();
					break;
				case "nodeVersionHeader":
					DisplayTextStyle(_dataSource.VersionHeader, e.Node.Text, "<span class=\"Sample\">King James Version</span>");
					break;
				case "nodeBookHeader":
					DisplayTextStyle(_dataSource.BookHeader, e.Node.Text, "<span class=\"Sample\">Genesis</span>");
					break;
				case "nodeChapterHeader":
					DisplayTextStyle(_dataSource.ChapterHeader, e.Node.Text, "<span class=\"Sample\">Chapter 1</span>");
					break;
				case "nodeSectionHeader":
					DisplayTextStyle(_dataSource.VerseHeader, e.Node.Text, "<span class=\"Sample\">The Creation</span>");
					break;
				case "nodeVerseNumber":
					DisplayTextStyle(_dataSource.VerseNumber, e.Node.Text, "<span class=\"Sample\">1</span> <span class=\"Normal\">In the beginning God</span>");
					break;
				case "nodeNormalText":
					DisplayTextStyle(_dataSource.NormalText, e.Node.Text, "<span class=\"Sample\">In the beginning God</span>");
					break;
				case "nodeConcordance":
					DisplayTextStyle(_dataSource.ConcordanceText, e.Node.Text, "<span class=\"Normal\">In the</span> <span class=\"Sample\">beginning</span> <span class=\"Sample\">God</span> <span class=\"Sample\">created</span>");
					break;
				case "nodeNotInOriginal":
					DisplayTextStyle(_dataSource.NotInText, e.Node.Text, "<span class=\"Normal\">speaketh in an</span> <span class=\"Sample\">unknown</span> <span class=\"Normal\">tongue</span>");
					break;
				case "nodeWordsOfChrist":
					DisplayTextStyle(_dataSource.WordsOfChrist, e.Node.Text, "<span class=\"Sample\">Blessed are the poor in spirit</span>");
					break;
				case "nodeDictionaryHeader":
					DisplayTextStyle(_dataSource.DictionaryHeader, e.Node.Text, "<span class=\"Sample\">created</span>");
					break;
				case "nodeDictionaryExample":
					DisplayTextStyle(_dataSource.DictionaryExample, e.Node.Text, "<span class=\"Sample\">God created the heavens and the earth</span>");
					break;
				case "nodeDictionaryText":
					DisplayTextStyle(_dataSource.DictionaryText, e.Node.Text, "<span class=\"Sample\">to cause to come into being</span>");
					break;
				case "nodeHebrewText":
					DisplayTextStyle(_dataSource.HebrewText, e.Node.Text, "<span class=\"Sample\">hryb</span>");
					break;
				case "nodeGreekText":
					DisplayTextStyle(_dataSource.GreekText, e.Node.Text, "<span class=\"Sample\">ejpitavssw</span>");
					break;
				case "nodeFileLocations":
					DisplayFileLocations();
					break;
			}
		}

		private void DisplayFileLocations()
		{
			if (_textStyleSource != null) _textStyleSource.PropertyChanged -= _propertyChangedHandler;
			grpTextStyle.Visible = false;
			grpPreferredVersion.Visible = false;
			grpFileLocations.Visible = true;
		}

		private void DisplayTextStyle(TextStyle textStyle, string textStyleName, string sampleText)
		{
			if (_textStyleSource != null) _textStyleSource.PropertyChanged -= _propertyChangedHandler;
			grpFileLocations.Visible = false;
			grpPreferredVersion.Visible = false;
			grpTextStyle.Visible = true;
			grpTextStyle.Text = textStyleName + " Font and Colors";

			_sampleText = sampleText;
			_textColor = textStyle.TextColor;
			_highliteColor = textStyle.HighliteColor;
			_textStyleSource = textStyle;

			BindTextStyle();
		}

		private void BindTextStyle()
		{
			cboFont.Text = _textStyleSource.FontName;
			cboSize.Text = _textStyleSource.FontSize;
			chkVisible.Checked = _textStyleSource.Visible;
			chkBold.Checked = _textStyleSource.Bold;
			chkItalics.Checked = _textStyleSource.Italic;
			chkSuper.Checked = _textStyleSource.Superscript;
			chkUnderline.Checked = _textStyleSource.Underline;

			if (_textStyleSource.TextColor.IsNamedColor)
			{
				cboColor.Text = _textStyleSource.TextColor.Name;
			}
			else
			{
				cboColor.SelectedIndex = 0;
			}

			if (_textStyleSource.HighliteColor.IsNamedColor)
			{
				cboHighlite.Text = _textStyleSource.HighliteColor.Name;
			}
			else
			{
				cboHighlite.SelectedIndex = 0;
			}

			switch (_textStyleSource.TextAlign)
			{
				case "right":
					rbnRight.Checked = true;
					break;
				case "center":
					rbnRight.Checked = true;
					break;
				case "left":
				default:
					rbnLeft.Checked = true;
					break;
			}
			_textStyleSource.PropertyChanged += _propertyChangedHandler;
			UpdateSampleDisplay();
		}

		private void DisplayPreferredVersion()
		{
			if (_textStyleSource != null) _textStyleSource.PropertyChanged -= _propertyChangedHandler;
			grpFileLocations.Visible = false;
			grpTextStyle.Visible = false;
			grpPreferredVersion.Visible = true;
		}

		private void cboPreferredVersion_SelectedIndexChanged(object sender, EventArgs e)
		{
			int versionId = (int)cboPreferredVersion.SelectedValue;
			Version version = BibleStore.Versions.Find(item => item.VersionId == versionId);

			if (version != null)
			{
				txtVersionAbrev.Text = version.VersionName;
				txtVersionName.Text = version.LongName;
				txtVersionCopyright.Text = version.Copyright;
			}
		}

		private void btnNotes_Click(object sender, EventArgs e)
		{
			if (Directory.Exists(txtNotesPath.Text))
			{
				folderBrowserDialog.SelectedPath = txtNotesPath.Text;
			}
			else
			{
				folderBrowserDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			}

			folderBrowserDialog.Description = "Select Default Notes location";
			if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
			{
				txtNotesPath.Text = folderBrowserDialog.SelectedPath;
				_dataSource.NotesPath = folderBrowserDialog.SelectedPath;
			}
		}

		private void btnAudio_Click(object sender, EventArgs e)
		{
			if (Directory.Exists(txtAudioPath.Text))
			{
				folderBrowserDialog.SelectedPath = txtAudioPath.Text;
			}
			else
			{
				folderBrowserDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
			}

			folderBrowserDialog.Description = "Select Audio Files locaation";
			if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
			{
				txtAudioPath.Text = folderBrowserDialog.SelectedPath;
				_dataSource.AudioPath = folderBrowserDialog.SelectedPath;
			}
		}

		private void btnColor_Click(object sender, EventArgs e)
		{
			colorDialog.Color = _textStyleSource.TextColor.Value;
			if (colorDialog.ShowDialog(this) == DialogResult.OK)
			{
				_textStyleSource.TextColor.Value = colorDialog.Color;
				_textColor.Value = colorDialog.Color;
				cboColor.SelectedIndex = 0;
			}
		}

		private void btnHighlite_Click(object sender, EventArgs e)
		{
			colorDialog.Color = _textStyleSource.HighliteColor.Value;
			if (colorDialog.ShowDialog(this) == DialogResult.OK)
			{
				_textStyleSource.HighliteColor.Value = colorDialog.Color;
				_highliteColor.Value = colorDialog.Color;
				cboHighlite.SelectedIndex = 0;
			}
		}

		private void cboColor_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cboColor.SelectedIndex == 0)
			{
				_textStyleSource.TextColor = _textColor;
			}
			else
			{
				_textStyleSource.TextColor.Value = Color.FromName(cboColor.Text);
			}
		}

		private void cboHighlite_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cboHighlite.SelectedIndex == 0)
			{
				_textStyleSource.HighliteColor = _highliteColor;
			}
			else
			{
				_textStyleSource.HighliteColor.Value = Color.FromName(cboHighlite.Text);
			}
		}

		private void radio_CheckedChanged(object sender, EventArgs e)
		{
			if (rbnLeft.Checked)
			{
				_textStyleSource.TextAlign = "left";
			}
			else if (rbnCenter.Checked)
			{
				_textStyleSource.TextAlign = "center";
			}
			else
			{
				_textStyleSource.TextAlign = "right";
			}
		}

		private void OptionsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (_textStyleSource != null) _textStyleSource.PropertyChanged -= _propertyChangedHandler;
		}

		private void chkVisible_CheckedChanged(object sender, EventArgs e)
		{
			_textStyleSource.Visible = chkVisible.Checked;
		}

		private void cboFont_SelectedIndexChanged(object sender, EventArgs e)
		{
			_textStyleSource.FontName = cboFont.Text;
		}

		private void cboSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			_textStyleSource.FontSize = cboSize.Text;
		}

		private void chkBold_CheckedChanged(object sender, EventArgs e)
		{
			_textStyleSource.Bold = chkBold.Checked;
		}

		private void chkItalics_CheckedChanged(object sender, EventArgs e)
		{
			_textStyleSource.Italic = chkItalics.Checked;
		}

		private void chkUnderline_CheckedChanged(object sender, EventArgs e)
		{
			_textStyleSource.Underline = chkUnderline.Checked;
		}

		private void chkSuper_CheckedChanged(object sender, EventArgs e)
		{
			_textStyleSource.Superscript = chkSuper.Checked;
		}

		private void btnReset_Click(object sender, EventArgs e)
		{
			string msg = "Are you sure you want to reset all User Preferences to the default values?";
			string caption = "Reset User Preferences";

			if (MessageBox.Show(this, msg, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				_dataSource = UserPreferences.CreateNewPreferences();
			}
		}
	}
}
