using Robot;

namespace WallFollowerRobot
{
    /// <summary>
    /// Wall detector sensor with OnWallDetected event.
    /// </summary>
    public class SmartSideSensor
    {
        private readonly DistanceSensor distanceSensor;
        private readonly double relativeDirection;
        private readonly int minMapValueForObstacles;
        private readonly int maxDistance;

        public SmartSideSensor(IRobot robot, DistanceSensor distanceSensor, double relativeDirection, int minMapValueForObstacles, int maxDistance)
        {
            this.distanceSensor = distanceSensor;
            this.relativeDirection = relativeDirection;
            this.minMapValueForObstacles = minMapValueForObstacles;
            this.maxDistance = maxDistance;
            robot.OnTick += PollSensorAndFireEventIfNeeded;
        }

        private void PollSensorAndFireEventIfNeeded()
        {
            double dist = distanceSensor.GetDistance(relativeDirection, minMapValueForObstacles, maxDistance);
            if (dist < maxDistance)
                OnWallDetected?.Invoke(dist);
        }

        public event OnWallDetectedDelegate OnWallDetected;

        public delegate void OnWallDetectedDelegate(double distance);
    }
}
