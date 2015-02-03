using System;
using System.Collections.Generic;
namespace ConsultingLeadsPro.Scrapers
{
	public interface IScrapeResult
	{
		bool IsEmailSent
		{
			get;
			set;
		}
		bool IsSelected
		{
			get;
			set;
		}
		string Category
		{
			get;
			set;
		}
		string Headline
		{
			get;
			set;
		}
		List<string> Emails
		{
			get;
		}
		string Email
		{
			get;
			set;
		}
		string AdUrl
		{
			get;
			set;
		}
		bool IsQueued
		{
			get;
			set;
		}
		string GetEmail();
	}
}
