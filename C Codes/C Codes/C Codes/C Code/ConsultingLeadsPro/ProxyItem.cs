using System;
namespace ConsultingLeadsPro
{
	[Serializable]
	public class ProxyItem
	{
		public string IP
		{
			get;
			set;
		}
		public int Port
		{
			get;
			set;
		}
		public string Type
		{
			get;
			set;
		}
		public string Country
		{
			get;
			set;
		}
		public bool NeedAuth
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
	}
}
