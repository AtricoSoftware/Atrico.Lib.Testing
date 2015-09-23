using System;

namespace Atrico.Lib.Testing.TestAttributes.NUnit
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class IgnoreAttribute : global::NUnit.Framework.IgnoreAttribute
    {
    }
}