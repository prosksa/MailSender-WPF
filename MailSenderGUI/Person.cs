using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MailSender.GUI
{
	/// <summary>
	/// Вторая часть объявления класса Persone из системы Linq2SQL
	/// </summary>
	public partial class Person : IDataErrorInfo
	{
		/// <summary>
		/// Меотд, вызываемый до изменения значения свойства. Здесь мы можем проконтролировать значение
		/// </summary>
		/// <param name="value">Изменяемое значение</param>
		partial void OnIdChanging(int value)
		{
			if (value <= 0)
				throw new ArgumentOutOfRangeException(nameof(value), "Значение id должно быть больше нуля");
		}

		// Ниже неявная реализация интерфейса IDataErrorInfo
		string IDataErrorInfo.Error => ""; // Общая информация об шибке в объекте

		string IDataErrorInfo.this[string PropertyName] // Система привязок через данных индексатор проверяет состояние ошибки для интересующих её свойств
		{
			get
			{
				switch (PropertyName)
				{
					// Если система проверяе свойство Name...
					case nameof(Name):
						if (string.IsNullOrWhiteSpace(_Name))
							return "Имя не указано";
						if (_Name.Length <= 3)
							return "Длина имени должна быть больше 3 символов";
						break;

					// Если система проверяе свойство Address...
					case nameof(Address):
						if (string.IsNullOrWhiteSpace(_Address))
							return "Адрес не указан";
						if (!Regex.IsMatch(_Address, @"[a-zA-Z][\w._]*@([a-zA-Z]+\.)+[a-zA-Z]{2,5}"))
							return "Ошибка формата адреса электронной почты";
						break;

				}

				return "";
			}
		}
	}
}
