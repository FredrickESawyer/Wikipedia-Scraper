using ConsultingLeadsPro.Scrapers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace ConsultingLeadsPro
{
	[Serializable]
	public class Campaign
	{
		[Serializable]
		public class SentEmail
		{
			public string Email
			{
				get;
				set;
			}
			public DateTime Date
			{
				get;
				set;
			}
			public SentEmail(string email, DateTime date)
			{
				this.Email = email;
				this.Date = date;
			}
		}
		protected List<IScrapeResult> leads = new List<IScrapeResult>();
		protected List<Campaign.SentEmail> sentEmails = new List<Campaign.SentEmail>();
		public int SearchEngine
		{
			get;
			set;
		}
		public List<IScrapeResult> Leads
		{
			get
			{
				return this.leads;
			}
			set
			{
				this.leads = value;
			}
		}
		public List<Campaign.SentEmail> SentEmails
		{
			get
			{
				return this.sentEmails;
			}
			set
			{
				this.sentEmails = value;
			}
		}
		public void Save(string fileName)
		{
			using (Stream stream = File.Open(fileName, FileMode.Create, FileAccess.Write))
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(stream, this);
				stream.Close();
			}
		}
		public static Campaign Load(string fileName)
		{
			Campaign result = null;
			try
			{
				using (Stream stream = File.Open(fileName, FileMode.OpenOrCreate, FileAccess.Read))
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					result = (binaryFormatter.Deserialize(stream) as Campaign);
					stream.Close();
				}
			}
			catch
			{
			}
			return result;
		}
	}
}
