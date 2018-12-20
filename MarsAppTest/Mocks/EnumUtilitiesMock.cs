using MarsApp.Interface;
using MarsApp.Utilities;

namespace MarsAppTest.Mocks
{
    /// <summary>
    /// Mock of EnumUtilities
    /// </summary>
    public class EnumUtilitiesMock : IEnumUtilities
    {
        public EnumUtilities.Direction Direction { get; set; }
        public string DirectionStr { get; set; }

        public EnumUtilities.Direction GetDirection(char str)
        {
            return Direction;
        }

        public string GetDirectionString(EnumUtilities.Direction direction)
        {
            return DirectionStr;
        }
    }
}
