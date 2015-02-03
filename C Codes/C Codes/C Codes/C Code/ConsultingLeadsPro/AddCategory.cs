using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace ConsultingLeadsPro
{
	public class AddCategory : Form
	{
		private IContainer components;
		private Button btnOk;
		private Button btnCancel;
		internal TextBox txtCategory;
		public AddCategory(string caption, string initialText)
		{
			this.InitializeComponent();
			this.Text = caption;
			this.txtCategory.Text = initialText;
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
			this.txtCategory = new TextBox();
			this.btnOk = new Button();
			this.btnCancel = new Button();
			base.SuspendLayout();
			this.txtCategory.Location = new Point(12, 12);
			this.txtCategory.Name = "txtCategory";
			this.txtCategory.Size = new Size(305, 20);
			this.txtCategory.TabIndex = 0;
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new Point(161, 38);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new Size(75, 23);
			this.btnOk.TabIndex = 1;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new Point(242, 38);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new Size(75, 23);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			base.AcceptButton = this.btnOk;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.btnCancel;
			base.ClientSize = new Size(327, 68);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOk);
			base.Controls.Add(this.txtCategory);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "AddCategory";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "AddCategory";
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
