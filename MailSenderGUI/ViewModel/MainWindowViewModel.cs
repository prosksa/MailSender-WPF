using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MailSender.Controls;
using MailSender.GUI.Services;
using MailSender.GUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MailSender.GUI.ViewModel
{
	public class MainWindowViewModel : ViewModelBase
	{
		private readonly IDataService _DataService;

		private Person _CurrentPerson;

		private IEnumerable<Person> _Persones;

		//private readonly MailService _MailService;

		public RelayCommand ApplicationCloseCommand { get; }

		public RelayCommand LoadPersonesCommand { get; }

		public RelayCommand NewPersonCommand { get; }

		public RelayCommand<Person> SavePersonCommand { get; }

		//public RelayCommand SendEmailCommand { get; }

		public IEnumerable<Person> Persones
		{
			get => _Persones;
			//set
			//{
			//    if(Equals(_Persones, value)) return;
			//    _Persones = value;
			//    //RaisePropertyChanged(() => Persones);
			//    //RaisePropertyChanged(nameof(Persones));
			//    RaisePropertyChanged();
			//}
			set => Set(ref _Persones, value);
		}

		public Person CurrentPerson { get => _CurrentPerson; set => Set(ref _CurrentPerson, value); }


		public MainWindowViewModel(IDataService DataService)
		{
			_DataService = DataService;
			
			#region Создаём команды

			ApplicationCloseCommand = new RelayCommand(OnApplicationCloseCommandExecuted);

			LoadPersonesCommand = new RelayCommand(OnLoadPersonesExecuted);

			NewPersonCommand = new RelayCommand(() => CurrentPerson = new Person { Id = _DataService.GetPersones().Max(p => p.Id) + 1 });

			SavePersonCommand = new RelayCommand<Person>(OnSavePersonCommandExecuted);

			//SendEmailCommand = new RelayCommand(OnSendEmailCommandExecuted);

			#endregion
		}

		//private void OnSendEmailCommandExecuted() => _MailService.SendMail();

		private void OnSavePersonCommandExecuted(Person person) => _DataService.SavePerson(person);

		private void OnApplicationCloseCommandExecuted() => Application.Current.Shutdown();

		private void OnLoadPersonesExecuted() => Persones = _DataService.GetPersones();


	}
}
