using System;
using Atrico.Lib.Assertions;
using Atrico.Lib.Testing.NUnitAttributes;

namespace Atrico.Lib.Testing.Tests
{
	public class TestCatch : TestFixtureBase
	{
		private class CustomException : Exception
		{
		}

		[Test]
		public void TestCatchException()
		{
			// Arrange
			var msg = RandomValues.String();

			// Act
			var ex = Catch.Exception(() => { throw new Exception(msg); });

			// Assert
			Assert.That(ex, Is.TypeOf<Exception>(), "Exception type");
			Assert.That(ex.Message, Is.EqualTo(msg), "Message");
		}

		[Test]
		public void TestCatchNoException()
		{
			// Arrange

			// Act
			var ex = Catch.Exception(() => { });

			// Assert
			Assert.That(ex, Is.Null);
		}

		[Test]
		public void TestCatchCustomException()
		{
			// Arrange

			// Act
			var ex = Catch.Exception(() => { throw new CustomException(); });

			// Assert
			Assert.That(ex, Is.TypeOf<CustomException>());
		}
	}
}