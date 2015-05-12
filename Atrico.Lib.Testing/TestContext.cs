using System;
using Atrico.Lib.Assertions;
using Atrico.Lib.Assertions.Constraints;
using Atrico.Lib.Assertions.Elements;
using Atrico.Lib.Testing.NUnitAttributes;

namespace Atrico.Lib.Testing
{
    [TestFixture]
    public abstract class TestContext : TestFixtureBase
    {
        private Exception _exception;

        /// <summary>
        ///     Steps that are run before each test.
        /// </summary>
        [TestFixtureSetUp]
        public void BaseSetup()
        {
            Establish();
            _exception = Catch.Exception(Because);
        }

        /// <summary>
        ///     Steps that are run after each test.
        /// </summary>
        [TestFixtureTearDown]
        public void BaseTearDown()
        {
            TearDown();
        }

        /// <summary>
        ///     Sets up the environment for a specification context.
        /// </summary>
        protected abstract void Establish();

        /// <summary>
        ///     Acts on the context to create the observable condition.
        /// </summary>
        protected abstract void Because();

        /// <summary>
        ///     Cleans up the context after the specification is verified.
        /// </summary>
        protected virtual void TearDown()
        {
        }

        protected void ShouldNotHaveThrownException()
        {
            Assert.That(Value.Of(_exception).Is().Null(), _exception != null ? _exception.Message : "");
        }
    }
}