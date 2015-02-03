using ConsultingLeadsPro.Callbacks;
using ConsultingLeadsPro.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
namespace ConsultingLeadsPro.Scrapers
{
	internal class YahooScraper : AbstractMapScraper
	{
		public YahooScraper(UrlDownloader downloader, ScrapeResultCallback callback) : base(downloader, callback)
		{
			this.initialPageNum = 0;
			base.Radius = 0f;
			this.allowsCategorylessSearch = true;
			this.campaign.SearchEngine = 4;
		}
		protected string GetAddress(string text)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string value = base.ExtractValue(text, "<span.*?property=\"vcard:street-address\".*?>(?<value>.*?)</span>");
			string value2 = base.ExtractValue(text, "<span.*?property=\"vcard:locality\".*?>(?<value>.*?)</span>");
			string value3 = base.ExtractValue(text, "<span.*?property=\"vcard:region\".*?>(?<value>.*?)</span>");
			string value4 = base.ExtractValue(text, "<span.*?property=\"vcard:postal-code\".*?>(?<value>.*?)</span>");
			stringBuilder.Append(value);
			if (!string.IsNullOrEmpty(value2) && stringBuilder.Length > 0)
			{
				stringBuilder.Append(", ");
			}
			stringBuilder.Append(value2);
			if (!string.IsNullOrEmpty(value3) && stringBuilder.Length > 0)
			{
				stringBuilder.Append(", ");
			}
			stringBuilder.Append(value3);
			if (!string.IsNullOrEmpty(value4) && stringBuilder.Length > 0)
			{
				stringBuilder.Append(" ");
			}
			stringBuilder.Append(value4);
			return stringBuilder.ToString();
		}
		protected MapScrapeResult ParseDetailsCanada(string url)
		{
			MapScrapeResult mapScrapeResult = new MapScrapeResult();
			mapScrapeResult.AdUrl = url;
			if (this.terminated)
			{
				return mapScrapeResult;
			}
			string text;
			if (this.downloader.DownloadUrl(url, out text))
			{
				mapScrapeResult.Headline = base.ExtractValue(text, "<h1.*?class=\"fn org\".*?>(?<value>.*?)</h1>");
				mapScrapeResult.Address = base.ExtractValue(text, "<span.*?class=\"street-address\".*?>(?<value>.*?)</span>");
				mapScrapeResult.City = base.ExtractValue(text, "<span.*?class=\"locality\".*?>(?<value>.*?)</span>");
				mapScrapeResult.Region = base.ExtractValue(text, "<span.*?class=\"region\".*?>(?<value>.*?)</span>");
				mapScrapeResult.ZipCode = base.ExtractValue(text, "<span.*?class=\"postal-code\".*?>(?<value>.*?)</span>");
				mapScrapeResult.Map = base.ExtractValue(text, "<a.*?href=\"(?<value>http://ca.maps.yahoo.com.*?)\">");
				mapScrapeResult.Latitude = base.ExtractValue(mapScrapeResult.Map, "lon=(?<value>.*?)&");
				mapScrapeResult.Longitude = base.ExtractValue(mapScrapeResult.Map, "lat=(?<value>.*?)&");
				mapScrapeResult.Email = base.ExtractEmails(text).FirstOrDefault<string>();
				mapScrapeResult.Phone = base.ExtractPhones(text).FirstOrDefault<string>();
			}
			return mapScrapeResult;
		}
		protected MapScrapeResult ParseDetailsUSA(string url)
		{
			MapScrapeResult mapScrapeResult = new MapScrapeResult();
			mapScrapeResult.AdUrl = url;
			if (this.terminated)
			{
				return mapScrapeResult;
			}
			string text;
			if (this.downloader.DownloadUrl(url, out text))
			{
				mapScrapeResult.Headline = base.ExtractValue(text, "<span.*?property=\"vcard:Name\".*?>(?<value>.*?)</span>");
				mapScrapeResult.Address = this.GetAddress(text);
				StringBuilder stringBuilder = new StringBuilder();
				mapScrapeResult.Region = base.ExtractValue(text, "<span.*?property=\"vcard:region\".*?>(?<value>.*?)</span>");
				mapScrapeResult.ZipCode = base.ExtractValue(text, "<span.*?property=\"vcard:postal-code\".*?>(?<value>.*?)</span>");
				string value = base.ExtractValue(text, "<span.*?property=\"vcard:street-address\".*?>(?<value>.*?)</span>");
				mapScrapeResult.City = base.ExtractValue(text, "<span.*?property=\"vcard:locality\".*?>(?<value>.*?)</span>");
				stringBuilder.Append(value);
				if (!string.IsNullOrEmpty(mapScrapeResult.City) && stringBuilder.Length > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(mapScrapeResult.City);
				if (!string.IsNullOrEmpty(mapScrapeResult.Region) && stringBuilder.Length > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(mapScrapeResult.Region);
				if (!string.IsNullOrEmpty(mapScrapeResult.ZipCode) && stringBuilder.Length > 0)
				{
					stringBuilder.Append(" ");
				}
				stringBuilder.Append(mapScrapeResult.ZipCode);
				mapScrapeResult.Address = stringBuilder.ToString();
				mapScrapeResult.Latitude = base.ExtractValue(text, "<span.*?property=\"vcard:latitude\".*?>(?<value>.*?)</span>");
				mapScrapeResult.Longitude = base.ExtractValue(text, "<span.*?property=\"vcard:longitude\".*?>(?<value>.*?)</span>");
				mapScrapeResult.Map = base.ExtractValue(text, "<div.*?id=\"yls-dt-mapcont\".*?>.*?<img.*?src=\"(?<value>.*?)\"");
				mapScrapeResult.Email = base.ExtractEmails(text).FirstOrDefault<string>();
				mapScrapeResult.Phone = base.ExtractValue(text, "<li.*?property=\"vcard:tel\".*?>(?<value>.*?)</li>");
				string text2 = base.ExtractValue(text, "<ul.*?id=\"yls-dt-weblinks\">.*?(?<value><a.*?property=\"vcard:url\".*?>)");
				mapScrapeResult.Website = base.ExtractValue(text2, "href=\"(?<value>.*?)\"").Trim();
			}
			return mapScrapeResult;
		}
		protected MapScrapeResult ParseDetailsUK(string url)
		{
			MapScrapeResult mapScrapeResult = new MapScrapeResult();
			mapScrapeResult.AdUrl = url;
			if (this.terminated)
			{
				return mapScrapeResult;
			}
			string text;
			if (this.downloader.DownloadUrl(url, out text))
			{
				mapScrapeResult.Headline = base.ExtractValue(text, "<h1 class=\"org fn n\">(?<value>.*?)</h1>");
				mapScrapeResult.Address = base.ExtractValue(text, "<p class=\"street-address\">(?<value>.*?)</p>");
				mapScrapeResult.ZipCode = base.ExtractValue(text, "<span class=\"postal-code\">(?<value>.*?)</span>");
				mapScrapeResult.Address = UrlDownloader.SkipHtmlTags(mapScrapeResult.Address).Trim();
				List<string> list = new List<string>(mapScrapeResult.Address.Split(new string[]
				{
					"\t", 
					"\n", 
					","
				}, StringSplitOptions.RemoveEmptyEntries));
				list.RemoveAll((string s) => s.Trim() == string.Empty);
				mapScrapeResult.Address = mapScrapeResult.Address.Replace("\t", string.Empty);
				mapScrapeResult.Address = mapScrapeResult.Address.Replace("\n", string.Empty);
				if (list.Count > 1)
				{
					mapScrapeResult.City = list[1];
				}
				string text2 = base.ExtractValue(text, "<p class=\"geo\">(?<value>.*?)</p>");
				mapScrapeResult.Latitude = base.ExtractValue(text2, "<span class=\"latitude\">(?<value>.*?)</span>");
				mapScrapeResult.Longitude = base.ExtractValue(text2, "<span class=\"longitude\">(?<value>.*?)</span>");
				mapScrapeResult.Map = base.ExtractValue(text, "<div id=\"ent-page-md-links\">.*?href=\"(?<value>.*?)\".*?</div>");
				mapScrapeResult.Email = base.ExtractEmails(text).FirstOrDefault<string>();
				mapScrapeResult.Phone = base.ExtractValue(text, "<h2 class=\"tel\">.*?<span class=\"value\">(?<value>.*?)</span>").Trim();
			}
			return mapScrapeResult;
		}
		protected MapScrapeResult ParseDetailsAustralia(string url)
		{
			MapScrapeResult mapScrapeResult = new MapScrapeResult();
			mapScrapeResult.AdUrl = url;
			if (this.terminated)
			{
				return mapScrapeResult;
			}
			string text;
			if (this.downloader.DownloadUrl(url, out text))
			{
				mapScrapeResult.Headline = base.ExtractValue(text, "<h1>(?<value>.*?)</h1>");
				mapScrapeResult.Address = base.ExtractValue(text, "<div class=\"adr\">(?<value>.*?)</div>");
				mapScrapeResult.Region = base.ExtractValue(mapScrapeResult.Address, "<span class=\"region\".*?>(?<value>.*?)</span>");
				mapScrapeResult.City = base.ExtractValue(mapScrapeResult.Address, "<span.*?class=\"locality\".*?>(?<value>.*?)</span>");
				mapScrapeResult.ZipCode = base.ExtractValue(mapScrapeResult.Address, "<span.*?class=\"postal-code\".*?>(?<value>.*?)</span>");
				mapScrapeResult.Address = UrlDownloader.SkipHtmlTags(mapScrapeResult.Address).Replace("\t", string.Empty).Replace("\n", string.Empty).Trim();
				mapScrapeResult.Latitude = base.ExtractValue(text, "<span class=\"latitude\">(?<value>.*?)</span>");
				mapScrapeResult.Longitude = base.ExtractValue(text, "<span class=\"longitude\">(?<value>.*?)</span>");
				mapScrapeResult.Map = string.Format("http://maps.yahoo.com/maps_result?ard=1&lat={0}&lon={1}&zoom=18", mapScrapeResult.Latitude, mapScrapeResult.Longitude);
				string text2 = base.ExtractValue(text, "<ul class=\"pageTools\">(?<value>.*?)</ul>");
				mapScrapeResult.Website = base.ExtractValue(text2, "href=\"(?<value>.*?)\"");
				mapScrapeResult.Phone = base.ExtractValue(text2, "<span class=\"tl-phone-full\">(?<value>.*?)</span>").Trim();
				mapScrapeResult.Email = base.ExtractEmails(text).FirstOrDefault<string>();
			}
			return mapScrapeResult;
		}
		protected bool ProcessCanada(string text, List<IScrapeResult> result, SearchCategory Location, SearchCategory Category)
		{
			RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline;
			Regex regex = new Regex("<li.*?class=\"vcard\".*?>.*?<a.*?class=\"ttl\".*?href=\"(?<href>.*?)\".*?</li>", options);
			MatchCollection matchCollection = regex.Matches(text);
			bool result2;
			foreach (Match match in matchCollection)
			{
				if (this.terminated)
				{
					result2 = false;
					return result2;
				}
				MapScrapeResult mapScrapeResult = this.ParseDetailsCanada(match.Groups["href"].Value);
				mapScrapeResult.Radius = base.ExtractValue(match.Value, "<span.*?class=\"mlg\">(?<value>.*?)</span>");
				mapScrapeResult.Category = Category.Name;
				if (this.CanAddRes(result, mapScrapeResult))
				{
					if (this.terminated)
					{
						result2 = false;
						return result2;
					}
					if (!string.IsNullOrEmpty(mapScrapeResult.Website))
					{
						string website;
						base.WalkWebsite(mapScrapeResult.Website, mapScrapeResult, out website);
						mapScrapeResult.Website = website;
					}
					if (this.CanAddRes(result, mapScrapeResult))
					{
						Monitor.Enter(result);
						try
						{
							result.Add(mapScrapeResult);
						}
						finally
						{
							Monitor.Exit(result);
						}
						if (this.terminated)
						{
							result2 = false;
							return result2;
						}
						if (base.Callback != null)
						{
							base.Callback.Process(mapScrapeResult);
						}
					}
				}
			}
			return text.IndexOf("<span>Next</span></a></b>") >= 0;
			return result2;
		}
		protected bool ProcessUK(string text, List<IScrapeResult> result, SearchCategory Location, SearchCategory Category)
		{
			RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline;
			Regex regex = new Regex("<div class=\"addr \">.*?<h2>(?<txt>.*?)</h2>.*?<span class=\"note\">(?<radius>.*?)<span>", options);
			MatchCollection matchCollection = regex.Matches(text);
			bool result2;
			foreach (Match match in matchCollection)
			{
				if (this.terminated)
				{
					result2 = false;
					return result2;
				}
				string text2 = base.ExtractValue(match.Groups["txt"].Value, "href=\"(?<value>.*?)\"");
				MapScrapeResult mapScrapeResult = this.ParseDetailsUK(string.Format("{0}{1}", "http://uk.local.yahoo.com", text2));
				mapScrapeResult.Radius = match.Groups["radius"].Value;
				mapScrapeResult.Region = text2.Split(new string[]
				{
					"/"
				}, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault<string>();
				if (!string.IsNullOrEmpty(mapScrapeResult.Region))
				{
					mapScrapeResult.Region = mapScrapeResult.Region.Replace('_', ' ');
				}
				mapScrapeResult.Category = Category.Name;
				if (this.CanAddRes(result, mapScrapeResult))
				{
					if (this.terminated)
					{
						result2 = false;
						return result2;
					}
					if (!string.IsNullOrEmpty(mapScrapeResult.Website))
					{
						string website;
						base.WalkWebsite(mapScrapeResult.Website, mapScrapeResult, out website);
						mapScrapeResult.Website = website;
					}
					if (this.CanAddRes(result, mapScrapeResult))
					{
						Monitor.Enter(result);
						try
						{
							result.Add(mapScrapeResult);
						}
						finally
						{
							Monitor.Exit(result);
						}
						if (this.terminated)
						{
							result2 = false;
							return result2;
						}
						if (base.Callback != null)
						{
							base.Callback.Process(mapScrapeResult);
						}
					}
				}
			}
			return text.IndexOf("Next</a></li>") >= 0;
			return result2;
		}
		protected bool ProcessAustralia(string text, List<IScrapeResult> result, SearchCategory Location, SearchCategory Category)
		{
			RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline;
			Regex regex = new Regex("<a class=\"org\" href=\"/(?<href>.*?)\">", options);
			MatchCollection matchCollection = regex.Matches(text);
			bool result2;
			foreach (Match match in matchCollection)
			{
				if (this.terminated)
				{
					result2 = false;
					return result2;
				}
				MapScrapeResult mapScrapeResult = this.ParseDetailsAustralia(string.Format("{0}{1}", "http://local.yahoo.com.au/", match.Groups["href"].Value));
				mapScrapeResult.Category = Category.Name;
				mapScrapeResult.Radius = 0.ToString();
				if (this.CanAddRes(result, mapScrapeResult))
				{
					if (this.terminated)
					{
						result2 = false;
						return result2;
					}
					if (!string.IsNullOrEmpty(mapScrapeResult.Website))
					{
						string website;
						base.WalkWebsite(mapScrapeResult.Website, mapScrapeResult, out website);
						mapScrapeResult.Website = website;
					}
					if (this.CanAddRes(result, mapScrapeResult))
					{
						Monitor.Enter(result);
						try
						{
							result.Add(mapScrapeResult);
						}
						finally
						{
							Monitor.Exit(result);
						}
						if (this.terminated)
						{
							result2 = false;
							return result2;
						}
						if (base.Callback != null)
						{
							base.Callback.Process(mapScrapeResult);
						}
					}
				}
			}
			return text.IndexOf("\"TL_pagelink_next\"") >= 0;
			return result2;
		}
		protected override bool ProcessKeywordRequest(string text, List<IScrapeResult> result, SearchCategory Location, SearchCategory Category)
		{
			return this.ProcessSimpleRequest(text, result, Location, Category);
		}
		protected bool ProcessUSA(string text, List<IScrapeResult> result, SearchCategory Location, SearchCategory Category)
		{
			RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline;
			Regex regex = new Regex("<tr class=\"yls-rs-listinfo\">.*?href=\"(?<href>.*?)\".*?</tr>", options);
			Regex regex2 = new Regex("<td class=\"distance\">(?<radius>.*?)</td>", options);
			MatchCollection matchCollection = regex.Matches(text);
			bool result2;
			foreach (Match match in matchCollection)
			{
				if (this.terminated)
				{
					result2 = false;
					return result2;
				}
				MapScrapeResult mapScrapeResult = this.ParseDetailsUSA(match.Groups["href"].Value);
				Match match2 = regex2.Match(match.Value);
				if (match2.Success)
				{
					mapScrapeResult.Radius = UrlDownloader.SkipHtmlTags(match2.Groups["radius"].Value).Trim();
				}
				mapScrapeResult.Category = Category.Name;
				if (this.CanAddRes(result, mapScrapeResult))
				{
					if (this.terminated)
					{
						result2 = false;
						return result2;
					}
					if (!string.IsNullOrEmpty(mapScrapeResult.Website))
					{
						string website;
						base.WalkWebsite(mapScrapeResult.Website, mapScrapeResult, out website);
						mapScrapeResult.Website = website;
					}
					if (this.CanAddRes(result, mapScrapeResult))
					{
						Monitor.Enter(result);
						try
						{
							result.Add(mapScrapeResult);
						}
						finally
						{
							Monitor.Exit(result);
						}
						if (this.terminated)
						{
							result2 = false;
							return result2;
						}
						if (base.Callback != null)
						{
							base.Callback.Process(mapScrapeResult);
						}
					}
				}
			}
			return text.IndexOf("Next <span>&#187;") >= 0;
			return result2;
		}
		protected override bool ProcessSimpleRequest(string text, List<IScrapeResult> result, SearchCategory Location, SearchCategory Category)
		{
			switch (this.country)
			{
				case AbstractScraper.Country.Canada:
				{
					return this.ProcessCanada(text, result, Location, Category);
				}
				case AbstractScraper.Country.UK:
				{
					return this.ProcessUK(text, result, Location, Category);
				}
				case AbstractScraper.Country.Australia:
				{
					return this.ProcessAustralia(text, result, Location, Category);
				}
				default:
				{
					return this.ProcessUSA(text, result, Location, Category);
				}
			}
		}
		protected override string GetUrl(SearchCategory scLocation, SearchCategory scCategory, string keyword, int pageNum)
		{
			string text = scLocation.Url;
			string url = scCategory.Url;
			string text2 = url.Split(new char[]
			{
				'/'
			}).LastOrDefault<string>();
			string text3 = string.Empty;
			text = UrlDownloader.UrlEncode(text);
			string text4 = scLocation.Meta ?? string.Empty;
			if (!string.IsNullOrEmpty(keyword))
			{
				text2 += UrlDownloader.UrlEncode(string.Format(" {0}", keyword));
			}
			string a;
			if ((a = text4) != null)
			{
				if (a == "canada")
				{
					this.country = AbstractScraper.Country.Canada;
					text3 = string.Format("http://ca.local.yahoo.com/results?csz={0}&stx={1}&pg_nm={2}", text.Replace("/", "+"), text2, pageNum);
					goto IL_15D;
				}
				if (a == "australia")
				{
					this.country = AbstractScraper.Country.Australia;
					text3 = string.Format("http://local.yahoo.com.au/search/{0}/{1}?search.offset={2}", text2, text.Replace("/", "+"), (pageNum - 1) * 20);
					goto IL_15D;
				}
				if (a == "uk")
				{
					this.country = AbstractScraper.Country.UK;
					text3 = string.Format("http://uk.local.yahoo.com/search.html?poi={0}&p={1}&cb={2}&output=html", text.Replace("/", "+"), text2, (pageNum - 1) * 10 + 1);
					goto IL_15D;
				}
			}
			text3 = string.Format("http://local.yahoo.com/results?csz={0}&stx={1}&pg_nm={2}", text.Replace("/", "+"), text2, pageNum);
			this.country = AbstractScraper.Country.USA;
			IL_15D:
			if (base.Radius > 0f)
			{
				text3 += string.Format("&radius={0}", base.Radius.ToString("#0.##"));
			}
			return text3;
		}
	}
}
