using ConsultingLeadsPro.Callbacks;
using ConsultingLeadsPro.Network;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
namespace ConsultingLeadsPro.Scrapers
{
	internal class GumtreeScraper : AbstractScraper
	{
		public GumtreeScraper(UrlDownloader downloader, ScrapeResultCallback callback) : base(downloader, callback)
		{
			this.initialPageNum = 0;
			this.campaign.SearchEngine = 2;
		}
		private DateTime ParseDate(string sDate)
		{
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			DateTime result;
			if (!DateTime.TryParseExact(sDate, "dddd d MMMM", invariantCulture, DateTimeStyles.AllowWhiteSpaces, out result) && !DateTime.TryParseExact(sDate, "ddd dd MMM", invariantCulture, DateTimeStyles.AllowWhiteSpaces, out result))
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
				simpleScrapeResult.Headline = base.ExtractValue(text, "<h2 class=\"summary\">(?<value>.*?)</h2>");
				simpleScrapeResult.DatePosted = base.ExtractValue(text, "<li>.*?<h3>Posted</h3>(?<value>.*?)</li>");
				simpleScrapeResult.Description = base.ExtractValue(text, "<div id=\"description\">(?<value>.*?)</div>").Trim();
				List<string> list = base.ExtractEmails(text);
				foreach (string current in list)
				{
					string text2 = current.ToLower().Trim();
					if (!simpleScrapeResult.Emails.Contains(text2) && simpleScrapeResult.Email != text2)
					{
						simpleScrapeResult.Emails.Add(text2);
					}
				}
				string phone = base.ExtractValue(text, "<span.*?class=\"telephone\".*?>(?<value>.*?)</span>").Trim();
				simpleScrapeResult.PhonesInBody.AddRange(base.ExtractPhones(simpleScrapeResult.Description));
				simpleScrapeResult.PhonesInBody.RemoveAll((string s) => string.IsNullOrEmpty(s));
				if (!string.IsNullOrEmpty(phone))
				{
					simpleScrapeResult.PhonesInBody.RemoveAll((string s) => phone.Contains(s));
					simpleScrapeResult.PhonesInBody.Insert(0, phone);
				}
			}
			return simpleScrapeResult;
		}
		protected override string GetUrl(SearchCategory scLocation, SearchCategory scCategory, string keyword, int pageNum)
		{
			string url = scLocation.Url;
			string url2 = scCategory.Url;
			string result = string.Empty;
			if (pageNum != 1)
			{
				result = string.Format("http://www.gumtree.com/cgi-bin/list_postings.pl?search_location={0}&ubercat={1}&page_no={2}&search_terms={3}", new object[]
				{
					url, 
					url2, 
					pageNum, 
					keyword
				});
			}
			else
			{
				result = string.Format("http://www.gumtree.com/cgi-bin/list_postings.pl?search_location={0}&ubercat={1}&search_terms={2}", url, url2, keyword);
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
			Regex regex = new Regex("<div class=\"day-divider sort-options\"><h3>(?<date>.*?)</h3>", options);
			DateTime t = base.LowDate;
			bool result2;
			foreach (Match match in regex.Matches(text))
			{
				t = this.ParseDate(match.Groups["date"].Value.Trim());
				if (t < base.LowDate)
				{
					result2 = false;
					return result2;
				}
				if (!(t > base.HighDate))
				{
					regex = new Regex("<div class=\"description\"><h3><a href=\"(?<href>.*?)\".*?</a>", options);
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
			return text.IndexOf("Next</a>") >= 0;
			return result2;
		}
	}
}
