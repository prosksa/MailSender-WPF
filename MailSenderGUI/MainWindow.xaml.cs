using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MailSender.Service;

namespace MailSender.GUI
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void OnTabSwitcher_Forward(object sender, EventArgs E)
		{
			MailTabControl.SelectedIndex++;
		}

		private void OnTabSwitcher_Backward(object sender, EventArgs E)
		{
			MailTabControl.SelectedIndex--;
		}

		//private void Button_Click(object sender, RoutedEventArgs e)
		//{
		//	var userName = SenderName.SelectedItem.ToString();
		//	var ind = SenderName.SelectedIndex;
		//	SecureString password = "".ToSecureString();

		//	foreach (KeyValuePair<string, string> d in Senders.SendersList)
		//	{
		//		if (d.Key == userName)
		//			password = PasswordService.Decode(d.Value).ToSecureString();
		//	}

		//	MailService mailService = new MailService(userName, password);

		//	SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

		//	string destination = "prosksa88@gmail.com";

			//try
			//{
			//	MailService.SendMail(destination);

			//	MessageBox.Show("Message sent!");

			//}
			//catch (Exception exc)
			//{
			//	MessageBox.Show("Невозможно отправить письмо " + exc.ToString());
			//	//MessageBox.Show(exc.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
			//}
		//}
	}
}
