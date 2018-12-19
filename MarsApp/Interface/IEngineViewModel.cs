using System;

namespace MarsApp.Interface
{
    /// <summary>
    /// Interface of the engine which parse data and calculate the new positions of rovers
    /// </summary>
    public interface IEngineViewModel : IDisposable
    {
        /// <summary>
        /// Parse and save the data
        /// </summary>
        /// <param name="paramsToParse">Parameters to handle</param>
        /// <returns>True if everything is set, false otherwise</returns>
        bool ParseData(string[] paramsToParse);

        /// <summary>
        /// Calculate moves of rovers over the map
        /// Each rover controls its own behavior
        /// </summary>
        /// <return>Array of string which represents final positions of rovers, empty array otherwise</return>
        string[] MakeMoves();
    }
}
