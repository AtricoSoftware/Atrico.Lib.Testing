using System;
using Atrico.Lib.Assertions;
using Atrico.Lib.Assertions.Constraints;
using Atrico.Lib.Assertions.Elements;
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
            Assert.That(Value.Of(ex).Is().TypeOf(typeof (Exception)), "Exception type");
            Assert.That(Value.Of(ex.Message).Is().EqualTo(msg), "Message");
        }

        [Test]
        public void TestCatchNoException()
        {
            // Arrange

            // Act
            var ex = Catch.Exception(() => { });

            // Assert
            Assert.That(Value.Of(ex).Is().Null());
        }

        [Test]
        public void TestCatchCustomException()
        {
            // Arrange

            // Act
            var ex = Catch.Exception(() => { throw new CustomException(); });

            // Assert
            Assert.That(Value.Of(ex).Is().TypeOf(typeof (CustomException)));
        }
    }
}
