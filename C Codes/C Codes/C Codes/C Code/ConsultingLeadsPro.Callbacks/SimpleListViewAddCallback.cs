using ConsultingLeadsPro.Network;
using ConsultingLeadsPro.Scrapers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
namespace ConsultingLeadsPro.Callbacks
{
	internal class SimpleListViewAddCallback : TsProgressCallback
	{
		public SimpleListViewAddCallback(ListView listView, ToolStripLabel label, ToolStripProgressBar progressBar) : base(listView, label, progressBar)
		{
		}
		public override void Finish()
		{
			this.StopProgress("Status: Ready");
		}
		public override void ShowProgress()
		{
			this.Step();
		}
		public static void PrepareList(ScrapeResultCallback src)
		{
			ScrapeResultCallback.PrepareList(src, new List<string>
			{
				"Category", 
				"Location", 
				"Headline", 
				"Description", 
				"Email", 
				"Emails in body", 
				"Phones in body", 
				"Date posted", 
				"Ad URL"
			});
		}
		public override void Init(int workCount)
		{
			SimpleListViewAddCallback.PrepareList(this);
			this.InitProgressBar(workCount, "Search status:");
		}
		public override void Process(IScrapeResult scr)
		{
			SimpleScrapeResult simpleScrapeResult = scr as SimpleScrapeResult;
			if (simpleScrapeResult == null)
			{
				return;
			}
			if (this.listView != null)
			{
				if (this.listView.InvokeRequired)
				{
					ScrapeResultCallback.ProcessDelegate method = new ScrapeResultCallback.ProcessDelegate(this.Process);
					try
					{
						this.listView.Invoke(method, new object[]
						{
							simpleScrapeResult
						});
						return;
					}
					catch
					{
						return;
					}
				}
				string text = string.Empty;
				for (int i = 0; i < simpleScrapeResult.Emails.Count; i++)
				{
					text += simpleScrapeResult.Emails[i];
					if (i < simpleScrapeResult.Emails.Count - 1)
					{
						text += ", ";
					}
				}
				string text2 = string.Empty;
				for (int j = 0; j < simpleScrapeResult.PhonesInBody.Count; j++)
				{
					text2 += simpleScrapeResult.PhonesInBody[j];
					if (j < simpleScrapeResult.PhonesInBody.Count - 1)
					{
						text2 += ", ";
					}
				}
				ListViewItem listViewItem = new ListViewItem(new string[]
				{
					simpleScrapeResult.Category, 
					simpleScrapeResult.Location, 
					simpleScrapeResult.Headline, 
					UrlDownloader.SkipHtmlTags(simpleScrapeResult.Description), 
					simpleScrapeResult.Email, 
					text, 
					text2, 
					simpleScrapeResult.DatePosted, 
					simpleScrapeResult.AdUrl
				});
				listViewItem.Tag = simpleScrapeResult;
				if (simpleScrapeResult.IsEmailSent)
				{
					foreach (ListViewItem.ListViewSubItem listViewSubItem in listViewItem.SubItems)
					{
						listViewSubItem.BackColor = Color.GreenYellow;
					}
				}
				listViewItem.Checked = simpleScrapeResult.IsSelected;
				Font font = new Font(listViewItem.SubItems[8].Font, listViewItem.SubItems[8].Font.Style | FontStyle.Underline);
				this.listView.Items.Add(listViewItem);
				listViewItem.UseItemStyleForSubItems = false;
				listViewItem.SubItems[8].Font = font;
				listViewItem.SubItems[8].ForeColor = Color.FromArgb(0, 0, 0, 255);
				this.listView.CausesValidation = !this.listView.CausesValidation;
			}
		}
	}
}
