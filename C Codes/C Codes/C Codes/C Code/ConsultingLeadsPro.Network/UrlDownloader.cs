using ConsultingLeadsPro.Callbacks;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
namespace ConsultingLeadsPro.Network
{
	public class UrlDownloader
	{
		public bool KeepAlive
		{
			get;
			set;
		}
		public LogCallback errorCallback
		{
			get;
			set;
		}
		public UrlDownloader(LogCallback lc)
		{
			this.errorCallback = lc;
			this.KeepAlive = true;
		}
		public static string SkipHtmlTags(string text)
		{
			Regex regex = new Regex("<.*?>");
			return regex.Replace(text, "");
		}
		public static string UrlEncode(string text)
		{
			return HttpUtility.UrlEncode(text);
		}
		public static string HtmlDecode(string text)
		{
			return HttpUtility.HtmlDecode(text);
		}
		public static string Post(string url, string parameters)
		{
			WebRequest webRequest = WebRequest.Create(url);
			webRequest.ContentType = "application/x-www-form-urlencoded";
			webRequest.Method = "POST";
			byte[] bytes = Encoding.ASCII.GetBytes(parameters);
			webRequest.ContentLength = (long)bytes.Length;
			using (Stream requestStream = webRequest.GetRequestStream())
			{
				requestStream.Write(bytes, 0, bytes.Length);
			}
			WebResponse response = webRequest.GetResponse();
			if (response == null)
			{
				return null;
			}
			StreamReader streamReader = new StreamReader(response.GetResponseStream());
			return streamReader.ReadToEnd().Trim();
		}
		public static string Get(string url)
		{
			WebRequest webRequest = WebRequest.Create(url);
			((HttpWebRequest)webRequest).UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)";
			WebResponse response = webRequest.GetResponse();
			Stream responseStream = response.GetResponseStream();
			StreamReader streamReader = new StreamReader(responseStream);
			return streamReader.ReadToEnd();
		}
		public bool DownloadUrl(string url, out string text)
		{
			Uri uri;
			return this.DownloadUrl(url, true, out text, out uri);
		}
		public bool DownloadUrl(string url, bool logErrors, out string text, out Uri actualUrl)
		{
			actualUrl = null;
			bool result;
			try
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
				httpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)";
				httpWebRequest.KeepAlive = this.KeepAlive;
				httpWebRequest.Proxy = CommonData.Instance.Settings.ProxySetting.GetProxy();
				WebResponse response = httpWebRequest.GetResponse();
				actualUrl = response.ResponseUri;
				Stream responseStream = response.GetResponseStream();
				StreamReader streamReader = new StreamReader(responseStream);
				text = streamReader.ReadToEnd();
				result = true;
			}
			catch (Exception arg_6C_0)
			{
				Exception exception = arg_6C_0;
				if (logErrors && this.errorCallback != null)
				{
					this.errorCallback.Log(string.Format("{0} ({1})", exception.Message, url));
				}
				text = string.Empty;
				result = false;
			}
			return result;
		}
	}
}
