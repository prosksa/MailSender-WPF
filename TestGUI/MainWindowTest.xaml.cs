using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using MailSender;

namespace TestGUI
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindowTest : Window
	{
		public MainWindowTest()
		{
			InitializeComponent();
		}

		private void TestSendButton_Click(object sender, RoutedEventArgs e)
		{
			var user_name = UserName_TextBox.Text;
			SecureString password = Password_PasswordBox.SecurePassword;

			try
			{

				MailService.SendMail(user_name, password, "prosksa88@gmail.com", "nyurab@gmail.com", "Test message", "Body");

				MessageBox.Show("Сообщение отправлено!");

			}
			catch (Exception exc)
			{
				MessageBox.Show("Невозможно отправить письмо " + exc.Message, "Ошибка!");
			}


		}

	}
}
