using ConsultingLeadsPro.Callbacks;
using ConsultingLeadsPro.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
namespace ConsultingLeadsPro.Scrapers
{
	internal class GoogleScraper : AbstractMapScraper
	{
		public GoogleScraper(UrlDownloader downloader, ScrapeResultCallback callback) : base(downloader, callback)
		{
			this.initialPageNum = -1;
			base.Radius = 0f;
			this.allowsCategorylessSearch = true;
			this.campaign.SearchEngine = 5;
		}
		protected MapScrapeResult ParseDetails(string url, bool isRealEstate)
		{
			MapScrapeResult mapScrapeResult = new MapScrapeResult();
			mapScrapeResult.AdUrl = url;
			string text;
			if (this.downloader.DownloadUrl(url, out text))
			{
				if (this.terminated)
				{
					return mapScrapeResult;
				}
				mapScrapeResult.Headline = UrlDownloader.SkipHtmlTags(base.ExtractValue(text, "<span class=\"*?pp-place-title\"*?>(?<value>.*?)</span>")).Trim();
				mapScrapeResult.Address = UrlDownloader.SkipHtmlTags(base.ExtractValue(text, "<span.*?class=\"*?pp-headline-item pp-headline-address\"*?.*?>(?<value>.*?)</span>")).Trim();
				if (isRealEstate)
				{
					if (mapScrapeResult.Address.Trim().Length > 0)
					{
						mapScrapeResult.Address = mapScrapeResult.Headline + "," + mapScrapeResult.Address;
					}
					else
					{
						mapScrapeResult.Address = mapScrapeResult.Headline;
					}
					if (this.country == AbstractScraper.Country.UK)
					{
						MapScrapeResult expr_AE = mapScrapeResult;
						expr_AE.Address += ", UK";
					}
				}
				mapScrapeResult.Phone = base.ExtractValue(text, "<span.*?class=\"*?telephone\"*?.*?>.*?<nobr>(?<value>.*?)</nobr>").Trim();
				string text2 = base.ExtractValue(text, "<div.*?class=\"*?pp-compact-story\"*?.*?>(?<value>.*?)</div>");
				mapScrapeResult.Map = base.ExtractValue(text2, "src=\"*?(?<value>.*?)\"*?\\s");
				string text3 = base.ExtractValue(text, "<div.*?class=\"*?pp-story\"*?.*?>.*?Email.*?(?<value>.*?)</div>");
				mapScrapeResult.Email = base.ExtractEmails(text3).FirstOrDefault<string>();
				if (isRealEstate)
				{
					base.ExtractValue(text, "latlng:\\{(?<value>.*?)\\}");
					mapScrapeResult.Latitude = base.ExtractValue(text, "lat:(?<value>[-+]?[0-9]*\\.?[0-9]+)");
					mapScrapeResult.Longitude = base.ExtractValue(text, "lng:\\s*?(?<value>[-+]?[0-9]*\\.?[0-9]+)");
				}
				else
				{
					mapScrapeResult.Latitude = base.ExtractValue(text, "latitude_e6:(?<value>.*?),").Trim();
					int num = (mapScrapeResult.Latitude.StartsWith("-") || mapScrapeResult.Latitude.StartsWith("+")) ? 3 : 2;
					if (mapScrapeResult.Latitude.Length > num)
					{
						mapScrapeResult.Latitude = mapScrapeResult.Latitude.Insert(num, ".");
					}
					mapScrapeResult.Longitude = base.ExtractValue(text, "longitude_e6:(?<value>.*?),").Trim();
					num = ((mapScrapeResult.Longitude.StartsWith("-") || mapScrapeResult.Longitude.StartsWith("+")) ? 3 : 2);
					if (mapScrapeResult.Longitude.Length > num)
					{
						mapScrapeResult.Longitude = mapScrapeResult.Longitude.Insert(num, ".");
					}
				}
				base.ParseAddress(mapScrapeResult);
				mapScrapeResult.Radius = base.Radius.ToString();
				mapScrapeResult.Website = base.ExtractValue(text, "<span.*?class=\"?pp-authority-page\"?>.*?<a.*?href=\"?(?<value>.*?)\"?(\\s|>)").Trim();
			}
			return mapScrapeResult;
		}
		protected override bool ProcessSimpleRequest(string text, List<IScrapeResult> result, SearchCategory Location, SearchCategory Category)
		{
			RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline;
			Regex regex = new Regex("<span\\sclass=detls>.*?href=(?<href>.*?)\\s", options);
			MatchCollection matchCollection = regex.Matches(text);
			bool result2;
			foreach (Match match in matchCollection)
			{
				if (this.terminated)
				{
					result2 = false;
					return result2;
				}
				MapScrapeResult mapScrapeResult = this.ParseDetails(string.Format("http://maps.google.com{0}", match.Groups["href"].Value), Category.Url.Contains("Real+Estate"));
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
            return text.IndexOf("</div>Next</a>") >= 0;
			return result2;
		}
		protected override bool ProcessKeywordRequest(string text, List<IScrapeResult> result, SearchCategory Location, SearchCategory Category)
		{
			return this.ProcessSimpleRequest(text, result, Location, Category);
		}
		protected override string GetUrl(SearchCategory scLocation, SearchCategory scCategory, string keyword, int pageNum)
		{
			string text = scLocation.Meta ?? string.Empty;
			string arg = string.Format("{0},{1}", scLocation.Url.Replace("/", "+"), text);
			string arg2 = string.IsNullOrEmpty(keyword) ? scCategory.Url : string.Format("{0}+{1}", scCategory.Url, keyword);
			string text2 = string.Format("http://maps.google.com/maps?hl=en&num=20&q={0}&near={1}&start={2}", arg2, arg, pageNum * 20);
			if (base.Radius > 0f)
			{
				text2 += string.Format("&radius={0}", base.Radius.ToString("#0.##"));
			}
			string a;
			if ((a = text) != null)
			{
				if (a == "canada")
				{
					this.country = AbstractScraper.Country.Canada;
					return text2;
				}
				if (a == "australia")
				{
					this.country = AbstractScraper.Country.Australia;
					return text2;
				}
				if (a == "uk")
				{
					this.country = AbstractScraper.Country.UK;
					return text2;
				}
				if (a == "usa")
				{
					this.country = AbstractScraper.Country.USA;
					return text2;
				}
			}
			this.country = AbstractScraper.Country.Unknown;
			return text2;
		}
	}
}
