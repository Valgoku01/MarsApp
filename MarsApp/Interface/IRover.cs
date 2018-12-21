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
        /// <param name="max_size_x">The rover can not go further than max_size_x in x axe</param>
        /// <param name="map_size_y">The rover can not go further than max_size_y in y axe</param>
        /// <param name="position">Initial position</param>
        /// <param name="movement">New movements</param>
        /// <returns>True if rover is started, false otherwise</returns>
        bool StartRover(int max_size_x, int map_size_y, string position, string movement);

        /// <summary>
        /// Calculate the position of the rover after its moves
        /// </summary>
        /// <returns>The new position in string type, string.empty otherwise</returns>
        string CalculateNewPosition();
    }
}
