using System;

namespace MarsApp.Interface
{
    /// <summary>
    /// Interface of the rover
    /// </summary>
    public interface IRover : IDisposable
    {
        /// <summary>
        /// Start the rover
        /// </summary>
        /// <param name="position">Initial position</param>
        /// <param name="movement">New movements</param>
        /// <returns>True if rover is started, false otherwise</returns>
        bool StartRover(string position, string movement);

        /// <summary>
        /// Calculate the position of the rover after its moves
        /// </summary>
        /// <returns>The new position in string type, string.empty otherwise</returns>
        string CalculateNewPosition();
    }
}
