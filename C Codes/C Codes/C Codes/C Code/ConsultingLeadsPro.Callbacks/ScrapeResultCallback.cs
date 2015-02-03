using ConsultingLeadsPro.Scrapers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
namespace ConsultingLeadsPro.Callbacks
{
	internal abstract class ScrapeResultCallback
	{
		protected delegate void InitDelegate(int workCount);
		protected delegate void PrepareListDelegate(ScrapeResultCallback t, List<string> columns);
		protected delegate void ProcessDelegate(IScrapeResult scr);
		protected ListView listView;
		public ScrapeResultCallback(ListView listView)
		{
			this.listView = listView;
		}
		public static void PrepareList(ScrapeResultCallback t, List<string> columns)
		{
			if (t.listView != null)
			{
				if (t.listView.InvokeRequired)
				{
					ScrapeResultCallback.PrepareListDelegate method = new ScrapeResultCallback.PrepareListDelegate(ScrapeResultCallback.PrepareList);
					t.listView.Invoke(method, new object[]
					{
						t, 
						columns
					});
					return;
				}
				t.listView.Items.Clear();
				t.listView.Columns.Clear();
				foreach (string current in columns)
				{
					t.listView.Columns.Add(current);
				}
				t.listView.CausesValidation = !t.listView.CausesValidation;
			}
		}
		public abstract void Process(IScrapeResult scr);
		public abstract void Init(int workCount);
		public abstract void Finish();
		public abstract void ShowProgress();
	}
}
