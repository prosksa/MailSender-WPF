using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.GUI.Services
{
	public interface IDataService
	{
		ObservableCollection<Person> GetPersones();

		int SavePerson(Person person);

	}

	public class DataBaseService : IDataService
	{
		private readonly MailSenderDBDataContext _Context = new MailSenderDBDataContext();

		public ObservableCollection<Person> GetPersones()
		{
			return new ObservableCollection<Person>(_Context.Person);
		}

		public int SavePerson(Person person)
		{
			_Context.Person.InsertOnSubmit(person);
			_Context.SubmitChanges();
			return person.Id;
		}

	}

	class FileDataBaseService : IDataService
	{
		public ObservableCollection<Person> GetPersones() => throw new NotImplementedException();

		public int SavePerson(Person person) => throw new NotImplementedException();

	}
}
