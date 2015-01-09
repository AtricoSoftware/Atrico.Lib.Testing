using System;

// ReSharper disable once CheckNamespace

namespace Atrico.Lib.Testing.NUnitAttributes
{
	public class TestCaseSourceAttribute : NUnit.Framework.TestCaseSourceAttribute
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
