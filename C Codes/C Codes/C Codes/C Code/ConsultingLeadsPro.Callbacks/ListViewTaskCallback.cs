using ConsultingLeadsPro.Scrapers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
namespace ConsultingLeadsPro.Callbacks
{
	internal class ListViewTaskCallback : EmailSendingCallback
	{
		protected delegate void AddTaskDelegate(KeyValuePair<IScrapeResult, SmtpItem> sender, string status, DateTime schedule);
		protected delegate void EmailSentDelegate(KeyValuePair<IScrapeResult, SmtpItem> sender);
		protected delegate void InitDelegate();
		protected ListView listView;
		public ListViewTaskCallback(ListView listView)
		{
			this.listView = listView;
		}
		public void Init()
		{
			if (this.listView.InvokeRequired)
			{
				ListViewTaskCallback.InitDelegate method = new ListViewTaskCallback.InitDelegate(this.Init);
				this.listView.Invoke(method);
				return;
			}
			this.listView.Items.Clear();
		}
		public void AddTask(KeyValuePair<IScrapeResult, SmtpItem> sender, string status, DateTime schedule)
		{
			if (this.listView.InvokeRequired)
			{
				ListViewTaskCallback.AddTaskDelegate method = new ListViewTaskCallback.AddTaskDelegate(this.AddTask);
				this.listView.Invoke(method, new object[]
				{
					sender, 
					status, 
					schedule
				});
				return;
			}
			ListViewItem listViewItem = new ListViewItem(new string[]
			{
				sender.Value.EmailAddress, 
				sender.Key.GetEmail(), 
				schedule.ToString(), 
				status
			});
			listViewItem.Tag = sender;
			listViewItem.SubItems[2].Tag = schedule;
			this.listView.Items.Add(listViewItem);
			this.listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
		}
		public void EmailSent(KeyValuePair<IScrapeResult, SmtpItem> sender)
		{
			if (this.listView == null)
			{
				return;
			}
			if (this.listView.InvokeRequired)
			{
				ListViewTaskCallback.EmailSentDelegate method = new ListViewTaskCallback.EmailSentDelegate(this.EmailSent);
				this.listView.Invoke(method, new object[]
				{
					sender
				});
				return;
			}
			ListViewItem listViewItem = null;
			foreach (ListViewItem listViewItem2 in this.listView.Items)
			{
				KeyValuePair<IScrapeResult, SmtpItem> keyValuePair;
				try
				{
					keyValuePair = (KeyValuePair<IScrapeResult, SmtpItem>)listViewItem2.Tag;
				}
				catch
				{
					continue;
				}
				if (keyValuePair.Key == sender.Key && keyValuePair.Value == sender.Value)
				{
					listViewItem = listViewItem2;
					break;
				}
			}
			if (listViewItem != null)
			{
				listViewItem.Remove();
			}
		}
	}
}
