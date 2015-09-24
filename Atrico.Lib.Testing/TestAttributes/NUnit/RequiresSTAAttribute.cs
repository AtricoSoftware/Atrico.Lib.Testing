using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atrico.Lib.Testing.TestAttributes.NUnit
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class RequiresSTAAttribute : global::NUnit.Framework.RequiresSTAAttribute
    {
    }
}
