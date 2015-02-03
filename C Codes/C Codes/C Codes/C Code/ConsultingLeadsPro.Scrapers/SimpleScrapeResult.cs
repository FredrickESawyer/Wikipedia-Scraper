using System;
using System.Collections.Generic;
using System.Linq;
namespace ConsultingLeadsPro.Scrapers
{
	[Serializable]
	internal class SimpleScrapeResult : IScrapeResult
	{
		protected List<string> emailsInBody = new List<string>();
		protected List<string> phonesInBody = new List<string>();
		public string Category
		{
			get;
			set;
		}
		public string Location
		{
			get;
			set;
		}
		public string Headline
		{
			get;
			set;
		}
		public string Description
		{
			get;
			set;
		}
		public string Email
		{
			get;
			set;
		}
		public List<string> Emails
		{
			get
			{
				return this.emailsInBody;
			}
			set
			{
				this.emailsInBody = value;
			}
		}
		public List<string> PhonesInBody
		{
			get
			{
				return this.phonesInBody;
			}
			set
			{
				this.phonesInBody = value;
			}
		}
		public string DatePosted
		{
			get;
			set;
		}
		public string AdUrl
		{
			get;
			set;
		}
		public bool IsEmailSent
		{
			get;
			set;
		}
		public bool IsSelected
		{
			get;
			set;
		}
		public bool IsQueued
		{
			get;
			set;
		}
		public SimpleScrapeResult()
		{
			this.Category = string.Empty;
			this.Location = string.Empty;
			this.Headline = string.Empty;
			this.Description = string.Empty;
			this.Email = string.Empty;
			this.DatePosted = string.Empty;
			this.AdUrl = string.Empty;
		}
		public string GetEmail()
		{
			if (!string.IsNullOrEmpty(this.Email))
			{
				return this.Email;
			}
			return this.emailsInBody.FirstOrDefault<string>();
		}
	}
}
