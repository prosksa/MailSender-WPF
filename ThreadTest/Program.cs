using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest
{
	class Program
	{
		static void Main(string[] args)
		{
			var thread_id = Thread.CurrentThread.ManagedThreadId;
			Console.WriteLine("Идентификатор главного потока id:{0}", thread_id);

			//ThreadTest1();
			//ThreadTest2();
			//ConcurentTest();
			//SynchronizationTest();
			//ThreadPoolTest();
			//ThreadMenegmentTest();

			//ConcurentQueue

			var server = new WebServer();
			server.ContextReceived += (s, e) =>
			{
				using (var writer = new StreamWriter(e.Context.Response.OutputStream))
				{
					writer.Write("123");
				}
				Console.WriteLine("Получено новое подключение!");
			};

			server.Start();

			lock (_SyncRoot) Console.WriteLine("Главный поток работу закончил");
			Console.ReadLine();
		}

		#region Поток
		private static void ThreadTest1()
		{
			var thread = new Thread(ThreadMethod); //new Thread(new ThreadStart(ThreadMethod));
			thread.Name = "Вторичный поток";
			thread.Priority = ThreadPriority.Highest;
			thread.Start();

			Console.WriteLine("Главный поток ...");

			//thread.Join();
		}

		private static void ThreadMethod()
		{
			Thread.Sleep(1000);
			Console.WriteLine("Вторичный поток запущен id:{0}", Thread.CurrentThread.ManagedThreadId);
			Thread.Sleep(1000);
			Console.WriteLine("Вторичный поток работу закончил id:{0}", Thread.CurrentThread.ManagedThreadId);
		}
		#endregion

		#region Параметризованный поток
		private static void ThreadTest2()
		{
			var thread = new Thread(ParametrizedThreadMethod); //new Thread(new ParameterizedThreadStart(ParametrizedThreadMethod));
			thread.Start(1500);
			Console.WriteLine("Поток с параметром запущен");
			thread.Join();
			Console.WriteLine("Поток с параметром завершился");
		}

		private static void ParametrizedThreadMethod(object parameter)
		{
			if (!(parameter is int int_parameter)) return;
			Console.WriteLine("Параметр потока id:{0} = {1}", Thread.CurrentThread.ManagedThreadId, int_parameter);
			Thread.Sleep(int_parameter);
		}
		#endregion

		private static void ConcurentTest()
		{
			var thread_list = new List<Thread>();

			for (var i = 0; i < 10; i++)
			{
				var thread = new Thread(ConcurentThreadMethod);
				//var thread = new Thread(SinchronizedMethod);
				thread_list.Add(thread);
				thread.Start();
			}
		}

		#region Синхронизация на основе lock
		private static readonly object _SyncRoot = new object();
		private static void ConcurentThreadMethod()
		{
			lock (_SyncRoot)
			{
				for (var i = 0; i < 10; i++)
				{
					Thread.Sleep(10);
					Console.Write("{0}, ", i);
				}
				Console.WriteLine();
			}
			//Monitor.Enter(_SyncRoot);
			//try
			//{
			//    for (var i = 0; i < 10; i++)
			//    {
			//        Thread.Sleep(10);
			//        Console.Write("{0}, ", i);
			//    }
			//    Console.WriteLine();
			//}
			//finally
			//{
			//    Monitor.Exit(_SyncRoot);
			//}
		}

		private static string SyncronizedProperty
		{
			get => "qwe";
			[MethodImpl(MethodImplOptions.Synchronized)]
			set => Console.WriteLine(value);
		}

		//[MethodImpl(MethodImplOptions.Synchronized)]
		//private static void SinchronizedMethod()
		//{
		//    for (var i = 0; i < 10; i++)
		//    {
		//        Thread.Sleep(10);
		//        Console.Write("{0}, ", i);
		//    }
		//    Console.WriteLine();
		//} 
		#endregion

		#region Примитивы синхронизации
		private static readonly AutoResetEvent _AutoResetEvent = new AutoResetEvent(false);
		//private static readonly ManualResetEvent _ManualResetEvent = new ManualResetEvent(false);
		private static void SynchronizationTest()
		{
			var thread = new Thread(SyncThreadTestMethod);
			thread.Start();

			Console.ReadLine();
			_AutoResetEvent.Set();

			//var mutex = new Mutex(true, "Имя мьютекса");
			//var semaphore = new Semaphore(10, 20);
			//semaphore.WaitOne();
			//// Критическая секция
			//semaphore.Release();

		}

		private static void SyncThreadTestMethod()
		{
			Console.WriteLine("Вторичный поток запущен");
			_AutoResetEvent.WaitOne();
			Console.WriteLine("Вторичный поток завершён");
		}
		#endregion

		#region Пул потоков

		private static void ThreadPoolTest()
		{
			ThreadPool.SetMaxThreads(20, 20);
			ThreadPool.SetMinThreads(15, 15);
			for (var i = 0; i < 50; i++)
			{
				ThreadPool.QueueUserWorkItem(ThreadPoolMethod, i);
			}
		}

		private static void ThreadPoolMethod(object parameter)
		{
			Thread.Sleep(2000);
			Console.WriteLine("Поток id:{0}, parameter:{1}", Thread.CurrentThread.ManagedThreadId, parameter);
		}

		#endregion

		#region Управление потоками

		private static bool _Enabled = true;
		private static void ThreadMenegmentTest()
		{
			var thread = new Thread(ThreadMenegmentMethod);
			thread.Start();

			Console.ReadLine();
			_Enabled = false;
			if (thread.IsAlive)
			{
				Console.WriteLine("Вторичный поток ещё работает...");
				if (!thread.Join(10))
				{
					Console.WriteLine("Вторичный поток всё ещё работает!!! Хотя прошло 10 мс");
					thread.Abort();
					//thread.Interrupt();
				}
			}

			//if(thread.IsAlive) thread.Abort();
			//if(thread.IsAlive && !thread.Join(100)) thread.Abort();

		}

		private static void ThreadMenegmentMethod()
		{
			Console.WriteLine("Вторичный поток запущен");
			for (var i = 0; i < 1000000000; i++)
			{
				Thread.Sleep(3000);
				Console.WriteLine(i);
				if (!_Enabled) return;
			}
		}

		#endregion
	}

	[Synchronization]
	internal class SynchronizedClass : ContextBoundObject
	{

	}
}
