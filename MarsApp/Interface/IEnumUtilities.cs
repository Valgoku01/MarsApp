using static MarsApp.Utilities.EnumUtilities;

namespace MarsApp.Interface
{
    /// <summary>
    /// Interface for enum utilites
    /// </summary>
    public interface IEnumUtilities
    {
        /// <summary>
        /// Transform the direction in char into enum Direction
        /// </summary>
        /// <param name="str">char to compare</param>
        /// <returns>Direction as enum</returns>
        Direction GetDirection(char str);
    }
}
