using System;
using System.Collections.Generic;
using System.Text;

namespace Robot
{
    public class BeaconProximitySensor
    {
        private readonly IRobot robot;
        private readonly double maxDistance;

        public BeaconProximitySensor(IRobot robot, double maxDistance)
        {
            this.robot = robot;
            this.maxDistance = maxDistance;
        }

        public IEnumerable<int> GetCloseBeaconIds()
        {
            return robot.Environment.GetCloseBeaconIds(
                robot.Location.X, robot.Location.Y, maxDistance);
        }
    }
}
