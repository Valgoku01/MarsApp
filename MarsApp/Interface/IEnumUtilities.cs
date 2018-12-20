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

        /// <summary>
        /// Get the direction as string
        /// </summary>
        /// <param name="direction">Direction to translate</param>
        /// <returns>Direction as string</returns>
        string GetDirectionString(Direction direction);
    }
}
