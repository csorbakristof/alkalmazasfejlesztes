using System.Linq;

namespace Robot
{
    public class LineSensor
    {
        private readonly IRobot robot;

        public LineSensor(IRobot robot)
        {
            this.robot = robot;
        }

        public int[] Scan()
        {
            // Scan in front of the vehicle +/-30 degrees, 11 pixels wide
            return robot.Environment.ScanRelative(robot.LocationOrientation, -30.0, 10.0, 30.0, 10.0)
                .ToArray();
        }
    }
}
