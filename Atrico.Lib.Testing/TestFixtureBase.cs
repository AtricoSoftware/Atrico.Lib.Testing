using System;
using Atrico.Lib.Common;
using Moq;
using Version = Atrico.Lib.Common.SemanticVersion.Version;

namespace Atrico.Lib.Testing
{
    public abstract class TestFixtureBase
    {
        private readonly Lazy<IRunContextInfo> _runContextInfo = new Lazy<IRunContextInfo>(CreateRunContextInfo);
        /// <summary>
        ///     Random value generator
        /// </summary>
        protected readonly RandomValueGenerator RandomValues = new RandomValueGenerator();


        protected IRunContextInfo MockRunContext
        {
            get { return _runContextInfo.Value; }
        }

        private static IRunContextInfo CreateRunContextInfo()
        {
            var mock = new Mock<IRunContextInfo>();
            mock.Setup(rc => rc.EntryAssemblyName).Returns(@"assembly.name");
            mock.Setup(rc => rc.EntryAssemblyVersion).Returns(Version.From(1, 2, 3, 4));
            mock.Setup(rc => rc.EntryAssemblyCopyright).Returns(@"copyright");
            mock.Setup(rc => rc.EntryAssemblyPath).Returns(@"C:\test.exe");
            return mock.Object;
        }
    }
}