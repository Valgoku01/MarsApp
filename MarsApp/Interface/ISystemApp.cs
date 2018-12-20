using Unity;

namespace MarsApp.Interface
{
    /// <summary>
    /// Interface to set the system
    /// </summary>
    public interface ISystemApp
    {
        /// <summary>
        /// Launch the app
        /// Could be threaded
        /// </summary>
        /// <param name="params">parameters of the app</param>
        /// <param name="container">Unity container</param>
        void Run(string[] parameters, IUnityContainer container);
    }
}
