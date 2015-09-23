using System;

namespace Atrico.Lib.Testing.TestAttributes.NUnit
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class TestFixtureSetUpAttribute : global::NUnit.Framework.TestFixtureSetUpAttribute
    {
    }
}