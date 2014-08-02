using System;

namespace Atrico.Lib.Testing.NUnitAttributes
{
	public class ValueSourceAttribute : NUnit.Framework.ValueSourceAttribute
	{
		public ValueSourceAttribute(string sourceName)	
		: base(sourceName)
		{
		}

		public ValueSourceAttribute(Type sourceType, string sourceName)
			: base(sourceType, sourceName)
		{
		}
	}

	public class ValuesAttribute : NUnit.Framework.ValuesAttribute
	{
		public ValuesAttribute(object arg1)
			: base(arg1)
		{
			
		}
		public ValuesAttribute(params object[] args)
			: base(args)
		{
			
		}
		public ValuesAttribute(object arg1, object arg2)
			: base(arg1, arg2)
		{
			
		}
		public ValuesAttribute(object arg1, object arg2, object arg3)
			: base(arg1, arg2, arg3)
		{
			
		}
	}
}	