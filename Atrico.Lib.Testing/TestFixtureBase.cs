namespace Atrico.Lib.Testing
{
    public abstract class TestFixtureBase
    {
        /// <summary>
        ///     Random value generator
        /// </summary>
        protected readonly RandomValueGenerator RandomValues = new RandomValueGenerator();
    }
}