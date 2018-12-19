using MarsApp.Interface;
using MarsApp.Utilities;
using System;
using Unity;

namespace MarsApp.ViewModel
{
    /// <summary>
    /// Class of the system
    /// </summary>
    public class SystemApp : ISystem
    {
        private IUnityContainer _container;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="container">Unity container</param>
        public SystemApp(IUnityContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// Init the system
        /// </summary>
        public void InitSystem()
        {
            var byteUtilities = _container.Resolve<ByteUtilities>();
            var enumUtilities = _container.Resolve<EnumUtilities>();
            var fileUtilities = _container.Resolve<FileUtilities>();

            _container.RegisterType<IByteUtilities, ByteUtilities>();
            _container.RegisterType<IEnumUtilities, EnumUtilities>();
            _container.RegisterType<IFileUtilities, FileUtilities>();

            _container.RegisterInstance(typeof(IByteUtilities), byteUtilities as IByteUtilities);
            _container.RegisterInstance(typeof(IEnumUtilities), enumUtilities as IEnumUtilities);
            _container.RegisterInstance(typeof(IFileUtilities), fileUtilities as IFileUtilities);
        }

        /// <summary>
        /// Run the app
        /// </summary>
        /// <param name="parameters">Parameters of the app</param>
        public void Run(string[] parameters)
        {
            // create main object
            var _engine = new EngineViewModel(_container);

            // store the data
            if (_engine != null && _engine.ParseData(parameters))
            {
                // make the moves
                var moves = _engine.MakeMoves();
                if (moves.Length > 0)
                {
                    // console log the moves
                    foreach (var move in moves)
                        Console.WriteLine(move);
                }
            }

            // release main object
            if (_engine != null)
                _engine.Dispose();
        }
    }
}
