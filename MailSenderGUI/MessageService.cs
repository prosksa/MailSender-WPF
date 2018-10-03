using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MailSender.GUI
{
	public class MessageService
	{
		public static MessageServiceWindow msw = new MessageServiceWindow();
		public static void Show(string message)
		{
			msw.TextBlock.Text = message;
			msw.ShowDialog();
		}
		public static void Show(string message, string title)
		{
			msw.TextBlock.Text = message;
			msw.Title = title;
			msw.ShowDialog();
		}
		public static void Show(string message, string title, MessageBoxButton button)
		{
			msw.TextBlock.Text = message;
			msw.Title = title;
			MessageBoxButton mswButton = button;
			msw.ShowDialog();

		}

	}
}
