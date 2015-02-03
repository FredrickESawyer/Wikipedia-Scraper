using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace ConsultingLeadsPro
{
	public class CampaignInfoForm : Form
	{
		private Campaign campaign;
		private IContainer components;
		private ListView listEmails;
		private ColumnHeader columnHeader1;
		private ColumnHeader columnHeader2;
		private Button btnOk;
		private Label lblTotalSent;
		public CampaignInfoForm(Campaign campaign)
		{
			this.campaign = campaign;
			this.InitializeComponent();
		}
		private void CampaignInfoForm_Load(object sender, EventArgs e)
		{
			if (this.campaign != null)
			{
				foreach (Campaign.SentEmail current in this.campaign.SentEmails)
				{
					this.listEmails.Items.Add(new ListViewItem(new string[]
					{
						current.Email, 
						current.Date.ToString()
					}));
				}
			}
			this.lblTotalSent.Text = string.Format("Emails sent: {0}", this.listEmails.Items.Count);
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
			this.listEmails = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.btnOk = new Button();
			this.lblTotalSent = new Label();
			base.SuspendLayout();
			this.listEmails.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader1, 
				this.columnHeader2
			});
			this.listEmails.Location = new Point(12, 25);
			this.listEmails.Name = "listEmails";
			this.listEmails.Size = new Size(446, 187);
			this.listEmails.TabIndex = 0;
			this.listEmails.UseCompatibleStateImageBehavior = false;
			this.listEmails.View = View.Details;
			this.columnHeader1.Text = "Email address";
			this.columnHeader1.Width = 180;
			this.columnHeader2.Text = "Sent";
			this.columnHeader2.Width = 223;
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new Point(383, 218);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new Size(75, 23);
			this.btnOk.TabIndex = 1;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.lblTotalSent.AutoSize = true;
			this.lblTotalSent.Location = new Point(12, 9);
			this.lblTotalSent.Name = "lblTotalSent";
			this.lblTotalSent.Size = new Size(63, 13);
			this.lblTotalSent.TabIndex = 2;
			this.lblTotalSent.Text = "Emails sent:";
			base.AcceptButton = this.btnOk;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new Size(470, 253);
			base.Controls.Add(this.lblTotalSent);
			base.Controls.Add(this.btnOk);
			base.Controls.Add(this.listEmails);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CampaignInfoForm";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Campaign info";
			base.Load += new EventHandler(this.CampaignInfoForm_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
