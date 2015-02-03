using System;
using System.Collections;
using System.Windows.Forms;
namespace ConsultingLeadsPro.Helpers
{
	internal class ListViewItemDateComparer : IComparer
	{
		private int col;
		private bool asc;
		public ListViewItemDateComparer()
		{
			this.col = 0;
		}
		public ListViewItemDateComparer(int column, bool asc)
		{
			this.col = column;
			this.asc = asc;
		}
		public int Compare(object x, object y)
		{
			DateTime dateTime;
			try
			{
				dateTime = (DateTime)((ListViewItem)x).SubItems[this.col].Tag;
			}
			catch
			{
				dateTime = DateTime.Now;
			}
			DateTime dateTime2;
			try
			{
				dateTime2 = (DateTime)((ListViewItem)y).SubItems[this.col].Tag;
			}
			catch
			{
				dateTime2 = DateTime.Now;
			}
			int num = (dateTime == dateTime2) ? 0 : ((dateTime > dateTime2) ? 1 : -1);
			if (!this.asc)
			{
				return -num;
			}
			return num;
		}
	}
}
