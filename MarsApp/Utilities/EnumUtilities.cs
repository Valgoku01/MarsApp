using MarsApp.Interface;

namespace MarsApp.Utilities
{
    /// <summary>
    /// Enum utilities
    /// </summary>
    public class EnumUtilities : IEnumUtilities
    {
        public enum Direction { WRONG = -1, N, S, E, O, M, L, R };

        /// <summary>
        /// Transform the direction in char into enum Direction
        /// </summary>
        /// <param name="str">char to compare</param>
        /// <returns>Direction as enum</returns>
        public Direction GetDirection(char str)
        {
            switch (str)
            {
                case 'N':
                    return Direction.N;
                case 'S':
                    return Direction.S;
                case 'E':
                    return Direction.E;
                case 'O':
                    return Direction.O;
                case 'M':
                    return Direction.M;
                case 'L':
                    return Direction.L;
                case 'R':
                    return Direction.R;
            }

            return Direction.WRONG;
        }

        /// <summary>
        /// Get the direction as string
        /// </summary>
        /// <param name="direction">Direction to translate</param>
        /// <returns>Direction as string</returns>
        public static string GetDirectionString(Direction direction)
        {
            switch (direction)
            {
                case Direction.N:
                    return "N";
                case Direction.S:
                    return "S";
                case Direction.E:
                    return "E";
                case Direction.O:
                    return "O";
                case Direction.M:
                    return "M";
                case Direction.R:
                    return "R";
                case Direction.L:
                    return "L";
            }

            return Constants.WRONG.ToUpper();
        }
    }
}
