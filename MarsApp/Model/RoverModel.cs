using System;
using System.Collections.Generic;
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
        private int _maximum_x = -1;
        private int _maximum_y = -1;
        private int _initial_position_x = -1;
        private int _initial_position_y = -1;
        private int _final_position_x = -1;
        private int _final_position_y = -1;
        private EnumUtilities.Direction _initial_direction = EnumUtilities.Direction.WRONG;
        private EnumUtilities.Direction _final_direction = EnumUtilities.Direction.WRONG;
        private List<char> _movements = null;
        #endregion

        #region Functions
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="container">container</param>
        /// <param name="max_size_x">The rover can not go further than max_size_x in x axe</param>
        /// <param name="map_size_y">The rover can not go further than max_size_y in y axe</param>
        public RoverModel(IUnityContainer container, int max_size_x, int map_size_y)
        {
            _container = container;
            _maximum_x = max_size_x;
            _maximum_y = map_size_y;
        }

        /// <summary>
        /// Start the rover
        /// </summary>
        /// <param name="position">Initial position</param>
        /// <param name="movement">New movements</param>
        /// <returns>True if rover is started, false otherwise</returns>
        public bool StartRover(string position, string movement)
        {
            if (_maximum_x < 0 || _maximum_y < 0) return false;
            if (string.IsNullOrEmpty(position) || string.IsNullOrEmpty(movement) || position.Length != 5) return false;

            var enumUtilities = _container.Resolve<IEnumUtilities>();
            if (enumUtilities == null) return false;

            var list = position.Split(' ');
            if (list.Length == 0 || list.Length != 3) return false;

            var p_str = "";
            p_str = list[0];
            if (int.TryParse(p_str, out int x))
                _initial_position_x = x;

            p_str = "";
            p_str = list[1];
            if (int.TryParse(p_str, out int y))
                _initial_position_y = y;

            _initial_direction = enumUtilities.GetDirection(position[4]);
            if (_initial_direction == EnumUtilities.Direction.WRONG)
            {
                ReleaseMemory();
                return false;
            }

            _movements = new List<char>();
            for (var i = 0; i < movement.Length; i++)
                _movements.Add(movement[i]);

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
            return IsRoverReadyToDrive() ? CalculatePosition() : string.Empty;
        }

        /// <summary>
        /// Release memory
        /// </summary>
        public void Dispose()
        {
            ReleaseMemory();
        }

        /// <summary>
        /// Does the rover have some moves to do ?
        /// </summary>
        /// <returns>True, false otherwise</returns>
        private bool IsRoverReadyToDrive()
        {
            return _initial_position_x >= 0 && _initial_position_y >= 0 &&
                _movements != null && _movements.Count > 0 &&
                _initial_direction != EnumUtilities.Direction.WRONG;
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
            for(var i = 0; i < _movements.Count && continueParsing; i++)
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
                return string.Empty;

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
        private void Move(EnumUtilities.Direction direction, ref int x, ref int y)
        {
            var res = 0;
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
        private void SetFinalAttributes(int x, int y, EnumUtilities.Direction direction)
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
        }
        #endregion
    }
}
