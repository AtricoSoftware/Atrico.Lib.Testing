using System;

namespace Atrico.Lib.Testing.TestAttributes.NUnit
{
    public class ValueSourceAttribute : global::NUnit.Framework.ValueSourceAttribute
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
}
