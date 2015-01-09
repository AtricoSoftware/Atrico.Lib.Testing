using NUnit.Framework;

namespace Atrico.Lib.Testing
{
	[TestFixture(typeof (char))]
	[TestFixture(typeof (string))]
	[TestFixture(typeof (byte))]
	[TestFixture(typeof (sbyte))]
	[TestFixture(typeof (short))]
	[TestFixture(typeof (ushort))]
	[TestFixture(typeof (int))]
	[TestFixture(typeof (uint))]
	[TestFixture(typeof (long))]
	[TestFixture(typeof (ulong))]
	[TestFixture(typeof (float))]
	[TestFixture(typeof (double))]
// ReSharper disable once InconsistentNaming
// ReSharper disable once UnusedTypeParameter
	public class TestPODTypes<T> : TestFixtureBase
	{
		[Test]
		// ReSharper disable once InconsistentNaming
		public void _TestFramework()
		{
			// Do nothing
		}
	}
}
