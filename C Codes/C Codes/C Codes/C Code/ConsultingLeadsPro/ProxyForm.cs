using System;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
namespace ConsultingLeadsPro
{
	public class ProxyForm : Form
	{
		private IContainer components;
		private GroupBox groupProxy;
		private Label lblCountry;
		private Label lblType;
		private Label lblPort;
		private Label lblIP;
		private Label lblPassword;
		private Label lblUsername;
		private CheckBox chkNeedAuth;
		private TextBox txtPassword;
		private TextBox txtUsername;
		private TextBox txtCountry;
		private TextBox txtPort;
		private TextBox txtIP;
		private ComboBox cmbType;
		private Button btnOK;
		private Button btnCancel;
		protected ProxyItem item;
		protected Color oldColor = Color.Empty;
		public ProxyItem Item
		{
			get
			{
				return this.item;
			}
		}
		public ProxyForm(ProxyItem item)
		{
			this.InitializeComponent();
			if (item == null)
			{
				this.Text = "Add account";
				this.item = new ProxyItem();
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
			this.groupProxy = new GroupBox();
			this.cmbType = new ComboBox();
			this.txtPassword = new TextBox();
			this.txtUsername = new TextBox();
			this.txtCountry = new TextBox();
			this.txtPort = new TextBox();
			this.txtIP = new TextBox();
			this.lblPassword = new Label();
			this.lblUsername = new Label();
			this.chkNeedAuth = new CheckBox();
			this.lblCountry = new Label();
			this.lblType = new Label();
			this.lblPort = new Label();
			this.lblIP = new Label();
			this.btnOK = new Button();
			this.btnCancel = new Button();
			this.groupProxy.SuspendLayout();
			base.SuspendLayout();
			this.groupProxy.Controls.Add(this.cmbType);
			this.groupProxy.Controls.Add(this.txtPassword);
			this.groupProxy.Controls.Add(this.txtUsername);
			this.groupProxy.Controls.Add(this.txtCountry);
			this.groupProxy.Controls.Add(this.txtPort);
			this.groupProxy.Controls.Add(this.txtIP);
			this.groupProxy.Controls.Add(this.lblPassword);
			this.groupProxy.Controls.Add(this.lblUsername);
			this.groupProxy.Controls.Add(this.chkNeedAuth);
			this.groupProxy.Controls.Add(this.lblCountry);
			this.groupProxy.Controls.Add(this.lblType);
			this.groupProxy.Controls.Add(this.lblPort);
			this.groupProxy.Controls.Add(this.lblIP);
			this.groupProxy.Location = new Point(12, 12);
			this.groupProxy.Name = "groupProxy";
			this.groupProxy.Size = new Size(322, 196);
			this.groupProxy.TabIndex = 0;
			this.groupProxy.TabStop = false;
			this.groupProxy.Text = "Proxy settings";
			this.cmbType.FormattingEnabled = true;
			this.cmbType.Items.AddRange(new object[]
			{
				"Anonymous", 
				"Transparent", 
				"Distorting", 
				"CoDeeN"
			});
			this.cmbType.Location = new Point(71, 52);
			this.cmbType.Name = "cmbType";
			this.cmbType.Size = new Size(236, 21);
			this.cmbType.TabIndex = 13;
			this.txtPassword.Location = new Point(101, 163);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.Size = new Size(121, 20);
			this.txtPassword.TabIndex = 12;
			this.txtPassword.UseSystemPasswordChar = true;
			this.txtUsername.Location = new Point(101, 137);
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new Size(121, 20);
			this.txtUsername.TabIndex = 11;
			this.txtCountry.Location = new Point(71, 79);
			this.txtCountry.Name = "txtCountry";
			this.txtCountry.Size = new Size(236, 20);
			this.txtCountry.TabIndex = 10;
			this.txtPort.Location = new Point(247, 26);
			this.txtPort.Name = "txtPort";
			this.txtPort.Size = new Size(60, 20);
			this.txtPort.TabIndex = 8;
			this.txtPort.TextChanged += new EventHandler(this.txtPort_TextChanged);
			this.txtIP.Location = new Point(71, 26);
			this.txtIP.Name = "txtIP";
			this.txtIP.Size = new Size(135, 20);
			this.txtIP.TabIndex = 7;
			this.txtIP.TextChanged += new EventHandler(this.txtIP_TextChanged);
			this.lblPassword.AutoSize = true;
			this.lblPassword.Location = new Point(39, 166);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new Size(56, 13);
			this.lblPassword.TabIndex = 6;
			this.lblPassword.Text = "Password:";
			this.lblUsername.AutoSize = true;
			this.lblUsername.Location = new Point(37, 140);
			this.lblUsername.Name = "lblUsername";
			this.lblUsername.Size = new Size(58, 13);
			this.lblUsername.TabIndex = 5;
			this.lblUsername.Text = "Username:";
			this.chkNeedAuth.AutoSize = true;
			this.chkNeedAuth.Location = new Point(23, 114);
			this.chkNeedAuth.Name = "chkNeedAuth";
			this.chkNeedAuth.Size = new Size(122, 17);
			this.chkNeedAuth.TabIndex = 4;
			this.chkNeedAuth.Text = "Need authentication";
			this.chkNeedAuth.UseVisualStyleBackColor = true;
			this.chkNeedAuth.CheckedChanged += new EventHandler(this.chkNeedAuth_CheckedChanged);
			this.lblCountry.AutoSize = true;
			this.lblCountry.Location = new Point(6, 82);
			this.lblCountry.Name = "lblCountry";
			this.lblCountry.Size = new Size(46, 13);
			this.lblCountry.TabIndex = 3;
			this.lblCountry.Text = "Country:";
			this.lblType.AutoSize = true;
			this.lblType.Location = new Point(6, 55);
			this.lblType.Name = "lblType";
			this.lblType.Size = new Size(34, 13);
			this.lblType.TabIndex = 2;
			this.lblType.Text = "Type:";
			this.lblPort.AutoSize = true;
			this.lblPort.Location = new Point(212, 29);
			this.lblPort.Name = "lblPort";
			this.lblPort.Size = new Size(29, 13);
			this.lblPort.TabIndex = 1;
			this.lblPort.Text = "Port:";
			this.lblIP.AutoSize = true;
			this.lblIP.Location = new Point(6, 29);
			this.lblIP.Name = "lblIP";
			this.lblIP.Size = new Size(20, 13);
			this.lblIP.TabIndex = 0;
			this.lblIP.Text = "IP:";
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new Point(178, 214);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new Size(75, 23);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new Point(258, 214);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new Size(75, 23);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			base.AcceptButton = this.btnOK;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.btnCancel;
			base.ClientSize = new Size(345, 243);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.groupProxy);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ProxyForm";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "ProxyForm";
			base.Load += new EventHandler(this.ProxyForm_Load);
			base.FormClosing += new FormClosingEventHandler(this.ProxyForm_FormClosing);
			this.groupProxy.ResumeLayout(false);
			this.groupProxy.PerformLayout();
			base.ResumeLayout(false);
		}
		private void ProxyForm_Load(object sender, EventArgs e)
		{
			this.cmbType.Text = this.item.Type;
			this.txtCountry.Text = this.item.Country;
			this.txtIP.Text = this.item.IP;
			this.txtPort.Text = ((this.item.Port > 0) ? this.item.Port.ToString() : string.Empty);
			this.chkNeedAuth.Checked = this.item.NeedAuth;
			this.txtUsername.Text = this.item.Username;
			this.txtPassword.Text = this.item.Password;
			this.chkNeedAuth_CheckedChanged(sender, e);
		}
		private void btnOK_Click(object sender, EventArgs e)
		{
			this.item.Type = this.item.Type;
			this.item.Country = this.txtCountry.Text;
			this.item.IP = this.txtIP.Text;
			this.item.NeedAuth = this.chkNeedAuth.Checked;
			this.item.Username = this.txtUsername.Text;
			this.item.Password = this.txtPassword.Text;
			int port;
			if (int.TryParse(this.txtPort.Text, out port))
			{
				this.item.Port = port;
				return;
			}
			this.item.Port = 80;
		}
		private void chkNeedAuth_CheckedChanged(object sender, EventArgs e)
		{
			this.lblUsername.Enabled = (this.lblPassword.Enabled = (this.txtUsername.Enabled = (this.txtPassword.Enabled = this.chkNeedAuth.Checked)));
		}
		private void ProxyForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (base.DialogResult == DialogResult.OK)
			{
				int num = 0;
				if (!int.TryParse(this.txtPort.Text, out num) || (num < 0 && num > 65535))
				{
					e.Cancel = true;
					this.txtPort.ForeColor = Color.Red;
					this.txtPort.Focus();
					return;
				}
				IPAddress iPAddress;
				if (!IPAddress.TryParse(this.txtIP.Text, out iPAddress))
				{
					e.Cancel = true;
					this.txtIP.ForeColor = Color.Red;
					this.txtIP.Focus();
					return;
				}
				e.Cancel = false;
			}
		}
		private void txtPort_TextChanged(object sender, EventArgs e)
		{
			this.txtPort.ForeColor = this.oldColor;
		}
		private void txtIP_TextChanged(object sender, EventArgs e)
		{
			this.txtIP.ForeColor = this.oldColor;
		}
	}
}
