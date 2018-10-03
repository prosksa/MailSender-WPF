using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender
{
	class Servers
	{
		public static Dictionary<string, int> ServersList { get; } = new Dictionary<string, int>
		{
			{ "smtp.gmail.com", 587 },
			{ "smtp.yandex.ru", 25 },
			{ "smtp.mail.ru", 25 },
			{ "smtp.outlook.com", 123 },
		};

	}
}
