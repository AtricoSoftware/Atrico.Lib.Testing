using System.Diagnostics;
using Atrico.Lib.Assertions;
using Atrico.Lib.Assertions.Constraints;
using Atrico.Lib.Assertions.Elements;
using Atrico.Lib.Testing.NUnitAttributes;

namespace Atrico.Lib.Testing.Tests.NUnitAttributes
{
    [TestFixture]
    public class TestRangeAttribute : TestFixtureBase
    {
        [Test]
        public void TestChar([Range('2', '6')] char value)
        {
            // Assert
            Debug.WriteLine("Value - {0} ({1})", value, value.GetType().Name);
            Assert.That(Value.Of(value).Is().Between('2', '6'), "Within range");
        }

        [Test]
        public void TestByte([Range(0x10, 0x20)] byte value)
        {
            // Assert
            Debug.WriteLine("Value - {0} ({1})", value, value.GetType().Name);
            Assert.That(Value.Of(value).Is().Between((byte)0x10, (byte)0x20), "Within range");
        }

        [Test]
        public void TestSbyte([Range(-2, +2)] sbyte value)
        {
            // Assert
            Debug.WriteLine("Value - {0} ({1})", value, value.GetType().Name);
            Assert.That(Value.Of(value).Is().Between((sbyte)-2, (sbyte)+2), "Within range");
        }

        [Test]
        public void TestInt([Range(-2, +6)] int value)
        {
            // Assert
            Debug.WriteLine("Value - {0} ({1})", value, value.GetType().Name);
            Assert.That(Value.Of(value).Is().Between(-2, 6), "Within range");
        }

        [Test]
        public void TestUint([Range(2u, 6u)] uint value)
        {
            // Assert
            Debug.WriteLine("Value - {0} ({1})", value, value.GetType().Name);
            Assert.That(Value.Of(value).Is().Between(2u, 6u), "Within range");
        }

        [Test]
        public void TestLong([Range(-2L, 6L)] long value)
        {
            // Assert
            Debug.WriteLine("Value - {0} ({1})", value, value.GetType().Name);
            Assert.That(Value.Of(value).Is().Between(-2L, 6L), "Within range");
        }

        [Test]
        public void TestUlong([Range(2UL, 6UL)] ulong value)
        {
            // Assert
            Debug.WriteLine("Value - {0} ({1})", value, value.GetType().Name);
            Assert.That(Value.Of(value).Is().Between(2ul, 6ul), "Within range");
        }

        [Test]
        public void TestFloat([Range(2.5f, 6.5f)] float value)
        {
            // Assert
            Debug.WriteLine("Value - {0} ({1})", value, value.GetType().Name);
            Assert.That(Value.Of(value).Is().Between(2.5f, 6.5f), "Within range");
        }

        [Test]
        public void TestDouble([Range(2.5, 6.5)] double value)
        {
            // Assert
            Debug.WriteLine("Value - {0} ({1})", value, value.GetType().Name);
            Assert.That(Value.Of(value).Is().Between(2.5, 6.5), "Within range");
        }

        //[Test]
        //public void TestDecimal([Range(2m, 6m)] decimal value)
        //{
        //    // Assert
        //    Debug.WriteLine("Value - {0} ({1})", value, value.GetType().Name);
        //    Assert.That(Value.Of(value).Is().Between<decimal>(2m, 6m), "Within range");
        //}
    }
}