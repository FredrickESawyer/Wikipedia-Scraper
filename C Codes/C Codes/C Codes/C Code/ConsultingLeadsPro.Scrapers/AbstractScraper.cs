using ConsultingLeadsPro.Callbacks;
using ConsultingLeadsPro.Network;
using ConsultingLeadsPro.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
namespace ConsultingLeadsPro.Scrapers
{
	internal abstract class AbstractScraper
	{
		protected enum Country
		{
			USA,
			Canada,
			UK,
			Australia,
			Unknown
		}
		protected delegate bool PageProcessDelegate(string text, List<IScrapeResult> result, SearchCategory Location, SearchCategory Category);
		protected bool allowsCategorylessSearch;
		protected AbstractScraper.Country country;
		protected UrlDownloader downloader;
		protected int initialPageNum;
		protected string keyword = string.Empty;
		protected bool terminated;
		protected List<SearchCategory> categories = new List<SearchCategory>();
		protected List<SearchCategory> locations = new List<SearchCategory>();
		protected Campaign campaign = new Campaign();
		protected int maxThreadCount = Settings.Default.MaxThreadNumber;
		public List<SearchCategory> Categories
		{
			get
			{
				return this.categories;
			}
		}
		public List<SearchCategory> Locations
		{
			get
			{
				return this.locations;
			}
		}
		public DateTime LowDate
		{
			get;
			set;
		}
		public DateTime HighDate
		{
			get;
			set;
		}
		public ScrapeResultCallback Callback
		{
			get;
			set;
		}
		public UrlDownloader Downloader
		{
			get
			{
				return this.downloader;
			}
		}
		public string Keyword
		{
			get
			{
				return this.keyword;
			}
			set
			{
				if (value == null)
				{
					this.keyword = string.Empty;
					return;
				}
				this.keyword = value.Trim();
			}
		}
		public Campaign SearchCampaign
		{
			get
			{
				return this.campaign;
			}
			set
			{
				this.campaign = value;
			}
		}
		public AbstractScraper(UrlDownloader downloader, ScrapeResultCallback callback)
		{
			this.downloader = downloader;
			this.Callback = callback;
		}
		protected abstract bool ProcessSimpleRequest(string text, List<IScrapeResult> result, SearchCategory Location, SearchCategory Category);
		protected abstract bool ProcessKeywordRequest(string text, List<IScrapeResult> result, SearchCategory Location, SearchCategory Category);
		protected abstract string GetUrl(SearchCategory scLocation, SearchCategory scCategory, string keyword, int pageNum);
		protected virtual bool CanAddRes(List<IScrapeResult> result, IScrapeResult res)
		{
			Monitor.Enter(result);
			bool result2;
			try
			{
				switch (CommonData.Instance.Settings.DuplicateProcess)
				{
					case ConsultingLeadsProSettings.Duplicates.Email:
					{
						result2 = (result.Count((IScrapeResult it) => (!string.IsNullOrEmpty(it.Email) && it.Email == res.Email) || it.Emails.Contains(res.Email) || res.Emails.Contains(it.Email) || it.Emails.Intersect(res.Emails).Count<string>() > 0) <= 0);
						break;
					}
					case ConsultingLeadsProSettings.Duplicates.Headline:
					{
						result2 = (result.Count((IScrapeResult it) => !string.IsNullOrEmpty(it.Headline) && it.Headline == res.Headline) <= 0);
						break;
					}
					case ConsultingLeadsProSettings.Duplicates.EmailHeadline:
					{
						result2 = (result.Count((IScrapeResult it) => !string.IsNullOrEmpty(it.Headline) && it.Headline == res.Headline && ((!string.IsNullOrEmpty(it.Email) && it.Email == res.Email) || it.Emails.Contains(res.Email) || res.Emails.Contains(it.Email) || it.Emails.Intersect(res.Emails).Count<string>() > 0)) <= 0);
						break;
					}
					default:
					{
						result2 = true;
						break;
					}
				}
			}
			finally
			{
				Monitor.Exit(result);
			}
			return result2;
		}
		protected string ExtractValue(string text, string regexp)
		{
			RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline;
			Regex regex = new Regex(regexp, options);
			Match match = regex.Match(text);
			if (match.Success)
			{
				return UrlDownloader.HtmlDecode(match.Groups["value"].Value);
			}
			return string.Empty;
		}
		protected List<string> ExtractEmails(string text)
		{
			List<string> list = new List<string>();
			RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline;
			Regex regex = new Regex("[_a-zA-Z\\d\\-\\.]+@[_a-zA-Z\\d\\-]+(\\.[_a-zA-Z\\d\\-]+)+", options);
			MatchCollection matchCollection = regex.Matches(text);
			foreach (Match match in matchCollection)
			{
				string item = match.Value.ToLower().Trim();
				if (!list.Contains(item))
				{
					list.Add(item);
				}
			}
			return list;
		}
		protected List<string> ExtractPhones(string text)
		{
			List<string> list = new List<string>();
			RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline;
			Regex regex = new Regex("[01]?[- .]?(\\([2-9]\\d{2}\\)|[2-9]\\d{2})[- .]?\\d{3}[- .]?\\d{4}", options);
			MatchCollection matchCollection = regex.Matches(text);
			foreach (Match match in matchCollection)
			{
				if (!list.Contains(match.Value.Trim()))
				{
					list.Add(match.Value.Trim());
				}
			}
			return list;
		}
		public void StopScraping()
		{
			this.terminated = true;
			if (this.Callback != null)
			{
				this.Callback.Finish();
			}
		}
		protected void ProcessUrlMatches(string host, string actualUrl, Uri actualUri, MatchCollection matches, Queue<string> urls)
		{
		}
		protected void ThreadedWalk(object o)
		{
			string url = ((object[])o)[0] as string;
			IScrapeResult scrapeResult = ((object[])o)[1] as IScrapeResult;
			if (this.terminated)
			{
				return;
			}
			string text;
			if (this.downloader.DownloadUrl(url, out text))
			{
				if (this.terminated)
				{
					return;
				}
				List<string> list = this.ExtractEmails(text);
				if (list.Count > 0)
				{
					IScrapeResult obj;
					Monitor.Enter(obj = scrapeResult);
					try
					{
						foreach (string current in list)
						{
							if (!scrapeResult.Emails.Contains(current))
							{
								scrapeResult.Emails.Add(current);
							}
						}
						if (string.IsNullOrEmpty(scrapeResult.Email))
						{
							scrapeResult.Email = scrapeResult.Emails.FirstOrDefault<string>();
						}
						scrapeResult.Emails.Remove(scrapeResult.Email);
					}
					finally
					{
						Monitor.Exit(obj);
					}
				}
			}
		}
		protected void WalkWebsite(string url, IScrapeResult res, out string resolvedUrl)
		{
			resolvedUrl = url;
			if (CommonData.Instance.Settings.DeepSearch == ConsultingLeadsProSettings.DeepSearchOptions.SearchNever)
			{
				return;
			}
			if (CommonData.Instance.Settings.DeepSearch == ConsultingLeadsProSettings.DeepSearchOptions.SearchIfNoEmails)
			{
				if (!string.IsNullOrEmpty(res.Email))
				{
					return;
				}
				if (res.Emails.Count > 0)
				{
					return;
				}
			}
			if (this.terminated)
			{
				return;
			}
			string text;
			Uri uri;
			if (this.downloader.DownloadUrl(url, false, out text, out uri))
			{
				resolvedUrl = uri.AbsoluteUri;
				if (this.terminated)
				{
					return;
				}
				foreach (string current in this.ExtractEmails(text))
				{
					if (!res.Emails.Contains(current))
					{
						res.Emails.Add(current);
					}
				}
				if (string.IsNullOrEmpty(res.Email))
				{
					res.Email = res.Emails.FirstOrDefault<string>();
				}
				res.Emails.Remove(res.Email);
				if (this.terminated)
				{
					return;
				}
				string text2 = string.Format("{0}://{1}", uri.Scheme, uri.Host);
				StringBuilder stringBuilder = new StringBuilder(text2);
				for (int i = 0; i < uri.Segments.Length; i++)
				{
					if (i != uri.Segments.Length - 1 || uri.Segments[i].EndsWith("/"))
					{
						stringBuilder.Append(uri.Segments[i]);
					}
				}
				string text3 = stringBuilder.ToString();
				if (this.terminated)
				{
					return;
				}
				RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline;
				Regex regex = new Regex("<a.*?href=(?<char>\"|')(?<href>.*?)\\k<char>.*?</a>", options);
				MatchCollection matchCollection = regex.Matches(text);
				if (this.terminated)
				{
					return;
				}
				Queue<string> queue = new Queue<string>();
				foreach (Match match in matchCollection)
				{
					if (this.terminated)
					{
						return;
					}
					string text4 = match.Groups["href"].Value.Trim().Replace("&amp;", "&");
					if (!text4.StartsWith("'") && !text4.StartsWith("#"))
					{
						if (text4.StartsWith("./"))
						{
							text4 = text4.Remove(0, 1);
						}
						if (text4.StartsWith("www."))
						{
							text4 = "http://" + text4;
						}
						if (!text4.StartsWith("javascript:") && !text4.StartsWith("mailto:"))
						{
							if (!text4.StartsWith("http://") && !text4.StartsWith("https://"))
							{
								if (text4.StartsWith("/"))
								{
									text4 = text2 + text4;
								}
								else
								{
									if (text3.EndsWith("/"))
									{
										text4 = text3 + text4;
									}
									else
									{
										text4 = text3 + "/" + text4;
									}
								}
							}
							if (text4.StartsWith(text2))
							{
								if (!queue.Contains(text4) && string.CompareOrdinal(text4, uri.AbsoluteUri) != 0)
								{
									queue.Enqueue(text4);
								}
								if (this.terminated)
								{
									return;
								}
							}
						}
					}
				}
				Queue<KeyValuePair<Thread, object>> queue2 = new Queue<KeyValuePair<Thread, object>>();
				while (queue.Count > 0)
				{
					string text5 = queue.Dequeue();
					object[] value = new object[]
					{
						text5, 
						res
					};
					queue2.Enqueue(new KeyValuePair<Thread, object>(new Thread(new ParameterizedThreadStart(this.ThreadedWalk))
					{
						Name = text5, 
						Priority = ThreadPriority.BelowNormal
					}, value));
				}
				int num = this.maxThreadCount * 2;
				List<Thread> list = new List<Thread>(this.maxThreadCount);
				Thread.CurrentThread.Priority = ThreadPriority.Lowest;
				while (queue2.Count > 0 || list.Count > 0)
				{
					if (this.terminated)
					{
						return;
					}
					while (list.Count < num && queue2.Count > 0)
					{
						KeyValuePair<Thread, object> keyValuePair = queue2.Dequeue();
						list.Add(keyValuePair.Key);
						keyValuePair.Key.Start(keyValuePair.Value);
					}
					if (this.terminated)
					{
						return;
					}
					int j = 0;
					while (j < list.Count)
					{
						if (list[j].ThreadState == ThreadState.Stopped)
						{
							list.RemoveAt(j);
						}
						else
						{
							j++;
						}
					}
				}
			}
		}
		protected void ProcessSearch(object o)
		{
			List<IScrapeResult> result = ((object[])o)[0] as List<IScrapeResult>;
			SearchCategory searchCategory = ((object[])o)[1] as SearchCategory;
			SearchCategory searchCategory2 = ((object[])o)[2] as SearchCategory;
			string text = ((object[])o)[3] as string;
			AbstractScraper.PageProcessDelegate pageProcessDelegate = ((object[])o)[4] as AbstractScraper.PageProcessDelegate;
			int num = this.initialPageNum;
			string empty = string.Empty;
			while (!this.terminated)
			{
				num++;
				if (this.downloader.DownloadUrl(this.GetUrl(searchCategory, searchCategory2, text, num), out empty) && this.terminated)
				{
					return;
				}
				if (!pageProcessDelegate(empty, result, searchCategory, searchCategory2))
				{
					return;
				}
			}
		}
		public virtual void Search()
		{
			if (this.Callback != null)
			{
				this.Callback.Init(this.Locations.Count * this.Categories.Count);
			}
			this.terminated = false;
			string text = UrlDownloader.UrlEncode(this.Keyword);
			if (this.Categories.Count <= 0)
			{
				if (!this.allowsCategorylessSearch)
				{
					if (this.downloader != null && this.downloader.errorCallback != null)
					{
						this.downloader.errorCallback.Log("No categories selected.");
					}
					return;
				}
				if (string.IsNullOrEmpty(text))
				{
					if (this.downloader != null && this.downloader.errorCallback != null)
					{
						this.downloader.errorCallback.Log("No keyword and categories selected.");
					}
					return;
				}
			}
			if (this.Locations.Count <= 0)
			{
				if (this.downloader != null && this.downloader.errorCallback != null)
				{
					this.downloader.errorCallback.Log("No locations selected.");
				}
				return;
			}
			AbstractScraper.PageProcessDelegate pageProcessDelegate = string.IsNullOrEmpty(text) ? new AbstractScraper.PageProcessDelegate(this.ProcessSimpleRequest) : new AbstractScraper.PageProcessDelegate(this.ProcessKeywordRequest);
			Queue<KeyValuePair<Thread, object>> queue = new Queue<KeyValuePair<Thread, object>>();
			if (this.Categories.Count > 0)
			{
				using (List<SearchCategory>.Enumerator enumerator = this.Locations.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						SearchCategory current = enumerator.Current;
						foreach (SearchCategory current2 in this.Categories)
						{
							object[] value = new object[]
							{
								this.campaign.Leads, 
								current, 
								current2, 
								text, 
								pageProcessDelegate
							};
							queue.Enqueue(new KeyValuePair<Thread, object>(new Thread(new ParameterizedThreadStart(this.ProcessSearch))
							{
								Name = string.Format("{0}|{1}", current.Name, current2.Name)
							}, value));
						}
					}
					goto IL_2C0;
				}
			}
			foreach (SearchCategory current3 in this.Locations)
			{
				SearchCategory searchCategory = new SearchCategory();
				searchCategory.Name = text;
				searchCategory.Url = text;
				object[] value2 = new object[]
				{
					this.campaign.Leads, 
					current3, 
					searchCategory, 
					null, 
					pageProcessDelegate
				};
				queue.Enqueue(new KeyValuePair<Thread, object>(new Thread(new ParameterizedThreadStart(this.ProcessSearch))
				{
					Name = string.Format("{0}|{1}", current3.Name, searchCategory.Name)
				}, value2));
			}
			IL_2C0:
			List<Thread> list = new List<Thread>(this.maxThreadCount);
			Thread.CurrentThread.Priority = ThreadPriority.Lowest;
			int i = 0;
			while (queue.Count > 0 || list.Count > 0)
			{
				if (this.terminated)
				{
					break;
				}
				while (list.Count < this.maxThreadCount && queue.Count > 0)
				{
					KeyValuePair<Thread, object> keyValuePair = queue.Dequeue();
					list.Add(keyValuePair.Key);
					keyValuePair.Key.Start(keyValuePair.Value);
				}
				if (this.terminated)
				{
					break;
				}
				i = 0;
				while (i < list.Count)
				{
					if (list[i].ThreadState == ThreadState.Stopped)
					{
						list.RemoveAt(i);
						if (this.Callback != null)
						{
							this.Callback.ShowProgress();
						}
					}
					else
					{
						i++;
					}
				}
			}
			if (this.Callback != null)
			{
				this.Callback.Finish();
			}
			if (this.downloader != null && this.downloader.errorCallback != null)
			{
				if (this.terminated)
				{
					this.downloader.errorCallback.Log("Search has been stopped.");
					return;
				}
				this.downloader.errorCallback.Log("Search finished.");
			}
		}
	}
}
