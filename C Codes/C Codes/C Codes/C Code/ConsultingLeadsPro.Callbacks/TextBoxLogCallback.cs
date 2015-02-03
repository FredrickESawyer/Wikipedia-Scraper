using System;
using System.Windows.Forms;
namespace ConsultingLeadsPro.Callbacks
{
	public class TextBoxLogCallback : LogCallback
	{
		protected delegate void StringDelegate(string text);
		protected TextBox textBox;
		public TextBoxLogCallback(TextBox textBox)
		{
			this.textBox = textBox;
		}
		public void Log(string text, bool appendNewLine)
		{
			if (this.textBox == null)
			{
				return;
			}
			if (this.textBox.InvokeRequired)
			{
				TextBoxLogCallback.StringDelegate method = new TextBoxLogCallback.StringDelegate(this.Log);
				this.textBox.Invoke(method, new object[]
				{
					text
				});
				return;
			}
			TextBox expr_43 = this.textBox;
			expr_43.Text += string.Format("{0}\t{1}{2}", DateTime.Now, text, appendNewLine ? Environment.NewLine : string.Empty);
			this.textBox.SelectionStart = this.textBox.Text.Length;
			this.textBox.ScrollToCaret();
			this.textBox.Refresh();
		}
		public void Log(string text)
		{
			this.Log(text, true);
		}
	}
}
