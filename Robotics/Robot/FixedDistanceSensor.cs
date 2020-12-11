using System;
using System.Collections.Generic;
using System.Text;

namespace Robot
{
    /// <summary>
    /// Swaps the DistanceSensor class with presets
    /// </summary>
    public class FixedDistanceSensor
    {
        private readonly DistanceSensor sensor;
        private readonly double relativeDirection;
        private readonly int minMapValueForObstacles;
        private readonly int maxDistance;

        public FixedDistanceSensor(IRobot robot, double relativeDirection, int minMapValueForObstacles, int maxDistance)
        {
            sensor = new DistanceSensor(robot);
            this.relativeDirection = relativeDirection;
            this.minMapValueForObstacles = minMapValueForObstacles;
            this.maxDistance = maxDistance;
        }

        public double GetDistance()
        {
            return sensor.GetDistance(relativeDirection, minMapValueForObstacles, maxDistance);
        }


    }
}
