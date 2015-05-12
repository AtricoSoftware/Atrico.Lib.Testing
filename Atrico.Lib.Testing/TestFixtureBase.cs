using NUnit.Framework;

namespace Atrico.Lib.Testing
{
    [TestFixture]
    public abstract class TestFixtureBase
    {
        /// <summary>
        /// Random value generator
        /// </summary>
        protected readonly RandomValueGenerator RandomValues = new RandomValueGenerator();
    }
}
