using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace ThreadTestGUI
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void ButtonBase_OnClick(object Sender, RoutedEventArgs E)
		{
			var data = "Строковые данные";
			var thread = new Thread(() => ThreadMethod(data));
			thread.Start();
		}

		private void ThreadMethod(string parameter)
		{
			Thread.Sleep(3000);

			OutTextBlock.Dispatcher.Invoke(() => OutTextBlock.Text = parameter);
			Thread.Sleep(2000);

			for (var i = 0; i < 1000; i++)
			{
				var temp_i = i;
				OutTextBlock.Dispatcher.Invoke(() => OutTextBlock.Text = temp_i.ToString());
				Thread.Sleep(10);
			}
		}
	}
}
