using MarsApp.Interface;
using MarsApp.Utilities;
using MarsAppTest.Mocks;
using System;
using Unity;

namespace MarsAppTest
{
    /// <summary>
    /// Mother class of test classes needing mocks
    /// Could also use the class 'Mock' for mocking classes of the app
    /// Needs more dependencies
    /// </summary>
    public class BaseApp : IDisposable
    {
        protected UnityContainer Container;

        protected ByteUtilitiesMock ByteUtilities;
        protected FileUtilitiesMock FileUtilities;

        public BaseApp()
        {
            Container = new UnityContainer();

            ByteUtilities = Container.Resolve<ByteUtilitiesMock>();
            FileUtilities = Container.Resolve<FileUtilitiesMock>();

            Container.RegisterType<IByteUtilities, ByteUtilitiesMock>();
            Container.RegisterType<IEnumUtilities, EnumUtilities>();
            Container.RegisterType<IFileUtilities, FileUtilitiesMock>();

            Container.RegisterInstance(typeof(ByteUtilitiesMock), ByteUtilities as IByteUtilities);
            Container.RegisterInstance(typeof(FileUtilitiesMock), FileUtilities as IFileUtilities);
        }

        public void Dispose()
        {
            if (Container != null)
                Container.Dispose();
        }
    }
}
