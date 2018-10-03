using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskTest
{
	class Program
	{
		static void Main(string[] args)
		{
			//var data = 15;
			////var thread = new Thread(() => ThreadMethod(data));  // Создание параллельного потока с параметром, переданным через конструктор
			////thread.Start();

			////thread.Join();   // Ожидание завершения потока

			//Action<int> action = ThreadMethod;                        // Можо создать делегат на основе любого метода
			////action.BeginInvoke(data, null, null);                   // И запустить его параллельно, передав в него все необъодимые параметры, а также метод, который будет вызыван по завершении асинхронной операции и объект-параметр (может быть всё что угодно)
			//action.BeginInvoke(data, OnThreadMethodCompleted, null);

			//var web_client = new WebClient();                                             // Пример использования асинхронной операции для скачивания данных из сети
			//Func<string, string> downloader = web_client.DownloadString;
			//downloader.BeginInvoke("http://www.ya.ru", OnDownloadCompleted, downloader);

			//var bw = new BackgroundWorker();   // Объект - менеджер параллельного процесса. Позволяет детально контролировать ход выполнения параллельного процесса: передавать информацию о прогрессе, о прерывании, о ошибках, о завершении
			//bw.DoWork += (s, e) => { };        // Асинзронная операция устанавливается как обработчик события. МОжно выполнить сразу несколько операций
			//bw.RunWorkerAsync();

			//TPL.Test();
			//Tasks.Test();
			//Tasks.Test2();
			Tasks.Test2Async();

			Console.WriteLine("Главный поток завершён");

			Console.ReadLine();
		}

		private static void OnDownloadCompleted(IAsyncResult result)
		{
			var downloader = (Func<string, string>)result.AsyncState;
			var txt = downloader.EndInvoke(result);
			Console.WriteLine(txt);
		}

		private static void ThreadMethod(int data)
		{
			Console.WriteLine("Поток id {0} стартовал. Данные потока {1}", Thread.CurrentThread.ManagedThreadId, data);
			Thread.Sleep(3000);
			Console.WriteLine("Поток id {0} завершился.", Thread.CurrentThread.ManagedThreadId);
		}

		private static void OnThreadMethodCompleted(IAsyncResult result)
		{
			Console.WriteLine("Вызван метод-продолжение в потоке id {0}", Thread.CurrentThread.ManagedThreadId);
		}

	}
}
