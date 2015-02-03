using ConsultingLeadsPro.Properties;
using System;
using System.IO;
namespace ConsultingLeadsPro
{
	public sealed class CommonData
	{
		private static readonly CommonData _instance = new CommonData();
		public readonly string ApplicationFolderPath;
		public readonly ConsultingLeadsProSettings Settings;
		public static CommonData Instance
		{
			get
			{
				return CommonData._instance;
			}
		}
		private CommonData()
		{
			this.ApplicationFolderPath = string.Format("{0}\\{1}", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Resources.ApplicationFolder);
			if (!Directory.Exists(this.ApplicationFolderPath))
			{
				Directory.CreateDirectory(this.ApplicationFolderPath);
			}
			this.Settings = ConsultingLeadsProSettings.Load(string.Format("{0}\\settings.dat", this.ApplicationFolderPath));
		}
	}
}
