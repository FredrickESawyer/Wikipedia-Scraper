using System;
using System.Collections.Generic;
using System.Linq;
namespace ConsultingLeadsPro.Scrapers
{
	[Serializable]
	internal class MapScrapeResult : IScrapeResult
	{
		protected List<string> emails = new List<string>();
		public string Category
		{
			get;
			set;
		}
		public string Radius
		{
			get;
			set;
		}
		public string Headline
		{
			get;
			set;
		}
		public string Address
		{
			get;
			set;
		}
		public string City
		{
			get;
			set;
		}
		public string Region
		{
			get;
			set;
		}
		public string Country
		{
			get;
			set;
		}
		public string ZipCode
		{
			get;
			set;
		}
		public string Phone
		{
			get;
			set;
		}
		public string Email
		{
			get;
			set;
		}
		public string Website
		{
			get;
			set;
		}
		public string Latitude
		{
			get;
			set;
		}
		public string Longitude
		{
			get;
			set;
		}
		public string Map
		{
			get;
			set;
		}
		public string AdUrl
		{
			get;
			set;
		}
		public List<string> Emails
		{
			get
			{
				return this.emails;
			}
			set
			{
				this.emails = value;
			}
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
		public MapScrapeResult()
		{
			this.Category = string.Empty;
			this.Radius = string.Empty;
			this.Headline = string.Empty;
			this.Address = string.Empty;
			this.City = string.Empty;
			this.Region = string.Empty;
			this.Country = string.Empty;
			this.ZipCode = string.Empty;
			this.Phone = string.Empty;
			this.Email = string.Empty;
			this.Website = string.Empty;
			this.Latitude = string.Empty;
			this.Longitude = string.Empty;
			this.Map = string.Empty;
			this.AdUrl = string.Empty;
		}
		public string GetEmail()
		{
			if (!string.IsNullOrEmpty(this.Email))
			{
				return this.Email;
			}
			return this.emails.FirstOrDefault<string>();
		}
	}
}
