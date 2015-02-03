using ConsultingLeadsPro.Callbacks;
using ConsultingLeadsPro.Network;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
namespace ConsultingLeadsPro.Scrapers
{
	internal class BackpageScraper : AbstractScraper
	{
		public BackpageScraper(UrlDownloader downloader, ScrapeResultCallback callback) : base(downloader, callback)
		{
			this.initialPageNum = 0;
			this.campaign.SearchEngine = 1;
		}
		private DateTime ParseDate(string sDate)
		{
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			DateTime result;
			if (!DateTime.TryParseExact(sDate, "ddd. MMM. d", invariantCulture, DateTimeStyles.AllowWhiteSpaces, out result) && !DateTime.TryParseExact(sDate, "ddd dd MMM", invariantCulture, DateTimeStyles.AllowWhiteSpaces, out result))
			{
				DateTime.TryParseExact(sDate, "MMM d", invariantCulture, DateTimeStyles.AllowWhiteSpaces, out result);
			}
			return result;
		}
		public SimpleScrapeResult ParseDetails(string url)
		{
			SimpleScrapeResult simpleScrapeResult = new SimpleScrapeResult();
			simpleScrapeResult.AdUrl = url;
			string text;
			if (this.downloader.DownloadUrl(url, out text))
			{
				simpleScrapeResult.Headline = base.ExtractValue(text, "<h1>(?<value>.*?)</h1>");
				simpleScrapeResult.Email = base.ExtractValue(text, "b>Reply</b>:&nbsp;<a href=\"mailto:(?<value>.*?)\\?subject=").ToLower().Trim();
				simpleScrapeResult.DatePosted = base.ExtractValue(text, ">posted: (?<value>.*?)</div>");
				simpleScrapeResult.Description = base.ExtractValue(text, "<div class=\"adBody\">(?<value>.*?)</div>").Trim();
				List<string> list = base.ExtractEmails(text);
				foreach (string current in list)
				{
					string text2 = current.ToLower().Trim();
					if (!simpleScrapeResult.Emails.Contains(text2) && simpleScrapeResult.Email != text2)
					{
						simpleScrapeResult.Emails.Add(text2);
					}
				}
				simpleScrapeResult.PhonesInBody.AddRange(base.ExtractPhones(text));
			}
			return simpleScrapeResult;
		}
		protected override string GetUrl(SearchCategory scLocation, SearchCategory scCategory, string keyword, int pageNum)
		{
			string url = scLocation.Url;
			string url2 = scCategory.Url;
			url.IndexOf('.');
			string result = string.Empty;
			if (string.IsNullOrEmpty(keyword))
			{
				if (pageNum != 1)
				{
					result = string.Format("http://{0}.backpage.com/{1}&page={2}", url, url2, pageNum);
				}
				else
				{
					result = string.Format("http://{0}.backpage.com/{1}", url, url2);
				}
			}
			else
			{
				if (pageNum != 1)
				{
					result = string.Format("http://{0}.backpage.com/{1}&keyword={2}&page={3}", new object[]
					{
						url, 
						url2, 
						keyword, 
						pageNum
					});
				}
				else
				{
					result = string.Format("http://{0}.backpage.com/{1}&keyword={2}", url, url2, keyword);
				}
			}
			return result;
		}
		protected override bool ProcessKeywordRequest(string text, List<IScrapeResult> result, SearchCategory Location, SearchCategory Category)
		{
			return this.ProcessSimpleRequest(text, result, Location, Category);
		}
		protected override bool ProcessSimpleRequest(string text, List<IScrapeResult> result, SearchCategory Location, SearchCategory Category)
		{
			RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline;
			Regex regex = new Regex("<div class=\"date\">(?<date>.*?)</div>", options);
			DateTime t = base.LowDate;
			bool result2;
			foreach (Match match in regex.Matches(text))
			{
				t = this.ParseDate(match.Groups["date"].Value);
				if (t < base.LowDate)
				{
					result2 = false;
					return result2;
				}
				if (!(t > base.HighDate))
				{
					regex = new Regex("<div class=\"cat\">.*?<a href=\"(?<href>.*?)\">.*?</a>.*?</div>", options);
					Match match2 = match.NextMatch();
					string input;
					if (match2.Success)
					{
						input = text.Substring(match.Index, match2.Index - match.Index + 1);
					}
					else
					{
						input = text.Substring(match.Index);
					}
					foreach (Match match3 in regex.Matches(input))
					{
						SimpleScrapeResult simpleScrapeResult = this.ParseDetails(match3.Groups["href"].Value);
						simpleScrapeResult.Location = Location.Name;
						simpleScrapeResult.Category = Category.Name;
						simpleScrapeResult.Emails.Remove(simpleScrapeResult.Email);
						if (this.terminated)
						{
							result2 = false;
							return result2;
						}
						if (this.CanAddRes(result, simpleScrapeResult))
						{
							Monitor.Enter(result);
							try
							{
								result.Add(simpleScrapeResult);
							}
							finally
							{
								Monitor.Exit(result);
							}
							if (base.Callback != null)
							{
								base.Callback.Process(simpleScrapeResult);
							}
						}
					}
				}
			}
			return text.IndexOf("class=\"pagination\">Next</a>") >= 0;
			return result2;
		}
	}
}
