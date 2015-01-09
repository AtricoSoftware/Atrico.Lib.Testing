using NUnit.Framework;

namespace Atrico.Lib.Testing
{
	[TestFixture]
	public abstract class TestFixtureBase
	{
		/// <summary>
		/// Random value generator
		/// </summary>
		protected static readonly RandomValueGenerator RandomValues = new RandomValueGenerator();
	}
}
