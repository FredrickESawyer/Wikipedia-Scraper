using ConsultingLeadsPro.Network;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace ConsultingLeadsPro
{
	public class ActivationForm : Form
	{
		private IContainer components;
		private GroupBox gbActivation;
		private Label lblPlease;
		private Label label2;
		private Label label1;
		private TextBox txtKey;
		private TextBox txtUsername;
		private Button btnOk;
		private Button btnCancel;
		public ActivationForm()
		{
			this.InitializeComponent();
		}
		public NetResult CheckActivation(string username, string serial)
		{
			return NetResult.Success;
		}
		private void ActivationForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (base.DialogResult == DialogResult.OK)
			{
				CommonData.Instance.Settings.UserName = this.txtUsername.Text;
				CommonData.Instance.Settings.Serial = this.txtKey.Text;
				switch (this.CheckActivation(this.txtUsername.Text, this.txtKey.Text))
				{
					case NetResult.Success:
					{
						CommonData.Instance.Settings.Activated = true;
						e.Cancel = false;
						return;
					}
					case NetResult.ServerDown:
					{
						MessageBox.Show("Cannot connect to the activation server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						e.Cancel = true;
						return;
					}
					case NetResult.Failed:
					{
						MessageBox.Show("The serial number is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						e.Cancel = true;
						break;
					}
					default:
					{
						return;
					}
				}
			}
		}
		private void ActivationForm_Load(object sender, EventArgs e)
		{
			this.txtUsername.Text = CommonData.Instance.Settings.UserName;
			this.txtKey.Text = CommonData.Instance.Settings.Serial;
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
			this.gbActivation = new GroupBox();
			this.lblPlease = new Label();
			this.label2 = new Label();
			this.label1 = new Label();
			this.txtKey = new TextBox();
			this.txtUsername = new TextBox();
			this.btnOk = new Button();
			this.btnCancel = new Button();
			this.gbActivation.SuspendLayout();
			base.SuspendLayout();
			this.gbActivation.Controls.Add(this.lblPlease);
			this.gbActivation.Controls.Add(this.label2);
			this.gbActivation.Controls.Add(this.label1);
			this.gbActivation.Controls.Add(this.txtKey);
			this.gbActivation.Controls.Add(this.txtUsername);
			this.gbActivation.Location = new Point(12, 12);
			this.gbActivation.Name = "gbActivation";
			this.gbActivation.Size = new Size(270, 111);
			this.gbActivation.TabIndex = 4;
			this.gbActivation.TabStop = false;
			this.gbActivation.Text = "Activation";
			this.lblPlease.AutoSize = true;
			this.lblPlease.Location = new Point(6, 25);
			this.lblPlease.Name = "lblPlease";
			this.lblPlease.Size = new Size(200, 13);
			this.lblPlease.TabIndex = 8;
			this.lblPlease.Text = "Please enter your registration information:";
			this.label2.AutoSize = true;
			this.label2.Location = new Point(6, 80);
			this.label2.Name = "label2";
			this.label2.Size = new Size(77, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "Activation key:";
			this.label1.AutoSize = true;
			this.label1.Location = new Point(6, 54);
			this.label1.Name = "label1";
			this.label1.Size = new Size(61, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "User name:";
			this.txtKey.Location = new Point(98, 77);
			this.txtKey.Name = "txtKey";
			this.txtKey.Size = new Size(158, 20);
			this.txtKey.TabIndex = 5;
			this.txtUsername.Location = new Point(98, 51);
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new Size(158, 20);
			this.txtUsername.TabIndex = 4;
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new Point(126, 129);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new Size(75, 23);
			this.btnOk.TabIndex = 6;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new Point(207, 129);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new Size(75, 23);
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			base.AcceptButton = this.btnOk;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.btnCancel;
			base.ClientSize = new Size(295, 163);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOk);
			base.Controls.Add(this.gbActivation);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ActivationForm";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Program activation";
			base.Load += new EventHandler(this.ActivationForm_Load);
			base.FormClosing += new FormClosingEventHandler(this.ActivationForm_FormClosing);
			this.gbActivation.ResumeLayout(false);
			this.gbActivation.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
