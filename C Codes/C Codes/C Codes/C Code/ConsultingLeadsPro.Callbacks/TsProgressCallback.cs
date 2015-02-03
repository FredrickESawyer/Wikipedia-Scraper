using System;
using System.Windows.Forms;
namespace ConsultingLeadsPro.Callbacks
{
	internal abstract class TsProgressCallback : ScrapeResultCallback
	{
		protected ToolStripLabel label;
		protected ToolStripProgressBar progressBar;
		public TsProgressCallback(ListView listView, ToolStripLabel label, ToolStripProgressBar progressBar) : base(listView)
		{
			this.label = label;
			this.progressBar = progressBar;
		}
		public virtual void InitProgressBar(int max, string text)
		{
			if (this.progressBar != null && this.progressBar.Owner != null)
			{
				if (this.progressBar.Owner.InvokeRequired)
				{
					this.progressBar.Owner.Invoke(new MethodInvoker(delegate
					{
						this.progressBar.Visible = true;
						this.progressBar.Step = 1;
						this.progressBar.Minimum = 0;
						this.progressBar.Maximum = max;
						this.progressBar.Value = 0;
					}
					));
				}
				else
				{
					this.progressBar.Visible = true;
					this.progressBar.Step = 1;
					this.progressBar.Minimum = 0;
					this.progressBar.Maximum = max;
					this.progressBar.Value = 0;
				}
			}
			if (this.label != null && this.label.Owner != null)
			{
				if (this.label.Owner.InvokeRequired)
				{
					this.label.Owner.Invoke(new MethodInvoker(delegate
					{
						this.label.Text = text;
					}
					));
					return;
				}
				this.label.Text = text;
			}
		}
		public virtual void StopProgress(string text)
		{
			if (this.progressBar != null && this.progressBar.Owner != null)
			{
				if (this.progressBar.Owner.InvokeRequired)
				{
					this.progressBar.Owner.Invoke(new MethodInvoker(delegate
					{
						this.progressBar.Visible = false;
					}
					));
				}
				else
				{
					this.progressBar.Visible = false;
				}
			}
			if (this.label != null && this.label.Owner != null)
			{
				if (this.label.Owner.InvokeRequired)
				{
					this.label.Owner.Invoke(new MethodInvoker(delegate
					{
						this.label.Text = text;
					}
					));
					return;
				}
				this.label.Text = text;
			}
		}
        public virtual void Step()
        {
            if (this.progressBar != null)
            {
                if (this.progressBar.Owner == null)
                {
                    return;
                }
                if (this.progressBar.Owner.InvokeRequired)
                {
                    this.progressBar.Owner.Invoke(new MethodInvoker(delegate { Step(); }));
                    return;
                }
                this.progressBar.PerformStep();
            }
        }
    }
}