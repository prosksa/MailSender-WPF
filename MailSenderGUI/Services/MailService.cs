using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Security;
using System.Linq;

namespace MailSender.GUI
{
	public class MailService
	{
		private string userName;
		private SecureString password;
		//private string From;
		private string destination;
		private string subject;
		private string body;
		private string smtpHost;
		private int smtpPort;

		public MailService(string _userName, SecureString _password)
		{
			userName = _userName;
			password = _password;
		}

		public void SendMail(string destination)
		{
			using (MailMessage message = new MailMessage(userName, destination)
			{
				Subject = subject,
				Body = body
			})
			using (SmtpClient client = new SmtpClient(smtpHost, smtpPort)
			{
				EnableSsl = true,
				Credentials = new NetworkCredential(userName, password)
			}) 
			client.Send(message);

		}

		//public void SendMails(IQueryable<Email> emails)
		//{
		//	foreach (Email email in emails)
		//	{
		//		SendMail(email.Email, email.Name);
		//	}
		//}
	}
}
