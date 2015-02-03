using ConsultingLeadsPro.Scrapers;
using System;
using System.Collections.Generic;
namespace ConsultingLeadsPro.Callbacks
{
	internal interface EmailSendingCallback
	{
		void Init();
		void EmailSent(KeyValuePair<IScrapeResult, SmtpItem> sender);
		void AddTask(KeyValuePair<IScrapeResult, SmtpItem> sender, string status, DateTime schedule);
	}
}
