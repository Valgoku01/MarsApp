using MarsApp.Interface;
using System;
using Unity;

namespace MarsApp.ViewModel
{
    /// <summary>
    /// Class of the system
    /// </summary>
    public class SystemAppViewModel : ISystemApp
    {
        /// <summary>
        /// Run the app
        /// </summary>
        /// <param name="parameters">Parameters of the app</param>
        public void Run(string[] parameters, IUnityContainer container)
        {
            // create main object
            var _engine = new EngineViewModel(container);

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
