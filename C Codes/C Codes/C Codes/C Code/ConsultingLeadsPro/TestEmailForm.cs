using ConsultingLeadsPro.Callbacks;
using ConsultingLeadsPro.Network;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
namespace ConsultingLeadsPro
{
	public class TestEmailForm : Form
	{
		private IContainer components;
		private Label lblEmail;
		private TextBox txtEmail;
		private Button btnTest;
		private Button btnClose;
		private TextBox txtLog;
		private Thread sendingThread;
		private List<SmtpItem> smtpItems;
		private SmtpTaskManager smtpTaskManager = new SmtpTaskManager();
		private TextBoxLogCallback tblc;
		public TestEmailForm(List<SmtpItem> smtpItems, string messageSubject, string messageBody)
		{
			this.InitializeComponent();
			this.tblc = new TextBoxLogCallback(this.txtLog);
			this.smtpTaskManager.Subject = messageSubject;
			this.smtpTaskManager.Message = messageBody;
			this.smtpItems = smtpItems;
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
			this.lblEmail = new Label();
			this.txtEmail = new TextBox();
			this.btnTest = new Button();
			this.btnClose = new Button();
			this.txtLog = new TextBox();
			base.SuspendLayout();
			this.lblEmail.AutoSize = true;
			this.lblEmail.Location = new Point(12, 15);
			this.lblEmail.Name = "lblEmail";
			this.lblEmail.Size = new Size(58, 13);
			this.lblEmail.TabIndex = 0;
			this.lblEmail.Text = "Test email:";
			this.txtEmail.Location = new Point(76, 12);
			this.txtEmail.Name = "txtEmail";
			this.txtEmail.Size = new Size(349, 20);
			this.txtEmail.TabIndex = 1;
			this.btnTest.Location = new Point(269, 163);
			this.btnTest.Name = "btnTest";
			this.btnTest.Size = new Size(75, 23);
			this.btnTest.TabIndex = 3;
			this.btnTest.Text = "Test";
			this.btnTest.UseVisualStyleBackColor = true;
			this.btnTest.Click += new EventHandler(this.btnTest_Click);
			this.btnClose.Location = new Point(350, 163);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new Size(75, 23);
			this.btnClose.TabIndex = 4;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new EventHandler(this.btnClose_Click);
			this.txtLog.Location = new Point(12, 38);
			this.txtLog.Multiline = true;
			this.txtLog.Name = "txtLog";
			this.txtLog.ReadOnly = true;
			this.txtLog.ScrollBars = ScrollBars.Both;
			this.txtLog.Size = new Size(413, 119);
			this.txtLog.TabIndex = 5;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new Size(437, 198);
			base.Controls.Add(this.txtLog);
			base.Controls.Add(this.btnClose);
			base.Controls.Add(this.btnTest);
			base.Controls.Add(this.txtEmail);
			base.Controls.Add(this.lblEmail);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "TestEmailForm";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Test SMTP settings";
			base.FormClosed += new FormClosedEventHandler(this.TestEmailForm_FormClosed);
			base.Load += new EventHandler(this.TestEmailForm_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
		private void DoSend()
		{
			this.tblc.Log("Start testing...");
			foreach (SmtpItem current in this.smtpItems)
			{
				this.tblc.Log(string.Format("Sending from {0}: ", current.EmailAddress), false);
				try
				{
					this.smtpTaskManager.Send(current, this.smtpTaskManager.GetMailMessage(current.EmailAddress, this.txtEmail.Text));
					this.tblc.Log(string.Format("Success", new object[0]));
				}
				catch (Exception arg_8A_0)
				{
					Exception exception = arg_8A_0;
					this.tblc.Log(string.Format("{0}", exception.Message));
				}
			}
			this.tblc.Log("Testing finished.");
		}
		private void btnTest_Click(object sender, EventArgs e)
		{
			if (this.sendingThread != null && this.sendingThread.IsAlive)
			{
				this.sendingThread.Abort();
			}
			this.sendingThread = new Thread(new ThreadStart(this.DoSend));
			this.txtLog.Clear();
			this.sendingThread.Start();
		}
		private void btnClose_Click(object sender, EventArgs e)
		{
			if (this.sendingThread != null && this.sendingThread.IsAlive)
			{
				this.sendingThread.Abort();
			}
			base.Close();
		}
		private void TestEmailForm_Load(object sender, EventArgs e)
		{
			this.txtEmail.Text = CommonData.Instance.Settings.TestEmail;
		}
		private void TestEmailForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			CommonData.Instance.Settings.TestEmail = this.txtEmail.Text;
		}
	}
}
