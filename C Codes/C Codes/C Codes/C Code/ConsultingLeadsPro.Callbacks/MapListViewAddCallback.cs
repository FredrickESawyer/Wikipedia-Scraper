using ConsultingLeadsPro.Scrapers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
namespace ConsultingLeadsPro.Callbacks
{
	internal class MapListViewAddCallback : TsProgressCallback
	{
		public MapListViewAddCallback(ListView listView, ToolStripLabel label, ToolStripProgressBar progressBar) : base(listView, label, progressBar)
		{
		}
		public override void ShowProgress()
		{
			this.Step();
		}
		public override void Finish()
		{
			this.StopProgress("Status: Ready");
		}
		public static void PrepareList(ScrapeResultCallback src)
		{
			ScrapeResultCallback.PrepareList(src, new List<string>
			{
				"Category", 
				"Radius", 
				"Headline", 
				"Address", 
				"City", 
				"Region", 
				"Zip code", 
				"Phone", 
				"Email", 
				"Emails from website", 
				"Website", 
				"Latitude", 
				"Longitude", 
				"Map", 
				"Ad URL"
			});
		}
		public override void Init(int workCount)
		{
			MapListViewAddCallback.PrepareList(this);
			this.InitProgressBar(workCount, "Search status:");
		}
		public override void Process(IScrapeResult scr)
		{
			MapScrapeResult mapScrapeResult = scr as MapScrapeResult;
			if (mapScrapeResult == null)
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
							mapScrapeResult
						});
						return;
					}
					catch
					{
						return;
					}
				}
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < mapScrapeResult.Emails.Count; i++)
				{
					stringBuilder.Append(mapScrapeResult.Emails[i]);
					if (i < mapScrapeResult.Emails.Count - 1)
					{
						stringBuilder.Append(", ");
					}
				}
				ListViewItem listViewItem = new ListViewItem(new string[]
				{
					mapScrapeResult.Category, 
					mapScrapeResult.Radius, 
					mapScrapeResult.Headline, 
					mapScrapeResult.Address, 
					mapScrapeResult.City, 
					mapScrapeResult.Region, 
					mapScrapeResult.ZipCode, 
					mapScrapeResult.Phone, 
					mapScrapeResult.Email, 
					stringBuilder.ToString(), 
					mapScrapeResult.Website, 
					mapScrapeResult.Latitude, 
					mapScrapeResult.Longitude, 
					mapScrapeResult.Map, 
					mapScrapeResult.AdUrl
				});
				listViewItem.Tag = mapScrapeResult;
				if (mapScrapeResult.IsEmailSent)
				{
					foreach (ListViewItem.ListViewSubItem listViewSubItem in listViewItem.SubItems)
					{
						listViewSubItem.BackColor = Color.GreenYellow;
					}
				}
				listViewItem.Checked = mapScrapeResult.IsSelected;
				Font font = new Font(listViewItem.SubItems[8].Font, listViewItem.SubItems[8].Font.Style | FontStyle.Underline);
				this.listView.Items.Add(listViewItem);
				listViewItem.UseItemStyleForSubItems = false;
				listViewItem.SubItems[10].Font = (listViewItem.SubItems[13].Font = (listViewItem.SubItems[14].Font = font));
				listViewItem.SubItems[10].ForeColor = (listViewItem.SubItems[13].ForeColor = (listViewItem.SubItems[14].ForeColor = Color.FromArgb(0, 0, 0, 255)));
				this.listView.CausesValidation = !this.listView.CausesValidation;
			}
		}
	}
}
