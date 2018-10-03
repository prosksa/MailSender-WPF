using System;
using System.Diagnostics;
using MailSender.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailService.Tests
{
	[TestClass]
	public class PasswordServiceTest
	{
		/// <summary>
		/// Метод, который будет запущен перез началом выполнения каждого модульного текста
		/// </summary>
		[TestInitialize]
		public void TestInitialize() // Метод должен быть нестатическим, возвращаемое значение void. Параметров быть не должно!
		{
			Debug.WriteLine("Подготовка модульного теста");
		}

		/// <summary>
		/// Метод, который будет запущен перед началом выполнения всех тестов
		/// </summary>
		/// <param name="context">Контекст тестирования</param>
		[ClassInitialize]
		public static void ClassInitialize(TestContext context)
		{
			Debug.WriteLine("Подготовка класса модульного теста");
		}

		/// <summary>
		/// Модульный тест метода кодирования. Принимает на вход строку 123, ключ кодиро вания 1, в результате должно получиться 234
		/// </summary>
		[TestMethod, Description("Описание теста"), Timeout(1000)/*, ExpectedException(typeof(ApplicationException))*/]
		public void Encode_123_to_234_with_key_1()
		{
			// AAA подход:
			// Arrange:
			var str = "123";
			var key = 1;
			var expected = "234";

			//throw new ApplicationException("Примекр исключения");
			// Act:
			var actual = PasswordService.Encode(str, key);

			// Assert:
			Assert.AreEqual(expected, actual, "Тест увенчался провалом!");
			//CollectionAssert.
			//StringAssert.
		}

		[TestMethod]
		public void Decode_234_to_123_with_key_1()
		{
			// AAA подход:
			// Arrange:
			var str = "234";
			var key = 1;
			var expected = "123";

			// Act:
			var actual = PasswordService.Decode(str, key);

			// Assert:
			Assert.AreEqual(expected, actual, "Тест увенчался провалом!");
		}
	}
}
