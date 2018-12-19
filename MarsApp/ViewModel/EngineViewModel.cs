using Unity;
using System.Collections.Generic;
using MarsApp.Interface;
using MarsApp.Model;

namespace MarsApp.ViewModel
{
    /// <summary>
    /// Main class to handle moves of rovers on the map
    /// </summary>
    public class EngineViewModel : IEngineViewModel
    {
        #region Attributes
        private IUnityContainer _container;

        private int _length_x = -1;
        private int _length_y = -1;
        private IList<RoverModel> _rovers;
        #endregion

        #region Functions
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="container">container</param>
        public EngineViewModel(IUnityContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// Parse and register the data
        /// </summary>
        /// <param name="paramsToParse">Parameters to handle</param>
        /// <returns>True if everything is set, false otherwise</returns>
        public bool ParseData(string[] paramsToParse)
        {
            if (paramsToParse == null || paramsToParse.Length != 1) return false;

            var fileUtilities = _container.Resolve<IFileUtilities>();
            if (fileUtilities == null) return false;

            var data = fileUtilities.ParseFile(_container, paramsToParse[0]);
            if (data.Count == 0) return false;

            var dimensionMap = data[0].Split(' ');
            if (dimensionMap.Length != 2) return false;

            if (int.TryParse(dimensionMap[0], out int x))
                _length_x = x;

            if (int.TryParse(dimensionMap[1], out int y))
                _length_y = y;

            // if no lenght no need to go further
            if (_length_x < 0 || _length_y < 0)
            {
                ReleaseAttributes();
                return false;
            }

            // build rovers
            _rovers = new List<RoverModel>();
            for (var i = 1; i < data.Count && i + 1 < data.Count; i += 2)
            {
                var newCar = new RoverModel(_container, _length_x, _length_y);
                if (newCar.StartRover(data[i], data[i + 1]))
                    _rovers.Add(newCar);
            }

            if (_rovers.Count == 0)
            {
                ReleaseAttributes();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Calculate moves of rovers over the map
        /// Each rover controls its own behavior
        ///     compared to what the system gives him about its knowlege of rovers
        /// </summary>
        /// <return>Array of string which represents final positions of rovers, null otherwise</return>
        public string[] MakeMoves()
        {
            var counter = 0;
            var moves = CalculateNewPositions();
            var finalMoves = new string[moves.Count];
            foreach(var move in moves)
            {
                finalMoves.SetValue(move, counter);
                counter += 1;
            }

            return finalMoves;
        }

        /// <summary>
        /// Release memory
        /// </summary>
        public void Dispose()
        {
            ReleaseAttributes();
        }

        /// <summary>
        /// Calculate the new position of all cars
        /// if a car is already on the aim area, the area won't be available
        /// the car will not move from its original position
        /// </summary>
        private IList<string> CalculateNewPositions()
        {
            var newPositions = new List<string>();

            if (_rovers == null || _rovers.Count == 0)
                return newPositions;

            foreach(var car in _rovers)
            {
                var newPosition = car.CalculateNewPosition();
                if (!string.IsNullOrEmpty(newPosition))
                    newPositions.Add(newPosition);
            }

            return newPositions;
        }

        /// <summary>
        /// Release attributes
        /// </summary>
        private void ReleaseAttributes()
        {
            if (_rovers != null)
            {
                foreach (var rover in _rovers)
                    rover.Dispose();
                _rovers = null;
            }

            _length_x = -1;
            _length_y = -1;
        }
        #endregion
    }
}
