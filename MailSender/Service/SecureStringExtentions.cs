using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Service
{
	public static class SecureStringExtentions
	{
		public static string GetString(this SecureString str)
		{
			var ptr = IntPtr.Zero;
			try
			{
				return Marshal.PtrToStringUni(ptr = Marshal.SecureStringToGlobalAllocUnicode(str));
			}
			finally
			{
				Marshal.ZeroFreeGlobalAllocUnicode(ptr);
			}
		}

		public static SecureString ToSecureString(this string str)
		{
			var result = new SecureString();
			foreach (var c in str)
				result.AppendChar(c);

			return result;
		}

		public static byte[] MD5(this SecureString str) => str.GetString().MD5();
		public static byte[] MD5(this SecureString str, Encoding encoding) => str.GetString().MD5(encoding);

		public static byte[] MD5(this string str) => str.MD5(Encoding.Default);
		public static byte[] MD5(this string str, Encoding encoding)
		{
			var hasher = System.Security.Cryptography.MD5.Create();
			return hasher.ComputeHash(encoding.GetBytes(str));
		}
	}
}
