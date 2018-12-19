using System;

namespace MarsApp.Interface
{
    /// <summary>
    /// Interface to set the system
    /// </summary>
    public interface ISystem : IDisposable
    {
        /// <summary>
        /// Init the system
        /// </summary>
        void InitSystem();

        /// <summary>
        /// Launch the app
        /// Could be threaded
        /// </summary>
        /// <param name="params">parameters of the app</param>
        void Run(string[] parameters);
    }
}
