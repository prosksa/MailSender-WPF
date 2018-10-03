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

namespace MailSender.GUI.Views
{
	/// <summary>
	/// Логика взаимодействия для SavePersonView.xaml
	/// </summary>
	public partial class SavePersonView : UserControl
	{
		public SavePersonView()
		{
			InitializeComponent();
		}

		private void OnValidationError(object Sender, ValidationErrorEventArgs E)
		{
			if (!(Sender is Control control)) return;
			switch (E.Action)
			{
				case ValidationErrorEventAction.Added:
					control.ToolTip = E.Error.ErrorContent.ToString();
					break;
				case ValidationErrorEventAction.Removed:
					control.ToolTip = null;
					break;

			}
		}
	}
}
