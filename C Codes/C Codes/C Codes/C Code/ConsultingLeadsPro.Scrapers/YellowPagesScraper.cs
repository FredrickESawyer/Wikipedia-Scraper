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
	internal class YellowPagesScraper : AbstractMapScraper
	{
		public YellowPagesScraper(UrlDownloader downloader, ScrapeResultCallback callback) : base(downloader, callback)
		{
			this.initialPageNum = 0;
			base.Radius = 0f;
			this.allowsCategorylessSearch = true;
			this.campaign.SearchEngine = 6;
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
				mapScrapeResult.Website = base.ExtractValue(text, "\"webURL\":{.*?\"href\":\"(?<value>.*?)\"").Trim();
				mapScrapeResult.Map = base.ExtractValue(text, "<a id=\"interactiveMapPrint\" href='(?<value>.*?)'.*?>").Trim();
				mapScrapeResult.Latitude = base.ExtractValue(text, "\"latitude\":\\s*?(?<value>[-+]?[0-9]*\\.?[0-9]+)");
				mapScrapeResult.Longitude = base.ExtractValue(text, "\"longitude\":\\s*?(?<value>[-+]?[0-9]*\\.?[0-9]+)");
				text = base.ExtractValue(text, "<div id=\"busCardLeft\">(?<value>.*?)</div>");
				mapScrapeResult.Phone = base.ExtractPhones(text).FirstOrDefault<string>();
				mapScrapeResult.Email = base.ExtractEmails(text).FirstOrDefault<string>();
				mapScrapeResult.Headline = UrlDownloader.SkipHtmlTags(base.ExtractValue(text, "<h1 id=\"ypBusCardBusName\">(?<value>.*?)</h1>")).Trim();
				mapScrapeResult.Address = base.ExtractValue(text, "<p>(?<value>.*?)</p>").Trim();
				string[] array = mapScrapeResult.Address.Split(new char[]
				{
					','
				});
				if (array.Length > 0)
				{
					string text2 = array[array.Length - 1].Trim();
					int num = text2.IndexOf(' ');
					if (num >= 0)
					{
						mapScrapeResult.Region = text2.Substring(0, num).Trim();
						mapScrapeResult.ZipCode = text2.Substring(num + 1).Trim();
					}
				}
				if (array.Length > 1)
				{
					mapScrapeResult.City = array[array.Length - 2].Trim();
				}
				if (!string.IsNullOrEmpty(mapScrapeResult.Map))
				{
					mapScrapeResult.Map = string.Format("http://www.yellowpages.ca{0}", mapScrapeResult.Map);
				}
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
				mapScrapeResult.Headline = UrlDownloader.SkipHtmlTags(base.ExtractValue(text, "<h1 class=\"fn org\">(?<value>.*?)</h1>")).Trim();
				string text2 = base.ExtractValue(text, "<p class=\"primary-location\">(?<value>.*?)</p>");
				if (string.IsNullOrEmpty(text2))
				{
					mapScrapeResult.Address = base.ExtractValue(text, "<span class=\"listing-address adr\">(?<value>.*?)</span>").Trim();
				}
				else
				{
					mapScrapeResult.Address = base.ExtractValue(text2, "<span class=\"street-address\">(?<value>.*?)</span>").Trim();
					mapScrapeResult.City = base.ExtractValue(text2, "<span class=\"locality\">(?<value>.*?)</span>");
					mapScrapeResult.Region = base.ExtractValue(text2, "<span class=\"region\">(?<value>.*?)</span>");
					mapScrapeResult.ZipCode = base.ExtractValue(text2, "<span class=\"postal-code\">(?<value>.*?)</span>");
				}
				mapScrapeResult.Email = base.ExtractEmails(text).FirstOrDefault<string>();
				mapScrapeResult.Phone = base.ExtractValue(text, "<p class=\"phone\">(?<value>.*?)</p>");
				string text3 = base.ExtractValue(text, "<ul class=\"feature-links blue-arrow\">(?<value>.*?)</ul>").Trim();
				mapScrapeResult.Website = base.ExtractValue(text3, "href=\"(?<value>.*?)\".*?Visit Website</a>").Trim();
				if (mapScrapeResult.Website.StartsWith("mailto:"))
				{
					mapScrapeResult.Website = string.Empty;
				}
				mapScrapeResult.Map = string.Format("http://www.yellowpages.com{0}", base.ExtractValue(text, "<div id=\"mip-minimap\">.*?href=\"(?<value>.*?)\".*?</div>").Trim());
				mapScrapeResult.Latitude = base.ExtractValue(text, "<span class=\"latitude\" id=\"map-latitude\">(?<value>.*?)</span>");
				mapScrapeResult.Longitude = base.ExtractValue(text, "<span class=\"longitude\" id=\"map-longitude\">(?<value>.*?)</span>");
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
				mapScrapeResult.Headline = base.ExtractValue(text, "<h1 class=\"listingName\">(?<value>.*?)</h1>");
				mapScrapeResult.Address = base.ExtractValue(text, "<span class=\"listingAddressText\">(?<value>.*?)</span>");
				base.ParseAddress(mapScrapeResult);
				mapScrapeResult.Latitude = base.ExtractValue(text, "latitude=\"(?<value>.*?)\"");
				mapScrapeResult.Longitude = base.ExtractValue(text, "longitude=\"(?<value>.*?)\"");
				mapScrapeResult.Map = string.Format("http://www.yellowpages.com.au/app/staticMap?markers={0},{1},1&width=261&height=175&type=bpp", mapScrapeResult.Latitude, mapScrapeResult.Longitude);
				mapScrapeResult.Website = base.ExtractValue(text, "<a class=\"webAddressLink\".*?href=\"(?<value>.*?)\"").Trim();
				if (!string.IsNullOrEmpty(mapScrapeResult.Website))
				{
					mapScrapeResult.Website = string.Format("http://www.yellowpages.com.au{0}", mapScrapeResult.Website);
				}
				mapScrapeResult.Phone = base.ExtractValue(text, "<div class=\"primaryPhoneNumber\">(?<value>.*?)</div>").Trim();
				mapScrapeResult.Email = base.ExtractEmails(text).FirstOrDefault<string>();
			}
			return mapScrapeResult;
		}
		protected bool ProcessCanada(string text, List<IScrapeResult> result, SearchCategory Location, SearchCategory Category)
		{
			RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline;
			Regex regex = new Regex("<div class=\"listingDetail\".*?>.*?<h3 class=\"listingTitleLine\">.*?href=\"(?<href>.*?)\".*?</h3>.*?<h4 class=\"phoneLink\">(?<phone>.*?)</h4>", options);
			MatchCollection matchCollection = regex.Matches(text);
			bool result2;
			foreach (Match match in matchCollection)
			{
				if (this.terminated)
				{
					result2 = false;
					return result2;
				}
				MapScrapeResult mapScrapeResult = this.ParseDetailsCanada(string.Format("http://www.yellowpages.ca{0}", match.Groups["href"].Value));
				mapScrapeResult.Phone = UrlDownloader.SkipHtmlTags(match.Groups["phone"].Value);
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
			return text.IndexOf(">Next</a>") >= 0;
			return result2;
		}
		protected bool ProcessUK(string text, List<IScrapeResult> result, SearchCategory Location, SearchCategory Category)
		{
			RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline;
			Regex regex = new Regex("<div class=\"advert-content\">(?<text>.*?)</div>.*?(?<cta><div class=\"advert-cta\">.*?</div>){1}.*?<ul class=\"tabbed\">(?<tab>.*?)</ul>{1}", options);
			MatchCollection matchCollection = regex.Matches(text);
			bool result2;
			foreach (Match match in matchCollection)
			{
				if (this.terminated)
				{
					result2 = false;
					return result2;
				}
				string value = match.Groups["text"].Value;
				string value2 = match.Groups["cta"].Value;
				string value3 = match.Groups["tab"].Value;
				MapScrapeResult mapScrapeResult = new MapScrapeResult();
				mapScrapeResult.AdUrl = string.Format("http://www.yell.com{0}", base.ExtractValue(value3, "<li class=\"summaryTL\">.*?href=\"(?<value>.*?)\".*?</li>"));
				mapScrapeResult.Map = string.Format("http://www.yell.com{0}", base.ExtractValue(value3, "<li class=\"mapTL\">.*?href=\"(?<value>.*?)\".*?</li>"));
				mapScrapeResult.Phone = UrlDownloader.SkipHtmlTags(base.ExtractValue(value2, "<ul class=\"(tel-single|tel-multiple)\">.*?<strong>(?<value>.*?)</strong>.*?</ul>")).Trim();
				mapScrapeResult.Headline = base.ExtractValue(value, "<h2 class=\"coName\">(?<value>.*?)</h2>");
				mapScrapeResult.Website = base.ExtractValue(mapScrapeResult.Headline, "href='(?<value>.*?)'").Trim();
				mapScrapeResult.Headline = UrlDownloader.SkipHtmlTags(mapScrapeResult.Headline).Trim();
				mapScrapeResult.Address = UrlDownloader.SkipHtmlTags(base.ExtractValue(value, "<p class=\"address\">(?<value>.*?)</p>")).Trim();
				mapScrapeResult.ZipCode = base.ExtractValue(mapScrapeResult.Address, "(?<value>[A-Z]{1,2}[0-9R][0-9A-Z]?\\s*?[0-9][ABD-HJLNP-UW-Z]{2})");
				string[] array = mapScrapeResult.Address.Split(new char[]
				{
					','
				});
				if (array.Length > 0)
				{
					mapScrapeResult.Region = array[array.Length - 1];
					if (mapScrapeResult.ZipCode.Length > 0)
					{
						mapScrapeResult.Region = mapScrapeResult.Region.Replace(mapScrapeResult.ZipCode, string.Empty).Trim();
					}
					else
					{
						mapScrapeResult.Region = mapScrapeResult.Region.Trim();
					}
				}
				if (array.Length > 1)
				{
					if (string.IsNullOrEmpty(mapScrapeResult.ZipCode))
					{
						mapScrapeResult.City = array[array.Length - 2].Trim();
					}
					else
					{
						mapScrapeResult.City = array[array.Length - 2].Replace(mapScrapeResult.ZipCode, string.Empty).Trim();
					}
				}
				if (this.terminated)
				{
					result2 = false;
					return result2;
				}
				if (this.CanAddRes(result, mapScrapeResult))
				{
					if (!string.IsNullOrEmpty(mapScrapeResult.Website))
					{
						string website;
						base.WalkWebsite(mapScrapeResult.Website, mapScrapeResult, out website);
						mapScrapeResult.Website = website;
					}
					mapScrapeResult.Category = Category.Name;
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
			return text.IndexOf(">Next</a>") >= 0;
			return result2;
		}
		protected bool ProcessAustralia(string text, List<IScrapeResult> result, SearchCategory Location, SearchCategory Category)
		{
			RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline;
			Regex regex = new Regex("<div class=\"listingName\">.*?href=\"(?<href>.*?)\"", options);
			MatchCollection matchCollection = regex.Matches(text);
			bool result2;
			foreach (Match match in matchCollection)
			{
				if (this.terminated)
				{
					result2 = false;
					return result2;
				}
				MapScrapeResult mapScrapeResult = this.ParseDetailsAustralia(string.Format("{0}{1}", "http://www.yellowpages.com.au", match.Groups["href"].Value));
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
			return text.IndexOf("id=\"link-page-next\"") >= 0;
			return result2;
		}
		protected override bool ProcessKeywordRequest(string text, List<IScrapeResult> result, SearchCategory Location, SearchCategory Category)
		{
			return this.ProcessSimpleRequest(text, result, Location, Category);
		}
		protected bool ProcessUSA(string text, List<IScrapeResult> result, SearchCategory Location, SearchCategory Category)
		{
			RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline;
			Regex regex = new Regex("<h3 class=\"business-name fn org\">.*?<a.*?href=\"(?<href>.*?)\".*?</a>.*?</h3>.*?<div class=\"distance\">(?<radius>.*?)</div>", options);
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
				mapScrapeResult.Radius = UrlDownloader.SkipHtmlTags(match.Groups["radius"].Value).Trim();
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
			return text.IndexOf("<li class=\"next\">") >= 0;
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
			string text = scLocation.Url.Replace(" ", "+");
			string text2 = scCategory.Url.Replace(" ", "+");
			string text3 = string.Empty;
			keyword = UrlDownloader.UrlEncode(keyword);
			string text4 = scLocation.Meta ?? string.Empty;
			string a;
			if ((a = text4) != null)
			{
				if (a == "canada")
				{
					this.country = AbstractScraper.Country.Canada;
					text2 = UrlDownloader.SkipHtmlTags(text2.Replace('-', ' '));
					text3 = (string.IsNullOrEmpty(keyword) ? string.Format("http://www.yellowpages.ca/search/?stype=si&what={0}&where={1}", text2, text) : string.Format("http://www.yellowpages.ca/search/?stype=si&what={0}+{1}&where={2}", text2, keyword, text));
					goto IL_1D9;
				}
				if (a == "australia")
				{
					if (base.Downloader != null)
					{
						base.Downloader.KeepAlive = false;
					}
					this.country = AbstractScraper.Country.Australia;
					text3 = (string.IsNullOrEmpty(keyword) ? string.Format("http://www.yellowpages.com.au/search/listings?clue={1}&locationClue={0}&x=0&y=0&pageNumber={2}", text, text2, pageNum) : string.Format("http://www.yellowpages.com.au/search/listings?clue={1}+{3}&locationClue={0}&x=0&y=0&pageNumber={2}", new object[]
					{
						text, 
						text2, 
						pageNum, 
						keyword
					}));
					goto IL_1D9;
				}
				if (a == "uk")
				{
					this.country = AbstractScraper.Country.UK;
					text3 = (string.IsNullOrEmpty(keyword) ? string.Format("http://www.yell.com/ucs/UcsSearchAction.do?keywords={0}&location={1}&pageNum={2}", text2, text, pageNum) : string.Format("http://www.yell.com/ucs/UcsSearchAction.do?keywords={0}+{1}&location={2}&pageNum={3}", new object[]
					{
						text2, 
						keyword, 
						text, 
						pageNum
					}));
					goto IL_1D9;
				}
			}
			text3 = (string.IsNullOrEmpty(keyword) ? string.Format("http://www.yellowpages.com/{0}/{1}?page={2}", text, text2, pageNum) : string.Format("http://www.yellowpages.com/{0}/{1}?q={2}&page={3}", new object[]
			{
				text, 
				text2, 
				keyword, 
				pageNum
			}));
			this.country = AbstractScraper.Country.USA;
			IL_1D9:
			if (base.Radius > 0f)
			{
				text3 += string.Format("&refinements[radius]={0}", base.Radius.ToString("#0.##"));
			}
			return text3;
		}
	}
}
