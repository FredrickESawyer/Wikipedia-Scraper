using ConsultingLeadsPro.Callbacks;
using ConsultingLeadsPro.Helpers;
using ConsultingLeadsPro.Network;
using ConsultingLeadsPro.Properties;
using ConsultingLeadsPro.Scrapers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace ConsultingLeadsPro
{
    public class MainForm : Form
    {
        private IContainer components;
        private Button btnSearch;
        private Button btnStop;
        private ComboBox cmbSite;
        private DateTimePicker dtpLowDate;
        private ComboBox cmbSearchInterval;
        private DateTimePicker dtpHighDate;
        private Label lblSource;
        private TextBox txtKeywords;
        private Label lblKeywords;
        private Label lblSearchIn;
        private Label lblIntervalBetween;
        private Label lblAnd;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private SplitContainer splitContainer4;
        private SplitContainer splitContainer3;
        private SplitContainer splitContainer2;
        private TreeView treeCategories;
        private TreeView treeLocations;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer5;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private GroupBox gbCategories;
        private ToolStrip toolStripCategories;
        private ToolStripButton tsbAddCategory;
        private ToolStripButton tsbEditCategory;
        private ToolStripButton tsbDeleteCategory;
        private GroupBox gbLocations;
        private ToolStrip toolStripLocations;
        private ToolStripButton tsbAddLocation;
        private ToolStripButton tsbEditLocation;
        private ToolStripButton tsbDeleteLocation;
        private GroupBox gbResults;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
        private ColumnHeader columnHeader9;
        private ToolStrip toolStripResults;
        private ToolStripButton tsrbtnSelectAll;
        private ToolStripButton tsrbtnSelectWithEmails;
        private ToolStripButton tsrbtnClearSelection;
        private ToolStripButton tsrbtnDeleteSelected;
        private ToolStripButton tsrbtnImportItems;
        private ToolStripButton tsrbtnExportItems;
        private ToolStripButton tsrbtnEditEmail;
        private ToolStripButton tsrbtnSendEmails;
        private GroupBox gbDescription;
        private WebBrowser webBrowser1;
        private NumericUpDown nudRadius;
        private GroupBox gbQueue;
        private ToolStrip toolStrip1;
        private ToolStripButton tsbEmailSelectAll;
        private ToolStripButton tsbEmailClearSelection;
        private ToolStripButton tsbEmailDeleteSelected;
        private ToolStripButton tsbEmaiLStopQueue;
        private ToolStripButton tsbEmailProcessQueue;
        private ListView listEmailTasks;
        private ColumnHeader columnHeader10;
        private ColumnHeader columnHeader11;
        private ColumnHeader columnHeader12;
        private ColumnHeader columnHeader13;
        private GroupBox gbLog;
        private TextBox txtLog;
        private ToolStrip toolStrip2;
        private ToolStripButton tsbClearLog;
        private ToolStripLabel toolStripLabel1;
        private ToolStripLabel toolStripLabel2;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripLabel tslStatus;
        private ToolStripProgressBar tpbProgress;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem checkForToolStripMenuItem;
        private ToolStripLabel tslItemCount;
        private Label lblRadius;
        private TableLayoutPanel tableLayoutPanel1;
        private ToolStripMenuItem saveCampaignToolStripMenuItem;
        private ToolStripMenuItem loadCampaignToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ListView listResults;
        private ToolStripMenuItem campaignInfoToolStripMenuItem;
        private ToolStripButton tsrbtnDeleteSent;
        protected bool isCampaignLoaded;
        protected Campaign campaign;
        private SmtpTaskManager smtpTaskManager = new SmtpTaskManager();
        private AbstractScraper cs;
        private TextBoxLogCallback tblc;

        public MainForm()
        {
            this.InitializeComponent();
            this.tblc = new TextBoxLogCallback(this.txtLog);
            this.smtpTaskManager.ErrorCallback = this.tblc;
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
            ComponentResourceManager ComponentResourceManager = new ComponentResourceManager(typeof(MainForm));
//            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnSearch = new Button();
            this.btnStop = new Button();
            this.cmbSite = new ComboBox();
            this.dtpLowDate = new DateTimePicker();
            this.cmbSearchInterval = new ComboBox();
            this.dtpHighDate = new DateTimePicker();
            this.lblSource = new Label();
            this.txtKeywords = new TextBox();
            this.lblKeywords = new Label();
            this.lblSearchIn = new Label();
            this.lblIntervalBetween = new Label();
            this.lblAnd = new Label();
            this.menuStrip1 = new MenuStrip();
            this.fileToolStripMenuItem = new ToolStripMenuItem();
            this.saveCampaignToolStripMenuItem = new ToolStripMenuItem();
            this.loadCampaignToolStripMenuItem = new ToolStripMenuItem();
            this.campaignInfoToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripMenuItem1 = new ToolStripSeparator();
            this.exitToolStripMenuItem = new ToolStripMenuItem();
            this.toolsToolStripMenuItem = new ToolStripMenuItem();
            this.settingsToolStripMenuItem = new ToolStripMenuItem();
            this.helpToolStripMenuItem = new ToolStripMenuItem();
            this.checkForToolStripMenuItem = new ToolStripMenuItem();
            this.aboutToolStripMenuItem = new ToolStripMenuItem();
            this.splitContainer4 = new SplitContainer();
            this.splitContainer3 = new SplitContainer();
            this.splitContainer2 = new SplitContainer();
            this.gbCategories = new GroupBox();
            this.toolStripCategories = new ToolStrip();
            this.tsbAddCategory = new ToolStripButton();
            this.tsbEditCategory = new ToolStripButton();
            this.tsbDeleteCategory = new ToolStripButton();
            this.treeCategories = new TreeView();
            this.gbLocations = new GroupBox();
            this.treeLocations = new TreeView();
            this.toolStripLocations = new ToolStrip();
            this.tsbAddLocation = new ToolStripButton();
            this.tsbEditLocation = new ToolStripButton();
            this.tsbDeleteLocation = new ToolStripButton();
            this.splitContainer1 = new SplitContainer();
            this.gbResults = new GroupBox();
            this.listResults = new ListView();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader2 = new ColumnHeader();
            this.columnHeader3 = new ColumnHeader();
            this.columnHeader4 = new ColumnHeader();
            this.columnHeader5 = new ColumnHeader();
            this.columnHeader6 = new ColumnHeader();
            this.columnHeader7 = new ColumnHeader();
            this.columnHeader8 = new ColumnHeader();
            this.columnHeader9 = new ColumnHeader();
            this.toolStripResults = new ToolStrip();
            this.tsrbtnSelectAll = new ToolStripButton();
            this.tsrbtnSelectWithEmails = new ToolStripButton();
            this.tsrbtnClearSelection = new ToolStripButton();
            this.tsrbtnDeleteSelected = new ToolStripButton();
            this.tsrbtnDeleteSent = new ToolStripButton();
            this.tsrbtnImportItems = new ToolStripButton();
            this.tsrbtnExportItems = new ToolStripButton();
            this.tsrbtnEditEmail = new ToolStripButton();
            this.tsrbtnSendEmails = new ToolStripButton();
            this.tslItemCount = new ToolStripLabel();
            this.gbDescription = new GroupBox();
            this.webBrowser1 = new WebBrowser();
            this.splitContainer5 = new SplitContainer();
            this.gbQueue = new GroupBox();
            this.toolStrip1 = new ToolStrip();
            this.tsbEmailSelectAll = new ToolStripButton();
            this.tsbEmailClearSelection = new ToolStripButton();
            this.tsbEmailDeleteSelected = new ToolStripButton();
            this.tsbEmaiLStopQueue = new ToolStripButton();
            this.tsbEmailProcessQueue = new ToolStripButton();
            this.listEmailTasks = new ListView();
            this.columnHeader10 = new ColumnHeader();
            this.columnHeader11 = new ColumnHeader();
            this.columnHeader12 = new ColumnHeader();
            this.columnHeader13 = new ColumnHeader();
            this.gbLog = new GroupBox();
            this.toolStrip2 = new ToolStrip();
            this.tsbClearLog = new ToolStripButton();
            this.toolStripLabel1 = new ToolStripLabel();
            this.toolStripLabel2 = new ToolStripLabel();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.tslStatus = new ToolStripLabel();
            this.tpbProgress = new ToolStripProgressBar();
            this.txtLog = new TextBox();
            this.nudRadius = new NumericUpDown();
            this.lblRadius = new Label();
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.menuStrip1.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.gbCategories.SuspendLayout();
            this.toolStripCategories.SuspendLayout();
            this.gbLocations.SuspendLayout();
            this.toolStripLocations.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbResults.SuspendLayout();
            this.toolStripResults.SuspendLayout();
            this.gbDescription.SuspendLayout();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this.gbQueue.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.gbLog.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((ISupportInitialize)this.nudRadius).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            base.SuspendLayout();
            this.btnSearch.Location = new Point(274, 54);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new Size(75, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new EventHandler(this.button1_Click);
            this.btnStop.Location = new Point(355, 54);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new Size(75, 23);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new EventHandler(this.button2_Click);
            this.cmbSite.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbSite.FormattingEnabled = true;
            this.cmbSite.Items.AddRange(new object[]
			{
				"CraigsList", 
				"BackPage", 
				"GumTree", 
				"EbayClassifieds", 
				"Yahoo", 
				"Google", 
				"Yellow Pages"
			});
            this.cmbSite.Location = new Point(74, 54);
            this.cmbSite.Name = "cmbSite";
            this.cmbSite.Size = new Size(190, 21);
            this.cmbSite.TabIndex = 8;
            this.cmbSite.SelectedIndexChanged += new EventHandler(this.cmbSite_SelectedIndexChanged);
            this.dtpLowDate.Anchor = AnchorStyles.Left;
            this.dtpLowDate.Format = DateTimePickerFormat.Short;
            this.dtpLowDate.Location = new Point(589, 3);
            this.dtpLowDate.Name = "dtpLowDate";
            this.dtpLowDate.Size = new Size(97, 20);
            this.dtpLowDate.TabIndex = 9;
            this.cmbSearchInterval.Anchor = AnchorStyles.Left;
            this.cmbSearchInterval.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbSearchInterval.FormattingEnabled = true;
            this.cmbSearchInterval.Items.AddRange(new object[]
			{
				"Today", 
				"Last 7 days", 
				"Last 30 days", 
				"Custom interval"
			});
            this.cmbSearchInterval.Location = new Point(370, 3);
            this.cmbSearchInterval.Name = "cmbSearchInterval";
            this.cmbSearchInterval.Size = new Size(121, 21);
            this.cmbSearchInterval.TabIndex = 10;
            this.cmbSearchInterval.Visible = false;
            this.cmbSearchInterval.SelectedIndexChanged += new EventHandler(this.cmbSearchInterval_SelectedIndexChanged);
            this.dtpHighDate.Anchor = AnchorStyles.Left;
            this.dtpHighDate.Format = DateTimePickerFormat.Short;
            this.dtpHighDate.Location = new Point(723, 3);
            this.dtpHighDate.Name = "dtpHighDate";
            this.dtpHighDate.Size = new Size(97, 20);
            this.dtpHighDate.TabIndex = 11;
            this.lblSource.AutoSize = true;
            this.lblSource.Location = new Point(12, 62);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new Size(44, 13);
            this.lblSource.TabIndex = 14;
            this.lblSource.Text = "Source:";
            this.txtKeywords.Anchor = AnchorStyles.Left;
            this.txtKeywords.Location = new Point(65, 3);
            this.txtKeywords.Name = "txtKeywords";
            this.txtKeywords.Size = new Size(123, 20);
            this.txtKeywords.TabIndex = 15;
            this.lblKeywords.Anchor = AnchorStyles.Left;
            this.lblKeywords.AutoSize = true;
            this.lblKeywords.Location = new Point(3, 7);
            this.lblKeywords.Name = "lblKeywords";
            this.lblKeywords.Size = new Size(56, 13);
            this.lblKeywords.TabIndex = 16;
            this.lblKeywords.Text = "Keywords:";
            this.lblSearchIn.Anchor = AnchorStyles.Left;
            this.lblSearchIn.AutoSize = true;
            this.lblSearchIn.Location = new Point(309, 7);
            this.lblSearchIn.Name = "lblSearchIn";
            this.lblSearchIn.Size = new Size(55, 13);
            this.lblSearchIn.TabIndex = 17;
            this.lblSearchIn.Text = "Search in:";
            this.lblIntervalBetween.Anchor = AnchorStyles.Left;
            this.lblIntervalBetween.AutoSize = true;
            this.lblIntervalBetween.Location = new Point(497, 7);
            this.lblIntervalBetween.Name = "lblIntervalBetween";
            this.lblIntervalBetween.Size = new Size(86, 13);
            this.lblIntervalBetween.TabIndex = 18;
            this.lblIntervalBetween.Text = "Interval between";
            this.lblAnd.Anchor = AnchorStyles.Left;
            this.lblAnd.AutoSize = true;
            this.lblAnd.Location = new Point(692, 7);
            this.lblAnd.Name = "lblAnd";
            this.lblAnd.Size = new Size(25, 13);
            this.lblAnd.TabIndex = 19;
            this.lblAnd.Text = "and";
            this.menuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.fileToolStripMenuItem, 
				this.toolsToolStripMenuItem, 
				this.helpToolStripMenuItem
			});
            this.menuStrip1.Location = new Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new Size(1074, 24);
            this.menuStrip1.TabIndex = 21;
            this.menuStrip1.Text = "menuStrip1";
            this.fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.saveCampaignToolStripMenuItem, 
				this.loadCampaignToolStripMenuItem, 
				this.campaignInfoToolStripMenuItem, 
				this.toolStripMenuItem1, 
				this.exitToolStripMenuItem
			});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            this.saveCampaignToolStripMenuItem.Name = "saveCampaignToolStripMenuItem";
            this.saveCampaignToolStripMenuItem.Size = new Size(165, 22);
            this.saveCampaignToolStripMenuItem.Text = "Save CLP Profile...";
            this.saveCampaignToolStripMenuItem.Click += new EventHandler(this.saveCampaignToolStripMenuItem_Click);
            this.loadCampaignToolStripMenuItem.Name = "loadCampaignToolStripMenuItem";
            this.loadCampaignToolStripMenuItem.Size = new Size(165, 22);
            this.loadCampaignToolStripMenuItem.Text = "Load CLP Profile...";
            this.loadCampaignToolStripMenuItem.Click += new EventHandler(this.loadCampaignToolStripMenuItem_Click);
            this.campaignInfoToolStripMenuItem.Name = "campaignInfoToolStripMenuItem";
            this.campaignInfoToolStripMenuItem.Size = new Size(165, 22);
            this.campaignInfoToolStripMenuItem.Text = "CLP Profile info...";
            this.campaignInfoToolStripMenuItem.Click += new EventHandler(this.campaignInfoToolStripMenuItem_Click);
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new Size(162, 6);
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new Size(165, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new EventHandler(this.exitToolStripMenuItem_Click);
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.settingsToolStripMenuItem
			});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new Size(125, 22);
            this.settingsToolStripMenuItem.Text = "Settings...";
            this.settingsToolStripMenuItem.Click += new EventHandler(this.settingsToolStripMenuItem_Click);
            this.helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.checkForToolStripMenuItem, 
				this.aboutToolStripMenuItem
			});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            this.checkForToolStripMenuItem.Name = "checkForToolStripMenuItem";
            this.checkForToolStripMenuItem.Size = new Size(179, 22);
            this.checkForToolStripMenuItem.Text = "Check for updates...";
            this.checkForToolStripMenuItem.Click += new EventHandler(this.checkForToolStripMenuItem_Click);
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new Size(179, 22);
            this.aboutToolStripMenuItem.Text = "About CLP";
            this.aboutToolStripMenuItem.Click += new EventHandler(this.aboutToolStripMenuItem_Click);
            this.splitContainer4.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.splitContainer4.Location = new Point(0, 80);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = Orientation.Vertical;
            this.splitContainer4.Panel1.Controls.Add(this.splitContainer3);
            this.splitContainer4.Panel2.Controls.Add(this.splitContainer5);
            this.splitContainer4.Size = new Size(1074, 637);
            this.splitContainer4.SplitterDistance = 780;
            this.splitContainer4.TabIndex = 24;
            this.splitContainer3.Dock = DockStyle.Fill;
            this.splitContainer3.Location = new Point(0, 20);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer3.Size = new Size(1074, 400);
            this.splitContainer3.SplitterDistance = 300;
            this.splitContainer3.TabIndex = 24;
            this.splitContainer2.Dock = DockStyle.Fill;
            this.splitContainer2.Location = new Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = Orientation.Horizontal;
            this.splitContainer2.Panel1.Controls.Add(this.gbCategories);
            this.splitContainer2.Panel2.Controls.Add(this.gbLocations);
            this.splitContainer2.Size = new Size(270, 350);
            this.splitContainer2.SplitterDistance = 172;
            this.splitContainer2.TabIndex = 23;
            this.gbCategories.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.gbCategories.Controls.Add(this.toolStripCategories);
            this.gbCategories.Controls.Add(this.treeCategories);
            this.gbCategories.Location = new Point(3, 3);
            this.gbCategories.Name = "gbCategories";
            this.gbCategories.Size = new Size(264, 166);
            this.gbCategories.TabIndex = 7;
            this.gbCategories.TabStop = false;
            this.gbCategories.Text = "Categories (check the ones to be used)";
            this.toolStripCategories.Dock = DockStyle.Bottom;
            this.toolStripCategories.GripStyle = ToolStripGripStyle.Hidden;
            this.toolStripCategories.Items.AddRange(new ToolStripItem[]
			{
				this.tsbAddCategory, 
				this.tsbEditCategory, 
				this.tsbDeleteCategory
			});
            this.toolStripCategories.Location = new Point(3, 138);
            this.toolStripCategories.Name = "toolStripCategories";
            this.toolStripCategories.Size = new Size(258, 25);
            this.toolStripCategories.TabIndex = 6;
            this.toolStripCategories.Text = "toolStrip1";
            this.tsbAddCategory.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsbAddCategory.Image = (Image)ComponentResourceManager.GetObject("tsbAddCategory.Image");
            this.tsbAddCategory.ImageTransparentColor = Color.Magenta;
            this.tsbAddCategory.Name = "tsbAddCategory";
            this.tsbAddCategory.Size = new Size(33, 22);
            this.tsbAddCategory.Text = "Add";
            this.tsbAddCategory.Click += new EventHandler(this.tsbAddCategory_Click);
            this.tsbEditCategory.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsbEditCategory.Enabled = false;
            this.tsbEditCategory.Image = (Image)ComponentResourceManager.GetObject("tsbEditCategory.Image");
            this.tsbEditCategory.ImageTransparentColor = Color.Magenta;
            this.tsbEditCategory.Name = "tsbEditCategory";
            this.tsbEditCategory.Size = new Size(31, 22);
            this.tsbEditCategory.Text = "Edit";
            this.tsbEditCategory.Click += new EventHandler(this.tsbEditCategory_Click);
            this.tsbDeleteCategory.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsbDeleteCategory.Enabled = false;
            this.tsbDeleteCategory.Image = (Image)ComponentResourceManager.GetObject("tsbDeleteCategory.Image");
            this.tsbDeleteCategory.ImageTransparentColor = Color.Magenta;
            this.tsbDeleteCategory.Name = "tsbDeleteCategory";
            this.tsbDeleteCategory.Size = new Size(44, 22);
            this.tsbDeleteCategory.Text = "Delete";
            this.tsbDeleteCategory.Click += new EventHandler(this.tsbDeleteCategory_Click);
            this.treeCategories.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.treeCategories.CheckBoxes = true;
            this.treeCategories.HideSelection = false;
            this.treeCategories.Location = new Point(6, 16);
            this.treeCategories.Name = "treeCategories";
            this.treeCategories.Size = new Size(252, 119);
            this.treeCategories.TabIndex = 5;
            this.treeCategories.AfterCheck += new TreeViewEventHandler(this.treeCategories_AfterCheck);
            this.treeCategories.AfterSelect += new TreeViewEventHandler(this.treeCategories_AfterSelect);
            this.gbLocations.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.gbLocations.Controls.Add(this.treeLocations);
            this.gbLocations.Controls.Add(this.toolStripLocations);
            this.gbLocations.Location = new Point(4, 3);
            this.gbLocations.Name = "gbLocations";
            this.gbLocations.Size = new Size(263, 168);
            this.gbLocations.TabIndex = 0;
            this.gbLocations.TabStop = false;
            this.gbLocations.Text = "Locations (check the onest to be used)";
            this.treeLocations.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.treeLocations.CheckBoxes = true;
            this.treeLocations.HideSelection = false;
            this.treeLocations.Location = new Point(2, 19);
            this.treeLocations.Name = "treeLocations";
            this.treeLocations.Size = new Size(255, 118);
            this.treeLocations.TabIndex = 7;
            this.treeLocations.AfterCheck += new TreeViewEventHandler(this.treeLocations_AfterCheck);
            this.treeLocations.AfterSelect += new TreeViewEventHandler(this.treeLocations_AfterSelect);
            this.toolStripLocations.Dock = DockStyle.Bottom;
            this.toolStripLocations.GripStyle = ToolStripGripStyle.Hidden;
            this.toolStripLocations.Items.AddRange(new ToolStripItem[]
			{
				this.tsbAddLocation, 
				this.tsbEditLocation, 
				this.tsbDeleteLocation
			});
            this.toolStripLocations.Location = new Point(3, 140);
            this.toolStripLocations.Name = "toolStripLocations";
            this.toolStripLocations.Size = new Size(257, 25);
            this.toolStripLocations.TabIndex = 0;
            this.toolStripLocations.Text = "toolStrip1";
            this.tsbAddLocation.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsbAddLocation.Image = (Image)ComponentResourceManager.GetObject("tsbAddLocation.Image");
            this.tsbAddLocation.ImageTransparentColor = Color.Magenta;
            this.tsbAddLocation.Name = "tsbAddLocation";
            this.tsbAddLocation.Size = new Size(33, 22);
            this.tsbAddLocation.Text = "Add";
            this.tsbAddLocation.Click += new EventHandler(this.tsbAddLocation_Click);
            this.tsbEditLocation.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsbEditLocation.Enabled = false;
            this.tsbEditLocation.Image = (Image)ComponentResourceManager.GetObject("tsbEditLocation.Image");
            this.tsbEditLocation.ImageTransparentColor = Color.Magenta;
            this.tsbEditLocation.Name = "tsbEditLocation";
            this.tsbEditLocation.Size = new Size(31, 22);
            this.tsbEditLocation.Text = "Edit";
            this.tsbEditLocation.Click += new EventHandler(this.tsbEditLocation_Click);
            this.tsbDeleteLocation.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsbDeleteLocation.Enabled = false;
            this.tsbDeleteLocation.Image = (Image)ComponentResourceManager.GetObject("tsbDeleteLocation.Image");
            this.tsbDeleteLocation.ImageTransparentColor = Color.Magenta;
            this.tsbDeleteLocation.Name = "tsbDeleteLocation";
            this.tsbDeleteLocation.Size = new Size(44, 22);
            this.tsbDeleteLocation.Text = "Delete";
            this.tsbDeleteLocation.Click += new EventHandler(this.tsbDeleteLocation_Click);
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.Location = new Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = Orientation.Horizontal;
            this.splitContainer1.Panel1.Controls.Add(this.gbResults);
            this.splitContainer1.Panel2.Controls.Add(this.gbDescription);
            this.splitContainer1.Size = new Size(800, 350);
            this.splitContainer1.SplitterDistance = 210;
            this.splitContainer1.TabIndex = 21;
            this.gbResults.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.gbResults.Controls.Add(this.listResults);
            this.gbResults.Controls.Add(this.toolStripResults);
            this.gbResults.Location = new Point(3, 3);
            this.gbResults.Name = "gbResults";
            this.gbResults.Size = new Size(794, 205);
            this.gbResults.TabIndex = 12;
            this.gbResults.TabStop = false;
            this.gbResults.Text = "Results";
            this.listResults.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.listResults.CheckBoxes = true;
            this.listResults.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader1, 
				this.columnHeader2, 
				this.columnHeader3, 
				this.columnHeader4, 
				this.columnHeader5, 
				this.columnHeader6, 
				this.columnHeader7, 
				this.columnHeader8, 
				this.columnHeader9
			});
            this.listResults.FullRowSelect = true;
            this.listResults.GridLines = true;
            this.listResults.HideSelection = false;
            this.listResults.Location = new Point(3, 16);
            this.listResults.Name = "listResults";
            this.listResults.ShowItemToolTips = true;
            this.listResults.Size = new Size(782, 158);
            this.listResults.TabIndex = 13;
            this.listResults.UseCompatibleStateImageBehavior = false;
            this.listResults.View = View.Details;
            this.listResults.ColumnClick += new ColumnClickEventHandler(this.listResults_ColumnClick);
            this.listResults.ItemChecked += new ItemCheckedEventHandler(this.listResults_ItemChecked);
            this.listResults.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.listResults.CausesValidationChanged += new EventHandler(this.listResults_CausesValidationChanged);
            this.listResults.MouseClick += new MouseEventHandler(this.listView1_MouseClick);
            this.listResults.MouseMove += new MouseEventHandler(this.listView1_MouseMove);
            this.columnHeader1.Text = "Category";
            this.columnHeader2.Text = "Location";
            this.columnHeader3.Text = "Headline";
            this.columnHeader4.Text = "Description";
            this.columnHeader5.Text = "Email";
            this.columnHeader6.Text = "Emails in body";
            this.columnHeader7.Text = "Phones in body";
            this.columnHeader8.Text = "Date posted";
            this.columnHeader9.Text = "Ad URL";
            this.toolStripResults.Dock = DockStyle.Bottom;
            this.toolStripResults.GripStyle = ToolStripGripStyle.Hidden;
            this.toolStripResults.Items.AddRange(new ToolStripItem[]
			{
				this.tsrbtnSelectAll, 
				this.tsrbtnSelectWithEmails, 
				this.tsrbtnClearSelection, 
				this.tsrbtnDeleteSelected, 
				this.tsrbtnDeleteSent, 
				this.tsrbtnImportItems, 
				this.tsrbtnExportItems, 
				this.tsrbtnEditEmail, 
				this.tsrbtnSendEmails, 
				this.tslItemCount
			});
            this.toolStripResults.Location = new Point(3, 177);
            this.toolStripResults.Name = "toolStripResults";
            this.toolStripResults.Size = new Size(788, 25);
            this.toolStripResults.TabIndex = 12;
            this.toolStripResults.Text = "toolStrip1";
            this.tsrbtnSelectAll.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsrbtnSelectAll.Image = (Image)ComponentResourceManager.GetObject("tsrbtnSelectAll.Image");
            this.tsrbtnSelectAll.ImageTransparentColor = Color.Magenta;
            this.tsrbtnSelectAll.Name = "tsrbtnSelectAll";
            this.tsrbtnSelectAll.Size = new Size(57, 22);
            this.tsrbtnSelectAll.Text = "Select all";
            this.tsrbtnSelectAll.Click += new EventHandler(this.tsrbtnSelectAll_Click);
            this.tsrbtnSelectWithEmails.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsrbtnSelectWithEmails.Image = (Image)ComponentResourceManager.GetObject("tsrbtnSelectWithEmails.Image");
            this.tsrbtnSelectWithEmails.ImageTransparentColor = Color.Magenta;
            this.tsrbtnSelectWithEmails.Name = "tsrbtnSelectWithEmails";
            this.tsrbtnSelectWithEmails.Size = new Size(105, 22);
            this.tsrbtnSelectWithEmails.Text = "Select with emails";
            this.tsrbtnSelectWithEmails.Click += new EventHandler(this.tsrbtnSelectWithEmails_Click);
            this.tsrbtnClearSelection.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsrbtnClearSelection.Image = (Image)ComponentResourceManager.GetObject("tsrbtnClearSelection.Image");
            this.tsrbtnClearSelection.ImageTransparentColor = Color.Magenta;
            this.tsrbtnClearSelection.Name = "tsrbtnClearSelection";
            this.tsrbtnClearSelection.Size = new Size(88, 22);
            this.tsrbtnClearSelection.Text = "Clear selection";
            this.tsrbtnClearSelection.Click += new EventHandler(this.tsrbtnClearSelection_Click);
            this.tsrbtnDeleteSelected.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsrbtnDeleteSelected.Image = (Image)ComponentResourceManager.GetObject("tsrbtnDeleteSelected.Image");
            this.tsrbtnDeleteSelected.ImageTransparentColor = Color.Magenta;
            this.tsrbtnDeleteSelected.Name = "tsrbtnDeleteSelected";
            this.tsrbtnDeleteSelected.Size = new Size(90, 22);
            this.tsrbtnDeleteSelected.Text = "Delete selected";
            this.tsrbtnDeleteSelected.Click += new EventHandler(this.tsrbtnDeleteSelected_Click);
            this.tsrbtnDeleteSent.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsrbtnDeleteSent.Image = (Image)ComponentResourceManager.GetObject("tsrbtnDeleteSent.Image");
            this.tsrbtnDeleteSent.ImageTransparentColor = Color.Magenta;
            this.tsrbtnDeleteSent.Name = "tsrbtnDeleteSent";
            this.tsrbtnDeleteSent.Size = new Size(69, 22);
            this.tsrbtnDeleteSent.Text = "Delete sent";
            this.tsrbtnDeleteSent.Click += new EventHandler(this.tsrbtnDeleteSent_Click);
            this.tsrbtnImportItems.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsrbtnImportItems.Image = (Image)ComponentResourceManager.GetObject("tsrbtnImportItems.Image");
            this.tsrbtnImportItems.ImageTransparentColor = Color.Magenta;
            this.tsrbtnImportItems.Name = "tsrbtnImportItems";
            this.tsrbtnImportItems.Size = new Size(79, 22);
            this.tsrbtnImportItems.Text = "Import items";
            this.tsrbtnImportItems.Click += new EventHandler(this.tsrbtnImportItems_Click);
            this.tsrbtnExportItems.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsrbtnExportItems.Image = (Image)ComponentResourceManager.GetObject("tsrbtnExportItems.Image");
            this.tsrbtnExportItems.ImageTransparentColor = Color.Magenta;
            this.tsrbtnExportItems.Name = "tsrbtnExportItems";
            this.tsrbtnExportItems.Size = new Size(76, 22);
            this.tsrbtnExportItems.Text = "Export items";
            this.tsrbtnExportItems.Click += new EventHandler(this.tsrbtnExportItems_Click);
            this.tsrbtnEditEmail.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsrbtnEditEmail.Image = (Image)ComponentResourceManager.GetObject("tsrbtnEditEmail.Image");
            this.tsrbtnEditEmail.ImageTransparentColor = Color.Magenta;
            this.tsrbtnEditEmail.Name = "tsrbtnEditEmail";
            this.tsrbtnEditEmail.Size = new Size(63, 22);
            this.tsrbtnEditEmail.Text = "Edit email";
            this.tsrbtnEditEmail.Click += new EventHandler(this.tsrbtnEditEmail_Click);
            this.tsrbtnSendEmails.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsrbtnSendEmails.Image = (Image)ComponentResourceManager.GetObject("tsrbtnSendEmails.Image");
            this.tsrbtnSendEmails.ImageTransparentColor = Color.Magenta;
            this.tsrbtnSendEmails.Name = "tsrbtnSendEmails";
            this.tsrbtnSendEmails.Size = new Size(74, 22);
            this.tsrbtnSendEmails.Text = "Send emails";
            this.tsrbtnSendEmails.Click += new EventHandler(this.tsrbtnSendEmails_Click);
            this.tslItemCount.Alignment = ToolStripItemAlignment.Right;
            this.tslItemCount.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tslItemCount.Name = "tslItemCount";
            this.tslItemCount.Size = new Size(76, 22);
            this.tslItemCount.Text = "0 items listed";
            this.gbDescription.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.gbDescription.Controls.Add(this.webBrowser1);
            this.gbDescription.Location = new Point(6, 3);
            this.gbDescription.Name = "gbDescription";
            this.gbDescription.Size = new Size(788, 130);
            this.gbDescription.TabIndex = 0;
            this.gbDescription.TabStop = false;
            this.gbDescription.Text = "Description";
            this.webBrowser1.AllowWebBrowserDrop = false;
            this.webBrowser1.Dock = DockStyle.Fill;
            this.webBrowser1.Location = new Point(3, 16);
            this.webBrowser1.MinimumSize = new Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new Size(782, 111);
            this.webBrowser1.TabIndex = 15;
            this.splitContainer5.Dock = DockStyle.Fill;
            this.splitContainer5.Location = new Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            this.splitContainer5.Orientation = Orientation.Horizontal;
            this.splitContainer5.Panel1.Controls.Add(this.gbQueue);
            this.splitContainer5.Panel2.Controls.Add(this.gbLog);
            this.splitContainer5.Size = new Size(1074, 283);
            this.splitContainer5.SplitterDistance = 160;
            this.splitContainer5.TabIndex = 26;
            this.gbQueue.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.gbQueue.Controls.Add(this.toolStrip1);
            this.gbQueue.Controls.Add(this.listEmailTasks);
            this.gbQueue.Location = new Point(3, 8);
            this.gbQueue.Name = "gbQueue";
            this.gbQueue.Size = new Size(1062, 149);
            this.gbQueue.TabIndex = 2;
            this.gbQueue.TabStop = false;
            this.gbQueue.Text = "Queue";
            this.toolStrip1.Dock = DockStyle.Bottom;
            this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.tsbEmailSelectAll, 
				this.tsbEmailClearSelection, 
				this.tsbEmailDeleteSelected, 
				this.tsbEmaiLStopQueue, 
				this.tsbEmailProcessQueue
			});
            this.toolStrip1.Location = new Point(3, 121);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new Size(1056, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            this.tsbEmailSelectAll.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsbEmailSelectAll.Image = (Image)ComponentResourceManager.GetObject("tsbEmailSelectAll.Image");
            this.tsbEmailSelectAll.ImageTransparentColor = Color.Magenta;
            this.tsbEmailSelectAll.Name = "tsbEmailSelectAll";
            this.tsbEmailSelectAll.Size = new Size(57, 22);
            this.tsbEmailSelectAll.Text = "Select all";
            this.tsbEmailSelectAll.Click += new EventHandler(this.tsbEmailSelectAll_Click);
            this.tsbEmailClearSelection.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsbEmailClearSelection.Image = (Image)ComponentResourceManager.GetObject("tsbEmailClearSelection.Image");
            this.tsbEmailClearSelection.ImageTransparentColor = Color.Magenta;
            this.tsbEmailClearSelection.Name = "tsbEmailClearSelection";
            this.tsbEmailClearSelection.Size = new Size(88, 22);
            this.tsbEmailClearSelection.Text = "Clear selection";
            this.tsbEmailClearSelection.Click += new EventHandler(this.tsbEmailClearSelection_Click);
            this.tsbEmailDeleteSelected.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsbEmailDeleteSelected.Image = (Image)ComponentResourceManager.GetObject("tsbEmailDeleteSelected.Image");
            this.tsbEmailDeleteSelected.ImageTransparentColor = Color.Magenta;
            this.tsbEmailDeleteSelected.Name = "tsbEmailDeleteSelected";
            this.tsbEmailDeleteSelected.Size = new Size(90, 22);
            this.tsbEmailDeleteSelected.Text = "Delete selected";
            this.tsbEmailDeleteSelected.Click += new EventHandler(this.tsbEmailDeleteSelected_Click);
            this.tsbEmaiLStopQueue.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsbEmaiLStopQueue.Image = (Image)ComponentResourceManager.GetObject("tsbEmaiLStopQueue.Image");
            this.tsbEmaiLStopQueue.ImageTransparentColor = Color.Magenta;
            this.tsbEmaiLStopQueue.Name = "tsbEmaiLStopQueue";
            this.tsbEmaiLStopQueue.Size = new Size(71, 22);
            this.tsbEmaiLStopQueue.Text = "Stop queue";
            this.tsbEmaiLStopQueue.Click += new EventHandler(this.tsbEmaiLStopQueue_Click);
            this.tsbEmailProcessQueue.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsbEmailProcessQueue.Image = (Image)ComponentResourceManager.GetObject("tsbEmailProcessQueue.Image");
            this.tsbEmailProcessQueue.ImageTransparentColor = Color.Magenta;
            this.tsbEmailProcessQueue.Name = "tsbEmailProcessQueue";
            this.tsbEmailProcessQueue.Size = new Size(87, 22);
            this.tsbEmailProcessQueue.Text = "Process queue";
            this.tsbEmailProcessQueue.Click += new EventHandler(this.tsbEmailProcessQueue_Click);
            this.listEmailTasks.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.listEmailTasks.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader10, 
				this.columnHeader11, 
				this.columnHeader12, 
				this.columnHeader13
			});
            this.listEmailTasks.FullRowSelect = true;
            this.listEmailTasks.GridLines = true;
            this.listEmailTasks.HideSelection = false;
            this.listEmailTasks.Location = new Point(3, 19);
            this.listEmailTasks.Name = "listEmailTasks";
            this.listEmailTasks.Size = new Size(1053, 99);
            this.listEmailTasks.Sorting = SortOrder.Ascending;
            this.listEmailTasks.TabIndex = 1;
            this.listEmailTasks.UseCompatibleStateImageBehavior = false;
            this.listEmailTasks.View = View.Details;
            this.columnHeader10.Text = "From";
            this.columnHeader11.Text = "To";
            this.columnHeader12.Text = "Schedule";
            this.columnHeader13.Text = "Status";
            this.gbLog.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.gbLog.Controls.Add(this.toolStrip2);
            this.gbLog.Controls.Add(this.txtLog);
            this.gbLog.Location = new Point(4, 3);
            this.gbLog.Name = "gbLog";
            this.gbLog.Size = new Size(1064, 113);
            this.gbLog.TabIndex = 0;
            this.gbLog.TabStop = false;
            this.gbLog.Text = "Log";
            this.toolStrip2.Dock = DockStyle.Bottom;
            this.toolStrip2.GripStyle = ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new ToolStripItem[]
			{
				this.tsbClearLog, 
				this.toolStripLabel1, 
				this.toolStripLabel2, 
				this.toolStripSeparator1, 
				this.tslStatus, 
				this.tpbProgress
			});
            this.toolStrip2.Location = new Point(3, 85);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new Size(1058, 25);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            this.tsbClearLog.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsbClearLog.Image = (Image)ComponentResourceManager.GetObject("tsbClearLog.Image");
            this.tsbClearLog.ImageTransparentColor = Color.Magenta;
            this.tsbClearLog.Name = "tsbClearLog";
            this.tsbClearLog.Size = new Size(58, 22);
            this.tsbClearLog.Text = "Clear log";
            this.tsbClearLog.Click += new EventHandler(this.tsbClearLog_Click);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new Size(6, 25);
            this.tslStatus.Name = "tslStatus";
            this.tslStatus.Size = new Size(77, 22);
            this.tslStatus.Text = "Status: Ready";
            this.tpbProgress.Name = "tpbProgress";
            this.tpbProgress.Size = new Size(100, 22);
            this.tpbProgress.Visible = false;
            this.toolStripLabel1.Alignment = ToolStripItemAlignment.Right;
            this.toolStripLabel1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.toolStripLabel1.IsLink = true;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new Size(177, 22);
            this.toolStripLabel1.Text = "http://www.consultingleadspro.com";
            this.toolStripLabel1.Click += new EventHandler(this.toolStripLabel1_Click);
            this.toolStripLabel2.Alignment = ToolStripItemAlignment.Right;
            this.toolStripLabel2.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new Size(188, 22);
            this.toolStripLabel2.Text = "Copyright (c) 2011 ConsultingLeadsPro - DS Systems";
           
            this.txtLog.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.txtLog.Location = new Point(6, 19);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = ScrollBars.Vertical;
            this.txtLog.Size = new Size(1049, 61);
            this.txtLog.TabIndex = 0;
            this.txtLog.TextChanged += new EventHandler(this.txtLog_TextChanged);
            this.nudRadius.Anchor = AnchorStyles.Left;
            this.nudRadius.DecimalPlaces = 2;
            this.nudRadius.Location = new Point(243, 3);
            this.nudRadius.Name = "nudRadius";
            this.nudRadius.Size = new Size(60, 20);
            this.nudRadius.TabIndex = 25;
            this.lblRadius.Anchor = AnchorStyles.Left;
            this.lblRadius.AutoSize = true;
            this.lblRadius.Location = new Point(194, 7);
            this.lblRadius.Name = "lblRadius";
            this.lblRadius.Size = new Size(43, 13);
            this.lblRadius.TabIndex = 26;
            this.lblRadius.Text = "Radius:";
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 10;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.cmbSearchInterval, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.nudRadius, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblRadius, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblIntervalBetween, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtpLowDate, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblAnd, 8, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtKeywords, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblKeywords, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtpHighDate, 9, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblSearchIn, 4, 0);
            this.tableLayoutPanel1.Location = new Point(9, 27);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel1.Size = new Size(823, 27);
            this.tableLayoutPanel1.TabIndex = 27;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(1074, 718);
            base.Controls.Add(this.splitContainer4);
            base.Controls.Add(this.lblSource);
            base.Controls.Add(this.cmbSite);
            base.Controls.Add(this.btnStop);
            base.Controls.Add(this.btnSearch);
            base.Controls.Add(this.menuStrip1);
            base.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            base.MainMenuStrip = this.menuStrip1;
            base.Name = "MainForm";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "ConsultingLeads Pro ";
            base.FormClosing += new FormClosingEventHandler(this.MainForm_FormClosing);
            base.Load += new EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.gbCategories.ResumeLayout(false);
            this.gbCategories.PerformLayout();
            this.toolStripCategories.ResumeLayout(false);
            this.toolStripCategories.PerformLayout();
            this.gbLocations.ResumeLayout(false);
            this.gbLocations.PerformLayout();
            this.toolStripLocations.ResumeLayout(false);
            this.toolStripLocations.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.gbResults.ResumeLayout(false);
            this.gbResults.PerformLayout();
            this.toolStripResults.ResumeLayout(false);
            this.toolStripResults.PerformLayout();
            this.gbDescription.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            this.splitContainer5.ResumeLayout(false);
            this.gbQueue.ResumeLayout(false);
            this.gbQueue.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.gbLog.ResumeLayout(false);
            this.gbLog.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((ISupportInitialize)this.nudRadius).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }
        public static void Work(object o)
        {
            AbstractScraper abstractScraper = o as AbstractScraper;
            if (abstractScraper != null)
            {
                abstractScraper.Search();
            }
        }
        private void InitSmtpTaskManager()
        {
            this.smtpTaskManager.Stop();
            this.smtpTaskManager.PerHour = (int)CommonData.Instance.Settings.NumberOfSendings;
            this.smtpTaskManager.Subject = CommonData.Instance.Settings.MessageSubject;
            this.smtpTaskManager.Message = CommonData.Instance.Settings.MessageBody;
            this.smtpTaskManager.Accounts.Clear();
            foreach (SmtpItem current in CommonData.Instance.Settings.SmtpSettings)
            {
                if (current.Checked)
                {
                    this.smtpTaskManager.Accounts.AddLast(current);
                }
            }
        }
        private void AddTreeNode(TreeNodeCollection nodes, SearchCategory sc)
        {
            TreeNode treeNode = nodes.Add(sc.Name);
            treeNode.Tag = sc;
            foreach (SearchCategory current in sc.Categories)
            {
                this.AddTreeNode(treeNode.Nodes, current);
            }
        }
        private void BuildTree(List<SearchCategory> cats, TreeView tree)
        {
            tree.Nodes.Clear();
            foreach (SearchCategory current in cats)
            {
                this.AddTreeNode(tree.Nodes, current);
            }
        }
        private List<SearchCategory> CollectNotEmbedded(TreeView treeView)
        {
            List<SearchCategory> list = new List<SearchCategory>();
            foreach (TreeNode treeNode in treeView.Nodes)
            {
                SearchCategory searchCategory = treeNode.Tag as SearchCategory;
                if (searchCategory != null && !searchCategory.IsEmbedded)
                {
                    list.Add(searchCategory);
                }
            }
            return list;
        }
        private void CollectUrlsFromTreeNodes(TreeNodeCollection nodes, List<SearchCategory> categories, bool checkedOnly)
        {
            foreach (TreeNode treeNode in nodes)
            {
                bool flag = !checkedOnly || treeNode.Checked;
                if (flag)
                {
                    SearchCategory searchCategory = treeNode.Tag as SearchCategory;
                    if (treeNode.Nodes.Count > 0 && string.IsNullOrEmpty(searchCategory.Url))
                    {
                        this.CollectUrlsFromTreeNodes(treeNode.Nodes, categories, checkedOnly);
                    }
                    else
                    {
                        if (searchCategory.Url != null)
                        {
                            categories.Add(searchCategory);
                        }
                    }
                }
                else
                {
                    this.CollectUrlsFromTreeNodes(treeNode.Nodes, categories, checkedOnly);
                }
            }
        }
        private List<SearchCategory> GetUrlsFromTree(TreeView tree, bool checkedOnly)
        {
            List<SearchCategory> list = new List<SearchCategory>();
            this.CollectUrlsFromTreeNodes(tree.Nodes, list, checkedOnly);
            return list;
        }
        private void StopScraping()
        {
            if (this.cs != null)
            {
                this.cs.Callback = null;
                if (this.cs.Downloader != null)
                {
                    this.cs.Downloader.errorCallback = null;
                }
                this.cs.StopScraping();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.listResults.Items.Clear();
            List<SearchCategory> urlsFromTree = this.GetUrlsFromTree(this.treeCategories, true);
            List<SearchCategory> urlsFromTree2 = this.GetUrlsFromTree(this.treeLocations, true);
            Thread thread = new Thread(new ParameterizedThreadStart(MainForm.Work));
            this.StopScraping();
            switch (this.cmbSite.SelectedIndex)
            {
                case 0:
                    {
                        this.cs = new CraigslistScraper(new UrlDownloader(this.tblc), new SimpleListViewAddCallback(this.listResults, this.tslStatus, this.tpbProgress));
                        break;
                    }
                case 1:
                    {
                        this.cs = new BackpageScraper(new UrlDownloader(this.tblc), new SimpleListViewAddCallback(this.listResults, this.tslStatus, this.tpbProgress));
                        break;
                    }
                case 2:
                    {
                        this.cs = new GumtreeScraper(new UrlDownloader(this.tblc), new SimpleListViewAddCallback(this.listResults, this.tslStatus, this.tpbProgress));
                        break;
                    }
                case 3:
                    {
                        this.cs = new KijijiScraper(new UrlDownloader(this.tblc), new SimpleListViewAddCallback(this.listResults, this.tslStatus, this.tpbProgress));
                        break;
                    }
                case 4:
                    {
                        this.cs = new YahooScraper(new UrlDownloader(this.tblc), new MapListViewAddCallback(this.listResults, this.tslStatus, this.tpbProgress));
                        (this.cs as YahooScraper).Radius = (float)this.nudRadius.Value;
                        break;
                    }
                case 5:
                    {
                        this.cs = new GoogleScraper(new UrlDownloader(this.tblc), new MapListViewAddCallback(this.listResults, this.tslStatus, this.tpbProgress));
                        (this.cs as GoogleScraper).Radius = (float)this.nudRadius.Value;
                        break;
                    }
                case 6:
                    {
                        this.cs = new YellowPagesScraper(new UrlDownloader(this.tblc), new MapListViewAddCallback(this.listResults, this.tslStatus, this.tpbProgress));
                        (this.cs as YellowPagesScraper).Radius = (float)this.nudRadius.Value;
                        break;
                    }
                default:
                    {
                        return;
                    }
            }
            this.campaign = this.cs.SearchCampaign;
            switch (this.cmbSearchInterval.SelectedIndex)
            {
                case 0:
                    {
                        this.cs.LowDate = DateTime.Today;
                        this.cs.HighDate = DateTime.Today.AddDays(1.0).AddSeconds(-1.0);
                        break;
                    }
                case 1:
                    {
                        this.cs.LowDate = DateTime.Today.AddDays(-7.0);
                        this.cs.HighDate = DateTime.Today.AddDays(1.0).AddSeconds(-1.0);
                        break;
                    }
                case 2:
                    {
                        this.cs.LowDate = DateTime.Today.AddDays(-30.0);
                        this.cs.HighDate = DateTime.Today.AddDays(1.0).AddSeconds(-1.0);
                        break;
                    }
                case 3:
                    {
                        this.cs.LowDate = this.dtpLowDate.Value;
                        this.cs.HighDate = this.dtpHighDate.Value.AddDays(1.0).AddSeconds(-1.0);
                        break;
                    }
            }
            this.cs.Categories.AddRange(urlsFromTree);
            this.cs.Locations.AddRange(urlsFromTree2);
            this.cs.Keyword = this.txtKeywords.Text;
            thread.Start(this.cs);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.cs != null)
            {
                this.cs.StopScraping();
                this.cs.Callback = null;
                if (this.cs.Downloader != null)
                {
                    this.cs.Downloader.errorCallback = null;
                }
            }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
                this.Text = string.Format("{0} {1}", versionInfo.ProductName, versionInfo.ProductVersion);
            }
            catch
            {
                this.Text = "Consulting Leads Pro";
            }
            Guid guid = new Guid(Settings.Default.ProxyListGuid);
            if (guid != CommonData.Instance.Settings.ProxyListGuid)
            {
                StringReader stringReader = new StringReader(Resources.proxy);
                string text;
                while ((text = stringReader.ReadLine()) != null)
                {
                    string[] array = text.Split(new char[]
					{
						':'
					});
                    ProxyItem pi = new ProxyItem();
                    pi.IP = array[0];
                    try
                    {
                        pi.Port = int.Parse(array[1]);
                    }
                    catch
                    {
                        continue;
                    }
                    CommonData.Instance.Settings.ProxyListGuid = guid;
                    if (CommonData.Instance.Settings.ProxySetting.ProxyItems.Count((ProxyItem p) => p.IP == pi.IP && p.Port == pi.Port) <= 0)
                    {
                        CommonData.Instance.Settings.ProxySetting.ProxyItems.Add(pi);
                    }
                }
                CommonData.Instance.Settings.Save();
            }
            this.cmbSite.SelectedIndex = 0;
            this.cmbSearchInterval.SelectedIndex = 0;
            this.dtpLowDate.Value = DateTime.Today;
            this.dtpHighDate.Value = DateTime.Today;
            this.listResults_CausesValidationChanged(sender, e);
            this.smtpTaskManager.Callback = new ListViewTaskCallback(this.listEmailTasks);
            this.listEmailTasks.ListViewItemSorter = new ListViewItemDateComparer(2, true);
            this.InitSmtpTaskManager();
        }
        private void treeCategories_AfterCheck(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode treeNode in e.Node.Nodes)
            {
                if (treeNode.Checked != e.Node.Checked)
                {
                    treeNode.Checked = e.Node.Checked;
                }
            }
        }
        private void listView1_MouseMove(object sender, MouseEventArgs e)
        {
            ListViewItem itemAt = this.listResults.GetItemAt(e.X, e.Y);
            if (itemAt != null)
            {
                if (itemAt.Tag is SimpleScrapeResult)
                {
                    ListViewItem.ListViewSubItem subItemAt = itemAt.GetSubItemAt(e.X, e.Y);
                    if (itemAt.SubItems.Count > 8 && subItemAt == itemAt.SubItems[8])
                    {
                        this.listResults.Cursor = Cursors.Hand;
                        return;
                    }
                }
                else
                {
                    if (itemAt.Tag is MapScrapeResult)
                    {
                        ListViewItem.ListViewSubItem subItemAt2 = itemAt.GetSubItemAt(e.X, e.Y);
                        if (itemAt.SubItems.Count > 10 && subItemAt2 == itemAt.SubItems[10])
                        {
                            if (!string.IsNullOrEmpty(subItemAt2.Text))
                            {
                                this.listResults.Cursor = Cursors.Hand;
                            }
                            return;
                        }
                        if (itemAt.SubItems.Count > 13 && subItemAt2 == itemAt.SubItems[13])
                        {
                            if (!string.IsNullOrEmpty(subItemAt2.Text))
                            {
                                this.listResults.Cursor = Cursors.Hand;
                            }
                            return;
                        }
                        if (itemAt.SubItems.Count > 14 && subItemAt2 == itemAt.SubItems[14])
                        {
                            if (!string.IsNullOrEmpty(subItemAt2.Text))
                            {
                                this.listResults.Cursor = Cursors.Hand;
                            }
                            return;
                        }
                    }
                }
            }
            this.listResults.Cursor = Cursors.Default;
        }
        private void ShowToolbars(bool show)
        {
            if (show)
            {
                this.toolStripCategories.Visible = true;
                this.treeCategories.Height = this.toolStripCategories.Bottom - this.treeCategories.Top - this.toolStripCategories.Height;
                this.toolStripLocations.Visible = true;
                this.treeLocations.Height = this.toolStripLocations.Bottom - this.treeLocations.Top - this.toolStripLocations.Height;
                this.lblIntervalBetween.Visible = (this.lblAnd.Visible = (this.dtpHighDate.Visible = (this.dtpLowDate.Visible = (this.cmbSearchInterval.Visible = !show))));
            }
            else
            {
                this.toolStripCategories.Visible = false;
                this.treeCategories.Height = this.toolStripCategories.Bottom - this.treeCategories.Top;
                this.toolStripLocations.Visible = false;
                this.treeLocations.Height = this.toolStripLocations.Bottom - this.treeLocations.Top;
            }
            this.lblSearchIn.Visible = (this.lblIntervalBetween.Visible = (this.lblAnd.Visible = (this.dtpHighDate.Visible = (this.dtpLowDate.Visible = (this.cmbSearchInterval.Visible = !show)))));
            Control arg_195_0 = this.lblRadius;
            this.nudRadius.Visible = show;
            arg_195_0.Visible = show;
        }
        private void SaveCurrentState()
        {
            if (this.cmbSite.Tag != null)
            {
                switch ((int)this.cmbSite.Tag)
                {
                    case 4:
                        {
                            List<SearchCategory> data = this.CollectNotEmbedded(this.treeCategories);
                            List<SearchCategory> data2 = this.CollectNotEmbedded(this.treeLocations);
                            SearchCategory.Save(string.Format("{0}\\yacateg.dat", CommonData.Instance.ApplicationFolderPath), data);
                            SearchCategory.Save(string.Format("{0}\\yalocat.dat", CommonData.Instance.ApplicationFolderPath), data2);
                            return;
                        }
                    case 5:
                        {
                            List<SearchCategory> data = this.CollectNotEmbedded(this.treeCategories);
                            List<SearchCategory> data2 = this.CollectNotEmbedded(this.treeLocations);
                            SearchCategory.Save(string.Format("{0}\\gocateg.dat", CommonData.Instance.ApplicationFolderPath), data);
                            SearchCategory.Save(string.Format("{0}\\golocat.dat", CommonData.Instance.ApplicationFolderPath), data2);
                            return;
                        }
                    case 6:
                        {
                            List<SearchCategory> data = this.CollectNotEmbedded(this.treeCategories);
                            List<SearchCategory> data2 = this.CollectNotEmbedded(this.treeLocations);
                            SearchCategory.Save(string.Format("{0}\\ypcateg.dat", CommonData.Instance.ApplicationFolderPath), data);
                            SearchCategory.Save(string.Format("{0}\\yplocat.dat", CommonData.Instance.ApplicationFolderPath), data2);
                            break;
                        }
                    default:
                        {
                            return;
                        }
                }
            }
        }
        private void cmbSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.StopScraping();
            this.SaveCurrentState();
            List<SearchCategory> list = null;
            List<SearchCategory> list2 = null;
            this.cmbSite.Tag = this.cmbSite.SelectedIndex;
            byte[] buffer;
            byte[] buffer2;
            switch (this.cmbSite.SelectedIndex)
            {
                case 0:
                    {
                        buffer = Resources.clcateg_gdb;
                        buffer2 = Resources.cllocat_gdb;
                        this.ShowToolbars(false);
                        break;
                    }
                case 1:
                    {
                        buffer = Resources.bpcateg_gdb;
                        buffer2 = Resources.bplocat_gdb;
                        this.ShowToolbars(false);
                        break;
                    }
                case 2:
                    {
                        buffer = Resources.gtcateg_gdb;
                        buffer2 = Resources.gtlocat_gdb;
                        this.ShowToolbars(false);
                        break;
                    }
                case 3:
                    {
                        buffer = Resources.kjcateg_gdb;
                        buffer2 = Resources.kjlocat_gdb;
                        this.ShowToolbars(false);
                        break;
                    }
                case 4:
                    {
                        list2 = SearchCategory.Load(string.Format("{0}\\yalocat.dat", CommonData.Instance.ApplicationFolderPath));
                        list = SearchCategory.Load(string.Format("{0}\\yacateg.dat", CommonData.Instance.ApplicationFolderPath));
                        buffer = Resources.yacateg_gdb;
                        buffer2 = Resources.yalocat_gdb;
                        this.ShowToolbars(true);
                        break;
                    }
                case 5:
                    {
                        list2 = SearchCategory.Load(string.Format("{0}\\golocat.dat", CommonData.Instance.ApplicationFolderPath));
                        list = SearchCategory.Load(string.Format("{0}\\gocateg.dat", CommonData.Instance.ApplicationFolderPath));
                        buffer = Resources.yacateg_gdb;
                        buffer2 = Resources.yalocat_gdb;
                        this.ShowToolbars(true);
                        break;
                    }
                case 6:
                    {
                        list2 = SearchCategory.Load(string.Format("{0}\\yplocat.dat", CommonData.Instance.ApplicationFolderPath));
                        list = SearchCategory.Load(string.Format("{0}\\ypcateg.dat", CommonData.Instance.ApplicationFolderPath));
                        buffer = Resources.ypcateg_gdb;
                        buffer2 = Resources.yplocat_gdb;
                        this.ShowToolbars(true);
                        break;
                    }
                default:
                    {
                        return;
                    }
            }
            using (MemoryStream memoryStream = new MemoryStream(buffer))
            {
                using (GZipStream gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    List<SearchCategory> list3 = SearchCategory.LoadGdb(gZipStream);
                    if (list != null)
                    {
                        list3.AddRange(list);
                    }
                    this.BuildTree(list3, this.treeCategories);
                }
            }
            using (MemoryStream memoryStream2 = new MemoryStream(buffer2))
            {
                using (GZipStream gZipStream2 = new GZipStream(memoryStream2, CompressionMode.Decompress))
                {
                    List<SearchCategory> list4 = SearchCategory.LoadGdb(gZipStream2);
                    if (list2 != null)
                    {
                        list4.AddRange(list2);
                    }
                    this.BuildTree(list4, this.treeLocations);
                }
            }
        }
        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            ListViewItem itemAt = this.listResults.GetItemAt(e.X, e.Y);
            if (itemAt != null)
            {
                ListViewItem.ListViewSubItem subItemAt = itemAt.GetSubItemAt(e.X, e.Y);
                if (itemAt.Tag is SimpleScrapeResult)
                {
                    try
                    {
                        if (itemAt.SubItems.Count > 8 && subItemAt == itemAt.SubItems[8])
                        {
                            Process.Start(subItemAt.Text);
                        }
                        return;
                    }
                    catch
                    {
                        return;
                    }
                }
                if (itemAt.Tag is MapScrapeResult)
                {
                    try
                    {
                        if (itemAt.SubItems.Count > 10 && subItemAt == itemAt.SubItems[10] && !string.IsNullOrEmpty(subItemAt.Text))
                        {
                            Process.Start(subItemAt.Text);
                        }
                        if (itemAt.SubItems.Count > 13 && subItemAt == itemAt.SubItems[13] && !string.IsNullOrEmpty(subItemAt.Text))
                        {
                            Process.Start(subItemAt.Text);
                        }
                        if (itemAt.SubItems.Count > 14 && subItemAt == itemAt.SubItems[14] && !string.IsNullOrEmpty(subItemAt.Text))
                        {
                            Process.Start(subItemAt.Text);
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listResults.SelectedItems.Count <= 0)
            {
                return;
            }
            ListViewItem listViewItem = this.listResults.SelectedItems[0];
            SimpleScrapeResult simpleScrapeResult = listViewItem.Tag as SimpleScrapeResult;
            if (simpleScrapeResult != null)
            {
                this.webBrowser1.DocumentText = simpleScrapeResult.Description;
                return;
            }
            MapScrapeResult mapScrapeResult = listViewItem.Tag as MapScrapeResult;
            if (mapScrapeResult != null)
            {
                try
                {
                    Uri uri = new Uri(mapScrapeResult.AdUrl);
                    if (uri == null)
                    {
                        string documentText;
                        if (new UrlDownloader(null).DownloadUrl(mapScrapeResult.AdUrl, out documentText))
                        {
                            this.webBrowser1.DocumentText = documentText;
                        }
                        else
                        {
                            this.webBrowser1.DocumentText = string.Empty;
                        }
                    }
                    else
                    {
                        this.webBrowser1.Url = uri;
                    }
                }
                catch
                {
                }
            }
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.StopScraping();
            CommonData.Instance.Settings.Save();
            this.SaveCurrentState();
        }
        private void cmbSearchInterval_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dtpHighDate.Enabled = (this.dtpLowDate.Enabled = (this.lblAnd.Enabled = (this.lblIntervalBetween.Enabled = (this.cmbSearchInterval.SelectedIndex == 3))));
        }
        private void treeLocations_AfterCheck(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode treeNode in e.Node.Nodes)
            {
                if (treeNode.Checked != e.Node.Checked)
                {
                    treeNode.Checked = e.Node.Checked;
                }
            }
        }
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                this.InitSmtpTaskManager();
            }
        }
        private void tsrbtnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem listViewItem in this.listResults.Items)
            {
                listViewItem.Checked = true;
            }
        }
        private void tsrbtnSelectWithEmails_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem listViewItem in this.listResults.Items)
            {
                SimpleScrapeResult simpleScrapeResult = listViewItem.Tag as SimpleScrapeResult;
                if (simpleScrapeResult != null)
                {
                    listViewItem.Checked = !string.IsNullOrEmpty(simpleScrapeResult.GetEmail());
                }
                MapScrapeResult mapScrapeResult = listViewItem.Tag as MapScrapeResult;
                if (mapScrapeResult != null)
                {
                    listViewItem.Checked = !string.IsNullOrEmpty(mapScrapeResult.GetEmail());
                }
            }
        }
        private void tsrbtnClearSelection_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem listViewItem in this.listResults.Items)
            {
                listViewItem.Checked = false;
            }
        }
        private void tsrbtnDeleteSelected_Click(object sender, EventArgs e)
        {
            while (this.listResults.CheckedItems.Count > 0)
            {
                IScrapeResult item = this.listResults.CheckedItems[0].Tag as IScrapeResult;
                this.listResults.CheckedItems[0].Remove();
                this.campaign.Leads.Remove(item);
            }
        }
        private void tsrbtnSendEmails_Click(object sender, EventArgs e)
        {
            if (this.smtpTaskManager.Accounts.Count <= 0)
            {
                new TextBoxLogCallback(this.txtLog).Log("No SMTP accounts added: please add one or more via Settings option");
                return;
            }
            foreach (ListViewItem listViewItem in this.listResults.CheckedItems)
            {
                IScrapeResult scrapeResult = listViewItem.Tag as IScrapeResult;
                if (scrapeResult != null && !scrapeResult.IsEmailSent && !string.IsNullOrEmpty(scrapeResult.GetEmail()) && !this.smtpTaskManager.Recipients.Contains(scrapeResult))
                {
                    this.smtpTaskManager.Recipients.Add(scrapeResult);
                    scrapeResult.IsQueued = true;
                }
            }
            this.smtpTaskManager.Start(true);
        }
        private void tsrbtnEditEmail_Click(object sender, EventArgs e)
        {
            if (new SettingsForm
            {
                tabControl1 =
                {
                    SelectedIndex = 2
                }
            }.ShowDialog() == DialogResult.OK)
            {
                this.InitSmtpTaskManager();
            }
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }
        private void tsrbtnExportItems_Click(object sender, EventArgs e)
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
                    foreach (ListViewItem listViewItem in this.listResults.Items)
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        for (int i = 0; i < listViewItem.SubItems.Count; i++)
                        {
                            stringBuilder.AppendFormat("\"{0}\"", listViewItem.SubItems[i].Text.Replace(Environment.NewLine, " ").Replace("\n", " ").Replace("\r", " ").Replace("\"", "\"\""));
                            if (i < listViewItem.SubItems.Count - 1)
                            {
                                stringBuilder.Append(",");
                            }
                        }
                        streamWriter.WriteLine(stringBuilder.ToString());
                    }
                }
            }
        }
        private void tsrbtnImportItems_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files files (*.csv)|*.csv";
            openFileDialog.CheckPathExists = true;
            openFileDialog.CheckFileExists = true;
            openFileDialog.AddExtension = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                bool flag = false;
                switch (this.cmbSite.SelectedIndex)
                {
                    case 0:
                        {
                            flag = true;
                            break;
                        }
                    case 1:
                        {
                            flag = true;
                            break;
                        }
                    case 2:
                        {
                            flag = true;
                            break;
                        }
                    case 3:
                        {
                            flag = true;
                            break;
                        }
                    case 4:
                        {
                            flag = false;
                            break;
                        }
                    case 5:
                        {
                            flag = false;
                            break;
                        }
                    case 6:
                        {
                            flag = false;
                            break;
                        }
                    default:
                        {
                            return;
                        }
                }
                if (flag)
                {
                    SimpleListViewAddCallback simpleListViewAddCallback = new SimpleListViewAddCallback(this.listResults, null, null);
                    SimpleListViewAddCallback.PrepareList(simpleListViewAddCallback);
                    using (StreamReader streamReader = new StreamReader(openFileDialog.FileName))
                    {
                        while (!streamReader.EndOfStream)
                        {
                            string s = streamReader.ReadLine();
                            List<string> list = CsvHelper.ParseLine(s);
                            SimpleScrapeResult simpleScrapeResult = new SimpleScrapeResult();
                            try
                            {
                                simpleScrapeResult.Category = list[0];
                                simpleScrapeResult.Location = list[1];
                                simpleScrapeResult.Headline = list[2];
                                simpleScrapeResult.Description = list[3];
                                simpleScrapeResult.Email = list[4];
                                simpleScrapeResult.Emails.AddRange(list[5].Split(new string[]
								{
									", "
								}, StringSplitOptions.RemoveEmptyEntries));
                                simpleScrapeResult.PhonesInBody.AddRange(list[6].Split(new string[]
								{
									", "
								}, StringSplitOptions.RemoveEmptyEntries));
                                simpleScrapeResult.DatePosted = list[7];
                                simpleScrapeResult.AdUrl = list[8];
                            }
                            catch
                            {
                            }
                            finally
                            {
                                simpleListViewAddCallback.Process(simpleScrapeResult);
                            }
                        }
                        return;
                    }
                }
                MapListViewAddCallback mapListViewAddCallback = new MapListViewAddCallback(this.listResults, null, null);
                MapListViewAddCallback.PrepareList(mapListViewAddCallback);
                using (StreamReader streamReader2 = new StreamReader(openFileDialog.FileName))
                {
                    while (!streamReader2.EndOfStream)
                    {
                        string s2 = streamReader2.ReadLine();
                        List<string> list2 = CsvHelper.ParseLine(s2);
                        MapScrapeResult mapScrapeResult = new MapScrapeResult();
                        try
                        {
                            mapScrapeResult.Category = list2[0];
                            mapScrapeResult.Radius = list2[1];
                            mapScrapeResult.Headline = list2[2];
                            mapScrapeResult.Address = list2[3];
                            mapScrapeResult.City = list2[4];
                            mapScrapeResult.Region = list2[5];
                            mapScrapeResult.ZipCode = list2[6];
                            mapScrapeResult.Phone = list2[7];
                            mapScrapeResult.Email = list2[8];
                            mapScrapeResult.Emails.AddRange(list2[9].Split(new string[]
							{
								", "
							}, StringSplitOptions.RemoveEmptyEntries));
                            mapScrapeResult.Website = list2[10];
                            mapScrapeResult.Latitude = list2[11];
                            mapScrapeResult.Longitude = list2[12];
                            mapScrapeResult.Map = list2[13];
                            mapScrapeResult.AdUrl = list2[14];
                        }
                        catch
                        {
                        }
                        finally
                        {
                            mapListViewAddCallback.Process(mapScrapeResult);
                        }
                    }
                }
            }
        }
        private void tsbAddCategory_Click(object sender, EventArgs e)
        {
            AddCategory addCategory = new AddCategory("Add category", string.Empty);
            if (addCategory.ShowDialog() == DialogResult.OK)
            {
                SearchCategory searchCategory = new SearchCategory();
                searchCategory.Name = (searchCategory.Url = addCategory.txtCategory.Text);
                TreeNode treeNode = this.treeCategories.Nodes.Add(searchCategory.Name);
                searchCategory.IsEmbedded = false;
                treeNode.Tag = searchCategory;
            }
        }
        private void tsbEditCategory_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.treeCategories.SelectedNode;
            if (selectedNode == null)
            {
                return;
            }
            SearchCategory searchCategory = selectedNode.Tag as SearchCategory;
            if (searchCategory == null)
            {
                return;
            }
            if (searchCategory.IsEmbedded)
            {
                return;
            }
            AddCategory addCategory = new AddCategory("Edit category", selectedNode.Text);
            if (addCategory.ShowDialog() == DialogResult.OK)
            {
                selectedNode.Text = addCategory.txtCategory.Text;
                if (selectedNode != null)
                {
                    searchCategory.Name = (searchCategory.Url = addCategory.txtCategory.Text);
                }
            }
        }
        private void tsbDeleteCategory_Click(object sender, EventArgs e)
        {
            if (this.treeCategories.SelectedNode != null)
            {
                this.treeCategories.SelectedNode.Remove();
            }
        }
        private void tsbAddLocation_Click(object sender, EventArgs e)
        {
            AddCategory addCategory = new AddCategory("Add location", string.Empty);
            if (addCategory.ShowDialog() == DialogResult.OK)
            {
                SearchCategory searchCategory = new SearchCategory();
                searchCategory.Name = (searchCategory.Url = addCategory.txtCategory.Text);
                TreeNode treeNode = this.treeLocations.Nodes.Add(searchCategory.Name);
                searchCategory.IsEmbedded = false;
                treeNode.Tag = searchCategory;
            }
        }
        private void tsbEditLocation_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.treeLocations.SelectedNode;
            if (selectedNode == null)
            {
                return;
            }
            SearchCategory searchCategory = selectedNode.Tag as SearchCategory;
            if (searchCategory == null)
            {
                return;
            }
            if (searchCategory.IsEmbedded)
            {
                return;
            }
            AddCategory addCategory = new AddCategory("Edit location", selectedNode.Text);
            if (addCategory.ShowDialog() == DialogResult.OK)
            {
                selectedNode.Text = addCategory.txtCategory.Text;
                if (selectedNode != null)
                {
                    searchCategory.Name = (searchCategory.Url = addCategory.txtCategory.Text);
                }
            }
        }
        private void treeCategories_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selectedNode = this.treeCategories.SelectedNode;
            if (selectedNode == null)
            {
                return;
            }
            SearchCategory searchCategory = selectedNode.Tag as SearchCategory;
            if (searchCategory == null)
            {
                return;
            }
            this.tsbDeleteCategory.Enabled = (this.tsbEditCategory.Enabled = !searchCategory.IsEmbedded);
        }
        private void treeLocations_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selectedNode = this.treeLocations.SelectedNode;
            if (selectedNode == null)
            {
                return;
            }
            SearchCategory searchCategory = selectedNode.Tag as SearchCategory;
            if (searchCategory == null)
            {
                return;
            }
            this.tsbDeleteLocation.Enabled = (this.tsbEditLocation.Enabled = !searchCategory.IsEmbedded);
        }
        private void tsbEmailSelectAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem listViewItem in this.listEmailTasks.Items)
            {
                listViewItem.Selected = true;
            }
            this.listEmailTasks.Focus();
        }
        private void tsbEmailClearSelection_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem listViewItem in this.listEmailTasks.Items)
            {
                listViewItem.Selected = false;
            }
            this.listEmailTasks.Focus();
        }
        private void tsbEmaiLStopQueue_Click(object sender, EventArgs e)
        {
            this.smtpTaskManager.Stop();
        }
        private void tsbEmailProcessQueue_Click(object sender, EventArgs e)
        {
            this.smtpTaskManager.SearchCampaign = this.campaign;
            this.smtpTaskManager.Start(false);
        }
        private void tsbEmailDeleteSelected_Click(object sender, EventArgs e)
        {
            while (this.listEmailTasks.SelectedItems.Count > 0)
            {
                ListViewItem listViewItem = this.listEmailTasks.SelectedItems[0];
                try
                {
                    KeyValuePair<IScrapeResult, SmtpItem> keyValuePair = (KeyValuePair<IScrapeResult, SmtpItem>)listViewItem.Tag;
                    this.smtpTaskManager.Recipients.Remove(keyValuePair.Key);
                    foreach (IScrapeResult current in this.campaign.Leads)
                    {
                        if (current == keyValuePair.Key)
                        {
                            current.IsQueued = false;
                        }
                    }
                }
                catch
                {
                }
                finally
                {
                    listViewItem.Remove();
                }
            }
        }
        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            Process.Start(this.toolStripLabel1.Text);
        }
        private void tsbClearLog_Click(object sender, EventArgs e)
        {
            this.txtLog.Clear();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            base.Close();
        }
        private void tsbDeleteLocation_Click(object sender, EventArgs e)
        {
            if (this.treeLocations.SelectedNode != null)
            {
                this.treeLocations.SelectedNode.Remove();
            }
        }
        private void listResults_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            bool flag = true;
            if (this.listResults.Tag != null)
            {
                flag = !(bool)this.listResults.Tag;
            }
            this.listResults.Tag = flag;
            this.listResults.ListViewItemSorter = new ListViewItemComparer(e.Column, flag);
        }
        private NetResult CheckUpdate()
        {
            NetResult result;
            try
            {
                string text = UrlDownloader.Get(Resources.LatestVersionUrl);
                if (text.Trim() == string.Empty)
                {
                    result = NetResult.Failed;
                }
                else
                {
                    string[] array = text.Split(new char[]
					{
						'.'
					});
                    if (array.Length != 4)
                    {
                        result = NetResult.Failed;
                    }
                    else
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            int num;
                            if (!int.TryParse(array[i], out num))
                            {
                                result = NetResult.Failed;
                                return result;
                            }
                        }
                        FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
                        if (text == versionInfo.ProductVersion)
                        {
                            result = NetResult.Success;
                        }
                        else
                        {
                            result = NetResult.Failed;
                        }
                    }
                }
            }
            catch
            {
                result = NetResult.ServerDown;
            }
            return result;
        }
        private void checkForToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (this.CheckUpdate())
            {
                case NetResult.Success:
                    {
                        MessageBox.Show("Your software is up to date", "Update", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                    }
                case NetResult.ServerDown:
                    {
                        MessageBox.Show("Cannot connect to the update server, Automatic Updates are Activated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;
                    }
                case NetResult.Failed:
                    {
                        if (MessageBox.Show("The update is available. Open download page?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                        {
                            Process.Start(Resources.UpdateUrl);
                            return;
                        }
                        break;
                    }
                default:
                    {
                        return;
                    }
            }
        }
        private void listResults_CausesValidationChanged(object sender, EventArgs e)
        {
            this.tslItemCount.Text = string.Format("{0} items listed, {1} selected", this.listResults.Items.Count, this.listResults.CheckedItems.Count);
        }
        private void listResults_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            this.listResults_CausesValidationChanged(sender, e);
            IScrapeResult scrapeResult = e.Item.Tag as IScrapeResult;
            if (scrapeResult != null)
            {
                scrapeResult.IsSelected = e.Item.Checked;
            }
        }
        private void saveCampaignToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "ConsultingLeadsPro Profiles (*.plc)|*.plc";
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.AddExtension = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK && this.campaign != null)
            {
                this.campaign.Save(saveFileDialog.FileName);
            }
        }
        private void loadCampaignToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.StopScraping();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "ConsultingLeadsPro Profiles (*.plc)|*.plc";
            openFileDialog.CheckPathExists = true;
            openFileDialog.CheckFileExists = true;
            openFileDialog.AddExtension = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.campaign = Campaign.Load(openFileDialog.FileName);
                bool flag = false;
                this.cmbSite.SelectedIndex = this.campaign.SearchEngine;
                switch (this.campaign.SearchEngine)
                {
                    case 0:
                        {
                            flag = true;
                            break;
                        }
                    case 1:
                        {
                            flag = true;
                            break;
                        }
                    case 2:
                        {
                            flag = true;
                            break;
                        }
                    case 3:
                        {
                            flag = true;
                            break;
                        }
                    case 4:
                        {
                            flag = false;
                            break;
                        }
                    case 5:
                        {
                            flag = false;
                            break;
                        }
                    case 6:
                        {
                            flag = false;
                            break;
                        }
                    default:
                        {
                            return;
                        }
                }
                ScrapeResultCallback scrapeResultCallback = null;
                if (flag)
                {
                    scrapeResultCallback = new SimpleListViewAddCallback(this.listResults, null, null);
                    SimpleListViewAddCallback.PrepareList(scrapeResultCallback);
                }
                else
                {
                    scrapeResultCallback = new MapListViewAddCallback(this.listResults, null, null);
                    MapListViewAddCallback.PrepareList(scrapeResultCallback);
                }
                this.smtpTaskManager.Recipients.Clear();
                this.InitSmtpTaskManager();
                this.listEmailTasks.Items.Clear();
                foreach (IScrapeResult current in this.campaign.Leads)
                {
                    scrapeResultCallback.Process(current);
                    if (current.IsQueued && !string.IsNullOrEmpty(current.GetEmail()) && !this.smtpTaskManager.Recipients.Contains(current))
                    {
                        this.smtpTaskManager.Recipients.Add(current);
                    }
                }
                if (this.smtpTaskManager.Accounts.Count <= 0)
                {
                    new TextBoxLogCallback(this.txtLog).Log("No SMTP accounts added: please add one or more via Settings option");
                    return;
                }
                this.smtpTaskManager.Start(true);
                this.isCampaignLoaded = true;
            }
        }
        private void txtLog_TextChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem listViewItem in this.listResults.Items)
            {
                IScrapeResult scrapeResult = listViewItem.Tag as IScrapeResult;
                if (scrapeResult != null)
                {
                    if (scrapeResult.IsEmailSent)
                    {
                        IEnumerator enumerator2 = listViewItem.SubItems.GetEnumerator();
                        try
                        {
                            while (enumerator2.MoveNext())
                            {
                                ListViewItem.ListViewSubItem listViewSubItem = (ListViewItem.ListViewSubItem)enumerator2.Current;
                                listViewSubItem.BackColor = Color.GreenYellow;
                            }
                            continue;
                        }
                        finally
                        {
                            IDisposable disposable = enumerator2 as IDisposable;
                            if (disposable != null)
                            {
                                disposable.Dispose();
                            }
                        }
                    }
                    foreach (ListViewItem.ListViewSubItem listViewSubItem2 in listViewItem.SubItems)
                    {
                        listViewSubItem2.BackColor = this.listResults.BackColor;
                    }
                }
            }
        }
        private void campaignInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CampaignInfoForm campaignInfoForm = new CampaignInfoForm(this.campaign);
            campaignInfoForm.ShowDialog();
        }
        private void tsrbtnDeleteSent_Click(object sender, EventArgs e)
        {
            int i = 0;
            while (i < this.listResults.Items.Count)
            {
                ListViewItem listViewItem = this.listResults.Items[i];
                IScrapeResult scrapeResult = listViewItem.Tag as IScrapeResult;
                if (scrapeResult != null)
                {
                    if (scrapeResult.IsEmailSent)
                    {
                        listViewItem.Remove();
                        this.campaign.Leads.Remove(scrapeResult);
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }
    }
}
