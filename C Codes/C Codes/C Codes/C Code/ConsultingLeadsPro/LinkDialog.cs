using ConsultingLeadsPro.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace ConsultingLeadsPro
{
	public class LinkDialog : Form
	{
		private IContainer components;
		private Button cancelButton;
		private Button OKButton;
		private ComboBox comboBox1;
		private ComboBox linkEdit;
		private GroupBox groupBox1;
		private Label label1;
		private bool _accepted;
		public string URL
		{
			get
			{
				return this.comboBox1.Text + this.linkEdit.Text.Trim();
			}
		}
		public string URI
		{
			get
			{
				return this.linkEdit.Text.Trim();
			}
		}
		public bool Accepted
		{
			get
			{
				return this._accepted;
			}
		}
		public LinkDialog()
		{
			this.InitializeComponent();
			this.LoadUrls();
			this.linkEdit.TextChanged += new EventHandler(this.linkEdit_TextChanged);
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
			this.cancelButton = new Button();
			this.OKButton = new Button();
			this.comboBox1 = new ComboBox();
			this.linkEdit = new ComboBox();
			this.groupBox1 = new GroupBox();
			this.label1 = new Label();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new Point(254, 106);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new Size(75, 23);
			this.cancelButton.TabIndex = 2;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new EventHandler(this.cancelButton_Click);
			this.OKButton.Location = new Point(174, 106);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new Size(75, 23);
			this.OKButton.TabIndex = 1;
			this.OKButton.Text = "OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new EventHandler(this.OKButton_Click);
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Items.AddRange(new object[]
			{
				"http://", 
				"https://"
			});
			this.comboBox1.Location = new Point(19, 30);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new Size(67, 21);
			this.comboBox1.TabIndex = 3;
			this.comboBox1.Text = "http://";
			this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
			this.linkEdit.AutoCompleteMode = AutoCompleteMode.Suggest;
			this.linkEdit.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.linkEdit.FormattingEnabled = true;
			this.linkEdit.Location = new Point(92, 30);
			this.linkEdit.Name = "linkEdit";
			this.linkEdit.Size = new Size(231, 21);
			this.linkEdit.TabIndex = 4;
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new Point(13, 13);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(316, 87);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "URL";
			this.label1.AutoSize = true;
			this.label1.Location = new Point(6, 54);
			this.label1.Name = "label1";
			this.label1.Size = new Size(0, 13);
			this.label1.TabIndex = 0;
			base.AcceptButton = this.OKButton;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.cancelButton;
			base.ClientSize = new Size(341, 141);
			base.Controls.Add(this.linkEdit);
			base.Controls.Add(this.comboBox1);
			base.Controls.Add(this.OKButton);
			base.Controls.Add(this.cancelButton);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "LinkDialog";
			this.Text = "Create a Link";
			base.Load += new EventHandler(this.LinkDialog_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			base.ResumeLayout(false);
		}
		private void linkEdit_TextChanged(object sender, EventArgs e)
		{
			this.label1.Text = this.URL;
		}
		private void LinkDialog_Load(object sender, EventArgs e)
		{
			this.label1.Text = this.URL;
			base.BeginInvoke(new MethodInvoker(delegate
			{
				this.linkEdit.Focus();
			}
			));
		}
		private void LoadUrls()
		{
			string linkDialogURLs = Settings.Default.LinkDialogURLs;
			string[] array = linkDialogURLs.Split(null);
			if (array != null)
			{
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string item = array2[i];
					this.linkEdit.Items.Add(item);
				}
			}
		}
		private void OKButton_Click(object sender, EventArgs e)
		{
			string text = this.linkEdit.Text;
			string text2 = Settings.Default.LinkDialogURLs;
			if (text2 == null)
			{
				text2 = "";
			}
			if (!text2.Contains(text))
			{
				if (text2.Length > 0)
				{
					text2 += "\n";
				}
				text2 += text;
			}
			Settings.Default.LinkDialogURLs = text2;
			Settings.Default.Save();
			this._accepted = true;
			base.Close();
		}
		private void cancelButton_Click(object sender, EventArgs e)
		{
			this._accepted = false;
			base.Close();
		}
		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.label1.Text = this.URL;
		}
	}
}
