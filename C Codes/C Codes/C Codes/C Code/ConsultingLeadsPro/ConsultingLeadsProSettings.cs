using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace ConsultingLeadsPro
{
	[Serializable]
	public class ConsultingLeadsProSettings
	{
		public enum DeepSearchOptions
		{
			SearchIfNoEmails,
			SearchAlways,
			SearchNever
		}
		public enum Duplicates
		{
			Ignore,
			Email,
			Headline,
			EmailHeadline
		}
		protected List<SmtpItem> smtpSettings = new List<SmtpItem>();
		protected ProxySettings proxySettings = new ProxySettings();
		protected string fileName;
		public List<SmtpItem> SmtpSettings
		{
			get
			{
				return this.smtpSettings;
			}
			set
			{
			}
		}
		public ProxySettings ProxySetting
		{
			get
			{
				return this.proxySettings;
			}
			set
			{
			}
		}
		public string MessageBody
		{
			get;
			set;
		}
		public string MessageSubject
		{
			get;
			set;
		}
		public string TestEmail
		{
			get;
			set;
		}
		public string UserName
		{
			get;
			set;
		}
		public string Serial
		{
			get;
			set;
		}
		public bool Activated
		{
			get;
			set;
		}
		public ConsultingLeadsProSettings.DeepSearchOptions DeepSearch
		{
			get;
			set;
		}
		public Guid ProxyListGuid
		{
			get;
			set;
		}
		public ConsultingLeadsProSettings.Duplicates DuplicateProcess
		{
			get;
			set;
		}
		public decimal NumberOfSendings
		{
			get;
			set;
		}
		public ConsultingLeadsProSettings()
		{
			this.fileName = string.Empty;
		}
		public ConsultingLeadsProSettings(string fileName)
		{
			this.fileName = fileName;
		}
		public void Save()
		{
			using (Stream stream = File.Open(this.fileName, FileMode.Create, FileAccess.Write))
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(stream, this);
				stream.Close();
			}
		}
		public static ConsultingLeadsProSettings Load(string fileName)
		{
			ConsultingLeadsProSettings ConsultingLeadsProSettings = null;
			try
			{
				using (Stream stream = File.Open(fileName, FileMode.OpenOrCreate, FileAccess.Read))
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					ConsultingLeadsProSettings = (binaryFormatter.Deserialize(stream) as ConsultingLeadsProSettings);
					ConsultingLeadsProSettings.fileName = fileName;
					stream.Close();
				}
			}
			catch
			{
			}
			if (ConsultingLeadsProSettings == null)
			{
				return new ConsultingLeadsProSettings(fileName);
			}
			return ConsultingLeadsProSettings;
		}
	}
}
