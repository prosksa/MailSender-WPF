using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
// ReSharper disable ConsiderUsingConfigureAwait

namespace TaskTest
{
	static class Tasks
	{
		/// <summary>
		/// Пример работы с классом <see cref="Task"/>
		/// </summary>
		public static void Test()
		{
			//var task = new Task<int>(() => TaskMethod(42)); // Создаём "холодную" задачу
			//Console.WriteLine("Задача создана");
			//task.Start();                                   // Превращаем её в "горячую" - запускаем
			//Console.WriteLine("Задача запущена");

			////task.Wait();                                  // Ожидать завершения задачи можно с помощью метда Whait
			//Console.WriteLine("Задача завершена. Результат {0}", task.Result); // Либо просто запросив результат. Оба способа являются !!!нехорошими!!! так как могут приводить к мёртвым блокировкам

			// Занные для задачи
			var data = 42;
			var task = Task.Run(() => TaskMethod(data));
			//var task = Task.Factory.StartNew(object_data => TaskMethod((int)object_data), data);
			Console.WriteLine("Задача создана");
			var task2 = task.ContinueWith(t => Console.WriteLine("Задача {0} завершена c резульатом {1}", t.Id, t.Result)); // К любой задаче можно прикрепить продолжение, которое будет выполнено после завершения задачи. Дополнение, в свою очередь, тоже превращается в задачу.
			var fault_task = task.ContinueWith(t => Console.WriteLine("Задача завершиласьошибкой"), TaskContinuationOptions.OnlyOnFaulted); // Дополнение может быть сконфигурировано для выполнения в определённых случаях - на пример в случае неудачи выполнения задачи.

			Console.WriteLine("Задача завершена. Результат {0}", task.Result);
		}

		private static int TaskMethod(int data)
		{
			Console.WriteLine("Поток id {0} стартовал. Задача id {1}, Данные потока {2}",
				Thread.CurrentThread.ManagedThreadId,
				Task.CurrentId, // Задачи также имеют свой идентификатор
				data);
			Thread.Sleep(3000);
			Console.WriteLine("Поток id {0} завершился. Задача id {1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId);

			//throw new Exception("Ошибка выполнения задачи");

			return data * 2 - 4;
		}

		public static void Test2()
		{
			var data = Enumerable.Range(1, 11);

			var task_list = new List<Task<int>>();

			foreach (var item in data)
			{
				task_list.Add(Task.Run(() => TaskMethod(item))); // Иногда задачи имеет смысл группировать в массивы
			}

			Task.WaitAll(task_list.ToArray()); // И затем ожидать их выполнения. 
											   //Task.WaitAny(task_list.ToArray()); // Либо выполнения первой из них.

			for (var i = 0; i < task_list.Count; i++)
				Console.WriteLine("Результат задачи {0} = {1}", i, task_list[i].Result);
		}

		/// <summary>
		/// Пример правильного использования задач с помощью ключевых слов async - await 
		/// </summary>
		public static async void Test2Async()
		{
			//var data = 15;

			//var task = Task.Run(() => TaskMethod(data));

			////var result = task.Result;
			//var result = await task.ConfigureAwait(false);
			//Console.WriteLine("Результат = {0}", result);

			var data = Enumerable.Range(1, 11);

			var tasks = data.Select(i => Task.Run(() => TaskMethod(i)));

			var all_task = Task.WhenAll(tasks);
			var result = await all_task.ConfigureAwait(false);  // Ключевое слово await делит метод на синхронную и асинхронную части. Всё, что стоит после await превращается в продолжение задачи и будет выполнено, когда задача завершится.
																// .ConfigureAwait(false) - говорит о том, что продолжение задачи можно выполнить в любом доступном потоке. .ConfigureAwait(true) - говорит о том, что продолжение надо выполнить в том же контексте синхронизации, что и первая часть метода.


			for (int i = 0; i < result.Length; i++)
			{
				Console.WriteLine(result[i]);
			}

		}
	}
}
