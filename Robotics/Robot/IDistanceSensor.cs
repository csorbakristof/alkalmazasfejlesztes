using System;
using System.Collections.Generic;
using System.Text;

namespace Robot
{
    public interface IDistanceSensor
    {
        /// <summary>
        /// Gets the distance of the first map location having an obstacle.
        /// Obstacles are map locations having a value at least minMapValueForObstacles.
        /// </summary>
        /// <param name="relativeDirection"></param>
        /// <param name="minMapValueForObstacles"></param>
        /// <param name="maxDistance"></param>
        /// <returns>Distance or maxDistance if no obstacles were found.</returns>
        double GetDistance(double relativeDirection, int minMapValueForObstacles, int maxDistance);
    }
}
