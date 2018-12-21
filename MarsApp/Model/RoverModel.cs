using System;
using MarsApp.Interface;
using MarsApp.Utilities;
using Unity;

namespace MarsApp.Model
{
    /// <summary>
    /// Model of the rover
    /// Handle borders
    /// Does not stop if infinite moves
    /// </summary>
    public class RoverModel : IRover
    {
        #region Attributes
        private IUnityContainer _container;
        private long _maximum_x = -1;
        private long _maximum_y = -1;
        private long _initial_position_x = -1;
        private long _initial_position_y = -1;
        private long _final_position_x = -1;
        private long _final_position_y = -1;
        private EnumUtilities.Direction _initial_direction = EnumUtilities.Direction.WRONG;
        private EnumUtilities.Direction _final_direction = EnumUtilities.Direction.WRONG;
        private char[] _movements = null;
        #endregion

        #region Functions
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="container">container</param>
        public RoverModel(IUnityContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// Start the rover
        /// </summary>
        /// <param name="max_size_x">The rover can not go further than max_size_x in x axe</param>
        /// <param name="map_size_y">The rover can not go further than max_size_y in y axe</param>
        /// <param name="position">Initial position</param>
        /// <param name="movement">New movements</param>
        /// <returns>True if rover is started, false otherwise</returns>
        public bool StartRover(int max_size_x, int map_size_y, string position, string movement)
        {
            var byteUtilities = _container.Resolve<IByteUtilities>();
            if (byteUtilities == null) return false;

            if (max_size_x < 1 || map_size_y < 1) return false;
            if (string.IsNullOrEmpty(position) || string.IsNullOrEmpty(movement)) return false;

            var enumUtilities = _container.Resolve<IEnumUtilities>();
            if (enumUtilities == null) return false;

            var list = position.Split(' ');
            if (list.Length == 0 || list.Length != 3) return false;

            _maximum_x = max_size_x;
            _maximum_y = map_size_y;

            if (int.TryParse(list[0], out int x))
                _initial_position_x = x;

            if (int.TryParse(list[1], out int y))
                _initial_position_y = y;

            try
            {
                // ToCharArray can throw exception
                _initial_direction = enumUtilities.GetDirection(list[2].ToCharArray()[0]);
                if (_initial_direction == EnumUtilities.Direction.WRONG)
                {
                    ReleaseMemory();
                    return false;
                }
            }
            catch (Exception e)
            {
                var exception = e;
                ReleaseMemory();
                return false;
            }
            

            var charsToTrim = new char[] {' ', '\t'};
            _movements = movement.Trim(charsToTrim).ToCharArray();

            if (!IsRoverReadyToDrive())
            {
                ReleaseMemory();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Calculate the position of the rover after its moves
        /// </summary>
        public string CalculateNewPosition()
        {
            try
            {
                var enumUtilities = _container.Resolve<EnumUtilities>();
                var strToRet = string.Empty;
                var direction = enumUtilities.GetDirectionString(_initial_direction);
                if (direction != Constants.WRONG)
                    strToRet = string.Format("{0} {1} {2}", _initial_position_x, _initial_position_y, direction);

                return IsRoverReadyToDrive() ?
                    CalculatePosition() : CanUseInitialPositions() ?
                        strToRet : string.Empty;
            }
            catch (Exception e)
            {
                // to have 0 warnings
                var exception = e;
                return string.Empty;
            }
        }

        /// <summary>
        /// Release memory
        /// </summary>
        public void Dispose()
        {
            ReleaseMemory();
        }

        /// <summary>
        /// Are initial positions set ?
        /// </summary>
        /// <returns>True, false otherwise</returns>
        private bool CanUseInitialPositions()
        {
            return _initial_position_x >= 0 && _initial_position_y >= 0 &&
                _initial_direction != EnumUtilities.Direction.WRONG;
        }

        /// <summary>
        /// Does the rover have some moves to do ?
        /// </summary>
        /// <returns>True, false otherwise</returns>
        private bool IsRoverReadyToDrive()
        {
            return CanUseInitialPositions() && _movements != null && _movements.Length > 0;
        }

        /// <summary>
        /// Algo to find last position of the rover 
        /// </summary>
        private string CalculatePosition()
        {
            var enumUtilities = _container.Resolve<IEnumUtilities>();
            if (enumUtilities == null) return string.Empty;

            var currentDirection = _initial_direction;
            var currentX = _initial_position_x;
            var currentY = _initial_position_y;

            // algo
            var continueParsing = true;
            for(var i = 0; i < _movements.Length && continueParsing; i++)
            {
                var move = enumUtilities.GetDirection(_movements[i]);
                if (move != EnumUtilities.Direction.WRONG)
                {
                    if (move == EnumUtilities.Direction.M)
                        Move(currentDirection, ref currentX, ref currentY);
                    else if (move == EnumUtilities.Direction.L)
                        ChangeDirection(EnumUtilities.Direction.L, currentDirection, ref currentDirection);
                    else if (move == EnumUtilities.Direction.R)
                        ChangeDirection(EnumUtilities.Direction.R, currentDirection, ref currentDirection);
                }
                else
                {
                    continueParsing = false;
                }
            }

            if (currentX < 0 || currentY < 0 || !continueParsing)
            {
                return string.Empty;
            }

            var strToRet = string.Empty;
            try
            {
                var direction = enumUtilities.GetDirectionString(currentDirection);
                if (direction != Constants.WRONG)
                    strToRet = string.Format("{0} {1} {2}", currentX, currentY, direction);
            }
            catch (Exception e)
            {
                // to have 0 warnings
                var exception = e;
                strToRet = string.Empty;
            }

            var isStrToRetNullOrEmpty = string.IsNullOrEmpty(strToRet);

            SetFinalAttributes(isStrToRetNullOrEmpty ? -1 : currentX,
                isStrToRetNullOrEmpty ? -1 : currentY,
                isStrToRetNullOrEmpty ? EnumUtilities.Direction.WRONG : currentDirection);

            return strToRet;
        }

        /// <summary>
        /// Move forward
        /// </summary>
        /// <param name="direction">New direction</param>
        /// <param name="x">x to modify if needed</param>
        /// <param name="y">y to modify if needed</param>
        private void Move(EnumUtilities.Direction direction, ref long x, ref long y)
        {
            long res = 0;
            switch (direction)
            {
                case EnumUtilities.Direction.E:
                    res = x + Constants.PAD_X;
                    if (res <= _maximum_x)
                        x += Constants.PAD_X;
                    break;
                case EnumUtilities.Direction.O:
                    res = x - Constants.PAD_X;
                    if (res >= 0)
                        x -= Constants.PAD_X;
                    break;
                case EnumUtilities.Direction.N:
                    res = y + Constants.PAD_Y;
                    if (res <= _maximum_y)
                        y += Constants.PAD_Y;
                    break;
                case EnumUtilities.Direction.S:
                    res = y - Constants.PAD_Y;
                    if (res >= 0)
                        y -= Constants.PAD_Y;
                    break;
            }
        }

        /// <summary>
        /// Change the direction of the rover
        /// </summary>
        /// <param name="move">Move</param>
        /// <param name="direction">New direction to take in account</param>
        /// <param name="finalNewDirection">Final new direction</param>
        private void ChangeDirection(EnumUtilities.Direction move, EnumUtilities.Direction direction, ref EnumUtilities.Direction finalNewDirection)
        {
            var newDirection = direction;
            switch (direction)
            {
                case EnumUtilities.Direction.N:
                    newDirection = move == EnumUtilities.Direction.L ? EnumUtilities.Direction.O : EnumUtilities.Direction.E;
                    break;
                case EnumUtilities.Direction.E:
                    newDirection = move == EnumUtilities.Direction.L ? EnumUtilities.Direction.N : EnumUtilities.Direction.S;
                    break;
                case EnumUtilities.Direction.O:
                    newDirection = move == EnumUtilities.Direction.L ? EnumUtilities.Direction.S : EnumUtilities.Direction.N;
                    break;
                case EnumUtilities.Direction.S:
                    newDirection = move == EnumUtilities.Direction.L ? EnumUtilities.Direction.E : EnumUtilities.Direction.O;
                    break;
            }
            finalNewDirection = newDirection;
        }

        /// <summary>
        /// Set the initial attributes
        /// </summary>
        /// <param name="x">New x position</param>
        /// <param name="y">New y position</param>
        /// <param name="direction">New direction</param>
        private void SetInitialAttributes(int x, int y, EnumUtilities.Direction direction)
        {
            _initial_position_x = x;
            _initial_position_y = y;
            _initial_direction = direction;
        }

        /// <summary>
        /// Set the final attributes
        /// </summary>
        /// <param name="x">New x position</param>
        /// <param name="y">New y position</param>
        /// <param name="direction">New direction</param>
        private void SetFinalAttributes(long x, long y, EnumUtilities.Direction direction)
        {
            _final_position_x = x;
            _final_position_y = y;
            _final_direction = direction;
        }

        /// <summary>
        /// Release attributes
        /// </summary>
        private void ReleaseMemory()
        {
            SetInitialAttributes(-1, -1, EnumUtilities.Direction.WRONG);
            SetFinalAttributes(-1, -1, EnumUtilities.Direction.WRONG);
            _movements = null;
            _maximum_x = -1;
            _maximum_y = -1;
        }
        #endregion
    }
}
