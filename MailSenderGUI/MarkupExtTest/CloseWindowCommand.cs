using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xaml;

namespace MailSender.GUI.MarkupExtTest
{
	/// <summary>Расширение разметки - команда для закрытия окна</summary>
	[MarkupExtensionReturnType(typeof(CloseWindowCommand))] // Так мы можем сообщить системе компоновки XAML какой тип будет возвращён в результате работы расширения разметки
	public class CloseWindowCommand : MarkupExtension, ICommand
	{
		private Window _CurrentWindow; // Здесь мы сохраним текущее окно

		/// <summary>
		/// Метод будет вызван при "раскрытии" расширения разметки
		/// </summary>
		/// <param name="sp">Менеджер сервисов - позволяет получить сервисы по указанному типу</param>
		/// <returns>Объект, который будет внедрён в разметку</returns>
		public override object ProvideValue(IServiceProvider sp)
		{
			var target_service = sp.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget; // Получаем сервис, позволяющий определить в каком объекте и в каком свойстве этого объекта сработало данное расширение разметки
			var obj = target_service?.TargetObject;
			var property = target_service?.TargetProperty;

			var root_service = sp.GetService(typeof(IRootObjectProvider)) as IRootObjectProvider; // Позволяет получить текущий корень логического дерева
			var root_object = root_service?.RootObject;
			_CurrentWindow = root_object as Window; // Он может быть окном, а может быть другим элементом

			return this; // Мы говорим, что в результате работы даннного расширения разметки вставить текущий экземпляр
		}

		#region ICommand
		/// <summary>
		/// Событие генерируется при смене состояния команды "можно/нельзя выполнять"
		/// </summary>
		public event EventHandler CanExecuteChanged
		{
			add => CommandManager.RequerySuggested += value;    // Делегируем переданный обработчик события событию менеджера команд
			remove => CommandManager.RequerySuggested -= value;
		}

		/// <summary>
		/// Данный метод вызывается что бы проверить - можно ли выполнить команду
		/// </summary>
		/// <param name="parameter">Параметр, который может быть передан команде при её выполнении</param>
		/// <returns>Истина, если команда может быть выполнена</returns>
		public bool CanExecute(object parameter)
		{
			if (parameter is Window) return true;
			if (_CurrentWindow != null) return true;
			if (Application.Current.MainWindow != null) return true;
			return false;
		}

		/// <summary>
		/// Метод, выполняемый командой
		/// </summary>
		/// <param name="parameter"></param>
		public void Execute(object parameter)
		{
			var window = parameter as Window ?? _CurrentWindow ?? Application.Current.MainWindow;
			window?.Close();
		}
		#endregion
	}
}
