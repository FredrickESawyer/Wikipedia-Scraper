using ConsultingLeadsPro.Callbacks;
using ConsultingLeadsPro.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
namespace ConsultingLeadsPro.Scrapers
{
	internal abstract class AbstractMapScraper : AbstractScraper
	{
		public float Radius
		{
			get;
			set;
		}
		public AbstractMapScraper(UrlDownloader downloader, ScrapeResultCallback callback) : base(downloader, callback)
		{
		}
		protected override bool CanAddRes(List<IScrapeResult> result, IScrapeResult res)
		{
			int num = 0;
			Monitor.Enter(result);
			try
			{
				num = result.Count((IScrapeResult it) => (it as MapScrapeResult).Address == (res as MapScrapeResult).Address);
			}
			finally
			{
				Monitor.Exit(result);
			}
			return num <= 0 && base.CanAddRes(result, res);
		}
		protected void ParseAddress(MapScrapeResult res)
		{
			switch (this.country)
			{
				case AbstractScraper.Country.USA:
				{
					string[] array = res.Address.Split(new char[]
					{
						','
					});
					if (array.Length > 1)
					{
						res.City = array[1].Trim();
					}
					if (array.Length > 2)
					{
						string[] array2 = array[2].Trim().Split(new string[]
						{
							" "
						}, StringSplitOptions.RemoveEmptyEntries);
						if (array2.Length > 0)
						{
							res.Region = array2[0].Trim();
						}
						if (array2.Length > 1)
						{
							res.ZipCode = array2[1].Trim();
							return;
						}
					}
					break;
				}
				case AbstractScraper.Country.Canada:
				{
					string[] array = res.Address.Split(new char[]
					{
						','
					});
					if (array.Length > 1)
					{
						string text = array[array.Length - 2].Trim();
						int num = text.IndexOf(' ');
						if (num >= 0)
						{
							res.Region = text.Substring(0, num).Trim();
							res.ZipCode = text.Substring(num + 1).Trim();
						}
					}
					if (array.Length > 2)
					{
						res.City = array[array.Length - 3];
						return;
					}
					break;
				}
				case AbstractScraper.Country.UK:
				{
					res.ZipCode = base.ExtractValue(res.Address, "(?<value>[A-Z]{1,2}[0-9R][0-9A-Z]? ?[0-9][ABD-HJLNP-UW-Z]{2})");
					string[] array = res.Address.Split(new char[]
					{
						','
					});
					if (array.Length > 0)
					{
						res.Region = array[array.Length - 1].Trim();
					}
					if (array.Length > 1)
					{
						if (string.IsNullOrEmpty(res.ZipCode))
						{
							res.City = array[array.Length - 2].Trim();
							return;
						}
						res.City = array[array.Length - 2].Replace(res.ZipCode, string.Empty).Trim();
						return;
					}
					break;
				}
				case AbstractScraper.Country.Australia:
				{
					string[] array = res.Address.Split(new char[]
					{
						','
					});
					if (array.Length > 1)
					{
						string text2 = array[array.Length - 2].Trim();
						string[] array3 = text2.Split(new string[]
						{
							" "
						}, StringSplitOptions.RemoveEmptyEntries);
						int num2 = 2;
						if (array3.Length > 1)
						{
							res.Region = array3[array3.Length - 2];
						}
						else
						{
							num2 = 1;
						}
						if (array3.Length > 0)
						{
							res.ZipCode = array3[array3.Length - 1];
						}
						else
						{
							num2 = 0;
						}
						StringBuilder stringBuilder = new StringBuilder();
						for (int i = 0; i < array3.Length - num2; i++)
						{
							stringBuilder.Append(array3[i]);
							stringBuilder.Append(" ");
						}
						res.City = stringBuilder.ToString().Trim();
						return;
					}
					break;
				}
				default:
				{
					string[] array = res.Address.Split(new char[]
					{
						','
					});
					if (array.Length > 0)
					{
						res.Region = array[array.Length - 1].Trim();
					}
					if (array.Length > 2)
					{
						string[] array4 = array[array.Length - 2].Trim().Split(new string[]
						{
							" "
						}, StringSplitOptions.RemoveEmptyEntries);
						if (array4.Length > 0)
						{
							res.City = array4[0].Trim();
						}
						if (array4.Length > 1)
						{
							res.ZipCode = array4[1].Trim();
						}
					}
					break;
				}
			}
		}
	}
}
