using System;
using System.Collections;
using System.Windows.Forms;
namespace ConsultingLeadsPro.Helpers
{
	internal class ListViewItemComparer : IComparer
	{
		private int col;
		private bool asc;
		public ListViewItemComparer()
		{
			this.col = 0;
		}
		public ListViewItemComparer(int column, bool asc)
		{
			this.col = column;
			this.asc = asc;
		}
		public int Compare(object x, object y)
		{
			if (!this.asc)
			{
				return -string.Compare(((ListViewItem)x).SubItems[this.col].Text, ((ListViewItem)y).SubItems[this.col].Text);
			}
			return string.Compare(((ListViewItem)x).SubItems[this.col].Text, ((ListViewItem)y).SubItems[this.col].Text);
		}
	}
}
