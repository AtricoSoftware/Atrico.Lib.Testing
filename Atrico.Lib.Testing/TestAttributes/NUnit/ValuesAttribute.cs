using System;

namespace Atrico.Lib.Testing.TestAttributes.NUnit
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public class ValuesAttribute : global::NUnit.Framework.ValuesAttribute
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