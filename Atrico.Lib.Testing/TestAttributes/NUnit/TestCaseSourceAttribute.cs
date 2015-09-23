using System;

namespace Atrico.Lib.Testing.TestAttributes.NUnit
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class TestCaseSourceAttribute : global::NUnit.Framework.TestCaseSourceAttribute
	{
		public TestCaseSourceAttribute(Type sourceType) : base(sourceType)
		{
		}

		public TestCaseSourceAttribute(Type sourceType, string sourceName) : base(sourceType, sourceName)
		{
		}

		public TestCaseSourceAttribute(string sourceName) : base(sourceName)
		{
		}
	}
}
