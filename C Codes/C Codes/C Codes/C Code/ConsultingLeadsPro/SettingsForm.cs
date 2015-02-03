using ConsultingLeadsPro.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
namespace ConsultingLeadsPro
{
	public class SettingsForm : Form
	{
		private IContainer components;
		private TabPage tabPage1;
		private TabPage tabPage2;
		private GroupBox groupBox1;
		private ListView listProxies;
		private GroupBox gbProxyConnection;
		private RadioButton rbProxyUse;
		private RadioButton rbProxyRandom;
		private RadioButton rbProxyDontUse;
		private Button btnSave;
		private Button btnCancel;
		private Panel panelAuth;
		private TextBox txtProxyPassword;
		private TextBox txtProxyUsername;
		private Label lblProxyPassword;
		private Label lblProxyUsername;
		private CheckBox chkProxyNeedAuth;
		private Label lblPort;
		private TextBox txtProxyHost;
		private Label lblHost;
		private TextBox txtProxyPort;
		private ColumnHeader columnHeader1;
		private ColumnHeader columnHeader2;
		private ColumnHeader columnHeader3;
		private ColumnHeader columnHeader4;
		private GroupBox gbAccounts;
		private ListView listEmails;
		private ColumnHeader columnHeader5;
		private ColumnHeader columnHeader6;
		private GroupBox gbFrequency;
		private Label label2;
		private Label label1;
		private NumericUpDown nudSendings;
		private Button btnDeleteEmail;
		private Button btnEditEmail;
		private Button btnAddEmail;
		private TabPage tabPage3;
		private Label lblBody;
		private TextBox txtSubject;
		private Label lblSubject;
		internal TabControl tabControl1;
		private Button btnDeleteProxy;
		private Button btnEditProxy;
		private Button btnAddProxy;
		private Button btnExportEmail;
		private Button btnImportEmail;
		private Button btnExportProxy;
		private Button btnImportProxy;
		private Label lblTip;
		private Editor editorBody;
		private TabPage tabPage4;
		private GroupBox dbDeepSearch;
		private RadioButton rbSearchAlways;
		private RadioButton rbSearchIfNoEmails;
		private RadioButton rbDoNotSearch;
		private Button btnTestSmtp;
		private GroupBox gbDuplicates;
		private Panel panelDuplicates;
		private RadioButton rbDupEmailHeadline;
		private RadioButton rbDupHeadline;
		private RadioButton rbDupEmail;
		private CheckBox chkDuplicates;
		protected Color oldColor = Color.Empty;
		public SettingsForm()
		{
			this.InitializeComponent();
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(SettingsForm));
			this.tabControl1 = new TabControl();
			this.tabPage1 = new TabPage();
			this.groupBox1 = new GroupBox();
			this.btnExportProxy = new Button();
			this.btnImportProxy = new Button();
			this.btnDeleteProxy = new Button();
			this.btnEditProxy = new Button();
			this.btnAddProxy = new Button();
			this.listProxies = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			this.gbProxyConnection = new GroupBox();
			this.panelAuth = new Panel();
			this.txtProxyPassword = new TextBox();
			this.txtProxyUsername = new TextBox();
			this.lblProxyPassword = new Label();
			this.lblProxyUsername = new Label();
			this.chkProxyNeedAuth = new CheckBox();
			this.txtProxyPort = new TextBox();
			this.lblPort = new Label();
			this.txtProxyHost = new TextBox();
			this.lblHost = new Label();
			this.rbProxyUse = new RadioButton();
			this.rbProxyRandom = new RadioButton();
			this.rbProxyDontUse = new RadioButton();
			this.tabPage2 = new TabPage();
			this.gbAccounts = new GroupBox();
			this.btnExportEmail = new Button();
			this.btnImportEmail = new Button();
			this.btnDeleteEmail = new Button();
			this.btnEditEmail = new Button();
			this.btnAddEmail = new Button();
			this.listEmails = new ListView();
			this.columnHeader5 = new ColumnHeader();
			this.columnHeader6 = new ColumnHeader();
			this.gbFrequency = new GroupBox();
			this.btnTestSmtp = new Button();
			this.label2 = new Label();
			this.label1 = new Label();
			this.nudSendings = new NumericUpDown();
			this.tabPage3 = new TabPage();
			this.editorBody = new Editor();
			this.lblTip = new Label();
			this.lblBody = new Label();
			this.txtSubject = new TextBox();
			this.lblSubject = new Label();
			this.tabPage4 = new TabPage();
			this.gbDuplicates = new GroupBox();
			this.panelDuplicates = new Panel();
			this.rbDupEmailHeadline = new RadioButton();
			this.rbDupHeadline = new RadioButton();
			this.rbDupEmail = new RadioButton();
			this.chkDuplicates = new CheckBox();
			this.dbDeepSearch = new GroupBox();
			this.rbSearchAlways = new RadioButton();
			this.rbSearchIfNoEmails = new RadioButton();
			this.rbDoNotSearch = new RadioButton();
			this.btnSave = new Button();
			this.btnCancel = new Button();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.gbProxyConnection.SuspendLayout();
			this.panelAuth.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.gbAccounts.SuspendLayout();
			this.gbFrequency.SuspendLayout();
			((ISupportInitialize)this.nudSendings).BeginInit();
			this.tabPage3.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.gbDuplicates.SuspendLayout();
			this.panelDuplicates.SuspendLayout();
			this.dbDeepSearch.SuspendLayout();
			base.SuspendLayout();
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Location = new Point(12, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new Size(647, 432);
			this.tabControl1.TabIndex = 0;
			this.tabPage1.Controls.Add(this.groupBox1);
			this.tabPage1.Controls.Add(this.gbProxyConnection);
			this.tabPage1.Location = new Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new Padding(3);
			this.tabPage1.Size = new Size(639, 406);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Connection settings";
			this.tabPage1.UseVisualStyleBackColor = true;
			this.groupBox1.Controls.Add(this.btnExportProxy);
			this.groupBox1.Controls.Add(this.btnImportProxy);
			this.groupBox1.Controls.Add(this.btnDeleteProxy);
			this.groupBox1.Controls.Add(this.btnEditProxy);
			this.groupBox1.Controls.Add(this.btnAddProxy);
			this.groupBox1.Controls.Add(this.listProxies);
			this.groupBox1.Location = new Point(6, 6);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(621, 188);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Suggested anonymous proxies";
			this.btnExportProxy.Location = new Point(540, 132);
			this.btnExportProxy.Name = "btnExportProxy";
			this.btnExportProxy.Size = new Size(75, 23);
			this.btnExportProxy.TabIndex = 6;
			this.btnExportProxy.Text = "Export";
			this.btnExportProxy.UseVisualStyleBackColor = true;
			this.btnExportProxy.Click += new EventHandler(this.btnExportProxy_Click);
			this.btnImportProxy.Location = new Point(540, 103);
			this.btnImportProxy.Name = "btnImportProxy";
			this.btnImportProxy.Size = new Size(75, 23);
			this.btnImportProxy.TabIndex = 5;
			this.btnImportProxy.Text = "Import";
			this.btnImportProxy.UseVisualStyleBackColor = true;
			this.btnImportProxy.Click += new EventHandler(this.btnImportProxy_Click);
			this.btnDeleteProxy.Location = new Point(540, 74);
			this.btnDeleteProxy.Name = "btnDeleteProxy";
			this.btnDeleteProxy.Size = new Size(75, 23);
			this.btnDeleteProxy.TabIndex = 4;
			this.btnDeleteProxy.Text = "Delete";
			this.btnDeleteProxy.UseVisualStyleBackColor = true;
			this.btnDeleteProxy.Click += new EventHandler(this.btnDeleteProxy_Click);
			this.btnEditProxy.Location = new Point(540, 45);
			this.btnEditProxy.Name = "btnEditProxy";
			this.btnEditProxy.Size = new Size(75, 23);
			this.btnEditProxy.TabIndex = 3;
			this.btnEditProxy.Text = "Edit";
			this.btnEditProxy.UseVisualStyleBackColor = true;
			this.btnEditProxy.Click += new EventHandler(this.btnEditProxy_Click);
			this.btnAddProxy.Location = new Point(540, 16);
			this.btnAddProxy.Name = "btnAddProxy";
			this.btnAddProxy.Size = new Size(75, 23);
			this.btnAddProxy.TabIndex = 2;
			this.btnAddProxy.Text = "Add";
			this.btnAddProxy.UseVisualStyleBackColor = true;
			this.btnAddProxy.Click += new EventHandler(this.btnAddProxy_Click);
			this.listProxies.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader1, 
				this.columnHeader2, 
				this.columnHeader3, 
				this.columnHeader4
			});
			this.listProxies.FullRowSelect = true;
			this.listProxies.Location = new Point(3, 16);
			this.listProxies.Name = "listProxies";
			this.listProxies.Size = new Size(531, 169);
			this.listProxies.TabIndex = 1;
			this.listProxies.UseCompatibleStateImageBehavior = false;
			this.listProxies.View = View.Details;
			this.listProxies.SelectedIndexChanged += new EventHandler(this.listProxies_SelectedIndexChanged);
			this.columnHeader1.Text = "IP";
			this.columnHeader2.Text = "Port";
			this.columnHeader3.Text = "Type";
			this.columnHeader4.Text = "Country";
			this.gbProxyConnection.Controls.Add(this.panelAuth);
			this.gbProxyConnection.Controls.Add(this.rbProxyUse);
			this.gbProxyConnection.Controls.Add(this.rbProxyRandom);
			this.gbProxyConnection.Controls.Add(this.rbProxyDontUse);
			this.gbProxyConnection.Location = new Point(6, 200);
			this.gbProxyConnection.Name = "gbProxyConnection";
			this.gbProxyConnection.Size = new Size(627, 200);
			this.gbProxyConnection.TabIndex = 1;
			this.gbProxyConnection.TabStop = false;
			this.gbProxyConnection.Text = "Connection";
			this.panelAuth.Controls.Add(this.txtProxyPassword);
			this.panelAuth.Controls.Add(this.txtProxyUsername);
			this.panelAuth.Controls.Add(this.lblProxyPassword);
			this.panelAuth.Controls.Add(this.lblProxyUsername);
			this.panelAuth.Controls.Add(this.chkProxyNeedAuth);
			this.panelAuth.Controls.Add(this.txtProxyPort);
			this.panelAuth.Controls.Add(this.lblPort);
			this.panelAuth.Controls.Add(this.txtProxyHost);
			this.panelAuth.Controls.Add(this.lblHost);
			this.panelAuth.Location = new Point(6, 84);
			this.panelAuth.Name = "panelAuth";
			this.panelAuth.Size = new Size(612, 110);
			this.panelAuth.TabIndex = 12;
			this.txtProxyPassword.Location = new Point(85, 79);
			this.txtProxyPassword.Name = "txtProxyPassword";
			this.txtProxyPassword.PasswordChar = '*';
			this.txtProxyPassword.Size = new Size(135, 20);
			this.txtProxyPassword.TabIndex = 20;
			this.txtProxyPassword.UseSystemPasswordChar = true;
			this.txtProxyUsername.Location = new Point(85, 53);
			this.txtProxyUsername.Name = "txtProxyUsername";
			this.txtProxyUsername.Size = new Size(135, 20);
			this.txtProxyUsername.TabIndex = 19;
			this.lblProxyPassword.AutoSize = true;
			this.lblProxyPassword.Location = new Point(24, 82);
			this.lblProxyPassword.Name = "lblProxyPassword";
			this.lblProxyPassword.Size = new Size(53, 13);
			this.lblProxyPassword.TabIndex = 18;
			this.lblProxyPassword.Text = "Password";
			this.lblProxyUsername.AutoSize = true;
			this.lblProxyUsername.Location = new Point(24, 56);
			this.lblProxyUsername.Name = "lblProxyUsername";
			this.lblProxyUsername.Size = new Size(55, 13);
			this.lblProxyUsername.TabIndex = 17;
			this.lblProxyUsername.Text = "Username";
			this.chkProxyNeedAuth.AutoSize = true;
			this.chkProxyNeedAuth.Location = new Point(6, 30);
			this.chkProxyNeedAuth.Name = "chkProxyNeedAuth";
			this.chkProxyNeedAuth.Size = new Size(122, 17);
			this.chkProxyNeedAuth.TabIndex = 16;
			this.chkProxyNeedAuth.Text = "Need authentication";
			this.chkProxyNeedAuth.UseVisualStyleBackColor = true;
			this.chkProxyNeedAuth.CheckedChanged += new EventHandler(this.chkProxyNeedAuth_CheckedChanged);
			this.txtProxyPort.Location = new Point(270, 3);
			this.txtProxyPort.Name = "txtProxyPort";
			this.txtProxyPort.Size = new Size(77, 20);
			this.txtProxyPort.TabIndex = 15;
			this.txtProxyPort.TextChanged += new EventHandler(this.txtProxyPort_TextChanged);
			this.lblPort.AutoSize = true;
			this.lblPort.Location = new Point(235, 7);
			this.lblPort.Name = "lblPort";
			this.lblPort.Size = new Size(29, 13);
			this.lblPort.TabIndex = 14;
			this.lblPort.Text = "Port:";
			this.txtProxyHost.Location = new Point(41, 4);
			this.txtProxyHost.Name = "txtProxyHost";
			this.txtProxyHost.Size = new Size(179, 20);
			this.txtProxyHost.TabIndex = 13;
			this.lblHost.AutoSize = true;
			this.lblHost.Location = new Point(3, 7);
			this.lblHost.Name = "lblHost";
			this.lblHost.Size = new Size(32, 13);
			this.lblHost.TabIndex = 12;
			this.lblHost.Text = "Host:";
			this.rbProxyUse.AutoSize = true;
			this.rbProxyUse.Location = new Point(6, 65);
			this.rbProxyUse.Name = "rbProxyUse";
			this.rbProxyUse.Size = new Size(94, 17);
			this.rbProxyUse.TabIndex = 2;
			this.rbProxyUse.Text = "Use this proxy:";
			this.rbProxyUse.UseVisualStyleBackColor = true;
			this.rbProxyUse.CheckedChanged += new EventHandler(this.rbProxyDontUse_CheckedChanged);
			this.rbProxyRandom.AutoSize = true;
			this.rbProxyRandom.Location = new Point(6, 42);
			this.rbProxyRandom.Name = "rbProxyRandom";
			this.rbProxyRandom.Size = new Size(167, 17);
			this.rbProxyRandom.TabIndex = 1;
			this.rbProxyRandom.Text = "Use random anonymous proxy";
			this.rbProxyRandom.UseVisualStyleBackColor = true;
			this.rbProxyRandom.CheckedChanged += new EventHandler(this.rbProxyDontUse_CheckedChanged);
			this.rbProxyDontUse.AutoSize = true;
			this.rbProxyDontUse.Checked = true;
			this.rbProxyDontUse.Location = new Point(6, 19);
			this.rbProxyDontUse.Name = "rbProxyDontUse";
			this.rbProxyDontUse.Size = new Size(98, 17);
			this.rbProxyDontUse.TabIndex = 0;
			this.rbProxyDontUse.TabStop = true;
			this.rbProxyDontUse.Text = "Don't use proxy";
			this.rbProxyDontUse.UseVisualStyleBackColor = true;
			this.rbProxyDontUse.CheckedChanged += new EventHandler(this.rbProxyDontUse_CheckedChanged);
			this.tabPage2.Controls.Add(this.gbAccounts);
			this.tabPage2.Controls.Add(this.gbFrequency);
			this.tabPage2.Location = new Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new Padding(3);
			this.tabPage2.Size = new Size(639, 406);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Email settings";
			this.tabPage2.UseVisualStyleBackColor = true;
			this.gbAccounts.Controls.Add(this.btnExportEmail);
			this.gbAccounts.Controls.Add(this.btnImportEmail);
			this.gbAccounts.Controls.Add(this.btnDeleteEmail);
			this.gbAccounts.Controls.Add(this.btnEditEmail);
			this.gbAccounts.Controls.Add(this.btnAddEmail);
			this.gbAccounts.Controls.Add(this.listEmails);
			this.gbAccounts.Location = new Point(6, 64);
			this.gbAccounts.Name = "gbAccounts";
			this.gbAccounts.Size = new Size(627, 336);
			this.gbAccounts.TabIndex = 1;
			this.gbAccounts.TabStop = false;
			this.gbAccounts.Text = "Accounts";
			this.btnExportEmail.Location = new Point(546, 135);
			this.btnExportEmail.Name = "btnExportEmail";
			this.btnExportEmail.Size = new Size(75, 23);
			this.btnExportEmail.TabIndex = 5;
			this.btnExportEmail.Text = "Export";
			this.btnExportEmail.UseVisualStyleBackColor = true;
			this.btnExportEmail.Click += new EventHandler(this.btnExportEmail_Click);
			this.btnImportEmail.Location = new Point(546, 106);
			this.btnImportEmail.Name = "btnImportEmail";
			this.btnImportEmail.Size = new Size(75, 23);
			this.btnImportEmail.TabIndex = 4;
			this.btnImportEmail.Text = "Import";
			this.btnImportEmail.UseVisualStyleBackColor = true;
			this.btnImportEmail.Click += new EventHandler(this.btnImportEmail_Click);
			this.btnDeleteEmail.Location = new Point(546, 77);
			this.btnDeleteEmail.Name = "btnDeleteEmail";
			this.btnDeleteEmail.Size = new Size(75, 23);
			this.btnDeleteEmail.TabIndex = 3;
			this.btnDeleteEmail.TabStop = false;
			this.btnDeleteEmail.Text = "Delete";
			this.btnDeleteEmail.UseVisualStyleBackColor = true;
			this.btnDeleteEmail.Click += new EventHandler(this.btnDeleteEmail_Click);
			this.btnEditEmail.Location = new Point(546, 48);
			this.btnEditEmail.Name = "btnEditEmail";
			this.btnEditEmail.Size = new Size(75, 23);
			this.btnEditEmail.TabIndex = 2;
			this.btnEditEmail.Text = "Edit";
			this.btnEditEmail.UseVisualStyleBackColor = true;
			this.btnEditEmail.Click += new EventHandler(this.btnEditEmail_Click);
			this.btnAddEmail.Location = new Point(546, 19);
			this.btnAddEmail.Name = "btnAddEmail";
			this.btnAddEmail.Size = new Size(75, 23);
			this.btnAddEmail.TabIndex = 1;
			this.btnAddEmail.Text = "Add";
			this.btnAddEmail.UseVisualStyleBackColor = true;
			this.btnAddEmail.Click += new EventHandler(this.btnAddEmail_Click);
			this.listEmails.CheckBoxes = true;
			this.listEmails.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader5, 
				this.columnHeader6
			});
			this.listEmails.FullRowSelect = true;
			this.listEmails.Location = new Point(9, 19);
			this.listEmails.Name = "listEmails";
			this.listEmails.Size = new Size(531, 311);
			this.listEmails.TabIndex = 0;
			this.listEmails.UseCompatibleStateImageBehavior = false;
			this.listEmails.View = View.Details;
			this.listEmails.SelectedIndexChanged += new EventHandler(this.listEmails_SelectedIndexChanged);
			this.columnHeader5.Text = "Email";
			this.columnHeader5.Width = 262;
			this.columnHeader6.Text = "Sender";
			this.columnHeader6.Width = 342;
			this.gbFrequency.Controls.Add(this.btnTestSmtp);
			this.gbFrequency.Controls.Add(this.label2);
			this.gbFrequency.Controls.Add(this.label1);
			this.gbFrequency.Controls.Add(this.nudSendings);
			this.gbFrequency.Location = new Point(6, 6);
			this.gbFrequency.Name = "gbFrequency";
			this.gbFrequency.Size = new Size(627, 52);
			this.gbFrequency.TabIndex = 0;
			this.gbFrequency.TabStop = false;
			this.gbFrequency.Text = "Frequency";
			this.btnTestSmtp.Location = new Point(519, 14);
			this.btnTestSmtp.Name = "btnTestSmtp";
			this.btnTestSmtp.Size = new Size(102, 23);
			this.btnTestSmtp.TabIndex = 3;
			this.btnTestSmtp.Text = "Test accounts...";
			this.btnTestSmtp.UseVisualStyleBackColor = true;
			this.btnTestSmtp.Click += new EventHandler(this.btnTestSmtp_Click);
			this.label2.AutoSize = true;
			this.label2.Location = new Point(106, 24);
			this.label2.Name = "label2";
			this.label2.Size = new Size(122, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "emails/hour per account";
			this.label1.AutoSize = true;
			this.label1.Location = new Point(6, 24);
			this.label1.Name = "label1";
			this.label1.Size = new Size(32, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Send";
			this.nudSendings.Location = new Point(44, 21);
			NumericUpDown arg_16C7_0 = this.nudSendings;
			int[] array = new int[4];
			array[0] = 10000;
			arg_16C7_0.Maximum = new decimal(array);
			NumericUpDown arg_16E6_0 = this.nudSendings;
			int[] array2 = new int[4];
			array2[0] = 1;
			arg_16E6_0.Minimum = new decimal(array2);
			this.nudSendings.Name = "nudSendings";
			this.nudSendings.Size = new Size(56, 20);
			this.nudSendings.TabIndex = 0;
			NumericUpDown arg_1735_0 = this.nudSendings;
			int[] array3 = new int[4];
			array3[0] = 1;
			arg_1735_0.Value = new decimal(array3);
			this.tabPage3.Controls.Add(this.editorBody);
			this.tabPage3.Controls.Add(this.lblTip);
			this.tabPage3.Controls.Add(this.lblBody);
			this.tabPage3.Controls.Add(this.txtSubject);
			this.tabPage3.Controls.Add(this.lblSubject);
			this.tabPage3.Location = new Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new Padding(3);
			this.tabPage3.Size = new Size(639, 406);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Email text";
			this.tabPage3.UseVisualStyleBackColor = true;
			this.editorBody.BodyHtml = null;
			this.editorBody.BodyText = null;
			this.editorBody.DocumentText = componentResourceManager.GetString("editorBody.DocumentText");
			this.editorBody.EditorBackColor = Color.FromArgb(255, 255, 255);
			this.editorBody.EditorForeColor = Color.FromArgb(0, 0, 0);
			this.editorBody.FontSize = FontSize.Three;
			this.editorBody.Location = new Point(9, 71);
			this.editorBody.Name = "editorBody";
			this.editorBody.Size = new Size(624, 316);
			this.editorBody.TabIndex = 5;
			this.editorBody.EnterKeyEvent += new EventHandler<Editor.EnterKeyEventArgs>(this.editorBody_EnterKeyEvent);
			this.lblTip.AutoSize = true;
			this.lblTip.Location = new Point(6, 390);
			this.lblTip.Name = "lblTip";
			this.lblTip.Size = new Size(320, 13);
			this.lblTip.TabIndex = 4;
			this.lblTip.Text = "Tip: use {text1|text2} syntax to randomly spin words in the brackets";
			this.lblBody.AutoSize = true;
			this.lblBody.Location = new Point(6, 50);
			this.lblBody.Name = "lblBody";
			this.lblBody.Size = new Size(34, 13);
			this.lblBody.TabIndex = 2;
			this.lblBody.Text = "Body:";
			this.txtSubject.Location = new Point(60, 17);
			this.txtSubject.Name = "txtSubject";
			this.txtSubject.Size = new Size(573, 20);
			this.txtSubject.TabIndex = 1;
			this.lblSubject.AutoSize = true;
			this.lblSubject.Location = new Point(6, 20);
			this.lblSubject.Name = "lblSubject";
			this.lblSubject.Size = new Size(46, 13);
			this.lblSubject.TabIndex = 0;
			this.lblSubject.Text = "Subject:";
			this.tabPage4.Controls.Add(this.gbDuplicates);
			this.tabPage4.Controls.Add(this.dbDeepSearch);
			this.tabPage4.Location = new Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new Padding(3);
			this.tabPage4.Size = new Size(639, 406);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Advanced";
			this.tabPage4.UseVisualStyleBackColor = true;
			this.gbDuplicates.Controls.Add(this.panelDuplicates);
			this.gbDuplicates.Controls.Add(this.chkDuplicates);
			this.gbDuplicates.Location = new Point(9, 103);
			this.gbDuplicates.Name = "gbDuplicates";
			this.gbDuplicates.Size = new Size(627, 119);
			this.gbDuplicates.TabIndex = 1;
			this.gbDuplicates.TabStop = false;
			this.gbDuplicates.Text = "Duplicates";
			this.panelDuplicates.Controls.Add(this.rbDupEmailHeadline);
			this.panelDuplicates.Controls.Add(this.rbDupHeadline);
			this.panelDuplicates.Controls.Add(this.rbDupEmail);
			this.panelDuplicates.Location = new Point(20, 42);
			this.panelDuplicates.Name = "panelDuplicates";
			this.panelDuplicates.Size = new Size(604, 69);
			this.panelDuplicates.TabIndex = 1;
			this.rbDupEmailHeadline.AutoSize = true;
			this.rbDupEmailHeadline.Location = new Point(3, 49);
			this.rbDupEmailHeadline.Name = "rbDupEmailHeadline";
			this.rbDupEmailHeadline.Size = new Size(181, 17);
			this.rbDupEmailHeadline.TabIndex = 2;
			this.rbDupEmailHeadline.Text = "with the same email and headline";
			this.rbDupEmailHeadline.UseVisualStyleBackColor = true;
			this.rbDupHeadline.AutoSize = true;
			this.rbDupHeadline.Location = new Point(3, 26);
			this.rbDupHeadline.Name = "rbDupHeadline";
			this.rbDupHeadline.Size = new Size(133, 17);
			this.rbDupHeadline.TabIndex = 1;
			this.rbDupHeadline.Text = "with the same headline";
			this.rbDupHeadline.UseVisualStyleBackColor = true;
			this.rbDupEmail.AutoSize = true;
			this.rbDupEmail.Checked = true;
			this.rbDupEmail.Location = new Point(3, 3);
			this.rbDupEmail.Name = "rbDupEmail";
			this.rbDupEmail.Size = new Size(117, 17);
			this.rbDupEmail.TabIndex = 0;
			this.rbDupEmail.TabStop = true;
			this.rbDupEmail.Text = "with the same email";
			this.rbDupEmail.UseVisualStyleBackColor = true;
			this.chkDuplicates.AutoSize = true;
			this.chkDuplicates.Location = new Point(6, 19);
			this.chkDuplicates.Name = "chkDuplicates";
			this.chkDuplicates.Size = new Size(117, 17);
			this.chkDuplicates.TabIndex = 0;
			this.chkDuplicates.Text = "Remove duplicates";
			this.chkDuplicates.UseVisualStyleBackColor = true;
			this.chkDuplicates.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
			this.dbDeepSearch.Controls.Add(this.rbSearchAlways);
			this.dbDeepSearch.Controls.Add(this.rbSearchIfNoEmails);
			this.dbDeepSearch.Controls.Add(this.rbDoNotSearch);
			this.dbDeepSearch.Location = new Point(6, 6);
			this.dbDeepSearch.Name = "dbDeepSearch";
			this.dbDeepSearch.Size = new Size(627, 91);
			this.dbDeepSearch.TabIndex = 0;
			this.dbDeepSearch.TabStop = false;
			this.dbDeepSearch.Text = "Deep search properties";
			this.rbSearchAlways.AutoSize = true;
			this.rbSearchAlways.Location = new Point(6, 65);
			this.rbSearchAlways.Name = "rbSearchAlways";
			this.rbSearchAlways.Size = new Size(137, 17);
			this.rbSearchAlways.TabIndex = 2;
			this.rbSearchAlways.TabStop = true;
			this.rbSearchAlways.Text = "Always search websites";
			this.rbSearchAlways.UseVisualStyleBackColor = true;
			this.rbSearchIfNoEmails.AutoSize = true;
			this.rbSearchIfNoEmails.Checked = true;
			this.rbSearchIfNoEmails.Location = new Point(6, 42);
			this.rbSearchIfNoEmails.Name = "rbSearchIfNoEmails";
			this.rbSearchIfNoEmails.Size = new Size(236, 17);
			this.rbSearchIfNoEmails.TabIndex = 1;
			this.rbSearchIfNoEmails.TabStop = true;
			this.rbSearchIfNoEmails.Text = "Search websites if there're no emails in listing";
			this.rbSearchIfNoEmails.UseVisualStyleBackColor = true;
			this.rbDoNotSearch.AutoSize = true;
			this.rbDoNotSearch.Location = new Point(6, 19);
			this.rbDoNotSearch.Name = "rbDoNotSearch";
			this.rbDoNotSearch.Size = new Size(136, 17);
			this.rbDoNotSearch.TabIndex = 0;
			this.rbDoNotSearch.Text = "Do not search websites";
			this.rbDoNotSearch.UseVisualStyleBackColor = true;
			this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnSave.Location = new Point(503, 450);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new Size(75, 23);
			this.btnSave.TabIndex = 1;
			this.btnSave.Text = "OK";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new Point(584, 450);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new Size(75, 23);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			base.AcceptButton = this.btnSave;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.btnCancel;
			base.ClientSize = new Size(671, 483);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnSave);
			base.Controls.Add(this.tabControl1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SettingsForm";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Settings";
			base.FormClosing += new FormClosingEventHandler(this.SettingsForm_FormClosing);
			base.FormClosed += new FormClosedEventHandler(this.SettingsForm_FormClosed);
			base.Load += new EventHandler(this.SettingsForm_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.gbProxyConnection.ResumeLayout(false);
			this.gbProxyConnection.PerformLayout();
			this.panelAuth.ResumeLayout(false);
			this.panelAuth.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.gbAccounts.ResumeLayout(false);
			this.gbFrequency.ResumeLayout(false);
			this.gbFrequency.PerformLayout();
			((ISupportInitialize)this.nudSendings).EndInit();
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			this.tabPage4.ResumeLayout(false);
			this.gbDuplicates.ResumeLayout(false);
			this.gbDuplicates.PerformLayout();
			this.panelDuplicates.ResumeLayout(false);
			this.panelDuplicates.PerformLayout();
			this.dbDeepSearch.ResumeLayout(false);
			this.dbDeepSearch.PerformLayout();
			base.ResumeLayout(false);
		}
		private void chkProxyNeedAuth_CheckedChanged(object sender, EventArgs e)
		{
			this.lblProxyUsername.Enabled = (this.lblProxyPassword.Enabled = (this.txtProxyUsername.Enabled = (this.txtProxyPassword.Enabled = this.chkProxyNeedAuth.Checked)));
			this.panelAuth.Enabled = this.rbProxyUse.Checked;
		}
		private void SettingsForm_Load(object sender, EventArgs e)
		{
			this.oldColor = this.txtProxyPort.ForeColor;
			switch (CommonData.Instance.Settings.ProxySetting.ProxyUse)
			{
				case ProxySettings.UseType.DontUse:
				{
					this.rbProxyDontUse.Checked = true;
					break;
				}
				case ProxySettings.UseType.UseRandom:
				{
					this.rbProxyRandom.Checked = true;
					break;
				}
				case ProxySettings.UseType.Use:
				{
					this.rbProxyUse.Checked = true;
					break;
				}
				default:
				{
					this.rbProxyDontUse.Checked = true;
					break;
				}
			}
			switch (CommonData.Instance.Settings.DuplicateProcess)
			{
				case ConsultingLeadsProSettings.Duplicates.Email:
				{
					this.chkDuplicates.Checked = true;
					this.rbDupEmail.Checked = true;
					break;
				}
				case ConsultingLeadsProSettings.Duplicates.Headline:
				{
					this.chkDuplicates.Checked = true;
					this.rbDupHeadline.Checked = true;
					break;
				}
				case ConsultingLeadsProSettings.Duplicates.EmailHeadline:
				{
					this.chkDuplicates.Checked = true;
					this.rbDupEmailHeadline.Checked = true;
					break;
				}
				default:
				{
					this.chkDuplicates.Checked = false;
					break;
				}
			}
			switch (CommonData.Instance.Settings.DeepSearch)
			{
				case ConsultingLeadsProSettings.DeepSearchOptions.SearchAlways:
				{
					this.rbSearchAlways.Checked = true;
					break;
				}
				case ConsultingLeadsProSettings.DeepSearchOptions.SearchNever:
				{
					this.rbDoNotSearch.Checked = true;
					break;
				}
				default:
				{
					this.rbSearchIfNoEmails.Checked = true;
					break;
				}
			}
			this.txtProxyHost.Text = CommonData.Instance.Settings.ProxySetting.Host;
			this.txtProxyPort.Text = ((CommonData.Instance.Settings.ProxySetting.Port == 0) ? string.Empty : CommonData.Instance.Settings.ProxySetting.Port.ToString());
			this.chkProxyNeedAuth.Checked = CommonData.Instance.Settings.ProxySetting.NeedsAuth;
			this.txtProxyUsername.Text = CommonData.Instance.Settings.ProxySetting.Username;
			this.txtProxyPassword.Text = CommonData.Instance.Settings.ProxySetting.Password;
			if (CommonData.Instance.Settings.NumberOfSendings >= this.nudSendings.Minimum && CommonData.Instance.Settings.NumberOfSendings <= this.nudSendings.Maximum)
			{
				this.nudSendings.Value = CommonData.Instance.Settings.NumberOfSendings;
			}
			else
			{
				this.nudSendings.Value = this.nudSendings.Minimum;
			}
			foreach (ProxyItem current in CommonData.Instance.Settings.ProxySetting.ProxyItems)
			{
				this.listProxies.Items.Add(new ListViewItem(new string[]
				{
					current.IP, 
					current.Port.ToString(), 
					current.Type, 
					current.Country
				})).Tag = current;
			}
			if (this.listProxies.Items.Count > 0)
			{
				this.listProxies.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
			}
			else
			{
				this.listProxies.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
			}
			foreach (SmtpItem current2 in CommonData.Instance.Settings.SmtpSettings)
			{
				ListViewItem listViewItem = new ListViewItem(new string[]
				{
					current2.EmailAddress, 
					current2.SenderName
				});
				listViewItem.Tag = current2;
				listViewItem.Checked = current2.Checked;
				this.listEmails.Items.Add(listViewItem);
			}
			if (this.listEmails.Items.Count > 0)
			{
				this.listEmails.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
			}
			else
			{
				this.listEmails.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
			}
			this.editorBody.DocumentText = CommonData.Instance.Settings.MessageBody;
			this.txtSubject.Text = CommonData.Instance.Settings.MessageSubject;
			this.rbProxyDontUse_CheckedChanged(sender, e);
			this.chkProxyNeedAuth_CheckedChanged(sender, e);
			this.listEmails_SelectedIndexChanged(sender, e);
			this.checkBox1_CheckedChanged(sender, e);
		}
		private void listProxies_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.listProxies.SelectedItems.Count > 0)
			{
				this.txtProxyHost.Text = this.listProxies.SelectedItems[0].SubItems[0].Text;
				this.txtProxyPort.Text = this.listProxies.SelectedItems[0].SubItems[1].Text;
				this.chkProxyNeedAuth.Checked = false;
			}
		}
		private void btnAddEmail_Click(object sender, EventArgs e)
		{
			EmailForm emailForm = new EmailForm(null);
			if (emailForm.ShowDialog() == DialogResult.OK)
			{
				ListViewItem listViewItem = new ListViewItem(new string[]
				{
					emailForm.Item.EmailAddress, 
					emailForm.Item.SenderName
				});
				listViewItem.Checked = true;
				listViewItem.Tag = emailForm.Item;
				this.listEmails.Items.Add(listViewItem);
				if (this.listEmails.Items.Count > 0)
				{
					this.listEmails.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
					return;
				}
				this.listEmails.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
			}
		}
		private void btnEditEmail_Click(object sender, EventArgs e)
		{
			if (this.listEmails.SelectedItems.Count > 0)
			{
				ListViewItem listViewItem = this.listEmails.SelectedItems[0];
				EmailForm emailForm = new EmailForm(listViewItem.Tag as SmtpItem);
				if (emailForm.ShowDialog() == DialogResult.OK)
				{
					listViewItem.SubItems[0].Text = emailForm.Item.EmailAddress;
					listViewItem.SubItems[1].Text = emailForm.Item.SenderName;
				}
			}
		}
		private void listEmails_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.btnEditEmail.Enabled = (this.btnDeleteEmail.Enabled = (this.listEmails.SelectedItems.Count > 0));
		}
		private void btnDeleteEmail_Click(object sender, EventArgs e)
		{
			while (this.listEmails.SelectedItems.Count > 0)
			{
				ListViewItem listViewItem = this.listEmails.SelectedItems[0];
				object arg_1A_0 = listViewItem.Tag;
				listViewItem.Remove();
			}
		}
		private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (base.DialogResult == DialogResult.OK)
			{
				int num;
				if (int.TryParse(this.txtProxyPort.Text, out num) && num > 0 && num < 65536)
				{
					e.Cancel = false;
					return;
				}
				if (!this.txtProxyPort.Enabled)
				{
					this.txtProxyPort.Text = string.Empty;
					e.Cancel = false;
					return;
				}
				e.Cancel = true;
				this.txtProxyPort.ForeColor = Color.Red;
				this.tabControl1.SelectedTab = this.tabPage1;
				this.txtProxyPort.Focus();
			}
		}
		private void txtProxyPort_TextChanged(object sender, EventArgs e)
		{
			this.txtProxyPort.ForeColor = this.oldColor;
		}
		private void btnAddProxy_Click(object sender, EventArgs e)
		{
			ProxyForm proxyForm = new ProxyForm(null);
			if (proxyForm.ShowDialog() == DialogResult.OK)
			{
				ListViewItem listViewItem = new ListViewItem(new string[]
				{
					proxyForm.Item.IP, 
					proxyForm.Item.Port.ToString(), 
					proxyForm.Item.Type, 
					proxyForm.Item.Country
				});
				listViewItem.Tag = proxyForm.Item;
				this.listProxies.Items.Add(listViewItem);
				if (this.listProxies.Items.Count > 0)
				{
					this.listProxies.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
					return;
				}
				this.listProxies.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
			}
		}
		private void btnEditProxy_Click(object sender, EventArgs e)
		{
			if (this.listProxies.SelectedItems.Count > 0)
			{
				ListViewItem listViewItem = this.listProxies.SelectedItems[0];
				ProxyForm proxyForm = new ProxyForm(listViewItem.Tag as ProxyItem);
				if (proxyForm.ShowDialog() == DialogResult.OK)
				{
					listViewItem.SubItems[0].Text = proxyForm.Item.IP;
					listViewItem.SubItems[1].Text = proxyForm.Item.Port.ToString();
					listViewItem.SubItems[2].Text = proxyForm.Item.Type;
					listViewItem.SubItems[3].Text = proxyForm.Item.Country;
				}
			}
		}
		private void btnDeleteProxy_Click(object sender, EventArgs e)
		{
			while (this.listProxies.SelectedItems.Count > 0)
			{
				ListViewItem listViewItem = this.listProxies.SelectedItems[0];
				object arg_1A_0 = listViewItem.Tag;
				listViewItem.Remove();
			}
		}
		private void btnImportEmail_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "CSV files files (*.csv)|*.csv";
			openFileDialog.CheckPathExists = true;
			openFileDialog.CheckFileExists = true;
			openFileDialog.AddExtension = true;
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				using (StreamReader streamReader = new StreamReader(openFileDialog.FileName))
				{
					while (!streamReader.EndOfStream)
					{
						string s = streamReader.ReadLine();
						List<string> list = CsvHelper.ParseLine(s);
						SmtpItem smtpItem = new SmtpItem();
						try
						{
							smtpItem.EmailAddress = list[0];
							smtpItem.SenderName = list[1];
							smtpItem.SmtpServer = list[2];
							smtpItem.Port = int.Parse(list[3]);
							smtpItem.NeedsAuth = (list[4].Trim() == "1");
							smtpItem.Username = list[5];
							smtpItem.Password = list[6];
							smtpItem.UseSsl = (list[7].Trim() == "1");
						}
						catch
						{
						}
						finally
						{
							ListViewItem listViewItem = new ListViewItem(new string[]
							{
								smtpItem.EmailAddress, 
								smtpItem.SenderName
							});
							listViewItem.Tag = smtpItem;
							this.listEmails.Items.Add(listViewItem);
						}
					}
				}
			}
			if (this.listEmails.Items.Count > 0)
			{
				this.listEmails.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
				return;
			}
			this.listEmails.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
		}
		private void btnExportEmail_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "CSV files files (*.csv)|*.csv";
			saveFileDialog.OverwritePrompt = true;
			saveFileDialog.CheckPathExists = true;
			saveFileDialog.AddExtension = true;
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				using (StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName))
				{
					foreach (SmtpItem current in CommonData.Instance.Settings.SmtpSettings)
					{
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.AppendFormat("\"{0}\",", (current.EmailAddress == null) ? string.Empty : current.EmailAddress.Replace("\"", "\"\""));
						stringBuilder.AppendFormat("\"{0}\",", (current.SenderName == null) ? string.Empty : current.SenderName.Replace("\"", "\"\""));
						stringBuilder.AppendFormat("\"{0}\",", (current.SmtpServer == null) ? string.Empty : current.SmtpServer.Replace("\"", "\"\""));
						stringBuilder.AppendFormat("\"{0}\",", current.Port);
						stringBuilder.AppendFormat("\"{0}\",", current.NeedsAuth ? "1" : "0");
						stringBuilder.AppendFormat("\"{0}\",", (current.Username == null) ? string.Empty : current.Username.Replace("\"", "\"\""));
						stringBuilder.AppendFormat("\"{0}\",", (current.Password == null) ? string.Empty : current.Password.Replace("\"", "\"\""));
						stringBuilder.AppendFormat("\"{0}\"", current.UseSsl ? "1" : "0");
						streamWriter.WriteLine(stringBuilder.ToString());
					}
				}
			}
		}
		private void btnImportProxy_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "CSV files files (*.csv)|*.csv";
			openFileDialog.CheckPathExists = true;
			openFileDialog.CheckFileExists = true;
			openFileDialog.AddExtension = true;
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				using (StreamReader streamReader = new StreamReader(openFileDialog.FileName))
				{
					while (!streamReader.EndOfStream)
					{
						string s = streamReader.ReadLine();
						List<string> list = CsvHelper.ParseLine(s);
						ProxyItem proxyItem = new ProxyItem();
						try
						{
							proxyItem.IP = list[0];
							proxyItem.Port = int.Parse(list[1]);
							proxyItem.Type = list[2];
							proxyItem.Country = list[3];
							proxyItem.NeedAuth = (list[4].Trim() == "1");
							proxyItem.Username = list[5];
							proxyItem.Password = list[6];
						}
						catch
						{
						}
						finally
						{
							ListViewItem listViewItem = new ListViewItem(new string[]
							{
								proxyItem.IP, 
								proxyItem.Port.ToString(), 
								proxyItem.Type, 
								proxyItem.Country
							});
							listViewItem.Tag = proxyItem;
							this.listProxies.Items.Add(listViewItem);
						}
					}
				}
			}
			if (this.listProxies.Items.Count > 0)
			{
				this.listProxies.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
				return;
			}
			this.listProxies.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
		}
		private void btnExportProxy_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "CSV files files (*.csv)|*.csv";
			saveFileDialog.OverwritePrompt = true;
			saveFileDialog.CheckPathExists = true;
			saveFileDialog.AddExtension = true;
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				using (StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName))
				{
					foreach (ProxyItem current in CommonData.Instance.Settings.ProxySetting.ProxyItems)
					{
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.AppendFormat("\"{0}\",", (current.IP == null) ? string.Empty : current.IP.Replace("\"", "\"\""));
						stringBuilder.AppendFormat("\"{0}\",", current.Port);
						stringBuilder.AppendFormat("\"{0}\",", (current.Type == null) ? string.Empty : current.Type.Replace("\"", "\"\""));
						stringBuilder.AppendFormat("\"{0}\",", (current.Country == null) ? string.Empty : current.Country.Replace("\"", "\"\""));
						stringBuilder.AppendFormat("\"{0}\",", current.NeedAuth ? "1" : "0");
						stringBuilder.AppendFormat("\"{0}\",", (current.Username == null) ? string.Empty : current.Username.Replace("\"", "\"\""));
						stringBuilder.AppendFormat("\"{0}\"", (current.Password == null) ? string.Empty : current.Password.Replace("\"", "\"\""));
						streamWriter.WriteLine(stringBuilder.ToString());
					}
				}
			}
		}
		private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (base.DialogResult == DialogResult.OK)
			{
				CommonData.Instance.Settings.SmtpSettings.Clear();
				CommonData.Instance.Settings.ProxySetting.ProxyItems.Clear();
				foreach (ListViewItem listViewItem in this.listEmails.Items)
				{
					SmtpItem smtpItem = listViewItem.Tag as SmtpItem;
					if (smtpItem != null)
					{
						smtpItem.Checked = listViewItem.Checked;
						CommonData.Instance.Settings.SmtpSettings.Add(smtpItem);
					}
				}
				foreach (ListViewItem listViewItem2 in this.listProxies.Items)
				{
					ProxyItem proxyItem = listViewItem2.Tag as ProxyItem;
					if (proxyItem != null)
					{
						CommonData.Instance.Settings.ProxySetting.ProxyItems.Add(proxyItem);
					}
				}
				if (this.rbProxyDontUse.Checked)
				{
					CommonData.Instance.Settings.ProxySetting.ProxyUse = ProxySettings.UseType.DontUse;
				}
				else
				{
					if (this.rbProxyRandom.Checked)
					{
						CommonData.Instance.Settings.ProxySetting.ProxyUse = ProxySettings.UseType.UseRandom;
					}
					else
					{
						CommonData.Instance.Settings.ProxySetting.ProxyUse = ProxySettings.UseType.Use;
					}
				}
				CommonData.Instance.Settings.ProxySetting.Host = this.txtProxyHost.Text;
				int port = 0;
				if (int.TryParse(this.txtProxyPort.Text, out port))
				{
					CommonData.Instance.Settings.ProxySetting.Port = port;
				}
				else
				{
					CommonData.Instance.Settings.ProxySetting.Port = 0;
				}
				CommonData.Instance.Settings.ProxySetting.NeedsAuth = this.chkProxyNeedAuth.Checked;
				CommonData.Instance.Settings.ProxySetting.Username = this.txtProxyUsername.Text;
				CommonData.Instance.Settings.ProxySetting.Password = this.txtProxyPassword.Text;
				CommonData.Instance.Settings.NumberOfSendings = this.nudSendings.Value;
				CommonData.Instance.Settings.MessageBody = this.editorBody.DocumentText;
				CommonData.Instance.Settings.MessageSubject = this.txtSubject.Text;
				if (this.rbDoNotSearch.Checked)
				{
					CommonData.Instance.Settings.DeepSearch = ConsultingLeadsProSettings.DeepSearchOptions.SearchNever;
				}
				else
				{
					if (this.rbSearchAlways.Checked)
					{
						CommonData.Instance.Settings.DeepSearch = ConsultingLeadsProSettings.DeepSearchOptions.SearchAlways;
					}
					else
					{
						CommonData.Instance.Settings.DeepSearch = ConsultingLeadsProSettings.DeepSearchOptions.SearchIfNoEmails;
					}
				}
				if (this.chkDuplicates.Checked)
				{
					if (this.rbDupEmail.Checked)
					{
						CommonData.Instance.Settings.DuplicateProcess = ConsultingLeadsProSettings.Duplicates.Email;
					}
					else
					{
						if (this.rbDupHeadline.Checked)
						{
							CommonData.Instance.Settings.DuplicateProcess = ConsultingLeadsProSettings.Duplicates.Headline;
						}
						else
						{
							if (this.rbDupEmailHeadline.Checked)
							{
								CommonData.Instance.Settings.DuplicateProcess = ConsultingLeadsProSettings.Duplicates.EmailHeadline;
							}
							else
							{
								CommonData.Instance.Settings.DuplicateProcess = ConsultingLeadsProSettings.Duplicates.Ignore;
							}
						}
					}
				}
				else
				{
					CommonData.Instance.Settings.DuplicateProcess = ConsultingLeadsProSettings.Duplicates.Ignore;
				}
			}
			CommonData.Instance.Settings.Save();
		}
		private void rbProxyDontUse_CheckedChanged(object sender, EventArgs e)
		{
			this.panelAuth.Enabled = this.rbProxyUse.Checked;
		}
		private void editorBody_EnterKeyEvent(object sender, Editor.EnterKeyEventArgs e)
		{
			this.editorBody.EmbedBr();
			e.Cancel = true;
		}
		private void btnTestSmtp_Click(object sender, EventArgs e)
		{
			List<SmtpItem> list = new List<SmtpItem>();
			foreach (ListViewItem listViewItem in this.listEmails.Items)
			{
				SmtpItem smtpItem = listViewItem.Tag as SmtpItem;
				if (smtpItem != null && listViewItem.Checked)
				{
					list.Add(smtpItem);
				}
			}
			TestEmailForm testEmailForm = new TestEmailForm(list, this.txtSubject.Text, this.editorBody.DocumentText);
			testEmailForm.ShowDialog();
		}
		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			this.panelDuplicates.Enabled = this.chkDuplicates.Checked;
		}
	}
}
