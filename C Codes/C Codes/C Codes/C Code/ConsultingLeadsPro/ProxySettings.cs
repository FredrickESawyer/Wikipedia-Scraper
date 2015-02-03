using System;
using System.Collections.Generic;
using System.Net;
using System.Xml.Serialization;
namespace ConsultingLeadsPro
{
	[XmlRoot("ProxySettings", IsNullable = false)]
	[Serializable]
	public class ProxySettings
	{
		public enum UseType
		{
			DontUse,
			UseRandom,
			Use
		}
		protected List<ProxyItem> proxyItems = new List<ProxyItem>();
		public List<ProxyItem> ProxyItems
		{
			get
			{
				return this.proxyItems;
			}
		}
		public ProxySettings.UseType ProxyUse
		{
			get;
			set;
		}
		public string Host
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
		public WebProxy GetProxy()
		{
			WebProxy webProxy = null;
			switch (this.ProxyUse)
			{
				case ProxySettings.UseType.UseRandom:
				{
					if (this.ProxyItems.Count > 0)
					{
						ProxyItem proxyItem = this.ProxyItems[new Random().Next(this.ProxyItems.Count)];
						webProxy = new WebProxy(proxyItem.IP, proxyItem.Port);
						if (proxyItem.NeedAuth)
						{
							webProxy.Credentials = new NetworkCredential(proxyItem.Username, proxyItem.Password);
						}
					}
					break;
				}
				case ProxySettings.UseType.Use:
				{
					webProxy = new WebProxy(this.Host, this.Port);
					if (this.NeedsAuth)
					{
						webProxy.Credentials = new NetworkCredential(this.Username, this.Password);
					}
					break;
				}
			}
			return webProxy;
		}
	}
}
