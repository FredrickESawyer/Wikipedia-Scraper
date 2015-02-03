using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace ConsultingLeadsPro
{
	public class EmailForm : Form
	{
		private IContainer components;
		private Button btnOk;
		private Button btnCancel;
		private GroupBox gbEmailConnection;
		private Label lblPassword;
		private Label lblAccount;
		private Label lblPort;
		private Label lblSmtpServer;
		private Label lblSenderName;
		private Label lblEmailAddress;
		private TextBox txtPassword;
		private TextBox txtAccount;
		private CheckBox chkNeedAuth;
		private TextBox txtPort;
		private TextBox txtSmtpServer;
		private TextBox txtSenderName;
		private TextBox txtEmailAddress;
		private CheckBox chkUseSsl;
		protected SmtpItem item;
		protected Color oldColor = Color.Empty;
		public SmtpItem Item
		{
			get
			{
				return this.item;
			}
		}
		public EmailForm(SmtpItem item)
		{
			this.InitializeComponent();
			if (item == null)
			{
				this.Text = "Add account";
				this.item = new SmtpItem();
				return;
			}
			this.Text = "Edit account";
			this.item = item;
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
			this.btnOk = new Button();
			this.btnCancel = new Button();
			this.gbEmailConnection = new GroupBox();
			this.chkUseSsl = new CheckBox();
			this.lblPassword = new Label();
			this.lblAccount = new Label();
			this.lblPort = new Label();
			this.lblSmtpServer = new Label();
			this.lblSenderName = new Label();
			this.lblEmailAddress = new Label();
			this.txtPassword = new TextBox();
			this.txtAccount = new TextBox();
			this.chkNeedAuth = new CheckBox();
			this.txtPort = new TextBox();
			this.txtSmtpServer = new TextBox();
			this.txtSenderName = new TextBox();
			this.txtEmailAddress = new TextBox();
			this.gbEmailConnection.SuspendLayout();
			base.SuspendLayout();
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new Point(251, 244);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new Size(75, 23);
			this.btnOk.TabIndex = 7;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new EventHandler(this.btnOk_Click);
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new Point(332, 244);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new Size(75, 23);
			this.btnCancel.TabIndex = 8;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.gbEmailConnection.Controls.Add(this.chkUseSsl);
			this.gbEmailConnection.Controls.Add(this.lblPassword);
			this.gbEmailConnection.Controls.Add(this.lblAccount);
			this.gbEmailConnection.Controls.Add(this.lblPort);
			this.gbEmailConnection.Controls.Add(this.lblSmtpServer);
			this.gbEmailConnection.Controls.Add(this.lblSenderName);
			this.gbEmailConnection.Controls.Add(this.lblEmailAddress);
			this.gbEmailConnection.Controls.Add(this.txtPassword);
			this.gbEmailConnection.Controls.Add(this.txtAccount);
			this.gbEmailConnection.Controls.Add(this.chkNeedAuth);
			this.gbEmailConnection.Controls.Add(this.txtPort);
			this.gbEmailConnection.Controls.Add(this.txtSmtpServer);
			this.gbEmailConnection.Controls.Add(this.txtSenderName);
			this.gbEmailConnection.Controls.Add(this.txtEmailAddress);
			this.gbEmailConnection.Location = new Point(12, 12);
			this.gbEmailConnection.Name = "gbEmailConnection";
			this.gbEmailConnection.Size = new Size(395, 226);
			this.gbEmailConnection.TabIndex = 9;
			this.gbEmailConnection.TabStop = false;
			this.gbEmailConnection.Text = "Email connection";
			this.chkUseSsl.AutoSize = true;
			this.chkUseSsl.Location = new Point(25, 194);
			this.chkUseSsl.Name = "chkUseSsl";
			this.chkUseSsl.Size = new Size(68, 17);
			this.chkUseSsl.TabIndex = 28;
			this.chkUseSsl.Text = "Use SSL";
			this.chkUseSsl.UseVisualStyleBackColor = true;
			this.lblPassword.AutoSize = true;
			this.lblPassword.Location = new Point(22, 167);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new Size(56, 13);
			this.lblPassword.TabIndex = 27;
			this.lblPassword.Text = "Password:";
			this.lblAccount.AutoSize = true;
			this.lblAccount.Location = new Point(22, 141);
			this.lblAccount.Name = "lblAccount";
			this.lblAccount.Size = new Size(50, 13);
			this.lblAccount.TabIndex = 26;
			this.lblAccount.Text = "Account:";
			this.lblPort.AutoSize = true;
			this.lblPort.Location = new Point(251, 83);
			this.lblPort.Name = "lblPort";
			this.lblPort.Size = new Size(29, 13);
			this.lblPort.TabIndex = 25;
			this.lblPort.Text = "Port:";
			this.lblSmtpServer.AutoSize = true;
			this.lblSmtpServer.Location = new Point(6, 83);
			this.lblSmtpServer.Name = "lblSmtpServer";
			this.lblSmtpServer.Size = new Size(72, 13);
			this.lblSmtpServer.TabIndex = 24;
			this.lblSmtpServer.Text = "SMTP server:";
			this.lblSenderName.AutoSize = true;
			this.lblSenderName.Location = new Point(6, 57);
			this.lblSenderName.Name = "lblSenderName";
			this.lblSenderName.Size = new Size(73, 13);
			this.lblSenderName.TabIndex = 23;
			this.lblSenderName.Text = "Sender name:";
			this.lblEmailAddress.AutoSize = true;
			this.lblEmailAddress.Location = new Point(6, 31);
			this.lblEmailAddress.Name = "lblEmailAddress";
			this.lblEmailAddress.Size = new Size(75, 13);
			this.lblEmailAddress.TabIndex = 22;
			this.lblEmailAddress.Text = "Email address:";
			this.txtPassword.Location = new Point(87, 164);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.Size = new Size(152, 20);
			this.txtPassword.TabIndex = 21;
			this.txtPassword.UseSystemPasswordChar = true;
			this.txtAccount.Location = new Point(87, 138);
			this.txtAccount.Name = "txtAccount";
			this.txtAccount.Size = new Size(152, 20);
			this.txtAccount.TabIndex = 20;
			this.chkNeedAuth.AutoSize = true;
			this.chkNeedAuth.Location = new Point(9, 115);
			this.chkNeedAuth.Name = "chkNeedAuth";
			this.chkNeedAuth.Size = new Size(122, 17);
			this.chkNeedAuth.TabIndex = 19;
			this.chkNeedAuth.Text = "Need authentication";
			this.chkNeedAuth.UseVisualStyleBackColor = true;
			this.chkNeedAuth.CheckedChanged += new EventHandler(this.chkNeedAuth_CheckedChanged);
			this.txtPort.Location = new Point(286, 80);
			this.txtPort.Name = "txtPort";
			this.txtPort.Size = new Size(100, 20);
			this.txtPort.TabIndex = 18;
			this.txtPort.TextChanged += new EventHandler(this.txtPort_TextChanged);
			this.txtSmtpServer.Location = new Point(87, 80);
			this.txtSmtpServer.Name = "txtSmtpServer";
			this.txtSmtpServer.Size = new Size(152, 20);
			this.txtSmtpServer.TabIndex = 17;
			this.txtSenderName.Location = new Point(87, 54);
			this.txtSenderName.Name = "txtSenderName";
			this.txtSenderName.Size = new Size(152, 20);
			this.txtSenderName.TabIndex = 16;
			this.txtEmailAddress.Location = new Point(87, 28);
			this.txtEmailAddress.Name = "txtEmailAddress";
			this.txtEmailAddress.Size = new Size(152, 20);
			this.txtEmailAddress.TabIndex = 15;
			base.AcceptButton = this.btnOk;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.btnCancel;
			base.ClientSize = new Size(419, 271);
			base.Controls.Add(this.gbEmailConnection);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOk);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "EmailForm";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "EmailForm";
			base.Load += new EventHandler(this.EmailForm_Load);
			base.FormClosing += new FormClosingEventHandler(this.EmailForm_FormClosing);
			this.gbEmailConnection.ResumeLayout(false);
			this.gbEmailConnection.PerformLayout();
			base.ResumeLayout(false);
		}
		private void EmailForm_Load(object sender, EventArgs e)
		{
			this.txtAccount.Text = this.item.Username;
			this.txtEmailAddress.Text = this.item.EmailAddress;
			this.txtPassword.Text = this.item.Password;
			this.txtPort.Text = ((this.item.Port > 0) ? this.item.Port.ToString() : string.Empty);
			this.txtSenderName.Text = this.item.SenderName;
			this.txtSmtpServer.Text = this.item.SmtpServer;
			this.oldColor = this.txtPort.ForeColor;
			this.lblAccount.Enabled = (this.lblPassword.Enabled = (this.txtAccount.Enabled = (this.txtPassword.Enabled = (this.chkUseSsl.Enabled = (this.chkNeedAuth.Checked = this.item.NeedsAuth)))));
			this.chkUseSsl.Checked = this.item.UseSsl;
		}
		private void btnOk_Click(object sender, EventArgs e)
		{
			this.item.Username = this.txtAccount.Text;
			this.item.EmailAddress = this.txtEmailAddress.Text;
			this.item.Password = this.txtPassword.Text;
			int port;
			if (int.TryParse(this.txtPort.Text, out port))
			{
				this.item.Port = port;
			}
			else
			{
				this.item.Port = 25;
			}
			this.item.SenderName = this.txtSenderName.Text;
			this.item.SmtpServer = this.txtSmtpServer.Text;
			this.item.UseSsl = this.chkUseSsl.Checked;
			this.item.NeedsAuth = this.chkNeedAuth.Checked;
		}
		private void EmailForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (base.DialogResult == DialogResult.OK)
			{
				int num;
				if (int.TryParse(this.txtPort.Text, out num) && num > 0 && num < 65536)
				{
					e.Cancel = false;
					return;
				}
				e.Cancel = true;
				this.txtPort.ForeColor = Color.Red;
				this.txtPort.Focus();
			}
		}
		private void txtPort_TextChanged(object sender, EventArgs e)
		{
			this.txtPort.ForeColor = this.oldColor;
		}
		private void chkNeedAuth_CheckedChanged(object sender, EventArgs e)
		{
			this.lblAccount.Enabled = (this.lblPassword.Enabled = (this.txtAccount.Enabled = (this.txtPassword.Enabled = (this.chkUseSsl.Enabled = this.chkNeedAuth.Checked))));
		}
	}
}
