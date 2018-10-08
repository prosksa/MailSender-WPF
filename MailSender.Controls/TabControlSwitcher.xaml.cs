using System;
using System.Collections.Generic;
using System.Linq;
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

namespace MailSender.Controls
{
	public partial class TabControlSwitcher : UserControl
	{
		public event EventHandler Forward;
		public event EventHandler Backward;

		public TabControlSwitcher() => InitializeComponent();

		private void OnForwardButton_Click(object sender, RoutedEventArgs e) => Forward?.Invoke(this, EventArgs.Empty);
		private void OnBackwardButton_Click(object sender, RoutedEventArgs e) => Backward?.Invoke(this, EventArgs.Empty);
	}
}
