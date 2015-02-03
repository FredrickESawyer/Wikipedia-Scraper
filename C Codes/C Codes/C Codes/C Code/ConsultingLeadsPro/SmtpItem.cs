using System;
namespace ConsultingLeadsPro
{
	[Serializable]
	public class SmtpItem
	{
		public bool Checked
		{
			get;
			set;
		}
		public string EmailAddress
		{
			get;
			set;
		}
		public string SenderName
		{
			get;
			set;
		}
		public string SmtpServer
		{
			get;
			set;
		}
		public int Port
		{
			get;
			set;
		}
		public bool NeedsAuth
		{
			get;
			set;
		}
		public string Username
		{
			get;
			set;
		}
		public string Password
		{
			get;
			set;
		}
		public bool UseSsl
		{
			get;
			set;
		}
		public SmtpItem()
		{
			this.EmailAddress = (this.SenderName = (this.SmtpServer = (this.Username = (this.Password = string.Empty))));
			this.Port = 25;
			this.Checked = (this.NeedsAuth = (this.UseSsl = false));
		}
	}
}
