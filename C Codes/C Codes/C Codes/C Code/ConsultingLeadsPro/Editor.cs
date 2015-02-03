using mshtml;
using ConsultingLeadsPro.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace ConsultingLeadsPro
{
	public class Editor : UserControl, SearchableBrowser
	{
		public class EnterKeyEventArgs : EventArgs
		{
			private bool _cancel;
			public bool Cancel
			{
				get
				{
					return this._cancel;
				}
				set
				{
					this._cancel = value;
				}
			}
		}
		public delegate void TickDelegate();
		private IHTMLDocument2 doc;
		private bool updatingFontName;
		private bool updatingFontSize;
		private bool setup;
		private IContainer components;
		private ToolStrip toolStrip1;
		private WebBrowser webBrowser1;
		private ToolStripButton boldButton;
		private ToolStripButton italicButton;
		private ToolStripComboBox fontComboBox;
		private ToolStripButton toolStripButton1;
		private ToolStripButton toolStripButton2;
		private ToolStripComboBox fontSizeComboBox;
		private ToolStripButton underlineButton;
		private ToolStripButton colorButton;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripSeparator toolStripSeparator2;
		private ToolStripButton linkButton;
		private ToolStripMenuItem copyToolStripMenuItem;
		private ToolStripMenuItem pasteToolStripMenuItem;
		private ToolStripMenuItem pasteToolStripMenuItem1;
		private ToolStripMenuItem cutToolStripMenuItem;
		private ToolStripMenuItem copyToolStripMenuItem1;
		private ToolStripMenuItem pasteToolStripMenuItem2;
		private ContextMenuStrip contextMenuStrip1;
		private ToolStripMenuItem cutToolStripMenuItem1;
		private ToolStripMenuItem copyToolStripMenuItem2;
		private ToolStripMenuItem pasteToolStripMenuItem3;
		private ToolStripButton imageButton;
		private ToolStripMenuItem deleteToolStripMenuItem;
		private Timer timer;
		private ToolStripSeparator toolStripSeparator3;
		private ToolStripButton outdentButton;
		private ToolStripButton indentButton;
		private ToolStripSeparator toolStripSeparator4;
		private ToolStripButton backColorButton;
		private ToolStripButton orderedListButton;
		private ToolStripButton unorderedListButton;
		private ToolStripSeparator toolStripSeparator5;
		private ToolStripButton justifyLeftButton;
		private ToolStripButton justifyCenterButton;
		private ToolStripButton justifyRightButton;
		private ToolStripButton justifyFullButton;
		public event Editor.TickDelegate Tick;
		public event WebBrowserNavigatedEventHandler Navigated;
		public event EventHandler<Editor.EnterKeyEventArgs> EnterKeyEvent;
		[Browsable(true)]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
				if (this.ReadyState == ReadyState.Complete)
				{
					this.SetBackgroundColor(value);
				}
			}
		}
		public HtmlDocument Document
		{
			get
			{
				return this.webBrowser1.Document;
			}
		}
		[Browsable(false)]
		public string DocumentText
		{
			get
			{
				return this.webBrowser1.DocumentText;
			}
			set
			{
				this.webBrowser1.DocumentText = value;
			}
		}
		[Browsable(false)]
		public string DocumentTitle
		{
			get
			{
				return this.webBrowser1.DocumentTitle;
			}
		}
		[Browsable(false)]
		public string BodyHtml
		{
			get
			{
				if (this.webBrowser1.Document != null && this.webBrowser1.Document.Body != null)
				{
					return this.webBrowser1.Document.Body.InnerHtml;
				}
				return string.Empty;
			}
			set
			{
				if (this.webBrowser1.Document.Body != null)
				{
					this.webBrowser1.Document.Body.InnerHtml = value;
				}
			}
		}
		[Browsable(false)]
		public string BodyText
		{
			get
			{
				if (this.webBrowser1.Document != null && this.webBrowser1.Document.Body != null)
				{
					return this.webBrowser1.Document.Body.InnerText;
				}
				return string.Empty;
			}
			set
			{
				if (this.webBrowser1.Document.Body != null)
				{
					this.webBrowser1.Document.Body.InnerText = value;
				}
			}
		}
		public ReadyState ReadyState
		{
			get
			{
				string a;
				if ((a = this.doc.readyState.ToLower()) != null)
				{
					if (a == "uninitialized")
					{
						return ReadyState.Uninitialized;
					}
					if (a == "loading")
					{
						return ReadyState.Loading;
					}
					if (a == "loaded")
					{
						return ReadyState.Loaded;
					}
					if (a == "interactive")
					{
						return ReadyState.Interactive;
					}
					if (a == "complete")
					{
						return ReadyState.Complete;
					}
				}
				return ReadyState.Uninitialized;
			}
		}
		public SelectionType SelectionType
		{
			get
			{
				string a;
				if ((a = this.doc.selection.type.ToLower()) != null)
				{
					if (a == "text")
					{
						return SelectionType.Text;
					}
					if (a == "control")
					{
						return SelectionType.Control;
					}
					if (a == "none")
					{
						return SelectionType.None;
					}
				}
				return SelectionType.None;
			}
		}
		[Browsable(false)]
		public FontSize FontSize
		{
			get
			{
				if (this.ReadyState != ReadyState.Complete)
				{
					return FontSize.NA;
				}
				string text;
				try
				{
					text = this.doc.queryCommandValue("FontSize").ToString();
				}
				catch
				{
					text = string.Empty;
				}
				string key;
				switch (key = text)
				{
					case "1":
					{
						return FontSize.One;
					}
					case "2":
					{
						return FontSize.Two;
					}
					case "3":
					{
						return FontSize.Three;
					}
					case "4":
					{
						return FontSize.Four;
					}
					case "5":
					{
						return FontSize.Five;
					}
					case "6":
					{
						return FontSize.Six;
					}
					case "7":
					{
						return FontSize.Seven;
					}
				}
				return FontSize.NA;
			}
			set
			{
				int num;
				switch (value)
				{
					case FontSize.One:
					{
						num = 1;
						break;
					}
					case FontSize.Two:
					{
						num = 2;
						break;
					}
					case FontSize.Three:
					{
						num = 3;
						break;
					}
					case FontSize.Four:
					{
						num = 4;
						break;
					}
					case FontSize.Five:
					{
						num = 5;
						break;
					}
					case FontSize.Six:
					{
						num = 6;
						break;
					}
					case FontSize.Seven:
					{
						num = 7;
						break;
					}
					default:
					{
						num = 7;
						break;
					}
				}
				this.webBrowser1.Document.ExecCommand("FontSize", false, num.ToString());
			}
		}
		[Browsable(false)]
		public FontFamily FontName
		{
			get
			{
				if (this.ReadyState != ReadyState.Complete)
				{
					return null;
				}
				string text = null;
				FontFamily result;
				try
				{
					text = (this.doc.queryCommandValue("FontName") as string);
				}
				catch
				{
					result = null;
					return result;
				}
				if (text == null)
				{
					return null;
				}
				return new FontFamily(text);
				return result;
			}
			set
			{
				if (value != null)
				{
					this.webBrowser1.Document.ExecCommand("FontName", false, value.Name);
				}
			}
		}
		[Browsable(false)]
		public Color EditorForeColor
		{
			get
			{
				if (this.ReadyState != ReadyState.Complete)
				{
					return Color.Black;
				}
				return Editor.ConvertToColor(this.doc.queryCommandValue("ForeColor").ToString());
			}
			set
			{
				string value2 = string.Format("#{0:X2}{1:X2}{2:X2}", value.R, value.G, value.B);
				this.webBrowser1.Document.ExecCommand("ForeColor", false, value2);
			}
		}
		[Browsable(false)]
		public Color EditorBackColor
		{
			get
			{
				if (this.ReadyState != ReadyState.Complete)
				{
					return Color.White;
				}
				return Editor.ConvertToColor(this.doc.queryCommandValue("BackColor").ToString());
			}
			set
			{
				string value2 = string.Format("#{0:X2}{1:X2}{2:X2}", value.R, value.G, value.B);
				this.webBrowser1.Document.ExecCommand("BackColor", false, value2);
			}
		}
		public Editor()
		{
			this.InitializeComponent();
			this.SetupEvents();
			this.SetupTimer();
			this.SetupBrowser();
			this.SetupFontComboBox();
			this.SetupFontSizeComboBox();
		}
		private void SetupEvents()
		{
			this.webBrowser1.Navigated += new WebBrowserNavigatedEventHandler(this.webBrowser1_Navigated);
			this.webBrowser1.GotFocus += new EventHandler(this.webBrowser1_GotFocus);
		}
		private void webBrowser1_GotFocus(object sender, EventArgs e)
		{
			this.SuperFocus();
		}
		private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
		{
			this.SetBackgroundColor(this.BackColor);
			if (this.Navigated != null)
			{
				this.Navigated(this, e);
			}
		}
		private void SetupTimer()
		{
			this.timer.Interval = 200;
			this.timer.Tick += new EventHandler(this.timer_Tick);
			this.timer.Start();
		}
		private void SetupBrowser()
		{
			this.webBrowser1.DocumentText = "<html><body></body></html>";
			this.doc = (this.webBrowser1.Document.DomDocument as IHTMLDocument2);
			this.doc.designMode = "On";
			this.webBrowser1.Document.ContextMenuShowing += new HtmlElementEventHandler(this.Document_ContextMenuShowing);
		}
		private void SuperFocus()
		{
			if (this.webBrowser1.Document != null && this.webBrowser1.Document.Body != null)
			{
				this.webBrowser1.Document.Body.Focus();
			}
		}
		private void SetBackgroundColor(Color value)
		{
			if (this.webBrowser1.Document != null && this.webBrowser1.Document.Body != null)
			{
				this.webBrowser1.Document.Body.Style = string.Format("background-color: {0}", value.Name);
			}
		}
		public void Clear()
		{
			if (this.webBrowser1.Document.Body != null)
			{
				this.webBrowser1.Document.Body.InnerHtml = "";
			}
		}
		public bool CanUndo()
		{
			return this.doc.queryCommandEnabled("Undo");
		}
		public bool CanRedo()
		{
			return this.doc.queryCommandEnabled("Redo");
		}
		public bool CanCut()
		{
			return this.doc.queryCommandEnabled("Cut");
		}
		public bool CanCopy()
		{
			return this.doc.queryCommandEnabled("Copy");
		}
		public bool CanPaste()
		{
			return this.doc.queryCommandEnabled("Paste");
		}
		public bool CanDelete()
		{
			return this.doc.queryCommandEnabled("Delete");
		}
		public bool IsJustifyLeft()
		{
			return this.doc.queryCommandState("JustifyLeft");
		}
		public bool IsJustifyRight()
		{
			return this.doc.queryCommandState("JustifyRight");
		}
		public bool IsJustifyCenter()
		{
			return this.doc.queryCommandState("JustifyCenter");
		}
		public bool IsJustifyFull()
		{
			return this.doc.queryCommandState("JustifyFull");
		}
		public bool IsBold()
		{
			return this.doc.queryCommandState("Bold");
		}
		public bool IsItalic()
		{
			return this.doc.queryCommandState("Italic");
		}
		public bool IsUnderline()
		{
			return this.doc.queryCommandState("Underline");
		}
		public bool IsOrderedList()
		{
			return this.doc.queryCommandState("InsertOrderedList");
		}
		public bool IsUnorderedList()
		{
			return this.doc.queryCommandState("InsertUnorderedList");
		}
		private void Document_ContextMenuShowing(object sender, HtmlElementEventArgs e)
		{
			e.ReturnValue = false;
			this.cutToolStripMenuItem1.Enabled = this.CanCut();
			this.copyToolStripMenuItem2.Enabled = this.CanCopy();
			this.pasteToolStripMenuItem3.Enabled = this.CanPaste();
			this.deleteToolStripMenuItem.Enabled = this.CanDelete();
			this.contextMenuStrip1.Show(this, e.ClientMousePosition);
		}
		private void SetupFontSizeComboBox()
		{
			for (int i = 1; i <= 7; i++)
			{
				this.fontSizeComboBox.Items.Add(i.ToString());
			}
			this.fontSizeComboBox.TextChanged += new EventHandler(this.fontSizeComboBox_TextChanged);
			this.fontSizeComboBox.KeyPress += new KeyPressEventHandler(this.fontSizeComboBox_KeyPress);
		}
		private void fontSizeComboBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (char.IsNumber(e.KeyChar))
			{
				e.Handled = true;
				if (e.KeyChar <= '7' && e.KeyChar > '0')
				{
					this.fontSizeComboBox.Text = e.KeyChar.ToString();
					return;
				}
			}
			else
			{
				if (!char.IsControl(e.KeyChar))
				{
					e.Handled = true;
				}
			}
		}
		private void fontSizeComboBox_TextChanged(object sender, EventArgs e)
		{
			if (this.updatingFontSize)
			{
				return;
			}
			string key;
			switch (key = this.fontSizeComboBox.Text.Trim())
			{
				case "1":
				{
					this.FontSize = FontSize.One;
					return;
				}
				case "2":
				{
					this.FontSize = FontSize.Two;
					return;
				}
				case "3":
				{
					this.FontSize = FontSize.Three;
					return;
				}
				case "4":
				{
					this.FontSize = FontSize.Four;
					return;
				}
				case "5":
				{
					this.FontSize = FontSize.Five;
					return;
				}
				case "6":
				{
					this.FontSize = FontSize.Six;
					return;
				}
				case "7":
				{
					this.FontSize = FontSize.Seven;
					return;
				}
			}
			this.FontSize = FontSize.Seven;
		}
		private void SetupFontComboBox()
		{
			AutoCompleteStringCollection autoCompleteStringCollection = new AutoCompleteStringCollection();
			FontFamily[] families = FontFamily.Families;
			for (int i = 0; i < families.Length; i++)
			{
				FontFamily fontFamily = families[i];
				this.fontComboBox.Items.Add(fontFamily.Name);
				autoCompleteStringCollection.Add(fontFamily.Name);
			}
			this.fontComboBox.Leave += new EventHandler(this.fontComboBox_TextChanged);
			this.fontComboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
			this.fontComboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.fontComboBox.AutoCompleteCustomSource = autoCompleteStringCollection;
		}
		private void fontComboBox_TextChanged(object sender, EventArgs e)
		{
			if (this.updatingFontName)
			{
				return;
			}
			FontFamily fontName;
			try
			{
				fontName = new FontFamily(this.fontComboBox.Text);
			}
			catch (Exception)
			{
				this.updatingFontName = true;
				this.fontComboBox.Text = this.FontName.GetName(0);
				this.updatingFontName = false;
				return;
			}
			this.FontName = fontName;
		}
		private void timer_Tick(object sender, EventArgs e)
		{
			if (this.ReadyState != ReadyState.Complete)
			{
				return;
			}
			this.SetupKeyListener();
			this.boldButton.Checked = this.IsBold();
			this.italicButton.Checked = this.IsItalic();
			this.underlineButton.Checked = this.IsUnderline();
			this.orderedListButton.Checked = this.IsOrderedList();
			this.unorderedListButton.Checked = this.IsUnorderedList();
			this.justifyLeftButton.Checked = this.IsJustifyLeft();
			this.justifyCenterButton.Checked = this.IsJustifyCenter();
			this.justifyRightButton.Checked = this.IsJustifyRight();
			this.justifyFullButton.Checked = this.IsJustifyFull();
			this.linkButton.Enabled = (this.SelectionType != SelectionType.None);
			this.UpdateFontComboBox();
			this.UpdateFontSizeComboBox();
			if (this.Tick != null)
			{
				this.Tick();
			}
		}
		private void UpdateFontSizeComboBox()
		{
			if (!this.fontSizeComboBox.Focused)
			{
				int value;
				switch (this.FontSize)
				{
					case FontSize.One:
					{
						value = 1;
						break;
					}
					case FontSize.Two:
					{
						value = 2;
						break;
					}
					case FontSize.Three:
					{
						value = 3;
						break;
					}
					case FontSize.Four:
					{
						value = 4;
						break;
					}
					case FontSize.Five:
					{
						value = 5;
						break;
					}
					case FontSize.Six:
					{
						value = 6;
						break;
					}
					case FontSize.Seven:
					{
						value = 7;
						break;
					}
					case FontSize.NA:
					{
						value = 0;
						break;
					}
					default:
					{
						value = 7;
						break;
					}
				}
				string text = Convert.ToString(value);
				if (text != this.fontSizeComboBox.Text)
				{
					this.updatingFontSize = true;
					this.fontSizeComboBox.Text = text;
					this.updatingFontSize = false;
				}
			}
		}
		private void UpdateFontComboBox()
		{
			if (!this.fontComboBox.Focused)
			{
				FontFamily fontName = this.FontName;
				if (fontName != null)
				{
					string name = fontName.Name;
					if (name != this.fontComboBox.Text)
					{
						this.updatingFontName = true;
						this.fontComboBox.Text = name;
						this.updatingFontName = false;
					}
				}
			}
		}
		private void SetupKeyListener()
		{
			if (!this.setup)
			{
				this.webBrowser1.Document.Body.KeyDown += new HtmlElementEventHandler(this.Body_KeyDown);
				this.setup = true;
			}
		}
		private void Body_KeyDown(object sender, HtmlElementEventArgs e)
		{
			if (e.KeyPressedCode == 13 && !e.ShiftKeyPressed)
			{
				bool flag = false;
				if (this.EnterKeyEvent != null)
				{
					Editor.EnterKeyEventArgs enterKeyEventArgs = new Editor.EnterKeyEventArgs();
					this.EnterKeyEvent(this, enterKeyEventArgs);
					flag = enterKeyEventArgs.Cancel;
				}
				e.ReturnValue = !flag;
			}
		}
		public void EmbedBr()
		{
			IHTMLTxtRange iHTMLTxtRange = this.doc.selection.createRange() as IHTMLTxtRange;
			iHTMLTxtRange.pasteHTML("<br/>");
			iHTMLTxtRange.collapse(false);
			iHTMLTxtRange.select();
		}
		private void SuperPaste()
		{
			if (Clipboard.ContainsText())
			{
				IHTMLTxtRange iHTMLTxtRange = this.doc.selection.createRange() as IHTMLTxtRange;
				iHTMLTxtRange.pasteHTML(Clipboard.GetText(TextDataFormat.Text));
				iHTMLTxtRange.collapse(false);
				iHTMLTxtRange.select();
			}
		}
		public void Print()
		{
			this.webBrowser1.Document.ExecCommand("Print", true, null);
		}
		public void InsertParagraph()
		{
			this.webBrowser1.Document.ExecCommand("InsertParagraph", false, null);
		}
		public void InsertBreak()
		{
			this.webBrowser1.Document.ExecCommand("InsertHorizontalRule", false, null);
		}
		public void SelectAll()
		{
			this.webBrowser1.Document.ExecCommand("SelectAll", false, null);
		}
		public void Undo()
		{
			this.webBrowser1.Document.ExecCommand("Undo", false, null);
		}
		public void Redo()
		{
			this.webBrowser1.Document.ExecCommand("Redo", false, null);
		}
		public void Cut()
		{
			this.webBrowser1.Document.ExecCommand("Cut", false, null);
		}
		public void Paste()
		{
			this.webBrowser1.Document.ExecCommand("Paste", false, null);
		}
		public void Copy()
		{
			this.webBrowser1.Document.ExecCommand("Copy", false, null);
		}
		public void OrderedList()
		{
			this.webBrowser1.Document.ExecCommand("InsertOrderedList", false, null);
		}
		public void UnorderedList()
		{
			this.webBrowser1.Document.ExecCommand("InsertUnorderedList", false, null);
		}
		public void JustifyLeft()
		{
			this.webBrowser1.Document.ExecCommand("JustifyLeft", false, null);
		}
		public void JustifyRight()
		{
			this.webBrowser1.Document.ExecCommand("JustifyRight", false, null);
		}
		public void JustifyCenter()
		{
			this.webBrowser1.Document.ExecCommand("JustifyCenter", false, null);
		}
		public void JustifyFull()
		{
			this.webBrowser1.Document.ExecCommand("JustifyFull", false, null);
		}
		public void Bold()
		{
			this.webBrowser1.Document.ExecCommand("Bold", false, null);
		}
		public void Italic()
		{
			this.webBrowser1.Document.ExecCommand("Italic", false, null);
		}
		public void Underline()
		{
			this.webBrowser1.Document.ExecCommand("Underline", false, null);
		}
		public void Delete()
		{
			this.webBrowser1.Document.ExecCommand("Delete", false, null);
		}
		public void InsertImage()
		{
			this.webBrowser1.Document.ExecCommand("InsertImage", true, null);
		}
		public void Indent()
		{
			this.webBrowser1.Document.ExecCommand("Indent", false, null);
		}
		public void Outdent()
		{
			this.webBrowser1.Document.ExecCommand("Outdent", false, null);
		}
		public void InsertLink(string url)
		{
			this.webBrowser1.Document.ExecCommand("CreateLink", false, url);
		}
		public void SelectForeColor()
		{
			Color editorForeColor = this.EditorForeColor;
			if (this.ShowColorDialog(ref editorForeColor))
			{
				this.EditorForeColor = editorForeColor;
			}
		}
		public void SelectBackColor()
		{
			Color editorBackColor = this.EditorBackColor;
			if (this.ShowColorDialog(ref editorBackColor))
			{
				this.EditorBackColor = editorBackColor;
			}
		}
		private static Color ConvertToColor(string clrs)
		{
			int red;
			int green;
			int blue;
			if (clrs.StartsWith("#"))
			{
				int num = Convert.ToInt32(clrs.Substring(1), 16);
				red = (num >> 16 & 255);
				green = (num >> 8 & 255);
				blue = (num & 255);
			}
			else
			{
				int num2 = Convert.ToInt32(clrs);
				red = (num2 & 255);
				green = (num2 >> 8 & 255);
				blue = (num2 >> 16 & 255);
			}
			return Color.FromArgb(red, green, blue);
		}
		private void cutToolStripButton_Click(object sender, EventArgs e)
		{
			this.Cut();
		}
		private void pasteToolStripButton_Click(object sender, EventArgs e)
		{
			this.Paste();
		}
		private void copyToolStripButton_Click(object sender, EventArgs e)
		{
			this.Copy();
		}
		private void boldButton_Click(object sender, EventArgs e)
		{
			this.Bold();
		}
		private void italicButton_Click(object sender, EventArgs e)
		{
			this.Italic();
		}
		private void underlineButton_Click(object sender, EventArgs e)
		{
			this.Underline();
		}
		private void colorButton_Click(object sender, EventArgs e)
		{
			this.SelectForeColor();
		}
		private void backColorButton_Click(object sender, EventArgs e)
		{
			this.SelectBackColor();
		}
		private bool ShowColorDialog(ref Color color)
		{
			bool result;
			using (ColorDialog colorDialog = new ColorDialog())
			{
				colorDialog.SolidColorOnly = true;
				colorDialog.AllowFullOpen = false;
				colorDialog.AnyColor = false;
				colorDialog.FullOpen = false;
				colorDialog.CustomColors = null;
				colorDialog.Color = color;
				if (colorDialog.ShowDialog(this) == DialogResult.OK)
				{
					result = true;
					color = colorDialog.Color;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}
		private void linkButton_Click(object sender, EventArgs e)
		{
			this.SelectLink();
		}
		public void SelectLink()
		{
			using (LinkDialog linkDialog = new LinkDialog())
			{
				linkDialog.ShowDialog(base.ParentForm);
				if (linkDialog.Accepted)
				{
					string uRI = linkDialog.URI;
					if (uRI == null || uRI.Length == 0)
					{
						MessageBox.Show(base.ParentForm, "Invalid URL");
					}
					else
					{
						this.InsertLink(linkDialog.URL);
					}
				}
			}
		}
		private void imageButton_Click(object sender, EventArgs e)
		{
			this.InsertImage();
		}
		private void outdentButton_Click(object sender, EventArgs e)
		{
			this.Outdent();
		}
		private void indentButton_Click(object sender, EventArgs e)
		{
			this.Indent();
		}
		private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			this.Cut();
		}
		private void copyToolStripMenuItem2_Click(object sender, EventArgs e)
		{
			this.Copy();
		}
		private void pasteToolStripMenuItem3_Click(object sender, EventArgs e)
		{
			this.Paste();
		}
		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Delete();
		}
		public bool Search(string text, bool forward, bool matchWholeWord, bool matchCase)
		{
			bool flag = false;
			if (this.webBrowser1.Document != null)
			{
				IHTMLDocument2 iHTMLDocument = this.webBrowser1.Document.DomDocument as IHTMLDocument2;
				IHTMLBodyElement iHTMLBodyElement = iHTMLDocument.body as IHTMLBodyElement;
				if (iHTMLBodyElement != null)
				{
					IHTMLTxtRange iHTMLTxtRange;
					if (iHTMLDocument.selection != null)
					{
						iHTMLTxtRange = (iHTMLDocument.selection.createRange() as IHTMLTxtRange);
						IHTMLTxtRange iHTMLTxtRange2 = iHTMLTxtRange.duplicate();
						iHTMLTxtRange2.collapse(true);
						if (iHTMLTxtRange.isEqual(iHTMLTxtRange2))
						{
							iHTMLTxtRange = iHTMLBodyElement.createTextRange();
						}
						else
						{
							if (forward)
							{
								iHTMLTxtRange.moveStart("character", 1);
							}
							else
							{
								iHTMLTxtRange.moveEnd("character", -1);
							}
						}
					}
					else
					{
						iHTMLTxtRange = iHTMLBodyElement.createTextRange();
					}
					int num = 0;
					if (matchWholeWord)
					{
						num += 2;
					}
					if (matchCase)
					{
						num += 4;
					}
					flag = iHTMLTxtRange.findText(text, forward ? 999999 : -999999, num);
					if (flag)
					{
						iHTMLTxtRange.select();
						iHTMLTxtRange.scrollIntoView(!forward);
					}
				}
			}
			return flag;
		}
		private void orderedListButton_Click(object sender, EventArgs e)
		{
			this.OrderedList();
		}
		private void unorderedListButton_Click(object sender, EventArgs e)
		{
			this.UnorderedList();
		}
		private void justifyLeftButton_Click(object sender, EventArgs e)
		{
			this.JustifyLeft();
		}
		private void justifyCenterButton_Click(object sender, EventArgs e)
		{
			this.JustifyCenter();
		}
		private void justifyRightButton_Click(object sender, EventArgs e)
		{
			this.JustifyRight();
		}
		private void justifyFullButton_Click(object sender, EventArgs e)
		{
			this.JustifyFull();
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}
		private void InitializeComponent()
		{
			this.components = new Container();
			this.toolStrip1 = new ToolStrip();
			this.fontComboBox = new ToolStripComboBox();
			this.fontSizeComboBox = new ToolStripComboBox();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.boldButton = new ToolStripButton();
			this.italicButton = new ToolStripButton();
			this.underlineButton = new ToolStripButton();
			this.toolStripSeparator4 = new ToolStripSeparator();
			this.colorButton = new ToolStripButton();
			this.backColorButton = new ToolStripButton();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.linkButton = new ToolStripButton();
			this.imageButton = new ToolStripButton();
			this.toolStripSeparator3 = new ToolStripSeparator();
			this.justifyLeftButton = new ToolStripButton();
			this.justifyCenterButton = new ToolStripButton();
			this.justifyRightButton = new ToolStripButton();
			this.justifyFullButton = new ToolStripButton();
			this.toolStripSeparator5 = new ToolStripSeparator();
			this.orderedListButton = new ToolStripButton();
			this.unorderedListButton = new ToolStripButton();
			this.outdentButton = new ToolStripButton();
			this.indentButton = new ToolStripButton();
			this.toolStripButton2 = new ToolStripButton();
			this.toolStripButton1 = new ToolStripButton();
			this.webBrowser1 = new WebBrowser();
			this.cutToolStripMenuItem = new ToolStripMenuItem();
			this.copyToolStripMenuItem1 = new ToolStripMenuItem();
			this.pasteToolStripMenuItem2 = new ToolStripMenuItem();
			this.copyToolStripMenuItem = new ToolStripMenuItem();
			this.pasteToolStripMenuItem = new ToolStripMenuItem();
			this.pasteToolStripMenuItem1 = new ToolStripMenuItem();
			this.contextMenuStrip1 = new ContextMenuStrip(this.components);
			this.cutToolStripMenuItem1 = new ToolStripMenuItem();
			this.copyToolStripMenuItem2 = new ToolStripMenuItem();
			this.pasteToolStripMenuItem3 = new ToolStripMenuItem();
			this.deleteToolStripMenuItem = new ToolStripMenuItem();
			this.timer = new Timer(this.components);
			this.toolStrip1.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.fontComboBox, 
				this.fontSizeComboBox, 
				this.toolStripSeparator1, 
				this.boldButton, 
				this.italicButton, 
				this.underlineButton, 
				this.toolStripSeparator4, 
				this.colorButton, 
				this.backColorButton, 
				this.toolStripSeparator2, 
				this.linkButton, 
				this.imageButton, 
				this.toolStripSeparator3, 
				this.justifyLeftButton, 
				this.justifyCenterButton, 
				this.justifyRightButton, 
				this.justifyFullButton, 
				this.toolStripSeparator5, 
				this.orderedListButton, 
				this.unorderedListButton, 
				this.outdentButton, 
				this.indentButton
			});
			this.toolStrip1.Location = new Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new Size(627, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			this.fontComboBox.Name = "fontComboBox";
			this.fontComboBox.Size = new Size(121, 25);
			this.fontComboBox.ToolTipText = "Font";
			this.fontSizeComboBox.Name = "fontSizeComboBox";
			this.fontSizeComboBox.Size = new Size(75, 25);
			this.fontSizeComboBox.ToolTipText = "Font Size";
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 25);
			this.boldButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.boldButton.Image = Resources.bold;
			this.boldButton.ImageTransparentColor = Color.Magenta;
			this.boldButton.Name = "boldButton";
			this.boldButton.Size = new Size(23, 22);
			this.boldButton.Text = "toolStripButton1";
			this.boldButton.ToolTipText = "Bold";
			this.boldButton.Click += new EventHandler(this.boldButton_Click);
			this.italicButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.italicButton.Image = Resources.italic;
			this.italicButton.ImageTransparentColor = Color.Magenta;
			this.italicButton.Name = "italicButton";
			this.italicButton.Size = new Size(23, 22);
			this.italicButton.Text = "toolStripButton2";
			this.italicButton.ToolTipText = "Italic";
			this.italicButton.Click += new EventHandler(this.italicButton_Click);
			this.underlineButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.underlineButton.Image = Resources.underscore;
			this.underlineButton.ImageTransparentColor = Color.Magenta;
			this.underlineButton.Name = "underlineButton";
			this.underlineButton.Size = new Size(23, 22);
			this.underlineButton.Text = "toolStripButton3";
			this.underlineButton.ToolTipText = "Underline";
			this.underlineButton.Click += new EventHandler(this.underlineButton_Click);
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new Size(6, 25);
			this.colorButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.colorButton.Image = Resources.color;
			this.colorButton.ImageTransparentColor = Color.Magenta;
			this.colorButton.Name = "colorButton";
			this.colorButton.Size = new Size(23, 22);
			this.colorButton.Text = "toolStripButton3";
			this.colorButton.ToolTipText = "Font Color";
			this.colorButton.Click += new EventHandler(this.colorButton_Click);
			this.backColorButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.backColorButton.Image = Resources.backcolor;
			this.backColorButton.ImageTransparentColor = Color.Magenta;
			this.backColorButton.Name = "backColorButton";
			this.backColorButton.Size = new Size(23, 22);
			this.backColorButton.Text = "toolStripButton3";
			this.backColorButton.ToolTipText = "Back Color";
			this.backColorButton.Click += new EventHandler(this.backColorButton_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new Size(6, 25);
			this.linkButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.linkButton.Image = Resources.link;
			this.linkButton.ImageTransparentColor = Color.Magenta;
			this.linkButton.Name = "linkButton";
			this.linkButton.Size = new Size(23, 22);
			this.linkButton.Text = "toolStripButton3";
			this.linkButton.ToolTipText = "Hyperlink";
			this.linkButton.Click += new EventHandler(this.linkButton_Click);
			this.imageButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.imageButton.Image = Resources.image;
			this.imageButton.ImageTransparentColor = Color.Magenta;
			this.imageButton.Name = "imageButton";
			this.imageButton.Size = new Size(23, 22);
			this.imageButton.Text = "toolStripButton3";
			this.imageButton.ToolTipText = "Image";
			this.imageButton.Click += new EventHandler(this.imageButton_Click);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new Size(6, 25);
			this.justifyLeftButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.justifyLeftButton.Image = Resources.lj;
			this.justifyLeftButton.ImageTransparentColor = Color.Magenta;
			this.justifyLeftButton.Name = "justifyLeftButton";
			this.justifyLeftButton.Size = new Size(23, 22);
			this.justifyLeftButton.Text = "toolStripButton3";
			this.justifyLeftButton.ToolTipText = "Justify Left";
			this.justifyLeftButton.Click += new EventHandler(this.justifyLeftButton_Click);
			this.justifyCenterButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.justifyCenterButton.Image = Resources.cj;
			this.justifyCenterButton.ImageTransparentColor = Color.Magenta;
			this.justifyCenterButton.Name = "justifyCenterButton";
			this.justifyCenterButton.Size = new Size(23, 22);
			this.justifyCenterButton.Text = "toolStripButton4";
			this.justifyCenterButton.ToolTipText = "Justify Center";
			this.justifyCenterButton.Click += new EventHandler(this.justifyCenterButton_Click);
			this.justifyRightButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.justifyRightButton.Image = Resources.rj;
			this.justifyRightButton.ImageTransparentColor = Color.Magenta;
			this.justifyRightButton.Name = "justifyRightButton";
			this.justifyRightButton.Size = new Size(23, 22);
			this.justifyRightButton.Text = "toolStripButton5";
			this.justifyRightButton.ToolTipText = "Justify Right";
			this.justifyRightButton.Click += new EventHandler(this.justifyRightButton_Click);
			this.justifyFullButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.justifyFullButton.Image = Resources.fj;
			this.justifyFullButton.ImageTransparentColor = Color.Magenta;
			this.justifyFullButton.Name = "justifyFullButton";
			this.justifyFullButton.Size = new Size(23, 22);
			this.justifyFullButton.Text = "toolStripButton6";
			this.justifyFullButton.ToolTipText = "Justify Full";
			this.justifyFullButton.Click += new EventHandler(this.justifyFullButton_Click);
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new Size(6, 25);
			this.orderedListButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.orderedListButton.Image = Resources.ol;
			this.orderedListButton.ImageTransparentColor = Color.Magenta;
			this.orderedListButton.Name = "orderedListButton";
			this.orderedListButton.Size = new Size(23, 22);
			this.orderedListButton.Text = "toolStripButton3";
			this.orderedListButton.ToolTipText = "Ordered List";
			this.orderedListButton.Click += new EventHandler(this.orderedListButton_Click);
			this.unorderedListButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.unorderedListButton.Image = Resources.uol;
			this.unorderedListButton.ImageTransparentColor = Color.Magenta;
			this.unorderedListButton.Name = "unorderedListButton";
			this.unorderedListButton.Size = new Size(23, 22);
			this.unorderedListButton.Text = "toolStripButton4";
			this.unorderedListButton.ToolTipText = "Unordered List";
			this.unorderedListButton.Click += new EventHandler(this.unorderedListButton_Click);
			this.outdentButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.outdentButton.Image = Resources.outdent;
			this.outdentButton.ImageTransparentColor = Color.Magenta;
			this.outdentButton.Name = "outdentButton";
			this.outdentButton.Size = new Size(23, 22);
			this.outdentButton.Text = "toolStripButton3";
			this.outdentButton.ToolTipText = "Outdent";
			this.outdentButton.Click += new EventHandler(this.outdentButton_Click);
			this.indentButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.indentButton.Image = Resources.indent;
			this.indentButton.ImageTransparentColor = Color.Magenta;
			this.indentButton.Name = "indentButton";
			this.indentButton.Size = new Size(23, 22);
			this.indentButton.Text = "toolStripButton4";
			this.indentButton.ToolTipText = "Indent";
			this.indentButton.Click += new EventHandler(this.indentButton_Click);
			this.toolStripButton2.Name = "toolStripButton2";
			this.toolStripButton2.Size = new Size(23, 23);
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new Size(23, 23);
			this.webBrowser1.Dock = DockStyle.Fill;
			this.webBrowser1.Location = new Point(0, 25);
			this.webBrowser1.MinimumSize = new Size(20, 20);
			this.webBrowser1.Name = "webBrowser1";
			this.webBrowser1.Size = new Size(627, 125);
			this.webBrowser1.TabIndex = 2;
			this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
			this.cutToolStripMenuItem.Size = new Size(32, 19);
			this.copyToolStripMenuItem1.Name = "copyToolStripMenuItem1";
			this.copyToolStripMenuItem1.Size = new Size(32, 19);
			this.pasteToolStripMenuItem2.Name = "pasteToolStripMenuItem2";
			this.pasteToolStripMenuItem2.Size = new Size(32, 19);
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Size = new Size(32, 19);
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.Size = new Size(32, 19);
			this.pasteToolStripMenuItem1.Name = "pasteToolStripMenuItem1";
			this.pasteToolStripMenuItem1.Size = new Size(32, 19);
			this.pasteToolStripMenuItem1.Text = "Paste";
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.cutToolStripMenuItem1, 
				this.copyToolStripMenuItem2, 
				this.pasteToolStripMenuItem3, 
				this.deleteToolStripMenuItem
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new Size(108, 92);
			this.cutToolStripMenuItem1.Name = "cutToolStripMenuItem1";
			this.cutToolStripMenuItem1.Size = new Size(107, 22);
			this.cutToolStripMenuItem1.Text = "Cut";
			this.cutToolStripMenuItem1.Click += new EventHandler(this.cutToolStripMenuItem1_Click);
			this.copyToolStripMenuItem2.Name = "copyToolStripMenuItem2";
			this.copyToolStripMenuItem2.Size = new Size(107, 22);
			this.copyToolStripMenuItem2.Text = "Copy";
			this.copyToolStripMenuItem2.Click += new EventHandler(this.copyToolStripMenuItem2_Click);
			this.pasteToolStripMenuItem3.Name = "pasteToolStripMenuItem3";
			this.pasteToolStripMenuItem3.Size = new Size(107, 22);
			this.pasteToolStripMenuItem3.Text = "Paste";
			this.pasteToolStripMenuItem3.Click += new EventHandler(this.pasteToolStripMenuItem3_Click);
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.Size = new Size(107, 22);
			this.deleteToolStripMenuItem.Text = "Delete";
			this.deleteToolStripMenuItem.Click += new EventHandler(this.deleteToolStripMenuItem_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.webBrowser1);
			base.Controls.Add(this.toolStrip1);
			base.Name = "Editor";
			base.Size = new Size(627, 150);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.contextMenuStrip1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
