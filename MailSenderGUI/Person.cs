using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MailSender.GUI
{
	public partial class Person : IDataErrorInfo
	{
		partial void OnIdChanging(int value)
		{
			if (value <= 0)
				throw new ArgumentOutOfRangeException(nameof(value), "Значение id должно быть больше нуля");
		}

		string IDataErrorInfo.Error => ""; 

		string IDataErrorInfo.this[string PropertyName] 
		{
			get
			{
				switch (PropertyName)
				{
					case nameof(Name):
						if (string.IsNullOrWhiteSpace(_Name))
							return "Имя не указано";
						if (_Name.Length <= 3)
							return "Длина имени должна быть больше 3 символов";
						break;

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
