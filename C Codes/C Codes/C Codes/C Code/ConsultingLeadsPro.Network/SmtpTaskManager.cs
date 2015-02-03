using ConsultingLeadsPro.Callbacks;
using ConsultingLeadsPro.Scrapers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
namespace ConsultingLeadsPro.Network
{
	internal class SmtpTaskManager
	{
		protected class SpinItem
		{
			protected List<string> spin = new List<string>();
			public string Text1
			{
				get;
				set;
			}
			public string Text2
			{
				get;
				set;
			}
			public List<string> Spin
			{
				get
				{
					return this.spin;
				}
			}
		}
		protected string subject;
		protected string message;
		protected int pendingCount;
		protected int perHour;
		protected Timer timer;
		protected LinkedList<SmtpItem> accounts = new LinkedList<SmtpItem>();
		protected List<IScrapeResult> recipients = new List<IScrapeResult>();
		protected Dictionary<SmtpItem, Tuple<DateTime, Queue<IScrapeResult>>> tsks = new Dictionary<SmtpItem, Tuple<DateTime, Queue<IScrapeResult>>>();
		protected List<SmtpTaskManager.SpinItem> messageParts = new List<SmtpTaskManager.SpinItem>();
		protected List<SmtpTaskManager.SpinItem> subjectParts = new List<SmtpTaskManager.SpinItem>();
		protected Random randomizer = new Random();
		public Campaign SearchCampaign
		{
			get;
			set;
		}
		public EmailSendingCallback Callback
		{
			get;
			set;
		}
		public LogCallback ErrorCallback
		{
			get;
			set;
		}
		public LinkedList<SmtpItem> Accounts
		{
			get
			{
				return this.accounts;
			}
		}
		public List<IScrapeResult> Recipients
		{
			get
			{
				return this.recipients;
			}
		}
		public string Message
		{
			get
			{
				return this.message;
			}
			set
			{
				this.message = value;
				if (this.message == null)
				{
					this.message = string.Empty;
				}
				this.ParseParts(this.message, this.messageParts);
			}
		}
		public string Subject
		{
			get
			{
				return this.subject;
			}
			set
			{
				this.subject = value;
				if (this.subject == null)
				{
					this.subject = string.Empty;
				}
				this.ParseParts(this.subject, this.subjectParts);
			}
		}
		public int PerHour
		{
			get
			{
				return this.perHour;
			}
			set
			{
				this.perHour = value;
			}
		}
		public SmtpTaskManager()
		{
			this.Message = string.Empty;
			this.Subject = string.Empty;
			this.perHour = 1;
		}
		protected void ParseParts(string text, List<SmtpTaskManager.SpinItem> parts)
		{
			RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline;
			Regex regex = new Regex("(?<text1>.*?)\\{(?<spin>.*?)\\}(?<text2>.*?)", options);
			parts.Clear();
			MatchCollection matchCollection = regex.Matches(text);
			if (matchCollection.Count > 0)
			{
				foreach (Match match in matchCollection)
				{
					SmtpTaskManager.SpinItem spinItem = new SmtpTaskManager.SpinItem();
					string[] array = match.Groups["spin"].Value.Split(new char[]
					{
						'|'
					});
					for (int i = 0; i < array.Length; i++)
					{
						string item = array[i];
						spinItem.Spin.Add(item);
					}
					spinItem.Text1 = match.Groups["text1"].Value;
					spinItem.Text2 = match.Groups["text2"].Value;
					parts.Add(spinItem);
				}
				Match match2 = matchCollection[matchCollection.Count - 1];
				SmtpTaskManager.SpinItem spinItem2 = parts.Last<SmtpTaskManager.SpinItem>();
				if (spinItem2 != null)
				{
					spinItem2.Text2 = text.Substring(match2.Index + match2.Length);
					return;
				}
			}
			else
			{
				parts.Add(new SmtpTaskManager.SpinItem
				{
					Text1 = text, 
					Text2 = string.Empty
				});
			}
		}
		protected string GetMessageBody()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (SmtpTaskManager.SpinItem current in this.messageParts)
			{
				stringBuilder.Append(current.Text1);
				if (current.Spin.Count > 0)
				{
					stringBuilder.Append(current.Spin[this.randomizer.Next(current.Spin.Count)]);
				}
				stringBuilder.Append(current.Text2);
			}
			return stringBuilder.ToString();
		}
		protected string GetMessageSubject()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (SmtpTaskManager.SpinItem current in this.subjectParts)
			{
				stringBuilder.Append(current.Text1);
				if (current.Spin.Count > 0)
				{
					stringBuilder.Append(current.Spin[this.randomizer.Next(current.Spin.Count)]);
				}
				stringBuilder.Append(current.Text2);
			}
			return stringBuilder.ToString();
		}
		public MailMessage GetMailMessage(string from, string to)
		{
			MailMessage mailMessage;
			MailMessage result;
			try
			{
				mailMessage = new MailMessage(from, to);
			}
			catch
			{
				result = null;
				return result;
			}
			try
			{
				mailMessage.Subject = this.GetMessageSubject();
			}
			catch
			{
				mailMessage.Subject = " ";
			}
			string text = this.GetMessageBody();
			AlternateView item = AlternateView.CreateAlternateViewFromString(UrlDownloader.SkipHtmlTags(text).Trim(), null, "text/plain");
			RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline;
			Regex regex = new Regex("<IMG.*?src=\"(?<imgpath>.*?)\".*?>", options);
			int num = 0;
			List<LinkedResource> list = new List<LinkedResource>();
			MatchCollection matchCollection = regex.Matches(text);
			foreach (Match match in matchCollection)
			{
				if (!match.Groups["imgpath"].Value.ToLower().Trim().StartsWith("http://"))
				{
					LinkedResource linkedResource = new LinkedResource(match.Groups["imgpath"].Value);
					linkedResource.ContentId = string.Format("img{0}", num++);
					text = text.Replace(match.Groups["imgpath"].Value, string.Format("cid:{0}", linkedResource.ContentId));
					list.Add(linkedResource);
				}
			}
			AlternateView alternateView = AlternateView.CreateAlternateViewFromString(text, null, "text/html");
			foreach (LinkedResource current in list)
			{
				alternateView.LinkedResources.Add(current);
			}
			mailMessage.AlternateViews.Add(item);
			mailMessage.AlternateViews.Add(alternateView);
			return mailMessage;
			return result;
		}
		protected SmtpClient GetClient(SmtpItem si)
		{
			SmtpClient smtpClient = new SmtpClient(si.SmtpServer, si.Port);
			if (si.NeedsAuth)
			{
				smtpClient.Credentials = new NetworkCredential(si.Username, si.Password);
				smtpClient.EnableSsl = si.UseSsl;
			}
			return smtpClient;
		}
		public void Send(SmtpItem si, MailMessage email)
		{
			SmtpClient client = this.GetClient(si);
			client.Send(email);
		}
		public void PerformSend(object stateInfo)
		{
			bool flag = true;
			foreach (KeyValuePair<SmtpItem, Tuple<DateTime, Queue<IScrapeResult>>> current in this.tsks)
			{
				if (current.Value.Value.Count > 0)
				{
					SmtpItem key = current.Key;
					SmtpClient client = this.GetClient(key);
					client.SendCompleted += new SendCompletedEventHandler(this.SendCompleted);
					this.pendingCount++;
					IScrapeResult scrapeResult = null;
					Queue<IScrapeResult> value;
					Monitor.Enter(value = current.Value.Value);
					try
					{
						scrapeResult = current.Value.Value.Dequeue();
					}
					finally
					{
						Monitor.Exit(value);
					}
					string email = scrapeResult.GetEmail();
					MailMessage mailMessage = this.GetMailMessage(key.EmailAddress, email);
					flag = false;
					try
					{
						if (this.ErrorCallback != null)
						{
							this.ErrorCallback.Log(string.Format("Sending message to {0} from {1}", email, key.EmailAddress));
						}
						client.SendAsync(mailMessage, new KeyValuePair<IScrapeResult, SmtpItem>(scrapeResult, key));
					}
					catch (Exception arg_EE_0)
					{
						Exception exception = arg_EE_0;
						if (this.ErrorCallback != null)
						{
							this.ErrorCallback.Log(exception.Message);
						}
						this.ProcessSendCompleted(new KeyValuePair<IScrapeResult, SmtpItem>(scrapeResult, key), false);
					}
				}
			}
			if (flag && this.pendingCount <= 0)
			{
				this.Stop();
			}
		}
		public void ProcessSendCompleted(KeyValuePair<IScrapeResult, SmtpItem> kvp, bool success)
		{
			if (this.Callback != null)
			{
				this.Callback.EmailSent(kvp);
			}
			if (success)
			{
				this.recipients.Remove(kvp.Key);
				if (this.SearchCampaign != null)
				{
					IScrapeResult key = kvp.Key;
					if (key != null)
					{
						key.IsEmailSent = true;
						key.IsQueued = false;
						this.SearchCampaign.SentEmails.Add(new Campaign.SentEmail(kvp.Key.GetEmail(), DateTime.Now));
					}
				}
				if (this.ErrorCallback != null)
				{
					this.ErrorCallback.Log(string.Format("Message sent: from {0} to {1}", kvp.Value.EmailAddress, kvp.Key));
				}
			}
			else
			{
				int min = this.tsks.Min((KeyValuePair<SmtpItem, Tuple<DateTime, Queue<IScrapeResult>>> t) => t.Value.Value.Count);
				KeyValuePair<SmtpItem, Tuple<DateTime, Queue<IScrapeResult>>> keyValuePair = this.tsks.FirstOrDefault((KeyValuePair<SmtpItem, Tuple<DateTime, Queue<IScrapeResult>>> t) => t.Value.Value.Count == min);
				IScrapeResult key2 = kvp.Key;
				Queue<IScrapeResult> value;
				Monitor.Enter(value = keyValuePair.Value.Value);
				try
				{
					keyValuePair.Value.Value.Enqueue(key2);
				}
				finally
				{
					Monitor.Exit(value);
				}
				keyValuePair.Value.Key = keyValuePair.Value.Key.AddMilliseconds((double)(3600000 / this.perHour));
				if (this.Callback != null)
				{
					this.Callback.AddTask(kvp, "Error occured - resend", keyValuePair.Value.Key);
				}
			}
			this.pendingCount--;
		}
		public void SendCompleted(object sender, AsyncCompletedEventArgs e)
		{
			KeyValuePair<IScrapeResult, SmtpItem>? keyValuePair;
			try
			{
				keyValuePair = new KeyValuePair<IScrapeResult, SmtpItem>?((KeyValuePair<IScrapeResult, SmtpItem>)e.UserState);
			}
			catch
			{
				keyValuePair = new KeyValuePair<IScrapeResult, SmtpItem>?(default(KeyValuePair<IScrapeResult, SmtpItem>));
			}
			if (e.Error != null && this.ErrorCallback != null)
			{
				this.ErrorCallback.Log(e.Error.Message);
			}
			this.ProcessSendCompleted(keyValuePair.Value, !e.Cancelled && e.Error == null);
		}
		public void Start(bool falseStart)
		{
			this.Stop();
			if (this.Accounts.Count <= 0 || this.perHour <= 0)
			{
				return;
			}
			this.tsks.Clear();
			LinkedListNode<SmtpItem> linkedListNode = this.Accounts.First;
			if (linkedListNode == null)
			{
				return;
			}
			while (linkedListNode != null)
			{
				this.tsks.Add(linkedListNode.Value, new Tuple<DateTime, Queue<IScrapeResult>>(DateTime.Now, new Queue<IScrapeResult>()));
				linkedListNode = linkedListNode.Next;
			}
			linkedListNode = this.accounts.First;
			foreach (IScrapeResult current in this.Recipients)
			{
				if (this.tsks.ContainsKey(linkedListNode.Value))
				{
					this.tsks[linkedListNode.Value].Value.Enqueue(current);
				}
				linkedListNode = linkedListNode.Next;
				if (linkedListNode == null)
				{
					linkedListNode = this.Accounts.First;
				}
			}
			this.pendingCount = 0;
			int num = 3600000 / this.perHour;
			if (this.Callback != null)
			{
				this.Callback.Init();
				foreach (KeyValuePair<SmtpItem, Tuple<DateTime, Queue<IScrapeResult>>> current2 in this.tsks)
				{
					current2.Value.Key = DateTime.Now;
					foreach (IScrapeResult current3 in current2.Value.Value)
					{
						current2.Value.Key = current2.Value.Key.AddMilliseconds((double)num);
						this.Callback.AddTask(new KeyValuePair<IScrapeResult, SmtpItem>(current3, current2.Key), falseStart ? "Ready to start" : "Pending...", current2.Value.Key);
					}
				}
			}
			if (!falseStart)
			{
				this.timer = new Timer(new TimerCallback(this.PerformSend), null, 0, num);
			}
		}
		public void Stop()
		{
			if (this.timer != null)
			{
				this.timer.Dispose();
				this.timer = null;
				if (this.ErrorCallback != null)
				{
					this.ErrorCallback.Log("Message sending has been stopped.");
				}
			}
		}
	}
}
