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

	public partial class ToolBarControl
	{
		public static readonly DependencyProperty TextProperty =
			DependencyProperty.Register(
				nameof(Text),
				typeof(string),
				typeof(ToolBarControl),
				new PropertyMetadata("Панель инструментов"));

		public string Text
		{
			get => (string)GetValue(TextProperty);
			set => SetValue(TextProperty, value);
		}

		public static readonly DependencyProperty AddCommandProperty =
			DependencyProperty.Register(
				nameof(AddCommand),
				typeof(ICommand),
				typeof(ToolBarControl),
				new PropertyMetadata(default(ICommand)));

		public ICommand AddCommand
		{
			get => (ICommand)GetValue(AddCommandProperty);
			set => SetValue(AddCommandProperty, value);
		}


		public ToolBarControl() => InitializeComponent();
	}
}
