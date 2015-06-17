using System;

namespace Atrico.Lib.Testing.TestAttributes.NUnit
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	public class TestFixtureAttribute : global::NUnit.Framework.TestFixtureAttribute
	{
		// Summary:
		//     Default constructor
		public TestFixtureAttribute()
		{
		}

		//
		// Summary:
		//     Construct with a object[] representing a set of arguments. In .NET 2.0, the
		//     arguments may later be separated into type arguments and constructor arguments.
		//
		// Parameters:
		//   arguments:
		public TestFixtureAttribute(params object[] arguments) : base(arguments)
		{
		}
	}
}
