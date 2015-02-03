using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;
namespace ConsultingLeadsPro.Properties
{
	[CompilerGenerated, GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
	internal sealed class Settings : ApplicationSettingsBase
	{
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}
		[UserScopedSetting, DefaultSettingValue(""), DebuggerNonUserCode]
		public string LinkDialogURLs
		{
			get
			{
				return (string)this["LinkDialogURLs"];
			}
			set
			{
				this["LinkDialogURLs"] = value;
			}
		}
		[UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("10")]
		public int MaxThreadNumber
		{
			get
			{
				return (int)this["MaxThreadNumber"];
			}
			set
			{
				this["MaxThreadNumber"] = value;
			}
		}
		[DefaultSettingValue("{0B502FE9-FEB2-4aa0-8635-769B05BE1F34}"), UserScopedSetting, DebuggerNonUserCode]
		public string ProxyListGuid
		{
			get
			{
				return (string)this["ProxyListGuid"];
			}
			set
			{
				this["ProxyListGuid"] = value;
			}
		}
	}
}
