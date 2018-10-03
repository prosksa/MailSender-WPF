using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ThreadTest
{
	/// <summary>
	/// Класс http-web-сервера
	/// </summary>
	/// <remarks>
	/// Для полноценной работы дтребуется прописать разрешения на открытие маршрутов http в вистеме
	///     netsh http add urlacl url=http://+:80/ user=USER_NAME
	///     netsh http add urlacl url=http://*:80/ user=USER_NAME
	/// И разрешить фаерволу пропускать входящие соединения
	///     netsh advfirewall firewall add rule name="Название_правила" dir=in action=allow protocol=TCP localport=80
	/// Команды вводятся в консоли с правами администратора
	/// В противном случае для запуска сервера потребуются права администратора для всего приложения.
	/// А отладка должна происходить в Visual Studio, загруженной с правами администратора
	/// </remarks>
	public class WebServer
	{
		/// <summary>
		/// Аргумент события, возникающего при получении нового входящего соединения
		/// </summary>
		public class ContextReceivedEventArgs : EventArgs
		{
			/// <summary>
			/// Контекст входящего соединения
			/// </summary>
			public HttpListenerContext Context { get; set; }
		}

		/// <summary>
		/// Событие, возникающее при получении входящего соединения
		/// </summary>
		public event EventHandler<ContextReceivedEventArgs> ContextReceived;

		/// <summary>Генерация события получения входящего соединения</summary>
		/// <param name="context">Контекст входящего соединения</param>
		protected virtual void OnContextReceived(HttpListenerContext context)
		{
			ContextReceived?.Invoke(this, new ContextReceivedEventArgs { Context = context });
		}

		/// <summary>
		/// Порт сервера
		/// </summary>
		private readonly int _Port;
		/// <summary>
		/// Признак активности сервера
		/// </summary>
		private bool _Enabled;
		/// <summary>
		/// Объект синхронизации потоков при запуске и остановке сервера
		/// </summary>
		private readonly object _SyncRoot = new object();
		/// <summary>
		/// Объет работы с драйвером http.sys для получения входящих соединений
		/// </summary>
		private HttpListener _Listner;

		/// <summary>
		/// Активность сервера
		/// </summary>
		public bool Enabled
		{
			get => _Enabled;
			set { if (value) Start(); else Stop(); }
		}

		/// <summary>
		/// Инициализация нового веб-сервера
		/// </summary>
		/// <param name="port">Порт сервера</param>
		public WebServer(int port = 80) => _Port = port;

		/// <summary>
		/// Запуск сервера
		/// </summary>
		public void Start()
		{
			if (_Enabled) return; // Проверяем - если сервер уже запущен, то дальнейшая работа метода бессмыслена
			lock (_SyncRoot) // Блокируем критическую секцию. Внутрь блока синзронизации сможет попасть только один поток. Остальные остановятся на этой строке и образуют очередь на вход
			{
				if (_Enabled) return; // Очередной поток из очереди в заголовке секции синхронизации должен проверить - а вдруг впереди идущий поток уже запустил сервер? Тогда ему тут делать нечего.
				_Enabled = true; // Если сервер ещё не активен, то выставляем признак активности в истину.

				_Listner = new HttpListener(); // Создаём новый объект для работы с драйвером  http.sys 
				_Listner.Prefixes.Add($"http://+:{_Port}/"); // Добавляем префиксы с маршрутов, с которыми мы хотим работать.
				_Listner.Prefixes.Add($"http://*:{_Port}/");

				_Listner.Start(); // Запускаем процесс ожидания входящих подключений

				_Listner.BeginGetContext( // Запускаем процесс получение входящих подключений в асинхронном режиме
					ConnectionReceived,   //    указываем метод, Который надо вызвать когда будет обнаружено входящее подключение
					_Listner);            //    в этот метод передаём параметр - объект HttpListener (здесь можно передать всё что угодно).
			}
		}

		/// <summary>
		/// Остановка сервера
		/// </summary>
		public void Stop()
		{
			if (!_Enabled) return;       // Если сервер уже остановлен, то выходим
			lock (_SyncRoot)            // Блокируем критическую секцию (распологаем все потоки, вошедшие сюда, в очередь) 
			{
				if (!_Enabled) return;   // Для очередного потока, прошедшего в критическую секцию, проверяем - сервер ещё активен? Если нет, то выходим.
				_Enabled = false;       // Сбрасываем флаг активности сервера (критическая секция ещё заблокирована - другой поток не сможет его сейчас установить)

				_Listner.Stop();        // Останавливаем объект работы с драйвером
			}
		}

		/// <summary>
		/// Метод получения контекстов входящих подключений
		/// </summary>
		/// <param name="result">Результат асинхронной операции</param>
		private void ConnectionReceived(IAsyncResult result)
		{
			if (!_Enabled) return; // Если сервер не активен (флаг был сброшен), то просто ничего не делаем. Клиент будет считать, что соединение установлено не было.

			var listner = (HttpListener)result.AsyncState; // Извлекаем параметр асинхронной операции - объект HttpListener, что бы иметь возможность вызвать завершение в следующей строке и получить данные 
			var context = listner.EndGetContext(result);    // Завершаем асинхронную операцию и извлекаем из неё данные - контекст входящего соединения
			if (_Enabled) listner.BeginGetContext(ConnectionReceived, listner); // Если сервер ещё активен, то тутже (не тратя времени) запускаем новый синхронный процесс получения входящего соединения
			OnContextReceived(context); // Генерируем событие обработки входящего соединения
		}

	}
}
