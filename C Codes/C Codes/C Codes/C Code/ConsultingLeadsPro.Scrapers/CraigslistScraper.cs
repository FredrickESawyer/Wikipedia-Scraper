using ConsultingLeadsPro.Callbacks;
using ConsultingLeadsPro.Network;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
namespace ConsultingLeadsPro.Scrapers
{
	internal class CraigslistScraper : AbstractScraper
	{
		public CraigslistScraper(UrlDownloader downloader, ScrapeResultCallback callback) : base(downloader, callback)
		{
			this.initialPageNum = -1;
			this.campaign.SearchEngine = 0;
		}
		private DateTime ParseDate(string sDate)
		{
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			DateTime result;
			if (!DateTime.TryParseExact(sDate, "ddd MMM dd", invariantCulture, DateTimeStyles.AllowWhiteSpaces, out result) && !DateTime.TryParseExact(sDate, "ddd dd MMM", invariantCulture, DateTimeStyles.AllowWhiteSpaces, out result))
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
				simpleScrapeResult.Headline = base.ExtractValue(text, "<h2>(?<value>.*?)</h2>");
				simpleScrapeResult.Email = base.ExtractValue(text, "<a href=\"mailto:(?<value>.*?)\\?subject=").ToLower().Trim();
				simpleScrapeResult.DatePosted = base.ExtractValue(text, "Date: (?<value>.*?)<br>");
				simpleScrapeResult.Description = base.ExtractValue(text, "<div id=\"userbody\">(?<value>.*?)(?:(<table summary=\"craigslist hosted images\">)|(?:</div>))").Trim();
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
				Stack<string> stack = new Stack<string>();
				using (List<string>.Enumerator enumerator2 = simpleScrapeResult.PhonesInBody.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						string current2 = enumerator2.Current;
						if (simpleScrapeResult.Email.Contains(current2) || url.Contains(current2))
						{
							stack.Push(current2);
						}
						else
						{
							foreach (string current3 in simpleScrapeResult.Emails)
							{
								if (current3.Contains(current2))
								{
									stack.Push(current2);
									break;
								}
							}
						}
					}
					goto IL_1AA;
				}
				IL_197:
				simpleScrapeResult.PhonesInBody.Remove(stack.Pop());
				IL_1AA:
				if (stack.Count > 0)
				{
					goto IL_197;
				}
			}
			return simpleScrapeResult;
		}
		protected override string GetUrl(SearchCategory scLocation, SearchCategory scCategory, string keyword, int pageNum)
		{
			string url = scLocation.Url;
			string url2 = scCategory.Url;
			bool flag = url.IndexOf('.') > 0;
			string text = string.Empty;
			if (string.IsNullOrEmpty(keyword))
			{
				if (flag)
				{
					if (pageNum != 0)
					{
						text = string.Format("http://{0}/{1}index{32}00.html", url, url2, pageNum);
					}
					else
					{
						text = string.Format("http://{0}/{1}", url, url2);
					}
				}
				else
				{
					if (pageNum != 0)
					{
						text = string.Format("http://{0}.en.craigslist.org/{1}index{2}00.html", url, url2, pageNum);
					}
					else
					{
						text = string.Format("http://{0}.en.craigslist.org/{1}", url, url2);
					}
				}
			}
			else
			{
				if (flag)
				{
					if (pageNum != 0)
					{
						text = string.Format("http://{0}/search/{1}?query={2}&s={3}00", new object[]
						{
							url, 
							url2, 
							keyword, 
							pageNum
						});
					}
					else
					{
						text = string.Format("http://{0}/search/{1}?query={2}", url, url2, keyword);
					}
				}
				else
				{
					if (pageNum != 0)
					{
						text = string.Format("http://{0}.en.craigslist.org/search/{1}?query={2}&s={3}00", new object[]
						{
							url, 
							url2, 
							keyword, 
							pageNum
						});
					}
					else
					{
						text = string.Format("http://{0}.en.craigslist.org/search/{1}?query={2}", url, url2, keyword);
					}
				}
				text = text.Replace("/?", "?");
			}
			return text;
		}
		protected override bool ProcessKeywordRequest(string text, List<IScrapeResult> result, SearchCategory Location, SearchCategory Category)
		{
			RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline;
			Regex regex = new Regex("<p class=\"row\">.*?<span.*?>.*?</span>(?<date>.*?) - <a.*?href=\"(?<href>.*?)\".*?>.*?</p>", options);
			DateTime t = base.LowDate;
			MatchCollection matchCollection = regex.Matches(text);
			if (matchCollection.Count <= 0)
			{
				regex = new Regex("<p.*?>.*?(?<date>.*?) - <a.*?href=\"(?<href>.*?)\".*?>.*?</p>", options);
				matchCollection = regex.Matches(text);
			}
			bool result2;
			foreach (Match match in matchCollection)
			{
				t = this.ParseDate(match.Groups["date"].Value.Trim());
				if (t < base.LowDate)
				{
					result2 = false;
					return result2;
				}
				if (!(t > base.HighDate))
				{
					SimpleScrapeResult simpleScrapeResult = this.ParseDetails(match.Groups["href"].Value);
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
			return text.IndexOf("<b>Next &gt;&gt;</b>") >= 0;
			return result2;
		}
		protected override bool ProcessSimpleRequest(string text, List<IScrapeResult> result, SearchCategory Location, SearchCategory Category)
		{
			RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline;
			Regex regex = new Regex("<h4.*?class=\"ban\".*?>(?<date>.*?)</h4>", options);
			DateTime t = base.LowDate;
			MatchCollection matchCollection = regex.Matches(text);
			bool result2;
			foreach (Match match in matchCollection)
			{
				t = this.ParseDate(match.Groups["date"].Value);
				if (t < base.LowDate)
				{
					result2 = false;
					return result2;
				}
				if (!(t > base.HighDate))
				{
					regex = new Regex("<p.*?href=\"(?<href>.*?)\".*?</p>", options);
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
					MatchCollection matchCollection2 = regex.Matches(input);
					foreach (Match match3 in matchCollection2)
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
			return text.IndexOf("next 100 postings</a>") >= 0;
			return result2;
		}
	}
}
