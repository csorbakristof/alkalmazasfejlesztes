using Robot;
using System;
using System.Collections.Generic;
using System.Text;

namespace WallFollowerRobot
{
    public class SmartSideSensor
    {
        private readonly IRobot robot;
        private readonly IDistanceSensor distanceSensor;
        private double relativeDirection;
        private int minMapValueForObstacles;
        private int maxDistance;

        public SmartSideSensor(IRobot robot, IDistanceSensor distanceSensor, double relativeDirection, int minMapValueForObstacles, int maxDistance)
        {
            this.robot = robot;
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
