using MarsApp.Interface;
using MarsApp.Utilities;
using System;
using Unity;

namespace MarsApp
{
    /// <summary>
    /// Main container for the app
    /// </summary>
    public class Base : IDisposable
    {
        public IUnityContainer Container;

        /// <summary>
        /// Constructor
        /// </summary>
        public Base()
        {
            Container = new UnityContainer();
        }

        /// <summary>
        /// Init the base
        /// </summary>
        public bool InitBase()
        {
            try
            {
                var byteUtilities = Container.Resolve<ByteUtilities>();
                var enumUtilities = Container.Resolve<EnumUtilities>();
                var fileUtilities = Container.Resolve<FileUtilities>();

                Container.RegisterType<IByteUtilities, ByteUtilities>();
                Container.RegisterType<IEnumUtilities, EnumUtilities>();
                Container.RegisterType<IFileUtilities, FileUtilities>();

                Container.RegisterInstance(typeof(ByteUtilities), byteUtilities as IByteUtilities);
                Container.RegisterInstance(typeof(EnumUtilities), enumUtilities as IEnumUtilities);
                Container.RegisterInstance(typeof(FileUtilities), fileUtilities as IFileUtilities);
            }
            catch(Exception e)
            {
                // to have 0 warning
                var exception = e;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Release memory
        /// </summary>
        public void Dispose()
        {
            if (Container != null)
                Container.Dispose();
        }
    }
}
