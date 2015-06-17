using System;

// ReSharper disable once CheckNamespace

namespace Atrico.Lib.Testing.TestAttributes.NUnit
{
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
