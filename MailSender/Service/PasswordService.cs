using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Service
{
	public static class PasswordService
	{
		public static string Encode(string str, int key = 1)
		{
			if (string.IsNullOrEmpty(str)) return str;
			return new string(str.Select(c => (char)(c + key)).ToArray());
		}

		public static string Decode(string str, int key = 1)
		{
			if (string.IsNullOrEmpty(str)) return str;
			return new string(str.Select(c => (char)(c - key)).ToArray());
		}
	}
}
