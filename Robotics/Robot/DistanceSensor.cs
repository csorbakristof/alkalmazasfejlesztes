using System.Linq;

namespace Robot
{
    public class DistanceSensor
    {
        private readonly IRobot robot;

        public DistanceSensor(IRobot robot)
        {
            this.robot = robot;
        }

        /// <summary>
        /// Gets the distance of the first map location having an obstacle.
        /// Obstacles are map locations having a value at least minMapValueForObstacles.
        /// </summary>
        /// <param name="relativeDirection"></param>
        /// <param name="minMapValueForObstacles"></param>
        /// <param name="maxDistance"></param>
        /// <returns>Distance or maxDistance if no obstacles were found.</returns>
        public double GetDistance(double relativeDirection, int minMapValueForObstacles, int maxDistance)
        {
            var scan = robot.Environment.ScanRelative(robot.LocationOrientation,
                relativeDirection, 0.0, relativeDirection, maxDistance).ToArray();
            var dist = scan.TakeWhile(v => v < minMapValueForObstacles).Count();
            return dist;
        }
    }
}
