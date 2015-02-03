using ConsultingLeadsPro.Callbacks;
using ConsultingLeadsPro.Network;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
namespace ConsultingLeadsPro.Scrapers
{
	internal class KijijiScraper : AbstractScraper
	{
		public KijijiScraper(UrlDownloader downloader, ScrapeResultCallback callback) : base(downloader, callback)
		{
			this.initialPageNum = 0;
			this.campaign.SearchEngine = 3;
		}
		private DateTime ParseDate(string sDate)
		{
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			DateTime result;
			if (!DateTime.TryParseExact(sDate, "dddd, MMMM d", invariantCulture, DateTimeStyles.AllowWhiteSpaces, out result) && !DateTime.TryParseExact(sDate, "MMMM d", invariantCulture, DateTimeStyles.AllowWhiteSpaces, out result))
			{
				DateTime.TryParseExact(sDate, "MM/dd/yy", invariantCulture, DateTimeStyles.AllowWhiteSpaces, out result);
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
				text = base.ExtractValue(text, "<body>(?<value>.*?)</body>");
				simpleScrapeResult.Headline = base.ExtractValue(text, "<h1 id=\"ad-title\"><span>(?<value>.*?)</span>");
				simpleScrapeResult.DatePosted = base.ExtractValue(text, "<span class=\"listlabel\">Date Posted:</span><span class=\"listvalue\">(?<date>.*?)</span></li>");
				simpleScrapeResult.Description = base.ExtractValue(text, "<span style=\"display:block;\".*?</span>(?<value>.*?)<div id=\"ad-details-stats\">").Trim();
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
			string result = string.Empty;
			string url = scLocation.Url;
			string url2 = scCategory.Url;
			if (pageNum != 1)
			{
				result = string.Format("http://{0}.ebayclassifieds.com{1}&page={2}&q={3}&setview=normal&output=123", new object[]
				{
					url, 
					url2, 
					pageNum, 
					keyword
				});
			}
			else
			{
				result = string.Format("http://{0}.ebayclassifieds.com{1}&q={2}&setview=normal&output=123", url, url2, keyword);
			}
			return result;
		}
		protected override bool ProcessKeywordRequest(string text, List<IScrapeResult> result, SearchCategory Location, SearchCategory Category)
		{
			if (text.IndexOf("0 results found in") >= 0 || text.IndexOf("No Results") >= 0)
			{
				return false;
			}
			RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline;
			Regex regex = new Regex("<input [^>]*?name=\"galleryoff\".*?/>");
			Match match = regex.Match(text);
			if (match.Success && match.Value.IndexOf("checked") >= 0)
			{
				regex = new Regex("<div class=\"gallery-ad-title\"><a href=\"(?<href>.*?)\".*?<div class=\"post-date\">Posted: (?<date>.*?)</div>", options);
			}
			else
			{
				regex = new Regex("<li>.*?href=\"(?<href>.*?)\".*?<em>.*?</em>.*?- (?<date>.*?)</li>", options);
			}
			DateTime t = base.LowDate;
			bool result2;
			foreach (Match match2 in regex.Matches(text))
			{
				t = this.ParseDate(match2.Groups["date"].Value);
				if (t < base.LowDate)
				{
					result2 = false;
					return result2;
				}
				if (!(t > base.HighDate))
				{
					SimpleScrapeResult simpleScrapeResult = this.ParseDetails(match2.Groups["href"].Value);
					simpleScrapeResult.Location = Location.Name;
					simpleScrapeResult.Category = Category.Name;
					if (string.IsNullOrEmpty(simpleScrapeResult.Email))
					{
						simpleScrapeResult.Email = simpleScrapeResult.Emails.FirstOrDefault<string>();
					}
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
			return text.IndexOf("<span class=\"pagination-label\">Next</span>") >= 0;
			return result2;
		}
		protected override bool ProcessSimpleRequest(string text, List<IScrapeResult> result, SearchCategory Location, SearchCategory Category)
		{
			if (text.IndexOf("0 results found in") >= 0)
			{
				return false;
			}
			RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline;
			Regex regex = new Regex("<input [^>]*?name=\"galleryoff\".*?/>");
			Match match = regex.Match(text);
			if (match.Success && match.Value.IndexOf("checked") >= 0)
			{
				return this.ProcessKeywordRequest(text, result, Location, Category);
			}
			regex = new Regex("<h3 class=\"section-title\">(?<date>.*?)</h3>", options);
			DateTime t = base.LowDate;
			bool result2;
			foreach (Match match2 in regex.Matches(text))
			{
				t = this.ParseDate(match2.Groups["date"].Value);
				if (t < base.LowDate)
				{
					result2 = false;
					return result2;
				}
				if (!(t > base.HighDate))
				{
					regex = new Regex("<span style=\"vertical-align:middle\">.*?<a href=\"(?<href>.*?)\".*?</a>", options);
					Match match3 = match2.NextMatch();
					string input;
					if (match3.Success)
					{
						input = text.Substring(match2.Index, match3.Index - match2.Index + 1);
					}
					else
					{
						input = text.Substring(match2.Index);
					}
					foreach (Match match4 in regex.Matches(input))
					{
						SimpleScrapeResult simpleScrapeResult = this.ParseDetails(match4.Groups["href"].Value);
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
			return text.IndexOf("<span class=\"pagination-label\">Next</span>") >= 0;
			return result2;
		}
	}
}
