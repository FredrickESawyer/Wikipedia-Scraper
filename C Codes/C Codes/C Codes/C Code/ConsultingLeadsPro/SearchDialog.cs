using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace ConsultingLeadsPro
{
	public class SearchDialog : Form
	{
		private readonly SearchableBrowser _browser;
		private static string _last;
		private IContainer components;
		private CheckBox matchCase;
		private CheckBox matchWholeWord;
		private GroupBox groupBox1;
		private RadioButton downButton;
		private RadioButton upButton;
		private Button cancelButton;
		private Button findButton;
		private TextBox searchString;
		private Label label1;
		public SearchDialog(SearchableBrowser browser)
		{
			this._browser = browser;
			this.InitializeComponent();
			this.downButton.Checked = true;
			this.searchString.Text = SearchDialog._last;
			this.findButton.Enabled = (this.searchString.Text.Length > 0);
			base.Disposed += new EventHandler(this.SearchDialog_Disposed);
			this.searchString.TextChanged += new EventHandler(this.searchString_TextChanged);
		}
		private void searchString_TextChanged(object sender, EventArgs e)
		{
			this.findButton.Enabled = (this.searchString.Text.Length > 0);
		}
		private void SearchDialog_Disposed(object sender, EventArgs e)
		{
			SearchDialog._last = this.searchString.Text;
		}
		private void findButton_Click(object sender, EventArgs e)
		{
			if (!this._browser.Search(this.searchString.Text, this.downButton.Checked, this.matchWholeWord.Checked, this.matchCase.Checked))
			{
				MessageBox.Show(this, "Finished searching the document.", "Explorer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		private void cancelButton_Click(object sender, EventArgs e)
		{
			base.Close();
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
			this.matchCase = new CheckBox();
			this.matchWholeWord = new CheckBox();
			this.groupBox1 = new GroupBox();
			this.downButton = new RadioButton();
			this.upButton = new RadioButton();
			this.cancelButton = new Button();
			this.findButton = new Button();
			this.searchString = new TextBox();
			this.label1 = new Label();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.matchCase.AutoSize = true;
			this.matchCase.Location = new Point(14, 73);
			this.matchCase.Name = "matchCase";
			this.matchCase.Size = new Size(82, 17);
			this.matchCase.TabIndex = 13;
			this.matchCase.Text = "Match &case";
			this.matchCase.UseVisualStyleBackColor = true;
			this.matchWholeWord.AutoSize = true;
			this.matchWholeWord.Location = new Point(14, 47);
			this.matchWholeWord.Name = "matchWholeWord";
			this.matchWholeWord.Size = new Size(135, 17);
			this.matchWholeWord.TabIndex = 12;
			this.matchWholeWord.Text = "Match &whole word only";
			this.matchWholeWord.UseVisualStyleBackColor = true;
			this.groupBox1.Controls.Add(this.downButton);
			this.groupBox1.Controls.Add(this.upButton);
			this.groupBox1.Location = new Point(155, 43);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(112, 47);
			this.groupBox1.TabIndex = 11;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Direction";
			this.downButton.AutoSize = true;
			this.downButton.Location = new Point(51, 19);
			this.downButton.Name = "downButton";
			this.downButton.Size = new Size(53, 17);
			this.downButton.TabIndex = 1;
			this.downButton.TabStop = true;
			this.downButton.Text = "&Down";
			this.downButton.UseVisualStyleBackColor = true;
			this.upButton.AutoSize = true;
			this.upButton.Location = new Point(6, 19);
			this.upButton.Name = "upButton";
			this.upButton.Size = new Size(39, 17);
			this.upButton.TabIndex = 0;
			this.upButton.TabStop = true;
			this.upButton.Text = "&Up";
			this.upButton.UseVisualStyleBackColor = true;
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new Point(282, 43);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new Size(75, 23);
			this.cancelButton.TabIndex = 10;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new EventHandler(this.cancelButton_Click);
			this.findButton.Location = new Point(282, 13);
			this.findButton.Name = "findButton";
			this.findButton.Size = new Size(75, 23);
			this.findButton.TabIndex = 9;
			this.findButton.Text = "&Find Next";
			this.findButton.UseVisualStyleBackColor = true;
			this.findButton.Click += new EventHandler(this.findButton_Click);
			this.searchString.Location = new Point(77, 14);
			this.searchString.Name = "searchString";
			this.searchString.Size = new Size(190, 20);
			this.searchString.TabIndex = 8;
			this.label1.AutoSize = true;
			this.label1.Location = new Point(11, 14);
			this.label1.Name = "label1";
			this.label1.Size = new Size(59, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "Fi&nd What:";
			base.AcceptButton = this.findButton;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.cancelButton;
			base.ClientSize = new Size(368, 103);
			base.Controls.Add(this.matchCase);
			base.Controls.Add(this.matchWholeWord);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.cancelButton);
			base.Controls.Add(this.findButton);
			base.Controls.Add(this.searchString);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "SearchDialog";
			this.Text = "SearchDialog";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
